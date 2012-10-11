using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public interface ICheckForTraitStep
        //: IGameState
    {
        ICardInPlay<ICard> CardInPlay { get; }
        Trait Trait { get; }
        bool HasTrait { get; set; }
    }
}
