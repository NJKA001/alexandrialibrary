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
using LotR.States;

namespace LotR.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                WriteLine("Lord of the Rings: Living Card Game");
                WriteLine("Simulator Console v. 1.0.0.0");
                WriteLine();

                playerDeckLoader = new PlayerDeckLoader();
                questLoader = new QuestLoader();

                var path = "SampleDeck.txt";
                var deck = LoadDeck(path);
                var questArea = questLoader.Load(ScenarioCode.Passage_Through_Mirkwood);

                if (deck == null)
                    return;

                DisplayDeck(deck);
                DisplayQuests(questArea.QuestDeck);
                DisplayEncounters(questArea.EncounterDecks);

                var line = "start";
                while (!string.IsNullOrEmpty(line))
                {
                    WriteLine();
                    WriteLine("Drawing 6 Cards (press ENTER to stop)");
                    
                    deck.Shuffle();

                    foreach (var card in deck.GetFromTop(6))
                    {
                        WriteLine("  {0}", card.Title);
                    }

                    line = System.Console.ReadLine().Trim();
                }

                WriteLine();
                WriteLine("Press ENTER to close simulator");
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine("Error: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private static IPlayerDeckLoader playerDeckLoader;
        private static IQuestLoader questLoader;

        private static IPlayerDeck LoadDeck(string path)
        {
            WriteLine("Loading deck from file: {0}", path);

            try
            {
                var deck = playerDeckLoader.Load(path);

                WriteLine("  Deck loaded");

                return deck;
            }
            catch (Exception ex)
            {
                WriteLine("  Could not load deck from file: " + ex.Message);
                return null;
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
            foreach (var encounterDeck in encounterDecks)
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
