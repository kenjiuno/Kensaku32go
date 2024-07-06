using System;
using System.Collections.Generic;
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

        internal static int LoadIFilter2(string pwcsPath, out IFilter filter)
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
                    filter = new Wrapper(filterUnk);
                }
                return 0;
            }
            filter = null;
            return hr;
        }

        class Wrapper : IFilter
        {
            private readonly object target;

            public Wrapper(object target)
            {
                this.target = target;
            }

            private object Invoke(string name, object[] args)
            {
                foreach (var iface in target.GetType().GetInterfaces())
                {
                    var met = iface.GetMethod(name);
                    if (met == null)
                    {
                        continue;
                    }
                    return met.Invoke(target, args);
                }
                throw new MissingMethodException();
            }

            int IFilter.Init(IFILTER_INIT grfFlags, int cAttributes, IntPtr aAttributes, out IFILTER_FLAGS pdwFlags)
            {
                var args = new object[] {
                    grfFlags, cAttributes, aAttributes, null
                };
                try
                {
                    return (int)Invoke("Init", args);
                }
                finally
                {
                    pdwFlags = (IFILTER_FLAGS)args[3];
                }
            }

            int IFilter.GetChunk(out STAT_CHUNK pStat)
            {
                var args = new object[] {
                    null
                };
                try
                {
                    return (int)Invoke("GetChunk", args);
                }
                finally
                {
                    pStat = (STAT_CHUNK)args[0];
                }
            }

            int IFilter.GetText(ref int pcwcBuffer, StringBuilder awcBuffer)
            {
                var args = new object[] {
                    pcwcBuffer, awcBuffer
                };
                try
                {
                    return (int)Invoke("GetText", args);
                }
                finally
                {
                    pcwcBuffer = (int)args[0];
                }
            }

            int IFilter.GetValue(ref IntPtr PropVal)
            {
                throw new NotImplementedException();
            }

            int IFilter.BindRegion(ref FILTERREGION origPos, ref Guid riid, ref object ppunk)
            {
                throw new NotImplementedException();
            }
        }
    }
}
