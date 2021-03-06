﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Modifiers;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public class Gimli
        : HeroCardBase
    {
        public Gimli()
            : base("Gimli", CardSet.Core, 4, Sphere.Tactics, 11, 2, 2, 2, 5)
        {
            AddTrait(Trait.Dwarf);
            AddTrait(Trait.Noble);
            AddTrait(Trait.Warrior);

            AddEffect(new StrengthBonusForDamage(this));
        }

        #region Abilities

        public class StrengthBonusForDamage
            : PassiveCharacterAbilityBase, IDuringDetermineAttack
        {
            public StrengthBonusForDamage(Gimli source)
                : base("Gimli gets +1 attack for each damage token on him.", source)
            {
            }

            public void DuringDetermineAttack(IDetermineAttack state)
            {
                state.Game.AddEffect(this);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var determineStrength = game.CurrentPhase.GetDetermineAttacks().Where(x => x.Attacker.Card.Id == source.Id).FirstOrDefault();
                if (determineStrength == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var damagable = game.GetCardInPlay<IHeroInPlay>(CardSource.Id);
                if (damagable == null)
                    { handle.Cancel(GetCancelledString()); return; }

                determineStrength.Attack += damagable.Damage;

                handle.Resolve(GetCompletedStatus());
            }
        }

        #endregion
    }
}
