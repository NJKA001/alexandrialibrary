using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

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

            private void DealTwoDamageToChosenCharacter(IGame game, IEffectHandle handle, ICharacterInPlay character)
            {
                character.Damage += 2;
                handle.Resolve(string.Format("'{0}' dealt 2 damage to '{1}'", CardSource.Title, character.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return new EffectHandle(this);

                if (game.FirstPlayer == null)
                    return new EffectHandle(this);

                var questingCharacters = questPhase.GetAllCharactersCommittedToQuest().OfType<ICharacterInPlay>().ToList();
                if (questingCharacters.Count == 0)
                    return new EffectHandle(this);

                var builder =
                    new ChoiceBuilder("The first player chooses 1 character currently committed to a quest", game, game.FirstPlayer)
                        .Question(string.Format("Which character will have 2 damage dealt to it by '{0}'?", CardSource.Title))
                            .Answers(questingCharacters, (item) => string.Format("{0} ({1} damage of {2} hit points)", item.Title, item.Damage, item.Card.PrintedHitPoints), (source, handle, character) => DealTwoDamageToChosenCharacter(source, handle, character));

                return new EffectHandle(this, builder.ToChoice());
            }
        }

        private class ShadowAttackingEnemyGainsPlusOneAttack
            : ShadowEffectBase
        {
            public ShadowAttackingEnemyGainsPlusOneAttack(DolGuldurOrcs source)
                : base("Attacking enemy gets +1 Attack. (+3 Attack instead if this attack is undefended.)", source)
            {
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var bonus = enemyAttack.IsUndefended ? 3 : 1;

                game.AddEffect(new AttackModifier(game.CurrentPhase.Code, source, enemyAttack.Enemy, TimeScope.None, bonus));

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
