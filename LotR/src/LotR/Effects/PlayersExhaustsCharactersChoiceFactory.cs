using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public class PlayersExhaustsCharactersChoiceFactory
    {
        #region Inner Classes

        private class ExhaustChoiceStatus
        {
            public ExhaustChoiceStatus(int total)
            {
                if (total < 1)
                    throw new ArgumentException("total must be great than zero");

                this.total = total;
            }

            private readonly int total;
            private readonly StringBuilder statusBuilder = new StringBuilder();

            private int count;

            public bool IsComplete
            {
                get { return count == total; }
            }

            public void Append(string status)
            {
                if (status == null)
                    throw new ArgumentNullException("status");

                count++;
                statusBuilder.AppendLine(status);
            }

            public string GetStatus()
            {
                return statusBuilder.ToString();
            }
        }

        #endregion

        public PlayersExhaustsCharactersChoiceFactory()
        {
        }

        private IDictionary<Guid, IList<IExhaustableInPlay>> GetReadyCharacters<T>(IEnumerable<IPlayer> players)
            where T : ICardInPlay
        {
            var readyCharacters = new Dictionary<Guid, IList<IExhaustableInPlay>>();

            foreach (var player in players)
            {
                readyCharacters[player.StateId] = player.CardsInPlay.OfType<T>().OfType<IExhaustableInPlay>().Where(x => !x.IsExhausted).ToList();
            }

            return readyCharacters;
        }

        private void ExhaustReadyCharacter(IEffectHandle handle, IExhaustableInPlay character, IPlayer player, ExhaustChoiceStatus choiceStatus)
        {
            character.Exhaust();

            choiceStatus.Append(string.Format("{0} chose to exhaust {1}", player.Name, character.Title));

            if (choiceStatus.IsComplete)
            {
                handle.Resolve(choiceStatus.GetStatus());
            }
        }

        private string GetPlayerNamesList(IGame game, IEnumerable<IPlayer> players)
        {
            if (game.Players.Count() == players.Count())
                return "Each player";

            var sb = new StringBuilder();

            var count = 0;
            var total = players.Count();
            foreach (var player in players)
            {
                count++;
                if (count < total)
                {
                    if (count < total - 1)
                        sb.AppendFormat("{0}, ", player.Name);
                    else
                        sb.AppendFormat("{0} and ", player.Name);
                }
                else
                    sb.Append(player.Name);
            }

            return sb.ToString();
        }

        public IChoice GetChoice<T>(IGame game, IPlayer chosingPlayer, IEnumerable<IPlayer> players, string description, bool isOptional, uint minimumChosen)
            where T : ICardInPlay
        {
            var readyHeroes = GetReadyCharacters<T>(players);

            var playerNames = GetPlayerNamesList(game, players);

            string type = "character";

            if (minimumChosen == 1)
                type = (typeof(T) == typeof(IHeroInPlay)) ? "1 hero" : "1 character";
            else
                type = (typeof(T) == typeof(IHeroInPlay)) ? string.Format("{0} heroes", minimumChosen) : string.Format("{0} characters", minimumChosen);

            var builder =
                new ChoiceBuilder(string.Format("{0} must exhaust {1} they control {2}", playerNames, type, description), game, chosingPlayer, isOptional);

            if (isOptional)
            {
                builder.Question(string.Format("Do you want to {0}?", description));
            }
            else
            {
                builder.Question(string.Format("This effect is not optional. {0} must exhaust {1} they control", playerNames, type));
            }

            if (!isOptional || (isOptional && readyHeroes.All(x => x.Value.Count > 0)))
            {
                if (isOptional)
                {
                    builder.Answer(string.Format("Yes, each player will exhaust {0} they control", type), 1);
                }
                else
                {
                    builder.Answer(string.Format("Each player must exhaust {0} they control, if able", type), 1);
                }

                var numberOfPlayers = readyHeroes.Where(x => x.Value.Count >= minimumChosen).Count();
                var totalExhausted = (int)(numberOfPlayers * minimumChosen);

                var choiceStatus = new ExhaustChoiceStatus(totalExhausted);

                foreach (var player in players)
                {
                    var items = readyHeroes[player.StateId];
                    if (items.Count < minimumChosen)
                        continue;

                    builder.Question(string.Format("{0}, which character do you want to exhaust?", player.Name), minimumChosen, minimumChosen)
                        .Answers(items, (item) => item.Title, (source, handle, character) => ExhaustReadyCharacter(handle, character, player, choiceStatus));
                }

                if (isOptional)
                {
                    builder.LastAnswer(string.Format("No, each player will not exhaust {0} they control", type), 2, (source, handle, item) => handle.Cancel(string.Format("{0} chose not to {1}", chosingPlayer.Name, description)));
                }
            }
            else
            {
                if (isOptional)
                {
                    if (players.Count() == 1)
                    {
                        builder.LastAnswer(string.Format("{0} does not have a ready hero to exhaust", playerNames), 1, (source, handle, item) => handle.Cancel(string.Format("{0} did not have a ready hero to exhaust, cancelling {1}", playerNames, description)));
                    }
                    else
                    {
                        builder.LastAnswer(string.Format("{0} do not each have a ready hero to exhaust", playerNames), 1, (source, handle, item) => handle.Cancel(string.Format("{0} did not each have a ready hero to exhaust, cancelling {1}", playerNames, description)));
                    }
                }
            }

            return builder.ToChoice();
        }
    }
}
