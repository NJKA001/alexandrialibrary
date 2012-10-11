﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Cards.Player.Events
{
    public class EverVigilant
        : EventCardBase
    {
        public EverVigilant()
            : base("Ever Vigilant", CardSet.Core, 20, Sphere.Leadership, 1)
        {
        }

        public class ReadyOneAlly
            : ActionEffectBase
        {
            public ReadyOneAlly(EverVigilant source)
                : base("Choose and ready 1 ally card.", source)
            {
            }

            public override IChoice GetChoice(IGameState state)
            {
                return new ChooseAlly(Source);
            }

            public override void Resolve(IGameState state, IChoice choice)
            {
                var allyChoice = choice as IChooseAlly;
                if (allyChoice == null || allyChoice.Ally == null)
                    return;

                //allyChoice.Ally.Ready();
            }
        }
    }
}
