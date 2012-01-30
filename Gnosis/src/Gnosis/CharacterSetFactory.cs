using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class CharacterSetFactory
        : ICharacterSetFactory
    {
        public CharacterSetFactory()
        {
            AddCharacterSet(Ascii);
            AddCharacterSet(Latin1);
            AddCharacterSet(Utf7);
            AddCharacterSet(Utf8);
            AddCharacterSet(Utf16);
            AddCharacterSet(Utf16Be);
            AddCharacterSet(Utf16Le);
            AddCharacterSet(Utf32);
            AddCharacterSet(Utf32Be);
            AddCharacterSet(Utf32Le);
            AddCharacterSet(Unknown);
        }

        private readonly IList<ICharacterSet> characterSets = new List<ICharacterSet>();
        private readonly IDictionary<string, ICharacterSet> byName = new Dictionary<string, ICharacterSet>();
        private readonly IDictionary<int, IDictionary<byte[], ICharacterSet>> byLengthAndBom = new Dictionary<int, IDictionary<byte[], ICharacterSet>>();

        #region Character Sets

        private static readonly ICharacterSet Ascii = new CharacterSet("US-ASCII", false, "ANSI X3.4-1968");
        private static readonly ICharacterSet Latin1 = new CharacterSet("ISO-8859-1", false, "ISO/IEC 8859-1:1998: Latin1");
        private static readonly ICharacterSet Utf7 = new CharacterSet("UTF-7", false, "Unicode UTF-7: RFC 2152", new byte[] { 43, 47, 118 });
        private static readonly ICharacterSet Utf8 = new CharacterSet("UTF-8", false, "Unicode UTF-8: RFC 3629", new byte[] { 239, 187, 191 });
        private static readonly ICharacterSet Utf16 = new CharacterSet("UTF-16", false, "Uncide UTF-16: RFC 2781");
        private static readonly ICharacterSet Utf16Be = new CharacterSet("UTF-16BE", false, "Unicode UTF-16: Big Endian RFC 2781", new byte[] { 254, 255 });
        private static readonly ICharacterSet Utf16Le = new CharacterSet("UTF-16LE", false, "Unicode UTF-16: Little Endian RFC 2781", new byte[] { 255, 254 });
        private static readonly ICharacterSet Utf32 = new CharacterSet("UTF-32", false, "Unicode UTF-32");
        private static readonly ICharacterSet Utf32Be = new CharacterSet("UTF-32BE", false, "Unicode UTF-32: Big Endian", new byte[] { 0, 0, 254, 255 });
        private static readonly ICharacterSet Utf32Le = new CharacterSet("UTF-32LE", false, "Unicode UTF-32: Little Endian", new byte[] { 255, 254, 0, 0 });
        private static readonly ICharacterSet Unknown = new CharacterSet("UNKNOWN", true, "Unknown Character Set");

        #endregion

        public ICharacterSet Default
        {
            get { return Unknown; }
        }

        public ICharacterSet GetByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            var key = name.ToUpper().Trim();

            return byName.ContainsKey(key) ? byName[key] : Unknown;
        }

        public ICharacterSet GetByEncoding(Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            if (encoding == Encoding.ASCII)
                return Ascii;
            else if (encoding == Encoding.UTF7)
                return Utf7;
            else if (encoding == Encoding.UTF8)
                return Utf8;
            else if (encoding == Encoding.Unicode)
                return Utf16Le;
            else if (encoding == Encoding.BigEndianUnicode)
                return Utf16Be;
            else if (encoding == Encoding.UTF32)
                return Utf32Le;

            return Unknown;
        }

        public ICharacterSet GetByHeader(byte[] header)
        {
            if (header == null)
                throw new ArgumentNullException("header");

            const int minimumHeaderSize = 4;
            if (header.Length < minimumHeaderSize)
                return Unknown;

            var length = minimumHeaderSize;
            while (length > 1)
            {
                if (byLengthAndBom.ContainsKey(length))
                {
                    var bomMap = byLengthAndBom[length];
                    var bom = new byte[length];
                    Array.Copy(header, bom, length);

                    foreach (var pair in bomMap)
                    {
                        if (bom.SequenceEqual(pair.Key))
                            return pair.Value;
                    }
                }
                length--;
            }

            return Unknown;
        }

        public void AddCharacterSet(ICharacterSet characterSet)
        {
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");

            byName.Add(characterSet.Name, characterSet);

            if (characterSet.ByteOrderMark != null)
            {
                var length = characterSet.ByteOrderMark.Length;
                if (!byLengthAndBom.ContainsKey(length))
                    byLengthAndBom.Add(length, new Dictionary<byte[], ICharacterSet> { { characterSet.ByteOrderMark, characterSet } });
                else
                    byLengthAndBom[length].Add(characterSet.ByteOrderMark, characterSet);
            }
        }
    }
}
