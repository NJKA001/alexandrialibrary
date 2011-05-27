using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrimaryKeyColumnAttribute : Attribute
    {
        public PrimaryKeyColumnAttribute()
        {
        }

        public PrimaryKeyColumnAttribute(string name)
        {
            this.name = name;
        }

        public PrimaryKeyColumnAttribute(string name, bool isAutoIncrement)
        {
            this.name = name;
            this.isAutoIncrement = isAutoIncrement;
        }

        public PrimaryKeyColumnAttribute(bool isAutoIncrement)
        {
            this.isAutoIncrement = isAutoIncrement;
        }

        private readonly string name = string.Empty;
        private readonly bool isAutoIncrement = false;

        public string Name
        {
            get { return name; }
        }

        public bool IsAutoIncrement
        {
            get { return isAutoIncrement; }
        }
    }
}
