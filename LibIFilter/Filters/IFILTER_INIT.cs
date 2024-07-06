using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibIFilter.Filters
{
    /// <summary>
    /// Flags controlling the operation of the FileFilter
    /// instance.
    /// </summary>
    [Flags]
    internal enum IFILTER_INIT
    {
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
}
