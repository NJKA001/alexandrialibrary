using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheBloodofGondor : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Blood of Gondor";
            Number = 20;
            SetType = Models.SetType.Adventure_Pack;

            Cards.Add(new Card() {
                ImageName = "M1885",
                Title = "Caldara",
                Id = "60725069-031c-4251-9b2c-3f368545e9ac",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                ThreatCost = 8,
                Willpower = 2,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor." },
                Text = "Action: Discard Caldara to put 1 Spirit ally from your discard pile into play for each other hero you control with a printed Spirit resource icon.",
                FlavorText = "...there is no purpose higher in the world as it now stands than the good of Gondor...\r\n-Denethor, The Return of the King",
                Quantity = 1,
                Number = 197
            });
            Cards.Add(new Card() {
                ImageName = "M1892",
                Title = "Anborn",
                Id = "ef8aec20-e0c9-4d02-9dea-20b6e7c2278b",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 4,
                Willpower = 1,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Text = "Action: Exhaust Anborn to return 1 Trap card from your discard pile to your hand.",
                FlavorText = "\"I sent my keenest huntsmen to seek him, but he slipped them, and they had no sight of him till now, save Anborn.\"\r\n-Faramir, The Two Towers",
                Quantity = 3,
                Number = 204
            });
            Cards.Add(new Card() {
                ImageName = "M1893",
                Title = "Poisoned Stakes",
                Id = "30e0c679-3ced-4862-a681-b67ceb8939d3",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Traits = new List<string>() { "Trap." },
                Text = "Play Poisoned Stakes into the staging area unattached.\r\nIf unattached, attach Poisoned Stakes to the next eligble enemy that enters the staging area.\r\nAt the end of each round, deal 2 damage to attached enemy.",
                Quantity = 3,
                Number = 205
            });
            Cards.Add(new Card() {
                ImageName = "M1891",
                Title = "Children of the Sea",
                Id = "65d3b334-df93-43c6-9525-12674bbb7f06",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Text = "Action: Choose a Silvan or Noldor ally you control. That ally gets +2 Willpower until the end of the phase. At the end of the phase, shuffle that ally into its owner's deck if it is still in play.",
                FlavorText = "\"For deep in the hears of all my kindred lies the sea-longing, which it is perilous to stir\"\r\n-Legolas, The Return of the King",
                Quantity = 3,
                Number = 203
            });
            Cards.Add(new Card() {
                ImageName = "M1889",
                Title = "The Hammer-stroke",
                Id = "cc2f73af-86c8-4d81-b706-14127adc0b37",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Text = "Encounter Action: Engage each enemy in play.",
                FlavorText = "..we have this honour: ever we bear the brunt of the chief hatred of the Dark Lord, for that hatred comes down out of the of the depths of time and over the deeps of the Sea.\r\n-Denethor, The Return of the King",
                Quantity = 3,
                Number = 201
            });
            Cards.Add(new Card() {
                ImageName = "M1887",
                Title = "Tome of Atanator",
                Id = "e88d7165-821c-4b04-9869-80ee1f33905c",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 4,
                Traits = new List<string>() { "Record." },
                Keywords = new List<string>() { "Attach to a Leadership hero." },
                Text = "Reduce the cost to play Tome of Atanator by 1 for each you control with the printed Leadership resource icon.\r\nAction: Discard Tome of Atanator to play any Leadership event card in your discard pile as if it were in your hand. Then, place that event on the bottom of your deck.",
                Quantity = 3,
                Number = 199
            });
            Cards.Add(new Card() {
                ImageName = "M1888",
                Title = "Guthlaf",
                Id = "06baca74-330a-4038-9387-32ce8657dd8f",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 3,
                Willpower = 1,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Rohan." },
                Text = "If there is at least 1 Rohan hero in play, lower the cost to play Guthlaf by 1.\r\nIf there is at least 1 Gondor hero in play, Guthlaf gains sentinel.",
                Quantity = 3,
                Number = 200
            });
            Cards.Add(new Card() {
                ImageName = "M1890",
                Title = "Emery",
                Id = "314eaf40-1554-4c1a-b643-1f4353e25633",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Willpower = 1,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Gondor." },
                Text = "Action: Discard the top 3 card of your deck to put Emery into play from your hand, under any player's control. Then, if any of the discarded cards have the Tactics, Lore or Leadership sphere, discard Emery.",
                Quantity = 3,
                Number = 202
            });
            Cards.Add(new Card() {
                ImageName = "M1886",
                Title = "Squire of the Citadel",
                Id = "d4f034b0-2444-4b61-a249-8d86e8856f7c",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Willpower = 0,
                Attack = 0,
                Defense = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Gondor." },
                Text = "Response: After Squire of the Citadel leaves play, add 1 resource to a Gondor hero's resource pool.",
                FlavorText = "As he spoke he struck a small silver gong that stood near his footstool, and at once servants came forward.\r\n-The Return of the King",
                Quantity = 3,
                Number = 198
            });
            Cards.Add(new Card() {
                ImageName = "M1894",
                Title = "Well-Equipped",
                Id = "0f027b32-f63f-4d37-8305-5ba2b059289d",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Discard the top 2 cards of your deck. You may attach 1 attachment card discarded by this effect to an eligible Dwarf character in play.",
                FlavorText = "Gimli the Dwarf alone wore openly a short shirt of steel-rings, for dwarves make light of burdens...\r\n-The Fellowship of the Ring",
                Number = 206
            });
            Cards.Add(new Card() {
                ImageName = "M1902",
                Title = "Southern Road",
                Id = "5a654416-4449-4d0c-add8-8b3ca04bb401",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "Ithilien.", " Road." },
                Text = "Travel: The first player must take 1 hidden card to travel here.",
                FlavorText = "This road, too, ran straight for a while, but soon it began to bend away southwards...-The Two Towers",
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 124
            });
            Cards.Add(new Card() {
                ImageName = "M1903",
                Title = "Western Road",
                Id = "01b1d42b-11a4-4030-988d-064673169fec",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "Ithilien.", " Road." },
                Text = "Western Road gets +5 quest points while it is in the staging area.\r\nTravel: The first player must take 1 hidden card to travel here.",
                FlavorText = "...he saw, beyond an arch of boughs, the road to Osgiliath running almost as straight as a streched ribbon down, down, into the West. -The Two Towers",
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 125
            });
            Cards.Add(new Card() {
                ImageName = "M1904",
                Title = "Northern Road",
                Id = "8eb1ca03-da9e-4e84-abf2-090804eeb1aa",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 3,
                Traits = new List<string>() { "Ithilien.", " Road." },
                Text = "Travel: The first player must take 1 hidden card to travel here.",
                FlavorText = "Behind them lay the road to the Morannon...\r\n-The Two Towers",
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 126
            });
            Cards.Add(new Card() {
                ImageName = "M1905",
                Title = "Eastern Road",
                Id = "9d58de28-c518-4888-8e91-a52bb5c3e663",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 2,
                Traits = new List<string>() { "Ithilien.", " Road." },
                Text = "Easter Road gets +5 quest points while it is in the staging area.\r\nTravel: The first player must take 2 hidden cards to travel here.",
                FlavorText = "Black and forbidding it loomed above them, darker than the dark sky behind. -The Two Towers",
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 127
            });
            Cards.Add(new Card() {
                ImageName = "M1906",
                Title = "The Dark Woods",
                Id = "eec35e8e-281e-4dc7-8a76-b4823de3cb71",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 2,
                Traits = new List<string>() { "Forest." },
                Keywords = new List<string>() { "Archery X." },
                Text = "X is the number of players in the game.\r\nForced: After The Dark Woods leaves play as an explored location, the first player discards 1 hidden card.",
                EncounterSet = "The Blood of Gondor",
                Quantity = 4,
                Number = 128
            });
            Cards.Add(new Card() {
                ImageName = "M1907",
                Title = "Lying in Wait",
                Id = "8affd994-f7f5-4341-8437-899f9a31b094",
                CardType = CardType.Treachery,
                Text = "When Revealed: The first player turns each hidden card in his play area faceup. Resolve the \"When Revealed\" effects of each encounter cards turned faceup by this effect. If a location is turned faceup, add it to the staging area.",
                FlavorText = "Up came the Orcs, yelling, with their long arms stretched out to seize him. -The Two Towers",
                EncounterSet = "The Blood of Gondor",
                Quantity = 4,
                Number = 129
            });
            Cards.Add(new Card() {
                ImageName = "M1908",
                Title = "Mordor Looms",
                Id = "fa3a740d-36b7-4fa9-b5d5-6d70aa50f078",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Each player takes 1 hidden card.",
                Shadow = "Shadow: If this attack destoys a characters, that character's controller turns each of his hidden cards faceup.",
                EncounterSet = "The Blood of Gondor",
                Quantity = 3,
                Number = 130
            });
            Cards.Add(new Card() {
                ImageName = "M1909",
                Title = "Conflict at the Crossroads",
                Id = "f4b35e95-7f32-490d-9591-ac9eb050bb7e",
                CardType = CardType.Treachery,
                Text = "When Revealed: Until the end of the phase, Orc enemies engaged with players add their Threat to the staging area. If no Orc enemies are engaged with players, Conflict at the Crossroads gains surge.",
                Shadow = "Shadow: Take 1 hidden card.",
                EncounterSet = "The Blood of Gondor",
                Quantity = 2,
                Number = 131
            });
            Cards.Add(new Card() {
                ImageName = "M1895",
                Title = "The Ambush - 1A",
                Id = "23768a0d-bf6c-4405-9b09-ea8fdbfded31",
                CardType = CardType.Quest,
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                QuestPoints = 11,
                Text = "Setup: Add the Crossroads and Black Númenorean to the staging area. Put the Faramir and Lord Alcaron objective allies into play. Shuffle the encounter deck and reveal 1 card from the encounter deck per player and add it to the staging area.",
                FlavorText = "After the assault on Osgiliath, Lord Alcaron urges Boromi to pursue the retreating enemy. Boromir agrees and gives permission to Faramir and his rangers. Grateful for you valiant efforts in Gondor's defense, Faramir invites you to join the hunt...",
                OppositeFlavorText = "You've pursued the enemy all the way to the Crossroads when a wicked horn blares in the distance. Within minutes, a host of orcs swarms out of the trees from all sides, led by a foreboding robed figure...",
                OppositeText = "At the beginning of the quest phase, each player takes 1 hidden card.\r\nAt the beginning of the combat phase, each player must turn each of his hidden cards faceup or take 1 hidden card.",
                Setup = "sstt",
                Number = 117
            });
            Cards.Add(new Card() {
                ImageName = "M1896",
                Title = "Captured! - 2A",
                Id = "d2bf00ad-2e1e-4537-b662-f6f20f9a1cfe",
                CardType = CardType.Quest,
                EncounterSet = "The Blood of Gondor",
                FlavorText = "Faramir and Lord Alcaron have been captured! Through the throng of warriors, you see their bloodied torn forms rushed eastward by a band of Uruks. You must move quickly to prevent the unthinkable! If there is to be any hope of rescuing your friends, you must fight your way out of the ambush.",
                Text = "When Revealed: Place Faramir and Lord Alcaron under Captured! (they are in play but players cannot gain control of them). Each player turns each of his hudden cards faceup.",
                Keywords = new List<string> { "Battle. (Characters use their Attack instead of Willpower when questing.)" },
                OppositeText = "At the beginning of the quest phase, each player takes 1 hidden card.\r\nIf a player has 5 hidden cards, he must turn them faceup.\r\nIf the players defeat this stage, they have won the game.",
                Quantity = 1,
                QuestPoints = 15,
                Number = 118
            });
            Cards.Add(new Card() {
                ImageName = "M1898",
                Title = "Orc Ambusher",
                Id = "8f106a70-0102-41ad-8752-fb5667748850",
                CardType = CardType.Enemy,
                EngagementCost = 10,
                Threat = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Orc.", " Mordor." },
                Text = "Forced: After Orc Ambusher engages you, discard 1 non-objective ally you control, if able.",
                Shadow = "Shadow: Attack enemy gets +1 Attack. (+3 Attack instead if undefended.)",
                EncounterSet = "The Blood of Gondor",
                Quantity = 4,
                Number = 120
            });
            Cards.Add(new Card() {
                ImageName = "M1899",
                Title = "Brutal Uruk",
                Id = "2d2e4f42-009d-4fde-ab42-3cd9351a4d0f",
                CardType = CardType.Enemy,
                EngagementCost = 20,
                Threat = 3,
                Attack = 4,
                Defense = 1,
                HitPoints = 5,
                Traits = new List<string>() { "Orc.", " Uruk.", " Mordor." },
                Text = "Forced: After Brutual Uruk attacks and destroys a character, that character's controller turns all hidden cards in his play area faceup. Deal a shadow card to each enemy turned faceup by this effect and resolve their attacks as normal.",
                EncounterSet = "The Blood of Gondor",
                Quantity = 3,
                Number = 121
            });
            Cards.Add(new Card() {
                ImageName = "M1900",
                Title = "Evil Crow",
                Id = "9d2926dc-ab14-492a-8f1e-b07c1b32b2d8",
                CardType = CardType.Enemy,
                EngagementCost = 5,
                Threat = 1,
                Attack = 1,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Creature." },
                Text = "Forced: When Evil Crow engages a player, that player takes 1 hidden card.",
                Shadow = "Shadow: Take 1 hidden card.",
                EncounterSet = "The Blood of Gondor",
                Quantity = 2,
                Number = 122
            });
            Cards.Add(new Card() {
                ImageName = "M1901",
                Title = "The Cross-roads",
                Id = "9b2302dc-3084-447f-8104-a4569fd26e38",
                IsUnique = true,
                CardType = CardType.Location,
                IsVariableThreat = true,
                QuestPoints = 2,
                Traits = new List<string>() { "Ithilien." },
                Keywords = new List<string>() { "The current quest card gains siege." },
                Text = "The current quest card gains siege.\r\nX is the number of players in the game.\r\nTravel: The first player must take 1 hidden card to travel here.",
                EncounterSet = "The Blood of Gondor",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 123
            });
            Cards.Add(new Card() {
                ImageName = "M1910",
                Title = "Faramir",
                Id = "ab2ac791-b266-43b2-b6ee-0d412a1100bc",
                IsUnique = true,
                CardType = CardType.Objective_Ally,
                Willpower = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Gondor.", " Noble.", " Ranger." },
                Keywords = new List<string>() { "The first player gains control of Faramir." },
                Text = "Combat Action: Exhaust Faramir to turn 1 hidden card faceup. If that card is an enemy, deal 3 damage to it.\r\nIf Faramir leaves play, the players have lost the game.",
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 132
            });
            Cards.Add(new Card() {
                ImageName = "M1897",
                Title = "Black Númenorean",
                NormalizedTitle = "Black Numenorean",
                Id = "6a4b0ba7-c80b-4580-bc43-d5369b0c44aa",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                IsVariableThreat = true,
                Attack = 5,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Mordor." },
                EncounterSet = "The Blood of Gondor",
                Text = "X is the total number of hidden cards in the game.\r\nForced: At the end of the round, each player must raise his threat by 1 for each hiddien card in his play area.",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 119
            });
            Cards.Add(new Card() {
                ImageName = "M1911",
                Title = "Lord Alcaron",
                Id = "fa4aaa97-c06d-42d5-b796-2f6200d9a404",
                IsUnique = true,
                CardType = CardType.Objective_Ally,
                Willpower = 1,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Noble." },
                Keywords = new List<string>() { "The first player gains control of Lord Alcaron." },
                Text = "Action: Exhaust Lord Alcaron to return an enemy engaged with you to the staging area.\r\nIf Lord Alcaron leaves play, the players have lost the game.",
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 133
            });
        }
    }
}
