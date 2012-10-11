using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Allies;
using LotR.Cards.Player.Attachments;
using LotR.Cards.Player.Events;
using LotR.Cards.Player.Heroes;
using LotR.Cards.Player.Treasures;
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

                reader = new PlayerDeckReader();

                var path = "SampleDeck.txt";
                var deck = ReadDeck(path);

                DisplayDeck(deck);

                var line = "start";
                while (!string.IsNullOrEmpty(line))
                {
                    WriteLine();
                    WriteLine("Drawing 6 Cards (press ENTER to stop)");
                    
                    deck.Shuffle();
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

        private static IPlayerDeckReader reader;

        private static IPlayerDeck ReadDeck(string path)
        {
            WriteLine("Reading Deck File: {0}", path);

            var deck = reader.Read(path);

            WriteLine("  File Loaded");

            return deck;
        }

        private static void DisplayDeck(IPlayerDeck deck)
        {
            WriteLine();
            WriteLine("Deck: {0}", deck.Name);
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
            WriteLine("Cards: {0} with Avg. Cost {1:0.0}", deck.Cards.Count(), deck.Cards.OfType<ICostlyCard>().Average(x => x.BaseResourceCost));
            foreach (var sphere in deck.Cards.OfType<ICostlyCard>().GroupBy(x => x.BaseResourceSphere))
            {
                var count = deck.Cards.OfType<ICostlyCard>().Where(x => x.BaseResourceSphere == sphere.Key).Count();
                var avg = deck.Cards.OfType<ICostlyCard>().Where(x => x.BaseResourceSphere == sphere.Key).Average(x => x.BaseResourceCost);
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
