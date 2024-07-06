using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace LibIFilter.Filters
{
    internal static class FilterClass
    {
        [DllImport("query.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int LoadIFilter(
            string pwcsPath,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
            [MarshalAs(UnmanagedType.Interface)] out object ppIUnk
        );

        [DllImport("query.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int LoadIFilter(
            string pwcsPath,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
            out IntPtr ppIUnk
        );

        class Peace : ICustomQueryInterface
        {
            private readonly IntPtr inner;
            private Guid IID_IManagedObject = new Guid("{C3FCC19E-A970-11D2-8B5A-00A0C9B7C9C4}");

            public Peace(IntPtr inner)
            {
                this.inner = inner;
            }

            ~Peace()
            {
                Marshal.Release(inner);
            }

            CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
            {
                ppv = IntPtr.Zero;
                if (iid != IID_IManagedObject)
                {
                    var res = Marshal.QueryInterface(inner, ref iid, out ppv);
                    if (res >= 0)
                    {
                        Marshal.Release(ppv);
                        return CustomQueryInterfaceResult.Handled;
                    }
                }
                return CustomQueryInterfaceResult.Failed;
            }
        }

        internal static int LoadIFilter(string pwcsPath, out IFilter filter)
        {
            IntPtr filterUnk;
            var hr = LoadIFilter(pwcsPath, null, out filterUnk);
            if (hr >= 0)
            {
                var aggregated = Marshal.CreateAggregatedObject(filterUnk, new Peace(filterUnk));
                filter = (IFilter)Marshal.GetObjectForIUnknown(aggregated);
                Marshal.Release(aggregated);
                return 0;
            }
            filter = null;
            return hr;
        }

        [DllImport("IFilterProxy.dll", SetLastError = true, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CreateIFilterProxy(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnk,
            [Out, MarshalAs(UnmanagedType.Interface)] out IFilter ppFilter
        );

        internal static int LoadIFilter3(string pwcsPath, out IFilter filter)
        {
            object filterUnk;
            var hr = LoadIFilter(pwcsPath, null, out filterUnk);
            if (hr >= 0)
            {
                if (Marshal.IsComObject(filterUnk))
                {
                    filter = (IFilter)filterUnk;
                }
                else
                {
                    hr = CreateIFilterProxy(filterUnk, out filter);
                    if (hr < 0)
                    {
                        throw new Win32Exception(hr);
                    }
                }
                return 0;
            }
            filter = null;
            return hr;
        }
    }
}
