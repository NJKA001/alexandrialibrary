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
        : CharacterCardBase, IAllyCard
    {
        protected AllyCardBase(string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte printedCost, byte printedWillpower, byte printedAttack, byte printedDefense, byte printedHitPoints)
            : base(CardType.Ally, title, cardSet, cardNumber, printedSphere, printedWillpower, printedAttack, printedDefense, printedHitPoints)
        {
            this.PrintedCost = printedCost;
        }

        protected bool IsVariableCost
        {
            get;
            set;
        }

        public byte PrintedCost
        {
            get;
            private set;
        }

        public IEffect GetCost(IGame game, IPlayer player)
        {
            return new PayResourcesEffect(game, PrintedSphere, PrintedCost, IsVariableCost, player, this);
        }
    }
}
