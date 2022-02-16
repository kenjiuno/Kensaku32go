using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using io = System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Data.SQLite;
using Kensaku32go.Utils;
using System.Security.Policy;
using LibIFilter;

namespace Kensaku32go
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SortedSet<string> allowedExts = new SortedSet<string>();
        private string dbFile;
        private SQLiteConnection db;

        public MainWindow()
        {
            InitializeComponent();

            Title += " v" + typeof(App).Assembly.GetCustomAttributes(false)
                .OfType<AssemblyFileVersionAttribute>()
                .Single()
                .Version;
            Title += " " + ((IntPtr.Size == 4) ? "(32 ビット)" : "(64 ビット)");

            foreach (var control in panelsPanel.Children.OfType<FrameworkElement>())
            {
                control.Visibility = Visibility.Collapsed;
            }

            allowedExts.Add(".pdf");

            allowedExts.Add(".doc");
            allowedExts.Add(".docx");
            allowedExts.Add(".xls");
            allowedExts.Add(".xlsx");
            allowedExts.Add(".ppt");
            allowedExts.Add(".pptx");

            allowedExts.Add(".txt");
            allowedExts.Add(".rtf");

            allowedExts.Add(".jtd");
            allowedExts.Add(".jtdc");

            allowedExts.Add(".odt");
            allowedExts.Add(".ods");
            allowedExts.Add(".odp");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dbFile = App.fpdb;

            if (String.IsNullOrEmpty(dbFile))
            {
                var sfd = new SaveFileDialog();
                sfd.DefaultExt = "Kensaku32go";
                sfd.Filter = "*.Kensaku32go|*.Kensaku32go";
                sfd.CheckPathExists = true;
                sfd.FileName = "検索32号の辞書";
                sfd.Title = "検索したい書類のフォルダで[保存]";
                if (!(sfd.ShowDialog() ?? false))
                {
                    Close();
                    return;
                }
                dbFile = sfd.FileName;
            }

            var builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = dbFile;
            builder.DateTimeKind = DateTimeKind.Utc;
            builder.JournalMode = SQLiteJournalModeEnum.Memory;

            db = new SQLiteConnection(builder.ConnectionString);
            db.Open();

            {
                var cm = new SQLiteCommand("create table if not exists f2(i integer primary key autoincrement, fp text, mt integer, cb integer, fts1 text);", db);
                cm.ExecuteNonQuery();
            }

            bwUpd.DoWork += bwUpd_DoWork;
            bwUpd.WorkerSupportsCancellation = true;
            bwUpd.RunWorkerCompleted += bwUpd_RunWorkerCompleted;
            bwUpd.RunWorkerAsync();
            panelUpdating.Visibility = Visibility.Visible;

            bwSe.DoWork += bwSe_DoWork;
            bwSe.RunWorkerCompleted += bwSe_RunWorkerCompleted;
            bwSe.ProgressChanged += bwSe_ProgressChanged;
            bwSe.WorkerReportsProgress = true;

            cvsent.Source = alent;
            lbItems.DataContext = cvsent;
        }

        void bwUpd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panelUpdating.Visibility = Visibility.Collapsed;

            if (e.Error == null)
            {
                panelUpdated.Visibility = Visibility.Visible;

                var stat = (UpdateStat)e.Result;
                if (stat != null)
                {
                    numUpdated.Text = $"{stat.numUpdated:#,##0}";
                    numFailed.Text = $"{stat.numFailed:#,##0}";
                }
            }
            if (e.Error != null)
            {
                panelUpdateFailed.Visibility = Visibility.Visible;
            }
            panelUpdateFailed.ToolTip = "" + e.Error;
        }

        class Entry
        {
            public Int64 AutoKey { get; set; }
            public String FilePath { get; set; }
            public Int64 ModTime { get; set; }
            public Int64 FileSize { get; set; }

            /// <summary>
            /// 削除しない
            /// </summary>
            public bool Keep { get; set; }

            public String FullText { get; set; }
            public String Name { get; set; }
            public String Dir { get; set; }
            public List<Hiti> Pos2 { get; set; }

            public List<HLPart> HLParts
            {
                get
                {
                    var list = new List<HLPart>();
                    if (Pos2 != null)
                        foreach (Hiti hi in Pos2)
                        {
                            int x1 = hi.x;
                            int x2 = x1 + hi.cx;
                            int x0 = Math.Max(0, hi.x - 10);
                            int x3 = Math.Min(FullText.Length, x2 + 10);

                            list.Add(new HLPart
                            {
                                Hit = SUt.L1(FullText.Substring(x1, x2 - x1)),
                                Prev = SUt.L1(FullText.Substring(x0, x1 - x0)),
                                Next = SUt.L1(FullText.Substring(x2, x3 - x2)),
                                Pos = x1
                            });
                        }
                    return list;
                }
            }

            class SUt
            {
                public static string L1(string s)
                {
                    return (s ?? "").Replace("\r", " ").Replace("\n", " ");
                }
            }

            public String When
            {
                get
                {
                    DateTime dt = new DateTime(ModTime, DateTimeKind.Utc);
                    TimeSpan ts = DateTime.UtcNow.Subtract(dt);
                    if (ts.TotalMinutes < 1) return String.Format("{0:0} 秒前", ts.TotalSeconds);
                    if (ts.TotalHours < 1) return String.Format("{0:0} 分前", ts.TotalMinutes);
                    if (ts.TotalDays < 1) return String.Format("{0:0} 時間前", ts.TotalHours);
                    if (ts.TotalDays < 31) return String.Format("{0:0.0} 日前", ts.TotalDays);
                    if (ts.TotalDays < 365) return String.Format("{0:0.0} 月前", ts.TotalDays / 30);
                    return String.Format("{0:0.0} 年前", ts.TotalDays / 365);
                }
            }
        }

        class HLPart
        {
            public String Prev { get; set; }
            public String Hit { get; set; }
            public String Next { get; set; }
            public int Pos { get; set; }

            public string GetDisp()
            {
                return Prev + "[" + Hit + "]" + Next;
            }
        }

        class UpdateStat
        {
            internal int numUpdated = 0;
            internal int numFailed = 0;
        }

        void bwUpd_DoWork(object sender, DoWorkEventArgs e)
        {
            var dirInfoList = new Stack<io.DirectoryInfo>();
            var ents = new SortedDictionary<string, Entry>();
            {
                if (App.dirs == null)
                {
                    App.dirs = new string[] { io.Path.GetDirectoryName(dbFile) };
                }

                using (var dr = new SQLiteCommand("select i,fp,mt,cb from f2", db).ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var fp = dr.GetString(1);
                        ents[fp] = (new Entry
                        {
                            AutoKey = dr.GetInt64(0),
                            FilePath = fp,
                            ModTime = dr.GetInt64(2),
                            FileSize = dr.GetInt64(3),
                        });
                    }
                }

                foreach (var dir in App.dirs)
                {
                    dirInfoList.Push(new io.DirectoryInfo(dir));
                }

                var cmUpdate = new SQLiteCommand("update f2 set mt=@mt,cb=@cb,fts1=@fts1 where i=@i", db);
                cmUpdate.Parameters.Add("mt", System.Data.DbType.Int64);
                cmUpdate.Parameters.Add("cb", System.Data.DbType.Int64);
                cmUpdate.Parameters.Add("fts1", System.Data.DbType.String);
                cmUpdate.Parameters.Add("i", System.Data.DbType.Int64);

                var cmInsert = new SQLiteCommand("insert into f2 (fp,mt,cb,fts1) values (@fp,@mt,@cb,@fts1)", db);
                cmInsert.Parameters.Add("fp", System.Data.DbType.String);
                cmInsert.Parameters.Add("mt", System.Data.DbType.Int64);
                cmInsert.Parameters.Add("cb", System.Data.DbType.Int64);
                cmInsert.Parameters.Add("fts1", System.Data.DbType.String);

                var stat = new UpdateStat();
                e.Result = stat;

                var walked = new HashSet<string>();

                while (dirInfoList.Count != 0)
                {
                    var dirInfo = dirInfoList.Pop();
                    if (IfSkip(dirInfo) || !walked.Add(dirInfo.FullName))
                    {
                        continue;
                    }
                    FileSystemInfo[] list;
                    try
                    {
                        list = dirInfo.GetFileSystemInfos();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                    foreach (var fsi in list)
                    {
                        if (bwUpd.CancellationPending)
                        {
                            return;
                        }
                        var fileInfo = fsi as io.FileInfo;
                        if (fsi is io.DirectoryInfo)
                        {
                            dirInfoList.Push(fsi as io.DirectoryInfo);
                        }
                        else if (fileInfo != null && Filter(fileInfo))
                        {
                            Entry entry;
                            bool needUpdate = true;
                            if (ents.TryGetValue(fileInfo.FullName, out entry))
                            {
                                if (entry.ModTime == fileInfo.LastWriteTimeUtc.Ticks && entry.FileSize == fileInfo.Length)
                                {
                                    needUpdate = false;
                                    entry.Keep = true;
                                }
                                else
                                {
                                    entry.Keep = true;
                                }
                            }
                            else
                            {
                                entry = new Entry
                                {
                                    AutoKey = -1,
                                    FilePath = fileInfo.FullName,
                                    ModTime = fileInfo.LastWriteTimeUtc.Ticks,
                                    FileSize = fileInfo.Length,

                                    Keep = true,
                                };
                            }
                            if (needUpdate)
                            {
                                try
                                {
                                    var text = IsPdf(fileInfo.FullName) ? GetTextUsingPdfium(fileInfo.FullName)
                                        : IsText(fileInfo.FullName) ? GetTextFromTextFile(fileInfo.FullName)
                                        : GetTextUsingIFilter(fileInfo.FullName);

                                    var text2 = ("" + text).Replace("￾", "").Normalize();

                                    if (entry.AutoKey != -1)
                                    {
                                        cmUpdate.Parameters["mt"].Value = fileInfo.LastWriteTimeUtc.Ticks;
                                        cmUpdate.Parameters["cb"].Value = fileInfo.Length;
                                        cmUpdate.Parameters["fts1"].Value = text2;
                                        cmUpdate.Parameters["i"].Value = entry.AutoKey;
                                        cmUpdate.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        cmInsert.Parameters["fp"].Value = fileInfo.FullName;
                                        cmInsert.Parameters["mt"].Value = fileInfo.LastWriteTimeUtc.Ticks;
                                        cmInsert.Parameters["cb"].Value = fileInfo.Length;
                                        cmInsert.Parameters["fts1"].Value = text2;
                                        cmInsert.ExecuteNonQuery();
                                    }

                                    ++stat.numUpdated;
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine("# {0}: {1}", fileInfo.FullName, err);
                                    ++stat.numFailed;
                                }
                            }
                        }
                    }
                }

                {
                    var cmDelete = new SQLiteCommand("delete from f2 where i=@i", db);
                    cmDelete.Parameters.Add("i", System.Data.DbType.Int64);

                    foreach (var entry in ents.Values.Where(it => !it.Keep))
                    {
                        cmDelete.Parameters["i"].Value = entry.AutoKey;
                        cmDelete.ExecuteNonQuery();
                    }
                }
            }
        }

        private bool IfSkip(DirectoryInfo dirInfo)
        {
            var name = dirInfo.Name.ToLowerInvariant();
            return name == "appdata" || name.StartsWith(".");
        }

        private string GetTextFromTextFile(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            return (DobonJcode.GetCode(bytes) ?? Encoding.GetEncoding(932)).GetString(bytes);
        }

        private string GetTextUsingPdfium(string filePath)
        {
            var writer = new StringWriter();
            using (var pdf = PdfiumViewer.PdfDocument.Load(filePath))
            {
                for (var x = 0; x < pdf.PageCount; x++)
                {
                    writer.WriteLine(pdf.GetPdfText(x));
                }
            }
            return writer.ToString();
        }

        private static bool IsPdf(string filePath) => Path.GetExtension(filePath).ToLowerInvariant() == ".pdf";
        private static bool IsText(string filePath) => Path.GetExtension(filePath).ToLowerInvariant() == ".txt";

        private string GetTextUsingIFilter(string filePath)
        {
            return TextExtractor.GetTextUsingIFilterAsync(filePath).Result;
        }

        private bool Filter(FileInfo fileInfo) => allowedExts.Contains(fileInfo.Extension.ToLowerInvariant());

        BackgroundWorker bwUpd = new BackgroundWorker();

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (db != null)
            {
                db.Close();
                db = null;
            }
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            SeNow();
        }

        private void SeNow()
        {
            if (bwSe.IsBusy) return;

            alent.Clear();

            bwSe.RunWorkerAsync(tbKws.Text);

            bSearch.IsEnabled = false;
            panelSearching.Visibility = Visibility.Visible;
            panelSearchDone.Visibility = Visibility.Collapsed;
            panelSearchFail.Visibility = Visibility.Collapsed;
        }

        void bwSe_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bSearch.IsEnabled = true;
            panelSearching.Visibility = Visibility.Collapsed;
            rTot.Text = String.Format("{0:#,##0}", alent.Count);
            if (e.Error == null)
            {
                panelSearchDone.Visibility = Visibility.Visible;
                panelSearchFail.Visibility = Visibility.Collapsed;
                panelSearchFail.ToolTip = "";
            }
            else
            {
                panelSearchDone.Visibility = Visibility.Collapsed;
                panelSearchFail.Visibility = Visibility.Visible;
                panelSearchFail.ToolTip = "" + e.Error;
            }
        }

        class Hiti
        {
            public int x { get; set; }
            public int cx { get; set; }
        }

        class Uts
        {
            internal static Entry Search(string fts1, string[] kws)
            {
                bool all = true, any = false;
                List<Hiti> pos2 = new List<Hiti>();
                foreach (string kw in kws)
                {
                    int i = Strut2.IndexOf(fts1, kw, 0);
                    if (i >= 0)
                    {
                        any = true;
                        pos2.Add(new Hiti { x = i, cx = kw.Length });

                        int i2 = Strut2.IndexOf(fts1, kw, i + kw.Length);
                        if (i2 >= 0)
                        {
                            pos2.Add(new Hiti { x = i2, cx = kw.Length });

                            int i3 = Strut2.IndexOf(fts1, kw, i2 + kw.Length);
                            if (i3 >= 0)
                            {
                                pos2.Add(new Hiti { x = i3, cx = kw.Length });
                            }
                        }
                    }
                    else
                    {
                        all = false;
                    }
                }
                if (all && any)
                {
                    return new Entry { FullText = fts1, Pos2 = pos2 };
                }
                return null;
            }

            public static bool Hitt(string fp, string[] kws)
            {
                bool all = true, any = false;
                foreach (string kw in kws)
                {
                    int i = Strut2.IndexOf(fp, kw, 0);
                    if (i >= 0)
                    {
                        any = true;
                    }
                    else
                    {
                        all = false;
                    }
                }
                if (all && any)
                {
                    return true;
                }
                return false;
            }
        }

        void bwSe_DoWork(object sender, DoWorkEventArgs e)
        {
            String[] kws = Regex.Replace("" + e.Argument, "[\\s　]+", " ").Trim().Split(' ');
            SQLiteCommand cm = new SQLiteCommand("select fts1,fp,mt,cb,i from f2 order by mt desc", db);
            using (var dr = cm.ExecuteReader())
            {
                while (dr.Read())
                {
                    String fts1 = dr.GetString(0);
                    Entry ent = Uts.Search(fts1, kws);
                    if (Uts.Hitt(dr.GetString(1), kws))
                        ent = new Entry { FullText = fts1 };
                    if (ent != null)
                    {
                        ent.FilePath = dr.GetString(1);
                        ent.ModTime = dr.GetInt64(2);
                        ent.FileSize = dr.GetInt64(3);
                        ent.AutoKey = dr.GetInt64(4);
                        ent.Dir = io.Path.GetDirectoryName(ent.FilePath);
                        ent.Name = io.Path.GetFileName(ent.FilePath);

                        bwSe.ReportProgress(0, ent);
                    }
                }
            }
        }

        ObservableCollection<Entry> alent = new ObservableCollection<Entry>();
        CollectionViewSource cvsent = new CollectionViewSource();

        void bwSe_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            alent.Add(e.UserState as Entry);
        }

        BackgroundWorker bwSe = new BackgroundWorker();

        private void tbKws_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                SeNow();
            }
        }

        private void lbItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ent = lbItems.SelectedItem as Entry;
            if (ent != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Process.Start(ent.FilePath);
                }
            }
        }

        private void mOpenIt_Click(object sender, RoutedEventArgs e)
        {
            var ent = lbItems.SelectedItem as Entry;
            if (ent != null)
            {
                Process.Start(ent.FilePath);
            }
        }

        private void mLocateIt_Click(object sender, RoutedEventArgs e)
        {
            var ent = lbItems.SelectedItem as Entry;
            if (ent != null)
            {
                Process.Start("explorer.exe", " /select,\"" + ent.FilePath + "\"");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (bwUpd.IsBusy) bwUpd.CancelAsync();
        }

        private void mViewIt_Click(object sender, RoutedEventArgs e)
        {
            var ent = lbItems.SelectedItem as Entry;
            if (ent != null)
            {
                VWin vw = new VWin();
                List<Seli> alSeli = new List<Seli>();
                var fd = vw.rtb.Document;
                {
                    Paragraph p = new Paragraph();
                    fd.Blocks.Add(p);
                    var fn = io.Path.GetFileNameWithoutExtension(ent.FilePath);
                    var r = new Run(fn);
                    p.Inlines.Add(r);

                    alSeli.Add(new Seli { Pos = r, Disp = fn });

                }
                int x1 = 0;
                foreach (String row in ent.FullText.Split('\n'))
                {
                    int x2 = x1 + row.Length;
                    Divi divi = new Divi(x1, x2);
                    List<HLPart> alh = new List<HLPart>();
                    foreach (var hlp in ent.HLParts)
                    {
                        if (x1 <= hlp.Pos && hlp.Pos <= x2)
                        {
                            divi.Insert(hlp.Pos, hlp.Pos + hlp.Hit.Length, "" + alh.Count);
                            alh.Add(hlp);
                        }
                    }
                    Paragraph p;
                    fd.Blocks.Add(p = new Paragraph());
                    bool hl = false;
                    foreach (Spli s in divi.al)
                    {
                        Run r = new Run { Text = row.Substring(s.x1 - x1, s.x2 - s.x1) };
                        if (s.k != null)
                        {
                            int ih = int.Parse(s.k);
                            r.FontWeight = FontWeights.Bold;
                            r.Background = Brushes.Yellow;
                            r.Foreground = Brushes.Black;
                            hl = true;
                            alSeli.Add(new Seli { Pos = r, Disp = alh[ih].GetDisp() });
                        }

                        p.Inlines.Add(r);
                    }
                    if (hl)
                        p.TextDecorations.Add(TextDecorations.Underline);
                    x1 = x2 + 1;
                }
                vw.lbItems.DataContext = alSeli;
                vw.Owner = this;
                vw.Show();
            }
        }

        class Spli
        {
            public int x1 { get; set; }
            public int x2 { get; set; }
            public String k { get; set; }
        }
        class Divi
        {
            public List<Spli> al = new List<Spli>();

            public Divi(int x1, int x2)
            {
                al.Add(new Spli { x1 = x1, x2 = x2 });
            }

            public void Insert(int p1, int p2, String k)
            {
                for (int y = 0; y < al.Count; y++)
                {
                    Spli s = al[y];
                    if (s.x1 <= p1 && p2 <= s.x2)
                    {
                        Spli s1 = new Spli { x1 = p1, x2 = p2, k = k };
                        Spli s2 = new Spli { x1 = p2, x2 = s.x2, k = s.k };
                        s.x2 = p1;
                        al.Insert(y + 1, s1);
                        al.Insert(y + 2, s2);
                        y += 2;
                    }

                }
            }
        }
    }

    class Seli
    {
        public Inline Pos { get; set; }
        public String Disp { get; set; }

        public override string ToString()
        {
            return Disp;
        }
    }
}
