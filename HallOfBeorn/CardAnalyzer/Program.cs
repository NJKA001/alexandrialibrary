using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HallOfBeorn.Models;
using HallOfBeorn.Services;

namespace CardAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            service = new CardService();

            Console.WriteLine("Starting Analysis");

            AnalyzeCardRelationships();

            Console.WriteLine("Analysis Complete. Press ENTER to close.");
            Console.ReadLine();
        }

        private static CardService service;

        private static void AnalyzeCardRelationships()
        {
            var sb = new StringBuilder();
            string outputFile = "LoadCardRelationships.cs";

            foreach (var card in service.All().Where(x => x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Attachment || x.CardType == CardType.Event))
            {
                var decks = card.Decks.Count;
                if (decks < 5)
                    continue;

                Console.WriteLine("{0} ({1}) is found in {2} decks", card.Title, card.CardSet.Abbreviation, decks);

                var relationshipMap = new Dictionary<string, uint>();
                var threshold = (int)Math.Truncate(decks * .67);

                foreach (var deck in card.Decks)
                {
                    foreach (var otherCard in service.All().Where(x => x.Id != card.Id && x.Decks.Contains(deck)))
                    {
                        if (!relationshipMap.ContainsKey(otherCard.Id))
                            relationshipMap.Add(otherCard.Id, 1);
                        else
                            relationshipMap[otherCard.Id] += 1;
                    }
                }

                foreach (var pair in relationshipMap.Where(x => x.Value >= threshold))
                {
                    var otherCard = service.Find(pair.Key);
                    if (otherCard == null)
                        continue;

                    var leftTitle = !string.IsNullOrEmpty(card.NormalizedTitle) ? card.NormalizedTitle : card.Title;
                    var rightTitle = !string.IsNullOrEmpty(otherCard.NormalizedTitle) ? otherCard.NormalizedTitle : otherCard.Title;

                    sb.AppendLine(string.Format("AddRelationship(\"{0}\", \"{1}\", \"{2}\", \"{3}\");", leftTitle, card.CardSet.Abbreviation, rightTitle, otherCard.CardSet.Abbreviation));
                    //Console.WriteLine("    {0} is related to {1}", card.Title, service.Find(pair.Key).Title);
                }
            }

            System.IO.File.WriteAllText(outputFile, sb.ToString());
        }
    }
}
