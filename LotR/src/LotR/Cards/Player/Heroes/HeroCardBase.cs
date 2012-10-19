using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases.Any;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public abstract class HeroCardBase
        : CharacterCardBase, IHeroCard
    {
        protected HeroCardBase(string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte threatCost, byte printedWillpower, byte printedAttack, byte printedDefense, byte printedHitPoints)
            : base(CardType.Hero, title, cardSet, cardNumber, printedSphere, printedWillpower, printedAttack, printedDefense, printedHitPoints)
        {
            this.ThreatCost = threatCost;
            this.IsUnique = true;
        }

        public byte ThreatCost
        {
            get;
            private set;
        }
    }
}
