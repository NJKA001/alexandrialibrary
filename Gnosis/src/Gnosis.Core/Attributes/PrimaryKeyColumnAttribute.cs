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

        public PrimaryKeyColumnAttribute(string name, bool autoIncrement)
        {
            this.name = name;
            this.autoIncrement = autoIncrement;
        }

        public PrimaryKeyColumnAttribute(bool autoIncrement)
        {
            this.autoIncrement = autoIncrement;
        }

        private readonly string name = string.Empty;
        private readonly bool autoIncrement = false;

        public string Name
        {
            get { return name; }
        }

        public bool AutoIncrement
        {
            get { return autoIncrement; }
        }
    }
}
