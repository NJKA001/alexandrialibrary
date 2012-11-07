using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects.Choices
{
    public interface IChooseCardInHand<T>
        : IChoice
        where T : class, IPlayerCard
    {
        IEnumerable<T> CardsToChooseFrom { get; }
        T ChosenCard { get; set; }
    }
}
