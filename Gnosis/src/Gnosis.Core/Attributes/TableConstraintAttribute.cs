using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple=true)]
    public abstract class TableConstraintAttribute : Attribute
    {
        protected TableConstraintAttribute(string name)
        {
            this.name = name;
        }

        private readonly string name;

        public string Name
        {
            get { return name; }
        }
    }
}
