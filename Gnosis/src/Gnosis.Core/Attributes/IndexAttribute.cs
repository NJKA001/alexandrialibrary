using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class IndexAttribute : KeyAttribute
    {
        public IndexAttribute(string name, params string[] columns)
            : base(name, false, columns)
        {
        }
    }
}
