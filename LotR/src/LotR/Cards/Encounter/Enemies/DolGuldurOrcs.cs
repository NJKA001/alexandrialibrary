using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Modifiers;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Phases.Combat;
using LotR.States.Phases.Quest;

namespace LotR.Cards.Encounter.Enemies
{
    public class DolGuldurOrcs
        : EnemyCardBase
    {
        public DolGuldurOrcs()
            : base("Dol Guldur Orcs", CardSet.Core, 89, EncounterSet.Dol_Guldur_Orcs, 3, 2, 10, 2, 0, 3, 0)
        {
            AddTrait(Trait.Dol_Guldur);
            AddTrait(Trait.Orc);
        }

        private class WhenRevealedFirstPlayerDealsTwoDamageToQuestingCharacter
            : WhenRevealedEffectBase
        {
            public WhenRevealedFirstPlayerDealsTwoDamageToQuestingCharacter(DolGuldurOrcs source)
                : base("The first player chooses 1 character currently committed to a quest. Deal 2 damage to that character.", source)
            {
            }

            public override IChoice GetChoice(IGameState state)
            {
                if (state.CurrentPhase != Phase.Quest)
                    return null;

                var committedToQuest = state.GetStates<ICharactersCommittedToQuest>().FirstOrDefault();
                if (committedToQuest == null)
                    return null;

                var questingCharacters = committedToQuest.GetCharactersCommittedToQuest(state.FirstPlayer.StateId);
                if (questingCharacters.Count() == 0)
                    return null;

                var availableCharacters = new Dictionary<Guid, IList<IWillpowerfulCard>> { { state.FirstPlayer.StateId, questingCharacters.Select(x => x.Card).ToList() } };

                return new PlayersChooseCards<IWillpowerfulCard>("The first player chooses 1 character currently commited to a quest", Source, new List<IPlayer> { state.FirstPlayer }, 1, availableCharacters);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var characterChoice = choice as IPlayersChooseCards<IWillpowerfulCard>;
                if (characterChoice == null)
                    return;
            }
        }

        private class ShadowAttackingEnemyGainsPlusOneAttack
            : ShadowEffectBase
        {
            public ShadowAttackingEnemyGainsPlusOneAttack(DolGuldurOrcs source)
                : base("Attacking enemy gets +1 Attack. (+3 Attack instead if this attack is undefended.)", source)
            {
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var enemyAttack = state.GetStates<IEnemyAttack>().FirstOrDefault();
                if (enemyAttack == null)
                    return;

                var bonus = enemyAttack.IsUndefended ? 3 : 1;

                state.AddEffect(new AttackModifier(state.CurrentPhase, Source, enemyAttack.Enemy, TimeScope.None, bonus));
            }
        }
    }
}
