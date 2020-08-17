using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kensaku32go.Utils
{
    public class Strut2
    {
        public static int IndexOf(String body, String part, int x)
        {
            int maxt = part.Length;
            for (; x < body.Length; x++)
            {
                int xx = x;
                for (int t = 0; ;)
                {
                    int r0 = Strut.Next(body, ref xx, body.Length);
                    int r1 = Strut.Next(part, ref t, maxt);
                    if (r0 != r1) break;
                    if (t == maxt) return x;
                }
            }
            return -1;
        }
    }
}
