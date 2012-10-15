using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.States
{
    public interface IPlayer
        : IState
    {
        string Name { get; }
        IPlayerDeck Deck { get; }
        IHand<IPlayerCard> Hand { get; }

        byte CurrentThreat { get; }
        bool IsFirstPlayer { get; set; }
        
        void IncreaseThreat(byte value);
        void DecreaseThreat(byte value);
        void DiscardFromHand(IEnumerable<IPlayerCard> card);
    }
}
