using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Attachments;
using LotR.Cards.Player.Treasures;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayAttachableEffect
        : PayResourcesEffectBase
    {
        public PlayAttachableEffect(IGame game, Sphere resourceSphere, byte numberOfResources, bool isVariableCost, IPlayer player, ICostlyCard costlyCard)
            : base(game, resourceSphere, numberOfResources, isVariableCost, player, costlyCard)
        {
            var attachableCard = costlyCard as IAttachableCard;

            if (attachableCard == null)
                throw new ArgumentException("costlyCard does not implement IAttachable - it cannot be played as an attachment");

            this.attachableCard = attachableCard;
            this.playerCard = costlyCard as IPlayerCard;
            this.attachmentCard = costlyCard as IAttachmentCard;
            this.treasureCard = costlyCard as ITreasureCard;
        }

        private readonly IPlayerCard playerCard;
        private readonly IAttachableCard attachableCard;
        private readonly IAttachmentCard attachmentCard;
        private readonly ITreasureCard treasureCard;

        private List<IAttachmentHostInPlay> GetAttachmentHosts(IGame game)
        {
            var hosts = new List<IAttachmentHostInPlay>();

            foreach (var player in game.Players)
            {
                hosts.AddRange(player.CardsInPlay.OfType<IAttachmentHostInPlay>().Where(x => x.Card.IsValidAttachment(attachableCard) && attachableCard.CanBeAttachedTo(game, x.Card)));
            }

            hosts.AddRange(game.StagingArea.CardsInStagingArea.OfType<IAttachmentHostInPlay>().Where(x => x.Card.IsValidAttachment(attachableCard) && attachmentCard.CanBeAttachedTo(game, x.Card)));

            if (game.QuestArea.ActiveLocation != null && game.QuestArea.ActiveLocation.Card.IsValidAttachment(attachableCard) && attachableCard.CanBeAttachedTo(game, game.QuestArea.ActiveLocation.Card))
            {
                hosts.Add(game.QuestArea.ActiveLocation as IAttachmentHostInPlay);
            }

            return hosts;
        }

        protected override void AfterCostPaid(IGame game, IEffectHandle handle, IEnumerable<Tuple<ICharacterInPlay, byte>> charactersAndPayments)
        {
            var attachedTo = handle.Target as IAttachmentHostInPlay;
            if (attachedTo == null)
                throw new InvalidOperationException("target undefined for this attachment");

            player.Hand.RemoveCards(new List<IPlayerCard> { playerCard });

            if (attachmentCard != null)
            {
                var attachmentInPlay = new AttachmentInPlay(game, attachmentCard, attachedTo);
                player.AddCardInPlay(attachmentInPlay);
            }
            else if (treasureCard != null)
            {
                var treasureInPlay = new TreasureInPlay(game, treasureCard, attachedTo);
                player.AddCardInPlay(treasureInPlay);
            }
        }

        protected override void ResolveEffect(IGame game, IEffectHandle handle, string paymentText)
        {
            var attachedTo = handle.Target as IAttachmentHostInPlay;
            if (attachedTo == null)
                throw new InvalidOperationException("target undefined for this attachment");

            handle.Resolve(string.Format("{0} chose to pay {1} to attach '{2}' to '{3}'", player.Name, paymentText, attachmentCard.Title, attachedTo.Title));
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            var hosts = GetAttachmentHosts(game);
            var characters = GetCharactersWithResourceMatch();
            var description = attachmentCard != null ? "attachment card" : "treasure card";
            var sum = characters.Sum(x => x.Resources);

            var builder =
                new ChoiceBuilder(GetChoiceText(), game, player);

            if (characters.Count == 0)
            {
                builder.Question("You do not have any characters with a resource match to pay this cost")
                    .Answer("Ok, cancel this payment", false, (source, handle, item) => UnableToPayCost(source, handle, player));
            }
            else if (hosts.Count == 0)
            {
                builder.Question(string.Format("There are not valid targets to which you can attach '{0}'", attachableCard.Title))
                    .Answer(string.Format("Ok, cancel playing this {0} from my hand", description), false, (source, handle, item) => CancelPayingCost(source, handle, player));
            }
            else if (isVariableCost)
            {
                if (characters.Count == 1)
                {
                    var first = characters.First();
                    var amounts = new List<byte>();
                    for (byte i = 1; i <= first.Resources; i++)
                    {
                        amounts.Add(i);
                    }

                    builder.Question(string.Format("'{0}' has a resource match, and this {1} has a variable cost. How many resources do you want to spend from their resource pool?", first.Title, description))
                        .Answers(amounts, (item) => item == 1 ? "1 resource" : string.Format("{0} resources", item), (source, handle, number) => PayResourcesFromCharacter(source, handle, first, player, number))
                        .LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
                else
                {
                    //TODO: Choose attachment host

                    builder.Question(string.Format("This {0} has a variable cost. Do you want to pay this cost?", description))
                        .Answer("Yes, pay this cost", true);

                    foreach (var character in characters)
                    {
                        var amounts = new List<byte>();
                        for (byte i = 1; i <= character.Resources; i++)
                        {
                            amounts.Add(i);
                        }

                        builder.Question(string.Format("'{0}' has a resource match, and this {1} has a variable cost. How many resources do you want to spend from their resource pool?", character.Title, description))
                            .LastAnswers(amounts, (item) => item == 1 ? "1 resource" : string.Format("{0} resources", item), (source, handle, number) => PayResourcesFromCharacter(source, handle, character, player, number));

                    }

                    builder.LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
            }
            else if (numberOfResources == 0)
            {
                var character = characters.First();

                //TODO: Choose attachment host

                if (costlyCard != null)
                {
                    builder.Question("This card does not have any cost. Do you want to play it?")
                        .Answer("Yes, I want to play this card", true, (source, handle, item) => PayResourcesFromCharacter(game, handle, character, player, 0))
                        .LastAnswer("No, I do not want to play this card", false, (source, handle, item) => CancelPayingCost(game, handle, player));
                }
                else if (cardEffect != null)
                {
                    builder.Question("This card effect does not have any cost. Do you want to trigger it?")
                        .Answer("Yes, I want to trigger this card effect", true, (source, handle, item) => PayResourcesFromCharacter(game, handle, character, player, 0))
                        .LastAnswer("No, I do not want to trigger this card effect", false, (source, handle, item) => CancelPayingCost(game, handle, player));
                }
            }
            else if (sum < numberOfResources)
            {
                builder.Question("You do not have characters with enough resources available to pay this cost")
                    .LastAnswer("Ok, cancel this payment", false, (source, handle, item) => UnableToPayCost(source, handle, player));
            }
            else if (characters.Count == 1)
            {
                var first = characters.First();
                if (first.Resources < numberOfResources)
                {
                    builder.Question(string.Format("'{0}' has a resource match but does not have enough resources to pay this cost", first.Title))
                        .LastAnswer("Ok, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
                else
                {
                    //TODO: Choose attachment host

                    var paymentText = numberOfResources == 1 ? "1 resource" : string.Format("{0} resources", numberOfResources);

                    builder.Question(string.Format("'{0}' has a resource match, do you want to pay {1} from their resource pool?", first.Title, paymentText))
                        .Answer("Yes, make this payment", first, (source, handle, character) => PayResourcesFromCharacter(source, handle, character, player, numberOfResources))
                        .LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
            }
            else
            {
                if (numberOfResources == 1)
                {
                    //TODO: Choose attachment host

                    builder.Question(string.Format("Multiple characters have a resource match, and this {0} costs 1 resource. Which character do you want to use to pay this cost?", description))
                        .Answers(characters, (item) => item.Title, (source, handle, character) => PayResourcesFromCharacter(source, handle, character, player, numberOfResources))
                        .LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
                else
                {
                    //TODO: Choose attachment host

                    if (sum == numberOfResources)
                    {
                        var paymentText = GetPaymentText(characters);
                        var charactersAndPayments = new List<Tuple<ICharacterInPlay, byte>>();

                        foreach (var character in characters)
                        {
                            charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(character, character.Resources));
                        }

                        builder.Question("You have just enough resources on your character to pay this cost. Do you want to pay all of the resources from matching characters?")
                            .Answer(string.Format("Yes, pay {0}", paymentText), characters, (source, handle, item) => PayResourcesFromCharacters(source, handle, charactersAndPayments, player))
                            .LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                    }
                    else
                    {
                        var characterNames = GetCharacterNames(characters);

                        builder.Question("You have muliple characters with a resource match to pay this cost. Do you want to choose the resources to pay from matching characters?")
                            .Answer(string.Format("Yes, pay resources as follows:", characterNames), true);

                        AddPaymentAnswers(builder, characters, numberOfResources);

                        builder.LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                    }
                }
            }

            return new EffectHandle(this, builder.ToChoice());
        }
    }
}
