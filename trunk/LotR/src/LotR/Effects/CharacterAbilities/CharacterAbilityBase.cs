﻿using System;
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
        }

        public abstract void Resolve(IPhaseStep step, IPayment payment);
    }
}