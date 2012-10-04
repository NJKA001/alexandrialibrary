using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public interface IAddResources
        : IReversableEffect
    {
        IDictionary<Guid, byte> TargetsAndAmounts { get; }
    }
}
