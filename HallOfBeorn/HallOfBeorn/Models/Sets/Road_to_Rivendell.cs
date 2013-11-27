using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class RoadtoRivendell : CardSet
    {
        protected override void Initialize()
        {
            Name = "Road to Rivendell";
            SetType = Models.SetType.Adventure_Pack;

            Cards.Add(new Card() {
                ImageName = "M1399",
                Title = "Along the Misty Mountains - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801209c9001",
                CardType = CardType.Quest,
                EncounterSet = "Road to Rivendell",
                Quantity = 1,
                QuestPoints = 20,
                Setup = "t",
                Text = "Setup: Put Arwen Undomiel into play under the control of the first player. Shuffle the encounter deck. Reveal 1 card from the encounter deck per player, and add them to the staging area.",
                FlavorText = "This is a wild and perilous country, and it is dangerous to follow the roads. The mountains rise up on the right, impassively watching your slow trek among their foothills.",
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1401",
                Title = "Approaching Rivendell - 3A",
                Id = "51223bd0-ffd1-11df-a976-0801209c9003",
                CardType = CardType.Quest,
                EncounterSet = "Road to Rivendell",
                Quantity = 1,
                QuestPoints = 13,
                Text = "When Revealed: Reveal 1 card from the encounter deck per player, and add it to the staging area.\r\nCharacters cannot be healed.\r\nIf the players defeat this stage, they have won the game.",
                FlavorText = "Orcs and other creatures have hounded you since fighting your way free of the orc outpost. Soon you will reach the safety of Rivendell's borders, but supplies have dwindled and you are dead weary from sleepless nights of keeping watch, as dark forms shadow your camp.",
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1402",
                Title = "Arwen Undomiel",
                Id = "51223bd0-ffd1-11df-a976-0801209c9005",
                CardType = CardType.Objective,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Noldor.", " Noble.", " Ally." },
                Quantity = 1,
                IsUnique = true,
                Attack = 0,
                Defense = 1,
                Willpower = 2,
                HitPoints = 2,
                Text = "The first player gains control of Arwen Undomiel, as an ally.Response: After Arwen Undomiel exhausts, choose a hero. Add 1 resource to that hero's resource pool.If Arwen Undomiel leaves play, the players are defeated.",
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1406",
                Title = "Barren Hills",
                Id = "51223bd0-ffd1-11df-a976-0801209c9006",
                CardType = CardType.Location,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Hills." },
                Quantity = 2,
                Text = "While Barren Hills is the active location, ignore ambush.",
                Shadow = "Shadow: Return attacking enemy to the staging area after it attacks.",
                Threat = 2,
                QuestPoints = 4,
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1396",
                Title = "Bombur",
                Id = "51223bd0-ffd1-11df-a976-0801209c9007",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 3,
                IsUnique = true,
                Attack = 0,
                Defense = 1,
                Willpower = 0,
                HitPoints = 3,
                Text = "Action: Exhaust Bombur to choose a location. That location gets -1 Threat until the end of the phase. (That location does not contribute its Threat instead if it is an Underground location.)",
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1412",
                Title = "Crebain",
                Id = "51223bd0-ffd1-11df-a976-0801209c9008",
                CardType = CardType.Enemy,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Creature." },
                Quantity = 3,
                EngagementCost = 35,
                Attack = 0,
                Defense = 0,
                HitPoints = 3,
                Text = "While Crebain is in the staging area, encounter card effects cannot be canceled.",
                Shadow = "Shadow: Return attacking enemy to the staging area after it attacks.",
                Keywords = new List<string>() { "Surge." },
                Threat = 2,
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1390",
                Title = "Dunedain Wanderer",
                Id = "51223bd0-ffd1-11df-a976-0801209c9009",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Dunedain.", " Ranger." },
                Quantity = 3,
                ResourceCost = 5,
                Attack = 2,
                Defense = 2,
                Willpower = 1,
                HitPoints = 2,
                Keywords = new List<string>() { "Ranged.", " Sentinel.", " Secrecy 3." },
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1389",
                Title = "Elladan",
                Id = "51223bd0-ffd1-11df-a976-0801209c9010",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Noldor.", " Noble.", " Ranger." },
                Quantity = 1,
                ThreatCost = 10,
                IsUnique = true,
                Attack = 1,
                Defense = 2,
                Willpower = 2,
                HitPoints = 4,
                Text = "While Elrohir is in play, Elladan gets +2 Attack.Response: After Elladan is declared as an attacker, pay 1 resource from his resource pool to ready him.",
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1408",
                Title = "Followed by Night",
                Id = "51223bd0-ffd1-11df-a976-0801209c9011",
                CardType = CardType.Treachery,
                EncounterSet = "Road to Rivendell",
                Quantity = 3,
                Text = "When Revealed: The first player (choose 1): deals 1 damage to all allies in play and Followed by Night gains surge, or all enemies engaged with players make an immediate attack, if able.",
                Shadow = "Shadow: Return attacking enemy to the staging area after it attacks.",
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1404",
                Title = "Goblin Gate",
                Id = "51223bd0-ffd1-11df-a976-0801209c9012",
                CardType = CardType.Location,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Gate." },
                Quantity = 1,
                Text = "While Goblin Gate is the active location, the first enemy revealed from the encounter deck each round gains ambush. If that enemy engages a player, it makes an immediate attack (deal and resolve a shadow card).",
                Threat = 5,
                QuestPoints = 4,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1410",
                Title = "Goblin Taskmaster",
                Id = "51223bd0-ffd1-11df-a976-0801209c9013",
                CardType = CardType.Enemy,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 4,
                EngagementCost = 27,
                Attack = 2,
                Defense = 2,
                HitPoints = 4,
                Text = "Forced: After Goblin Taskmaster engages a player, that player deals 2 damage to 1 character he controls.",
                Keywords = new List<string>() { "Ambush." },
                Threat = 2,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1393",
                Title = "Hail of Stones",
                Id = "51223bd0-ffd1-11df-a976-0801209c9014",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Exhaust X characters to deal X damage to an enemy in the staging area.",
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1391",
                Title = "Lure of Moria",
                Id = "51223bd0-ffd1-11df-a976-0801209c9015",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Quantity = 3,
                ResourceCost = 3,
                Text = "Action: Ready all Dwarf characters.",
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1409",
                Title = "Orc Ambush",
                Id = "51223bd0-ffd1-11df-a976-0801209c9016",
                CardType = CardType.Treachery,
                EncounterSet = "Road to Rivendell",
                Quantity = 2,
                Text = "When Revealed: All Orc enemies in the staging area engage the first player. If there are no Orc enemies in the staging area, return all Orc enemies in the encounter discard pile to the staging area, if able.",
                Keywords = new List<string>() { "Surge." },
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1400",
                Title = "Orc Outpost - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801209c9017",
                CardType = CardType.Quest,
                EncounterSet = "Road to Rivendell",
                Quantity = 1,
                QuestPoints = 7,
                Number = 15,
                Text = "When Revealed: Search the encounter deck and discard pile for Goblin Gate and add it to the staging area, if able. Then, if there is no active location, Goblin Gate becomes the active location.",
                FlavorText = "Heavy rain drives you to seek shelter among the caves of the mountains. They are dry, and the fire you start seeps into your bones and restores your spirit. Your eyes are heavy when teh soft clatter of falling pebbles reaches your ears. Perhaps you are not alone.",
            });
            Cards.Add(new Card() {
                ImageName = "M1411",
                Title = "Orc Raiders",
                Id = "51223bd0-ffd1-11df-a976-0801209c9019",
                CardType = CardType.Enemy,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Orc." },
                Quantity = 3,
                EngagementCost = 21,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Text = "Forced: After Orc Raiders engages a player, that player discards 2 attachments he controls, if able.",
                Keywords = new List<string>() { "Ambush." },
                Threat = 1,
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1397",
                Title = "Out of the Wild",
                Id = "51223bd0-ffd1-11df-a976-0801209c9020",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 3,
                Text = "Action: Search the top 5 cards of the encounter deck for any 1 non-objective card worth no victory points and add it to your victory display. Shuffle the encounter deck.",
                Keywords = new List<string>() { "Secrecy 2." },
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1405",
                Title = "Pathless Country",
                Id = "51223bd0-ffd1-11df-a976-0801209c9021",
                CardType = CardType.Location,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Hills." },
                Quantity = 3,
                Text = "Forced: After at least 1 progress token is placed on Pathless Country, remove 1 progress token from it.",
                Shadow = "Shadow: Deal 1 damage to each ally in play.",
                Threat = 3,
                QuestPoints = 5,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1394",
                Title = "Rider of the Mark",
                Id = "51223bd0-ffd1-11df-a976-0801209c9022",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Rohan." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 2,
                HitPoints = 2,
                Text = "Action: Spend 1 Spirit resource to give control of Rider of the Mark to another player. (Limit once per round.)Response: After Rider of the Mark changes control, discard a shadow card dealt to an enemy you are engaged with.",
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1392",
                Title = "Rivendell Blade",
                Id = "51223bd0-ffd1-11df-a976-0801209c9023",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Item.", " Weapon." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "When attached character attacks an enemy, that enemy gets -2 Defense until the end of the phase.",
                Keywords = new List<string>() { "Attach to a Noldor or Silvan character.", " Restricted." },
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1403",
                Title = "Ruined Road",
                Id = "51223bd0-ffd1-11df-a976-0801209c9024",
                CardType = CardType.Location,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Road." },
                Quantity = 2,
                Text = "Response: After you travel to Ruined Road, the first player places 2 progress tokens on it or readies 1 hero he controls.",
                Shadow = "Shadow: Return attacking enemy to the staging area after it attacks.",
                Threat = 1,
                QuestPoints = 5,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1407",
                Title = "Sleeping Sentry",
                Id = "51223bd0-ffd1-11df-a976-0801209c9025",
                CardType = CardType.Treachery,
                EncounterSet = "Road to Rivendell",
                Quantity = 2,
                Text = "When Revealed: Deal 1 damage to each exhausted character. Then, exhaust all ready characters.",
                Shadow = "Shadow: Defending player must discard all exhausted characters he controls.",
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1395",
                Title = "Song of Eärendil",
                NormalizedTitle = "Song of Earendil",
                Id = "51223bd0-ffd1-11df-a976-0801209c9026",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Song." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Response: After Song of Earendil enters play, draw 1 card.Response: After another player raises his threat, raise your threat by 1 to reduce that player's threat by 1.",
                Keywords = new List<string>() { "Attach to a Ê hero." },
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1398",
                Title = "The End Comes",
                Id = "51223bd0-ffd1-11df-a976-0801209c9027",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Response: After a Dwarf character leaves play, shuffle the encounter discard pile back into the encounter deck.",
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1413",
                Title = "Wild Bear",
                Id = "51223bd0-ffd1-11df-a976-0801209c9028",
                CardType = CardType.Enemy,
                EncounterSet = "Road to Rivendell",
                Traits = new List<string>() { "Creature." },
                Quantity = 3,
                EngagementCost = 34,
                Attack = 2,
                Defense = 3,
                HitPoints = 5,
                Text = "Forced: After Wild Bear engages a player, it makes an immediate attack.",
                Keywords = new List<string>() { "Ambush." },
                Threat = 0,
                Number = 25
            });
        }
    }
}
