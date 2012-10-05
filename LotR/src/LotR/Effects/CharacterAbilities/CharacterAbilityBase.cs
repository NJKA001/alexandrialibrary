using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.CharacterAbilities
{
    public abstract class CharacterAbilityBase
        : CardEffectBase, ICharacterAbility
    {
        protected CharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
            this.Source = source;
        }

        public new IPlayerCard Source
        {
            get;
            private set;
        }

        public abstract void Resolve(IPhaseStep step, IPayment payment);
    }
}
