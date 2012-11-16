using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter;
using LotR.Cards.Player;
using LotR.Cards.Player.Allies;
using LotR.Cards.Player.Attachments;
using LotR.Cards.Player.Events;
using LotR.Cards.Player.Heroes;
using LotR.Cards.Player.Treasures;
using LotR.Cards.Quests;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Controllers;
using LotR.Effects.Phases.Any;

namespace LotR.Console
{
    class Program
    {
        #region Constants

        const string command_exit = "exit";
        const string command_game = "game";
        const string command_help = "help";
        const string command_player1 = "player1";
        const string command_player2 = "player2";
        const string command_player3 = "player3";
        const string command_player4 = "player4";
        const string command_quest = "quest";
        const string command_staging = "staging";
        const string command_victory = "victory";
        const string option_hand = "hand";
        const string option_heros = "heroes";
        const string option_allies = "allies";
        const string option_attachments = "attachments";

        #endregion

        static void Main(string[] args)
        {
            try
            {
                WriteLine("Lord of the Rings: Living Card Game");
                WriteLine("Simulator Console v. 1.0.0.0");
                WriteLine();

                var player1 = new PlayerInfo("Dan", "TheThreeHunters.txt");
                var player2 = new PlayerInfo("Irma", "SpiritLeadership.txt");
                var playersInfo = new List<PlayerInfo> { player1, player2 };

                controller = GetController();
                game = new Game(controller);

                WriteLine("Setting Up Game");

                var players = GetPlayers(game, playersInfo);

                game.Setup(players, ScenarioCode.Passage_Through_Mirkwood);
            }
            catch (Exception ex)
            {
                WriteLine("Error: {0}\r\n{1}", ex.Message, ex.StackTrace);
                System.Console.ReadLine();
            }
        }

        private static IGame game;
        private static IGameController controller;

        private static IGameController GetController()
        {
            var controller = new GameController();
            controller.RegisterChoiceOfferedCallback((effect, choice) => ChoiceOfferedCallback(effect, choice));
            controller.RegisterEffectAddedCallback((effect) => EffectAddedCallback(effect));
            controller.RegisterEffectResolvedCallback((effect, payment, choice) => EffectResolvedCallback(effect, payment, choice));
            controller.RegisterGetPaymentCallback((effect, cost) => GetPaymentCallback(effect, cost));
            controller.RegisterPaymentRejectedCallback((effect, options) => PaymentRejectedCallback(effect, options));

            return controller;
        }

        private static bool CheckForCommand(string line)
        {
            var cmd = string.Empty;
            string[] options = null;

            options = line.Split(' ');
            if (options == null || options.Length == 0)
                return false;

            cmd = options[0];

            return HandleCommand(game, cmd, options);
        }

        private static bool HandleCommand(IGame game, string command, string[] options)
        {
            switch (command)
            {
                case command_exit:
                    return true;
                case command_game:
                    DisplayGame(game, options);
                    return true;
                case command_help:
                    WriteLine("exit     exit from simulator");
                    WriteLine("help     display a list of valid commands");
                    WriteLine("player1  display player #1 information");
                    WriteLine("player2  display player #2 information");
                    WriteLine("player3  display player #3 information");
                    WriteLine("player4  display player #4 information");
                    WriteLine("quest    display quest information");
                    WriteLine("staging  display staging area information");
                    WriteLine("victory  display victory display area information");
                    return true;
                case command_player1:
                    DisplayPlayerInfo(game, 1, options);
                    return true;
                case command_player2:
                    DisplayPlayerInfo(game, 2, options);
                    return true;
                case command_player3:
                    DisplayPlayerInfo(game, 3, options);
                    return true;
                case command_player4:
                    DisplayPlayerInfo(game, 4, options);
                    return true;
                case command_quest:
                    WriteLine(game.QuestArea.ToString());
                    return true;
                case command_staging:
                    WriteLine(game.StagingArea.ToString());
                    return true;
                case command_victory:
                    WriteLine(game.VictoryDisplay.ToString());
                    return true;
                default:
                    return false;
            }
        }

        private static IEnumerable<IPlayer> GetPlayers(IGame game, IEnumerable<PlayerInfo> playersInfo)
        {
            var players = new List<IPlayer>();

            var playerDeckLoader = new PlayerDeckLoader();

            foreach (var info in playersInfo)
            {
                var playerDeck = playerDeckLoader.Load(info.DeckPath);
                if (playerDeck != null)
                {
                    players.Add(new Player(game, info.Name, playerDeck));
                }
            }

            return players;
        }

