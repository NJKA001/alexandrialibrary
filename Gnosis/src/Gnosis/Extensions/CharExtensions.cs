using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public static class CharExtensions
    {
        public static bool IsVowel(this char self)
        {
            return (self == 'A') || (self == 'E') || (self == 'I') || (self == 'O') || (self == 'U') || (self == 'Y') || self == 'a' || self == 'e' || self == 'i' || self == 'o' || self == 'u' || self == 'y';
        }

        public static bool IsValidHexChar(this char self)
        {
            if (Char.IsDigit(self))
                return true;

            return (self == 'a' || self == 'A' || self == 'b' || self == 'B' || self == 'c' || self == 'C' || self == 'd' || self == 'D' || self == 'e' || self == 'E' || self == 'f' || self == 'F');
        }
    }
}
