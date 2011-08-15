using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    /// <summary>
    /// Defines a supranational region or geographic area based on UN standard M.49
    /// </summary>
    public interface IRegion
    {
        int Code { get; }
        string Name { get; }
    }
}
