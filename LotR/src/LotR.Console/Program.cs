﻿using System;
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

                gameLoader = new GameLoader();

                var player1 = new PlayerInfo("Dan", "TheThreeHunters.txt");
                var player2 = new PlayerInfo("Irma", "LeadershipTactics.txt");
                var playersInfo = new List<PlayerInfo> { player1, player2 };

                controller = GetController();
                game = new Game(controller);

                WriteLine("Loading Game");

                gameLoader.Load(game, playersInfo, ScenarioCode.Passage_Through_Mirkwood);

                WriteLine("\r\nGame Is Ready\r\n");

                var line = string.Empty;
                var cmd = string.Empty;
                string[] options = null;
                while (line != command_exit)
                {
                    Write("\r\nready>");

                    line = System.Console.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    
                    options = line.Split(' ');
                    if (options == null || options.Length == 0)
                        continue;
                    
                    cmd = options[0];

                    HandleCommand(game, cmd, options);
                }
            }
            catch (Exception ex)
            {
                WriteLine("Error: {0}\r\n{1}", ex.Message, ex.StackTrace);
                System.Console.ReadLine();
            }
        }

        private static IGame game;
        private static IGameController controller;
        private static IGameLoader gameLoader;

        private static IGameController GetController()
        {
            var controller = new GameController();
            controller.RegisterChoiceOfferedCallback((effect, choice) => ChoiceOfferedCallback(effect, choice));
            controller.RegisterEffectResolvedCallback((effect, payment, choice) => EffectResolvedCallback(effect, payment, choice));
            controller.RegisterGetPaymentCallback((effect, cost) => GetPaymentCallback(effect, cost));
            controller.RegisterPaymentRejectedCallback((effect, payment, choice) => PaymentRejectedCallback(effect, payment, choice));

            return controller;
        }

        private static bool TryParseInputAsInt(string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        private static void ChooseFirstPlayer(IChooseFirstPlayer choice)
        {
            var players = choice.Players.ToList();
            var number = 0;
            foreach (var player in players)
            {
                number++;
                WriteLine("{0,00}  {1}", number, player.Name);
            }

            number++;
            WriteLine("{0,00}  Choose First Player Randomly\r\n", number);

            var answer = 0;
            var input = System.Console.ReadLine();
            if (TryParseInputAsInt(input, out answer) && answer > 0 && answer <= number)
            {
                if (answer < number)
                {
                    choice.FirstPlayer = players[answer - 1];
                }
                else
                {
                    choice.ChooseRandomFirstPlayer();
                }
            }
            else
            {
                WriteLine("{0} is not a valid choice, please choose a number between 1 and {1}", input, number);
            }
        }

        private static void ChooseToKeepStartingHand(IChooseToKeepStartingHand choice)
        {
            var inputValid = false;

            while (!inputValid)
            {
                WriteLine("Starting Hand for Player: {0}\r\n", choice.Players.First().Name);
                foreach (var card in choice.StartingHand)
                {
                    WriteLine("  {0} ({1})", card.Title, card.PrintedCardType);
                }

                WriteLine("\r\nKeep Starting Hand? (y/n)\r\n");
                var input = System.Console.ReadLine() ?? string.Empty;
                
                var normalized = input.ToLower();
                if (normalized != "y" && normalized != "n")
                {
                    WriteLine("{0} is not a valid answer. Please enter 'y' for yes or 'n' for no.\r\n", input);
                    continue;
                }

                inputValid = true;
                choice.KeepStartingHand = (normalized == "y");
            }
        }

        private static void HandleChoice(IChoice choice)
        {
            WriteLine("\r\nChoice: {0}\r\n", choice.Description);

            if (choice is IChooseFirstPlayer)
            {
                ChooseFirstPlayer(choice as IChooseFirstPlayer);
            }
            else if (choice is IChooseToKeepStartingHand)
            {
                ChooseToKeepStartingHand(choice as IChooseToKeepStartingHand);
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
            WriteLine("Cost Incurred Callback - TODO");
            return null;
        }

        private static void PaymentRejectedCallback(IEffect effect, IPayment payment, IChoice choice)
        {
            WriteLine("Payment Rejected Callback - TODO");
        }

        private static void EffectResolvedCallback(IEffect effect, IPayment payment, IChoice choice)
        {
            try
            {
                if (effect != null)
                {
                    var description = effect.GetResolutionDescription(game, payment, choice) ?? "Undefined effect resolved";
                    WriteLine("\r\n{0}", description);
                }
                else
                    WriteLine("\r\nUnknown effect resolved");
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

        private static void HandleCommand(IGame game, string command, string[] options)
        {
            switch (command)
            {
                case command_exit:
                    break;
                case command_game:
                    DisplayGame(game, options);
                    break;
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
                    break;
                case command_player1:
                    DisplayPlayerInfo(game, 1, options);
                    break;
                case command_player2:
                    DisplayPlayerInfo(game, 2, options);
                    break;
                case command_player3:
                    DisplayPlayerInfo(game, 3, options);
                    break;
                case command_player4:
                    DisplayPlayerInfo(game, 4, options);
                    break;
                case command_quest:
                    WriteLine(game.QuestArea.ToString());
                    break;
                case command_staging:
                    WriteLine(game.StagingArea.ToString());
                    break;
                case command_victory:
                    WriteLine(game.VictoryDisplay.ToString());
                    break;
                default:
                    WriteLine("unrecognized command: {0}\r\nenter 'help' for a list of a valid commands", command);
                    break;
            }
        }

        private static void DisplayQuests(IDeck<IQuestCard> questDeck)
        {
            WriteLine();
            if (questDeck.Size > 0)
                WriteLine("Quest Deck: {0}", questDeck.Cards.First().Scenario.ToString().Replace('_', ' '));
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
