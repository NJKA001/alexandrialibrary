using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IAttributeFactory
    {
        string AttributeName { get; }
        bool IsValidFor(IAttribute attribute);
        IAttribute Create(IQualifiedName name, string value);
    }
}
