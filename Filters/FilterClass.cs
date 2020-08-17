using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Kensaku32go.Filters
{
    internal static class FilterClass
    {
        [DllImport("query.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int LoadIFilter(
            string pwcsPath,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
            ref IFilter ppIUnk
        );
    }
}
