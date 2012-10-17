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
            var questArea = game.GetStates<IQuestArea>().FirstOrDefault();
            if (questArea == null)
                return;

            if (questArea.ActiveLocation != null)
                return;

            game.AddEffect(this);
        }

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            var location = game.GetState<ILocationInPlay>(Source.Id);
            if (location == null)
                return;

            var questArea = game.GetStates<IQuestArea>().FirstOrDefault();
            if (questArea == null)
                return;

            questArea.SetActiveLocation(location);
        }
    }
}
