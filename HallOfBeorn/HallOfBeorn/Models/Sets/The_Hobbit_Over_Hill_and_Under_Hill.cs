using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheHobbitOverHillandUnderHill : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Hobbit: Over Hill and Under Hill";
            Number = 1001;
            SetType = Models.SetType.Saga_Expansion;

            Cards.Add(new Card() {
                ImageName = "M1587",
                Title = "A Deep Sack",
                Id = "51223bd0-ffd1-11df-a976-1801204c9001",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Sack." },
                Quantity = 1,
                Text = "When Sacked: Attach to the character (excluding Gandalf) with the highest printed Willpower without a Sack attached.Attached character cannot ready, attack, defend, commit to quests, or trigger effects. If this Sack card is removed, shuffle it into the sack deck.",
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1588",
                Title = "A Foul Sack",
                Id = "51223bd0-ffd1-11df-a976-1801204c9002",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Sack." },
                Quantity = 1,
                Text = "When Sacked: Attach to the character (excluding Gandalf) with the most attachments without a Sack attached.Attached character cannot ready, attack, defend, commit to quests, or trigger effects. If this Sack card is removed, shuffle it into the sack deck.",
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1586",
                Title = "A Large Sack",
                Id = "51223bd0-ffd1-11df-a976-1801204c9003",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Sack." },
                Quantity = 1,
                Text = "When Sacked: Attach to the character (excluding Gandalf) with the highest printed hit points without a Sack attached.Attached character cannot ready, attack, defend, commit to quests, or trigger effects. If this Sack card is removed, shuffle it into the sack deck.",
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1591",
                Title = "A Nice Pickle",
                Id = "51223bd0-ffd1-11df-a976-1801204c9004",
                CardType = CardType.Treachery,
                EncounterSet = "We Must Away, Ere Break of Day",
                Quantity = 2,
                Text = "When Revealed: Place the top X cards of the encounter discard pile on the bottom of the encounter deck. X is equal to twice the number of players in the game.",
                Shadow = "Shadow: Shuffle this card into the encounter deck.",
                Keywords = new List<string>() { "Doomed 1." },
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1565",
                Title = "A Short Rest - 1A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9005",
                CardType = CardType.Quest,
                EncounterSet = "Over the Misty Mountains Grim",
                Quantity = 1,
                QuestPoints = 0,
                Setup = "s",
                Text = "Setup: Shuffle the Over the Misty Mountains Grim and Western Lands encounter sets into one encounter deck and make it the active encounter deck. Then, shuffle the The Great Goblin and Misty Mountain Goblins encounter sets into a second encounter deck and set it aside, inactive.",
                FlavorText = "After a refreshing stay in the House of Elrond, Bilbo and his companions resumed their quest for the Lonely Mountain. But to reach Erebor, they first had to climb the high pass over the Misty Mountains.",
                OppositeText = "When Revealed: Each player may search his deck for 1 treasure card and add it to his hand, then shuffle his deck. Advance to stage 2A.",
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1583",
                Title = "A Smelly Sack",
                Id = "51223bd0-ffd1-11df-a976-1801204c9007",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Sack." },
                Quantity = 1,
                Text = "When Sacked: Attach to the ally (excluding Gandalf) with the highest printed cost without a Sack attached.Attached character cannot ready, attack, defend, commit to quests, or trigger effects. If this Sack card is removed, shuffle it into the sack deck.",
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1584",
                Title = "A Strong Sack",
                Id = "51223bd0-ffd1-11df-a976-1801204c9008",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Sack." },
                Quantity = 1,
                Text = "When Sacked: Attach to the character (excluding Gandalf) with the highest printed Attack without a Sack attached.Attached character cannot ready, attack, defend, commit to quests, or trigger effects. If this Sack card is removed, shuffle it into the sack deck.",
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1574",
                Title = "A Suspicious Crow",
                Id = "51223bd0-ffd1-11df-a976-1801204c9009",
                CardType = CardType.Enemy,
                EncounterSet = "Western Lands",
                Traits = new List<string>() { "Creature." },
                Quantity = 3,
                EngagementCost = 25,
                Attack = 1,
                Defense = 1,
                HitPoints = 1,
                Text = "When Revealed: Reveal the top card of the encounter discard pile and add it to the staging area, if able.",
                Shadow = "Shadow: If this attack deals at least 1 damage, shuffle this card back into the encounter deck.",
                Threat = 1,
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1585",
                Title = "A Tough Sack",
                Id = "51223bd0-ffd1-11df-a976-1801204c9010",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Sack." },
                Quantity = 1,
                Text = "When Sacked: Attach to the character (excluding Gandalf) with the highest printed Defense without a Sack attached.Attached character cannot ready, attack, defend, commit to quests, or trigger effects. If this Sack card is removed, shuffle it into the sack deck.",
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1553",
                Title = "A Very Good Tale",
                Id = "51223bd0-ffd1-11df-a976-1801204c9011",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Exhaust 2 allies you control to shuffle your deck and discard the top 5 cards. Put up to 2 allies discarded by this effect into play under your control. The total cost of the allies put into play cannot exceed the total cost of the allies exhausted to pay for this effect.",
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1589",
                Title = "A Worn Sack",
                Id = "51223bd0-ffd1-11df-a976-1801204c9012",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Sack." },
                Quantity = 1,
                Text = "When Sacked: Attach to hero with the most resources without a Sack attached.Attached character cannot ready, attack, defend, commit to quests, or trigger effects. If this Sack card is removed, shuffle it into the sack deck.",
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1562",
                Title = "An Unexpected Party - 1A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9013",
                CardType = CardType.Quest,
                EncounterSet = "We Must Away, Ere Break of Day",
                Quantity = 1,
                QuestPoints = 7,
                Setup = "tttt",
                Text = "Setup: Remove and shuffle the 7 Sack cards into a Sack deck and set it aside face down. Remove the 3 Troll enemies and the Troll Cave from the encounter deck and set them aside out of play. Then, shuffle the encounter deck. Each player reveals 1 card from the top of the encounter deck and adds it to the staging area.",
                FlavorText = "The wizard Gandalf has chosen Bilbo Baggins to join Thorin and company on their quest to the Lonley Mountain.",
                OppositeFlavorText = "At first they had passed through hobbit-lands, a wild respectable country inhabited by decent folk, with good roads, an inn or two, and now and then, a dwarf or a farmer ambling by on business. Then they came to lands where people spoke strangely, and sang songs Bilbo had never heard before. - The Hobbit.",
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1544",
                Title = "Beorn",
                Id = "51223bd0-ffd1-11df-a976-1801204c9015",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Beorning.", " Warrior." },
                Quantity = 1,
                ThreatCost = 12,
                IsUnique = true,
                Attack = 5,
                Defense = 1,
                Willpower = 0,
                HitPoints = 10,
                Text = "Immune to player card effects.Beorn does not exhaust to defend.",
                Keywords = new List<string>() { "Sentinel.", " Cannot have attachments." },
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1572",
                Title = "Bert",
                Id = "51223bd0-ffd1-11df-a976-1801204c9016",
                CardType = CardType.Enemy,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Troll." },
                Quantity = 1,
                EngagementCost = 23,
                IsUnique = true,
                Attack = 5,
                Defense = 2,
                HitPoints = 10,
                Text = "Players cannot play attachment cards on Troll enemies.Forced: After Bert engages a player, sack 1.Forced: Return Bert to the staging area at the end of the combat phase. The engaged player may raise his threat by 1 to cancel this effect.",
                Threat = 3,
                VictoryPoints = 3,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1540",
                Title = "Bilbo Baggins",
                Id = "51223bd0-ffd1-11df-a976-1801204c9017",
                CardType = CardType.Hero,
                Sphere = Sphere.Baggins,
                Traits = new List<string>() { "Hobbit." },
                Keywords = new List<string> { "The first player gains control of Bilbo Baggins.", "Bilbo Baggins cannot gain resources from player card effects." },
                Quantity = 1,
                ThreatCost = 0,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 3,
                Text = "If Bilbo Baggins leaves play, the players lose the game.",
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1618",
                Title = "Bilbo's Magic Ring",
                Id = "51223bd0-ffd1-11df-a976-1801204c9018",
                CardType = CardType.Objective,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Traits = new List<string>() { "Artifact.", " Ring." },
                Quantity = 1,
                IsUnique = true,
                Text = "Action: When answering a riddle, spend 1 Baggins resource to discard an additional player card from the top of your deck.Action: Exhaust Bilbo's Magic Ring and raise each player's threat by 2 to add 1 Baggins resource to Bilbo Baggins' resource pool.",
                Keywords = new List<string>() { "Attach to Bilbo Baggins." },
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1547",
                Title = "Bofur",
                Id = "51223bd0-ffd1-11df-a976-1801204c9019",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 3,
                IsUnique = true,
                Attack = 2,
                Defense = 0,
                Willpower = 2,
                HitPoints = 3,
                Text = "Action: Exhaust Bofur to search the top 5 cards of your deck for 1 Weapon attachment. Add that card to your hand and shuffle the other cards back into your deck.",
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1558",
                Title = "Burglar Baggins",
                Id = "51223bd0-ffd1-11df-a976-1801204c9020",
                CardType = CardType.Event,
                Sphere = Sphere.Baggins,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Bilbo Baggins gets +2 Willpower, +2 Attack, and +2 Defense until the end of the phase. (You may spend a Baggins resource from Bilbo Baggins' resource pool to play this card even if you do not control Bilbo Baggins.)",
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1580",
                Title = "Cave Entrance",
                Id = "51223bd0-ffd1-11df-a976-1801204c9021",
                CardType = CardType.Location,
                EncounterSet = "Western Lands",
                Traits = new List<string>() { "Western Lands." },
                Quantity = 2,
                Text = "While Cave Entrance is in the staging area, it gains: 'Forced: At the end of the round, place the top X cards of the encounter discard pile on the bottom of the encounter deck. X is the number of players in the game.'",
                Threat = 1,
                QuestPoints = 3,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1610",
                Title = "Chaos in the Cavern",
                Id = "51223bd0-ffd1-11df-a976-1801204c9022",
                CardType = CardType.Treachery,
                EncounterSet = "The Great Goblin",
                Quantity = 2,
                Text = "When Revealed: All engaged enemies return to the staging area. Then, each Goblin enemy gets +1 Threat until the end of the phase.",
                Shadow = "Shadow: attacking enemy is returned to the staging area after its attack resolves.",
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1619",
                Title = "Come down little bird",
                Id = "51223bd0-ffd1-11df-a976-1801204c9023",
                CardType = CardType.Treachery,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Quantity = 2,
                Text = "When Revealed: Starting with the first player, each player must search the encounter deck and encounter discard pile for 1 Creature enemy and put it into play engaged with him. Then, shuffle the encounter deck.Riddle: The first player names a card type and cost, shuffles his deck, then discards the top 3 cards. For each of those cards that matches both items, place 1 progress token on stage 2.",
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1550",
                Title = "Cram",
                Id = "51223bd0-ffd1-11df-a976-1801204c9024",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Item." },
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Discard Cram to ready attached hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1564",
                Title = "Dawn Take You All - 3A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9025",
                CardType = CardType.Quest,
                EncounterSet = "We Must Away, Ere Break of Day",
                Quantity = 1,
                QuestPoints = 0,
                FlavorText = "\"Dawn take you all, and be stone to you!\" - Gandalf, The Hobbit ",
                OppositeText = "Play after shadow cards have been dealt, before any attacks have resloved.\r\nCombat Action: Each player may choose and discard 1 facedown shadow card from an enemy with which he is engaged.",
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1548",
                Title = "Dori",
                Id = "51223bd0-ffd1-11df-a976-1801204c9027",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 3,
                IsUnique = true,
                Attack = 2,
                Defense = 1,
                Willpower = 1,
                HitPoints = 3,
                Text = "Response: After a hero is assigned any amount of damage, exhaust Dori to place that damage on Dori instead.",
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1567",
                Title = "Down, Down to Goblin Town - 3A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9028",
                CardType = CardType.Quest,
                EncounterSet = "The Great Goblin",
                Quantity = 1,
                QuestPoints = 20,
                Text = "When Revealed: Shuffle all encounter cards back into the encounter deck and set it aside, inactive. The second encounter deck becomes the active encounter deck. Search the encounter deck for The Great Goblin and add it to the staging area. Then, shuffle the encounter deck.",
                FlavorText = "Out jumped the goblins, big goblins, great ugly-looking goblins, lots of goblins, before you could say rocks and blocks. - The Hobbit",
                OppositeText = "Players cannot defeat this stage unless The Great Goblin is in the victory display.\r\nWhen Revealed: Reveal 3 encounter cards per player, Bilbo Baggins may spend X resources to reduce the total number of encounter cards revealed by X. (To a minimum of 1.)\r\nIf the players defeat this stage, they have won the game.",
                Number = 25
            });
            Cards.Add(new Card() {
                ImageName = "M1578",
                Title = "Dreary Hills",
                Id = "51223bd0-ffd1-11df-a976-1801204c9030",
                CardType = CardType.Location,
                EncounterSet = "Western Lands",
                Traits = new List<string>() { "Western Lands." },
                Quantity = 2,
                Text = "Forced: After placing 1 or more progress tokens on Dreary Hills, each player must discard 1 card at random from his hand.Response: After Dreary Hills leaves play as an explored location, Bilbo Baggins gains 1 resource.",
                Threat = 3,
                QuestPoints = 2,
                Number = 26
            });
            Cards.Add(new Card() {
                ImageName = "M1557",
                Title = "Expecting Mischief",
                Id = "51223bd0-ffd1-11df-a976-1801204c9031",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Play during the quest phase, before the staging step.Action: Deal 2 damage to the first enemy revealed from the encounter deck this phase.",
                Number = 27
            });
            Cards.Add(new Card() {
                ImageName = "M1545",
                Title = "Fili",
                Id = "51223bd0-ffd1-11df-a976-1801204c9032",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 3,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 2,
                Text = "Response: After you play Fili from your hand during the planning phase, search your deck for Kili and put him into play under your control. Then, shuffle your deck.",
                Number = 28
            });
            Cards.Add(new Card() {
                ImageName = "M1554",
                Title = "Foe-hammer",
                Id = "51223bd0-ffd1-11df-a976-1801204c9033",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Response: After a hero you control attacks and destroys an enemy, exhaust a Weapon card attached to that hero to draw 3 cards.",
                Number = 29
            });
            Cards.Add(new Card() {
                ImageName = "M1605",
                Title = "Front Porch",
                Id = "51223bd0-ffd1-11df-a976-1801204c9034",
                CardType = CardType.Location,
                EncounterSet = "The Great Goblin",
                Traits = new List<string>() { "Cave." },
                Quantity = 2,
                Text = "While Front Porch is the active location, players cannot attack Goblin enemies.Action: The first player may spend 2 Baggins resources to treat Front Porch's printed text box as if it were blank until the end of the round.",
                Threat = 4,
                QuestPoints = 4,
                Number = 30
            });
            Cards.Add(new Card() {
                ImageName = "M1611",
                Title = "Galloping Boulders",
                Id = "51223bd0-ffd1-11df-a976-1801204c9035",
                CardType = CardType.Treachery,
                EncounterSet = "Over the Misty Mountains Grim",
                Quantity = 4,
                Text = "When Revealed: The first player chooses a questing character. That character takes 3 damage and is removed from the quest.",
                Shadow = "Shadow: Put this card on top of the encounter deck.",
                Number = 31
            });
            Cards.Add(new Card() {
                ImageName = "M1549",
                Title = "Gandalf",
                Id = "51223bd0-ffd1-11df-a976-1801204c9036",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Istari." },
                Quantity = 3,
                ResourceCost = 5,
                IsUnique = true,
                Attack = 4,
                Defense = 4,
                Willpower = 4,
                HitPoints = 4,
                Text = "Gandalf does not exhaust to commit to a quest.Forced: At the end of the refresh phase, discard Gandalf from play. You may raise your threat by 2 to cancel this effect.",
                Number = 32
            });
            Cards.Add(new Card() {
                ImageName = "M1561",
                Title = "Glamdring",
                Id = "51223bd0-ffd1-11df-a976-1801204c9037",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Artifact.", " Item.", " Weapon." },
                Quantity = 1,
                ResourceCost = 0,
                IsUnique = true,
                Text = "Attached character gets +2 Attack.Response: After attached character destroys an Orc enemy, draw 1 card.",
                Keywords = new List<string>() { "Attach to a hero or Gandalf.", " Restricted." },
                Number = 33
            });
            Cards.Add(new Card() {
                ImageName = "M1597",
                Title = "Goblin Axeman",
                Id = "51223bd0-ffd1-11df-a976-1801204c9038",
                CardType = CardType.Enemy,
                EncounterSet = "Misty Mountain Goblins",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 3,
                EngagementCost = 25,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Text = "Goblin Axeman gets +1 Attack for each Cave location in play.",
                Shadow = "Shadow: Defending character gets -1 Defense.",
                Threat = 2,
                Number = 34
            });
            Cards.Add(new Card() {
                ImageName = "M1601",
                Title = "Goblin Bent-Swords",
                Id = "51223bd0-ffd1-11df-a976-1801204c9039",
                CardType = CardType.Enemy,
                EncounterSet = "The Great Goblin",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 4,
                EngagementCost = 33,
                Attack = 3,
                Defense = 2,
                HitPoints = 3,
                Text = "If The Great Goblin is in the victory display, this card gains surge.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+2 Attack instead if The Great Goblin is in the victory display.)",
                Threat = 2,
                Number = 35
            });
            Cards.Add(new Card() {
                ImageName = "M1600",
                Title = "Goblin Driver",
                Id = "51223bd0-ffd1-11df-a976-1801204c9040",
                CardType = CardType.Enemy,
                EncounterSet = "Misty Mountain Goblins",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 3,
                EngagementCost = 30,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Text = "When Revealed: The first player exhausts 1 character he controls.",
                Shadow = "Shadow: Defending player exhausts 1 character he controls.",
                Threat = 3,
                Number = 36
            });
            Cards.Add(new Card() {
                ImageName = "M1598",
                Title = "Goblin Miners",
                Id = "51223bd0-ffd1-11df-a976-1801204c9041",
                CardType = CardType.Enemy,
                EncounterSet = "Misty Mountain Goblins",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 3,
                EngagementCost = 25,
                Attack = 2,
                Defense = 0,
                HitPoints = 2,
                Text = "Goblin Miners gets +1 Defense for each Cave location in play.",
                Shadow = "Shadow: attacking enemy gets +1 Attack.",
                Threat = 2,
                Number = 37
            });
            Cards.Add(new Card() {
                ImageName = "M1599",
                Title = "Goblin Runners",
                Id = "51223bd0-ffd1-11df-a976-1801204c9042",
                CardType = CardType.Enemy,
                EncounterSet = "Misty Mountain Goblins",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 3,
                EngagementCost = 20,
                Attack = 3,
                Defense = 1,
                HitPoints = 2,
                Text = " ",
                Shadow = "Shadow: attacking enemy makes an additional attack immediately after this one. (Deal a new shadow card for that attack.)",
                Keywords = new List<string>() { "Surge." },
                Threat = 1,
                Number = 38
            });
            Cards.Add(new Card() {
                ImageName = "M1555",
                Title = "Goblin-cleaver",
                Id = "51223bd0-ffd1-11df-a976-1801204c9043",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Combat Action: Exhaust a Weapon card attached to a hero you control to choose an enemy engaged with you. Deal 2 damage to that enemy. (Deal 3 damage instead if the enemy is an Orc.)",
                Number = 39
            });
            Cards.Add(new Card() {
                ImageName = "M1613",
                Title = "Gollum",
                Id = "51223bd0-ffd1-11df-a976-1801204c9044",
                CardType = CardType.Enemy,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Traits = new List<string>() { "Gollum." },
                Quantity = 1,
                EngagementCost = 50,
                IsUnique = true,
                Attack = 2,
                Defense = 3,
                HitPoints = 5,
                Text = "Forced: After the first player answers a riddle and fails to find at least 1 match, Gollum attacks Bilbo Baggins. (Do not deal a shadow card for this attack.)",
                Threat = 0,
                Number = 40
            });
            Cards.Add(new Card() {
                ImageName = "M1606",
                Title = "Great Cavern Room",
                Id = "51223bd0-ffd1-11df-a976-1801204c9045",
                CardType = CardType.Location,
                EncounterSet = "The Great Goblin",
                Traits = new List<string>() { "Cave." },
                Quantity = 3,
                Text = "While Great Cavern Room is the active location, it gains: 'Forced: After a player engages a Goblin enemy, he must deal 1 damage to a character he controls.'Forced: When faced with the option to travel, if The Great Goblin is in the victory display the players must travel to Great Cavern Room if able.",
                Threat = 2,
                QuestPoints = 3,
                Number = 41
            });
            Cards.Add(new Card() {
                ImageName = "M1614",
                Title = "Great Gray Wolf",
                Id = "51223bd0-ffd1-11df-a976-1801204c9046",
                CardType = CardType.Enemy,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Traits = new List<string>() { "Creature." },
                Quantity = 1,
                EngagementCost = 30,
                Attack = 5,
                Defense = 2,
                HitPoints = 5,
                Text = "Forced: If Great Gray Wolf is dealt a shadow card with a riddle, return all Creature enemies to the staging area at the end of the combat phase.Riddle: The first player names a card type, sphere and cost, shuffles his deck, then discards the top 3 cards. For each of those cards that matches all three items, place 1 progress token on stage 2.",
                Threat = 4,
                Number = 42
            });
            Cards.Add(new Card() {
                ImageName = "M1609",
                Title = "Grip, grab! Pinch, nab!",
                Id = "51223bd0-ffd1-11df-a976-1801204c9047",
                CardType = CardType.Treachery,
                EncounterSet = "Misty Mountain Goblins",
                Quantity = 3,
                Text = "When Revealed: Starting with the first player, each player must choose 1 Goblin enemy from the discard pile and add it to the staging area.",
                Shadow = "Shadow: Defending player deals damage among characters he controls equal to the number of Goblin enemies engaged with him.",
                Number = 43
            });
            Cards.Add(new Card() {
                ImageName = "M1612",
                Title = "Guffawing of Giants",
                Id = "51223bd0-ffd1-11df-a976-1801204c9048",
                CardType = CardType.Treachery,
                EncounterSet = "Over the Misty Mountains Grim",
                Quantity = 2,
                Text = "When Revealed: The first player chooses 1 Stone-giant in the staging area. At the end of the quest phase, that Stone-giant engages the player with the highest threat. If there are no Stone-giant cards in the staging area, search the encounter deck for 1 Stone-giant and add it to the staging area.",
                Number = 44
            });
            Cards.Add(new Card() {
                ImageName = "M1621",
                Title = "Hiding in the Trees",
                Id = "51223bd0-ffd1-11df-a976-1801204c9049",
                CardType = CardType.Treachery,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Quantity = 2,
                Text = "When Revealed: Characters get -1 Willpower and cannot attack until the end of the round.Riddle: The first player names a card type and sphere, shuffles his deck, then discards the top 3 cards. For each of those cards that matches both items, place 1 progress token on stage 2.",
                Number = 45
            });
            Cards.Add(new Card() {
                ImageName = "M1577",
                Title = "Hobbit-lands",
                Id = "51223bd0-ffd1-11df-a976-1801204c9050",
                CardType = CardType.Location,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Western Lands." },
                Quantity = 2,
                Text = "Response: After placing 1 or more progress tokens on Hobbit-lands, the first player draws 1 card.",
                Shadow = "Shadow: Defending player must put the top card of the encounter deck discard pile on top of the encounter deck.",
                Threat = 1,
                QuestPoints = 1,
                Number = 46
            });
            Cards.Add(new Card() {
                ImageName = "M1593",
                Title = "Hungry Troll",
                Id = "51223bd0-ffd1-11df-a976-1801204c9051",
                CardType = CardType.Treachery,
                EncounterSet = "We Must Away, Ere Break of Day",
                Quantity = 3,
                Text = "When Revealed: The first player chooses a Troll enemy in the staging area and engages that enemy. If there are no Troll enemies in the staging area, this card gains Surge.",
                Shadow = "Shadow: Deal 4 damage to each character with a Sack card attached. (The first player may spend 2 Baggins resources to cancel this effect.)",
                Number = 47
            });
            Cards.Add(new Card() {
                ImageName = "M1570",
                Title = "Into the Fire - 3A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9052",
                CardType = CardType.Quest,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Quantity = 1,
                QuestPoints = 16,
                FlavorText = "To everyone's surprise, Bilbo Baggins rejoined his companions on the eastern side of the Misty Mountains. However, their celebration was cut short by the chilling sound of wolves howling close by. To the frightened hobbit, it seemed that they had escaped from the goblins only to be eaten by wargs.",
                OppositeText = "When Revealed: The first player gains control of Bilbo Baggins. Reveal 1 encounter card per player and add it to the staging area.\r\nGollum engages the first player. Damage from undefended attacks made by Gollum must be placed on Bilbo Baggins.\r\nAll riddle effects are ignored. Treachery cards gain surge.\r\nIf players defeat this stage, they have won the game.",
                Number = 48
            });
            Cards.Add(new Card() {
                ImageName = "M1620",
                Title = "It Likes Riddles?",
                Id = "51223bd0-ffd1-11df-a976-1801204c9054",
                CardType = CardType.Treachery,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Quantity = 3,
                Text = "When Revealed: The first player must choose to answer the riddle on this card. If he finds at least 1 match, discard cards from the encounter deck until another card with a riddle is discarded. Then, answer that riddle.Riddle: The first player names a cost, shuffles his deck, then discards the top 2 cards. For each of those cards that matches, place 1 progress token on stage 2.",
                Number = 49
            });
            Cards.Add(new Card() {
                ImageName = "M1546",
                Title = "Kili",
                Id = "51223bd0-ffd1-11df-a976-1801204c9055",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 3,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 2,
                Text = "Response: After you play Kili from your hand during the planning phase, search your deck for Fili and put him into play under your control. Then, shuffle your deck.",
                Number = 50
            });
            Cards.Add(new Card() {
                ImageName = "M1617",
                Title = "Lake in the Cavern",
                Id = "51223bd0-ffd1-11df-a976-1801204c9056",
                CardType = CardType.Location,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Traits = new List<string>() { "Cave." },
                Quantity = 1,
                IsUnique = true,
                Text = "Players cannot travel here.Immune to player card effects.Forced: After players advance to stage 3, remove Lake in the Cavern from the game.",
                Keywords = new List<string>() { "X is twice the number of players in the game." },
                Threat = 0,
                QuestPoints = 0,
                Number = 51
            });
            Cards.Add(new Card() {
                ImageName = "M1556",
                Title = "Late Adventurer",
                Id = "51223bd0-ffd1-11df-a976-1801204c9057",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Quest Action: Exhaust a character you control that is not committed to the quest to commit that character to the quest.",
                Number = 52
            });
            Cards.Add(new Card() {
                ImageName = "M1579",
                Title = "Lone-Lands",
                Id = "51223bd0-ffd1-11df-a976-1801204c9058",
                CardType = CardType.Location,
                EncounterSet = "Western Lands",
                Traits = new List<string>() { "Western Lands." },
                Quantity = 2,
                Text = "Forced: After placing 1 or more progress tokens on Lone-Lands, each player removes 1 resource from one of his hero's resource pools, if able.Response: After Lone-Lands leaves play as an explored location, Bilbo Baggins gains 1 resource.",
                Threat = 2,
                QuestPoints = 3,
                Number = 53
            });
            Cards.Add(new Card() {
                ImageName = "M1590",
                Title = "Lots or None at All",
                Id = "51223bd0-ffd1-11df-a976-1801204c9059",
                CardType = CardType.Treachery,
                EncounterSet = "We Must Away, Ere Break of Day",
                Quantity = 3,
                Text = "When Revealed: If there is a Troll enemy in the staging area, Sack 1. If there are no Troll enemies in the staging area, this card gains Doomed 2.",
                Shadow = "Shadow: If the attacking enemy is a Troll, Sack 1. (The first player may spend 1 Baggins resource to cancel this effect.)",
                Number = 54
            });
            Cards.Add(new Card() {
                ImageName = "M1596",
                Title = "More Like a Grocer",
                Id = "51223bd0-ffd1-11df-a976-1801204c9060",
                CardType = CardType.Treachery,
                EncounterSet = "Western Lands",
                Quantity = 1,
                Text = "When Revealed: Discard all Baggins resources.",
                Shadow = "Shadow: Resolve this card's 'When Revealed' effect.",
                Number = 55
            });
            Cards.Add(new Card() {
                ImageName = "M1594",
                Title = "No Campfire",
                Id = "51223bd0-ffd1-11df-a976-1801204c9061",
                CardType = CardType.Treachery,
                EncounterSet = "Western Lands",
                Quantity = 2,
                Text = "When Revealed: Each player must choose one: increase his threat by 4, or reveal an additional encounter card from the encounter deck and add it to the staging area.",
                Shadow = "Shadow: The defending player raises his threat by 2.",
                Keywords = new List<string>() { "Doomed 1." },
                Number = 56
            });
            Cards.Add(new Card() {
                ImageName = "M1542",
                Title = "Nori",
                Id = "51223bd0-ffd1-11df-a976-1801204c9062",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 1,
                ThreatCost = 9,
                IsUnique = true,
                Attack = 1,
                Defense = 2,
                Willpower = 2,
                HitPoints = 4,
                Text = "Response: After you play a Dwarf character from your hand, reduce your threat by 1.",
                Number = 57
            });
            Cards.Add(new Card() {
                ImageName = "M1623",
                Title = "Not fair! Not fair!",
                Id = "51223bd0-ffd1-11df-a976-1801204c9063",
                CardType = CardType.Treachery,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Quantity = 1,
                Text = "When Revealed: The first player names a card type and then discards the top 3 cards of the encounter deck. For each of those cards that does not match the named type, remove 1 progress token from stage 2. This effect cannot be canceled.",
                Number = 58
            });
            Cards.Add(new Card() {
                ImageName = "M1560",
                Title = "Orcrist",
                Id = "51223bd0-ffd1-11df-a976-1801204c9064",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Artifact.", " Item.", " Weapon." },
                Quantity = 1,
                ResourceCost = 0,
                IsUnique = true,
                Text = "Attached character gets +2 Attack.Response: After attached hero destroys an Orc enemy, add 1 resource to that hero's resource pool.",
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                Number = 59
            });
            Cards.Add(new Card() {
                ImageName = "M1543",
                Title = "Ori",
                Id = "51223bd0-ffd1-11df-a976-1801204c9065",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 1,
                ThreatCost = 8,
                IsUnique = true,
                Attack = 2,
                Defense = 1,
                Willpower = 2,
                HitPoints = 3,
                Text = "If you control at least 5 Dwarf characters, draw 1 additional card at the beginning of the resource phase.",
                Number = 60
            });
            Cards.Add(new Card() {
                ImageName = "M1568",
                Title = "Out of the Frying Pan - 1A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9066",
                CardType = CardType.Quest,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Quantity = 1,
                QuestPoints = 14,
                Setup = "stt",
                Text = "Setup: Add Lake in the Cavern to the staging area. Create a riddle area with stage 2A and follow the setup instructions on that card.",
                FlavorText = "After killing the Great Goblin, Bilbo's companions fought to win their escape from the goblins. By the time they realized that Bilbo had been lost in the darkness, it was too late to turn back and search for him.",
                OppositeText = "Players cannot advance to stage 3A unless both 1B and 2B are complete.\r\nForced: Reveal 1 additional encounter card per player during the staging step.",
                Number = 61
            });
            Cards.Add(new Card() {
                ImageName = "M1607",
                Title = "Overhanging Rock",
                Id = "51223bd0-ffd1-11df-a976-1801204c9068",
                CardType = CardType.Location,
                EncounterSet = "Over the Misty Mountains Grim",
                Traits = new List<string>() { "Mountain." },
                Quantity = 2,
                Text = "While Overhanging Rock is the active location, it gains: 'Action: Spend 1 Baggins resource to look at the top 2 cards of your deck. Add 1 of those to your hand and discard the other.'",
                Threat = 2,
                QuestPoints = 3,
                Number = 62
            });
            Cards.Add(new Card() {
                ImageName = "M1569",
                Title = "Riddles in the Dark - 2A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9069",
                CardType = CardType.Quest,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Quantity = 1,
                QuestPoints = 9,
                Text = "Setup: Search the encounter deck for Gollum and Bilbo's Magic Ring. Place Gollum and Bilbo Baggins in the riddle area and attach Bilbo's Magic Ring to Bilbo Baggins. Then, shuffle the encounter deck.",
                FlavorText = "During the confusion, bilbo stumbled won a tunnel and into Gollum's cave. There the Hobbit had to outwit the creature Gollum in a dangerous riddle contest to discover the way out.",
                OppositeText = "Players cannot advance to stage 3A unless both 1B and 2B are complete.\r\nProgress tokens cannot be added to, or removed from, this quest except by answering riddles.\r\nCards in the riddle area are immune to player card effects and cannot leave the riddle area except by quest effects.",
                Number = 63
            });
            Cards.Add(new Card() {
                ImageName = "M1592",
                Title = "Roast 'Em or Boil 'Em?",
                Id = "51223bd0-ffd1-11df-a976-1801204c9071",
                CardType = CardType.Treachery,
                EncounterSet = "We Must Away, Ere Break of Day",
                Quantity = 3,
                Text = "When Revealed: Deal 1 damage to each ally. (2 damage instead if there is a Troll enemy in the staging area.)",
                Shadow = "Shadow: Search the encounter deck for Troll Camp and add it to the staging area. Then, shuffle the encounter deck.",
                Number = 64
            });
            Cards.Add(new Card() {
                ImageName = "M1563",
                Title = "Roast Mutton - 2A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9072",
                CardType = CardType.Quest,
                EncounterSet = "We Must Away, Ere Break of Day",
                Quantity = 1,
                QuestPoints = 1,
                Text = "When Revealed: Add the set-aside Troll enemies and the Troll Cave to the staging area. Shuffle the encounter discard pile back into the encounter deck.",
                FlavorText = "Obviously trolls. Even Bilbo, in spite of his sheltered life, could see that: from the great heavy faces of them, and their size, and the shape of their legs, not to mention their language, which was not drawing-room fashion at all - The Hobbit",
                OppositeText = "Forced: If there are no Troll enemies left in play, or if there are no cards left in the encounter deck, advance it to the next stage.\r\nAny time players would place progress tokens on this quest, discard an equal number of cards from the encounter deck instead. (Progress is placed on the active location before triggering this effect.)",
                Number = 65
            });
            Cards.Add(new Card() {
                ImageName = "M1551",
                Title = "Spare Hood and Cloak",
                Id = "51223bd0-ffd1-11df-a976-1801204c9074",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Item." },
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Exhaust Spare Hood and Cloak and exhaust attached character to ready another character. Then, attach Spare Hood and Cloak to that character.",
                Keywords = new List<string>() { "Attach to a character." },
                Number = 66
            });
            Cards.Add(new Card() {
                ImageName = "M1559",
                Title = "Sting",
                Id = "51223bd0-ffd1-11df-a976-1801204c9075",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Artifact.", " Item.", " Weapon." },
                Quantity = 1,
                ResourceCost = 0,
                IsUnique = true,
                Text = "Bilbo Baggins gets +1 Willpower, +1 Attack, and +1 Defense.Response: After Bilbo Baggins exhausts to defend, discard the top card of the encounter deck. Deal damage to the attacking enemy equal to the discarded card's Threat.",
                Keywords = new List<string>() { "Attach to Bilbo Baggins.", " Restricted." },
                Number = 67
            });
            Cards.Add(new Card() {
                ImageName = "M1603",
                Title = "Stone-Giant",
                Id = "51223bd0-ffd1-11df-a976-1801204c9076",
                CardType = CardType.Enemy,
                EncounterSet = "Over the Misty Mountains Grim",
                Traits = new List<string>() { "Giant." },
                Quantity = 3,
                EngagementCost = 40,
                Attack = 6,
                Defense = 3,
                HitPoints = 9,
                Text = "While at least one Stone-Giant is in the staging area, the Galloping Boulders card gains surge.Forced: After Stone-Giant engages a player, that player chooses and discards 1 ally he controls.",
                Threat = 4,
                Number = 68
            });
            Cards.Add(new Card() {
                ImageName = "M1604",
                Title = "The Goblins' Caves",
                Id = "51223bd0-ffd1-11df-a976-1801204c9077",
                CardType = CardType.Location,
                EncounterSet = "Misty Mountain Goblins",
                Traits = new List<string>() { "Cave." },
                Quantity = 3,
                Text = "While The Goblins' Caves is the active location, Goblin enemies get +1 Threat.",
                Shadow = "Shadow: Defending player raises his threat by X. X is the number of Goblin enemies engaged with him.",
                Threat = 3,
                QuestPoints = 3,
                Number = 69
            });
            Cards.Add(new Card() {
                ImageName = "M1602",
                Title = "The Great Goblin",
                Id = "51223bd0-ffd1-11df-a976-1801204c9078",
                CardType = CardType.Enemy,
                EncounterSet = "The Great Goblin",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 1,
                EngagementCost = 15,
                IsUnique = true,
                Attack = 5,
                Defense = 2,
                HitPoints = 8,
                Text = "Forced: After The Great Goblin attacks, discard X cards from the encounter deck where X is the number of players in the game. Add each Goblin enemy discarded by this effect to the staging area.",
                Threat = 3,
                VictoryPoints = 3,
                Number = 70
            });
            Cards.Add(new Card() {
                ImageName = "M1608",
                Title = "The High Pass",
                Id = "51223bd0-ffd1-11df-a976-1801204c9079",
                CardType = CardType.Location,
                EncounterSet = "Over the Misty Mountains Grim",
                Traits = new List<string>() { "Mountain." },
                Quantity = 1,
                IsUnique = true,
                Text = "X is the number of players in the game.Forced: At the end of the round, remove X progress tokens from the current quest.",
                Threat = 0,
                QuestPoints = 5,
                Number = 71
            });
            Cards.Add(new Card() {
                ImageName = "M1566",
                Title = "The Mountain Pass - 2A",
                Id = "51223bd0-ffd1-11df-a976-1801204c9080",
                CardType = CardType.Quest,
                EncounterSet = "Over the Misty Mountains Grim",
                Quantity = 1,
                QuestPoints = 16,
                FlavorText = "When he peeped out in the lightning-flashes, he saw that across the valley the stone-giants were out and were hurling rocks at one another for a game, and catching them, and tossing them down into the darkness where they smashed the trees far below, or splintered into little bits with a bang. - The Hobbit ",
                OppositeText = "When Revealed: Search the encounter deck for 1 copy of Stone-giant and add it to the staging area. Then, shuffle the encounter deck. Reveal 1 card per player from the encounter deck and add it to the staging area.",
                Number = 72
            });
            Cards.Add(new Card() {
                ImageName = "M1616",
                Title = "The Wargs' Glade",
                Id = "51223bd0-ffd1-11df-a976-1801204c9082",
                CardType = CardType.Location,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Traits = new List<string>() { "Forest." },
                Quantity = 3,
                Text = "Forced: After a character takes damage from an attack made by a Creature enemy, remove 1 progress from the current quest.Riddle: The first player names a sphere, shuffles his deck, and discards the top card. For each of those cards that matches, place 1 progress token on stage 2.",
                Threat = 2,
                QuestPoints = 4,
                Number = 73
            });
            Cards.Add(new Card() {
                ImageName = "M1541",
                Title = "Thorin Oakenshield",
                Id = "51223bd0-ffd1-11df-a976-1801204c9083",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Dwarf.", " Noble.", " Warrior." },
                Quantity = 1,
                ThreatCost = 12,
                IsUnique = true,
                Attack = 3,
                Defense = 1,
                Willpower = 3,
                HitPoints = 5,
                Text = "If you control at least 5 Dwarf characters, add 1 additional resource to Thorin Oakenshield's pool when you collect resources during the resource phase.",
                Number = 74
            });
            Cards.Add(new Card() {
                ImageName = "M1552",
                Title = "Thror's Map",
                Id = "51223bd0-ffd1-11df-a976-1801204c9084",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Artifact.", " Item." },
                Quantity = 3,
                ResourceCost = 1,
                IsUnique = true,
                Text = "Travel Action: Exhaust Thror's Map to choose a location in the staging area. Make that location the active location. (If there is another active location, return it to the staging area.)",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 75
            });
            Cards.Add(new Card() {
                ImageName = "M1573",
                Title = "Tom",
                Id = "51223bd0-ffd1-11df-a976-1801204c9085",
                CardType = CardType.Enemy,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Troll." },
                Quantity = 1,
                EngagementCost = 33,
                IsUnique = true,
                Attack = 5,
                Defense = 2,
                HitPoints = 11,
                Text = "Troll enemies can only be attacked by one character at a time.Forced: After Tom engages a player, sack 1.Forced: Return Tom to the staging area at the end of the combat phase. The engaged player may raise his threat by 1 to cancel this effect.",
                Threat = 3,
                VictoryPoints = 4,
                Number = 76
            });
            Cards.Add(new Card() {
                ImageName = "M1575",
                Title = "Troll Camp",
                Id = "51223bd0-ffd1-11df-a976-1801204c9086",
                CardType = CardType.Location,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Forest.", " Trollshaws." },
                Quantity = 3,
                Text = "While Troll Camp is in the staging area, Troll enemies get +1 Threat for each player in the game.While Troll Camp is in play, Bilbo Baggins gains: 'Action: Exhaust Bilbo Baggins and spend 1 Baggins resource to remove 1 Sack card from a character. Bilbo Baggins may trigger this effect even with a Sack card attached to him.'",
                Threat = 3,
                QuestPoints = 3,
                Number = 77
            });
            Cards.Add(new Card() {
                ImageName = "M1576",
                Title = "Troll Cave",
                Id = "51223bd0-ffd1-11df-a976-1801204c9087",
                CardType = CardType.Location,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Cave.", " Trollshaws." },
                Quantity = 1,
                Text = "Players cannot travel to Troll Cave unless Bilbo Baggins has the Troll Key attached and the first player spends 5 Baggins resources. (2 Baggins resources instead if Bilbo Baggins has the Troll Purse attached.)",
                Keywords = new List<string>() { "Immune to player card effects." },
                Threat = 2,
                QuestPoints = 4,
                VictoryPoints = 2,
                Number = 78
            });
            Cards.Add(new Card() {
                ImageName = "M1582",
                Title = "Troll Key",
                Id = "51223bd0-ffd1-11df-a976-1801204c9088",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Item." },
                Quantity = 1,
                IsUnique = true,
                Text = "If Troll Key is discarded, add it to the staging area.If Troll Key is unattached and in the staging area, attach it to a Troll enemy, if able.Response: After attached Troll enemy takes damage as the result of an attack, the first player may exhaust Bilbo Baggins to claim this objective and attach it to him.",
                Number = 79
            });
            Cards.Add(new Card() {
                ImageName = "M1581",
                Title = "Troll Purse",
                Id = "51223bd0-ffd1-11df-a976-1801204c9089",
                CardType = CardType.Objective,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Item." },
                Quantity = 1,
                IsUnique = true,
                Text = "If Troll Purse is discarded, add it to the staging area.If Troll Purse is unattached and in the staging area, attach it to a Troll enemy, if able.Response: After attached Troll enemy is destroyed, the first player may spend 1 Baggins resource to claim this objective and attach it to Bilbo Baggins.",
                Number = 80
            });
            Cards.Add(new Card() {
                ImageName = "M1622",
                Title = "What's In My Pocket?",
                Id = "51223bd0-ffd1-11df-a976-1801204c9090",
                CardType = CardType.Treachery,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Quantity = 2,
                Text = "When Revealed: The first player must choose to answer the riddle on this card. This effect cannot be canceled.Riddle: The first player names a sphere and cost, shuffles his deck, then discards the top 2 cards. For each of those cards that matches both items, place 1 progress token on stage 2.",
                Number = 81
            });
            Cards.Add(new Card() {
                ImageName = "M1615",
                Title = "Wild Wargs",
                Id = "51223bd0-ffd1-11df-a976-1801204c9091",
                CardType = CardType.Enemy,
                EncounterSet = "Dungeons Deep and Caverns Dim",
                Traits = new List<string>() { "Creature." },
                Quantity = 4,
                EngagementCost = 25,
                Attack = 3,
                Defense = 2,
                HitPoints = 3,
                Text = "Forced: If Wild Wargs is dealt a shadow card with a riddle, it gets +2 Attack.Riddle: The first player names a card type, shuffles his deck, and discards the top 2 cards. For each of those cards that matches, place 1 progress token on stage 2.",
                Threat = 3,
                Number = 82
            });
            Cards.Add(new Card() {
                ImageName = "M1571",
                Title = "William",
                Id = "51223bd0-ffd1-11df-a976-1801204c9092",
                CardType = CardType.Enemy,
                EncounterSet = "We Must Away, Ere Break of Day",
                Traits = new List<string>() { "Troll." },
                Quantity = 1,
                EngagementCost = 38,
                IsUnique = true,
                Attack = 5,
                Defense = 2,
                HitPoints = 12,
                Text = "Troll enemies not engaged with a player cannot take damage.Forced: After William engages a player, sack 2.Forced: Return William to the staging area at the end of the combat phase. The engaged player may raise his threat by 1 to cancel this effect.",
                Threat = 3,
                VictoryPoints = 5,
                Number = 83
            });
            Cards.Add(new Card() {
                ImageName = "M1595",
                Title = "Wind-whipped Rain",
                Id = "51223bd0-ffd1-11df-a976-1801204c9093",
                CardType = CardType.Treachery,
                EncounterSet = "Western Lands",
                Quantity = 2,
                Text = "When Revealed: Discard all non-treasure, non-objective attachments in play.",
                Shadow = "Shadow: The defending character gets -1 Defense.",
                Number = 84
            });
        }
    }
}
