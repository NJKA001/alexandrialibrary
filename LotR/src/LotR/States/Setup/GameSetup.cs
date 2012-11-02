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
            var options = game.GetOptions(shuffleDeck);

            shuffleDeck.DuringSetup(game);

            game.AddEffect(shuffleDeck);
            game.ResolveEffect(shuffleDeck, options);
        }

        private void PlaceHeroesAndSetInitialThreat(IPlayer player)
        {
            var placeHeroes = new PlayerPlacesHeroesAndSetsThreat(game, player);
            var options = game.GetOptions(placeHeroes);

            placeHeroes.DuringSetup(game);

            game.AddEffect(placeHeroes);
            game.ResolveEffect(placeHeroes, options);
        }

        private void DetermineFirstPlayer()
        {
            var determineFirstPlayer = new DetermineFirstPlayer(game);
            var options = game.GetOptions(determineFirstPlayer);

            determineFirstPlayer.DuringSetup(game);

            game.AddEffect(determineFirstPlayer);
            game.ResolveEffect(determineFirstPlayer, options);
        }

        private void DrawSetupHand(IPlayer player)
        {
            var drawSetupHand = new PlayerDrawsStartingHand(game, player);
            drawSetupHand.DuringSetup(game);

            var options = game.GetOptions(drawSetupHand);

            game.AddEffect(drawSetupHand);
            game.ResolveEffect(drawSetupHand, options);
        }

        private void SetQuestCards()
        {
            var setQuestCards = new SetQuestCards(game);
            var options = game.GetOptions(setQuestCards);
            
            setQuestCards.DuringSetup(game);

            game.AddEffect(setQuestCards);
            game.ResolveEffect(setQuestCards, options);
        }

        private void FollowScenarioSetup()
        {
            var scenarioSetup = new FollowScenarioSetup(game);
            var setupOptions = game.GetOptions(scenarioSetup);
            
            scenarioSetup.DuringSetup(game);

            game.AddEffect(scenarioSetup);
            game.ResolveEffect(scenarioSetup, setupOptions);

            foreach (var effect in game.GetEffects<ISetupEffect>())
            {
                effect.Setup(game);

                var options = game.GetOptions(effect);
                game.ResolveEffect(effect, options);
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
