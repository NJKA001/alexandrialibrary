using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public enum TextEncoding
    {
        /// <summary>
        ///    The string is to be Latin-1 encoded.
        /// </summary>
        Latin1 = 0,

        /// <summary>
        ///    The string is to be UTF-16 encoded.
        /// </summary>
        UTF16 = 1,

        /// <summary>
        ///    The string is to be UTF-16BE encoded.
        /// </summary>
        UTF16BE = 2,

        /// <summary>
        ///    The string is to be UTF-8 encoded.
        /// </summary>
        UTF8 = 3,

        /// <summary>
        ///    The string is to be UTF-16LE encoded.
        /// </summary>
        UTF16LE = 4
    }
}
