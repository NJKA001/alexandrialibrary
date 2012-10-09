﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Games.Phases;

namespace LotR.Effects.Modifiers
{
    public abstract class ModifierBase
        : EffectBase, IModifier
    {
        protected ModifierBase(string description, IPhase startPhase, ISource source, ICard target, TimeScope duration, int value)
            : base(description)
        {
            this.StartPhase = startPhase;
            this.Source = source;
            this.Target = target;
            this.Duration = duration;
            this.Value = value;
        }

        protected static string GetDefaultDescription(string name, int value)
        {
            return string.Format("{0} {1}", name, value);
        }

        public IPhase StartPhase
        {
            get;
            private set;
        }

        public ISource Source
        {
            get;
            private set;
        }

        public ICard Target
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
