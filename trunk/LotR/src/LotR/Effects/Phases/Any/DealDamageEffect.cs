﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class DealDamageEffect
        : FrameworkEffectBase
    {
        public DealDamageEffect(IGame game, ICardInPlay cardInPlay, byte damage)
            : base("Deal Damage", GetDescription(damage, cardInPlay), game)
        {
            this.cardInPlay = cardInPlay;
            this.damage = damage;
        }

        private static string GetDescription(byte damage, ICardInPlay cardInPlay)
        {
            return string.Format("Deal {0} damage to {1}", damage, cardInPlay.Title);
        }

        private readonly ICardInPlay cardInPlay;
        private readonly byte damage;

        public override string Resolve(IGame game, IEffectOptions options)
        {
            return ToString();
        }
    }
}