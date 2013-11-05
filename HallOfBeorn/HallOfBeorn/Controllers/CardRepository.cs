using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HallOfBeorn.Models;
using HallOfBeorn.Models.Sets;

namespace HallOfBeorn.Controllers
{
    public class CardRepository
    {
        public CardRepository()
        {
            InitializeCards();
        }

        private readonly List<CardSet> sets = new List<CardSet>();
        private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();

        private void InitializeCards()
        {
            sets.Add(new AJourneytoRhosgobel());
            sets.Add(new AssaultonOsgiliath());
            sets.Add(new BoarandRaven());
            sets.Add(new CelebrimborsForge());
            sets.Add(new ConflictattheCarrock());
            sets.Add(new CoreSet());
            sets.Add(new CoreSetNightmare());
            sets.Add(new EncounteratAmonDin());
            sets.Add(new FoundationsofStone());
            sets.Add(new HeirsofNumenor());
            sets.Add(new IsengardCyclePlayerCards());
            sets.Add(new Khazaddum());
            sets.Add(new Ninineliph());
            sets.Add(new ReturntoMirkwood());
            sets.Add(new RoadtoRivendell());
            sets.Add(new ShadowandFlame());
            sets.Add(new TheBattleofLakeTown());
            sets.Add(new TheBlackRiders());
            sets.Add(new TheBloodofGondor());
            sets.Add(new TheDeadMarshes());
            sets.Add(new TheDruadanForest());
            sets.Add(new TheDunlandTrap());
            sets.Add(new TheHillsofEmynMuil());
            sets.Add(new TheHobbitOntheDoorstep());
            sets.Add(new TheHobbitOverHillandUnderHill());
            sets.Add(new TheHuntforGollum());
            sets.Add(new TheLongDark());
            sets.Add(new TheMassingatOsgiliath());
            sets.Add(new TheRedhornGate());
            sets.Add(new TheRoadDarkens());
            sets.Add(new TheStewardsFear());
            sets.Add(new TheStoneofErech());
            sets.Add(new TheThreeTrials());
            sets.Add(new TheWatcherintheWater());
            sets.Add(new TroubleinTharbad());

            foreach (var set in sets)
            {
                foreach (var card in set.Cards)
                    cards.Add(card.Id, card);
            }

            /*
            var coreSet = new CardSet() { Name = "Core" };


            var aragorn = new Card() { Id = "c1", ThreatCost = 12, Willpower = 2, Attack = 3, Defense = 2, HitPoints = 5, CardSet = coreSet, DeckType = CardDeckType.Player, CardType = CardType.Hero, Number = 1, FlavorText = "'I am Aragorn son of Arathorn, and if by life or death I can save you, I will.' -The Fellowship of the Ring", Title = "Aragorn", Traits = new List<string> { "Dunedain", "Noble", "Ranger" }, Artist = "John Stanko" };
            aragorn.GameText.Add(new CardEffect() { EffectType = CardEffectType.Keyword, Text = "Sentinel" });
            aragorn.GameText.Add(new CardEffect() { EffectType = CardEffectType.Response, Text = "After Aragorn commits to a quest, spend 1 resource from his resource pool to ready him." });

            var theodred = new Card() { Id = "c2", ThreatCost = 8, Willpower = 1, Attack = 2, Defense = 1, HitPoints = 4, CardSet = coreSet, DeckType = CardDeckType.Player, CardType = CardType.Hero, Number = 2, FlavorText = "'Not all is dark. Take courage, Lord of the Mark...' -Gandalf, The Two Towers", Title = "Theodred", Traits = new List<string> { "Noble", "Rohan", "Warrior" }, Artist = "Jeff Himmelman" };
            theodred.GameText.Add(new CardEffect() { EffectType = CardEffectType.Response, Text = "After Theodred commits to a quest, choose a hero commited to that quest. Add 1 resource to that hero's resource pool." });

            var gloin = new Card() { Id = "c3", ThreatCost = 9, Willpower = 2, Attack = 2, Defense = 1, HitPoints = 4, CardSet = coreSet, DeckType = CardDeckType.Player, CardType = Models.CardType.Hero, Number = 3, FlavorText = "'His beard, very long and forked, was white, nearly as white as the snow-white cloth of his garments.' -The Fellowship of the Ring", Title = "Gloin", Traits = new List<string> { "Dwarf", "Noble" }, Artist = "Tom Garden" };
            gloin.GameText.Add(new CardEffect() { EffectType = CardEffectType.Response, Text = "After Gloin suffers damage, add 1 resource to his resource pool for each point of damage he just suffered." });

            var forestGate = new Card() { Id = "c100", QuestPoints = 4, CardSet = coreSet, DeckType = CardDeckType.Encounter, CardType = Models.CardType.Location, Number = 100, Title = "Forest Gate", Traits = new List<string> { "Forest" } };
            forestGate.GameText.Add(new CardEffect() { EffectType = CardEffectType.Response, Text = "After you travel to Forest Gate, the first player may draw 2 cards." });

            var fliesAndSpiders = new Card() { Id = "c119", EncounterSet = "Passage Through Mirkwood", StageNumber = 1, QuestPoints = 8, CardSet = coreSet, DeckType = CardDeckType.Quest, CardType = Models.CardType.Quest, Number = 119, Title = "Flies and Spiders" };
            fliesAndSpiders.GameText.Add(new CardEffect() { EffectType = CardEffectType.Setup, Text = "Search the encounter deck for 1 copy of the Forest Spider and 1 copy of the Old Forest Road, and add them to the staging area. Then, shuffle the encounter deck." });

            AddCard(aragorn);
            AddCard(theodred);
            AddCard(gloin);
            AddCard(forestGate);
            AddCard(fliesAndSpiders);
            */
        }

        private void AddCard(Card card)
        {
            cards.Add(card.Id, card);
        }

        public List<Card> Cards
        {
            get { return cards.Values.ToList(); }
        }

        public Card GetCard(string id)
        {
            return cards.ContainsKey(id) ? cards[id] : null;
        }
    }
}