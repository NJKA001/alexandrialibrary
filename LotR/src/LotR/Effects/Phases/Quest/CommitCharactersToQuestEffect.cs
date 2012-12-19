using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;
using LotR.States.Phases.Quest;

namespace LotR.Effects.Phases.Quest
{
    public class CommitCharactersToQuestEffect
        : FrameworkEffectBase
    {
        public CommitCharactersToQuestEffect(IGame game, IPlayer player, IQuestPhase questPhase)
            : base("Commit Characters", GetText(player), game)
        {
            if (player == null)
                throw new ArgumentNullException("player");
            if (questPhase == null)
                throw new ArgumentNullException("questPhase");

            this.player = player;
            this.questPhase = questPhase;
        }

        private static string GetText(IPlayer player)
        {
            return string.Format("{0} chooses which characters they want to commit to the quest", player.Name);
        }

        private readonly IPlayer player;
        private readonly IQuestPhase questPhase;

        private List<IWillpowerfulInPlay> GetReadyCharacters()
        {
            return player.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => !x.IsExhausted).OfType<IWillpowerfulInPlay>().ToList();
        }

        private void DoNotCommitAnyCharactersToTheQuest(IEffectHandle handle)
        {
            handle.Resolve(string.Format("{0} chose not commit any characters to the quest", player.Name));
        }

        private void CommitCharacterToTheQuest(IGame game, IEffectHandle handle, IWillpowerfulInPlay character)
        {
            questPhase.CommitCharacterToQuest(character);

            handle.Resolve(string.Format("{0} chose to commit '{1}' to the quest", player.Name, character.Title));
        }

        public override bool CanBeTriggered(IGame game)
        {
            return game.CurrentPhase.StepCode == PhaseStep.Quest_Commit_Characters;
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            var builder =
                new ChoiceBuilder(GetText(player), game, player);

            var characters = GetReadyCharacters();

            if (characters.Count == 0)
            {
                builder.Question("You do not have any ready characters available to commit to the quest")
                    .LastAnswer("Ok, I will not commit any characters to the quest", false, (source, handle, item) => DoNotCommitAnyCharactersToTheQuest(handle));
            }
            else
            {
                var description = characters.Count > 1 ? string.Format("{0} characters", characters.Count) : "1 character";
                var threat = game.StagingArea.CardsInStagingArea.OfType<IThreateningInPlay>().Sum(x => x.Threat);
                builder.Question(string.Format("You have {0} available to commit to the quest and there is currently {1} threat in the staging area.\r\nDo you want to commit any characters to the quest?", description, threat))
                    .Answer("Yes, I want to commit characters to the quest", true)
                        .Question("Which characters do you want to commit to the quest?")
                            .LastAnswers(characters, (item) => string.Format("{0} ({1} willpower)", item.Title, item.Willpower), (source, handle, character) => CommitCharacterToTheQuest(source, handle, character))
                    .LastAnswer("No, I do not want to commit characters to the quest", false, (source, handle, item) => DoNotCommitAnyCharactersToTheQuest(handle));
                    
            }

            return new EffectHandle(this, builder.ToChoice());
        }
    }
}
