using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Cards.Quests;
using LotR.States;

namespace LotR.Cards
{
    public class GameLoader
        : LoaderBase, IGameLoader
    {
        private readonly IQuestLoader questLoader = new QuestLoader();
        private readonly IPlayerDeckLoader playerDeckLoader = new PlayerDeckLoader();

        public IGame Load(IEnumerable<PlayerInfo> playersInfo, ScenarioCode scenarioCode)
        {
            if (playersInfo == null)
                throw new ArgumentNullException("playersInfo");
            if (playersInfo.Count() == 0)
                throw new ArgumentException("playersInfo is empty");

            var game = new Game();

            var questArea = questLoader.Load(game, scenarioCode);

            var players = new List<IPlayer>();

            foreach (var info in playersInfo)
            {
                var playerDeck = playerDeckLoader.Load(info.DeckPath);
                if (playerDeck != null)
                {
                    players.Add(new LotR.States.Player(game, info.Name, playerDeck));
                }
            }

            game.Setup(questArea, players);

            return game;
        }
    }
}
