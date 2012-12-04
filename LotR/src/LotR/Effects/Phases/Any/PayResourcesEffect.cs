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

        private void PayResourcesFromCharacter(IGame game, IEffectHandle handle, ICharacterInPlay character, IPlayer player, byte numberOfResources)
        {
            character.Resources -= numberOfResources;

            if (numberOfResources == 0)
                handle.Resolve(string.Format("{0} chose to have '{1}' not pay any resources", player.Name, character.Title));
            else if (numberOfResources == 1)
                handle.Resolve(string.Format("{0} chose to have '{1}' pay 1 resource from their resource pool", player.Name, character.Title));
            else
                handle.Resolve(string.Format("{0} chose to have '{1}' paid {2} resources from their resource pool", player.Name, character.Title, numberOfResources));
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
                    .Answer("Ok, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
            }
            else if (!isVariableCost && numberOfResources == 0)
            {
                if (costlyCard != null)
                {
                    builder.Question("This card does not have any cost. Do you want to play it?")
                        .Answer("Yes, play this card", true, (source, handle, item) => handle.Resolve(string.Format("'{0}' has no cost to play", costlyCard.Title)))
                        .Answer("No, cancel playing this card", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
                else if (cardEffect != null)
                {
                    builder.Question("This card effect does not have any cost. Do you want to trigger it?")
                        .Answer("Yes, trigger this card effect", true, (source, handle, item) => handle.Resolve(string.Format("'{0}' has no cost to trigger", cardEffect.ToString())))
                        .Answer("No, cancel triggering this card effect", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
            }
            else if (!isVariableCost && sum < numberOfResources)
            {
                builder.Question("You do not have characters with enough resources available to pay this cost")
                    .Answer("Ok, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
            }
            else if (characters.Count == 1)
            {
                var first = characters.First();
                if (first.Resources < numberOfResources && !isVariableCost)
                {
                    builder.Question(string.Format("'{0}' has a resource match but does not have enough resources to pay this cost", first.Title))
                        .Answer("Ok, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
                else if (!isVariableCost)
                {
                    var paymentText = numberOfResources == 1 ? "1 resource" : string.Format("{0} resources", numberOfResources);

                    builder.Question(string.Format("'{0}' has a resource match, do you want to pay {1} from their resource pool?", first.Title, paymentText))
                        .Answer("Yes, make this payment", first, (source, handle, character) => PayResourcesFromCharacter(source, handle, character, player, numberOfResources))
                        .Answer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
                else
                {
                    var amounts = new List<byte>();
                    for (byte i = 1; i <= first.Resources; i++)
                    {
                        amounts.Add(i);
                    }

                    builder.Question(string.Format("'{0}' has a resource match, and this {1} has a variable cost. How many resources do you want to spend from their resource pool?", first.Title, description))
                        .Answers(amounts, (item) => item == 1 ? "1 resource" : string.Format("{0} resources", item), (source, handle, number) => PayResourcesFromCharacter(source, handle, first, player, number))
                        .Answer("No, cancel this payment", false, (source, handle, item) => CancelPayingCost(source, handle, player));
                }
            }
            else
            {
                if (!isVariableCost && numberOfResources == 1)
                {
                }

                foreach (var character in characters)
                {
                    if (isVariableCost)
                        builder.Question(string.Format("'{0}' has a resource match, and this {1} has a variable cost. How many resources do you want to spend from their resource pool?", character.Title, description));
                    else if (numberOfResources == 1)
                        builder.Question(string.Format("'{0}' has a resource match, and this {1} costs 1 resource. How many resources do you want to spend from their resource pool?", character.Title, description));
                    else
                        builder.Question(string.Format("'{0}' has a resource match, and this {1} costs {2} resources. How many resources do you want to spend from their resource pool?", character.Title, description, numberOfResources));
                }
            }

            var choice = builder.ToChoice();
            return new EffectHandle(this, choice);
        }
    }
}
