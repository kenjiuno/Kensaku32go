using GenTbl.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenTbl
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            foreach (String row in File.ReadAllLines("DIC.txt"))
            {
                String[] cols = row.Split('\t');
                if (cols.Length < 2) continue;
                foreach (String col in cols)
                {
                    Tree16 t = t16;
                    for (int x = 0; x < col.Length; x++)
                    {
                        t = t.Get(col[x]);
                    }
                    Debug.Assert(t.itIs == char.MinValue);
                    t.itIs = cols[0][0];
                }
            }

            {
                StringWriter wr = new StringWriter();
                Walk(t16, char.MinValue, 0, wr);
                File.WriteAllText("Tree16.cs", Resources.Tree16.Replace("***", wr.ToString()));
            }
        }

        private void Walk(Tree16 t, char c, int d, StringWriter wr)
        {
            if (t.sub.Count == 0) return;

            String p1 = new String('\t', 3 + 3 * d);
            String p2 = p1 + "\t";
            String p3 = p2 + "\t";
            String p4 = p3 + "\t";

            wr.WriteLine(p1 + "if (x + " + (d) + " < maxx) {");
            wr.WriteLine(p2 + "switch ((int)s[x + " + (d) + "]) {");
            foreach (var kv in t.sub)
            {
                wr.WriteLine(p3 + "case " + String.Format("0x{0:X4}", (int)kv.Key) + ": // " + kv.Key);
                Walk(kv.Value, kv.Key, d + 1, wr);
                if (kv.Value.itIs != char.MinValue)
                {
                    wr.WriteLine(p4 + "x += " + (d + 1) + ";");
                    wr.WriteLine(p4 + "return " + String.Format("0x{0:X4}", (int)kv.Value.itIs) + ";" + " // " + (char)kv.Value.itIs);
                }
                else
                {
                    wr.WriteLine(p4 + "break;");
                }
            }
            wr.WriteLine(p2 + "}");
            wr.WriteLine(p1 + "}");
        }

        Tree16 t16 = new Tree16();

        class Tree16
        {
            public SortedDictionary<char, Tree16> sub = new SortedDictionary<char, Tree16>();
            public char itIs = char.MinValue;

            public Tree16 Get(char c)
            {
                Tree16 r;
                if (!sub.TryGetValue(c, out r))
                {
                    r = sub[c] = new Tree16();
                }
                return r;
            }
        }
    }
}
