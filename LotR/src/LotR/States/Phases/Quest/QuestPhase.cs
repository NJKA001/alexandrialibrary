using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases.Quest;
using LotR.States.Controllers;

namespace LotR.States.Phases.Quest
{
    public class QuestPhase
        : PhaseBase, IQuestPhase
    {
        public QuestPhase(IGame game)
            : base(game, PhaseCode.Quest, PhaseStep.Quest_Start)
        {
            NumberOfEncounterCardsToReveal = (byte)game.Players.Count();
        }

        private readonly IDictionary<Guid, IList<IWillpowerfulInPlay>> committedToQuest = new Dictionary<Guid, IList<IWillpowerfulInPlay>>();
        private readonly IDictionary<Guid, bool> exhaustsToQuest = new Dictionary<Guid, bool>();

        public byte NumberOfEncounterCardsToReveal
        {
            get;
            set;
        }

        public bool IsCommittedToQuest(Guid cardId)
        {
            return committedToQuest.Any(x => x.Value.Select(y => y.Card.Id).Contains(cardId));
        }

        public bool IsExhaustedToQuest(Guid cardId)
        {
            return exhaustsToQuest.ContainsKey(cardId) ? exhaustsToQuest[cardId] : true;
        }

        public IEnumerable<IWillpowerfulInPlay> GetAllCharactersCommittedToQuest()
        {
            var all = new List<IWillpowerfulInPlay>();

            foreach (var pair in committedToQuest)
            {
                all.AddRange(pair.Value.ToList());
            }

            return all;
        }

        public IEnumerable<IWillpowerfulInPlay> GetCharactersCommitedToTheQuest(Guid playerId)
        {
            if (!committedToQuest.ContainsKey(playerId))
                return Enumerable.Empty<IWillpowerfulInPlay>();

            return committedToQuest[playerId].ToList();
        }

        public void CommitCharacterToQuest(IWillpowerfulInPlay character)
        {
            if (character == null)
                throw new ArgumentNullException("character");

            var player = character.GetController(Game);
            if (player == null)
                return;

            if (!committedToQuest.ContainsKey(player.StateId))
                committedToQuest.Add(player.StateId, new List<IWillpowerfulInPlay> { character });
            else
                committedToQuest[player.StateId].Add(character);

            exhaustsToQuest[character.Card.Id] = true;
        }

        public void RemoveCharacterFromQuest(IWillpowerfulInPlay character)
        {
            if (character == null)
                throw new ArgumentNullException("character");

            foreach (var pair in committedToQuest)
            {
                var match = pair.Value.Where(x => x.Card.Id == character.Card.Id).FirstOrDefault();
                if (match != null)
                {
                    pair.Value.Remove(character);
                    return;
                }
            }

            if (exhaustsToQuest.ContainsKey(character.Card.Id))
                exhaustsToQuest.Remove(character.Card.Id);
        }

        public void CharacterExhaustsToQuest(Guid cardId)
        {
            if (!exhaustsToQuest.ContainsKey(cardId))
                return;

            exhaustsToQuest[cardId] = true;
        }

        public void CharacterDoesNotExhaustToQuest(Guid cardId)
        {
            if (!exhaustsToQuest.ContainsKey(cardId))
                return;

            exhaustsToQuest[cardId] = false;
        }

        private void BeforeCommittingToQuest()
        {
            foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IBeforeCommittingToQuest>())
            {
                foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IBeforeCommittingToQuest>())
                {
                    effect.BeforeCommittingToQuest(Game);
                }
            }
        }

        private void DuringCommitingToQuest()
        {
            foreach (var player in Game.Players)
            {
                var commitEffect = new CommitCharactersToQuestEffect(Game, player, this);
                var commitHandle = commitEffect.GetHandle(Game);
                Game.TriggerEffect(commitHandle);
            }

            foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IDuringCommittingToQuest>())
            {
                foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IDuringCommittingToQuest>())
                {
                    effect.DuringCommittingToQuest(Game);
                }
            }
        }

        private void AfterCommittingToQuest()
        {
            foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IAfterCommittingToQuest>())
            {
                foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IAfterCommittingToQuest>())
                {
                    effect.AfterCommittingToQuest(Game);
                }
            }
        }


        private void BeforeStaging(IQuestOutcome outcome)
        {
            foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IBeforeStaging>())
            {
                foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IBeforeStaging>())
                {
                    effect.BeforeStaging(outcome);
                }
            }
        }

        private void DuringStaging(IQuestOutcome outcome)
        {
            if (NumberOfEncounterCardsToReveal == 0)
                return;

            for (var i = 1; i <= NumberOfEncounterCardsToReveal; i++)
            {
                if (Game.StagingArea.EncounterDeck.Cards.Count() == 0)
                {
                    Game.StagingArea.EncounterDeck.ShuffleDiscardPileIntoDeck();
                }

                Game.StagingArea.RevealEncounterCards(1);

                if (Game.StagingArea.RevealedEncounterCard != null)
                {
                    outcome.EncounterCardRevealed(Game.StagingArea.RevealedEncounterCard);
                }

                foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IDuringStaging>())
                {
                    foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IDuringStaging>())
                    {
                        effect.DuringStaging(outcome);
                    }
                }
            }
        }

        private void AfterStaging(IQuestOutcome outcome)
        {
            foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IAfterStaging>())
            {
                foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IAfterStaging>())
                {
                    effect.AfterStaging(outcome);
                }
            }
        }

        private void BeforeQuestResolution(IQuestOutcome outcome)
        {
            foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IBeforeQuestResolution>())
            {
                foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IBeforeQuestResolution>())
                {
                    effect.BeforeQuestResolution(outcome);
                }
            }
        }

        private void DuringQuestResolution(IQuestOutcome outcome)
        {
            var totalWillpower = (uint)GetAllCharactersCommittedToQuest().Sum(x => x.Willpower);
            var totalThreat = (uint)Game.StagingArea.CardsInStagingArea.OfType<IThreateningInPlay>().Sum(x => x.Threat);

            outcome.Resolve(totalWillpower, totalThreat);

            foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IDuringQuestResolution>())
            {
                foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IDuringQuestResolution>())
                {
                    effect.DuringQuestResolution(outcome);
                }
            }
        }

        private void AfterQuestResolution(IQuestOutcome outcome)
        {
            foreach (var cardInPlay in Game.GetCardsInPlayWithEffect<ICardInPlay, IAfterQuestResolution>())
            {
                foreach (var effect in cardInPlay.BaseCard.Text.Effects.OfType<IAfterQuestResolution>())
                {
                    effect.AfterQuestResolution(outcome);
                }
            }
        }

        private void ExhaustCommittedCharacters()
        {
            foreach (var character in GetAllCharactersCommittedToQuest().OfType<IExhaustableInPlay>())
            {
                if (IsExhaustedToQuest(character.Card.Id))
                {
                    character.Exhaust();
                }
            }
        }

        private void RaisePlayersThreat(IQuestOutcome outcome)
        {
            if (!outcome.IsQuestFailed)
                return;

            foreach (var player in Game.Players)
            {
                var value = outcome.GetThreatIncrease(player.StateId);
                if (value == 0)
                    continue;

                player.IncreaseThreat(value);
            }
        }

        public override void Run()
        {
            var outcome = new QuestOutcome(Game);

            StepCode = PhaseStep.Quest_Start;

            StepCode = PhaseStep.Quest_Player_Actions_Before_Commit_Characters;

            Game.OpenPlayerActionWindow();

            StepCode = PhaseStep.Quest_Commit_Characters;

            BeforeCommittingToQuest();

            DuringCommitingToQuest();

            ExhaustCommittedCharacters();

            AfterCommittingToQuest();

            StepCode = PhaseStep.Quest_Player_Actions_Before_Staging;

            Game.OpenPlayerActionWindow();

            StepCode = PhaseStep.Quest_Staging;

            BeforeStaging(outcome);

            DuringStaging(outcome);

            AfterStaging(outcome);

            StepCode = PhaseStep.Quest_Player_Actions_Before_Quest_Resolution;

            Game.OpenPlayerActionWindow();

            StepCode = PhaseStep.Quest_Quest_Resolution;

            BeforeQuestResolution(outcome);

            DuringQuestResolution(outcome);

            StepCode = PhaseStep.Quest_Player_Actions_Before_End;

            Game.OpenPlayerActionWindow();

            StepCode = PhaseStep.Quest_End;
        }
    }
}
