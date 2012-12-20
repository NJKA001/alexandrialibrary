using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;

namespace LotR.States
{
    public interface ICharacterInPlay
        : ICardInPlay<ICharacterCard>
    {
        byte HitPoints { get; set; }

        bool CanPayFor(ICostlyCard costlyCard);
        bool CanPayFor(ICardEffect cardEffect);
    }
}
