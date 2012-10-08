using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

                loader = new PlayerLoader();

                var path = "SampleDeck.txt";
                var player = LoadPlayer(path);

                DisplayPlayer(player);

                var line = "start";
                while (!string.IsNullOrEmpty(line))
                {
                    WriteLine();
                    WriteLine("Drawing 6 Cards (press ENTER to stop)");
                    
                    player.Deck.Shuffle();
                    player.Deck.Shuffle();

                    foreach (var card in player.Deck.GetFromTop(6))
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

        private static IPlayerLoader loader;

        private static IPlayer LoadPlayer(string path)
        {
            WriteLine("Loading File: {0}", path);

            var player = loader.Load(path);

            WriteLine("  File Loaded");

            return player;
        }

        private static void DisplayPlayer(IPlayer player)
        {
            WriteLine();
            WriteLine("Player: {0}", player.Name);
            if (player.StartingHeroes.Count() > 0)
            {
                WriteLine("Heroes: {0}", string.Join(", ", player.StartingHeroes.Select(x => x.Title)));
                WriteLine("Starting Threat: {0}", player.StartingThreat);
            }
            else
            {
                WriteLine("Heroes: None");
                WriteLine("Starting Threat: 0");
            }
            WriteLine();
            WriteLine("Cards: {0} with Avg. Cost {1:0.0}", player.Deck.Cards.Count(), player.Deck.Cards.OfType<ICostlyCard>().Average(x => x.BaseResourceCost));
            foreach (var sphere in player.Deck.Cards.OfType<ICostlyCard>().GroupBy(x => x.BaseResourceSphere))
            {
                var count = player.Deck.Cards.OfType<ICostlyCard>().Where(x => x.BaseResourceSphere == sphere.Key).Count();
                var avg = player.Deck.Cards.OfType<ICostlyCard>().Where(x => x.BaseResourceSphere == sphere.Key).Average(x => x.BaseResourceCost);
                WriteLine("{0} Sphere: {1} Cards with Avg. Cost {2:0.0}", sphere.Key, count, avg);
            }

            if (player.Deck.Cards.OfType<IAllyCard>().Count() > 0)
            {
                WriteLine();
                WriteLine("{0,3} Allies", player.Deck.Cards.OfType<IAllyCard>().Count());
                foreach (var card in player.Deck.Cards.OfType<IAllyCard>().GroupBy(x => x.Title))
                {
                    WriteLine("{0,3} {1}", player.Deck.Cards.OfType<IAllyCard>().Where(x => x.Title == card.Key).Count(), card.Key);
                }
            }

            if (player.Deck.Cards.OfType<IAttachmentCard>().Count() > 0)
            {
                WriteLine();
                WriteLine("{0,3} Attachments", player.Deck.Cards.OfType<IAttachmentCard>().Count());
                foreach (var card in player.Deck.Cards.OfType<IAttachmentCard>().GroupBy(x => x.Title))
                {
                    WriteLine("{0,3} {1}", player.Deck.Cards.OfType<IAttachmentCard>().Where(x => x.Title == card.Key).Count(), card.Key);
                }
            }

            if (player.Deck.Cards.OfType<IEventCard>().Count() > 0)
            {
                WriteLine();
                WriteLine("{0,3} Events", player.Deck.Cards.OfType<IEventCard>().Count());
                foreach (var card in player.Deck.Cards.OfType<IEventCard>().GroupBy(x => x.Title))
                {
                    WriteLine("{0,3} {1}", player.Deck.Cards.OfType<IEventCard>().Where(x => x.Title == card.Key).Count(), card.Key);
                }
            }

            if (player.Deck.Cards.OfType<ITreasureCard>().Count() > 0)
            {
                WriteLine();
                WriteLine("{0,3} Treasures", player.Deck.Cards.OfType<ITreasureCard>().Count());
                foreach (var card in player.Deck.Cards.OfType<ITreasureCard>().GroupBy(x => x.Title))
                {
                    WriteLine("{0,3} {1}", player.Deck.Cards.OfType<ITreasureCard>().Where(x => x.Title == card.Key).Count(), card.Key);
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
