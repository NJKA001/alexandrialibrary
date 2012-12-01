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
            //if (!isVariableCost && numberOfResources == 0)
                //return base.GetHandle(game);

            var numberText = isVariableCost ? "You can pay any number of" : string.Format("You must pay {0}", numberOfResources);
            var typeText = resourceSphere == Sphere.Neutral ? "resources from any sphere" : string.Format("{0} resources", resourceSphere.ToString());
            var text = string.Format("{0}, {1} {2}", player.Name, numberText, typeText);

            var characters = (costlyCard != null) ?
                player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Resources > 0 && x.CanPayFor(costlyCard)).ToList()
                : player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Resources > 0 && x.CanPayFor(cardEffect)).ToList();

            var builder =
                new ChoiceBuilder(text, game, player);

            if (characters.Count == 0)
            {
                builder.Question("You do not have any characters that can pay this resource cost")
                    .Answer("Ok, cancel this payment", false);
            }
            else if (characters.Count == 1)
            {
                var first = characters.First();
                if (first.Resources < numberOfResources && !isVariableCost)
                {
                    builder.Question(string.Format("'{0}' has a resource match but does not have enough resources to pay this cost", first.Title))
                        .Answer("Ok, cancel this payment", false);
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

                    var description = costlyCard != null ? "card" : "effect";
                    builder.Question(string.Format("'{0}' has a resource match, and this {1} has a how many resources do you want to spend from their resource pool?", first.Title, description))
                        .Answers(amounts, (item) => item == 1 ? "1 resource" : string.Format("{0} resources", item), (source, handle, number) => PayResourcesFromCharacter(source, handle, first, player, number));

                }
            }
            else
            {
            }

            var choice = builder.ToChoice();
            return new EffectHandle(this, choice);
        }
    }
}
