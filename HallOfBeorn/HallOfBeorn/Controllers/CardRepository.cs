using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HallOfBeorn.Models;

namespace HallOfBeorn.Controllers
{
    public class CardRepository
    {
        public CardRepository()
        {
            InitializeCards();
        }

        private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();

        private void InitializeCards()
        {
            var coreSet = new CardSet() { Name = "Core" };
            var aragorn = new Card() { Id = "c1", ThreatCost = 12, HitPoints = 5, CardSet = coreSet, DeckType = CardDeckType.Player, CardType = CardType.Hero, GameText = "Response: After Aragorn commits to a quest, spend 1 resource from his resource pool to ready him.", Number = 1, FlavorText = "'I am Aragorn son of Arathorn, and if by life or death I can save you, I will.' -The Fellowship of the Ring", Title = "Aragorn", Traits = new List<string> { "Dunedain", "Noble", "Ranger" }, Artist = "John Stanko" };
            var theodred = new Card() { Id = "c2", ThreatCost = 8, HitPoints = 4, CardSet = coreSet, DeckType = CardDeckType.Player, CardType = CardType.Hero, GameText = "Response: After Theodred commits to a quest, choose a hero commited to that quest. Add 1 resource to that hero's resource pool.", Number = 2, FlavorText = "'Not all is dark. Take courage, Lord of the Mark...' -Gandalf, The Two Towers", Title = "Theodred", Traits = new List<string> { "Noble", "Rohan", "Warrior" }, Artist = "Jeff Himmelman" };
            var gloin = new Card() { Id = "c3", ThreatCost = 9, HitPoints = 4, CardSet = coreSet, DeckType = CardDeckType.Player, CardType = Models.CardType.Hero, GameText = "Response: After Gloin suffers damage, add 1 resource to his resource pool for each point of damage he just suffered.", Number = 3, FlavorText = "'His beard, very long and forked, was white, nearly as white as the snow-white cloth of his garments.' -The Fellowship of the Ring", Title = "Gloin", Traits = new List<string> { "Dwarf", "Noble" }, Artist = "Tom Garden" };
            var forestGate = new Card() { Id = "c100", QuestPoints = 4, CardSet = coreSet, DeckType = CardDeckType.Encounter, CardType = Models.CardType.Location, GameText = "Response: After you travel to Forest Gate, the first player may draw 2 cards.", Number = 100, Title = "Forest Gate", Traits = new List<string> { "Forest" } };
            var fliesAndSpiders = new Card() { Id = "c119", ScenarioTitle = "Passage Through Mirkwood", StageNumber = 1, QuestPoints = 8, CardSet = coreSet, DeckType = CardDeckType.Quest, CardType = Models.CardType.Quest, GameText = "Setup: Search the encounter deck for 1 copy of the Forest Spider and 1 copy of the Old Forest Road, and add them to the staging area. Then, shuffle the encounter deck.", Title = "Flies and Spiders", Number = 119 };

            AddCard(aragorn);
            AddCard(theodred);
            AddCard(gloin);
            AddCard(forestGate);
            AddCard(fliesAndSpiders);
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