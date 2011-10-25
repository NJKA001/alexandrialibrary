using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document
{
    public class CharacterSet
        : ICharacterSet
    {
        private CharacterSet(string name, string description)
            : this(name, description, null)
        {
        }

        private CharacterSet(string name, string description, byte[] byteOrderMark)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (description == null)
                throw new ArgumentNullException("description");

            this.name = name;
            this.description = description;
            this.byteOrderMark = byteOrderMark;
        }

        private readonly string name;
        private readonly string description;
        private readonly byte[] byteOrderMark;

        #region ICharacterSet Members

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public byte[] ByteOrderMark
        {
            get { return byteOrderMark; }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }

        static CharacterSet()
        {
            InitializeCharacterSets();

            foreach (var characterSet in characterSets)
            {
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

        private static void InitializeCharacterSets()
        {
            characterSets.Add(Ascii);
            characterSets.Add(Latin1);
            characterSets.Add(Utf7);
            characterSets.Add(Utf8);
            characterSets.Add(Utf16);
            characterSets.Add(Utf16Be);
            characterSets.Add(Utf16Le);
            characterSets.Add(Utf32);
            characterSets.Add(Utf32Be);
            characterSets.Add(Utf32Le);
            characterSets.Add(Unknown);
        }

        private static readonly IList<ICharacterSet> characterSets = new List<ICharacterSet>();
        private static readonly IDictionary<string, ICharacterSet> byName = new Dictionary<string, ICharacterSet>();
        private static readonly IDictionary<int, IDictionary<byte[], ICharacterSet>> byLengthAndBom = new Dictionary<int, IDictionary<byte[], ICharacterSet>>();

        #region Public Static Methods

        public static ICharacterSet Parse(string name)
        {
            if (name == null)
                return Unknown;

            var key = name.ToUpper().Trim();
            
            return byName.ContainsKey(key) ? byName[key] : Unknown;
        }

        public static ICharacterSet GetCharacterSet(Encoding encoding)
        {
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

        public static ICharacterSet GetCharacterSet(byte[] header)
        {
            var minimumHeaderSize = 4;
            if (header == null || header.Length < minimumHeaderSize)
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

        public static IEnumerable<ICharacterSet> GetCharacterSets()
        {
            return characterSets;
        }

        #endregion

        #region Character Sets

        public static readonly ICharacterSet Ascii = new CharacterSet("US-ASCII", "ANSI X3.4-1968");
        public static readonly ICharacterSet Latin1 = new CharacterSet("ISO-8859-1", "ISO/IEC 8859-1:1998: Latin1");
        public static readonly ICharacterSet Utf7 = new CharacterSet("UTF-7", "Unicode UTF-7: RFC 2152", new byte[] { 43, 47, 118 });
        public static readonly ICharacterSet Utf8 = new CharacterSet("UTF-8", "Unicode UTF-8: RFC 3629", new byte[] { 239, 187, 191 });
        public static readonly ICharacterSet Utf16 = new CharacterSet("UTF-16", "Uncide UTF-16: RFC 2781");
        public static readonly ICharacterSet Utf16Be = new CharacterSet("UTF-16BE", "Unicode UTF-16: Big Endian RFC 2781", new byte[] { 254, 255 });
        public static readonly ICharacterSet Utf16Le = new CharacterSet("UTF-16LE", "Unicode UTF-16: Little Endian RFC 2781", new byte[] { 255, 254 });
        public static readonly ICharacterSet Utf32 = new CharacterSet("UTF-32", "Unicode UTF-32");
        public static readonly ICharacterSet Utf32Be = new CharacterSet("UTF-32BE", "Unicode UTF-32: Big Endian", new byte[] { 0, 0, 254, 255 });
        public static readonly ICharacterSet Utf32Le = new CharacterSet("UTF-32LE", "Unicode UTF-32: Little Endian", new byte[] { 255, 254, 0, 0 });
        public static readonly ICharacterSet Unknown = new CharacterSet("UNKNOWN", "Unknown Character Set");

        #endregion
    }
}
