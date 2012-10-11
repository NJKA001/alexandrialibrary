using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Costs
{
    public interface IResourceCost
        : ICost
    {
        ICardInPlay<ICharacterCard> Character { get; }
        byte Resources { get; }
    }
}
