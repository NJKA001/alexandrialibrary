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
        Type BaseType { get; }
        object DefaultValue { get; }

        bool IsValid(object value);
        string GetToken(object value);
        byte[] GetData(object value);
        object GetValue(string token, byte[] data);
    }
}
