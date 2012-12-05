using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PayResourcesEffect
        : FrameworkEffectBase
    {
        public PayResourcesEffect(IGame game, Sphere resourceSphere, byte numberOfResources, bool isVariableCost, IPlayer player, ICostlyCard costlyCard)
            : this(game, resourceSphere, numberOfResources, isVariableCost, player, costlyCard, null)
        {
            if (costlyCard == null)
                throw new ArgumentNullException("costlyCard");
        }

        public PayResourcesEffect(IGame game, Sphere resourceSphere, byte numberOfResources, bool isVariableCost, IPlayer player, ICardEffect cardEffect)
            : this(game, resourceSphere, numberOfResources, isVariableCost, player, null, cardEffect)
        {
            if (cardEffect == null)
                throw new ArgumentNullException("cardEffect");
        }

        private PayResourcesEffect(IGame game, Sphere resourceSphere, byte numberOfResources, bool isVariableCost, IPlayer player, ICostlyCard costlyCard, ICardEffect cardEffect)
            : base("Pay Resources", GetText(player, resourceSphere, numberOfResources, isVariableCost), game)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            this.resourceSphere = resourceSphere;
            this.numberOfResources = numberOfResources;
            this.isVariableCost = isVariableCost;
            this.player = player;
            this.costlyCard = costlyCard;
            this.cardEffect = cardEffect;
        }

        private readonly Sphere resourceSphere;
        private readonly byte numberOfResources;
        private readonly bool isVariableCost;
        private readonly IPlayer player;
        private readonly ICostlyCard costlyCard;
        private readonly ICardEffect cardEffect;

        private static string GetText(IPlayer player, Sphere resourceSphere, byte numberOfResources, bool isVariableCost)
        {
            if (isVariableCost)
                return resourceSphere == Sphere.Neutral ? string.Format("{0} can pay any number of resources from any sphere", player.Name) : string.Format("{0} can pay any number of {1} resources", player.Name, resourceSphere);
            else if (numberOfResources == 0)
                return "No Resource Cost";
            else if (numberOfResources == 1)
                return resourceSphere == Sphere.Neutral ? string.Format("{0} must pay 1 resource from any sphere", player.Name) : string.Format("1 {0} resource", resourceSphere);
            else
                return resourceSphere == Sphere.Neutral ? "{0} resources from any sphere" : string.Format("{0} {1} resources", numberOfResources, resourceSphere);
        }

        private void CancelPayingCost(IGame game, IEffectHandle handle, IPlayer player)
        {
            handle.Cancel(string.Format("{0} chose not to pay this resource cost", player.Name));
        }

        private void UnableToPayCost(IGame game, IEffectHandle handler, IPlayer player)
        {
            handler.Cancel(string.Format("{0} was not able to pay this cost", player.Name));
        }

        private void PayResourcesFromCharacter(IGame game, IEffectHandle handle, ICharacterInPlay character, IPlayer player, byte numberOfResources)
        {
            character.Resources -= numberOfResources;

            if (numberOfResources == 1)
                handle.Resolve(string.Format("{0} chose to pay 1 resource from '{1}'", player.Name, character.Title));
            else
                handle.Resolve(string.Format("{0} chose to pay {1} resources from '{2}'", player.Name, numberOfResources, character.Title));
        }

        private void PayResourcesFromCharacters(IGame game, IEffectHandle handle, IEnumerable<Tuple<ICharacterInPlay, byte>> charactersAndPayments, IPlayer player)
        {
            foreach (var tuple in charactersAndPayments)
            {
                tuple.Item1.Resources -= tuple.Item2;
            }

            var paymentText = GetCharactersAndPaymentsString(charactersAndPayments);
            handle.Resolve(string.Format("{0} chose to pay {1}", player.Name, paymentText));
        }

        private string GetCharacterNames(IEnumerable<ICharacterInPlay> characters)
        {
            var characterNames = new StringBuilder();

            var total = characters.Count();
            var count = 0;

            foreach (var character in characters)
            {
                count++;

                if (count == 1)
                    characterNames.Append(character.Title);
                else if (count > 1 && count < total)
                    characterNames.AppendFormat(", {0}", character.Title);
                else
                    characterNames.AppendFormat(" and {0}", character.Title);
            }

            return characterNames.ToString();
        }

        private string GetCharactersAndPaymentsString(IEnumerable<ICharacterInPlay> characters)
        {
            return GetCharactersAndPaymentsString(characters.Select(x => new Tuple<ICharacterInPlay, byte>(x, x.Resources)).ToList());
        }

        private string GetCharactersAndPaymentsString(IEnumerable<Tuple<ICharacterInPlay, byte>> charactersAndPayments)
        {
            var sb = new StringBuilder();

            var available = charactersAndPayments.Where(x => x.Item1.Resources >= x.Item2).ToList();
            if (available.Count == 0)
                return "None of these character have available resources";

            var count = 0;
            foreach (var tuple in available)
            {
                count++;

                if (tuple.Item2 == 1)
                {
                    if (count == 1)
                        sb.AppendFormat("1 resource from '{0}'", tuple.Item1.Title);
                    else if (count < available.Count)
                        sb.AppendFormat(", 1 resource from '{0}'", tuple.Item1.Title);
                    else
                        sb.AppendFormat(" and 1 resource from '{0}'", tuple.Item1.Title);
                }
                else
                {
                    if (count == 1)
                        sb.AppendFormat("{0} resources from '{1}'", tuple.Item2, tuple.Item1.Title);
                    else if (count < available.Count)
                        sb.AppendFormat(", {0} resources from '{1}'", tuple.Item2, tuple.Item1.Title);
                    else
                        sb.AppendFormat(" and {0} resources from '{1}'", tuple.Item2, tuple.Item1.Title);
                }
            }

            return sb.ToString();
        }

        private void AddPaymentAnswers(IChoiceBuilder builder, IEnumerable<ICharacterInPlay> characters, byte numberOfResources)
        {
            //NOTE: characters.Count() > 1 and numberOfResources > 1
            var characterCount = characters.Count();

            //byte currentAmount = numberOfResources;
            //while (currentAmount > 0)
            //{
            //    var withCurrentAmount = characters.Where(x => x.Resources >= currentAmount).ToList();
            //    foreach (var character in withCurrentAmount)
            //    {
            //        if (currentAmount == numberOfResources)
            //        {
            //            builder.Answer(string.Format("Pay the full amount ({0} resources) from '{1}'", numberOfResources, character.Title), character, (source, handle, item) => PayResourcesFromCharacter(source, handle, item, player, numberOfResources));
            //        }
            //        else
            //        {
            //            byte difference = (byte)(numberOfResources - currentAmount);
            //            var secondCharacters = characters.Where(x => x.Card.Id != character.Card.Id).ToList();
            //            foreach (var secondCharacter in secondCharacters)
            //            {
            //                if (secondCharacter.Resources >= difference)
            //                {
            //                    var charactersAndPayments = new List<Tuple<ICharacterInPlay, byte>>() { new Tuple<ICharacterInPlay, byte>(character, currentAmount), new Tuple<ICharacterInPlay, byte>(secondCharacter, difference) };
            //                    var paymentText = GetCharactersAndPaymentsString(charactersAndPayments);
            //                    builder.Answer(string.Format("Pay {0}", paymentText), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
            //                }

            //                if (characterCount > 2 && difference > 1)
            //                {
            //                    byte nextDifference = (byte)(difference - 1);
            //                    var howManyMoreThanTwo = characterCount - 2;
            //                    var additionalCount = 0;
            //                    while (additionalCount <= howManyMoreThanTwo)
            //                    {
            //                        additionalCount++;

            //                        var threshold = (byte)(additionalCount * nextDifference);
            //                        var additionalCharacters = characters.Where(x => x.Card.Id != character.Card.Id && x.Resources >= threshold).ToList();
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    currentAmount--;
            //}
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            var description = costlyCard != null ? "card" : "effect";
            var numberText = isVariableCost ? "You can pay any number of" : string.Format("You must pay {0}", numberOfResources);
            var typeText = resourceSphere == Sphere.Neutral ? "resources from any sphere" : string.Format("{0} resources", resourceSphere.ToString());
            var text = string.Format("{0}, {1} {2}", player.Name, numberText, typeText);

            var characters = (costlyCard != null) ?
                player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Resources > 0 && x.CanPayFor(costlyCard)).ToList()
                : player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Resources > 0 && x.CanPayFor(cardEffect)).ToList();

            var sum = characters.Sum(x => x.Resources);

            var builder =
                new ChoiceBuilder(text, game, player);

            if (characters.Count == 0)
            {
                builder.Question("You do not have any characters with a resource match to pay this cost")
                    .Answer("Ok, cancel this payment", false, (source, handle, item) => UnableToPayCost(source, handle, player));
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
                if (costlyCard != null)
                {
                    builder.Question("This card does not have any cost. Do you want to play it?")
                        .LastAnswer("Yes, play this card", true, (source, handle, item) => handle.Resolve(string.Format("'{0}' has no cost to play", costlyCard.Title)));
                }
                else if (cardEffect != null)
                {
                    builder.Question("This card effect does not have any cost. Do you want to trigger it?")
                        .LastAnswer("Yes, trigger this card effect", true, (source, handle, item) => handle.Resolve(string.Format("'{0}' has no cost to trigger", cardEffect.ToString())));
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
                    builder.Question(string.Format("Multiple characters have a resource match, and this {0} costs 1 resource. Which character do you want to use to pay this cost?", description))
                        .Answers(characters, (item) => item.Title, (source, handle, character) => PayResourcesFromCharacter(source, handle, character, player, numberOfResources))
                        .LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
                else
                {
                    if (sum == numberOfResources)
                    {
                        var charactersAndPayments = GetCharactersAndPaymentsString(characters);

                        Action<IGame, IEffectHandle, List<ICharacterInPlay>> action = (source, handle, chars) =>
                            {
                                foreach (var character in chars)
                                {
                                    PayResourcesFromCharacter(source, handle, character, player, character.Resources);
                                }
                            };

                        builder.Question("You have just enough resources on your character to pay this cost. Do you want to pay all of the resources from matching characters?")
                            .Answer(string.Format("Yes, pay {0}", charactersAndPayments), characters, action)
                            .LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                    }
                    else
                    {
                        var characterNames = GetCharacterNames(characters);

                        builder.Question("You have muliple characters with a resource match to pay this cost. Do you want to select which characters to pay for this cost?")
                            .Answer(string.Format("Yes, pay resources from {0}", characterNames), true);

                        AddPaymentAnswers(builder, characters, numberOfResources);

                        builder.LastAnswer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                    }
                }
            }

            var choice = builder.ToChoice();
            return new EffectHandle(this, choice);
        }
    }
}
