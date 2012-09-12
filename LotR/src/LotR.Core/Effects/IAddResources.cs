using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects
{
    public interface IAddResources
        : IReversableEffect
    {
        IDictionary<Guid, byte> TargetsAndAmounts { get; }
    }
}
