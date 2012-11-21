using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects.Choices;
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
        }

        private static string GetDescription(IPlayer player)
        {
            return string.Format("{0} has the opportunity to play cards or trigger effects", player.Name);
        }

        private readonly IPlayer player;

        private IList<ICostlyCard> GetPlayableCardsInHand(IGame game)
        {
            var cards = new List<ICostlyCard>();

            if (game.CurrentPhase.StepCode == PhaseStep.Planning_Play_Allies_and_Attachments)
            {
                foreach (var card in player.Hand.Cards.OfType<ICostlyCard>())
                    cards.Add(card);
            }
            else
            {
                foreach (var card in player.Hand.Cards.OfType<ICostlyCard>().Where(x => x.HasEffect<IPlayerActionEffect>()))
                {
                    foreach (var effect in card.Text.Effects.OfType<IPlayerActionEffect>())
                    {
                        if (effect.CanBeTriggered(game))
                            cards.Add(card);
                    }
                }
            }

            return cards;
        }

        private IList<ICardEffect> GetPlayableEffects(IGame game)
        {
            var effects = new List<ICardEffect>();

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

        private void PlayCardFromHand(IGame game, IEffectHandle handle, ICostlyCard costlyCard)
        {
            var playCardEffect = new PlayCardFromHandEffect(game, costlyCard);
            game.AddEffect(playCardEffect);
            var playCardHandle = playCardEffect.GetHandle(game);
            game.TriggerEffect(playCardHandle);
            handle.Resolve(string.Format("{0} played {1} from their hand", player.Name, costlyCard.Title));
        }

        private void TriggerEffect(IGame game, IEffectHandle handle, ICardEffect cardEffect)
        {
            game.AddEffect(cardEffect);
            var playEffectHandle = cardEffect.GetHandle(game);
            game.TriggerEffect(playEffectHandle);
            handle.Resolve(string.Format("{0} triggered {1}", player.Name, cardEffect.ToString()));
        }

        private void PassOnTakingAnAction(IGame game, IEffectHandle handle, uint number)
        {
            handle.Cancel(string.Format("{0} passed on taking an action", player.Name));
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            var playableCards = GetPlayableCardsInHand(game);
            var playableEffects = GetPlayableEffects(game);

            var builder =
                new ChoiceBuilder<IGame>(string.Format("{0} can choose to take an action during the {1} step of the {2} phase", player.Name, game.CurrentPhase.StepName, game.CurrentPhase.Name), game, player)
                    .Question(string.Format("{0}, do you want to take an action?", player.Name))
                        .Answer<uint>("Yes, I will play a card from my hand", 1)
                            .Question("Which card would you like to play from your hand?")
                                .LastAnswers(playableCards, (item) => item.Title, (source, handle, costlyCard) => PlayCardFromHand(game, handle, costlyCard))
                        .Answer<uint>("Yes, I will trigger an effect on a card I control", 2)
                            .Question("Which effect would you like to trigger?")
                                .LastAnswers(playableEffects, (item) => item.ToString(), (source, handle, cardEffect) => TriggerEffect(source, handle, cardEffect))
                        .LastAnswer<uint>("No, I will pass on taking an action right now", 3, (source, handle, number) => PassOnTakingAnAction(source, handle, number));

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
