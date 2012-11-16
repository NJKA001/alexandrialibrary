﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Effects
{
    public abstract class EffectBase
        : IEffect
    {
        protected EffectBase(EffectType type, string text, ISource source)
        {
            if (type == EffectType.None)
                throw new ArgumentException("type cannot be None");

            this.type = type;
            this.text = text;
            this.source = source;
        }

        private readonly EffectType type;
        private readonly string text;
        private readonly ISource source;

        protected bool IsPlayAlliesAndAttachmentsStep(IGame game)
        {
            return game.CurrentPhase.StepCode == PhaseStep.Planning_Play_Allies_and_Attachments;
        }

        protected bool IsPlayerActionWindow(IGame game)
        {
            switch (game.CurrentPhase.StepCode)
            {
                case PhaseStep.Combat_Player_Actions_Before_Chosing_An_Attacking_Enemy:
                case PhaseStep.Combat_Player_Actions_Before_Declaring_Defenders:
                case PhaseStep.Combat_Player_Actions_Before_Declaring_Target_Enemy:
                case PhaseStep.Combat_Player_Actions_Before_Determine_Enemy_Combat_Damage:
                case PhaseStep.Combat_Player_Actions_Before_Determining_Character_Attack_Strength:
                case PhaseStep.Combat_Player_Actions_Before_Determining_Character_Combat_Damage:
                case PhaseStep.Combat_Player_Actions_Before_End:
                case PhaseStep.Combat_Player_Actions_Before_Resolve_Shadow_Effects:
                case PhaseStep.Encounter_Player_Actions_Before_End:
                case PhaseStep.Encounter_Player_Actions_Before_Engagement_Checks:
                case PhaseStep.Encounter_Player_Actions_Before_Optional_Engagement:
                case PhaseStep.Planning_Play_Allies_and_Attachments:
                case PhaseStep.Planning_Player_Actions_Before_End:
                case PhaseStep.Quest_Player_Actions_Before_Commit_Characters:
                case PhaseStep.Quest_Player_Actions_Before_End:
                case PhaseStep.Quest_Player_Actions_Before_Quest_Resolution:
                case PhaseStep.Quest_Player_Actions_Before_Staging:
                case PhaseStep.Refresh_Player_Actions_Before_End:
                case PhaseStep.Resource_Player_Actions_Before_End:
                case PhaseStep.Travel_Player_Actions_Before_End:
                case PhaseStep.Travel_Player_Actions_Before_Traveling:
                    return true;
                default:
                    return false;
            }
        }

        protected string GetCancelledString()
        {
            return string.Format("Effect Cancelled: {0}", ToString());
        }

        public EffectType Type
        {
            get { return type; }
        }

        public string Text
        {
            get { return text; }
        }

        public ISource Source
        {
            get { return source; }
        }

        public virtual IEffectOptions GetOptions(IGame game)
        {
            return new EffectOptions();
        }

        public virtual bool CanBeTriggered(IGame game)
        {
            return IsPlayerActionWindow(game);
        }

        public virtual bool PaymentAccepted(IGame game, IEffectOptions options)
        {
            return true;
        }

        public virtual string Resolve(IGame game, IEffectOptions options)
        {
            return ToString();
        }
    }
}