        private static void DisplayList<T>(IEnumerable<T> items, Func<T, string> getDescription, uint minValue)
        {
            var sequence = minValue;
            foreach (var item in items)
            {
                WriteLine("{0,00}  {1}", sequence, getDescription(item));
                sequence++;
            }
        }

        private static uint PromptForNumber<T>(IEnumerable<T> items)
        {
            return PromptForNumber<T>(items, 1);
        }

        private static uint PromptForNumber<T>(IEnumerable<T> items, uint minValue)
        {
            return PromptForNumber<T>(items, (item) => item.ToString(), minValue);
        }

        private static uint PromptForNumber<T>(IEnumerable<T> items, Func<T, string> getDescription, uint minValue)
        {
            uint result = 0;

            DisplayList<T>(items, getDescription, minValue);

            var maxValue = items.Count();

            var line = string.Empty;

            var isCommand = true;
            while (isCommand)
            {
                line = ReadLine();
                isCommand = CheckForCommand(line);
            }

            while (true)
            {
                if (!uint.TryParse(line, out result) || result < minValue || result > maxValue)
                {
                    WriteLine("'{0}' is not a valid answer. Please enter a number between '{1}' and '{2}'", line, minValue, maxValue);
                    line = ReadLine();
                    continue;
                }

                break;
            }

            return result;
        }

        private static bool IsValidNumber(string value, int max)
        {
            uint result = 0;
            return (uint.TryParse(value, out result) && result > 0 && result <= max);
        }

        private static IEnumerable<uint> PromptForNumbers<T>(IEnumerable<T> items, Func<T, string> getDescription, uint minValue, uint minChoices, uint maxChoices)
        {
            if (minChoices < 1)
                throw new ArgumentException("minChoices must be greater than zero");
            if (maxChoices < 2)
                throw new ArgumentException("maxChoices must be greater than 1");
            if (maxChoices < minChoices)
                throw new ArgumentException("maxChoices must be greater than or equal to minChoices");

            IEnumerable<uint> results = Enumerable.Empty<uint>();

            DisplayList<T>(items, getDescription, minValue);

            var total = items.Count();

            var line = string.Empty;

            var isCommand = true;
            while (isCommand)
            {
                line = ReadLine();
                isCommand = CheckForCommand(line);
            }

            while (true)
            {
                var tokens = line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens == null || tokens.Length == 0 || tokens.Any(x => string.IsNullOrEmpty(x)) || tokens.Length < minChoices || tokens.Length > maxChoices || !tokens.All(x => IsValidNumber(x, total)))
                {
                    if (minChoices == 1)
                        WriteLine("'{0}' is not a valid answer. Please enter a comma-separated list of numbers between '1' and '{1}. The list should contain at least 1 number and at most {2}.", line, total, maxChoices);
                    else
                    {
                        if (minChoices == maxChoices)
                            WriteLine("'{0}' is not a valid answer. Please enter a comma-separated list of numbers between '1' and '{1}. The list should contain {2} numbers.", line, total, minChoices);
                        else 
                            WriteLine("'{0}' is not a valid answer. Please enter a comma-separated list of numbers between '1' and '{1}. The list should contain at least {2} numbers and at most {3}.", line, total, minChoices, maxChoices);
                    }

                    line = ReadLine();
                    continue;
                }

                results = tokens.Select(x => uint.Parse(x)).ToList();
                
                break;
            }

            return results;
        }

        private static bool PromptForBool(string format, params object[] args)
        {
            return PromptForBool(string.Format(format, args));
        }

        private static bool PromptForBool(string message)
        {
            var result = false;

            Write("\r\n{0} (y/n): ", message);

            var line = string.Empty;

            var isCommand = true;
            while (isCommand)
            {
                line = ReadLine();
                isCommand = CheckForCommand(line);
            }

            while (true)
            {
                if (line == "Y" || line == "y")
                    line = bool.TrueString;

                if (line == "N" || line == "n")
                    line = bool.FalseString;

                if (!bool.TryParse(line, out result))
                {
                    WriteLine("'{0}' is not a valid answer. Please enter 'y' or 'n'.", line);
                    line = ReadLine();
                    continue;
                }

                break;
            }

            return result;
        }

        private static void ChooseFirstPlayer(IChooseFirstPlayer choice)
        {
            var players = choice.Players.ToList();
            var playerNames = players.Select(x => x.Name).ToList();
            playerNames.Add("Choose First Player Randomly");

            var result = PromptForNumber(playerNames);
            if (result > players.Count())
            {
                choice.ChooseRandomFirstPlayer();
            }
            else
            {
                choice.FirstPlayer = players[(int)(result - 1)];
            }
        }

