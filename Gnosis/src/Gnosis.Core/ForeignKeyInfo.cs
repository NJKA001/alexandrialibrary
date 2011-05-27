using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ForeignKeyInfo
    {
        public ForeignKeyInfo(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        private readonly string name;
        private readonly Type type;

        public string Name
        {
            get { return name; }
        }

        public Type Type
        {
            get { return type; }
        }

        public static readonly ForeignKeyInfo Default = new ForeignKeyInfo("Parent", typeof(Guid));
    }
}
