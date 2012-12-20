using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Events;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayerActionWindow
        : FrameworkEffectBase
    {
        public PlayerActionWindow(IGame game, IPlayer player)
            : base("Player Action Window", GetDescription(player), game)
        {
            this.player = player;

            player.IsActivePlayer = true;
        }

        private static string GetDescription(IPlayer player)
        {
            return string.Format("{0} has the opportunity to play cards or trigger effects", player.Name);
        }

        private readonly IPlayer player;

        private bool IsAffordable(ICostlyCard costlyCard)
        {
            var characters = player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.CanPayFor(costlyCard)).ToList();
            var sum = characters.Sum(x => x.Resources);

            return (characters.Count > 0 && (costlyCard.IsVariableCost || sum >= costlyCard.PrintedCost));
        }

        private IList<IPlayableFromHand> GetPlayableCardsInHand(IGame game)
        {
            var cards = new List<IPlayableFromHand>();

            if (game.CurrentPhase.StepCode == PhaseStep.Planning_Play_Allies_and_Attachments)
            {
                foreach (var card in player.Hand.Cards.OfType<IPlayableFromHand>())
                {
                    var costlyCard = card as ICostlyCard;
                    if (costlyCard != null)
                    {
                        if (!IsAffordable(costlyCard))
                            continue;
                    }

                    cards.Add(card);
                }
            }
            else
            {
                foreach (var card in player.Hand.Cards.OfType<IPlayableFromHand>().Where(x => x.HasEffect<IPlayerActionEffect>()))
                {
                    var costlyCard = card as ICostlyCard;

                    foreach (var effect in card.Text.Effects.OfType<IPlayerActionEffect>())
                    {
                        if (!effect.CanBeTriggered(game))
                            continue;

                        if (costlyCard != null && !IsAffordable(costlyCard))
                            continue;

                        cards.Add(card);
                    }
                }
            }

            return cards;
        }

        private IList<ICardEffect> GetPlayableEffects(IGame game)
        {
            var effects = new List<ICardEffect>();

            foreach (var card in player.Hand.Cards.Where(x => !(x is IEventCard) && x.HasEffect<IPlayerActionEffect>()))
            {
                foreach (var effect in card.Text.Effects.OfType<IPlayerActionEffect>())
                {
                    if (effect.CanBeTriggered(game))
                        effects.Add(effect);
                }
            }

            foreach (var card in player.CardsInPlay.Where(x => x.HasEffect<IPlayerActionEffect>()))
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<IPlayerActionEffect>())
                {
                    if (effect.CanBeTriggered(game))
                        effects.Add(effect);
                }
            }

            return effects;
        }

        private void PlayCardFromHand(IGame game, IEffectHandle handle, IPlayableFromHand playableCard)
        {
            var playCardEffect = playableCard.GetPlayFromHandEffect(game, player);
            game.AddEffect(playCardEffect);
            var playCardHandle = playCardEffect.GetHandle(game);
            game.TriggerEffect(playCardHandle);
            handle.Resolve(string.Format("{0} played {1} from their hand", player.Name, playableCard.Title));
        }

        private void TriggerEffect(IGame game, IEffectHandle handle, ICardEffect cardEffect)
        {
            game.AddEffect(cardEffect);
            var playEffectHandle = cardEffect.GetHandle(game);
            game.TriggerEffect(playEffectHandle);
            handle.Resolve(string.Format("{0} triggered {1}", player.Name, cardEffect.ToString()));
        }

        private void PassOnTakingAnAction(IGame game, IEffectHandle handle)
        {
            handle.Cancel(string.Format("{0} passed on taking an action", player.Name));
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            var playableCards = GetPlayableCardsInHand(game);
            var playableEffects = GetPlayableEffects(game);

            var builder =
                    new ChoiceBuilder<IGame>(string.Format("{0} can choose to take an action during the {1} step of the {2} phase", player.Name, game.CurrentPhase.StepName, game.CurrentPhase.Name), game, player);

            if (playableCards.Count == 0 && playableEffects.Count == 0)
            {
                builder.Question(string.Format("{0}, there are no actions that you can take right now", player.Name))
                        .LastAnswer("Ok, I will pass on taking actions right now", false, (source, handle, number) => PassOnTakingAnAction(source, handle));
            }
            else
            {
                builder.Question(string.Format("{0}, do you want to take an action?", player.Name));

                if (playableCards.Count > 0)
                {
                    builder.Answer<uint>("Yes, I would like to play a card from my hand", 1)
                        .Question("Which card would you like to play from your hand?")
                            .LastAnswers(playableCards, (item) => item.Title, (source, handle, costlyCard) => PlayCardFromHand(game, handle, costlyCard));
                }

                if (playableEffects.Count > 0)
                {
                    builder.Answer<uint>("Yes, I would like to trigger an effect on a card I control", 2)
                        .Question("Which effect would you like to trigger?")
                            .LastAnswers(playableEffects, (item) => item.ToString(), (source, handle, cardEffect) => TriggerEffect(source, handle, cardEffect));
                }

                builder.LastAnswer("No, I will pass on taking an action right now", false, (source, handle, number) => PassOnTakingAnAction(source, handle));
            }

            return new EffectHandle(this, builder.ToChoice()); //new ChoosePlayerAction(game, player));
        }

        /*
        public override void Trigger(IGame game, IEffectHandle handle)
        {
            var actionChoice = handle.Choice as IChoosePlayerAction;
            if (actionChoice == null)
            {
                handle.Cancel(GetCancelledString());
                return;
            }

            if (actionChoice.CardToPlay != null && actionChoice.CardToPlay is ICostlyCard)
            {
                var costlyCard = actionChoice.CardToPlay as ICostlyCard;
                var playCardEffect = new PlayCardFromHandEffect(game, costlyCard);
                game.AddEffect(playCardEffect);
                var playCardHandle = playCardEffect.GetHandle(game);
                //game.Prepare(playCardHandle);
                game.TriggerEffect(playCardHandle);
            }
            else if (actionChoice.CardEffectToTrigger != null)
            {
                game.AddEffect(actionChoice.CardEffectToTrigger);
                var playEffectHandle = actionChoice.CardEffectToTrigger.GetHandle(game);
                //game.Prepare(playEffectHandle);
                game.TriggerEffect(playEffectHandle);
            }

            handle.Resolve(GetCompletedStatus());
        }
        */
    }
}
