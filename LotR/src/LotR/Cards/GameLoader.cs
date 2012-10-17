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

        public IGameState Load(IEnumerable<PlayerInfo> playersInfo, ScenarioCode scenario)
        {
            if (playersInfo == null)
                throw new ArgumentNullException("playersInfo");
            if (playersInfo.Count() == 0)
                throw new ArgumentException("playersInfo is empty");

            var questArea = questLoader.Load(scenario);

            var players = new List<IPlayer>();

            foreach (var info in playersInfo)
            {
                var playerDeck = playerDeckLoader.Load(info.DeckPath);
                if (playerDeck != null)
                {
                    players.Add(new LotR.States.Player(info.Name, playerDeck));
                }
            }

            return new GameState(questArea, players);
        }
    }
}
