using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class ForeignIndexAttribute : KeyAttribute
    {
        public ForeignIndexAttribute(string name, params string[] columns)
            : base(name, false, columns)
        {
        }
    }
}
