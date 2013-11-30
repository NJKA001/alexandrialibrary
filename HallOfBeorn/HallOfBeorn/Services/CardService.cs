using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HallOfBeorn.Models;
using HallOfBeorn.Models.Sets;

namespace HallOfBeorn.Services
{
    public class CardService
    {
        public CardService()
        {
            sets.Add(new CoreSet());
            //sets.Add(new CoreSetNightmare());

            sets.Add(new TheHuntforGollum());
            sets.Add(new ConflictattheCarrock());
            sets.Add(new AJourneytoRhosgobel());
            sets.Add(new TheHillsofEmynMuil());
            sets.Add(new TheDeadMarshes());
            sets.Add(new ReturntoMirkwood());

            sets.Add(new Khazaddum());

            sets.Add(new TheRedhornGate());
            sets.Add(new RoadtoRivendell());
            sets.Add(new TheWatcherintheWater());
            sets.Add(new TheLongDark());
            sets.Add(new FoundationsofStone());
            sets.Add(new ShadowandFlame());

            sets.Add(new HeirsofNumenor());

            sets.Add(new TheStewardsFear());
            sets.Add(new TheDruadanForest());
            sets.Add(new EncounteratAmonDin());
            sets.Add(new AssaultonOsgiliath());
            sets.Add(new TheBloodofGondor());
            sets.Add(new TheMorgulVale());

            sets.Add(new TheHobbitOverHillandUnderHill());
            sets.Add(new TheHobbitOntheDoorstep());
            sets.Add(new TheBlackRiders());

            sets.Add(new TheMassingatOsgiliath());
            sets.Add(new TheBattleofLakeTown());
            sets.Add(new TheStoneofErech());

            //sets.Add(new IsengardCyclePlayerCards());
            //sets.Add(new Ninineliph());
            //sets.Add(new TheDunlandTrap());
            //sets.Add(new TheThreeTrials());
            //sets.Add(new TroubleinTharbad());
            //sets.Add(new BoarandRaven());
            //sets.Add(new CelebrimborsForge());

            //sets.Add(new TheRoadDarkens());

            foreach (var set in sets)
            {
                foreach (var card in set.Cards)
                {
                    cards.Add(card.Id, card);

                    foreach (var keyword in card.Keywords)
                    {
                        var keywordKey = keyword.Trim();
                        if (!keywords.ContainsKey(keywordKey))
                            keywords.Add(keywordKey, keywordKey);
                    }

                    foreach (var trait in card.Traits)
                    {
                        var traitKey = trait.Replace(".", string.Empty).Trim();
                        if (!traits.ContainsKey(traitKey))
                            traits.Add(traitKey, trait.Trim());
                    }
                }
            }
        }

        private readonly List<CardSet> sets = new List<CardSet>();
        private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();
        private readonly Dictionary<string, string> keywords = new Dictionary<string, string>();
        private readonly Dictionary<string, string> traits = new Dictionary<string, string>();

        const int maxResults = 128;

        public IEnumerable<Card> All()
        {
            return cards.Values.ToList().Take(maxResults);
        }

        public IEnumerable<Card> Search(SearchViewModel model)
        {
            var results = !string.IsNullOrEmpty(model.Query) ?
                cards.Values.Where(
                    x => x.Title.ToLower().Contains(model.Query.ToLower())
                    || (!string.IsNullOrEmpty(x.NormalizedTitle) && x.NormalizedTitle.ToLower().Contains(model.Query.ToLower()))
                    || (!string.IsNullOrEmpty(x.Text) && x.Text.ToLower().Contains(model.Query.ToLower()))
                    || x.Traits.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    || x.NormalizedTraits.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    )
                .OrderBy(x => x.ImageName)
                .ToList()
                : cards.Values.OrderBy(x => x.CardSet.Number).ThenBy(x => x.Number).ToList();

            if (model.CardType != CardType.None)
            {
                if (model.CardType == CardType.Boon)
                {
                    results = results.Where(x => x.CampaignCardType == CampaignCardType.Boon).ToList();
                }
                else if (model.CardType == CardType.Burden)
                {
                    results = results.Where(x => x.CampaignCardType == CampaignCardType.Burden).ToList();
                }
                else
                    results = results.Where(x => x.CardType == model.CardType).ToList();
            }

            if (model.CardSet != null && model.CardSet != "Any")
            {
                results = results.Where(x => x.CardSet.Name == model.CardSet).ToList();
            }

            if (model.Trait != null && model.Trait != "Any")
            {
                results = results.Where(x => x.HasTrait(model.Trait)).ToList();
            }

            if (model.Keyword != null && model.Keyword != "Any")
            {
                results = results.Where(x => x.HasKeyword(model.Keyword)).ToList();
            }

            if (model.Sphere != Sphere.None)
            {
                results = results.Where(x => x.Sphere == model.Sphere).ToList();
            }

            if (model.Cost != -1)
            {
                results = results.Where(x => x.ResourceCost == model.Cost).ToList();
            }

            //if (!string.IsNullOrEmpty(model.title))
            //{
            //    cards = cards.Where(x => x.Title.ToLower() == title.ToLower()).ToList();
            //}

            return results.Take(maxResults);//.OrderBy(x => x.Title);
        }

        public Card Find(string id)
        {
            return cards.ContainsKey(id) ?
                cards[id]
                : null;
        }

        public IEnumerable<byte> Costs()
        {
            return cards.Values.Select(x => x.ResourceCost).Distinct().OrderBy(x => x).ToList();
        }

        public IEnumerable<CardSet> Sets
        {
            get { return sets; }
        }

        public IEnumerable<string> Keywords()
        {
            return keywords.Values.ToList().OrderBy(x => x).ToList();
        }

        public IEnumerable<string> Traits()
        {
            return traits.Values.ToList().OrderBy(x => x).ToList();
        }
    }
}