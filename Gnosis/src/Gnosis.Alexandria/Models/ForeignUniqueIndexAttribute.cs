using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    class ForeignUniqueIndexAttribute : KeyAttribute
    {
        public ForeignUniqueIndexAttribute(string name, params string[] columns)
            : base(name, true, columns)
        {
        }
    }
}
