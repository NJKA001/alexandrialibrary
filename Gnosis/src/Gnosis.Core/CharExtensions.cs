using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class CharExtensions
    {
        public static bool IsVowel(this char self)
        {
            return (self == 'A') || (self == 'E') || (self == 'I') || (self == 'O') || (self == 'U') || (self == 'Y') || self == 'a' || self == 'e' || self == 'i' || self == 'o' || self == 'u' || self == 'y';
        }
    }
}
