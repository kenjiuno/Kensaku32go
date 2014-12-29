using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using io = System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Data.SQLite;
using System.Data;

namespace Kensaku32go {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            Title += " " + ((IntPtr.Size == 4) ? "(32ビット)" : "(64ビット)");
        }

        String fpdb;

        SQLiteConnection db;

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            fpdb = App.fpdb;

            if (String.IsNullOrEmpty(fpdb)) {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "Kensaku32go";
                sfd.Filter = "*.Kensaku32go|*.Kensaku32go";
                sfd.CheckPathExists = true;
                sfd.CheckFileExists = true;
                sfd.FileName = "検索32号の辞書";
                sfd.Title = "検索したい書類のフォルダで[保存]";
                if (!(sfd.ShowDialog() ?? false)) {
                    Close();
                    return;
                }
                fpdb = sfd.FileName;
            }

            SQLiteConnectionStringBuilder b = new SQLiteConnectionStringBuilder();
            b.DataSource = fpdb;
            b.DateTimeKind = DateTimeKind.Utc;

            db = new SQLiteConnection(b.ConnectionString);
            db.Open();

            {
                var cm = new SQLiteCommand("create table if not exists f2(i integer primary key autoincrement, fp text, mt integer, cb integer, fts1 text);", db);
                cm.ExecuteNonQuery();
            }

            bwUpd.DoWork += bwUpd_DoWork;
            bwUpd.WorkerSupportsCancellation = true;
            bwUpd.RunWorkerCompleted += bwUpd_RunWorkerCompleted;
            bwUpd.RunWorkerAsync();
            lUpd.Visibility = System.Windows.Visibility.Visible;

            bwSe.DoWork += bwSe_DoWork;
            bwSe.RunWorkerCompleted += bwSe_RunWorkerCompleted;
            bwSe.ProgressChanged += bwSe_ProgressChanged;
            bwSe.WorkerReportsProgress = true;

            cvsent.Source = alent;
            lbItems.DataContext = cvsent;
        }

        void bwUpd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            lUpd.Visibility = System.Windows.Visibility.Collapsed;
            if (e.Error == null) lUpdDone.Visibility = System.Windows.Visibility.Visible;
            if (e.Error != null) lUpdFail.Visibility = System.Windows.Visibility.Visible;
            lUpdFail.ToolTip = "" + e.Error;
        }

        class Ent {
            public Int64 i { get; set; }
            public String fp { get; set; }
            public Int64 mt { get; set; }
            public Int64 cb { get; set; }

            public bool need { get; set; }

            public String fts1 { get; set; }
            public String Name { get; set; }
            public String Dir { get; set; }
            public List<Hiti> Pos2 { get; set; }

            public List<HLPart> HLParts {
                get {
                    List<HLPart> al = new List<HLPart>();
                    if (Pos2 != null)
                        foreach (Hiti hi in Pos2) {
                            int x1 = hi.x;
                            int x2 = x1 + hi.cx;
                            int x0 = Math.Max(0, hi.x - 10);
                            int x3 = Math.Min(fts1.Length, x2 + 10);
                            al.Add(new HLPart {
                                Hit = SUt.L1(fts1.Substring(x1, x2 - x1)),
                                Prev = SUt.L1(fts1.Substring(x0, x1 - x0)),
                                Next = SUt.L1(fts1.Substring(x2, x3 - x2)),
                                Pos = x1
                            });
                        }
                    return al;
                }
            }

            class SUt {
                public static string L1(string s) {
                    return (s ?? "").Replace("\r", " ").Replace("\n", " ");
                }
            }

            public String When {
                get {
                    DateTime dt = new DateTime(mt, DateTimeKind.Utc);
                    TimeSpan ts = DateTime.UtcNow.Subtract(dt);
                    if (ts.TotalMinutes < 1) return String.Format("{0:0}秒前", ts.TotalSeconds);
                    if (ts.TotalHours < 1) return String.Format("{0:0}分前", ts.TotalMinutes);
                    if (ts.TotalDays < 1) return String.Format("{0:0}時間前", ts.TotalHours);
                    if (ts.TotalDays < 31) return String.Format("{0:0.0}日前", ts.TotalDays);
                    if (ts.TotalDays < 365) return String.Format("{0:0.0}月前", ts.TotalDays / 30);
                    return String.Format("{0:0.0}年前", ts.TotalDays / 365);
                }
            }
        }

        class HLPart {
            public String Prev { get; set; }
            public String Hit { get; set; }
            public String Next { get; set; }
            public int Pos { get; set; }

            public string GetDisp() {
                return Prev + "[" + Hit + "]" + Next;
            }
        }

        void bwUpd_DoWork(object sender, DoWorkEventArgs e) {
            Stack<io.DirectoryInfo> dirs = new Stack<io.DirectoryInfo>();
            SortedDictionary<string, Ent> ents = new SortedDictionary<string, Ent>();
            {
                String dir = io.Path.GetDirectoryName(fpdb);
                String dir1 = dir + "\\";
                {
                    using (var dr = new SQLiteCommand("select i,fp,mt,cb from f2", db).ExecuteReader()) {
                        while (dr.Read()) {
                            var fp = dr.GetString(1);
                            ents[fp] = (new Ent {
                                i = dr.GetInt64(0),
                                fp = fp,
                                mt = dr.GetInt64(2),
                                cb = dr.GetInt64(3),
                            });
                        }
                    }
                }
                dirs.Push(new io.DirectoryInfo(dir));
            }

            var cmUp = new SQLiteCommand("update f2 set mt=@mt,cb=@cb,fts1=@fts1 where i=@i", db);
            cmUp.Parameters.Add("mt", System.Data.DbType.Int64);
            cmUp.Parameters.Add("cb", System.Data.DbType.Int64);
            cmUp.Parameters.Add("fts1", System.Data.DbType.String);
            cmUp.Parameters.Add("i", System.Data.DbType.Int64);

            var cmI = new SQLiteCommand("insert into f2 (fp,mt,cb,fts1) values (@fp,@mt,@cb,@fts1)", db);
            cmI.Parameters.Add("fp", System.Data.DbType.String);
            cmI.Parameters.Add("mt", System.Data.DbType.Int64);
            cmI.Parameters.Add("cb", System.Data.DbType.Int64);
            cmI.Parameters.Add("fts1", System.Data.DbType.String);

            while (dirs.Count != 0) {
                var di = dirs.Pop();
                foreach (var fsi in di.GetFileSystemInfos()) {
                    if (bwUpd.CancellationPending) return;
                    var fi = fsi as io.FileInfo;
                    if (fsi is io.DirectoryInfo) {
                        dirs.Push(fsi as io.DirectoryInfo);
                    }
                    else if (fi != null) {
                        Ent ent;
                        bool upd = true;
                        if (ents.TryGetValue(fi.FullName, out ent)) {
                            if (ent.mt == fi.LastWriteTimeUtc.Ticks && ent.cb == fi.Length) {
                                upd = false;
                                ent.need = true;
                            }
                            else {
                                ent.need = true;
                            }
                        }
                        else {
                            ent = new Ent {
                                i = -1,
                                fp = fi.FullName,
                                mt = fi.LastWriteTimeUtc.Ticks,
                                cb = fi.Length,

                                need = true,
                            };
                        }
                        if (upd) {
                            try {
                                IFilter filter = null;
                                int hr = LoadIFilter(fi.FullName, null, ref filter);
                                if (hr != 0) throw Marshal.GetExceptionForHR(hr);
                                StringWriter wr = new StringWriter();
                                try {
                                    IFILTER_FLAGS ff;
                                    hr = filter.Init(IFILTER_INIT.IFILTER_INIT_CANON_PARAGRAPHS | IFILTER_INIT.IFILTER_INIT_HARD_LINE_BREAKS | IFILTER_INIT.IFILTER_INIT_APPLY_INDEX_ATTRIBUTES,
                                        0,
                                        IntPtr.Zero,
                                        out ff
                                        );
                                    if (hr != 0) throw Marshal.GetExceptionForHR(hr);

                                    StringBuilder str = new StringBuilder(30000);
                                    while (true) {
                                        STAT_CHUNK chunk;
                                        hr = filter.GetChunk(out chunk);
                                        if (hr != 0) break;

                                        String row = "";
                                        while (true) {
                                            int cwc = 30000;
                                            hr = filter.GetText(ref cwc, str);
                                            if (hr != 0) break;

                                            str.Length = cwc;
                                            row += "" + str;
                                        }

                                        wr.WriteLine(row);

                                    }
                                }
                                finally {
                                    Marshal.ReleaseComObject(filter);
                                }

                                if (ent.i != -1) {
                                    cmUp.Parameters["mt"].Value = fi.LastWriteTimeUtc.Ticks;
                                    cmUp.Parameters["cb"].Value = fi.Length;
                                    cmUp.Parameters["fts1"].Value = ("" + wr).Normalize();
                                    cmUp.Parameters["i"].Value = ent.i;
                                    cmUp.ExecuteNonQuery();
                                }
                                else {
                                    cmI.Parameters["fp"].Value = fi.FullName;
                                    cmI.Parameters["mt"].Value = fi.LastWriteTimeUtc.Ticks;
                                    cmI.Parameters["cb"].Value = fi.Length;
                                    cmI.Parameters["fts1"].Value = ("" + wr).Normalize();
                                    cmI.ExecuteNonQuery();
                                }
                            }
                            catch (Exception err) {
                                Debug.WriteLine("# {0}: {1}", fi.FullName, err);
                            }
                        }
                    }
                }
            }

            var cmD = new SQLiteCommand("delete from f2 where i=@i", db);
            cmD.Parameters.Add("i", System.Data.DbType.Int64);
            foreach (var ent in ents.Values) {
                if (ent.need) continue;
                cmD.Parameters["i"].Value = ent.i;
                cmD.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Flags controlling the operation of the FileFilter
        /// instance.
        /// </summary>
        [Flags]
        public enum IFILTER_INIT {
            /// <summary>
            /// Paragraph breaks should be marked with the Unicode PARAGRAPH
            /// SEPARATOR (0x2029)
            /// </summary>
            IFILTER_INIT_CANON_PARAGRAPHS = 1,

            /// <summary>
            /// Soft returns, such as the newline character in Microsoft Word, should
            /// be replaced by hard returnsLINE SEPARATOR (0x2028). Existing hard
            /// returns can be doubled. A carriage return (0x000D), line feed (0x000A),
            /// or the carriage return and line feed in combination should be considered
            /// a hard return. The intent is to enable pattern-expression matches that
            /// match against observed line breaks. 
            /// </summary>
            IFILTER_INIT_HARD_LINE_BREAKS = 2,

            /// <summary>
            /// Various word-processing programs have forms of hyphens that are not
            /// represented in the host character set, such as optional hyphens
            /// (appearing only at the end of a line) and nonbreaking hyphens. This flag
            /// indicates that optional hyphens are to be converted to nulls, and
            /// non-breaking hyphens are to be converted to normal hyphens (0x2010), or
            /// HYPHEN-MINUSES (0x002D). 
            /// </summary>
            IFILTER_INIT_CANON_HYPHENS = 4,

            /// <summary>
            /// Just as the IFILTER_INIT_CANON_HYPHENS flag standardizes hyphens,
            /// this one standardizes spaces. All special space characters, such as
            /// nonbreaking spaces, are converted to the standard space character
            /// (0x0020). 
            /// </summary>
            IFILTER_INIT_CANON_SPACES = 8,

            /// <summary>
            /// Indicates that the client wants text split into chunks representing
            /// public value-type properties. 
            /// </summary>
            IFILTER_INIT_APPLY_INDEX_ATTRIBUTES = 16,

            /// <summary>
            /// Indicates that the client wants text split into chunks representing
            /// properties determined during the indexing process. 
            /// </summary>
            IFILTER_INIT_APPLY_CRAWL_ATTRIBUTES = 256,

            /// <summary>
            /// Any properties not covered by the IFILTER_INIT_APPLY_INDEX_ATTRIBUTES
            /// and IFILTER_INIT_APPLY_CRAWL_ATTRIBUTES flags should be emitted. 
            /// </summary>
            IFILTER_INIT_APPLY_OTHER_ATTRIBUTES = 32,

            /// <summary>
            /// Optimizes IFilter for indexing because the client calls the
            /// IFilter::Init method only once and does not call IFilter::BindRegion.
            /// This eliminates the possibility of accessing a chunk both before and
            /// after accessing another chunk. 
            /// </summary>
            IFILTER_INIT_INDEXING_ONLY = 64,

            /// <summary>
            /// The text extraction process must recursively search all linked
            /// objects within the document. If a link is unavailable, the
            /// IFilter::GetChunk call that would have obtained the first chunk of the
            /// link should return FILTER_E_LINK_UNAVAILABLE. 
            /// </summary>
            IFILTER_INIT_SEARCH_LINKS = 128,

            /// <summary>
            /// The content indexing process can return property values set by the  filter. 
            /// </summary>
            IFILTER_INIT_FILTER_OWNED_VALUE_OK = 512
        }

        [ComImport, Guid("89BCB740-6119-101A-BCB7-00DD010655AF")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IFilter {
            /// <summary>
            /// The IFilter::Init method initializes a filtering session.
            /// </summary>
            [PreserveSig]
            int Init(
                //[in] Flag settings from the IFILTER_INIT enumeration for
                // controlling text standardization, property output, embedding
                // scope, and IFilter access patterns. 
              IFILTER_INIT grfFlags,

              // [in] The size of the attributes array. When nonzero, cAttributes
                //  takes 
                // precedence over attributes specified in grfFlags. If no
                // attribute flags 
                // are specified and cAttributes is zero, the default is given by
                // the 
                // PSGUID_STORAGE storage property set, which contains the date and
                //  time 
                // of the last write to the file, size, and so on; and by the
                //  PID_STG_CONTENTS 
                // 'contents' property, which maps to the main contents of the
                // file. 
                // For more information about properties and property sets, see
                // Property Sets. 
              int cAttributes,

              //[in] Array of pointers to FULLPROPSPEC structures for the
                // requested properties. 
                // When cAttributes is nonzero, only the properties in aAttributes
                // are returned. 
              IntPtr aAttributes,

              // [out] Information about additional properties available to the
                //  caller; from the IFILTER_FLAGS enumeration. 
              out IFILTER_FLAGS pdwFlags);

            /// <summary>
            /// The IFilter::GetChunk method positions the filter at the beginning
            /// of the next chunk, 
            /// or at the first chunk if this is the first call to the GetChunk
            /// method, and returns a description of the current chunk. 
            /// </summary>
            [PreserveSig]
            int GetChunk(out STAT_CHUNK pStat);

            /// <summary>
            /// The IFilter::GetText method retrieves text (text-type properties)
            /// from the current chunk, 
            /// which must have a CHUNKSTATE enumeration value of CHUNK_TEXT.
            /// </summary>
            [PreserveSig]
            int GetText(
                // [in/out] On entry, the size of awcBuffer array in wide/Unicode
                // characters. On exit, the number of Unicode characters written to
                // awcBuffer. 
                // Note that this value is not the number of bytes in the buffer. 
                ref int pcwcBuffer,

                // Text retrieved from the current chunk. Do not terminate the
                // buffer with a character.  
                [Out(), MarshalAs(UnmanagedType.LPWStr)] 
       StringBuilder awcBuffer);

            /// <summary>
            /// The IFilter::GetValue method retrieves a value (public
            /// value-type property) from a chunk, 
            /// which must have a CHUNKSTATE enumeration value of CHUNK_VALUE.
            /// </summary>
            [PreserveSig]
            int GetValue(
                // Allocate the PROPVARIANT structure with CoTaskMemAlloc. Some
                // PROPVARIANT 
                // structures contain pointers, which can be freed by calling the
                // PropVariantClear function. 
                // It is up to the caller of the GetValue method to call the
                //   PropVariantClear method.            
                // ref IntPtr ppPropValue
                // [MarshalAs(UnmanagedType.Struct)]
                ref IntPtr PropVal);

            /// <summary>
            /// The IFilter::BindRegion method retrieves an interface representing
            /// the specified portion of the object. 
            /// Currently reserved for future use.
            /// </summary>
            [PreserveSig]
            int BindRegion(ref FILTERREGION origPos,
              ref Guid riid, ref object ppunk);
        }

        struct STAT_CHUNK {
            /// <summary>
            /// The chunk identifier. Chunk identifiers must be unique for the
            /// current instance of the IFilter interface. 
            /// Chunk identifiers must be in ascending order. The order in which
            /// chunks are numbered should correspond to the order in which they appear
            /// in the source document. Some search engines can take advantage of the
            /// proximity of chunks of various properties. If so, the order in which
            /// chunks with different properties are emitted will be important to the
            /// search engine. 
            /// </summary>
            public int idChunk;

            /// <summary>
            /// The type of break that separates the previous chunk from the current
            ///  chunk. Values are from the CHUNK_BREAKTYPE enumeration. 
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public CHUNK_BREAKTYPE breakType;

            /// <summary>
            /// Flags indicate whether this chunk contains a text-type or a
            /// value-type property. 
            /// Flag values are taken from the CHUNKSTATE enumeration. If the CHUNK_TEXT flag is set, 
            /// IFilter::GetText should be used to retrieve the contents of the chunk
            /// as a series of words. 
            /// If the CHUNK_VALUE flag is set, IFilter::GetValue should be used to retrieve 
            /// the value and treat it as a single property value. If the filter dictates that the same 
            /// content be treated as both text and as a value, the chunk should be emitted twice in two       
            /// different chunks, each with one flag set. 
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public CHUNKSTATE flags;

            /// <summary>
            /// The language and sublanguage associated with a chunk of text. Chunk locale is used 
            /// by document indexers to perform proper word breaking of text. If the chunk is 
            /// neither text-type nor a value-type with data type VT_LPWSTR, VT_LPSTR or VT_BSTR, 
            /// this field is ignored. 
            /// </summary>
            public int locale;

            /// <summary>
            /// The property to be applied to the chunk. If a filter requires that       the same text 
            /// have more than one property, it needs to emit the text once for each       property 
            /// in separate chunks. 
            /// </summary>
            public FULLPROPSPEC attribute;

            /// <summary>
            /// The ID of the source of a chunk. The value of the idChunkSource     member depends on the nature of the chunk: 
            /// If the chunk is a text-type property, the value of the idChunkSource       member must be the same as the value of the idChunk member. 
            /// If the chunk is an public value-type property derived from textual       content, the value of the idChunkSource member is the chunk ID for the
            /// text-type chunk from which it is derived. 
            /// If the filter attributes specify to return only public value-type
            /// properties, there is no content chunk from which to derive the current
            /// public value-type property. In this case, the value of the
            /// idChunkSource member must be set to zero, which is an invalid chunk. 
            /// </summary>
            public int idChunkSource;

            /// <summary>
            /// The offset from which the source text for a derived chunk starts in
            /// the source chunk. 
            /// </summary>
            public int cwcStartSource;

            /// <summary>
            /// The length in characters of the source text from which the current
            /// chunk was derived. 
            /// A zero value signifies character-by-character correspondence between
            /// the source text and 
            /// the derived text. A nonzero value means that no such direct
            /// correspondence exists
            /// </summary>
            public int cwcLenSource;
        }

        /// <summary>
        /// Enumerates the different breaking types that occur between 
        /// chunks of text read out by the FileFilter.
        /// </summary>
        enum CHUNK_BREAKTYPE {
            /// <summary>
            /// No break is placed between the current chunk and the previous chunk.
            /// The chunks are glued together. 
            /// </summary>
            CHUNK_NO_BREAK = 0,
            /// <summary>
            /// A word break is placed between this chunk and the previous chunk that
            /// had the same attribute. 
            /// Use of CHUNK_EOW should be minimized because the choice of word
            /// breaks is language-dependent, 
            /// so determining word breaks is best left to the search engine. 
            /// </summary>
            CHUNK_EOW = 1,
            /// <summary>
            /// A sentence break is placed between this chunk and the previous chunk
            /// that had the same attribute. 
            /// </summary>
            CHUNK_EOS = 2,
            /// <summary>
            /// A paragraph break is placed between this chunk and the previous chunk
            /// that had the same attribute.
            /// </summary>     
            CHUNK_EOP = 3,
            /// <summary>
            /// A chapter break is placed between this chunk and the previous chunk
            /// that had the same attribute. 
            /// </summary>
            CHUNK_EOC = 4
        }

        [Flags]
        enum CHUNKSTATE {
            /// <summary>
            /// The current chunk is a text-type property.
            /// </summary>
            CHUNK_TEXT = 0x1,
            /// <summary>
            /// The current chunk is a value-type property. 
            /// </summary>
            CHUNK_VALUE = 0x2,
            /// <summary>
            /// Reserved
            /// </summary>
            CHUNK_FILTER_OWNED_VALUE = 0x4
        }

        struct FULLPROPSPEC {
            public Guid guidPropSet;
            public PROPSPEC psProperty;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct PROPSPEC {
            [FieldOffset(0)]
            public int ulKind;     // 0 - string used; 1 - PROPID
            [FieldOffset(4)]
            public int propid;
            [FieldOffset(4)]
            public IntPtr lpwstr;
        }


        struct FILTERREGION {
            public int idChunk;
            public int cwcStart;
            public int cwcExtent;
        }

        [Flags]
        enum IFILTER_FLAGS {
            /// <summary>
            /// The caller should use the IPropertySetStorage and IPropertyStorage
            /// interfaces to locate additional properties. 
            /// When this flag is set, properties available through COM
            /// enumerators should not be returned from IFilter. 
            /// </summary>
            IFILTER_FLAGS_OLE_PROPERTIES = 1
        }


        [DllImport("query.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern int LoadIFilter(string pwcsPath,
                  [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
                  ref IFilter ppIUnk);

        BackgroundWorker bwUpd = new BackgroundWorker();

        private void Window_Unloaded(object sender, RoutedEventArgs e) {
            if (db != null) {
                db.Close();
                db = null;
            }
        }

        private void bSearch_Click(object sender, RoutedEventArgs e) {
            SeNow();
        }

        private void SeNow() {
            if (bwSe.IsBusy) return;

            alent.Clear();

            bwSe.RunWorkerAsync(tbKws.Text);

            bSearch.IsEnabled = false;
            lSe.Visibility = System.Windows.Visibility.Visible;
            lSeDone.Visibility = System.Windows.Visibility.Collapsed;
            lSeFail.Visibility = System.Windows.Visibility.Collapsed;
        }

        void bwSe_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            bSearch.IsEnabled = true;
            lSe.Visibility = System.Windows.Visibility.Collapsed;
            if (e.Error == null) {
                lSeDone.Visibility = System.Windows.Visibility.Visible;
                lSeFail.Visibility = System.Windows.Visibility.Collapsed;
                lSeFail.ToolTip = "";
            }
            else {
                lSeDone.Visibility = System.Windows.Visibility.Collapsed;
                lSeFail.Visibility = System.Windows.Visibility.Visible;
                lSeFail.ToolTip = "" + e.Error;
            }
        }

        class Hiti {
            public int x { get; set; }
            public int cx { get; set; }
        }

        class Uts {
            internal static Ent Search(string fts1, string[] kws) {
                bool all = true, any = false;
                List<Hiti> pos2 = new List<Hiti>();
                foreach (string kw in kws) {
                    int i = Strut2.IndexOf(fts1, kw, 0);
                    if (i >= 0) {
                        any = true;
                        pos2.Add(new Hiti { x = i, cx = kw.Length });

                        int i2 = Strut2.IndexOf(fts1, kw, i + kw.Length);
                        if (i2 >= 0) {
                            pos2.Add(new Hiti { x = i2, cx = kw.Length });

                            int i3 = Strut2.IndexOf(fts1, kw, i2 + kw.Length);
                            if (i3 >= 0) {
                                pos2.Add(new Hiti { x = i3, cx = kw.Length });
                            }
                        }
                    }
                    else {
                        all = false;
                    }
                }
                if (all && any) {
                    return new Ent { fts1 = fts1, Pos2 = pos2 };
                }
                return null;
            }

            public static bool Hitt(string fp, string[] kws) {
                bool all = true, any = false;
                foreach (string kw in kws) {
                    int i = Strut2.IndexOf(fp, kw, 0);
                    if (i >= 0) {
                        any = true;
                    }
                    else {
                        all = false;
                    }
                }
                if (all && any) {
                    return true;
                }
                return false;
            }
        }

        void bwSe_DoWork(object sender, DoWorkEventArgs e) {
            String[] kws = Regex.Replace("" + e.Argument, "[\\s　]+", " ").Trim().Split(' ');
            SQLiteCommand cm = new SQLiteCommand("select fts1,fp,mt,cb,i from f2 order by mt desc", db);
            using (var dr = cm.ExecuteReader()) {
                while (dr.Read()) {
                    String fts1 = dr.GetString(0);
                    Ent ent = Uts.Search(fts1, kws);
                    if (Uts.Hitt(dr.GetString(1), kws))
                        ent = new Ent { fts1 = fts1 };
                    if (ent != null) {
                        ent.fp = dr.GetString(1);
                        ent.mt = dr.GetInt64(2);
                        ent.cb = dr.GetInt64(3);
                        ent.i = dr.GetInt64(4);
                        ent.Dir = io.Path.GetDirectoryName(ent.fp);
                        ent.Name = io.Path.GetFileName(ent.fp);

                        bwSe.ReportProgress(0, ent);
                    }
                }
            }
        }

        ObservableCollection<Ent> alent = new ObservableCollection<Ent>();
        CollectionViewSource cvsent = new CollectionViewSource();

        void bwSe_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            alent.Add(e.UserState as Ent);
        }

        BackgroundWorker bwSe = new BackgroundWorker();

        private void tbKws_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                e.Handled = true;
                SeNow();
            }
        }

        private void lbItems_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var ent = lbItems.SelectedItem as Ent;
            if (ent != null) {
                if (e.LeftButton == MouseButtonState.Pressed) {
                    Process.Start(ent.fp);
                }
            }
        }

        private void mOpenIt_Click(object sender, RoutedEventArgs e) {
            var ent = lbItems.SelectedItem as Ent;
            if (ent != null) {
                Process.Start(ent.fp);
            }
        }

        private void mLocateIt_Click(object sender, RoutedEventArgs e) {
            var ent = lbItems.SelectedItem as Ent;
            if (ent != null) {
                Process.Start("explorer.exe", " /select,\"" + ent.fp + "\"");
            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            if (bwUpd.IsBusy) bwUpd.CancelAsync();
        }

        private void mViewIt_Click(object sender, RoutedEventArgs e) {
            var ent = lbItems.SelectedItem as Ent;
            if (ent != null) {
                VWin vw = new VWin();
                List<Seli> alSeli = new List<Seli>();
                alSeli.Add(new Seli { });
                var fd = vw.rtb.Document;
                int x1 = 0;
                foreach (String row in ent.fts1.Split('\n')) {
                    int x2 = x1 + row.Length;
                    Divi divi = new Divi(x1, x2);
                    List<HLPart> alh = new List<HLPart>();
                    foreach (var hlp in ent.HLParts) {
                        if (x1 <= hlp.Pos && hlp.Pos <= x2) {
                            divi.Insert(hlp.Pos, hlp.Pos + hlp.Hit.Length, "" + alh.Count);
                            alh.Add(hlp);
                        }
                    }
                    Paragraph p;
                    fd.Blocks.Add(p = new Paragraph());
                    bool hl = false;
                    foreach (Spli s in divi.al) {
                        Run r = new Run { Text = row.Substring(s.x1 - x1, s.x2 - s.x1) };
                        if (s.k != null) {
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
                vw.cb.DataContext = alSeli;
                vw.Owner = this;
                vw.Show();
            }
        }

        class Spli {
            public int x1 { get; set; }
            public int x2 { get; set; }
            public String k { get; set; }
        }
        class Divi {
            public List<Spli> al = new List<Spli>();

            public Divi(int x1, int x2) {
                al.Add(new Spli { x1 = x1, x2 = x2 });
            }

            public void Insert(int p1, int p2, String k) {
                for (int y = 0; y < al.Count; y++) {
                    Spli s = al[y];
                    if (s.x1 <= p1 && p2 <= s.x2) {
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

    class Seli {
        public Inline Pos { get; set; }
        public String Disp { get; set; }

        public override string ToString() {
            return Disp;
        }
    }
}
