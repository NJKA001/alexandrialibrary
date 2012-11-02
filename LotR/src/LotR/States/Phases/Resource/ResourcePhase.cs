using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Phases.Resource;
using LotR.States.Controllers;
using LotR.States.Phases.Any;

namespace LotR.States.Phases.Resource
{
    public class ResourcePhase
        : PhaseBase, IResourcePhase
    {
        #region Structs

        private struct CollectingResourceInfo
        {
            public CollectingResourceInfo(CollectingResourcesEffect effect, CollectingResources state)
            {
                this.effect = effect;
                this.state = state;
            }

            private readonly CollectingResourcesEffect effect;
            private readonly CollectingResources state;

            public CollectingResourcesEffect Effect
            {
                get { return effect; }
            }

            public CollectingResources State
            {
                get { return state; }
            }
        }

        #endregion

        public ResourcePhase(IGame game)
            : base(game, PhaseCode.Resource, PhaseStep.Resource_Start)
        {
        }

        private void EachPlayerCollectsResources()
        {
            var infos = new List<CollectingResourceInfo>();

            foreach (var player in Game.Players)
            {
                foreach (var resourceful in player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Card is IResourcefulCard))
                {
                    var state = new CollectingResources(Game, resourceful, 1);
                    var effect = new CollectingResourcesEffect(Game, state);
                    infos.Add(new CollectingResourceInfo(effect, state));
                }
            }

            foreach (var player in Game.Players)
            {
                foreach (var card in player.CardsInPlay.Where(x => x.HasEffect<IDuringCollectingResources>()))
                {
                    foreach (var duringCollectingResourcesEffect in card.BaseCard.Text.Effects.OfType<IDuringCollectingResources>())
                    {
                        foreach (var info in infos)
                        {
                            duringCollectingResourcesEffect.DuringCollectingResource(info.State);
                        }
                    }
                }
            }

            foreach (var info in infos)
            {
                var options = Game.GetOptions(info.Effect);

                Game.AddEffect(info.Effect);
                Game.ResolveEffect(info.Effect, options);
            }
        }

        private void EachPlayerDrawsCards()
        {
            var playerDrawOptions = new Dictionary<Guid, Tuple<bool, uint>>();

            foreach (var player in Game.Players)
            {
                playerDrawOptions[player.StateId] = new Tuple<bool, uint>(true, 1);
            }

            var playersDrawingCards = new PlayersDrawingCards(Game, playerDrawOptions);

            foreach (var card in Game.GetCardsInPlayWithEffect<ICardInPlay, IDuringDrawingResourceCards>())
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<IDuringDrawingResourceCards>())
                {
                    effect.DuringDrawingResourceCards(playersDrawingCards);
                }
            }

            foreach (var player in Game.Players)
            {
                if (!playersDrawingCards.Players.Contains(player.StateId))
                    continue;

                uint numberOfCards = playersDrawingCards.PlayerCanDrawCards(player.StateId) ?
                    playersDrawingCards.GetNumberOfCards(player.StateId)
                    : 0;
                
                Game.ResolveEffect(new DrawingCardsEffect(Game, player, numberOfCards), EffectOptions.Empty);
            }
        }

        public override void Run()
        {
            EachPlayerCollectsResources();
            
            EachPlayerDrawsCards();
            
            Game.OpenPlayerActionWindow();
        }
    }
}
