using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PersistenceIgnoreAttribute : Attribute
    {
    }
}
