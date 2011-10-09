using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagDomain
    {
        int Id { get; }
        string Name { get; }
        Type BaseType { get; }
        object DefaultValue { get; }

        bool IsValid(object value);
        string GetName(object value);
        object GetValue(string name);
    }
}
