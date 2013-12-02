using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheLongDark : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Long Dark";
            Number = 12;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "Dwarrowdelf";

            Cards.Add(new Card() {
                ImageName = "M1451",
                Title = "Abandoned Mine",
                Id = "51223bd0-ffd1-11df-a976-0801211c9001",
                CardType = CardType.Location,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Underground.", " Dark." },
                Quantity = 3,
                Text = "Lost: Return the top 2 Goblin enemies in the encounter discard pile to the staging area, if able.",
                Threat = 3,
                QuestPoints = 3,
                VictoryPoints = 0,
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1459",
                Title = "Cave Spider",
                Id = "51223bd0-ffd1-11df-a976-0801211c9002",
                CardType = CardType.Enemy,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Spider.", " Creature." },
                Quantity = 3,
                EngagementCost = 31,
                Attack = 2,
                Defense = 1,
                HitPoints = 4,
                Text = "When Revealed: The first player draws 1 card. Then, that player must choose and discard 4 cards from his hand, if able.Forced: After Cave Spider engages a player, that player must choose and discard 1 card from his hand, if able.",
                Threat = 3,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1449",
                Title = "Continuing Eastward - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801211c9003",
                CardType = CardType.Quest,
                EncounterSet = "The Long Dark",
                Quantity = 1,
                QuestPoints = 17,
                FlavorText = "Time carries no weight in the darkness, and the hours creep by with no end in sight. The number of Orcs in the mines increase as you head toward the East-gate, but there appears to be little real organization within their ranks. You press onward!",
                Text = "When Revealed: The first player makes a locate test. If this test is failed, reveal cards from the encounter deck equal to the number of players in the game and add them to the staging area. Then, trigger all \"Lost:\" effects in play.\r\nIf the players quest unsuccessfully, trigger all \"Lost:\" effects in play.\r\nIf the players defeat this stage, they have won the game.",
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1450",
                Title = "Durin's Greaves",
                Id = "51223bd0-ffd1-11df-a976-0801211c9005",
                CardType = CardType.Objective,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Artifact.", " Armour." },
                Quantity = 1,
                IsUnique = true,
                Text = "When Revealed: The first player attaches Durin's Greaves to a hero of his choice as an attachment.Attached hero gets +1 Defense.",
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1452",
                Title = "Dwarven Forge",
                Id = "51223bd0-ffd1-11df-a976-0801211c9006",
                CardType = CardType.Location,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Underground.", " Dark." },
                Quantity = 2,
                Text = "Lost: Each player must choose and discard 1 card from his hand.",
                Threat = 2,
                QuestPoints = 4,
                VictoryPoints = 0,
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1441",
                Title = "Erebor Battle Master",
                Id = "51223bd0-ffd1-11df-a976-0801211c9007",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Dwarf.", " Warrior." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 0,
                HitPoints = 2,
                Text = "Erebor Battle Master gets +1 Attack for each other Dwarf character you control.",
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1439",
                Title = "Erestor",
                Id = "51223bd0-ffd1-11df-a976-0801211c9008",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Noldor." },
                Quantity = 3,
                ResourceCost = 4,
                IsUnique = true,
                Attack = 0,
                Defense = 1,
                Willpower = 2,
                HitPoints = 3,
                Text = "Action: Choose and discard 1 card from your hand to draw 1 card. (Limit once per round.)",
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1444",
                Title = "Ever My Heart Rises",
                Id = "51223bd0-ffd1-11df-a976-0801211c9009",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Condition." },
                Quantity = 3,
                ResourceCost = 0,
                Text = "Response: After you travel to a Mountain or Underground location, ready attached character and reduce your threat by 1.",
                Keywords = new List<string>() { "Attach to a Dwarf character." },
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1455",
                Title = "Fatigue",
                Id = "51223bd0-ffd1-11df-a976-0801211c9010",
                CardType = CardType.Treachery,
                EncounterSet = "The Long Dark",
                Quantity = 2,
                Text = "When Revealed: Each player must exhaust 1 character he controls, if able. Then, if any player controls no unexhausted characters, Fatigue gains surge.",
                Shadow = "Shadow: The defending player must exhaust 1 character he controls, if able.",
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1456",
                Title = "Foul Air",
                Id = "51223bd0-ffd1-11df-a976-0801211c9011",
                CardType = CardType.Treachery,
                EncounterSet = "The Long Dark",
                Quantity = 4,
                Text = "When Revealed: The first player makes a locate test. If this test is failed, deal 2 damage to all characters and trigger all 'Lost:' effects in play.",
                VictoryPoints = 0,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1440",
                Title = "Fresh Tracks",
                Id = "51223bd0-ffd1-11df-a976-0801211c9012",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Response: After an enemy is added to the staging area, deal 1 damage to that enemy. Players ignore that enemy while making engagement checks this round.",
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1457",
                Title = "Gathering Ground",
                Id = "51223bd0-ffd1-11df-a976-0801211c9013",
                CardType = CardType.Treachery,
                EncounterSet = "The Long Dark",
                Quantity = 1,
                Text = "When Revealed: Attach this card to a location in the staging area with the highest combined threat and remaining quest points. (Counts as a Condition attachment with the text: 'Each enemy revealed from the encounter deck gains surge.')",
                VictoryPoints = 0,
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1460",
                Title = "Goblin Sneak",
                Id = "51223bd0-ffd1-11df-a976-0801211c9014",
                CardType = CardType.Enemy,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 4,
                EngagementCost = 15,
                Attack = 1,
                Defense = 1,
                HitPoints = 2,
                Text = "Forced: After Goblin Sneak engages a player, discard the top card of the encounter deck. If it is a treachery card, Goblin Sneak engages the next player, if able.",
                Shadow = "Shadow: Add Goblin Sneak to the staging area.",
                Threat = 2,
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1461",
                Title = "Goblin Warlord",
                Id = "51223bd0-ffd1-11df-a976-0801211c9015",
                CardType = CardType.Enemy,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 1,
                EngagementCost = 39,
                Attack = 3,
                Defense = 3,
                HitPoints = 5,
                Text = "Lost: Each player must choose and discard 1 ally he controls from play, if able.",
                Shadow = "Shadow: Trigger all 'Lost:' effects in play.",
                Threat = 4,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1438",
                Title = "Háma",
                NormalizedTitle = "Hama",
                Id = "51223bd0-ffd1-11df-a976-0801211c9016",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Rohan.", " Warrior." },
                Quantity = 1,
                ThreatCost = 9,
                IsUnique = true,
                Attack = 3,
                Defense = 1,
                Willpower = 1,
                HitPoints = 4,
                Text = "Response: After Hama is declared as an attacker, return a Tactics event from your discard pile to your hand. Then, choose and discard 1 card from your hand.",
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1448",
                Title = "Journey in the Black Pit - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801211c9017",
                CardType = CardType.Quest,
                EncounterSet = "The Long Dark",
                Quantity = 1,
                QuestPoints = 13,
                Setup = "t",
                Text = "Setup: The first player attaches Cave Torch to a hero of his choice.",
                OppositeText = "When Revealed: Discard cards from the top of the encounter deck until you discard X locations, where X is one less than the number of players in the game (minimum 1). Add those locations to the staging area, and shuffle the other discarded cards back into the encounter deck.\r\nEach location gets +1 threat. If the players quest unsuccessfully, trigger all \"Lost:\" effects in play.",
                FlavorText = "Your party is scouting the Mines of Moria, searching for signs of Orcs. Dark tunnels and twisting passages spread out in all directions, a labyrinthine maze that you could wander in forever if you take the wrong path.",
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1447",
                Title = "Love of Tales",
                Id = "51223bd0-ffd1-11df-a976-0801211c9019",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Condition." },
                Quantity = 3,
                ResourceCost = 0,
                Text = "Response: After a Song card is played, add 1 resource to attached hero's resource pool.",
                Keywords = new List<string>() { "Attach to a Lore hero.", " Limit 1 per hero." },
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1443",
                Title = "Out of Sight",
                Id = "51223bd0-ffd1-11df-a976-0801211c9020",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Quantity = 3,
                ResourceCost = 5,
                Text = "Action: Enemies engaged with you cannot attack you this phase.",
                Keywords = new List<string>() { "Secrecy 3." },
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1442",
                Title = "Ring Mail",
                Id = "51223bd0-ffd1-11df-a976-0801211c9021",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Item.", " Armor." },
                Quantity = 3,
                ResourceCost = 2,
                Text = "Attached character gets +1 hit point and +1 Defense.",
                Keywords = new List<string>() { "Attach to a Dwarf or Hobbit character.", " Restricted." },
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "Rock Adder",
                Title = "Rock Adder",
                Id = "51223bd0-ffd1-11df-a976-0801211c9022",
                CardType = CardType.Enemy,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Creature." },
                Quantity = 3,
                EngagementCost = 20,
                Attack = 3,
                Defense = 0,
                HitPoints = 3,
                Text = "Rock Adder cannot be attacked unless it has dealt at least 1 damage this round.",
                Shadow = "Shadow: If this attack is undefended, the defending player must discard 1 character he controls from play.",
                Threat = 1,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1453",
                Title = "Silent Caverns",
                Id = "51223bd0-ffd1-11df-a976-0801211c9023",
                CardType = CardType.Location,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Underground." },
                Quantity = 2,
                Text = "Lost: Exhaust all characters.",
                Threat = 1,
                QuestPoints = 3,
                VictoryPoints = 0,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1454",
                Title = "Twisting Passage",
                Id = "51223bd0-ffd1-11df-a976-0801211c9024",
                CardType = CardType.Location,
                EncounterSet = "The Long Dark",
                Traits = new List<string>() { "Underground.", " Dark." },
                Quantity = 2,
                Text = "Forced: Before placing progress tokens on Twisting Passage, the first player must make a locate test. If this test is failed, do not place any progress tokens on Twisting Passage and trigger all 'Lost:' effects in play.",
                Threat = 3,
                QuestPoints = 5,
                VictoryPoints = 0,
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1458",
                Title = "Vast and Intricate",
                Id = "51223bd0-ffd1-11df-a976-0801211c9025",
                CardType = CardType.Treachery,
                EncounterSet = "The Long Dark",
                Quantity = 2,
                Text = "When Revealed: The first player makes a locate test. If this test is failed, raise each player's threat by 7, remove all progress tokens from play, and trigger all 'Lost:' effects in play.",
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1445",
                Title = "Warden of Healing",
                Id = "51223bd0-ffd1-11df-a976-0801211c9026",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Gondor.", " Healer." },
                Quantity = 3,
                ResourceCost = 2,
                Attack = 0,
                Defense = 1,
                Willpower = 1,
                HitPoints = 1,
                Text = "Action: Exhaust Warden of Healing to heal 1 damage on up to 2 different characters. Then, you may pay 2 Lore resources to ready Warden of Healing.",
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1446",
                Title = "Word of Command",
                Id = "51223bd0-ffd1-11df-a976-0801211c9027",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Exhaust an Istari character to search your deck for 1 card and add it to your hand. Shuffle your deck.",
                Number = 25
            });
        }
    }
}
