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
            var shuffleEffect = new PlayerShufflesDeck(game, player);
            var shuffleHandle = shuffleEffect.GetHandle(game);

            shuffleEffect.DuringSetup(game);

            //game.Prepare(shuffleHandle);
            game.AddEffect(shuffleEffect);
            game.TriggerEffect(shuffleHandle);
        }

        private void PlaceHeroesAndSetInitialThreat(IPlayer player)
        {
            var placeHeroesEffect = new PlayerPlacesHeroesAndSetsThreat(game, player);
            var placeHeroesHandle = placeHeroesEffect.GetHandle(game);

            placeHeroesEffect.DuringSetup(game);

            //game.Prepare(placeHeroesHandle);
            game.AddEffect(placeHeroesEffect);
            game.TriggerEffect(placeHeroesHandle);
        }

        private void DetermineFirstPlayer()
        {
            var determineFirstPlayer = new DetermineFirstPlayer(game);
            var determineFirstPlayerHandle = determineFirstPlayer.GetHandle(game);

            determineFirstPlayer.DuringSetup(game);

            //game.Prepare(determineFirstPlayerHandle);
            game.AddEffect(determineFirstPlayer);
            game.TriggerEffect(determineFirstPlayerHandle);
        }

        private void DrawSetupHand(IPlayer player)
        {
            var drawSetupHand = new PlayerDrawsStartingHand(game, player);
            drawSetupHand.DuringSetup(game);

            var drawSetupHandHandle = drawSetupHand.GetHandle(game);

            //game.Prepare(drawSetupHandHandle);
            game.AddEffect(drawSetupHand);
            game.TriggerEffect(drawSetupHandHandle);
        }

        private void SetQuestCards()
        {
            var setQuestCards = new SetQuestCards(game);
            var setQuestCardsHandle = setQuestCards.GetHandle(game);
            
            setQuestCards.DuringSetup(game);

            //game.Prepare(setQuestCardsHandle);
            game.AddEffect(setQuestCards);
            game.TriggerEffect(setQuestCardsHandle);
        }

        private void FollowScenarioSetup()
        {
            var scenarioSetup = new FollowScenarioSetup(game);
            var setupHandle = scenarioSetup.GetHandle(game);
            
            scenarioSetup.DuringSetup(game);

            //game.Prepare(setupHandle);
            game.AddEffect(scenarioSetup);
            game.TriggerEffect(setupHandle);

            foreach (var effect in game.GetEffects<ISetupEffect>())
            {
                effect.Setup(game);

                var handle = effect.GetHandle(game);
                //game.Prepare(handle);
                game.TriggerEffect(handle);
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
