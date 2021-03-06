﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Allies
{
    public class Gandalf_Core
        : AllyCardBase
    {
        public Gandalf_Core()
            : base("Gandalf", CardSet.Core, 73, Sphere.Neutral, 5, 4, 4, 4, 4)
        {
            IsUnique = true;

            AddTrait(Trait.Istari);

            AddEffect(new AfterGandalfEntersPlay(this));
        }

        private class AfterGandalfEntersPlay
            : ResponseCardEffectBase, IAfterCardEntersPlay
        {
            public AfterGandalfEntersPlay(ICard cardSource)
                : base("After Gandalf enters play, (choose 1): draw three cards, deal 4 damage to 1 enemy in play, or reduce your threat by 5.", cardSource)
            {
            }

            public void AfterCardEntersPlay(ICardEntersPlay state)
            {
                if (state.EnteringPlay.BaseCard.Id != CardSource.Id)
                    return;

                state.AddEffect(this);
            }

            private IEnumerable<IEnemyInPlay> GetEnemiesInPlay(IGame game)
            {
                var enemies = game.StagingArea.CardsInStagingArea.OfType<IEnemyInPlay>().ToList();

                foreach (var player in game.Players)
                {
                    enemies.AddRange(player.EngagedEnemies);
                }

                return enemies;
            }

            private void DealFourDamageToEnemyInPlay(IGame game, IEffectHandle handle, IPlayer player, IEnemyInPlay enemy)
            {
                enemy.Damage += 4;

                handle.Resolve(string.Format("{0} chose to have '{1}' deal 4 damage to '{2}'", player.Name, CardSource.Title, enemy.Title));
            }

            private void ReduceYourThreatByFive(IGame game, IEffectHandle handle, IPlayer player)
            {
                player.DecreaseThreat(5);

                handle.Resolve(string.Format("{0} chose to reduce their threat by 5", player.Name));
            }

            private void DrawThreeCards(IGame game, IEffectHandle handle, IPlayer player)
            {
                player.DrawCards(3);

                handle.Resolve(string.Format("{0} chose to draw 3 cards", player.Name));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    throw new InvalidOperationException("Could not determine the controll of Gandalf after he entered play");

                var enemies = GetEnemiesInPlay(game);

                var builder =
                    new ChoiceBuilder(string.Format("Choose which effect you want to trigger on '{0}' after he enters play", CardSource.Title), game, controller)
                        .Question(string.Format("{0}, which effect do you want to trigger on '{1}'?", controller.Name, CardSource.Title))
                            .Answer("Draw 3 cards", 1, (source, handle, item) => DrawThreeCards(game, handle, controller));

                if (enemies.Count() > 0)
                {
                    builder.Answer("Deal 4 damage to 1 enemy in play", 2)
                        .Question("Which enemy do you want to deal 4 damage to?")
                            .LastAnswers(enemies, (item) => string.Format("'{0}' ({1} damage of {2} hit points)", item.Title, item.Damage, item.Card.PrintedHitPoints), (source, handle, enemy) => DealFourDamageToEnemyInPlay(game, handle, controller, enemy));

                }

                builder.LastAnswer("Reduce your threat by 5", 3, (source, handle, item) => ReduceYourThreatByFive(game, handle, controller));

                var choice = builder.ToChoice();

                return new EffectHandle(this, choice);
            }
        }

        private class AtEndOfRoundDiscardGandalfFromPlay
            : PassiveCardEffectBase, IUntilEndOfRound
        {
            public AtEndOfRoundDiscardGandalfFromPlay(ICard cardSource)
                : base("At the end of the round, discard Gandalf from play.", cardSource)
            {
            }

            public override bool CanBeTriggered(IGame game)
            {
                return IsEndOfRound(game);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var allyInPlay = game.GetCardInPlay<IAllyInPlay>(CardSource.Id);
                if (allyInPlay == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var allyController = game.GetController(CardSource.Id);
                if (allyController == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                allyController.RemoveCardInPlay(allyInPlay);

                allyController.Deck.Discard(new List<IPlayerCard> { allyInPlay.Card });

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
