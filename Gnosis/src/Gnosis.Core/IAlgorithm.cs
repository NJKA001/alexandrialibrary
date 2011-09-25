using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IAlgorithm
    {
        int Id { get; }
        string Name { get; }
        string Execute(string value);
        string Execute(byte[] value);
    }
}
