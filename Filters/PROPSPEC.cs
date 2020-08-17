using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Kensaku32go.Filters
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct PROPSPEC
    {
        [FieldOffset(0)]
        public int ulKind;     // 0 - string used; 1 - PROPID
        [FieldOffset(4)]
        public int propid;
        [FieldOffset(4)]
        public IntPtr lpwstr;
    }
}
