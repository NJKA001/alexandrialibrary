using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class PrimaryKeyAttribute : KeyAttribute
    {
        public PrimaryKeyAttribute(string name, params string[] columns)
            : base(name, true, columns)
        {
        }

        public PrimaryKeyAttribute(string name, bool autoIncrement, params string[] columns)
            : base(name, true, columns)
        {
            this.autoIncrement = autoIncrement;
        }

        private readonly bool autoIncrement;

        public bool AutoIncrement
        {
            get { return autoIncrement; }
        }
    }
}
