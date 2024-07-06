using LibIFilter.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibIFilter
{
    public static class TextExtractor
    {
        public static async Task<string> GetTextUsingIFilterAsync(string filePath)
        {
            return await Task.Run(
                () =>
                {
                    IFilter filter;
                    int hr = FilterClass.LoadIFilter2(filePath, out filter);
                    if (hr < 0)
                    {
                        throw Marshal.GetExceptionForHR(hr);
                    }
                    var writer = new StringWriter();
                    try
                    {
                        IFILTER_FLAGS ff;
                        hr = filter.Init(IFILTER_INIT.IFILTER_INIT_CANON_PARAGRAPHS | IFILTER_INIT.IFILTER_INIT_HARD_LINE_BREAKS | IFILTER_INIT.IFILTER_INIT_APPLY_INDEX_ATTRIBUTES,
                            0,
                            IntPtr.Zero,
                            out ff
                            );
                        if (hr != 0) throw Marshal.GetExceptionForHR(hr);

                        var str = new StringBuilder(30000);
                        while (true)
                        {
                            STAT_CHUNK chunk;
                            hr = filter.GetChunk(out chunk);
                            if (hr != 0) break;

                            String row = "";
                            while (true)
                            {
                                int cwc = 30000;
                                hr = filter.GetText(ref cwc, str);
                                if (hr != 0) break;

                                str.Length = cwc;
                                row += "" + str;
                            }

                            writer.WriteLine(row);
                        }

                        return writer.ToString();
                    }
                    finally
                    {
                        if (filter != null && Marshal.IsComObject(filter))
                        {
                            Marshal.ReleaseComObject(filter);
                        }
                    }
                }
            );
        }
    }
}
