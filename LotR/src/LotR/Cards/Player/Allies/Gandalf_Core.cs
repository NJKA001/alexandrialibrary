using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
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

            public override IEffectHandle GetHandle(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return new EffectHandle();

                var enemiesToChoose = game.StagingArea.CardsInStagingArea.OfType<IEnemyInPlay>().ToList();

                foreach (var player in game.Players)
                {
                    foreach (var enemy in player.EngagedEnemies)
                        enemiesToChoose.Add(enemy);
                }

                var choice = new ChooseGandalfEffect(CardSource, controller, enemiesToChoose);
                return new EffectHandle(choice);
            }

            public override void Resolve(IGame game, IEffectHandle handle)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var chooseEffect = handle.Choice as IChooseGandalfEffect;
                if (chooseEffect == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                if (chooseEffect.EnemyToDamage != null)
                {
                    chooseEffect.EnemyToDamage.Damage += 4;
                    handle.Resolve(string.Format("dealt 4 damge to {0}", chooseEffect.EnemyToDamage.Title));
                    return;
                }
                else if (chooseEffect.DrawCards)
                {
                    controller.DrawCards(3);
                    handle.Resolve(string.Format("{0} drew three cards", controller.Name));
                    return;
                }
                else if (chooseEffect.ReduceYourThreat)
                {
                    controller.DecreaseThreat(5);
                    handle.Resolve(string.Format("{0} reduced their threat by 5", controller.Name));
                    return;
                }

                game.AddEffect(new AtEndOfRoundDiscardGandalfFromPlay(CardSource));

                handle.Resolve(GetCompletedStatus());
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

            public override void Resolve(IGame game, IEffectHandle handle)
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
