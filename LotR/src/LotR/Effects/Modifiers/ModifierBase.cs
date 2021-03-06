﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Effects.Modifiers
{
    public abstract class ModifierBase
        : EffectBase, IModifier
    {
        protected ModifierBase(string type, PhaseCode startPhase, ISource source, ICardInPlay target, TimeScope duration, int value)
            : base("Modifier", GetText(target, type, value), source)
        {
            this.StartPhase = startPhase;
            this.Target = target;
            this.Duration = duration;
            this.Value = value;
        }

        private static string GetText(ICardInPlay target, string type, int value)
        {
            return value > -1 ?
                string.Format("{0}: +{1} to {2}", target.Title, value, type)
                : string.Format("{0}: -{1} to {2}", target.Title, Math.Abs(value), type);
        }

        public PhaseCode StartPhase
        {
            get;
            private set;
        }

        public ICardInPlay Target
        {
            get;
            private set;
        }

        public TimeScope Duration
        {
            get;
            private set;
        }

        public int Value
        {
            get;
            private set;
        }
    }
}
