using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Effects
{
    public abstract class TravelEffectBase
        : PassiveCardEffectBase, ITravelEffect
    {
        protected TravelEffectBase(string description, ICard cardSource)
            : base(description, cardSource)
        {
        }

        public virtual void Travel(IGame game)
        {
            if (game.QuestArea.ActiveLocation != null)
                return;

            game.AddEffect(this);
        }

        public override void Trigger(IGame game, IEffectHandle handle)
        {
            var location = game.StagingArea.CardsInStagingArea.OfType<ILocationInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
            if (location == null)
            {
                handle.Cancel(GetCancelledString());
                return;
            }

            game.QuestArea.SetActiveLocation(location);

            handle.Resolve(string.Format("Travel: {0}", text));
        }
    }
}
