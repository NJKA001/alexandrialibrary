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
            this.name = name;
            this.type = type;
            this.isAutoIncrement = isAutoIncrement;
        }

        private readonly string name;
        private readonly Type type;
        private readonly bool isAutoIncrement;

        public string Name
        {
            get { return name; }
        }

        public Type Type
        {
            get { return type; }
        }

        public bool IsAutoIncrement
        {
            get { return isAutoIncrement; }
        }

        public static readonly PrimaryKeyInfo Default = new PrimaryKeyInfo("Id", typeof(Guid), false);
    }
}
