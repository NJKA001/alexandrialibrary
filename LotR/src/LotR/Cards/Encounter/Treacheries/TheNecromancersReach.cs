using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Cards.Encounter.Treacheries
{
    public class TheNecromancersReach
        : TreacheryCardBase
    {
        public TheNecromancersReach()
            : base("The Necromancer's Reach", CardSet.Core, 93, EncounterSet.Dol_Guldur_Orcs, 3)
        {
        }

        private class WhenRevealedDealOneDamageToEachExhaustedCharacter
            : WhenRevealedEffectBase
        {
            public WhenRevealedDealOneDamageToEachExhaustedCharacter(TheNecromancersReach source)
                : base("Deal 1 damage to each exhausted character.", source)
            {
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                foreach (var player in state.GetStates<IPlayer>())
                {
                    var playerArea = player.GetStates<IPlayerArea>().FirstOrDefault();
                    if (playerArea == null)
                        continue;

                    foreach (var exhaustable in playerArea.CardsInPlay.OfType<IExhaustableInPlay>())
                    {
                        if (!exhaustable.IsExhausted)
                            continue;

                        var damageable = exhaustable as IDamagableInPlay;
                        if (damageable == null)
                            continue;

                        damageable.Damage += 1;
                    }
                }
            }
        }
    }
}
