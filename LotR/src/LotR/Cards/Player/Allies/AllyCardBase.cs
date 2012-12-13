using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.States;

namespace LotR.Cards.Player.Allies
{
    public abstract class AllyCardBase
        : CharacterCardBase, IAllyCard, IPlayableFromHand
    {
        protected AllyCardBase(string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte printedCost, byte printedWillpower, byte printedAttack, byte printedDefense, byte printedHitPoints)
            : base(CardType.Ally, title, cardSet, cardNumber, printedSphere, printedWillpower, printedAttack, printedDefense, printedHitPoints)
        {
            this.PrintedCost = printedCost;
        }

        public byte PrintedCost
        {
            get;
            private set;
        }

        public bool IsVariableCost
        {
            get;
            protected set;
        }

        public IPlayCardFromHandEffect GetPlayFromHandEffect(IGame game, IPlayer player)
        {
            return new PlayAllyEffect(game, PrintedSphere, PrintedCost, player, this);
        }
    }
}
