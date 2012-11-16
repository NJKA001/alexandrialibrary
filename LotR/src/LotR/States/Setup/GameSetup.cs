using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Setup;
using LotR.States.Controllers;

namespace LotR.States.Setup
{
    public class GameSetup
    {
        public GameSetup(IGame game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            this.game = game;
        }

        private readonly IGame game;

        private void SetCardOwnership(IPlayer player)
        {
            foreach (var card in player.Deck.Cards)
            {
                card.Owner = player;
            }
        }

        private void ShufflePlayerDeck(IPlayer player)
        {
            var shuffleDeck = new PlayerShufflesDeck(game, player);
            var handle = game.GetHandle(shuffleDeck);

            shuffleDeck.DuringSetup(game);

            game.AddEffect(shuffleDeck);
            game.TriggerEffect(shuffleDeck, handle);
        }

        private void PlaceHeroesAndSetInitialThreat(IPlayer player)
        {
            var placeHeroes = new PlayerPlacesHeroesAndSetsThreat(game, player);
            var handle = game.GetHandle(placeHeroes);

            placeHeroes.DuringSetup(game);

            game.AddEffect(placeHeroes);
            game.TriggerEffect(placeHeroes, handle);
        }

        private void DetermineFirstPlayer()
        {
            var determineFirstPlayer = new DetermineFirstPlayer(game);
            var handle = game.GetHandle(determineFirstPlayer);

            determineFirstPlayer.DuringSetup(game);

            game.AddEffect(determineFirstPlayer);
            game.TriggerEffect(determineFirstPlayer, handle);
        }

        private void DrawSetupHand(IPlayer player)
        {
            var drawSetupHand = new PlayerDrawsStartingHand(game, player);
            drawSetupHand.DuringSetup(game);

            var handle = game.GetHandle(drawSetupHand);

            game.AddEffect(drawSetupHand);
            game.TriggerEffect(drawSetupHand, handle);
        }

        private void SetQuestCards()
        {
            var setQuestCards = new SetQuestCards(game);
            var handle = game.GetHandle(setQuestCards);
            
            setQuestCards.DuringSetup(game);

            game.AddEffect(setQuestCards);
            game.TriggerEffect(setQuestCards, handle);
        }

        private void FollowScenarioSetup()
        {
            var scenarioSetup = new FollowScenarioSetup(game);
            var setupOptions = game.GetHandle(scenarioSetup);
            
            scenarioSetup.DuringSetup(game);

            game.AddEffect(scenarioSetup);
            game.TriggerEffect(scenarioSetup, setupOptions);

            foreach (var effect in game.GetEffects<ISetupEffect>())
            {
                effect.Setup(game);

                var handle = game.GetHandle(effect);
                game.TriggerEffect(effect, handle);
            }
        }

        public void Run()
        {
            foreach (var player in game.Players)
            {
                SetCardOwnership(player);
                ShufflePlayerDeck(player);
                PlaceHeroesAndSetInitialThreat(player);
            }

            DetermineFirstPlayer();

            foreach (var player in game.Players)
            {
                DrawSetupHand(player);
            }

            SetQuestCards();
            FollowScenarioSetup();
        }
    }
}
