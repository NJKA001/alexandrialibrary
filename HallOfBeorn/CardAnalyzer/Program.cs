using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HallOfBeorn.Models;
using HallOfBeorn.Services;

namespace CardAnalyzer
{
    public static class Extensions
    {
        public static IEnumerable<int> Mode(this IEnumerable<int> values)
        {
            var valueCountMap = new Dictionary<int, int>();
            var mostFrequentValues = new List<int>();
            
            foreach (var value in values)
            {
                if (!valueCountMap.ContainsKey(value))
                    valueCountMap[value] = 1;
                else
                    valueCountMap[value] = valueCountMap[value] + 1;
            }

            var mode = new List<int>();

            var maxCount = 0;
            foreach (var item in valueCountMap)
            {
                if (item.Value > maxCount)
                {
                    maxCount = item.Value;
                    mode.Clear();
                }

                if (item.Value == maxCount)
                {
                    mode.Add(item.Key);
                }
            }

            System.Console.WriteLine(string.Join(", ", mode.Select(x => x.ToString())));

            return mode.OrderBy(x => x);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            service = new CardService();

            Console.WriteLine("Starting Analysis");

            //AnalyzeCardRelationships();
            AnalyzeSphereBreakdowns();

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

        private static void AnalyzeSphereBreakdowns()
        {
            var path = "SphereBreakdowns.txt";

            var sb = new StringBuilder();

            var avgHeroThreatLeadership = service.All().Where(x => x.Sphere == Sphere.Leadership && x.CardType == CardType.Hero).Select(x => (int)x.ThreatCost).Average();
            var modeHeroThreatLeadership = service.All().Where(x => x.Sphere == Sphere.Leadership && x.CardType == CardType.Hero).Select(x => (int)x.ThreatCost).Mode();
            var avgHeroThreatTactics = service.All().Where(x => x.Sphere == Sphere.Tactics && x.CardType == CardType.Hero).Select(x => (int)x.ThreatCost).Average();
            var modeHeroThreatTactics = service.All().Where(x => x.Sphere == Sphere.Tactics && x.CardType == CardType.Hero).Select(x => (int)x.ThreatCost).Mode();
            var avgHeroThreatSpirit = service.All().Where(x => x.Sphere == Sphere.Spirit && x.CardType == CardType.Hero).Select(x => (int)x.ThreatCost).Average();
            var modeHeroThreatSpirit = service.All().Where(x => x.Sphere == Sphere.Spirit && x.CardType == CardType.Hero).Select(x => (int)x.ThreatCost).Mode();
            var avgHeroThreatLore = service.All().Where(x => x.Sphere == Sphere.Lore && x.CardType == CardType.Hero).Select(x => (int)x.ThreatCost).Average();
            var modeHeroThreatLore = service.All().Where(x => x.Sphere == Sphere.Lore && x.CardType == CardType.Hero).Select(x => (int)x.ThreatCost).Mode();

            var avgAllyCostLeadership = service.All().Where(x => x.Sphere == Sphere.Leadership && x.CardType == CardType.Ally).Select(x => (int)x.ResourceCost).Average();
            var modeAllyCostLeadership = service.All().Where(x => x.Sphere == Sphere.Leadership && x.CardType == CardType.Ally).Select(x => (int)x.ResourceCost).Mode();
            var avgAllyCostTactics = service.All().Where(x => x.Sphere == Sphere.Tactics && x.CardType == CardType.Ally).Select(x => (int)x.ResourceCost).Average();
            var modeAllyCostTactics = service.All().Where(x => x.Sphere == Sphere.Tactics && x.CardType == CardType.Ally).Select(x => (int)x.ResourceCost).Mode();
            var avgAllyCostSpirit = service.All().Where(x => x.Sphere == Sphere.Spirit && x.CardType == CardType.Ally).Select(x => (int)x.ResourceCost).Average();
            var modeAllyCostSpirit = service.All().Where(x => x.Sphere == Sphere.Spirit && x.CardType == CardType.Ally).Select(x => (int)x.ResourceCost).Mode();
            var avgAllyCostLore = service.All().Where(x => x.Sphere == Sphere.Lore && x.CardType == CardType.Ally).Select(x => (int)x.ResourceCost).Average();
            var modeAllyCostLore = service.All().Where(x => x.Sphere == Sphere.Lore && x.CardType == CardType.Ally).Select(x => (int)x.ResourceCost).Mode();

            var avgAttachmentCostLeadership = service.All().Where(x => x.Sphere == Sphere.Leadership && x.CardType == CardType.Attachment).Select(x => (int)x.ResourceCost).Average();
            var modeAttachmentCostLeadership = service.All().Where(x => x.Sphere == Sphere.Leadership && x.CardType == CardType.Attachment).Select(x => (int)x.ResourceCost).Mode();
            var avgAttachmentCostTactics = service.All().Where(x => x.Sphere == Sphere.Tactics && x.CardType == CardType.Attachment).Select(x => (int)x.ResourceCost).Average();
            var modeAttachmentCostTactics = service.All().Where(x => x.Sphere == Sphere.Tactics && x.CardType == CardType.Attachment).Select(x => (int)x.ResourceCost).Mode();
            var avgAttachmentCostSpirit = service.All().Where(x => x.Sphere == Sphere.Spirit && x.CardType == CardType.Attachment).Select(x => (int)x.ResourceCost).Average();
            var modeAttachmentCostSpirit = service.All().Where(x => x.Sphere == Sphere.Spirit && x.CardType == CardType.Attachment).Select(x => (int)x.ResourceCost).Mode();
            var avgAttachmentCostLore = service.All().Where(x => x.Sphere == Sphere.Lore && x.CardType == CardType.Attachment).Select(x => (int)x.ResourceCost).Average();
            var modeAttachmentCostLore = service.All().Where(x => x.Sphere == Sphere.Lore && x.CardType == CardType.Attachment).Select(x => (int)x.ResourceCost).Mode();

            var avgEventCostLeadership = service.All().Where(x => x.Sphere == Sphere.Leadership && x.CardType == CardType.Event).Select(x => (int)x.ResourceCost).Average();
            var modeEventCostLeadership = service.All().Where(x => x.Sphere == Sphere.Leadership && x.CardType == CardType.Event).Select(x => (int)x.ResourceCost).Mode();
            var avgEventCostTactics = service.All().Where(x => x.Sphere == Sphere.Tactics && x.CardType == CardType.Event).Select(x => (int)x.ResourceCost).Average();
            var modeEventCostTactics = service.All().Where(x => x.Sphere == Sphere.Tactics && x.CardType == CardType.Event).Select(x => (int)x.ResourceCost).Mode();
            var avgEventCostSpirit = service.All().Where(x => x.Sphere == Sphere.Spirit && x.CardType == CardType.Event).Select(x => (int)x.ResourceCost).Average();
            var modeEventCostSpirit = service.All().Where(x => x.Sphere == Sphere.Spirit && x.CardType == CardType.Event).Select(x => (int)x.ResourceCost).Mode();
            var avgEventCostLore = service.All().Where(x => x.Sphere == Sphere.Lore && x.CardType == CardType.Event).Select(x => (int)x.ResourceCost).Average();
            var modeEventCostLore = service.All().Where(x => x.Sphere == Sphere.Lore && x.CardType == CardType.Event).Select(x => (int)x.ResourceCost).Mode();

            sb.AppendLine("Hero Threat Cost");
            sb.AppendLine("                Mean     Mode");
            sb.AppendFormat("    Leadership: {0,5:#0.00}    {1}\r\n", avgHeroThreatLeadership, string.Join(", ", modeHeroThreatLeadership.Select(x => string.Format("{0,2:#0}", x))));
            sb.AppendFormat("    Tactics   : {0,5:#0.00}    {1}\r\n", avgHeroThreatTactics, string.Join(", ", modeHeroThreatTactics.Select(x => string.Format("{0,2:#0}", x))));
            sb.AppendFormat("    Spirit    : {0,5:#0.00}    {1}\r\n", avgHeroThreatSpirit, string.Join(", ", modeHeroThreatSpirit.Select(x => string.Format("{0,2:#0}", x))));
            sb.AppendFormat("    Lore      : {0,5:#0.00}    {1}\r\n", avgHeroThreatLore, string.Join(", ", modeHeroThreatLore.Select(x => string.Format("{0,2:#0}", x))));

            sb.AppendLine();
            sb.AppendLine("Ally Resource Cost");
            sb.AppendLine("                Mean     Mode");
            sb.AppendFormat("    Leadership: {0,5:#0.00}    {1}\r\n", avgAllyCostLeadership, string.Join(", ", modeAllyCostLeadership.Select(x => string.Format("{0,1:#}", x))));
            sb.AppendFormat("    Tactics   : {0,5:#0.00}    {1}\r\n", avgAllyCostTactics, string.Join(", ", modeAllyCostTactics.Select(x => string.Format("{0,1:#}", x))));
            sb.AppendFormat("    Spirit    : {0,5:#0.00}    {1}\r\n", avgAllyCostSpirit, string.Join(", ", modeAllyCostSpirit.Select(x => string.Format("{0,1:#}", x))));
            sb.AppendFormat("    Lore      : {0,5:#0.00}    {1}\r\n", avgAllyCostLore, string.Join(", ", modeAllyCostLore.Select(x => string.Format("{0,1:#}", x))));

            sb.AppendLine();
            sb.AppendLine("Attachment Resource Cost");
            sb.AppendLine("                Mean     Mode");
            sb.AppendFormat("    Leadership: {0,5:#0.00}    {1}\r\n", avgAttachmentCostLeadership, string.Join(", ", modeAttachmentCostLeadership.Select(x => string.Format("{0,1:#}", x))));
            sb.AppendFormat("    Tactics   : {0,5:#0.00}    {1}\r\n", avgAttachmentCostTactics, string.Join(", ", modeAttachmentCostTactics.Select(x => string.Format("{0,1:#}", x))));
            sb.AppendFormat("    Spirit    : {0,5:#0.00}    {1}\r\n", avgAttachmentCostSpirit, string.Join(", ", modeAttachmentCostSpirit.Select(x => string.Format("{0,1:#}", x))));
            sb.AppendFormat("    Lore      : {0,5:#0.00}    {1}\r\n", avgAttachmentCostLore, string.Join(", ", modeAttachmentCostLore.Select(x => string.Format("{0,1:#}", x))));

            sb.AppendLine();
            sb.AppendLine("Event Resource Cost");
            sb.AppendLine("                Mean     Mode");
            sb.AppendFormat("    Leadership: {0,5:#0.00}    {1}\r\n", avgEventCostLeadership, string.Join(", ", modeEventCostLeadership.Select(x => string.Format("{0,1:0}", x))));
            sb.AppendFormat("    Tactics   : {0,5:#0.00}    {1}\r\n", avgEventCostTactics, string.Join(", ", modeEventCostTactics.Select(x => string.Format("{0,1:0}", x))));
            sb.AppendFormat("    Spirit    : {0,5:#0.00}    {1}\r\n", avgEventCostSpirit, string.Join(", ", modeEventCostSpirit.Select(x => string.Format("{0,1:0}", x))));
            sb.AppendFormat("    Lore      : {0,5:#0.00}    {1}\r\n", avgEventCostLore, string.Join(", ", modeEventCostLore.Select(x => string.Format("{0,1:0}", x))));

            System.IO.File.WriteAllText(path, sb.ToString());
        }
    }
}
