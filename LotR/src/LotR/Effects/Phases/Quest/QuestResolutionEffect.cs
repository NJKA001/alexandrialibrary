using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;
using LotR.States.Phases.Quest;

namespace LotR.Effects.Phases.Quest
{
    public class QuestResolutionEffect
        : FrameworkEffectBase
    {
        public QuestResolutionEffect(IGame game, IQuestOutcome outcome)
            : base("Quest Resolution", "Compare the combined willpower of committed characters against the combined threat of all cards in the staging area", game)
        {
            this.outcome = outcome;
        }

        private readonly IQuestOutcome outcome;

        private static string GetProgressDescription(byte value)
        {
            return value > 1 ? string.Format("{0} progress tokens", value) : "1 progress token";
        }

        private void ResolveSuccessfulQuest(IGame game, IEffectHandle handle, byte difference)
        {
            game.QuestArea.AddProgress(difference);
            
            var description = GetProgressDescription(difference);
            var verb = difference > 1 ? "were" : "was";
            handle.Resolve(string.Format("Questing was successful: {0} {1} added to the current quest.", description, verb));
        }

        private void ResolveFailedQuest(IGame game, IEffectHandle handle, int difference)
        {
            var sb = new StringBuilder("Questing was unsuccessful:\r\n");

            foreach (var player in game.Players)
            {
                var value = outcome.GetThreatIncrease(player.StateId);
                if (value > 0)
                {
                    player.IncreaseThreat(value);

                    sb.AppendFormat("{0} raised their threat by {1}.\r\n", player.Name, value);
                }
                else
                {
                    sb.AppendFormat("{0} did not raise their threat.\r\n", player.Name);
                }
            }

            handle.Resolve(sb.ToString());
        }

        private void ResolveTiedQuest(IGame game, IEffectHandle handle)
        {
            handle.Resolve("Questing was neither successful nor a failure. No progress is made on the current quest and players do not raise their threat.");
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            IChoiceBuilder builder = 
                new ChoiceBuilder("Quest Resolution", game, game.FirstPlayer);

            var check = Math.Abs(outcome.TotalWillpower - outcome.TotalThreat);
            byte difference = check <= 255 ? (byte)check : (byte)255;

            if (outcome.IsQuestSuccessful && difference > 0)
            {
                var description = GetProgressDescription(difference);
                builder.Question("The total willpower of committed characters is greater than the total threat of all cards in the staging area.")
                    .LastAnswer(string.Format("Ok, questing was successful. {0} will be added to the current quest.", description), difference, (source, handle, item) => ResolveSuccessfulQuest(source, handle, item));
            }
            else if (outcome.IsQuestFailed && difference > 0)
            {
                builder.Question("The total willpower of committed characters is less than the total threat of all cards in the staging area.")
                    .LastAnswer(string.Format("Ok, questing was unsuccessful. Each player's threat will be raised by {0}.", difference), difference, (source, handle, item) => ResolveFailedQuest(source, handle, item));
            }
            else
            {
                builder.Question("The total willpower of committed characters is equal to the total threat of all cards in the staging area.")
                    .LastAnswer("Ok, questing was neither successful nor a failure", difference, (source, handle, item) => ResolveTiedQuest(source, handle));
            }

            return new EffectHandle(this, builder.ToChoice());
        }
    }
}
