using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IResourceCost
        : ICost
    {
        ICharacterInPlay Character { get; }
        byte Resources { get; }
    }
}
