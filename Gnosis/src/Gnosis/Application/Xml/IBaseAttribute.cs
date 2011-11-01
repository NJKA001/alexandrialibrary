using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface IBaseAttribute
        : IAttribute
    {
        new Uri Value { get; } 
    }
}
