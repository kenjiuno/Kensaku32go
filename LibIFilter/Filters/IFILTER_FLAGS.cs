using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibIFilter.Filters
{
    [Flags]
    internal enum IFILTER_FLAGS
    {
        /// <summary>
        /// The caller should use the IPropertySetStorage and IPropertyStorage
        /// interfaces to locate additional properties. 
        /// When this flag is set, properties available through COM
        /// enumerators should not be returned from IFilter. 
        /// </summary>
        IFILTER_FLAGS_OLE_PROPERTIES = 1
    }
}
