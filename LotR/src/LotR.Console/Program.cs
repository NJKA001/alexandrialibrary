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

                var loader = new PlayerLoader();

                WriteLine();
                WriteLine("PlayerCards: {0}\r\n", loader.PlayerCards.Count());

                foreach (var playerCard in loader.PlayerCards)
                {
                    WriteLine("{0} {1}", playerCard.Title, GetPlayerCardTypeName(playerCard));
                }

                WriteLine("Press ENTER to close simulator");
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine("Error: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
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
