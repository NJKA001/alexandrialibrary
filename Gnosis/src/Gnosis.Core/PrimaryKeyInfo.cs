using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class PrimaryKeyInfo
    {
        public PrimaryKeyInfo(string name, Type type, bool isAutoIncrement)
        {
        }

        private readonly string name;
        private readonly Type type;
        private readonly bool isAutoIncrement;
    }
}
