using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ForeignUniqueIndexAttribute : KeyAttribute
    {
        public ForeignUniqueIndexAttribute(string name, params string[] columns)
            : base(name, true, columns)
        {
        }
    }
}
