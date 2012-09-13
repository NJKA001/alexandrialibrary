using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects.Modifiers
{
    public abstract class ModifierBase
        : IModifier
    {
        protected ModifierBase(string description, IPhase startPhase, ICard source, Duration duration, int value)
        {
            this.Description = description;
            this.StartPhase = startPhase;
            this.Source = source;
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

        public ICard Source
        {
            get;
            private set;
        }

        public Duration Duration
        {
            get;
            private set;
        }

        public int Value
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public virtual ICost GetCost()
        {
            return null;
        }
    }
}
