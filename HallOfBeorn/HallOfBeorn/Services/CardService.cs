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
            AddSet(new CoreSet());
            //AddSet(new CoreSetNightmare());

            AddSet(new TheHuntforGollum());
            AddSet(new ConflictattheCarrock());
            AddSet(new AJourneytoRhosgobel());
            AddSet(new TheHillsofEmynMuil());
            AddSet(new TheDeadMarshes());
            AddSet(new ReturntoMirkwood());

            AddSet(new Khazaddum());

            AddSet(new TheRedhornGate());
            AddSet(new RoadtoRivendell());
            AddSet(new TheWatcherintheWater());
            AddSet(new TheLongDark());
            AddSet(new FoundationsofStone());
            AddSet(new ShadowandFlame());

            AddSet(new HeirsofNumenor());

            AddSet(new TheStewardsFear());
            AddSet(new TheDruadanForest());
            AddSet(new EncounteratAmonDin());
            AddSet(new AssaultonOsgiliath());
            AddSet(new TheBloodofGondor());
            AddSet(new TheMorgulVale());

            AddSet(new TheHobbitOverHillandUnderHill());
            AddSet(new TheHobbitOntheDoorstep());
            AddSet(new TheBlackRiders());

            AddSet(new TheMassingatOsgiliath());
            AddSet(new TheBattleofLakeTown());
            AddSet(new TheStoneofErech());

            //AddSet(new IsengardCyclePlayerCards());
            //AddSet(new Ninineliph());
            //AddSet(new TheDunlandTrap());
            //AddSet(new TheThreeTrials());
            //AddSet(new TroubleinTharbad());
            //AddSet(new BoarandRaven());
            //AddSet(new CelebrimborsForge());

            //AddSet(new TheRoadDarkens());
        }

        private readonly List<CardSet> sets = new List<CardSet>();
        private readonly List<string> setNames = new List<string>();
        private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();
        private readonly Dictionary<string, string> keywords = new Dictionary<string, string>();
        private readonly Dictionary<string, string> traits = new Dictionary<string, string>();

        const int maxResults = 128;

        private void AddSet(CardSet cardSet)
        {
            sets.Add(cardSet);

            if (!string.IsNullOrEmpty(cardSet.Cycle) && !setNames.Contains(cardSet.Cycle.ToUpper()))
                setNames.Add(cardSet.Cycle.ToUpper());

            setNames.Add(cardSet.Name);

            foreach (var card in cardSet.Cards)
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
                    || x.Keywords.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    || x.NormalizedKeywords.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    || x.Traits.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    || x.NormalizedTraits.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    )
                .OrderBy(x => x.ImageName)
                .ToList()
                : cards.Values.OrderBy(x => x.CardSet.Number).ThenBy(x => x.Number).ToList();

            if (model.CardType != CardType.None)
            {
                if (model.CardType == CardType.Player)
                {
                    results = results.Where(x => x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Attachment || x.CardType == CardType.Event).ToList();
                }
                else if (model.CardType == CardType.Encounter)
                {
                    results = results.Where(x => x.CardType == CardType.Enemy || x.CardType == CardType.Location || x.CardType == CardType.Treachery || x.CardType == CardType.Objective || x.CardType == CardType.Objective_Ally).ToList();
                }
                else if (model.CardType == CardType.Objective)
                {
                    results = results.Where(x => x.CardType == CardType.Objective || x.CardType == CardType.Objective_Ally).ToList();
                }
                else if (model.CardType == CardType.Boon)
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
                results = results.Where(x => x.CardSet.Name == model.CardSet || (!string.IsNullOrEmpty(x.CardSet.Cycle) && x.CardSet.Cycle.ToUpper() == model.CardSet)).ToList();
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

        public IEnumerable<string> SetNames
        {
            get { return setNames; }
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