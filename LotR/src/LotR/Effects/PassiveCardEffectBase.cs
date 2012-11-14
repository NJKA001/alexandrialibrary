using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects
{
    public abstract class PassiveCardEffectBase
        : CardEffectBase, IPassiveEffect
    {
        protected PassiveCardEffectBase(string description, ICard cardSource)
            : base("Passive Card Effect", description, cardSource)
        {
        }

        protected PassiveCardEffectBase(string name, string description, ICard cardSource)
            : base(name, description, cardSource)
        {
        }

        protected bool IsEndOfPhase(IGame game)
        {
            switch (game.CurrentPhase.StepCode)
            {
                case PhaseStep.Combat_End:
                case PhaseStep.Encounter_End:
                case PhaseStep.Planning_End:
                case PhaseStep.Quest_End:
                case PhaseStep.Refresh_End:
                case PhaseStep.Resource_End:
                case PhaseStep.Travel_End:
                    return true;
                default:
                    return false;
            }
        }

        protected bool IsEndOfRound(IGame game)
        {
            return (game.CurrentPhase.StepCode == PhaseStep.Refresh_End);
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
