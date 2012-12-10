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

        //private IList<IList<byte>> GetPaymentOptionsForTwoCharacters(byte numberOfResources)
        //{
        //    if (numberOfResources < 2 || numberOfResources > 10)
        //        throw new ArgumentException("numberOfResources must be between 2 and 10");

        //    var outer = new List<IList<byte>>();

        //    var total = (byte)Math.Floor((double)(numberOfResources / 2));
        //    var highest = (byte)(numberOfResources - 1);

        //    for (byte i = 1; i <= total; i++)
        //    {
        //        outer.Add(new List<byte> { highest, i });
        //        highest--;
        //    }

        //    return outer;
        //}

        //private IList<IList<byte>> GetPaymentOptionsForThreeCharacters(byte numberOfResources)
        //{
        //    if (numberOfResources < 3 || numberOfResources > 10)
        //        throw new ArgumentException("numberOfResources must be between 3 and 10");

        //    var outer = new List<IList<byte>>();

        //    var total = (byte)Math.Floor((double)(numberOfResources / 3));
        //    var highest = (byte)(numberOfResources - 1);

        //    for (byte i = 1; i <= total; i++)
        //    {
        //        outer.Add(new List<byte> { highest, i });
        //        highest--;
        //    }

        //    return outer;
        //}

        private IList<IList<byte>> GetPaymentOptionsForTwoCharacters(byte numberOfResources)
        {
            switch (numberOfResources)
            {
                case 2:
                    return new List<IList<byte>> { new List<byte> { 1, 1} };
                case 3:
                    return new List<IList<byte>> { new List<byte> { 2, 1 } };
                case 4:
                    return new List<IList<byte>> { new List<byte> { 3, 1 }, new List<byte> { 2, 2 } };
                case 5:
                    return new List<IList<byte>> { new List<byte> { 4, 1 }, new List<byte> { 3, 2 } };
                case 6:
                    return new List<IList<byte>> { new List<byte> { 5, 1 }, new List<byte> { 4, 2 }, new List<byte> { 3, 3 } };
                case 7:
                    return new List<IList<byte>> { new List<byte> { 6, 1 }, new List<byte> { 5, 2 }, new List<byte> { 4, 3 } };
                case 8:
                    return new List<IList<byte>> { new List<byte> { 7, 1 }, new List<byte> { 6, 2 }, new List<byte> { 5, 3 }, new List<byte> { 4, 4 } };
                case 9:
                    return new List<IList<byte>> { new List<byte> { 8, 1 }, new List<byte> { 7, 2 }, new List<byte> { 6, 3 }, new List<byte> { 5, 4 } };
                case 10:
                    return new List<IList<byte>> { new List<byte> { 9, 1 }, new List<byte> { 8, 2 }, new List<byte> { 7, 3 }, new List<byte> { 6,4 }, new List<byte> { 5, 5 } };
                default:
                    throw new ArgumentException("numberOfResources must be between 2 and 10");
            }
        }

        private IList<IList<byte>> GetPaymentOptionsForThreeCharacters(byte numberOfResources)
        {
            switch (numberOfResources)
            {
                case 3:
                    return new List<IList<byte>> { new List<byte> { 1, 1, 1 } };
                case 4:
                    return new List<IList<byte>> { new List<byte> { 2, 1, 1 } };
                case 5:
                    return new List<IList<byte>> { new List<byte> { 3, 1, 1 }, new List<byte> { 2, 2, 1 } };
                case 6:
                    return new List<IList<byte>> { new List<byte> { 4, 1, 1 }, new List<byte> { 3, 2, 1 }, new List<byte> { 2, 2, 2 } };
                case 7:
                    return new List<IList<byte>> { new List<byte> { 5, 1, 1 }, new List<byte> { 4, 2, 1 }, new List<byte> { 3, 2, 2 }, new List<byte> { 3, 3, 1 } };
                case 8:
                    return new List<IList<byte>> { new List<byte> { 6, 1, 1 }, new List<byte> { 5, 2, 1 }, new List<byte> { 4, 2, 2 }, new List<byte> { 4, 3, 1 }, new List<byte> { 3, 3, 2 } };
                case 9:
                    return new List<IList<byte>> { new List<byte> { 7, 1, 1 }, new List<byte> { 6, 2, 1 }, new List<byte> { 5, 2, 2 }, new List<byte> { 5, 3, 1 }, new List<byte> { 4, 3, 2 }, new List<byte> { 4, 4, 1 }, new List<byte> { 3, 3, 3 } };
                case 10:
                    return new List<IList<byte>> { new List<byte> { 8, 1, 1 }, new List<byte> { 7, 2, 1 }, new List<byte> { 6, 2, 2 }, new List<byte> { 6, 3, 2 }, new List<byte> { 5, 3, 2 }, new List<byte> { 5, 4, 1 }, new List<byte> { 4, 3, 3 }, new List<byte> { 4, 4, 2} };
                default:
                    throw new ArgumentException("numberOfResources must be between 3 and 10");
            }
        }

        private IList<IList<byte>> GetPaymentOptionsForFourCharacters(byte numberOfResources)
        {
            switch (numberOfResources)
            {
                case 4:
                    return new List<IList<byte>> { new List<byte> { 1, 1, 1, 1 } };
                case 5:
                    return new List<IList<byte>> { new List<byte> { 2, 1, 1, 1 } };
                case 6:
                    return new List<IList<byte>> { new List<byte> { 3, 1, 1, 1 }, new List<byte> { 2, 2, 1, 1 } };
                case 7:
                    return new List<IList<byte>> { new List<byte> { 4, 1, 1, 1 }, new List<byte> { 3, 2, 1, 1 }, new List<byte> { 2, 2, 2, 1 } };
                case 8:
                    return new List<IList<byte>> { new List<byte> { 5, 1, 1, 1 }, new List<byte> { 4, 2, 1, 1 }, new List<byte> { 3, 2, 2, 1 }, new List<byte> { 3, 3, 1, 1 }, new List<byte> { 2, 2, 2, 2 } };
                case 9:
                    return new List<IList<byte>> { new List<byte> { 6, 1, 1, 1 }, new List<byte> { 5, 2, 1, 1 }, new List<byte> { 4, 2, 2, 1 }, new List<byte> { 4, 3, 1, 1 }, new List<byte> { 3, 2, 2, 2 }, new List<byte> { 3, 3, 2, 1 } };
                case 10:
                    return new List<IList<byte>> { new List<byte> { 7, 1, 1, 1 }, new List<byte> { 6, 2, 1, 1 }, new List<byte> { 5, 2, 2, 1 }, new List<byte> { 5, 3, 1, 1 }, new List<byte> { 4, 4, 1, 1, }, new List<byte> { 4, 3, 2, 1 }, new List<byte> { 4, 2, 2, 2 }, new List<byte> { 3, 3, 3, 1 }, new List<byte> { 3, 3, 2, 2 } };
                default:
                    throw new ArgumentException("numberOfResources must be between 4 and 10");
            }
        }

        private IList<IList<byte>> GetPaymentOptionsForFiveCharacters(byte numberOfResources)
        {
            switch (numberOfResources)
            {
                case 5:
                    return new List<IList<byte>> { new List<byte> { 1, 1, 1, 1, 1 } };
                case 6:
                    return new List<IList<byte>> { new List<byte> { 2, 1, 1, 1, 1 } };
                case 7:
                    return new List<IList<byte>> { new List<byte> { 3, 1, 1, 1, 1 }, new List<byte> { 2, 2, 1, 1, 1 } };
                case 8:
                    return new List<IList<byte>> { new List<byte> { 4, 1, 1, 1, 1 }, new List<byte> { 3, 2, 1, 1, 1 }, new List<byte> { 2, 2, 2, 1, 1 } };
                case 9:
                    return new List<IList<byte>> { new List<byte> { 5, 1, 1, 1, 1 }, new List<byte> { 4, 2, 1, 1, 1 }, new List<byte> { 3, 2, 2, 1, 1 }, new List<byte> { 2, 2, 2, 2, 1 } };
                case 10:
                    return new List<IList<byte>> { new List<byte> { 6, 1, 1, 1, 1 }, new List<byte> { 5, 2, 1, 1, 1 }, new List<byte> { 4, 3, 1, 1, 1, }, new List<byte> { 4, 2, 2, 1, 1 }, new List<byte> { 3, 3, 1, 1, 1 }, new List<byte> { 3, 2, 2, 2, 1 }, new List<byte> { 2, 2, 2, 2, 2 } };
                default:
                    throw new ArgumentException("numberOfResources must be between 5 and 10");
            }
        }

        private IList<IList<byte>> GetPaymentOptions(byte numberOfResources, byte numberOfCharacters)
        {
            var options = new List<IList<byte>>();

            switch (numberOfCharacters)
            {
                case 2:
                    options.AddRange(GetPaymentOptionsForTwoCharacters(numberOfResources));
                    break;
                case 3:
                    options.AddRange(GetPaymentOptionsForTwoCharacters(numberOfResources));
                    
                    if (numberOfResources > 2)
                        options.AddRange(GetPaymentOptionsForThreeCharacters(numberOfResources));

                    break;
                case 4:
                    options.AddRange(GetPaymentOptionsForTwoCharacters(numberOfResources));
                    
                    if (numberOfResources > 2)
                        options.AddRange(GetPaymentOptionsForThreeCharacters(numberOfResources));

                    if (numberOfResources > 3)
                        options.AddRange(GetPaymentOptionsForFourCharacters(numberOfResources));

                    break;
                case 5:
                    options.AddRange(GetPaymentOptionsForTwoCharacters(numberOfResources));
                    
                    if (numberOfResources > 2)
                        options.AddRange(GetPaymentOptionsForThreeCharacters(numberOfResources));

                    if (numberOfResources > 3)
                        options.AddRange(GetPaymentOptionsForFourCharacters(numberOfResources));

                    if (numberOfResources > 4)
                        options.AddRange(GetPaymentOptionsForFiveCharacters(numberOfResources));

                    break;
                default:
                    throw new ArgumentException("numberOfCharacters must be between 2 and 5");
            }

            return options;
        }

        private void AddPaymentAnswers(IChoiceBuilder builder, IEnumerable<ICharacterInPlay> characters, byte numberOfResources)
        {
            if (characters.Count() < 2)
                throw new ArgumentException("characters sequence must contain at least two items");
            if (numberOfResources < 2)
                throw new ArgumentException("numberOfResources cannot be less than 2");

            var numberOfCharacters = (byte)characters.Count();

            var paymentOptions = GetPaymentOptions(numberOfResources, numberOfCharacters);

            foreach (var character in characters)
            {
                if (character.Resources >= numberOfResources)
                {
                    builder.Answer(string.Format("Pay the full cost ({0} resources) from '{1}'", numberOfResources, character.Title), character, (source, handle, item) => PayResourcesFromCharacter(source, handle, item, player, numberOfResources));
                }
            }

            foreach (var optionList in paymentOptions.Where(x => x.Count >= 2))
            {
                foreach (var first in characters.Where(x => x.Resources >= optionList[0]))
                {
                    foreach (var second in characters.Where(x => x.Card.Id != first.Card.Id && x.Resources >= optionList[1]))
                    {
                        if (optionList.Count < 3)
                        {
                            var charactersAndPayments = new List<Tuple<ICharacterInPlay, byte>>();
                            charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(first, optionList[0]));
                            charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(second, optionList[1]));

                            builder.Answer(string.Format("Pay {0}", GetCharactersAndPaymentsString(charactersAndPayments)), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
                            break;
                        }

                        foreach (var third in characters.Where(x => x.Card.Id != first.Card.Id && x.Card.Id != second.Card.Id && x.Resources >= optionList[2]))
                        {
                            if (optionList.Count < 4)
                            {
                                var charactersAndPayments = new List<Tuple<ICharacterInPlay, byte>>();
                                charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(first, optionList[0]));
                                charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(second, optionList[1]));
                                charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(third, optionList[2]));

                                builder.Answer(string.Format("Pay {0}", GetCharactersAndPaymentsString(charactersAndPayments)), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
                                break;
                            }

                            foreach (var fourth in characters.Where(x => x.Card.Id != first.Card.Id && x.Card.Id != second.Card.Id && x.Card.Id != third.Card.Id && x.Resources >= optionList[3]))
                            {
                                if (optionList.Count < 5)
                                {
                                    var charactersAndPayments = new List<Tuple<ICharacterInPlay, byte>>();
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(first, optionList[0]));
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(second, optionList[1]));
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(third, optionList[2]));
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(fourth, optionList[3]));

                                    builder.Answer(string.Format("Pay {0}", GetCharactersAndPaymentsString(charactersAndPayments)), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
                                    break;
                                }

                                foreach (var fifth in characters.Where(x => x.Card.Id != first.Card.Id && x.Card.Id != second.Card.Id && x.Card.Id != third.Card.Id && x.Card.Id != fourth.Card.Id && x.Resources >= optionList[4]))
                                {
                                    var charactersAndPayments = new List<Tuple<ICharacterInPlay, byte>>();
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(first, optionList[0]));
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(second, optionList[1]));
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(third, optionList[2]));
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(fourth, optionList[3]));
                                    charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(fifth, optionList[4]));

                                    builder.Answer(string.Format("Pay {0}", GetCharactersAndPaymentsString(charactersAndPayments)), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
                                }
                            }
                        }
                    }
                }
            }
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

                        builder.Question("You have muliple characters with a resource match to pay this cost. Do you want to choose the resources to pay from matching characters?")
                            .Answer(string.Format("Yes, pay resources as follows:", characterNames), true);

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
