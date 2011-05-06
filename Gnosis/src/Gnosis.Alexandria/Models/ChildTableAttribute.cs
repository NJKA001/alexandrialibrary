using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ChildTableAttribute : Attribute
    {
        public ChildTableAttribute(string name)
        {
            this.name = name;
        }

        public ChildTableAttribute(string name, string foreignKeyColumn)
        {
            this.name = name;
            this.foreignKeyColumn = foreignKeyColumn;
        }

        public ChildTableAttribute(string name, string foreignKeyColumn, string foreignSequenceColumn)
        {
            this.name = name;
            this.foreignKeyColumn = foreignKeyColumn;
            this.foreignSequenceColumn = foreignSequenceColumn;
        }

        private readonly string name;
        private readonly string foreignKeyColumn;
        private readonly string foreignSequenceColumn;

        public string Name
        {
            get { return name; }
        }

        public string ForeignKeyColumn
        {
            get { return foreignKeyColumn; }
        }

        public string ForeignSequenceColumn
        {
            get { return foreignSequenceColumn; }
        }
    }
}
