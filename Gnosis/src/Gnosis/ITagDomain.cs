using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITagDomain
    {
        int Id { get; }
        string Name { get; }
        Type[] BaseTypes { get; }
        object DefaultValue { get; }

        bool IsValid(object value);
        ITagTuple GetTuple(object value);
        object GetValue(ITagTuple tuple);
    }
}
