using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrimaryKeyColumnAttribute : Attribute
    {
        public PrimaryKeyColumnAttribute()
        {
        }

        public PrimaryKeyColumnAttribute(bool autoIncrement)
        {
            this.autoIncrement = autoIncrement;
        }

        private bool autoIncrement;

        public bool AutoIncrement
        {
            get { return autoIncrement; }
        }
    }
}
