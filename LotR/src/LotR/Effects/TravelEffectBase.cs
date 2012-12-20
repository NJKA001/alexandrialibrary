using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

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
                handle.Cancel(string.Format("Could Not Travel To '{0}'", CardSource.Title));
                return;
            }

            game.QuestArea.SetActiveLocation(location);

            handle.Resolve(string.Format("Traveled To '{0}'", CardSource.Title));
        }
    }
}
