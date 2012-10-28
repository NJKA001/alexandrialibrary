using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Effects
{
    public abstract class TravelEffectBase
        : PassiveCardEffectBase, ITravelEffect
    {
        protected TravelEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public virtual void Travel(IGame game)
        {
            if (game.QuestArea.ActiveLocation != null)
                return;

            game.AddEffect(this);
        }

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            var location = game.StagingArea.CardsInStagingArea.OfType<ILocationInPlay>().Where(x => x.Card.Id == Source.Id).FirstOrDefault();
            if (location == null)
                return;

            game.QuestArea.SetActiveLocation(location);
        }

        public override string ToString()
        {
            return string.Format("Travel: {0}", Description);
        }
    }
}
