using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HallOfBeorn.Models;
using HallOfBeorn.Models.Decks.HallOfBeorn;
using HallOfBeorn.Models.Decks.TalesFromTheCards;
using HallOfBeorn.Models.Sets;

namespace HallOfBeorn.Services
{
    public class CardService
    {
        public CardService()
        {
            LoadSets();
            LoadDecks();
            LoadRelationships();
        }

        private readonly List<CardSet> sets = new List<CardSet>();
        private readonly List<string> setNames = new List<string>();
        private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();
        private readonly Dictionary<string, string> keywords = new Dictionary<string, string>();
        private readonly Dictionary<string, string> traits = new Dictionary<string, string>();
        private readonly List<Deck> decks = new List<Deck>();

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

        private void AddDeck(Deck deck)
        {
            decks.Add(deck);

            if (string.IsNullOrEmpty(deck.DeckList))
                return;

            foreach (var line in deck.DeckList.Split(new string [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var tokens = line.Split(' ').ToList();
                if (tokens == null || tokens.Count == 0)
                    continue;

                if (tokens.Last().StartsWith("x"))
                {
                    tokens.Remove(tokens.Last());
                }

                var setName = string.Empty;

                var last = tokens.LastOrDefault();
                if (last != null && last.Contains('(') && last.Contains(')'))
                {
                    setName = last.Trim().TrimStart('(').TrimEnd(')');
                    tokens.Remove(last);
                }

                if (tokens.Count == 0)
                    continue;

                var title = string.Join(" ", tokens).ToLower();

                var card = cards.Values.Where(x => 
                    (
                        (string.Equals(x.Title.ToLower(), title) || (!string.IsNullOrEmpty(x.NormalizedTitle) && string.Equals(x.NormalizedTitle.ToLower(), title)))
                        && ( string.IsNullOrEmpty(setName) || ( string.Equals(x.CardSet.Abbreviation, setName) || string.Equals(x.CardSet.Name, setName) ) )
                        && ( x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Attachment || x.CardType == CardType.Event || x.CardType == CardType.Treasure || x.CardType == CardType.Boon )
                    )
                ).FirstOrDefault();

                if (card != null)
                {
                    card.Decks.Add(deck);
                }
            }
        }

        private void LoadSets()
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

        private void LoadDecks()
        {
            AddDeck(new BoromirLeadsTheCharge());
            AddDeck(new CaldarasSacrifice());
            AddDeck(new KeepItSecretKeepItSafe());
            AddDeck(new RangersAndTraps());
            AddDeck(new SecretsOfTheWise());
            AddDeck(new CalledToTheSea());
            AddDeck(new PrisonerOfTheDarkForest());
            AddDeck(new ReclaimingKhazadDum());
            AddDeck(new ThreeKingsAQueenAndAPrince());
            AddDeck(new TheGreyCompanyDefendsGondor());
            AddDeck(new MagaliTribute());
            AddDeck(new BeornAttacks());
            AddDeck(new MastersOfTheForest());
            AddDeck(new TheFieldOfCormallen());
            AddDeck(new IsildursHeir());
            AddDeck(new FlightToTheFord());
            AddDeck(new TwoKingdomsReunited());
            AddDeck(new HamaTakesArcheryLessons());
            AddDeck(new BardGoesHunting());
            AddDeck(new TheIslandOfMisfitHeroes());
            AddDeck(new BalinHoldsTheLine());
            AddDeck(new TheRohirrimRideWithTheGreyCompany());
            AddDeck(new ThePowerOfThePalantir());
            AddDeck(new LoreMastery());
            AddDeck(new EaglesAndHorsesAndBearsOhMy());
            AddDeck(new TheDwarvesAndFaramir());
            AddDeck(new LocationControl());
            AddDeck(new Gluttony());
            AddDeck(new BoromirAndTheSevenDwarves());
            AddDeck(new VilyaTheRingOfAir());
            AddDeck(new DirectDamageTacticsLeadership());
            AddDeck(new DirectDamageLeadershipSpiritLore());
            AddDeck(new RenewedFriendshipsDwarvesElvesAndMen());
            AddDeck(new RenewedFriendshipsElvesAndElfFriends());
            AddDeck(new BattleOfPelennorRideToRuin());
            AddDeck(new BattleOfPelennorAndTheWorldsEnding());
            AddDeck(new SecretsOfErebor());
            AddDeck(new SecretsOfErebor2());
            AddDeck(new TheOrcHuntersOfImladris());
            AddDeck(new WardensOfImladris());

            AddDeck(new RideToRuin());
            AddDeck(new SpearmanSuperhero());
            AddDeck(new PalantirSupport());
            AddDeck(new TrapsOfIthilien());
            AddDeck(new EleanorsBigAdventure());
            AddDeck(new OutlandsGoneWild());
            AddDeck(new BlazeOfGlory());
            AddDeck(new RidingWithRohan());
            AddDeck(new WhereEaglesDare());

            AddDeck(new BeornsPathPTMLeadershipLore());
            AddDeck(new BeornsPathJAtALeadershipLore());
            AddDeck(new BeornsPathEFDGTacticsSpirit());
            AddDeck(new BeornsPathTHFGLeadershipLore());
            AddDeck(new BeornsPathTHFGTacticsSpirit());
            AddDeck(new BeornsPathCatCLeadershipLore());
            AddDeck(new BeornsPathAJtRLeadershipLore());
            AddDeck(new BeornsPathTHoEMLeadershipLore());
            AddDeck(new BeornsPathTDMLeadershipLore());
            AddDeck(new BeornsPathRtMTacticsSpirit());
        }

        private void AddRelationship(string leftTitle, string leftSet, string rightTitle, string rightSet)
        {
            var leftCard = cards.Values.Where(x => (string.Equals(x.Title, leftTitle) || string.Equals(x.NormalizedTitle, leftTitle)) && string.Equals(x.CardSet.Abbreviation, leftSet)).FirstOrDefault();
            var rightCard = cards.Values.Where(x => (string.Equals(x.Title, rightTitle) || string.Equals(x.NormalizedTitle, rightTitle)) && string.Equals(x.CardSet.Abbreviation, rightSet)).FirstOrDefault();

            if (leftCard == null || rightCard == null)
                return;

            if (!leftCard.RelatedCards.Any(x => x.Id == rightCard.Id))
                leftCard.RelatedCards.Add(rightCard);

            if (!rightCard.RelatedCards.Any(x => x.Id == leftCard.Id))
                rightCard.RelatedCards.Add(leftCard);
        }

        private void LoadRelationships()
        {
            AddRelationship("Aragorn", "Core", "Faramir", "Core");
            AddRelationship("Aragorn", "Core", "Sneak Attack", "Core");
            AddRelationship("Aragorn", "Core", "Steward of Gondor", "Core");
            AddRelationship("Aragorn", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Aragorn", "Core", "Gandalf", "Core");
            AddRelationship("Aragorn", "Core", "Guard of the Citadel", "Core");

            AddRelationship("Theodred", "Core", "Snowbourn Scout", "Core");
            AddRelationship("Theodred", "Core", "Steward of Gondor", "Core");
            AddRelationship("Theodred", "Core", "Faramir", "Core");
            AddRelationship("Theodred", "Core", "Aragorn", "Core");
            AddRelationship("Theodred", "Core", "Sneak Attack", "Core");

            AddRelationship("Gloin", "Core", "Lure of Moria", "RtR");
            AddRelationship("Gloin", "Core", "Longbeard Elder", "FoS");
            AddRelationship("Gloin", "Core", "We Are Not Idle", "SaF");
            AddRelationship("Gloin", "Core", "Cram", "THOHaUH");

            AddRelationship("Gimli", "Core", "Citadel Plate", "Core");
            AddRelationship("Gimli", "Core", "Horn of Gondor", "Core");
            AddRelationship("Gimli", "Core", "Gandalf", "Core");
            AddRelationship("Gimli", "Core", "Winged Guardian", "THfG");
            AddRelationship("Gimli", "Core", "Feint", "Core");
            AddRelationship("Gimli", "Core", "Quick Strike", "Core");
            AddRelationship("Gimli", "Core", "Swift Strike", "Core");
            AddRelationship("Gimli", "Core", "Thalin", "Core");

            AddRelationship("Legolas", "Core", "Sneak Attack", "Core");
            AddRelationship("Legolas", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Legolas", "Core", "Feint", "Core");
            AddRelationship("Legolas", "Core", "Blade of Gondolin", "Core");
            AddRelationship("Legolas", "Core", "Vassal of the Windlord", "TDM");
            AddRelationship("Legolas", "Core", "Rivendell Blade", "RtR");
            AddRelationship("Legolas", "Core", "Hands Upon the Bow", "SaF");
            AddRelationship("Legolas", "Core", "Gondorian Shield", "TSF");
            AddRelationship("Legolas", "Core", "Foe-hammer", "THOHaUH");
            AddRelationship("Legolas", "Core", "Horn of Gondor", "Core");
            AddRelationship("Legolas", "Core", "Hail of Stones", "RtR");

            AddRelationship("Thalin", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Thalin", "Core", "Feint", "Core");
            AddRelationship("Thalin", "Core", "Swift Strike", "Core");
            AddRelationship("Thalin", "Core", "Blade of Gondolin", "Core");
            AddRelationship("Thalin", "Core", "Horn of Gondor", "Core");
            AddRelationship("Thalin", "Core", "Gimli", "Core");

            AddRelationship("Eowyn", "Core", "Northern Tracker", "Core");
            AddRelationship("Eowyn", "Core", "A Test of Will", "Core");
            AddRelationship("Eowyn", "Core", "Unexpected Courage", "Core");
            AddRelationship("Eowyn", "Core", "Gandalf", "Core");
            AddRelationship("Denethor", "Core", "Gandalf", "Core");
            AddRelationship("Denethor", "Core", "Gleowine", "Core");

            AddRelationship("Faramir", "Core", "Sneak Attack", "Core");
            AddRelationship("Faramir", "Core", "Steward of Gondor", "Core");
            AddRelationship("Faramir", "Core", "Gandalf", "Core");
            AddRelationship("Faramir", "Core", "Celebrian's Stone", "Core");

            AddRelationship("Longbeard Orc Slayer", "Core", "Sneak Attack", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Steward of Gondor", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Gandalf", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Lure of Moria", "RtR");
            AddRelationship("Longbeard Orc Slayer", "Core", "Longbeard Elder", "FoS");
            AddRelationship("Longbeard Orc Slayer", "Core", "We Are Not Idle", "SaF");
            AddRelationship("Longbeard Orc Slayer", "Core", "Miner of the Iron Hills", "Core");

            AddRelationship("Sneak Attack", "Core", "Faramir", "Core");
            AddRelationship("Sneak Attack", "Core", "Steward of Gondor", "Core");
            AddRelationship("Sneak Attack", "Core", "Gandalf", "Core");

            AddRelationship("Steward of Gondor", "Core", "Sneak Attack", "Core");
            AddRelationship("Steward of Gondor", "Core", "Gandalf", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Steward of Gondor", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Aragorn", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Faramir", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Sneak Attack", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Gandalf", "Core");
            
            AddRelationship("Gondorian Spearman", "Core", "Feint", "Core");
            AddRelationship("Gondorian Spearman", "Core", "Gandalf", "Core");
            AddRelationship("Gondorian Spearman", "Core", "Horn of Gondor", "Core");
            
            AddRelationship("Feint", "Core", "Gandalf", "Core");
            AddRelationship("Feint", "Core", "A Test of Will", "Core");
            AddRelationship("Feint", "Core", "Horn of Gondor", "Core");
            
            AddRelationship("Quick Strike", "Core", "Gimli", "Core");
            AddRelationship("Quick Strike", "Core", "Feint", "Core");
            AddRelationship("Quick Strike", "Core", "Swift Strike", "Core");

            AddRelationship("Swift Strike", "Core", "Feint", "Core");
            AddRelationship("Swift Strike", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Swift Strike", "Core", "Thalin", "Core");

            AddRelationship("Blade of Gondolin", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Blade of Gondolin", "Core", "Feint", "Core");
            AddRelationship("Blade of Gondolin", "Core", "Gandalf", "Core");
            AddRelationship("Blade of Gondolin", "Core", "Hands Upon the Bow", "SaF");
            AddRelationship("Blade of Gondolin", "Core", "Foe-hammer", "THOHaUH");
            AddRelationship("Blade of Gondolin", "Core", "Horn of Gondor", "Core");
            
            AddRelationship("Citadel Plate", "Core", "Gimli", "Core");
            AddRelationship("Citadel Plate", "Core", "Blade of Gondolin", "Core");
            AddRelationship("Citadel Plate", "Core", "Horn of Gondor", "Core");

            AddRelationship("Horn of Gondor", "Core", "A Test of Will", "Core");
            AddRelationship("Horn of Gondor", "Core", "Gandalf", "Core");
            AddRelationship("Horn of Gondor", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Horn of Gondor", "Core", "Feint", "Core");
            AddRelationship("Northern Tracker", "Core", "A Test of Will", "Core");
            AddRelationship("Northern Tracker", "Core", "Unexpected Courage", "Core");
            AddRelationship("Northern Tracker", "Core", "Glorfindel", "FoS");
            AddRelationship("Northern Tracker", "Core", "Imladris Stargazer", "FoS");
            AddRelationship("Northern Tracker", "Core", "Light of Valinor", "FoS");
            AddRelationship("Northern Tracker", "Core", "Gandalf", "Core");
            AddRelationship("The Galadhrim's Greeting", "Core", "Northern Tracker", "Core");
            AddRelationship("The Galadhrim's Greeting", "Core", "A Test of Will", "Core");
            AddRelationship("The Galadhrim's Greeting", "Core", "Unexpected Courage", "Core");
            AddRelationship("The Galadhrim's Greeting", "Core", "Gandalf", "Core");
            AddRelationship("The Galadhrim's Greeting", "Core", "Imladris Stargazer", "FoS");
            AddRelationship("Hasty Stroke", "Core", "A Test of Will", "Core");
            AddRelationship("Hasty Stroke", "Core", "Unexpected Courage", "Core");
            AddRelationship("Hasty Stroke", "Core", "Escort from Edoras", "AJtR");
            AddRelationship("Hasty Stroke", "Core", "Gandalf", "Core");
            AddRelationship("Hasty Stroke", "Core", "Feint", "Core");
            AddRelationship("Hasty Stroke", "Core", "Horn of Gondor", "Core");
            AddRelationship("A Test of Will", "Core", "Unexpected Courage", "Core");
            AddRelationship("A Test of Will", "Core", "Gandalf", "Core");
            AddRelationship("Stand and Fight", "Core", "Eowyn", "Core");
            AddRelationship("Stand and Fight", "Core", "Feint", "Core");
            AddRelationship("Stand and Fight", "Core", "Horn of Gondor", "Core");
            AddRelationship("Stand and Fight", "Core", "The Galadhrim's Greeting", "Core");
            AddRelationship("Stand and Fight", "Core", "A Test of Will", "Core");
            AddRelationship("Stand and Fight", "Core", "Northern Tracker", "Core");
            AddRelationship("Stand and Fight", "Core", "Dwarven Tomb", "Core");
            AddRelationship("Stand and Fight", "Core", "Unexpected Courage", "Core");
            AddRelationship("Stand and Fight", "Core", "Gandalf", "Core");
            
            AddRelationship("A Light in the Dark", "Core", "A Test of Will", "Core");
            AddRelationship("A Light in the Dark", "Core", "The Galadhrim's Greeting", "Core");
            AddRelationship("A Light in the Dark", "Core", "Dwarven Tomb", "Core");

            AddRelationship("Dwarven Tomb", "Core", "Northern Tracker", "Core");
            AddRelationship("Dwarven Tomb", "Core", "A Test of Will", "Core");
            AddRelationship("Dwarven Tomb", "Core", "Unexpected Courage", "Core");
            AddRelationship("Dwarven Tomb", "Core", "Gandalf", "Core");
            AddRelationship("Unexpected Courage", "Core", "A Test of Will", "Core");
            AddRelationship("Unexpected Courage", "Core", "Imladris Stargazer", "FoS");
            AddRelationship("Unexpected Courage", "Core", "Gandalf", "Core");

            AddRelationship("Erebor Hammersmith", "Core", "Gleowine", "Core");
            AddRelationship("Henamarth Riversong", "Core", "Erebor Hammersmith", "Core");
            AddRelationship("Henamarth Riversong", "Core", "Miner of the Iron Hills", "Core");
            AddRelationship("Henamarth Riversong", "Core", "Gildor Inglorion", "THoEM");
            AddRelationship("Henamarth Riversong", "Core", "Daeron's Runes", "FoS");
            AddRelationship("Henamarth Riversong", "Core", "Master of the Forge", "SaF");
            AddRelationship("Henamarth Riversong", "Core", "Gleowine", "Core");
            AddRelationship("Henamarth Riversong", "Core", "Warden of Healing", "TLD");
            AddRelationship("Miner of the Iron Hills", "Core", "Steward of Gondor", "Core");
            AddRelationship("Miner of the Iron Hills", "Core", "Erebor Hammersmith", "Core");
            AddRelationship("Miner of the Iron Hills", "Core", "Gleowine", "Core");
            AddRelationship("Miner of the Iron Hills", "Core", "Gandalf", "Core");
            AddRelationship("Miner of the Iron Hills", "Core", "Sneak Attack", "Core");
            AddRelationship("Gleowine", "Core", "Erebor Hammersmith", "Core");
            AddRelationship("Gleowine", "Core", "Gandalf", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Erebor Hammersmith", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Gleowine", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Secret Paths", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Forest Snare", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Protector of Lorien", "Core");
            AddRelationship("Radagast's Cunning", "Core", "A Burning Brand", "CatC");
            AddRelationship("Radagast's Cunning", "Core", "Faramir", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Sneak Attack", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Steward of Gondor", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Miner of the Iron Hills", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Gandalf", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Aragorn", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Theodred", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Denethor", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Guard of the Citadel", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Snowbourn Scout", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Daughter of the Nimrodel", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Self Preservation", "Core");
            AddRelationship("Radagast's Cunning", "Core", "Valiant Sacrifice", "Core");
            AddRelationship("Secret Paths", "Core", "Erebor Hammersmith", "Core");
            AddRelationship("Secret Paths", "Core", "Gleowine", "Core");
            AddRelationship("Secret Paths", "Core", "Radagast's Cunning", "Core");
            AddRelationship("Secret Paths", "Core", "Forest Snare", "Core");
            AddRelationship("Secret Paths", "Core", "Protector of Lorien", "Core");
            AddRelationship("Secret Paths", "Core", "A Burning Brand", "CatC");
            AddRelationship("Secret Paths", "Core", "Haldir of Lorien", "AJtR");
            AddRelationship("Secret Paths", "Core", "Faramir", "Core");
            AddRelationship("Secret Paths", "Core", "Sneak Attack", "Core");
            AddRelationship("Secret Paths", "Core", "Steward of Gondor", "Core");
            AddRelationship("Secret Paths", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Secret Paths", "Core", "Miner of the Iron Hills", "Core");
            AddRelationship("Secret Paths", "Core", "Gandalf", "Core");
            AddRelationship("Secret Paths", "Core", "Aragorn", "Core");
            AddRelationship("Secret Paths", "Core", "Theodred", "Core");
            AddRelationship("Secret Paths", "Core", "Denethor", "Core");
            AddRelationship("Secret Paths", "Core", "Guard of the Citadel", "Core");
            AddRelationship("Secret Paths", "Core", "Snowbourn Scout", "Core");
            AddRelationship("Secret Paths", "Core", "Daughter of the Nimrodel", "Core");
            AddRelationship("Secret Paths", "Core", "Valiant Sacrifice", "Core");
            AddRelationship("Secret Paths", "Core", "Rivendell Minstrel", "THfG");
            AddRelationship("Secret Paths", "Core", "Song of Kings", "THfG");
            AddRelationship("Forest Snare", "Core", "Denethor", "Core");
            AddRelationship("Forest Snare", "Core", "Erebor Hammersmith", "Core");
            AddRelationship("Forest Snare", "Core", "Miner of the Iron Hills", "Core");
            AddRelationship("Forest Snare", "Core", "Gleowine", "Core");
            AddRelationship("Forest Snare", "Core", "Faramir", "Core");
            AddRelationship("Forest Snare", "Core", "Sneak Attack", "Core");
            AddRelationship("Forest Snare", "Core", "Gandalf", "Core");
            AddRelationship("Forest Snare", "Core", "Steward of Gondor", "Core");
            AddRelationship("Forest Snare", "Core", "Radagast's Cunning", "Core");
            AddRelationship("Forest Snare", "Core", "Protector of Lorien", "Core");
            AddRelationship("Forest Snare", "Core", "Aragorn", "Core");
            AddRelationship("Forest Snare", "Core", "Theodred", "Core");
            AddRelationship("Forest Snare", "Core", "Guard of the Citadel", "Core");
            AddRelationship("Forest Snare", "Core", "Snowbourn Scout", "Core");
            AddRelationship("Forest Snare", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Forest Snare", "Core", "Daughter of the Nimrodel", "Core");
            AddRelationship("Protector of Lorien", "Core", "Steward of Gondor", "Core");
            AddRelationship("Protector of Lorien", "Core", "Gandalf", "Core");
            AddRelationship("Protector of Lorien", "Core", "Gleowine", "Core");
            AddRelationship("Protector of Lorien", "Core", "A Burning Brand", "CatC");
            AddRelationship("Protector of Lorien", "Core", "Erebor Hammersmith", "Core");
            AddRelationship("Self Preservation", "Core", "Steward of Gondor", "Core");
            AddRelationship("Self Preservation", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Self Preservation", "Core", "Gandalf", "Core");
            AddRelationship("Self Preservation", "Core", "Faramir", "Core");
            AddRelationship("Self Preservation", "Core", "Gleowine", "Core");
            AddRelationship("Self Preservation", "Core", "Erebor Hammersmith", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Sneak Attack", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Steward of Gondor", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Erebor Hammersmith", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Forest Snare", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Gandalf", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Gleowine", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Theodred", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Aragorn", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Snowbourn Scout", "Core");
            AddRelationship("Dunedain Mark", "THfG", "Celebrian's Stone", "Core");
            AddRelationship("Rivendell Minstrel", "THfG", "Steward of Gondor", "Core");
            AddRelationship("Rivendell Minstrel", "THfG", "Celebrian's Stone", "Core");
            AddRelationship("Rivendell Minstrel", "THfG", "Protector of Lorien", "Core");
            AddRelationship("Rivendell Minstrel", "THfG", "Song of Kings", "THfG");
            AddRelationship("Rivendell Minstrel", "THfG", "A Burning Brand", "CatC");
            AddRelationship("Rivendell Minstrel", "THfG", "Daughter of the Nimrodel", "Core");
            AddRelationship("Rivendell Minstrel", "THfG", "Erebor Hammersmith", "Core");
            AddRelationship("Rivendell Minstrel", "THfG", "Gandalf", "Core");
            AddRelationship("Rivendell Minstrel", "THfG", "Gleowine", "Core");

            AddRelationship("Westfold Horse-Breaker", "THfG", "Eowyn", "Core");
            AddRelationship("Westfold Horse-Breaker", "THfG", "A Test of Will", "Core");
            AddRelationship("Westfold Horse-Breaker", "THfG", "Unexpected Courage", "Core");
            AddRelationship("Westfold Horse-Breaker", "THfG", "Gandalf", "Core");
            AddRelationship("Winged Guardian", "THfG", "Gondorian Spearman", "Core");
            AddRelationship("Winged Guardian", "THfG", "Horn of Gondor", "Core");
            AddRelationship("Winged Guardian", "THfG", "Gandalf", "Core");
            AddRelationship("Winged Guardian", "THfG", "The Eagles Are Coming!", "THfG");
            AddRelationship("Winged Guardian", "THfG", "Vassal of the Windlord", "TDM");
            AddRelationship("Winged Guardian", "THfG", "Eagles of the Misty Mountains", "RtM");
            AddRelationship("Winged Guardian", "THfG", "Feint", "Core");
            AddRelationship("Frodo Baggins", "CatC", "A Test of Will", "Core");
            AddRelationship("Frodo Baggins", "CatC", "Unexpected Courage", "Core");
            AddRelationship("Frodo Baggins", "CatC", "Imladris Stargazer", "FoS");
            AddRelationship("Frodo Baggins", "CatC", "The Galadhrim's Greeting", "Core");
            AddRelationship("Frodo Baggins", "CatC", "Master of the Forge", "SaF");
            AddRelationship("Dunedain Warning", "CatC", "Sneak Attack", "Core");
            AddRelationship("Dunedain Warning", "CatC", "Steward of Gondor", "Core");
            AddRelationship("Dunedain Warning", "CatC", "Gandalf", "Core");
            AddRelationship("Dunedain Warning", "CatC", "Snowbourn Scout", "Core");
            AddRelationship("Dunedain Warning", "CatC", "Celebrian's Stone", "Core");
            AddRelationship("A Burning Brand", "CatC", "Gandalf", "Core");

            AddRelationship("Haldir of Lorien", "AJtR", "Erebor Hammersmith", "Core");
            AddRelationship("Haldir of Lorien", "AJtR", "Gleowine", "Core");
            AddRelationship("Haldir of Lorien", "AJtR", "Gildor Inglorion", "THoEM");
            AddRelationship("Haldir of Lorien", "AJtR", "Protector of Lorien", "Core");
            AddRelationship("Haldir of Lorien", "AJtR", "A Burning Brand", "CatC");
            AddRelationship("Ancient Mathom", "AJtR", "Northern Tracker", "Core");
            AddRelationship("Ancient Mathom", "AJtR", "A Test of Will", "Core");
            AddRelationship("Ancient Mathom", "AJtR", "Unexpected Courage", "Core");
            AddRelationship("Ancient Mathom", "AJtR", "Escort from Edoras", "AJtR");
            AddRelationship("Ancient Mathom", "AJtR", "Glorfindel", "FoS");
            AddRelationship("Ancient Mathom", "AJtR", "Imladris Stargazer", "FoS");
            AddRelationship("Ancient Mathom", "AJtR", "Light of Valinor", "FoS");
            AddRelationship("Ancient Mathom", "AJtR", "Gandalf", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "Hasty Stroke", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "A Test of Will", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "Unexpected Courage", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "Ancient Mathom", "AJtR");
            AddRelationship("Escort from Edoras", "AJtR", "Gandalf", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "West Road Traveller", "RtM");
            AddRelationship("Descendant of Thorondor", "THoEM", "Steward of Gondor", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Feint", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Winged Guardian", "THfG");
            AddRelationship("Descendant of Thorondor", "THoEM", "Radagast", "AJtR");
            AddRelationship("Descendant of Thorondor", "THoEM", "Vassal of the Windlord", "TDM");
            AddRelationship("Descendant of Thorondor", "THoEM", "Eagles of the Misty Mountains", "RtM");
            AddRelationship("Descendant of Thorondor", "THoEM", "Hail of Stones", "RtR");
            AddRelationship("Descendant of Thorondor", "THoEM", "Sneak Attack", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Horn of Gondor", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Gandalf", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Gondorian Spearman", "Core");
            AddRelationship("Gildor Inglorion", "THoEM", "Gleowine", "Core");
            AddRelationship("Gildor Inglorion", "THoEM", "Haldir of Lorien", "AJtR");
            AddRelationship("Gildor Inglorion", "THoEM", "Asfaloth", "FoS");
            AddRelationship("Gildor Inglorion", "THoEM", "Master of the Forge", "SaF");
            AddRelationship("Gildor Inglorion", "THoEM", "Protector of Lorien", "Core");
            AddRelationship("Gildor Inglorion", "THoEM", "A Burning Brand", "CatC");
            AddRelationship("Song of Travel", "THoEM", "Protector of Lorien", "Core");
            AddRelationship("Song of Travel", "THoEM", "A Burning Brand", "CatC");
            AddRelationship("Song of Travel", "THoEM", "Master of the Forge", "SaF");
            AddRelationship("Song of Travel", "THoEM", "A Test of Will", "Core");
            AddRelationship("Song of Travel", "THoEM", "Unexpected Courage", "Core");
            AddRelationship("The Riddermark's Finest", "THoEM", "A Test of Will", "Core");
            AddRelationship("The Riddermark's Finest", "THoEM", "Gandalf", "Core");
            AddRelationship("The Riddermark's Finest", "THoEM", "Elrond's Counsel", "TWitW");
            AddRelationship("The Riddermark's Finest", "THoEM", "Glorfindel", "FoS");
            AddRelationship("The Riddermark's Finest", "THoEM", "Light of Valinor", "FoS");
            AddRelationship("The Riddermark's Finest", "THoEM", "Eowyn", "Core");
            AddRelationship("The Riddermark's Finest", "THoEM", "Unexpected Courage", "Core");
            AddRelationship("Elfhelm", "TDM", "Hasty Stroke", "Core");
            AddRelationship("Elfhelm", "TDM", "A Test of Will", "Core");
            AddRelationship("Elfhelm", "TDM", "Unexpected Courage", "Core");
            AddRelationship("Elfhelm", "TDM", "Escort from Edoras", "AJtR");
            AddRelationship("Elfhelm", "TDM", "Zigil Miner", "KD");
            AddRelationship("Elfhelm", "TDM", "Arwen Undomiel", "TWitW");
            AddRelationship("Elfhelm", "TDM", "Glorfindel", "FoS");
            AddRelationship("Elfhelm", "TDM", "Imladris Stargazer", "FoS");
            AddRelationship("Elfhelm", "TDM", "Damrod", "HoN");
            AddRelationship("Elfhelm", "TDM", "Westfold Horse-Breaker", "THfG");
            AddRelationship("Elfhelm", "TDM", "Eowyn", "Core");
            AddRelationship("Elfhelm", "TDM", "Gandalf", "Core");
            AddRelationship("Elfhelm", "TDM", "The Riddermark's Finest", "THoEM");
            AddRelationship("Fast Hitch", "TDM", "Unexpected Courage", "Core");
            AddRelationship("Fast Hitch", "TDM", "A Burning Brand", "CatC");
            AddRelationship("Fast Hitch", "TDM", "Imladris Stargazer", "FoS");
            AddRelationship("Fast Hitch", "TDM", "Master of the Forge", "SaF");
            AddRelationship("Silvan Tracker", "TDM", "A Test of Will", "Core");
            AddRelationship("Silvan Tracker", "TDM", "Protector of Lorien", "Core");
            AddRelationship("Silvan Tracker", "TDM", "A Burning Brand", "CatC");
            AddRelationship("Silvan Tracker", "TDM", "Haldir of Lorien", "AJtR");
            AddRelationship("Silvan Tracker", "TDM", "Gildor Inglorion", "THoEM");
            AddRelationship("Silvan Tracker", "TDM", "Arwen Undomiel", "TWitW");
            AddRelationship("Silvan Tracker", "TDM", "Resourceful", "TWitW");
            AddRelationship("Silvan Tracker", "TDM", "Daeron's Runes", "FoS");
            AddRelationship("Silvan Tracker", "TDM", "Glorfindel", "FoS");
            AddRelationship("Silvan Tracker", "TDM", "Imladris Stargazer", "FoS");
            AddRelationship("Silvan Tracker", "TDM", "Master of the Forge", "SaF");
            AddRelationship("Silvan Tracker", "TDM", "Mirlonde", "TDF");
            AddRelationship("Silvan Tracker", "TDM", "Daughter of the Nimrodel", "Core");
            AddRelationship("Silvan Tracker", "TDM", "Elrond", "SaF");
            AddRelationship("Silvan Tracker", "TDM", "Mirkwood Runner", "RtM");
            AddRelationship("Song of Battle", "TDM", "Steward of Gondor", "Core");
            AddRelationship("Song of Battle", "TDM", "Sneak Attack", "Core");
            AddRelationship("Song of Battle", "TDM", "Feint", "Core");
            AddRelationship("Song of Battle", "TDM", "Horn of Gondor", "Core");
            AddRelationship("Song of Battle", "TDM", "Gandalf", "Core");
            
            AddRelationship("Vassal of the Windlord", "TDM", "Sneak Attack", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Steward of Gondor", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Gondorian Spearman", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Feint", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Gandalf", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Horn of Gondor", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Winged Guardian", "THfG");
            AddRelationship("Vassal of the Windlord", "TDM", "Eagles of the Misty Mountains", "RtM");
            
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Horn of Gondor", "Core");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Gandalf", "Core");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "The Eagles Are Coming!", "THfG");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Winged Guardian", "THfG");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Landroval", "AJtR");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Vassal of the Windlord", "TDM");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Support of the Eagles", "RtM");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Radagast", "AJtR");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Descendant of Thorondor", "THoEM");

            AddRelationship("Bifur", "KD", "Erebor Hammersmith", "Core");
            AddRelationship("Bifur", "KD", "Gleowine", "Core");
            AddRelationship("Bifur", "KD", "Ranger Spikes", "HoN");
            AddRelationship("Bifur", "KD", "Cram", "THOHaUH");
            AddRelationship("Bifur", "KD", "Miner of the Iron Hills", "Core");
            AddRelationship("Bifur", "KD", "Warden of Healing", "TLD");
            AddRelationship("Bifur", "KD", "Erebor Record Keeper", "KD");
            AddRelationship("Bifur", "KD", "Legacy of Durin", "TWitW");
            AddRelationship("Bifur", "KD", "Daeron's Runes", "FoS");
            AddRelationship("Bifur", "KD", "We Are Not Idle", "SaF");
            AddRelationship("Erebor Record Keeper", "KD", "Miner of the Iron Hills", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Lure of Moria", "RtR");
            AddRelationship("Erebor Record Keeper", "KD", "Legacy of Durin", "TWitW");
            AddRelationship("Erebor Record Keeper", "KD", "Daeron's Runes", "FoS");
            AddRelationship("Erebor Record Keeper", "KD", "We Are Not Idle", "SaF");
            AddRelationship("Erebor Record Keeper", "KD", "Fili", "THOHaUH");
            AddRelationship("Erebor Record Keeper", "KD", "Kili", "THOHaUH");
            AddRelationship("Erebor Record Keeper", "KD", "Gloin", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Erebor Hammersmith", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Longbeard Map-Maker", "CatC");
            AddRelationship("Erebor Record Keeper", "KD", "Bifur", "KD");
            AddRelationship("Erebor Record Keeper", "KD", "Longbeard Elder", "FoS");
            AddRelationship("Erebor Record Keeper", "KD", "Cram", "THOHaUH");
            AddRelationship("Narvi's Belt", "KD", "Erebor Record Keeper", "KD");
            AddRelationship("Narvi's Belt", "KD", "Bofur", "TRG");
            AddRelationship("Narvi's Belt", "KD", "Lure of Moria", "RtR");
            AddRelationship("Narvi's Belt", "KD", "Erebor Battle Master", "TLD");
            AddRelationship("Narvi's Belt", "KD", "We Are Not Idle", "SaF");
            AddRelationship("Narvi's Belt", "KD", "A Very Good Tale", "THOHaUH");
            AddRelationship("Narvi's Belt", "KD", "Fili", "THOHaUH");
            AddRelationship("Narvi's Belt", "KD", "Kili", "THOHaUH");
            AddRelationship("Narvi's Belt", "KD", "Balin", "THOtD");
            AddRelationship("Narvi's Belt", "KD", "King Under the Mountain", "THOtD");
            AddRelationship("Narvi's Belt", "KD", "Sneak Attack", "Core");
            AddRelationship("Narvi's Belt", "KD", "Gandalf", "Core");
            AddRelationship("Narvi's Belt", "KD", "Longbeard Elder", "FoS");
            AddRelationship("Narvi's Belt", "KD", "Cram", "THOHaUH");
            AddRelationship("Zigil Miner", "KD", "A Test of Will", "Core");
            AddRelationship("Zigil Miner", "KD", "Glorfindel", "FoS");
            AddRelationship("Zigil Miner", "KD", "Imladris Stargazer", "FoS");
            AddRelationship("Bofur", "TRG", "Zigil Miner", "KD");
            AddRelationship("Bofur", "TRG", "Lure of Moria", "RtR");
            AddRelationship("Bofur", "TRG", "Erebor Battle Master", "TLD");
            AddRelationship("Bofur", "TRG", "We Are Not Idle", "SaF");
            AddRelationship("Bofur", "TRG", "A Very Good Tale", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Fili", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Kili", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Balin", "THOtD");
            AddRelationship("Bofur", "TRG", "King Under the Mountain", "THOtD");
            AddRelationship("Bofur", "TRG", "Sneak Attack", "Core");
            AddRelationship("Bofur", "TRG", "Gandalf", "Core");
            AddRelationship("Bofur", "TRG", "Feint", "Core");
            AddRelationship("Bofur", "TRG", "A Test of Will", "Core");
            
            AddRelationship("Hail of Stones", "RtR", "Gondorian Spearman", "Core");
            AddRelationship("Hail of Stones", "RtR", "Blade of Gondolin", "Core");
            AddRelationship("Hail of Stones", "RtR", "Horn of Gondor", "Core");
            AddRelationship("Hail of Stones", "RtR", "The Eagles Are Coming!", "THfG");
            AddRelationship("Hail of Stones", "RtR", "Winged Guardian", "THfG");
            AddRelationship("Hail of Stones", "RtR", "Vassal of the Windlord", "TDM");
            AddRelationship("Hail of Stones", "RtR", "Hands Upon the Bow", "SaF");
            AddRelationship("Hail of Stones", "RtR", "Feint", "Core");
            AddRelationship("Hail of Stones", "RtR", "Descendant of Thorondor", "THoEM");
            
            AddRelationship("Lure of Moria", "RtR", "Miner of the Iron Hills", "Core");
            AddRelationship("Lure of Moria", "RtR", "Erebor Record Keeper", "KD");
            AddRelationship("Lure of Moria", "RtR", "Narvi's Belt", "KD");
            AddRelationship("Lure of Moria", "RtR", "Bofur", "TRG");
            AddRelationship("Lure of Moria", "RtR", "Legacy of Durin", "TWitW");
            AddRelationship("Lure of Moria", "RtR", "Erebor Battle Master", "TLD");
            AddRelationship("Lure of Moria", "RtR", "Daeron's Runes", "FoS");
            AddRelationship("Lure of Moria", "RtR", "We Are Not Idle", "SaF");
            AddRelationship("Lure of Moria", "RtR", "A Very Good Tale", "THOHaUH");
            AddRelationship("Lure of Moria", "RtR", "Fili", "THOHaUH");
            AddRelationship("Lure of Moria", "RtR", "Kili", "THOHaUH");
            AddRelationship("Lure of Moria", "RtR", "Balin", "THOtD");
            AddRelationship("Lure of Moria", "RtR", "King Under the Mountain", "THOtD");
            AddRelationship("Lure of Moria", "RtR", "Longbeard Orc Slayer", "Core");
            AddRelationship("Lure of Moria", "RtR", "Sneak Attack", "Core");
            AddRelationship("Lure of Moria", "RtR", "Longbeard Elder", "FoS");
            AddRelationship("Lure of Moria", "RtR", "Cram", "THOHaUH");
            
            AddRelationship("Rivendell Blade", "RtR", "Sneak Attack", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Steward of Gondor", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Feint", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Gandalf", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Vassal of the Windlord", "TDM");
            AddRelationship("Rivendell Blade", "RtR", "Light of Valinor", "FoS");
            AddRelationship("Rivendell Blade", "RtR", "Horn of Gondor", "Core");
            
            AddRelationship("Aragorn", "TWitW", "Steward of Gondor", "Core");
            AddRelationship("Aragorn", "TWitW", "Celebrian's Stone", "Core");
            AddRelationship("Aragorn", "TWitW", "Protector of Lorien", "Core");
            AddRelationship("Aragorn", "TWitW", "Rivendell Minstrel", "THfG");
            AddRelationship("Aragorn", "TWitW", "Song of Kings", "THfG");
            AddRelationship("Aragorn", "TWitW", "A Burning Brand", "CatC");
            AddRelationship("Aragorn", "TWitW", "Gildor Inglorion", "THoEM");
            AddRelationship("Aragorn", "TWitW", "Song of Travel", "THoEM");
            AddRelationship("Aragorn", "TWitW", "Sword that was Broken", "TWitW");
            AddRelationship("Aragorn", "TWitW", "Asfaloth", "FoS");
            AddRelationship("Aragorn", "TWitW", "Daeron's Runes", "FoS");
            AddRelationship("Aragorn", "TWitW", "Master of the Forge", "SaF");
            AddRelationship("Aragorn", "TWitW", "Ranger Spikes", "HoN");
            AddRelationship("Aragorn", "TWitW", "A Test of Will", "Core");
            
            AddRelationship("Arwen Undomiel", "TWitW", "A Test of Will", "Core");
            AddRelationship("Arwen Undomiel", "TWitW", "Unexpected Courage", "Core");
            AddRelationship("Arwen Undomiel", "TWitW", "Glorfindel", "FoS");
            AddRelationship("Arwen Undomiel", "TWitW", "Imladris Stargazer", "FoS");
            AddRelationship("Arwen Undomiel", "TWitW", "Light of Valinor", "FoS");
            AddRelationship("Arwen Undomiel", "TWitW", "Gandalf", "Core");
            AddRelationship("Arwen Undomiel", "TWitW", "Elrond's Counsel", "TWitW");
            
            AddRelationship("Elrond's Counsel", "TWitW", "A Test of Will", "Core");
            AddRelationship("Elrond's Counsel", "TWitW", "Gandalf", "Core");
            AddRelationship("Elrond's Counsel", "TWitW", "Arwen Undomiel", "TWitW");
            AddRelationship("Elrond's Counsel", "TWitW", "Glorfindel", "FoS");
            AddRelationship("Elrond's Counsel", "TWitW", "Light of Valinor", "FoS");
            AddRelationship("Elrond's Counsel", "TWitW", "Unexpected Courage", "Core");
            AddRelationship("Elrond's Counsel", "TWitW", "Imladris Stargazer", "FoS");
            
            AddRelationship("Legacy of Durin", "TWitW", "Miner of the Iron Hills", "Core");
            AddRelationship("Legacy of Durin", "TWitW", "Erebor Record Keeper", "KD");
            AddRelationship("Legacy of Durin", "TWitW", "Lure of Moria", "RtR");
            AddRelationship("Legacy of Durin", "TWitW", "Daeron's Runes", "FoS");
            AddRelationship("Legacy of Durin", "TWitW", "We Are Not Idle", "SaF");
            AddRelationship("Legacy of Durin", "TWitW", "Erebor Hammersmith", "Core");
            AddRelationship("Legacy of Durin", "TWitW", "Longbeard Map-Maker", "CatC");
            AddRelationship("Legacy of Durin", "TWitW", "Bifur", "KD");
            AddRelationship("Legacy of Durin", "TWitW", "Longbeard Elder", "FoS");
            
            AddRelationship("Resourceful", "TWitW", "Arwen Undomiel", "TWitW");
            AddRelationship("Resourceful", "TWitW", "Glorfindel", "FoS");
            AddRelationship("Resourceful", "TWitW", "Imladris Stargazer", "FoS");
            AddRelationship("Resourceful", "TWitW", "Light of Valinor", "FoS");
            AddRelationship("Resourceful", "TWitW", "Gleowine", "Core");
            AddRelationship("Resourceful", "TWitW", "Gildor Inglorion", "THoEM");
            
            AddRelationship("Sword that was Broken", "TWitW", "Steward of Gondor", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Celebrian's Stone", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Aragorn", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Faramir", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Sneak Attack", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Gandalf", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Errand-rider", "HoN");
            
            AddRelationship("Erebor Battle Master", "TLD", "Zigil Miner", "KD");
            AddRelationship("Erebor Battle Master", "TLD", "Bofur", "TRG");
            AddRelationship("Erebor Battle Master", "TLD", "Lure of Moria", "RtR");
            AddRelationship("Erebor Battle Master", "TLD", "We Are Not Idle", "SaF");
            AddRelationship("Erebor Battle Master", "TLD", "Fili", "THOHaUH");
            AddRelationship("Erebor Battle Master", "TLD", "Kili", "THOHaUH");
            AddRelationship("Erebor Battle Master", "TLD", "Sneak Attack", "Core");

            AddRelationship("Warden of Healing", "TLD", "Gleowine", "Core");
            AddRelationship("Warden of Healing", "TLD", "Gandalf", "Core");

            AddRelationship("Asfaloth", "FoS", "A Test of Will", "Core");
            AddRelationship("Asfaloth", "FoS", "Gleowine", "Core");
            AddRelationship("Asfaloth", "FoS", "Gildor Inglorion", "THoEM");
            AddRelationship("Asfaloth", "FoS", "Elrond's Counsel", "TWitW");
            AddRelationship("Asfaloth", "FoS", "Light of Valinor", "FoS");
            AddRelationship("Asfaloth", "FoS", "Master of the Forge", "SaF");
            AddRelationship("Asfaloth", "FoS", "Unexpected Courage", "Core");
            AddRelationship("Asfaloth", "FoS", "A Burning Brand", "CatC");
            AddRelationship("Asfaloth", "FoS", "Imladris Stargazer", "FoS");

            AddRelationship("Glorfindel", "FoS", "A Test of Will", "Core");
            AddRelationship("Glorfindel", "FoS", "Unexpected Courage", "Core");
            AddRelationship("Glorfindel", "FoS", "Arwen Undomiel", "TWitW");
            AddRelationship("Glorfindel", "FoS", "Imladris Stargazer", "FoS");
            AddRelationship("Glorfindel", "FoS", "Light of Valinor", "FoS");
            AddRelationship("Glorfindel", "FoS", "Gandalf", "Core");
            AddRelationship("Glorfindel", "FoS", "Gandalf", "THOHaUH");
            AddRelationship("Glorfindel", "FoS", "Elrond's Counsel", "TWitW");
            AddRelationship("Glorfindel", "FoS", "Asfaloth", "FoS");

            AddRelationship("Imladris Stargazer", "FoS", "A Test of Will", "Core");
            AddRelationship("Imladris Stargazer", "FoS", "Unexpected Courage", "Core");
            AddRelationship("Imladris Stargazer", "FoS", "Glorfindel", "FoS");
            AddRelationship("Imladris Stargazer", "FoS", "Light of Valinor", "FoS");
            AddRelationship("Imladris Stargazer", "FoS", "Gandalf", "Core");
            AddRelationship("Imladris Stargazer", "FoS", "Elrond's Counsel", "TWitW");
            
            AddRelationship("Light of Valinor", "FoS", "A Test of Will", "Core");
            AddRelationship("Light of Valinor", "FoS", "Unexpected Courage", "Core");
            AddRelationship("Light of Valinor", "FoS", "Arwen Undomiel", "TWitW");
            AddRelationship("Light of Valinor", "FoS", "Glorfindel", "FoS");
            AddRelationship("Light of Valinor", "FoS", "Imladris Stargazer", "FoS");
            AddRelationship("Light of Valinor", "FoS", "Gandalf", "Core");
            AddRelationship("Light of Valinor", "FoS", "Elrond's Counsel", "TWitW");
            
            AddRelationship("Longbeard Elder", "FoS", "Longbeard Orc Slayer", "Core");
            AddRelationship("Longbeard Elder", "FoS", "Lure of Moria", "RtR");
            AddRelationship("Longbeard Elder", "FoS", "We Are Not Idle", "SaF");
            AddRelationship("Longbeard Elder", "FoS", "Fili", "THOHaUH");
            AddRelationship("Longbeard Elder", "FoS", "Kili", "THOHaUH");
            AddRelationship("Longbeard Elder", "FoS", "Miner of the Iron Hills", "Core");
            AddRelationship("Longbeard Elder", "FoS", "Cram", "THOHaUH");
            AddRelationship("Longbeard Elder", "FoS", "Gloin", "Core");
            AddRelationship("Longbeard Elder", "FoS", "Erebor Hammersmith", "Core");
            AddRelationship("Longbeard Elder", "FoS", "Bifur", "KD");
            AddRelationship("Longbeard Elder", "FoS", "Erebor Record Keeper", "KD");
            AddRelationship("Longbeard Elder", "FoS", "Legacy of Durin", "TWitW");
            
            AddRelationship("Elrond", "SaF", "Gildor Inglorion", "THoEM");
            AddRelationship("Elrond", "SaF", "A Burning Brand", "CatC");
            AddRelationship("Elrond", "SaF", "Vilya", "SaF");
            AddRelationship("Elrond", "SaF", "Warden of Healing", "TLD");
            AddRelationship("Elrond", "SaF", "Imladris Stargazer", "FoS");

            AddRelationship("Hands Upon the Bow", "SaF", "Legolas", "Core");
            AddRelationship("Hands Upon the Bow", "SaF", "Feint", "Core");
            AddRelationship("Hands Upon the Bow", "SaF", "Blade of Gondolin", "Core");
            AddRelationship("Hands Upon the Bow", "SaF", "Gandalf", "Core");
            AddRelationship("Hands Upon the Bow", "SaF", "Foe-hammer", "THOHaUH");
           
            AddRelationship("Master of the Forge", "SaF", "Gleowine", "Core");
            AddRelationship("Master of the Forge", "SaF", "Asfaloth", "FoS");
            AddRelationship("Master of the Forge", "SaF", "Light of Valinor", "FoS");
            AddRelationship("Master of the Forge", "SaF", "A Burning Brand", "CatC");
            
            AddRelationship("Miruvor", "SaF", "Northern Tracker", "Core");
            AddRelationship("Miruvor", "SaF", "A Test of Will", "Core");
            AddRelationship("Miruvor", "SaF", "Unexpected Courage", "Core");
            AddRelationship("Miruvor", "SaF", "A Burning Brand", "CatC");
            AddRelationship("Miruvor", "SaF", "Elrond's Counsel", "TWitW");
            AddRelationship("Miruvor", "SaF", "Asfaloth", "FoS");
            AddRelationship("Miruvor", "SaF", "Imladris Stargazer", "FoS");
            AddRelationship("Miruvor", "SaF", "Light of Valinor", "FoS");
            AddRelationship("Miruvor", "SaF", "Master of the Forge", "SaF");
            AddRelationship("Miruvor", "SaF", "The Galadhrim's Greeting", "Core");
            
            AddRelationship("Peace, and Thought", "SaF", "Steward of Gondor", "Core");
            AddRelationship("Peace, and Thought", "SaF", "Gleowine", "Core");
            AddRelationship("Peace, and Thought", "SaF", "Gandalf", "Core");
            AddRelationship("Peace, and Thought", "SaF", "Warden of Healing", "TLD");
            AddRelationship("Peace, and Thought", "SaF", "Asfaloth", "FoS");
            
            AddRelationship("We Are Not Idle", "SaF", "Sneak Attack", "Core");
            AddRelationship("We Are Not Idle", "SaF", "Gandalf", "Core");
            AddRelationship("We Are Not Idle", "SaF", "Lure of Moria", "RtR");
            AddRelationship("We Are Not Idle", "SaF", "Longbeard Elder", "FoS");
            AddRelationship("We Are Not Idle", "SaF", "Cram", "THOHaUH");
            
            AddRelationship("Envoy of Pelargir", "HoN", "Faramir", "Core");
            AddRelationship("Envoy of Pelargir", "HoN", "Sneak Attack", "Core");
            AddRelationship("Envoy of Pelargir", "HoN", "Steward of Gondor", "Core");
            AddRelationship("Envoy of Pelargir", "HoN", "Gandalf", "Core");
            
            AddRelationship("Errand-rider", "HoN", "Faramir", "Core");
            AddRelationship("Errand-rider", "HoN", "Sneak Attack", "Core");
            AddRelationship("Errand-rider", "HoN", "Steward of Gondor", "Core");
            AddRelationship("Errand-rider", "HoN", "Gandalf", "Core");
            
            AddRelationship("Ithilien Tracker", "HoN", "Denethor", "Core");
            AddRelationship("Ithilien Tracker", "HoN", "Gandalf", "Core");
            AddRelationship("Ithilien Tracker", "HoN", "Warden of Healing", "TLD");
            AddRelationship("Ithilien Tracker", "HoN", "Ranger Spikes", "HoN");
            AddRelationship("Ithilien Tracker", "HoN", "A Burning Brand", "CatC");
            AddRelationship("Ithilien Tracker", "HoN", "Gleowine", "Core");
            
            AddRelationship("Ranger Spikes", "HoN", "Gleowine", "Core");
            AddRelationship("Ranger Spikes", "HoN", "Erebor Hammersmith", "Core");

            AddRelationship("Gondorian Shield", "TSF", "Denethor", "Core");
            AddRelationship("Gondorian Shield", "TSF", "Beregond", "HoN");
            AddRelationship("Gondorian Shield", "TSF", "Horn of Gondor", "Core");
            
            AddRelationship("A Very Good Tale", "THOHaUH", "Sneak Attack", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "Steward of Gondor", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "Gandalf", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("A Very Good Tale", "THOHaUH", "Balin", "THOtD");
            AddRelationship("A Very Good Tale", "THOHaUH", "King Under the Mountain", "THOtD");
            AddRelationship("A Very Good Tale", "THOHaUH", "Fili", "THOHaUH");
            AddRelationship("A Very Good Tale", "THOHaUH", "Kili", "THOHaUH");
            
            AddRelationship("Cram", "THOHaUH", "Sneak Attack", "Core");
            AddRelationship("Cram", "THOHaUH", "Gandalf", "Core");
            AddRelationship("Cram", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("Cram", "THOHaUH", "Erebor Hammersmith", "Core");

            AddRelationship("Fili", "THOHaUH", "Erebor Record Keeper", "KD");
            AddRelationship("Fili", "THOHaUH", "Narvi's Belt", "KD");
            AddRelationship("Fili", "THOHaUH", "Zigil Miner", "KD");
            AddRelationship("Fili", "THOHaUH", "Bofur", "TRG");
            AddRelationship("Fili", "THOHaUH", "Lure of Moria", "RtR");
            AddRelationship("Fili", "THOHaUH", "Erebor Battle Master", "TLD");
            AddRelationship("Fili", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("Fili", "THOHaUH", "A Very Good Tale", "THOHaUH");
            AddRelationship("Fili", "THOHaUH", "Kili", "THOHaUH");
            AddRelationship("Fili", "THOHaUH", "Balin", "THOtD");
            AddRelationship("Fili", "THOHaUH", "King Under the Mountain", "THOtD");
            AddRelationship("Fili", "THOHaUH", "Longbeard Elder", "FoS");
            AddRelationship("Fili", "THOHaUH", "Cram", "THOHaUH");

            AddRelationship("Foe-hammer", "THOHaUH", "Gondorian Spearman", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Feint", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Gandalf", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Hands Upon the Bow", "SaF");
            AddRelationship("Foe-hammer", "THOHaUH", "Bofur", "THOHaUH");

            AddRelationship("Gandalf", "THOHaUH", "Glorfindel", "FoS");
            AddRelationship("Gandalf", "THOHaUH", "Elrond's Counsel", "TWitW");
            AddRelationship("Gandalf", "THOHaUH", "The Galadhrim's Greeting", "Core");

            AddRelationship("Kili", "THOHaUH", "Erebor Record Keeper", "KD");
            AddRelationship("Kili", "THOHaUH", "Narvi's Belt", "KD");
            AddRelationship("Kili", "THOHaUH", "Zigil Miner", "KD");
            AddRelationship("Kili", "THOHaUH", "Bofur", "TRG");
            AddRelationship("Kili", "THOHaUH", "Lure of Moria", "RtR");
            AddRelationship("Kili", "THOHaUH", "Erebor Battle Master", "TLD");
            AddRelationship("Kili", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("Kili", "THOHaUH", "A Very Good Tale", "THOHaUH");
            AddRelationship("Kili", "THOHaUH", "Fili", "THOHaUH");
            AddRelationship("Kili", "THOHaUH", "Balin", "THOtD");
            AddRelationship("Kili", "THOHaUH", "King Under the Mountain", "THOtD");
            AddRelationship("Kili", "THOHaUH", "Longbeard Elder", "FoS");
            AddRelationship("Kili", "THOHaUH", "Cram", "THOHaUH");

            AddRelationship("Balin", "THOtD", "Steward of Gondor", "Core");
            AddRelationship("Balin", "THOtD", "Gandalf", "Core");
            AddRelationship("Balin", "THOtD", "We Are Not Idle", "SaF");
            AddRelationship("Balin", "THOtD", "Errand-rider", "HoN");
            AddRelationship("Balin", "THOtD", "Gaining Strength", "TSF");
            AddRelationship("Balin", "THOtD", "A Very Good Tale", "THOHaUH");
            AddRelationship("Balin", "THOtD", "King Under the Mountain", "THOtD");
            AddRelationship("Balin", "THOtD", "Narvi's Belt", "KD");
            AddRelationship("Balin", "THOtD", "Zigil Miner", "KD");
            AddRelationship("Balin", "THOtD", "Bofur", "TRG");
            AddRelationship("Balin", "THOtD", "Lure of Moria", "RtR");
            AddRelationship("Balin", "THOtD", "Erebor Battle Master", "TLD");
            AddRelationship("Balin", "THOtD", "Fili", "THOHaUH");
            AddRelationship("Balin", "THOtD", "Kili", "THOHaUH");
            
            AddRelationship("King Under the Mountain", "THOtD", "Steward of Gondor", "Core");
            AddRelationship("King Under the Mountain", "THOtD", "We Are Not Idle", "SaF");
            AddRelationship("King Under the Mountain", "THOtD", "A Very Good Tale", "THOHaUH");
            AddRelationship("King Under the Mountain", "THOtD", "Balin", "THOtD");
            AddRelationship("King Under the Mountain", "THOtD", "Narvi's Belt", "KD");
            AddRelationship("King Under the Mountain", "THOtD", "Zigil Miner", "KD");
            AddRelationship("King Under the Mountain", "THOtD", "Bofur", "TRG");
            AddRelationship("King Under the Mountain", "THOtD", "Lure of Moria", "RtR");
            AddRelationship("King Under the Mountain", "THOtD", "Erebor Battle Master", "TLD");
            AddRelationship("King Under the Mountain", "THOtD", "Fili", "THOHaUH");
            AddRelationship("King Under the Mountain", "THOtD", "Kili", "THOHaUH");
        }

        public IEnumerable<Card> All()
        {
            return cards.Values.ToList();
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
                .ToList()
                : cards.Values.ToList();

            if (model.CardType != CardType.None)
            {
                if (model.CardType == CardType.Player)
                {
                    results = results.Where(x => x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Attachment || x.CardType == CardType.Event).ToList();
                }
                else if (model.CardType == CardType.Character)
                {
                    results = results.Where(x => x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Objective_Ally || (x.CardType == CardType.Objective && x.HitPoints > 0)).ToList();
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

            if (model.Unique)
            {
                results = results.Where(x => x.IsUnique).ToList();
            }

            results = results.Take(maxResults).ToList();

            switch (model.Sort)
            {
                case Sort.Set_and_number:
                    return results.OrderBy(x => x.CardSet.Number).ThenBy(x => x.Number);
                case Sort.Alphabetical:
                    return results.OrderBy(x => x.Title);
                case Sort.Sphere_type_cost:
                    return results.OrderBy(x => x.Sphere).ThenBy(x => x.CardType).ThenBy(x => x.ResourceCost > 0 ? x.ResourceCost : x.ThreatCost);
                default:
                    return results;
            }
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