        private static void ChooseToKeepStartingHand(IChooseToKeepStartingHand choice)
        {
            WriteLine("Starting Hand for Player: {0}\r\n", choice.Players.First().Name);
            foreach (var card in choice.StartingHand)
            {
                WriteLine("  {0} ({1})", card.Title, card.PrintedCardType);
            }

            var keepStartingHand = PromptForBool("Keep Starting Hand?");

            choice.KeepStartingHand = keepStartingHand;
        }

        private static void ChoosePlayerAction(IChoosePlayerAction choice)
        {
            var player = choice.Players.FirstOrDefault();

            var items = new List<string>();
            var playableCardsInHand = GetPlayableCardsInHand(player);
            var playableEffects = GetPlayableEffects(player);

            uint playCardFromHand = 0;
            uint playEffect = 0;
            uint pass = 0;

            if (playableCardsInHand.Count > 0)
            {
                items.Add("Play a card from your hand");
                playCardFromHand = (uint)items.Count;
                
            }

            if (playableEffects.Count > 0)
            {
                items.Add("Trigger an effect on a card you control");
                playEffect = (uint)items.Count;
            }

            if (items.Count == 0)
            {
                choice.IsTakingAction = false;
                WriteLine("\r\n{0} does not have any actions that can be taken during this step", player.Name);
                return;
            }
            else
            {
                items.Add("Pass (if all players pass, this action window closes)");
                pass = (uint)items.Count;
            }

            var action = PromptForNumber(items);

            if (action == playCardFromHand)
            {
                ChooseCardToPlay(player, choice, playableCardsInHand);
                choice.IsTakingAction = true;
            }
            else if (action == playEffect)
            {
                ChooseCardEffectToTrigger(player, choice, playableEffects);
                choice.IsTakingAction = true;
            }
            else if (action == pass)
            {
                choice.IsTakingAction = false;
                WriteLine("\r\n{0} is passing on taking any actions right now", player.Name);
            }
        }

        //private static IPayment GetPayment(IPlayer player, ICardEffect cardEffect)
        //{
        //    if (player == null)
        //        throw new ArgumentNullException("player");
        //    if (cardEffect == null)
        //        throw new ArgumentNullException("cardEffect");

        //    var options = cardEffect.GetOptions(game);
        //    var cost = options.Cost;
        //    if (cost == null)
        //        return null;

        //    if (cost is IPayResources)
        //    {
        //        var resourceCost = cost as IPayResources;
        //        var payment = new ResourcePayment(cardEffect);

        //        foreach (var character in player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.CanPayFor(cardEffect)).ToList())
        //        {
        //            GetResourcePayment(resourceCost, cardEffect.Name, payment, character);
        //            if (!resourceCost.IsVariableCost && payment.GetTotalPayment() == resourceCost.NumberOfResources)
        //                break;
        //        }

        //        return payment;
        //    }

        //    return null;
        //}

