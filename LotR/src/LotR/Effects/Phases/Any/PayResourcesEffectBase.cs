using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects
{
    public abstract class PayResourcesEffectBase
        : FrameworkEffectBase
    {
        protected PayResourcesEffectBase(IGame game, Sphere resourceSphere, byte numberOfResources, bool isVariableCost, IPlayer player, ICostlyCard costlyCard)
            : this(game, resourceSphere, numberOfResources, isVariableCost, player, costlyCard, null)
        {
            if (costlyCard == null)
                throw new ArgumentNullException("costlyCard");
        }

        protected PayResourcesEffectBase(IGame game, Sphere resourceSphere, byte numberOfResources, bool isVariableCost, IPlayer player, ICardEffect cardEffect)
            : this(game, resourceSphere, numberOfResources, isVariableCost, player, null, cardEffect)
        {
            if (cardEffect == null)
                throw new ArgumentNullException("cardEffect");
        }

        private PayResourcesEffectBase(IGame game, Sphere resourceSphere, byte numberOfResources, bool isVariableCost, IPlayer player, ICostlyCard costlyCard, ICardEffect cardEffect)
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

        protected readonly Sphere resourceSphere;
        protected readonly byte numberOfResources;
        protected readonly bool isVariableCost;
        protected readonly IPlayer player;
        protected readonly ICostlyCard costlyCard;
        protected readonly ICardEffect cardEffect;

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

        #region Private Payment Option Methods

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

        #endregion

        #region Protected Abstract and Virtual Methods

        protected abstract void AfterCostPaid(IGame game, IEffectHandle handle, IEnumerable<Tuple<ICharacterInPlay, byte>> charactersAndPayments);

        protected virtual string GetChoiceText()
        {
            var numberText = isVariableCost ? "You can pay any number of" : string.Format("You must pay {0}", numberOfResources);
            var typeText = resourceSphere == Sphere.Neutral ? "resources from any sphere" : string.Format("{0} resources", resourceSphere.ToString());
            return string.Format("{0}, {1} {2}", player.Name, numberText, typeText);
        }

        protected virtual void ResolveEffect(IGame game, IEffectHandle handle, string paymentText)
        {
            if (costlyCard != null)
                handle.Resolve(string.Format("{0} chose to pay {1} to play '{2}' from their hand", player.Name, paymentText, costlyCard.Title));
            else
                handle.Resolve(string.Format("{0} chose to pay {1} to trigger '{2}'", player.Name, paymentText, cardEffect.ToString()));
        }

        #endregion

        #region Protected Helper Methods

        protected void AddPaymentAnswers(IChoiceBuilder builder, IEnumerable<ICharacterInPlay> characters, byte numberOfResources)
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

                            builder.Answer(string.Format("Pay {0}", GetPaymentText(charactersAndPayments)), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
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

                                builder.Answer(string.Format("Pay {0}", GetPaymentText(charactersAndPayments)), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
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

                                    builder.Answer(string.Format("Pay {0}", GetPaymentText(charactersAndPayments)), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
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

                                    builder.Answer(string.Format("Pay {0}", GetPaymentText(charactersAndPayments)), charactersAndPayments, (source, handle, item) => PayResourcesFromCharacters(source, handle, item, player));
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void PayResourcesFromCharacter(IGame game, IEffectHandle handle, ICharacterInPlay character, IPlayer player, byte numberOfResources)
        {
            var charactersAndPayments = new List<Tuple<ICharacterInPlay, byte>>();
            charactersAndPayments.Add(new Tuple<ICharacterInPlay, byte>(character, numberOfResources));
            PayResourcesFromCharacters(game, handle, charactersAndPayments, player);
        }

        protected void PayResourcesFromCharacters(IGame game, IEffectHandle handle, IEnumerable<Tuple<ICharacterInPlay, byte>> charactersAndPayments, IPlayer player)
        {
            foreach (var tuple in charactersAndPayments.Where(x => x.Item2 > 0))
            {
                tuple.Item1.Resources -= tuple.Item2;
            }

            AfterCostPaid(game, handle, charactersAndPayments);

            var paymentText = GetPaymentText(charactersAndPayments);

            ResolveEffect(game, handle, paymentText);
        }

        protected string GetCharacterNames(IEnumerable<ICharacterInPlay> characters)
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

        protected string GetPaymentText(IEnumerable<ICharacterInPlay> characters)
        {
            return GetPaymentText(characters.Select(x => new Tuple<ICharacterInPlay, byte>(x, x.Resources)).ToList());
        }

        protected string GetPaymentText(IEnumerable<Tuple<ICharacterInPlay, byte>> charactersAndPayments)
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

        protected List<ICharacterInPlay> GetCharactersWithResourceMatch()
        {
            return (costlyCard != null) ?
                player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Resources > 0 && x.CanPayFor(costlyCard)).ToList()
                : player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Resources > 0 && x.CanPayFor(cardEffect)).ToList();
        }

        protected void CancelPayingCost(IGame game, IEffectHandle handle, IPlayer player)
        {
            var title = (costlyCard != null) ? costlyCard.Title : cardEffect.ToString();
            handle.Cancel(string.Format("{0} chose not to pay the cost for '{1}'", player.Name, title));
        }

        protected void UnableToPayCost(IGame game, IEffectHandle handler, IPlayer player)
        {
            var title = (costlyCard != null) ? costlyCard.Title : cardEffect.ToString();
            handler.Cancel(string.Format("{0} was not able to pay the cost for '{1}'", player.Name, title));
        }

        #endregion

        public override IEffectHandle GetHandle(IGame game)
        {
            var characters = GetCharactersWithResourceMatch();
            var description = costlyCard != null ? "card" : "effect";
            var sum = characters.Sum(x => x.Resources);

            var builder =
                new ChoiceBuilder(GetChoiceText(), game, player);

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
                var character = characters.First();

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
