using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects;
using LotR.Effects.Modifiers;
using LotR.Games;
using LotR.Games.Phases;

namespace LotR.Cards.Player.Heroes
{
    public class Eowyn
        : HeroCardBase
    {
        public Eowyn()
            : base("Eowyn", CardSet.Core, 7, Sphere.Spirit, 9, 4, 1, 1, 3)
        {
            AddTrait(Trait.Noble);
            AddTrait(Trait.Rohan);

            AddEffect(new DiscardACardToAddOneWillpower(this));
        }

        public class DiscardACardToAddOneWillpower
            : ActionCharacterAbilityBase
        {
            public DiscardACardToAddOneWillpower(Eowyn source)
                : base("Discard 1 card from your hand to give Eowyn +1 Willpower until the end of the phase. This effect may be triggered by each player once per round.", source)
            {
            }

            public override ICost GetCost(IPhaseStep step)
            {
                return new DiscardCardsFromHand(Source, step, 1);
            }

            public override ILimit GetLimit(IPhaseStep step)
            {
                return new Limit(PlayerScope.AnyPlayer, TimeScope.Round, 1);
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var willpowerful = step.GetCardInPlay(Source.Id) as IWillpowerfulCard;
                if (willpowerful == null)
                    return;

                step.AddEffect(new WillpowerModifier(step.Phase, Source, Source, TimeScope.Phase, 1));
            }
        }
    }
}