        private static void GetResourcePayment(IPayResources cost, string costSource, IResourcePayment payment, ICharacterInPlay character)
        {
            if (cost == null)
                throw new ArgumentNullException("cost");
            if (costSource == null)
                throw new ArgumentNullException("costSource");
            if (payment == null)
                throw new ArgumentNullException("payment");
            if (character == null)
                throw new ArgumentNullException("character");

            if (character.Resources == 1)
            {
                var question = (cost.Sphere == Sphere.Neutral) ?
                    string.Format("{0} can pay for this card and only has 1 resource. Do you want to pay 1 resource from this character?", character.Title)
                    : string.Format("{0} can pay for this card and only has 1 {1} resource. Do you want to pay 1 {1} resource from this character?", character.Title, cost.Sphere);
                
                    if (PromptForBool(question))
                    {
                        payment.AddPayment(character, 1);
                    }
            }
            else
            {
                var maxNumberOfResources = character.Resources;
                var currentPayment = payment.GetTotalPayment();
                

                if (!cost.IsVariableCost)
                {
                    var remainder = (byte)(cost.NumberOfResources - payment.GetTotalPayment());
                    if (character.Resources > remainder)
                    {
                        maxNumberOfResources = remainder;
                    }

                    if (cost.Sphere == Sphere.Neutral)
                        WriteLine("{0} has a cost of {1} of any type of resource", costSource, cost.NumberOfResources);
                    else
                        WriteLine("{0} has a cost of {1} {2} resources", costSource, cost.NumberOfResources, cost.Sphere);

                    if (currentPayment == 0)
                        WriteLine("You have not paid any resources yet.", maxNumberOfResources);
                    else
                        WriteLine("You have paid {0} {1} resources so far with {2} left to pay", currentPayment, cost.Sphere, remainder);

                    if (cost.Sphere == Sphere.Neutral)
                        WriteLine("{0} has {1} resources available. How many resources do you want to pay?", character.Title, character.Resources);
                    else
                        WriteLine("{0} has {1} {2} resources available. How many resources do you want to pay?", character.Title, character.Resources, cost.Sphere);
                }
                else
                {
                    if (cost.Sphere == Sphere.Neutral)
                        WriteLine("{0} has a variable cost of any type of resource", costSource);
                    else
                        WriteLine("{0} has a variable cost of {1} resources", costSource, cost.Sphere);

                    if (currentPayment == 0)
                        WriteLine("You have not paid any resources yet");
                    else
                        WriteLine("You have paid {0} resource so far", currentPayment);

                    if (cost.Sphere == Sphere.Neutral)
                        WriteLine("{0} has {1} resources available. How many resources do you want to pay?", character.Title, character.Resources);
                    else
                        WriteLine("{0} has {1} {2} resources available. How many resources do you want to pay?", character.Title, character.Resources, cost.Sphere);
                }
                
                var paymentOptions = new List<string>();
                for (var i = 1; i <= maxNumberOfResources; i++)
                {
                    if (cost.Sphere == Sphere.Neutral)
                    {
                        if (i == 1)
                            paymentOptions.Add("Pay 1 resource");
                        else
                            paymentOptions.Add(string.Format("Pay {0} resources", i));
                    }
                    else
                    {
                        if (i == 1)
                            paymentOptions.Add(string.Format("Pay 1 {0} resource", cost.Sphere));
                        else
                            paymentOptions.Add(string.Format("Pay {0} {1} resources", i, cost.Sphere));
                    }
                }

                var numberOfResources = (byte)PromptForNumber(paymentOptions, 1);
                payment.AddPayment(character, numberOfResources);
            }
        }

        private static IPayment GetPayment(IPlayer player, ICostlyCard costlyCard)
        {
            var cost = costlyCard.GetResourceCost(game) as IPayResources;
            if (cost == null)
                return null;

            IResourcePayment payment = new ResourcePayment(costlyCard);

            var characters = player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.CanPayFor(costlyCard) && x.Resources >= cost.NumberOfResources).ToList();
            if (characters.Count == 0)
            {
                WriteLine("You do not have any characters with matching resources to pay for {0}", costlyCard.Title);
                return null;
            }

            var resourcesAvailable = characters.Sum(x => x.Resources);

            if (cost.IsVariableCost)
            {
                if (resourcesAvailable < 1)
                {
                    if (PromptForBool(string.Format("Your characters do not have any resources available to pay for {0} which has a variable cost. Do you want to pay zero resources for it?", costlyCard.Title)))
                    {
                        payment.AddPayment(characters.First(), 0);
                        return payment;
                    }
                    return null;
                }
            }
            else
            {
                if (cost.NumberOfResources == 0)
                {
                    payment.AddPayment(characters.First(), 0);
                    return payment;
                }
                else if (resourcesAvailable < cost.NumberOfResources)
                {
                    WriteLine("Your characters do not have enough available resources to pay for {0}", costlyCard.Title);
                    return null;
                }
            }

            foreach (var character in characters)
            {
                GetResourcePayment(cost, costlyCard.Title, payment, character);
                if (!cost.IsVariableCost && payment.GetTotalPayment() == cost.NumberOfResources)
                    break;
            }

