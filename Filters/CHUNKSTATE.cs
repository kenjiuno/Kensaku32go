using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kensaku32go.Filters
{
    [Flags]
    internal enum CHUNKSTATE
    {
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
}
