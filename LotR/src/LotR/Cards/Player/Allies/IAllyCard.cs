using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Allies
{
    public interface IAllyCard
        : ICostlyCard,
        ICharacterCard
    {
    }
}