            return payment;
        }

        private static IEffect ChooseEffectOnCardToPlay(IPlayer player, IChoosePlayerAction choice, IPlayerCard card)
        {
            return null;
        }

        private static IList<IPlayerCard> GetPlayableCardsInHand(IPlayer player)
        {
            var cards = new List<IPlayerCard>();

            if (game.CurrentPhase.StepCode == PhaseStep.Planning_Play_Allies_and_Attachments)
            {
                foreach (var card in player.Hand.Cards)
                    cards.Add(card);
            }
            else
            {
                foreach (var card in player.Hand.Cards.Where(x => x.HasEffect<IPlayerActionEffect>()))
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

        private static IList<ICardEffect> GetPlayableEffects(IPlayer player)
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

        private static void ChooseCardToPlay(IPlayer player, IChoosePlayerAction choice, IList<IPlayerCard> cards)
        {
            if (cards.Count == 0)
                return;

            WriteLine("Choose a card to play from your hand");

            var cardNames = cards.Select(x => string.Format("{0} ({1})", x.Title, x.PrintedCardType)).ToList();
            cardNames.Add("Pass on playing a card from your hand");

            var cardNumber = PromptForNumber(cardNames);

            if (cardNumber == cardNames.Count)
            {
                WriteLine("Passing on playing an action");
            }
            else
            {
                var costlyCard = cards[(int)cardNumber - 1] as ICostlyCard;
                if (costlyCard == null)
                    return;

                choice.CardToPlay = costlyCard;
            }
        }

        private static void ChooseCardEffectToTrigger(IPlayer player, IChoosePlayerAction choice, IList<ICardEffect> effects)
        {
            if (effects.Count == 0)
                return;

            WriteLine("Choose an effect to trigger");

            var effectNames = effects.Select(x => string.Format("{0} ({1} - {2})", x, x.CardSource.Title, x.CardSource.PrintedCardType)).ToList();
            effectNames.Add("Pass on triggering an effect");

            var effectNumber = PromptForNumber(effectNames);

            if (effectNumber == effectNames.Count)
            {
            }
            else
            {
                var cardEffect = effects[(int)effectNumber - 1] as ICardEffect;
                if (cardEffect == null)
                    return;

                choice.CardEffectToTrigger = cardEffect;
            }
        }

        private static string GetAttachmentHostName(IAttachmentHostInPlay host)
        {
            if (host.Card is IPlayerCard)
            {
                var playerCard = host.Card as IPlayerCard;
                return string.Format("{0} ({1}) [controlled by {2}]", playerCard.Title, playerCard.PrintedCardType, playerCard.Owner.Name);
            }
            else if (host.Card is IEncounterCard)
            {
                var encounterCard = host.Card as IEncounterCard;
                var desc = "not in staging area";
                if (game.StagingArea.CardsInStagingArea.Any(x => x.Card.Id == host.Card.Id))
                    desc = "in staging area";
                else if (game.QuestArea.ActiveLocation != null && game.QuestArea.ActiveLocation.Card.Id == host.Card.Id)
                    desc = "active location";

                return string.Format("{0} ({1}) [{2}]", encounterCard.Title, encounterCard.PrintedCardType, desc);
            }
            else
                return string.Format("{0} ({1}) [not in play]", host.Card.Title, host.Card.PrintedCardType);
        }

        private static void ChooseAttachmentHost(IChooseAttachmentHost choice)
        {
            var hosts = game.GetAllCardsInPlay<IAttachmentHostInPlay>().Where(x => x.Card.IsValidAttachment(choice.Attachment) && choice.Attachment.CanBeAttachedTo(game, x.Card)).ToList();
            if (hosts.Count == 0)
            {
                WriteLine("There are no valid targets to which {0} can be attached", choice.Attachment.Title);
                return;
            }

            var hostNames = hosts.Select(x => GetAttachmentHostName(x)).ToList();

            //if (choice.IsOptional)
            //{
            //    hostNames.Add("Cancel playing this attachment");
            //}

            var number = PromptForNumber(hostNames); //hosts, x => GetAttachmentHostName(x), 1);

            if (choice.IsOptional && number == hostNames.Count)
            {
                WriteLine("Cancelled playing of attachment {0}", choice.Attachment.Title);
                return;
            }

            choice.ChosenAttachmentHost = hosts[(int)number - 1];
            WriteLine("{0} will be attached to {1}", choice.Attachment.Title, choice.ChosenAttachmentHost.Title);
        }

        private static void ChooseCardInHand(IChooseCardInHand choice)
        {
            if (choice.PlayerCardsToChooseFrom.Count() == 0)
            {
                WriteLine("There are no valid cards in your hand to choose from");
                return;
            }

            if (choice.PlayerCardsToChooseFrom.Count() == 1)
            {
                choice.ChosenPlayerCard = choice.PlayerCardsToChooseFrom.First();
                WriteLine("Chose {0} from your hand", choice.ChosenPlayerCard.Title);
                return;
            }

            var cards = choice.PlayerCardsToChooseFrom.ToList();
            var cardNames = choice.PlayerCardsToChooseFrom.Select(x => string.Format("{0} ({1})", x.Title, x.PrintedCardType)).ToList();

            if (choice.IsOptional)
            {
                cardNames.Add("Cancel choosing a card in your hand");
            }

            var number = PromptForNumber(cardNames);

            if (choice.IsOptional && number == cardNames.Count)
            {
                WriteLine("Cancelled choosing a card in your hand");
                return;
            }

            choice.ChosenPlayerCard = cards[(int)number - 1];
            WriteLine("Chose {0} from your hand", choice.ChosenPlayerCard.Title);
        }

        private static void ChooseGandalfEffect(IChooseGandalfEffect choice)
        {
            var items = new List<string> { "draw three cards", "reduce your threat by 5" };
            if (choice.EnemiesToChoose.Count() > 0)
                items.Add("deal 4 damage to 1 enemy in play");

            var answer = PromptForNumber(items);

            switch (answer)
            {
                case 1:
                    choice.DrawCards = true;
                    return;
                case 2:
                    choice.ReduceYourThreat = true;
                    return;
                case 3:
                default:
                    {
                        if (choice.EnemiesToChoose.Count() == 1)
                        {
                            WriteLine("Dealing 4 damage to the only enemy that is a valid target");
                            choice.EnemyToDamage = choice.EnemiesToChoose.First();
                        }
                        else
                        {
                            var enemies = choice.EnemiesToChoose.ToList();
                            var enemyNames = enemies.Select(x => string.Format("{0} ({1} threat, {2} attack, {3} defense, {4}/{5} hit points)", x.Title, x.Card.PrintedThreat, x.Card.PrintedAttack, x.Card.PrintedDefense, x.Card.PrintedHitPoints - x.Damage, x.Card.PrintedHitPoints)).ToList();
                            var enemyNumber = PromptForNumber(enemyNames);
                            choice.EnemyToDamage = enemies[(int)enemyNumber - 1];
                        }
                    }
                    return;
            }
            
        }

        private static void ChooseGaladhrimsGreetingEffect(IChooseGaladhrimsGreetingEffect choice)
        {
            var answer = PromptForNumber(new List<string> { "Reduce 1 player's threat by 6", "Reduce each player's threat by 2" });
            if (answer == 1)
            {
                var players = game.Players.ToList();
                var playerNames = players.Select(x => string.Format("{0} ({1} threat)", x.Name, x.CurrentThreat)).ToList();
                var playerNumber = PromptForNumber(playerNames);
                choice.ReduceOnePlayersThreatBySix = players[(int)playerNumber - 1];
            }
            else if (answer == 2)
            {
                choice.ReduceEachPlayersThreatByTwo = true;
            }
        }

        private static void HandleChoice(IChoice choice)
        {
            WriteLine("\r\nChoice: {0}\r\n", choice.Text);

            if (choice is IChooseFirstPlayer)
            {
                ChooseFirstPlayer(choice as IChooseFirstPlayer);
            }
            else if (choice is IChooseToKeepStartingHand)
            {
                ChooseToKeepStartingHand(choice as IChooseToKeepStartingHand);
            }
            else if (choice is IChoosePlayerAction)
            {
                ChoosePlayerAction(choice as IChoosePlayerAction);
            }
            else if (choice is IChooseAttachmentHost)
            {
                ChooseAttachmentHost(choice as IChooseAttachmentHost);
            }
            else if (choice is IChooseCardInHand)
            {
                ChooseCardInHand(choice as IChooseCardInHand);
            }
            else if (choice is IChooseGandalfEffect)
            {
                ChooseGandalfEffect(choice as IChooseGandalfEffect);
            }
            else if (choice is IChooseGaladhrimsGreetingEffect)
            {
                ChooseGaladhrimsGreetingEffect(choice as IChooseGaladhrimsGreetingEffect);
            }
        }

        private static void ChoiceOfferedCallback(IEffect effect, IChoice choice)
        {
            try
            {
                if (choice == null)
                    throw new ArgumentNullException("choice");

                var isValid = false;

                while (!isValid)
                {
                    HandleChoice(choice);
                    isValid = choice.IsValid(game);
                }
            }
            catch (Exception ex)
            {
                WriteLine("Error in choice offered callback: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private static IPayment GetPaymentCallback(IEffect effect, ICost cost)
        {
            WriteLine("Payment Required: {0}", cost.Description);

            IPayment payment = null;
            var desc = string.Empty;

            while (true)
            {
                if (effect is IPlayCardFromHandEffect)
                {
                    var playCard = effect as IPlayCardFromHandEffect;
                    if (playCard == null)
                        return null;

                    desc = playCard.CostlyCard.Title;
                    payment = GetPayment(game.ActivePlayer, playCard.CostlyCard);
                }
                else if (effect is ICardEffect)
                {
                    desc = effect.ToString();
                }
                else
                    break;

                if (payment != null)
                {
                    if (!cost.IsMetBy(payment))
                    {
                        if (PromptForBool(string.Format("This payment is not valid for {0}. Do you want to try paying for it again?", desc)))
                        {
                            continue;
                        }

                        break;
                    }

                    break;
                }
                else
                {
                    break;
                }
            }

            return payment;
        }

        private static void PaymentRejectedCallback(IEffect effect, IEffectOptions options)
        {
            WriteLine("Payment for this effect was not accepted");
        }

        private static void EffectAddedCallback(IEffect effect)
        {
            try
            {
                if (effect is IResponseEffect && effect.CanBeTriggered(game))
                {
                    if (PromptForBool("You have a response that can be triggered right now.\r\n\r\n{0}\r\n\r\nWould you like to trigger this repsonse?", effect))
                    {
                        var options = game.GetOptions(effect);
                        game.ResolveEffect(effect, options);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine("\r\nError in effect added callback: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private static void EffectResolvedCallback(IEffect effect, IEffectOptions options, string status)
        {
            try
            {
                if (effect == null)
                    throw new InvalidOperationException("effect is undefined");

                if (string.IsNullOrEmpty(status))
                    return;
                    
                WriteLine("\r\n{0}", status);
            }
            catch (Exception ex)
            {
                WriteLine("\r\nError in effect resolved callback: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private static int GetPlayerCount(IGame game)
        {
            return game.Players.Count();
        }

        private static void DisplayPlayerInfo(IGame game, int number, string[] options)
        {
            if (GetPlayerCount(game) < number)
            {
                WriteLine("There are not {0} players in the game", number);
                return;
            }

            var player = game.Players.ToList()[number - 1];

            if (options == null)
                return;
            
            if (options.Length == 1)
            {
                WriteLine("Player #{0}", number);
                WriteLine(player.ToString());
            }
            else if (options.Length == 2 && options[1] != null)
            {
                var option1 = options[1].Trim();
                var option2 = options.Length > 2 ? options[2].Trim() : string.Empty;
                
                switch (option1)
                {
                    case option_hand:
                        if (player.Hand.Cards.Count() > 0)
                        {
                            WriteLine("Cards in Hand: {0}", player.Hand.Cards.Count());
                            var seq = 0;
                            foreach (var card in player.Hand.Cards)
                            {
                                seq++;
                                WriteLine("{0,00}  {1} ({2})", seq, card.Title, card.PrintedCardType);
                            }
                        }
                        else
                        {
                            WriteLine("No Cards in Hand");
                        }
                        break;
                    default:
                        WriteLine("unrecognized option: {0}", option1);
                        break;
                }
            }
        }

        private static void DisplayGame(IGame game, string[] options)
        {
            WriteLine(game.ToString());
        }

        private static void DisplayQuests(IDeck<IQuestCard> questDeck)
        {
            WriteLine();
            if (questDeck.Size > 0)
                WriteLine("Quest Deck: {0}", questDeck.Cards.First().ScenarioCode.ToString().Replace('_', ' '));
            else
                WriteLine("Quest Deck: Unknown Scenario");

            foreach (var quest in questDeck.Cards.OrderBy(x => x.Sequence))
            {
                WriteLine("{0,3} {1}", quest.Sequence, quest.Title);
            }
            WriteLine();
        }

        private static void DisplayEncounters(IEnumerable<IDeck<IEncounterCard>> encounterDecks)
        {
            WriteLine();
            var number = 0;
            foreach (var encounterDeck in encounterDecks)
            {
                number++;
                WriteLine("Encounter Deck #{0}: {1} Cards", number, encounterDeck.Size);

                var displayBreakdown = false;

                if (displayBreakdown)
                {
                    var encounterSet = EncounterSet.None;
                    foreach (var encounterCard in encounterDeck.Cards.OrderBy(x => x.EncounterSet).ThenBy(x => x.Title))
                    {
                        if (encounterCard.EncounterSet != encounterSet)
                            WriteLine("EncounterSet: {0}", encounterCard.EncounterSet.ToString().Replace('_', ' '));

                        encounterSet = encounterCard.EncounterSet;

                        WriteLine("{0,3} {1}", encounterCard.Quantity, encounterCard.Title);
                    }
                }
                else
                {
                    var count = 0;
                    foreach (var encounterCard in encounterDeck.Cards)
                    {
                        count++;
                        WriteLine("{0,3} {1}", count, encounterCard.Title);
                    }
                }
            }
            WriteLine();
        }

        private static void DisplayDeck(IPlayerDeck deck)
        {
            WriteLine();
            WriteLine("Player Deck: {0}", deck.Name);
            if (deck.Heroes.Count() > 0)
            {
                WriteLine("Heroes: {0}", string.Join(", ", deck.Heroes.Select(x => x.Title)));
                WriteLine("Starting Threat: {0}", deck.Heroes.Select(x => (int)x.ThreatCost).Sum());
            }
            else
            {
                WriteLine("Heroes: None");
                WriteLine("Starting Threat: 0");
            }
            WriteLine();
            WriteLine("Cards: {0} with Avg. Cost {1:0.0}", deck.Cards.Count(), deck.Cards.OfType<ICostlyCard>().Average(x => x.PrintedCost));
            foreach (var sphere in deck.Cards.OfType<ICostlyCard>().GroupBy(x => x.PrintedSphere))
            {
                var count = deck.Cards.OfType<ICostlyCard>().Where(x => x.PrintedSphere == sphere.Key).Count();
                var avg = deck.Cards.OfType<ICostlyCard>().Where(x => x.PrintedSphere == sphere.Key).Average(x => x.PrintedCost);
                WriteLine("{0} Sphere: {1} Cards with Avg. Cost {2:0.0}", sphere.Key, count, avg);
            }

            if (deck.Cards.OfType<IAllyCard>().Count() > 0)
            {
                WriteLine();
                WriteLine("{0,3} Allies", deck.Cards.OfType<IAllyCard>().Count());
                foreach (var card in deck.Cards.OfType<IAllyCard>().GroupBy(x => x.Title))
                {
                    WriteLine("{0,3} {1}", deck.Cards.OfType<IAllyCard>().Where(x => x.Title == card.Key).Count(), card.Key);
                }
            }

            if (deck.Cards.OfType<IAttachmentCard>().Count() > 0)
            {
                WriteLine();
                WriteLine("{0,3} Attachments", deck.Cards.OfType<IAttachmentCard>().Count());
                foreach (var card in deck.Cards.OfType<IAttachmentCard>().GroupBy(x => x.Title))
                {
                    WriteLine("{0,3} {1}", deck.Cards.OfType<IAttachmentCard>().Where(x => x.Title == card.Key).Count(), card.Key);
                }
            }

            if (deck.Cards.OfType<IEventCard>().Count() > 0)
            {
                WriteLine();
                WriteLine("{0,3} Events", deck.Cards.OfType<IEventCard>().Count());
                foreach (var card in deck.Cards.OfType<IEventCard>().GroupBy(x => x.Title))
                {
                    WriteLine("{0,3} {1}", deck.Cards.OfType<IEventCard>().Where(x => x.Title == card.Key).Count(), card.Key);
                }
            }

            if (deck.Cards.OfType<ITreasureCard>().Count() > 0)
            {
                WriteLine();
                WriteLine("{0,3} Treasures", deck.Cards.OfType<ITreasureCard>().Count());
                foreach (var card in deck.Cards.OfType<ITreasureCard>().GroupBy(x => x.Title))
                {
                    WriteLine("{0,3} {1}", deck.Cards.OfType<ITreasureCard>().Where(x => x.Title == card.Key).Count(), card.Key);
                }
            }
            WriteLine();
        }

        private static string GetPlayerCardTypeName(IPlayerCard card)
        {
            if (card is IHeroCard)
                return "Hero";
            else if (card is IAllyCard)
                return "Ally";
            else if (card is IAttachableCard)
                return "Attachment";
            else if (card is IEventCard)
                return "Event";
            else if (card is ITreasureCard)
                return "Treasure";
            else
                return "Unknown";
        }

        private static string ReadLine()
        {
            return System.Console.ReadLine() ?? string.Empty;
        }

        private static void Write(string line)
        {
            System.Console.Write(line);
        }

        private static void Write(string format, params object[] args)
        {
            System.Console.Write(format, args);
        }

        private static void WriteLine()
        {
            System.Console.WriteLine();
        }

        private static void WriteLine(string line)
        {
            System.Console.WriteLine(line);
        }

        private static void WriteLine(string format, params object[] args)
        {
            System.Console.WriteLine(format, args);
        }
    }
}
