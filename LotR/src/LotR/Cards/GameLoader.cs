using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Cards.Quests;
using LotR.Effects;
using LotR.States;
using LotR.States.Controllers;

namespace LotR.Cards
{
    public class GameLoader
        : LoaderBase, IGameLoader
    {
        private readonly IQuestLoader questLoader = new QuestLoader();
        private readonly IPlayerDeckLoader playerDeckLoader = new PlayerDeckLoader();

        public void Load(IGame game, IEnumerable<PlayerInfo> playersInfo, ScenarioCode scenarioCode)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            if (playersInfo == null)
                throw new ArgumentNullException("playersInfo");
            if (playersInfo.Count() == 0)
                throw new ArgumentException("playersInfo is empty");

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
        }
    }
}
