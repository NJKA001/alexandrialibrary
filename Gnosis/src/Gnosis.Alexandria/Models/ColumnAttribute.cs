using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string name)
        {
            this.name = name;
        }

        public ColumnAttribute(string name, object defaultValue)
        {
            this.name = name;
            this.defaultValue = defaultValue;
        }

        private readonly string name;
        private readonly object defaultValue;

        public string Name
        {
            get { return name; }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
        }
    }
}
