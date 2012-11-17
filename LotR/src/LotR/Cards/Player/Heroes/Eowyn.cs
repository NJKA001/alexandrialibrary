using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Modifiers;
using LotR.States;
using LotR.States.Phases.Any;

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

            public override IEffectHandle GetHandle(IGame game)
            {
                var limit = new Limit(PlayerScope.AnyPlayer, TimeScope.Round, 1);
                var cost = new DiscardCardsFromHand(source, game, 1);
                return new EffectHandle(this, null, cost, limit);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var willpowerful = controller.CardsInPlay.OfType<IWillpowerfulInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (willpowerful == null)
                    { handle.Cancel(GetCancelledString()); return; }

                game.AddEffect(new WillpowerModifier(game.CurrentPhase.Code, source, willpowerful, TimeScope.Phase, 1));

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
