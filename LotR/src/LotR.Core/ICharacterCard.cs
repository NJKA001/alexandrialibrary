using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ICharacterCard
        : IPlayerCard, IKillableCard
    {
        byte Willpower { get; }
    }
}
