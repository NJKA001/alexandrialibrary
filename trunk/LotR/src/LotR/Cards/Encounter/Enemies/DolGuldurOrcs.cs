﻿using System;
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

            public override IEffectOptions GetOptions(IGame game)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return new EffectOptions();

                if (game.FirstPlayer == null)
                    return new EffectOptions();

                var questingCharacters = questPhase.GetCharactersCommitedToTheQuest(game.FirstPlayer.StateId);
                if (questingCharacters.Count() == 0)
                    return new EffectOptions();

                var availableCharacters = new Dictionary<Guid, IList<IWillpowerfulCard>> { { game.FirstPlayer.StateId, questingCharacters.Select(x => x.Card).ToList() } };

                var choice = new PlayersChooseCards<IWillpowerfulCard>("The first player chooses 1 character currently commited to a quest", Source, new List<IPlayer> { game.FirstPlayer }, 1, availableCharacters);

                return new EffectOptions(choice);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var characterChoice = options.Choice as IPlayersChooseCards<IWillpowerfulCard>;
                if (characterChoice == null)
                    return GetCancelledString();

                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null || game.FirstPlayer == null)
                    return GetCancelledString();

                var characterToRemoveFromQuest = characterChoice.GetChosenCards(game.FirstPlayer.StateId).FirstOrDefault();
                if (characterToRemoveFromQuest == null)
                    return GetCancelledString();

                var characterInPlay = game.GetAllCardsInPlay<IWillpowerfulInPlay>().Where(x => x.Card.Id == characterToRemoveFromQuest.Id).FirstOrDefault();
                if (characterInPlay == null)
                    return GetCancelledString();

                questPhase.RemoveCharacterFromQuest(characterInPlay);

                return ToString();
            }
        }

        private class ShadowAttackingEnemyGainsPlusOneAttack
            : ShadowEffectBase
        {
            public ShadowAttackingEnemyGainsPlusOneAttack(DolGuldurOrcs source)
                : base("Attacking enemy gets +1 Attack. (+3 Attack instead if this attack is undefended.)", source)
            {
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return GetCancelledString();

                var bonus = enemyAttack.IsUndefended ? 3 : 1;

                game.AddEffect(new AttackModifier(game.CurrentPhase.Code, Source, enemyAttack.Enemy, TimeScope.None, bonus));

                return ToString();
            }
        }
    }
}