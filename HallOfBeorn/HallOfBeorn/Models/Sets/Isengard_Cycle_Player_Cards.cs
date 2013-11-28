using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class IsengardCyclePlayerCards : CardSet
    {
        protected override void Initialize()
        {
            Name = "Voice of Isengard";
            SetType = Models.SetType.Deluxe_Expansion;

            Cards.Add(new Card() {
                Title = "Eomer",
                Id = "9472b675-605e-446e-a432-1146eec90001",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                ThreatCost = 10,
                Quantity = 1,
                Traits = new List<string>() { "Rohan.", " Noble.", " Warrior." },
                Willpower = 1,
                HitPoints = 4,
                Attack = 3,
                Defense = 2,
                Text = "Response: After an ally leaves play, Eomer gets +2 Attack until the end of the round. (Limit once per round.)",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Saruman",
                Id = "9472b675-605e-446e-a432-1146eec90002",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Doomed 3." },
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Istari.", " Isengard." },
                Willpower = 3,
                HitPoints = 4,
                Attack = 5,
                Defense = 4,
                Text = "At the end of the round, discard Saruman from play.Response: After Saruman enters play, choose a non-unique enemy or location in the staging area. While Saruman is in play, treat the chosen enemy or location as if it were not in play.",
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "The Seeing-stone",
                Id = "9472b675-605e-446e-a432-1146eec90003",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                Keywords = new List<string>() { "Doomed 1." },
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Search your deck for a card with the Doomed keyword and add it to your hand. Shuffle your deck.",
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Orthanc Guard",
                Id = "9472b675-605e-446e-a432-1146eec90004",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Isengard." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 0,
                Defense = 2,
                Text = "Response: After you raise your threat from the Doomed keyword, ready Orthanc Guard.",
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Isengard Messenger",
                Id = "9472b675-605e-446e-a432-1146eec90005",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Isengard." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 0,
                Defense = 1,
                Text = "Response: After you raise your threat from the Doomed keyword, Isengard Messenger gets +1 Willpower until the end of the round. (Limit twice per round.)",
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Westfold Outrider",
                Id = "9472b675-605e-446e-a432-1146eec90006",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Rohan.", " Scout." },
                Willpower = 0,
                HitPoints = 2,
                Attack = 2,
                Defense = 1,
                Text = "Action: Discard Westfold Outrider to choose an enemy not engaged with you. Engage the chosen enemy.",
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Rohan Horse-breeder",
                Id = "9472b675-605e-446e-a432-1146eec90007",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Rohan." },
                Willpower = 1,
                HitPoints = 1,
                Attack = 0,
                Defense = 0,
                Text = "Response: After Rohan Horse-breeder enters play, search the top 10 cards of your deck for a Mount attachment and add it to your hand. Shuffle your deck.",
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Friendship of Isengard",
                Id = "9472b675-605e-446e-a432-1146eec90008",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Keywords = new List<string>() { "Doomed 3." },
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Add 1 resource to each hero's resource pool.",
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Deep Knowledge",
                Id = "9472b675-605e-446e-a432-1146eec90009",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Keywords = new List<string>() { "Doomed 2." },
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Each player draws 2 cards.",
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Saruman's Voice",
                Id = "9472b675-605e-446e-a432-1146eec90010",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                Keywords = new List<string>() { "Doomed 3." },
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Each player chooses 1 enemy engaged with him. Until the end of the phase, each chosen enemy cannot attack the player that chose it.",
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Power of Orthanc",
                Id = "9472b675-605e-446e-a432-1146eec90011",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Keywords = new List<string>() { "Doomed 2." },
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Each player may choose and discard a Condition attachment from play.",
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Keys of Orthanc",
                Id = "9472b675-605e-446e-a432-1146eec90012",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a hero." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Item." },
                Text = "Response: After you raise your threat from the Doomed keyword, exhaust Keys of Orthanc to add 1 resource to attached hero's resource pool.",
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Grima",
                Id = "9472b675-605e-446e-a432-1146eec90013",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                IsUnique = true,
                ThreatCost = 9,
                Quantity = 1,
                Traits = new List<string>() { "Rohan." },
                Willpower = 2,
                HitPoints = 3,
                Attack = 1,
                Defense = 2,
                Text = "Action: Lower the cost of the next card you play from your hand this round by 1. That card gains Doomed 1. (Limit once per round.)",
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Rohan Warhorse",
                Id = "9472b675-605e-446e-a432-1146eec90014",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Keywords = new List<string>() { "Attach to a Ï or Rohan hero.", " Restricted." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Mount." },
                Text = "Response: After attached hero participates in an attack that destroys an enemy, exhaust Rohan Warhorse to ready attached hero.",
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Silver Lamp",
                Id = "9472b675-605e-446e-a432-1146eec90015",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                Keywords = new List<string>() { "Attach to a Spirit hero." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Item." },
                Text = "While attached hero is ready, shadow cards dealt to enemies engaged with you are dealt face up. (Shadow card effects are still resolved when resolving enemy attacks.)",
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Orc Hunter",
                Id = "9472b675-605e-446e-a432-1146eec90016",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Orc." },
                HitPoints = 4,
                Attack = 4,
                Defense = 2,
                Text = "Forced: After Orc Hunter engages a player, that player must either deal 2 damage to a character he controls, or remove 1 time counter from the current quest.",
                EncounterSet = "Misty Mountain Orcs",
                EngagementCost = 33,
                Shadow = "Shadow: Attacking enemy gets +1 Attack.",
                Threat = 3,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Orc Scout",
                Id = "9472b675-605e-446e-a432-1146eec90017",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Orc." },
                HitPoints = 3,
                Attack = 3,
                Defense = 1,
                Text = "When Revealed: Either reveal an additional encounter card, or remove 1 time counter from the current quest.",
                EncounterSet = "Misty Mountain Orcs",
                EngagementCost = 35,
                Shadow = "Shadow: Attacking enemy makes an additional attack after this one.",
                Threat = 2,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Orc Hound",
                Id = "9472b675-605e-446e-a432-1146eec90018",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Surge." },
                Quantity = 2,
                Traits = new List<string>() { "Creature." },
                HitPoints = 2,
                Attack = 2,
                Defense = 1,
                Text = "Forced: After Orc Hound engages a player, that player must exhaust a character he controls.",
                EncounterSet = "Misty Mountain Orcs",
                EngagementCost = 20,
                Threat = 1,
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Orc Hunting Party",
                Id = "9472b675-605e-446e-a432-1146eec90019",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Deal each Orc enemy in play a shadow card. Each Orc enemy gets -10 engagement cost until the end of the round. If there are no Orc enemies in the staging area, Orc Hunting Party gains surge.",
                EncounterSet = "Misty Mountain Orcs",
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "Low on Provisions",
                Id = "9472b675-605e-446e-a432-1146eec90020",
                CardType = CardType.Treachery,
                Quantity = 3,
                Text = "When Revealed: Each player must assign X damage among characters he controls where X is the number of characters that player controls.",
                EncounterSet = "Weary Travelers",
                Shadow = "Shadow: Remove the defending character from combat. You may declare a new defender by exhausting that character.",
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "Off Track",
                Id = "9472b675-605e-446e-a432-1146eec90021",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Quantity = 2,
                Text = "When Revealed: Attach to a location in play. (Counts as a Condition attachment with the text: 'Limit 1 per location. Attached location gets +2 quest points and gains: 'Forced: Remove an additional time counter from the current quest at the end of each round, if able.'')",
                EncounterSet = "Weary Travelers",
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "In Need of Rest",
                Id = "9472b675-605e-446e-a432-1146eec90022",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Remove a hero from the quest and attach to that hero. Limit 1 per hero. Counts as a Condition attachment with the text: 'Forced: After a time counter is removed from the current quest, attached hero takes 1 damage.')",
                EncounterSet = "Weary Travelers",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each damage on the defending character.",
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "Ancient Forest",
                Id = "9472b675-605e-446e-a432-1146eec90023",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Forest." },
                Text = "While Ancient Forest is in the staging area, each Forest location in the staging area gets +1 Threat and +3 quest points. This ability does not stack with other copies of Ancient Forest.",
                QuestPoints = 5,
                EncounterSet = "Ancient Forest",
                Shadow = "Shadow: Defending player exhausts a character he controls.",
                Threat = 2,
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "Tangled Woods",
                Id = "9472b675-605e-446e-a432-1146eec90024",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Forest." },
                Text = "While Tangled Woods is in the staging area, each Forest location in play gains: 'Travel: Exhaust a hero to travel here.' This abilitydoes not stack with other copies of Tangled Woods.",
                QuestPoints = 4,
                EncounterSet = "Ancient Forest",
                Threat = 2,
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "Turned Around",
                Id = "9472b675-605e-446e-a432-1146eec90025",
                CardType = CardType.Treachery,
                Quantity = 3,
                Text = "When Revealed: Either return the active location to the staging area, or remove 1 time counter from the current quest.",
                EncounterSet = "Ancient Forest",
                Shadow = "Shadow: If this attack is undefended, return the active location to the staging area.",
                Number = 25
            });
            Cards.Add(new Card() {
                Title = "Broken Lands",
                Id = "9472b675-605e-446e-a432-1146eec90026",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Highlands." },
                Text = "While Broken Lands is in the staging area, progress cannot be placed on locations in the staging area.",
                QuestPoints = 6,
                EncounterSet = "Broken Lands",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each location in the staging area.",
                Threat = 2,
                Number = 26
            });
            Cards.Add(new Card() {
                Title = "Territorial Wolf",
                Id = "9472b675-605e-446e-a432-1146eec90027",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Creature." },
                HitPoints = 3,
                Attack = 1,
                Defense = 1,
                Text = "While engaged with a player, Territorial Wolf gets +1 Attack for each location in the staging area.",
                EncounterSet = "Broken Lands",
                EngagementCost = 28,
                Shadow = "Shadow: Defending player exhausts a character he controls.",
                Threat = 3,
                Number = 27
            });
            Cards.Add(new Card() {
                Title = "Hard Pressed",
                Id = "9472b675-605e-446e-a432-1146eec90028",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Quantity = 3,
                Text = "When Revealed: Either deal 1 damage to each exhausted character, or remove 1 time counter from the current quest.",
                EncounterSet = "Broken Lands",
                Number = 28
            });
            Cards.Add(new Card() {
                Title = "Fight at the Ford - 1A",
                Id = "9472b675-605e-446e-a432-1146eec90029",
                CardType = CardType.Quest,
                Text = "Setup: Add The Islet to the staging area and attach Grima to that location. Each player searches the encounter deck and discard pile for 1 different Dunland enemy and adds it to the staging area. Shuffle the encounter deck.",
                EncounterSet = "Fight at the Ford",
                Quantity = 1,
                Setup = "st",
                Number = 29
            });
            Cards.Add(new Card() {
                Title = "Dunlending Attack - 2A",
                Id = "9472b675-605e-446e-a432-1146eec90031",
                CardType = CardType.Quest,
                Text = "When Revealed: Each player searches the encounter deck and discard pile for 1 different Dunland enemy and adds it to the staging area.",
                EncounterSet = "Fight at the Ford",
                Quantity = 1,
                Number = 30
            });
            Cards.Add(new Card() {
                Title = "Hold the Ford - 3A",
                Id = "9472b675-605e-446e-a432-1146eec90035",
                CardType = CardType.Quest,
                Text = "When Revealed: Each player searches the encounter deck and discard pile for 1 different Dunland enemy and adds it to the staging area.",
                EncounterSet = "Fight at the Ford",
                Quantity = 1,
                Number = 31
            });
            Cards.Add(new Card() {
                Title = "Grima",
                Id = "9472b675-605e-446e-a432-1146eec90037",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Rohan." },
                Willpower = 2,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                IsUnique = true,
                Text = "If free of encounters, the first player gains control of Grima. Action: Exhaust Grima to draw a card. If Grima leaves play, the players lose the game.",
                EncounterSet = "Fight at the Ford",
                Quantity = 1,
                Number = 32
            });
            Cards.Add(new Card() {
                Title = "The Islet",
                Id = "9472b675-605e-446e-a432-1146eec90038",
                CardType = CardType.Location,
                Traits = new List<string>() { "River." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Threat = 1,
                IsUnique = true,
                Text = "While The Islet is the active location, each Dunland enemy in play gets +1 Threat.",
                QuestPoints = 1,
                EncounterSet = "Fight at the Ford",
                Quantity = 1,
                VictoryPoints = 1,
                Number = 33
            });
            Cards.Add(new Card() {
                Title = "Fords of Isen",
                Id = "9472b675-605e-446e-a432-1146eec90039",
                CardType = CardType.Location,
                Traits = new List<string>() { "Valley." },
                Threat = 3,
                Text = "While Fords of Isen is in the staging area, players cannot gain resources from card effects.Forced: After Fords of Isen becomes the active location, each player with fewer than 5 cards in his hand draws cards until he has 5 in his hand.",
                QuestPoints = 4,
                EncounterSet = "Fight at the Ford",
                Quantity = 3,
                Number = 34
            });
            Cards.Add(new Card() {
                Title = "The King's Road",
                Id = "9472b675-605e-446e-a432-1146eec90040",
                CardType = CardType.Location,
                Traits = new List<string>() { "Road." },
                Threat = 2,
                Text = "While any player has 3 or more cards in hand, The King's Road gets +3 quest points.While any player has 5 or more cards in hand, The King's Road gains: 'When faced with the option to travel, the players must travel to The King's Road, if able.'",
                QuestPoints = 2,
                EncounterSet = "Fight at the Ford",
                Quantity = 2,
                Number = 35
            });
            Cards.Add(new Card() {
                Title = "Pillaging and Burning",
                Id = "9472b675-605e-446e-a432-1146eec90042",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player draws a card and raises his threat by 1 for each card in his hand.",
                Shadow = "Shadow: Discard an attachment you control (each attachment you control instead if this attack is undefended).",
                EncounterSet = "Fight at the Ford",
                Quantity = 2,
                Number = 36
            });
            Cards.Add(new Card() {
                Title = "Down from the Hills",
                Id = "9472b675-605e-446e-a432-1146eec90043",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must choose: Either remove 1 time counter from the current quest, or search the encounter deck and discard pile for a Dunland enemy, reveal it, and add it to the staging area. Shuffle the encounter deck. If any player has 5 or more cards in hand, this effect cannot be canceled.",
                EncounterSet = "Fight at the Ford",
                Quantity = 1,
                Number = 37
            });
            Cards.Add(new Card() {
                Title = "Gap of Rohan",
                Id = "9472b675-605e-446e-a432-1146eec90044",
                CardType = CardType.Location,
                Traits = new List<string>() { "River." },
                Threat = 2,
                Text = "While Gap of Rohan is in the staging area, Dunland enemies get +1 Attack.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. If this attack destroys a character, remove 1 time counter from the current quest.",
                QuestPoints = 3,
                EncounterSet = "Fight at the Ford",
                Quantity = 4,
                Number = 38
            });
            Cards.Add(new Card() {
                Title = "Ill Tidings",
                Id = "9472b675-605e-446e-a432-1146eec90081",
                CardType = CardType.Treachery,
                Text = "When Revealed: The first player draws this card into his hand. Cannot leave that player's hand. Then, if the first player has 5 or more cards in his hand, Ill Tidings gains surge.",
                EncounterSet = "Fight at the Ford",
                Quantity = 2,
                Number = 39
            });
            Cards.Add(new Card() {
                Title = "Dunland Prowler",
                Id = "9472b675-605e-446e-a432-1146eec90045",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Dunland." },
                EngagementCost = 30,
                Threat = 1,
                Attack = 2,
                Defense = 3,
                HitPoints = 3,
                Text = "While any player has 3 or more cards in hand, Dunland Prowler gains surge. While any player has 5 or more cards in hand, Dunland Prowler gets +1 Threat.",
                EncounterSet = "Dunland Rogues",
                Quantity = 3,
                Number = 40
            });
            Cards.Add(new Card() {
                Title = "Dunlending Bandit",
                Id = "9472b675-605e-446e-a432-1146eec90046",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Dunland." },
                EngagementCost = 33,
                Threat = 2,
                Attack = 1,
                Defense = 2,
                HitPoints = 4,
                Text = "While engaged with a player, Dunlending Bandit gets +1 Attack for each card in that player’s hand.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack (+2 Attack instead if the defending player has at least 3 cards in his hand).",
                EncounterSet = "Dunland Rogues",
                Quantity = 2,
                Number = 41
            });
            Cards.Add(new Card() {
                Title = "Dunland Raider",
                Id = "9472b675-605e-446e-a432-1146eec90047",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Dunland." },
                EngagementCost = 35,
                Threat = 3,
                Attack = 5,
                Defense = 3,
                HitPoints = 5,
                Text = "Forced: After Dunland Raider engages a player, that player must deal X damage divided among characters he controls where X equals the number of cards in his hand.",
                Shadow = "Shadow: Discard an attachment you control.",
                EncounterSet = "Dunland Rogues",
                Quantity = 2,
                Number = 42
            });
            Cards.Add(new Card() {
                Title = "Old Hatreds",
                Id = "9472b675-605e-446e-a432-1146eec90048",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Attach to the current quest stage. (Counts as a Condition attachment with the text: 'Limit 1 per quest. Forced: After a player draws any number of cards, raise his threat by 1.')",
                EncounterSet = "Dunland Rogues",
                Quantity = 2,
                Number = 43
            });
            Cards.Add(new Card() {
                Title = "Wild Men of Dunland",
                Id = "9472b675-605e-446e-a432-1146eec90049",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Attach to the current quest. (Counts as a Condition attachment with the text: 'Forced: After a player draws any number of cards, that player deals 1 damage to a character he controls.')",
                EncounterSet = "Dunland Warriors",
                Quantity = 2,
                Number = 44
            });
            Cards.Add(new Card() {
                Title = "Dunland Tribesman",
                Id = "9472b675-605e-446e-a432-1146eec90050",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Dunland." },
                EngagementCost = 37,
                Threat = 0,
                Attack = 4,
                Defense = 2,
                HitPoints = 3,
                Text = "When Revealed: Each player draws a card. Forced: After a player draws any number of cards, Dunland Tribesman gets +1 Threat until the end of the round.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack.",
                EncounterSet = "Dunland Warriors",
                Quantity = 2,
                Number = 45
            });
            Cards.Add(new Card() {
                Title = "Dunland Chieftan",
                Id = "9472b675-605e-446e-a432-1146eec90051",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Dunland." },
                EngagementCost = 35,
                Threat = 3,
                Attack = 5,
                Defense = 3,
                HitPoints = 6,
                Text = "Forced: After Dunland Chieftan engages a player, discard X cards from the top of the encounter deck where X is the number of cards in the engaged player’s hand. Put the topmost Dunland enemy discarded this way into play engaged with that player.",
                EncounterSet = "Dunland Warriors",
                Quantity = 2,
                Number = 46
            });
            Cards.Add(new Card() {
                Title = "Dunlending Berserker",
                Id = "9472b675-605e-446e-a432-1146eec90052",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Dunland." },
                EngagementCost = 23,
                Threat = 2,
                Attack = 2,
                Defense = 1,
                HitPoints = 4,
                Text = "While Dunland Berserker is engaged with a player it gains: 'Forced: After the engaged player draws any number of cards, Dunland Berserker makes an immediate attack.'",
                Shadow = "Shadow: Attacking enemy makes an additional attack after this one.",
                EncounterSet = "Dunland Warriors",
                Quantity = 3,
                Number = 47
            });
            Cards.Add(new Card() {
                Title = "Orders from Orthanc - 1A",
                Id = "9472b675-605e-446e-a432-1146eec90053",
                CardType = CardType.Quest,
                Text = "Setup: Each player removes the top 20 cards of his deck and places them aside, out of play. The first player shuffles Mugbush plus 1 Mugbush’s Guard for each other player in the game into an Orc deck. Place the remaining copies of Mugbush’s Guard aside, out of play. Shuffle 1 card from the Orc deck into each player’s out-of-play deck. Shuffle the encounter deck.",
                EncounterSet = "To Catch an Orc",
                Quantity = 1,
                VictoryPoints = 0,
                Number = 48
            });
            Cards.Add(new Card() {
                Title = "Searching for Mugbush - 2A",
                Id = "9472b675-605e-446e-a432-1146eec90055",
                CardType = CardType.Quest,
                EncounterSet = "To Catch an Orc",
                Quantity = 1,
                Number = 49
            });
            Cards.Add(new Card() {
                Title = "The Wizard's Prize - 3A",
                Id = "9472b675-605e-446e-a432-1146eec90057",
                CardType = CardType.Quest,
                EncounterSet = "To Catch an Orc",
                Quantity = 1,
                Number = 50
            });
            Cards.Add(new Card() {
                Title = "Mugbush",
                Id = "9472b675-605e-446e-a432-1146eec90059",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Keywords = new List<string>() { "Cannot have attachments." },
                EngagementCost = 1,
                Threat = 4,
                Attack = 7,
                Defense = 4,
                HitPoints = 8,
                IsUnique = true,
                Text = "Forced: After Mugbush is defeated, the first player attaches him to a hero he controls and exhausts that hero. (Counts as an Captive attachment with the text: 'Attached hero cannot ready. If attached hero leaves play, the players lose the game.')",
                EncounterSet = "To Catch an Orc",
                Quantity = 1,
                Number = 51
            });
            Cards.Add(new Card() {
                Title = "Mugbush's Guard",
                Id = "9472b675-605e-446e-a432-1146eec90060",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Orc.", " Uruk." },
                EngagementCost = 40,
                Threat = 3,
                Attack = 6,
                Defense = 3,
                HitPoints = 6,
                Text = "While a player controls a hero with Mugbush attached, Mugbush’s Guard engages that player.  Forced: After Mugbush’s Guard destroys a character, if Mugbush is attached to a hero, return Mugbush to the staging area.",
                EncounterSet = "To Catch an Orc",
                Quantity = 3,
                VictoryPoints = 3,
                Number = 52
            });
            Cards.Add(new Card() {
                Title = "Methedras",
                Id = "9472b675-605e-446e-a432-1146eec90061",
                CardType = CardType.Location,
                Traits = new List<string>() { "Mountain." },
                Threat = 2,
                Text = "While Methedras is the active location, each location in the staging area gets +1 Threat. Forced: After Methedras leaves play as an explored location, each player Searches 3.",
                QuestPoints = 3,
                EncounterSet = "To Catch an Orc",
                Quantity = 4,
                Number = 53
            });
            Cards.Add(new Card() {
                Title = "Orc Cave",
                Id = "9472b675-605e-446e-a432-1146eec90062",
                CardType = CardType.Location,
                Traits = new List<string>() { "Mountain.", " Cave." },
                Threat = 3,
                Text = "Forced: After Orc Cave leaves play as an explored location, the first player Searches 5. Travel: Discard the top X cards of the encounter deck to travel here. X is the number of players in the game. Add each Orc enemy discarded this way to the staging area.",
                QuestPoints = 4,
                EncounterSet = "To Catch an Orc",
                Quantity = 3,
                Number = 54
            });
            Cards.Add(new Card() {
                Title = "Methedras Orc",
                Id = "9472b675-605e-446e-a432-1146eec90063",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Orc." },
                EngagementCost = 30,
                Threat = 3,
                Attack = 5,
                Defense = 3,
                HitPoints = 5,
                Text = "Forced: When Methedras Orc attacks, the defending player shuffles 1 random card from his hand into his out-of-play deck.",
                Shadow = "Shadow: If this attack destroys a character, remove 1 time counter from the current quest.",
                EncounterSet = "To Catch an Orc",
                Quantity = 3,
                Number = 55
            });
            Cards.Add(new Card() {
                Title = "Orc Territory",
                Id = "9472b675-605e-446e-a432-1146eec90064",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player searches the encounter deck and discard pile for a location and adds it to the staging area. If the total Threat in the staging area is less than the total Willpower of all characters committed to the quest, each Orc enemy engaged with a player makes an immediate attack.",
                EncounterSet = "To Catch an Orc",
                Quantity = 2,
                Number = 56
            });
            Cards.Add(new Card() {
                Title = "Mugbush's Lair",
                Id = "9472b675-605e-446e-a432-1146eec90065",
                CardType = CardType.Location,
                Traits = new List<string>() { "Mountain.", " Cave." },
                Threat = 4,
                Text = "Forced: At the end beginning of the encounter phase, reveal the top card of each player’s out-of-play deck. Add each revealed enemy to the staging area and discard the rest.",
                Shadow = "Shadow: Defending player discards an attachment he controls.",
                QuestPoints = 2,
                EncounterSet = "To Catch an Orc",
                Quantity = 2,
                Number = 57
            });
            Cards.Add(new Card() {
                Title = "Into the Woods - 1A",
                Id = "9472b675-605e-446e-a432-1146eec90066",
                CardType = CardType.Quest,
                Text = "Setup: Add Edge of Fangorn to the staging area and attach Mugbush to that location. Shuffle the encounter deck. Reveal X encounter cards where X equals the number of players in the game minus 1.",
                EncounterSet = "Fangorn Forest",
                Quantity = 1,
                Setup = "ss",
                Number = 58
            });
            Cards.Add(new Card() {
                Title = "Escape from Fangorn - 2A",
                Id = "9472b675-605e-446e-a432-1146eec90068",
                CardType = CardType.Quest,
                Text = "When Revealed: Each player searches the encounter deck and discard pile for a Huorn enemy and adds it to the staging area.",
                EncounterSet = "Fangorn Forest",
                Quantity = 1,
                Number = 59
            });
            Cards.Add(new Card() {
                Title = "The Angry Forest - 3A",
                Id = "9472b675-605e-446e-a432-1146eec90070",
                CardType = CardType.Quest,
                Shadow = "9472b675-605e-446e-a432-1146eec90071",
                EncounterSet = "Fangorn Forest",
                Quantity = 1,
                Number = 60
            });
            Cards.Add(new Card() {
                Title = "Edge of Fangorn",
                Id = "9472b675-605e-446e-a432-1146eec90072",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "Forest." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Threat = 2,
                QuestPoints = 2,
                Text = "Travel: Search the encounter deck and discard pile for a Huorn enemy and add it to the staging area to travel here (two Huorn enemies instead if there are 3 or more players in the game).",
                EncounterSet = "Fangorn Forest",
                Quantity = 1,
                VictoryPoints = 1,
                Number = 61
            });
            Cards.Add(new Card() {
                Title = "Mugbush",
                Id = "9472b675-605e-446e-a432-1146eec90073",
                CardType = CardType.Objective,
                IsUnique = true,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Keywords = new List<string>() { "Guarded." },
                Text = "Action: Exhaust a hero to claim this objective and attach it to that hero if it is free of encounters. Forced: After attached hero takes damage, return Mugbush to the top of the encounter deck.",
                EncounterSet = "Fangorn Forest",
                Quantity = 1,
                Number = 62
            });
            Cards.Add(new Card() {
                Title = "Angry Huorn",
                Id = "9472b675-605e-446e-a432-1146eec90074",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Huorn." },
                Keywords = new List<string>() { "Hinder.", " Cannot have attachments." },
                EngagementCost = 1,
                Threat = 2,
                Attack = 4,
                Defense = 4,
                HitPoints = 5,
                Text = "While Angry Huorn is engaged with a player it gains: 'Forced: At the beginning of each resource phase, the engaged player must raise his threat by 2.'",
                EncounterSet = "Fangorn Forest",
                Quantity = 4,
                Number = 63
            });
            Cards.Add(new Card() {
                Title = "Dark-hearted Huorn",
                Id = "9472b675-605e-446e-a432-1146eec90075",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Huorn." },
                Keywords = new List<string>() { "Hinder.", " Cannot have attachments." },
                EngagementCost = 38,
                Threat = 3,
                Attack = 5,
                Defense = 4,
                HitPoints = 8,
                Text = "While Dark-hearted Huorn is engaged with a player it gains: 'Forced: At the beginning of each resource phase, Dark-hearted Huorn makes an attack.'",
                EncounterSet = "Fangorn Forest",
                Quantity = 3,
                Number = 64
            });
            Cards.Add(new Card() {
                Title = "Deadly Huorn",
                Id = "9472b675-605e-446e-a432-1146eec90076",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Huorn." },
                Keywords = new List<string>() { "Hinder.", " Cannot have attachments." },
                EngagementCost = 34,
                Threat = 2,
                Attack = 3,
                Defense = 4,
                HitPoints = 6,
                Text = "While Deadly Huorn is engaged with a player it gains: 'Forced: At the beginning of each resource phase, the engaged player must deal 3 damage to a character he controls.'",
                EncounterSet = "Fangorn Forest",
                Quantity = 3,
                Number = 65
            });
            Cards.Add(new Card() {
                Title = "Heart of Fangorn",
                Id = "9472b675-605e-446e-a432-1146eec90077",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Threat = 3,
                QuestPoints = 3,
                Text = "While Heart of Fangorn is in the staging area, each player cannot refresh more than 5 characters during the refresh phase.",
                EncounterSet = "Fangorn Forest",
                Quantity = 3,
                Number = 66
            });
            Cards.Add(new Card() {
                Title = "The Forest's Malice",
                Id = "9472b675-605e-446e-a432-1146eec90078",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each Huorn enemy engaged with a player makes an immediate attack. If no attack was made as a result of this effect, each player must search the encounter deck and discard pile for a Huorn enemy and put it into play engaged with him. This effect cannot be canceled.",
                EncounterSet = "Fangorn Forest",
                Quantity = 4,
                Number = 67
            });
            Cards.Add(new Card() {
                Title = "Erkenbrand",
                Id = "5dbab8f9-4cc4-4fbf-a1d9-9941da3412bd",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                IsUnique = true,
                Keywords = new List<string>() { "Sentinel." },
                ThreatCost = 10,
                Quantity = 1,
                Traits = new List<string>() { "Rohan.", " Warrior." },
                Willpower = 1,
                HitPoints = 4,
                Attack = 2,
                Defense = 3,
                Text = "While Erkenbrand is defending, he gains: 'Response: Deal 1 damage to Erkenbrand to cancel a shadow effect just triggered.'",
                Number = 68
            });
            Cards.Add(new Card() {
                Title = "Blue Mountain Trader",
                Id = "8cf19b4d-4d9d-44b9-bbbb-46eea8284248",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Dwarf." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 0,
                Defense = 1,
                Text = "Action: Choose a player. That player gains control of Blue Mountain Trader. Then, that player moves 1 resource from the resource pool of a hero he controls to the resource pool of a hero you control, or Blue Mountain Trader is discarded.",
                Number = 69
            });
            Cards.Add(new Card() {
                Title = "Shadows Gave Way",
                Id = "9c88a19b-201a-42bc-a13b-9a147370e3fd",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Quantity = 3,
                Text = "You must use resources from 3 different heroes' pools to pay for this card.Action: Discard each shadow card in play.",
                Number = 70
            });
            Cards.Add(new Card() {
                Title = "O Lorien!",
                Id = "6fc7eff2-8a4e-47fb-9083-5665c2519acc",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Song." },
                Willpower = 1,
                HitPoints = 1,
                Attack = 1,
                Defense = 0,
                Text = "Action: Exhaust O Lorien! to lower the cost of the next Silvan ally played this phase by 1 (to a minimum of 1).",
                Number = 71
            });
            Cards.Add(new Card() {
                Title = "Swift and Silent",
                Id = "a3dc6ffc-535b-4397-b86e-10ca4c754c55",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: Ready a hero you control. Then, if your threat is 20 or less, return this card to your hand instead of discarding it (limit once per round).",
                Number = 72
            });
            Cards.Add(new Card() {
                Title = "Lead the Charge",
                Id = "adad6e74-e0af-42bf-baed-1e9c8a2edfc3",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: Take control of the first player token and draw 1 card.  (The first player token still passes to the next player during the refresh phase.)",
                Number = 73
            });
            Cards.Add(new Card() {
                Title = "Celduin Traveller",
                Id = "b997e14e-61c8-41bf-a3cb-a1f4fd9fff31",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Keywords = new List<string>() { "Secrecy 2." },
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Dale." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 0,
                Defense = 1,
                Text = "Response: After Celduin Traveller enters play, look at the top card of the encounter deck.  If it is a location, discard it.",
                Number = 74
            });
            Cards.Add(new Card() {
                Title = "Messenger of Elrond",
                Id = "8b96dc25-2626-4347-a685-bbe7d6866513",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Choose a player. That player takes 1 card from their hand and adds it to another player's hand.",
                Number = 75
            });
            Cards.Add(new Card() {
                Title = "Feigned Voices",
                Id = "28eb709e-1ea5-4f36-b768-3719e32cba89",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Combat Action: Return a Silvan ally you control to your hand to choose an enemy engaged with a player. That enemy cannot attack that player this phase.",
                Number = 76
            });
            Cards.Add(new Card() {
                Title = "Galadhon Warrior",
                Id = "16752711-90cd-4cd1-98bc-fdfdc01f255b",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Keywords = new List<string>() { "Ranged." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Silvan.", " Warrior." },
                Willpower = 0,
                HitPoints = 1,
                Attack = 2,
                Defense = 0,
                Text = "Response: After Galadhon Warrior enters play, deal 1 damage to an enemy not engaged with you.",
                Number = 77
            });
            Cards.Add(new Card() {
                Title = "Ride Them Down",
                Id = "dadcae2a-9180-4792-91c2-56bccd3d9593",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Quantity = 3,
                Text = "Response: After questing successfully, cancel all progress that would be placed on the quest. Then, place X damage on a non-unique enemy in the staging area. X is the amount of progress that would have been placed on the quest.",
                Number = 78
            });
            Cards.Add(new Card() {
                Title = "Bow of the Galadhrim",
                Id = "3780a628-e83d-4945-adba-1df2c327e019",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Item.", " Weapon." },
                Text = "Attach to a Noldor or Silvan character. Restricted.Attacking character gets +1 Attack. (+2 Attack instead if attacking an enemy not engaged with you.",
                Number = 79
            });
            Cards.Add(new Card() {
                Title = "Tighten Our Belts",
                Id = "15c7ac0e-607c-4680-ba7a-5cf3fa67c536",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Refresh Action: Each hero that did not spend any resources this round gains 1 resource. (Limit 1 Tighten Our Belts per round.)",
                Number = 80
            });
            Cards.Add(new Card() {
                Title = "Celeborn",
                Id = "26342f1a-615e-4d02-849d-aab4379e75a6",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                IsUnique = true,
                ThreatCost = 11,
                Quantity = 1,
                Traits = new List<string>() { "Silvan.", " Noble." },
                Willpower = 3,
                HitPoints = 4,
                Attack = 2,
                Defense = 2,
                Text = "Response: After a Silvan ally enters play, that ally gets +1 Willpower, +1 Attack, and +1 Defense until the end of the round.",
                Number = 81
            });
            Cards.Add(new Card() {
                Title = "Lembas",
                Id = "8fe7e3db-146e-4884-abe6-528817feb264",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Item." },
                Text = "In order to play Lembas, exhaust a Noldor or Silvan hero you control. Attach to a hero. Action: Discard Lembas to ready attached hero and heal 3 damage from it.",
                Number = 82
            });
            Cards.Add(new Card() {
                Title = "Galadhrim Weaver",
                Id = "743f63a9-d270-4124-83ef-6c93fe976187",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Silvan.", " Craftsman." },
                Willpower = 1,
                HitPoints = 1,
                Attack = 0,
                Defense = 0,
                Text = "Response: After Galadhrim Weaver enters play, search the top 5 cards of your deck for an event card and add it to your hand. Shuffle the rest back into your deck.",
                Number = 83
            });
            Cards.Add(new Card() {
                Title = "Fangorn Tree-Herder",
                Id = "f7c0eda7-eb18-4602-9b10-4751ecbb3997",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Ent." },
                Willpower = 2,
                HitPoints = 3,
                Attack = 1,
                Defense = 2,
                Text = "Fangorn Tree-Herder enters play exhausted. Cannot have restricted attachments. Response: After Fangorn Tree-Herder commits to the quest, look at the top card of your deck. If that card is an Ent ally, add it to your hand. If it is not, put it on the bottom of your deck.",
                Number = 84
            });
            Cards.Add(new Card() {
                Title = "Wandering Ent",
                Id = "1c227813-fab1-4f29-a8d1-80a518cb65be",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Ent." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 1,
                Defense = 1,
                Text = "Wandering Ent enters play exhausted. Cannot have restricted attachments.",
                Number = 85
            });
            Cards.Add(new Card() {
                Title = "Ent-draught",
                Id = "d77f1559-f2fb-4909-8008-be1b1116bb6d",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Keywords = new List<string>() { "Play only if you control an Ent." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Item." },
                Text = "Attach to a character. Attached character gets +1 hit point. Response: After you attach Ent-draught to a character, heal up to 3 damage from that character.",
                Number = 86
            });
            Cards.Add(new Card() {
                Title = "The Tree People",
                Id = "6311a402-9667-4d9d-bff3-3d40df681268",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Return a Silvan ally you control to your hand to search the top 5 cards of your deck for a Silvan ally. Put that ally into play and shuffle the other cards back into your deck.",
                Number = 87
            });
            Cards.Add(new Card() {
                Title = "Naith Guide",
                Id = "10cf03e9-e63d-4ee2-a776-8ec7919e3b16",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Silvan.", " Scout." },
                Willpower = 1,
                HitPoints = 1,
                Attack = 1,
                Defense = 0,
                Text = "Response: After Naith Guide enters play, choose a hero. That hero does not exhaust to quest this round.",
                Number = 88
            });
            Cards.Add(new Card() {
                Title = "Elven Cloak",
                Id = "82f64538-4a0b-4da1-a473-9987b35fcf74",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Item." },
                Text = "Attach to a Noldor or Silvan character. Restricted. Limit 1 per character.Attached character gets +1 Defense (+2 instead if the active location is a Forest).",
                Number = 89
            });
            Cards.Add(new Card() {
                Title = "Don't Be Hasty!",
                Id = "376cceaf-aee5-4732-a830-64f4d6a4536b",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Response: When an encounter card is revealed but before it resolves, choose a character committed to the quest. Ready that character and remove it from the quest.",
                Number = 90
            });
            Cards.Add(new Card() {
                Title = "Ithilien Lookout",
                Id = "e6aeb80b-be28-48b7-9094-5b8a6e56a002",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Keywords = new List<string>() { "Secrecy 2." },
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 1,
                Defense = 0,
                Text = "Response: After Ithilien Lookout enters play, examine the top card of the encounter deck. If it is an enemy, discard it.",
                Number = 91
            });
            Cards.Add(new Card() {
                Title = "Noiseless Movement",
                Id = "ec82ff53-2718-46f2-8a0b-5bdc0c7b1eef",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: Choose an enemy in the staging area. That enemy does not make an engagement check this round. Then, if your threat is 20 or less, return this card to your hand instead of discarding it (limit once per round).",
                Number = 92
            });
            Cards.Add(new Card() {
                Title = "Island Amid Perils",
                Id = "91775ff2-857f-46e3-b7d8-120a443dd2d6",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Return a Silvan ally you control to your hand to choose a player. Reduce that player’s threat by X where is the cost of the ally returned to your hand.",
                Number = 93
            });
            Cards.Add(new Card() {
                Title = "Galadriel",
                Id = "620162bd-2454-4097-82f3-e70ddf3a56e8",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                IsUnique = true,
                Keywords = new List<string>() { "Galadriel cannot quest, attack, or defend." },
                ThreatCost = 9,
                Quantity = 1,
                Traits = new List<string>() { "Noldor.", " Noble." },
                Willpower = 4,
                HitPoints = 4,
                Attack = 0,
                Defense = 0,
                Text = "Allies you control do not exhaust to quest the round they enter play. Action: Exhaust Galadriel to choose a player. That player lowers his threat by 1 and draws 1 card. (Limit once per round.)",
                Number = 94
            });
            Cards.Add(new Card() {
                Title = "Idraen",
                Id = "e4c1a9a1-9614-4be5-946d-36c32f92922a",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                IsUnique = true,
                ThreatCost = 11,
                Quantity = 1,
                Traits = new List<string>() { "Dúnedain.", " Scout." },
                Willpower = 2,
                HitPoints = 4,
                Attack = 3,
                Defense = 2,
                Text = "Response: After a location is explored, ready Idraen.",
                Number = 95
            });
            Cards.Add(new Card() {
                Title = "Galadriel's Handmaiden",
                Id = "4c655190-54b5-4bbe-aaed-62d01df7f8c1",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Silvan." },
                Willpower = 2,
                HitPoints = 1,
                Attack = 0,
                Defense = 1,
                Text = "Response: After Galadriel’s Handmaiden enters play, choose a player. That player lowers his threat by 1.",
                Number = 96
            });
            Cards.Add(new Card() {
                Title = "Dúnedain Lookout",
                Id = "b89acbdd-a8e7-4a09-ba80-0f21c3d48c7e",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Dúnedain.", " Ranger." },
                Willpower = 2,
                HitPoints = 2,
                Attack = 1,
                Defense = 1,
                Text = "Response: When you play Dúnedain Lookout, you may give it Doomed 2 to place 1 progress on each location in play.",
                Number = 97
            });
            Cards.Add(new Card() {
                Title = "Defender of the Naith",
                Id = "01998302-0492-410f-a2bf-453eea0bdd39",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                Keywords = new List<string>() { "Sentinel." },
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Silvan.", " Warrior." },
                Willpower = 0,
                HitPoints = 2,
                Attack = 1,
                Defense = 2,
                Text = "Response: After a Silvan ally you control leaves play, ready Defender of the Naith.",
                Number = 98
            });
            Cards.Add(new Card() {
                Title = "Mirror of Galadriel",
                Id = "fcd534f4-7293-41fb-ab78-6bcfdb3a8613",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to Galadriel." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Artifact.", " Item." },
                Text = "Action: Exhaust Mirror of Galadriel to search your deck for a card and add it to your hand. Then, discard a random card from your hand.",
                Number = 99
            });
            Cards.Add(new Card() {
                Title = "Stroke of Luck",
                Id = "9dfd9b18-d56c-4a5e-a399-ea79a53673c8",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                Keywords = new List<string>() { "Doomed X." },
                ResourceCost = 1,
                Quantity = 3,
                Text = "Response: Cancel X damage just dealt to a character.",
                Number = 100
            });
            Cards.Add(new Card() {
                Title = "Defiance",
                Id = "63e0bcbc-d730-46d9-8335-70fc9b82b676",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Skill." },
                Text = "Attach to a character/hero with Sentinel. Limit 1 per hero.Cancel the first point of damage dealt to attached character/hero each round.",
                Number = 101
            });
            Cards.Add(new Card() {
                Title = "Courage Awakened",
                Id = "edae0a93-7f21-4cf0-9967-10a35bd52a80",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: Choose a hero. That hero gets +2 Willpower until the end of the phase. Then, if your threat is 20 or less, return this card to your hand instead of discarding it (limit once per round).",
                Number = 102
            });
            Cards.Add(new Card() {
                Title = "Virtue of the Elves",
                Id = "d702f894-61de-4621-a0f4-591082477d60",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                Keywords = new List<string>() { "Doomed 3." },
                ResourceCost = 5,
                Quantity = 3,
                Text = "Action: Each player chooses a hero or ally in their discard pile and puts it into play, under its owner’s control.",
                Number = 103
            });
            Cards.Add(new Card() {
                Title = "Henneth Annûn Guard",
                Id = "c5a7bffb-366c-4c44-9c58-359c9ea8b8de",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Willpower = 0,
                HitPoints = 2,
                Attack = 2,
                Defense = 2,
                Text = "Response: When you play Henneth Annûn Guard, you may give it Doomed 1 to choose an enemy. Until the end of the round, the chosen enemy has -2 Attack.",
                Number = 104
            });
            Cards.Add(new Card() {
                Title = "Free to Choose",
                Id = "0c2e6d1b-4cfc-4c1b-b672-163c01e14a57",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Keywords = new List<string>() { "Secrecy 2." },
                ResourceCost = 2,
                Quantity = 3,
                Text = "Response: After your threat is raised by an encounter card effect, reduce your threat by an equal amount.",
                Number = 105
            });
            Cards.Add(new Card() {
                Title = "High Blood",
                Id = "e8625f28-0204-429f-9a06-bd9c23a0828f",
                CardType = CardType.Attachment,
                Keywords = new List<string>() { "Attach to a Noble hero." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Skill." },
                Text = "Response: After attached hero gains any number of resources from a card effect, ready attached hero. (Limit once per phase).",
                Number = 106
            });
            Cards.Add(new Card() {
                Title = "Rivendell Scout",
                Id = "e8625f28-0204-429f-9a06-bd9c23a0829f",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Keywords = new List<string>() { "Secrecy 2." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Noldor.", " Scout." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 1,
                Defense = 0,
                Number = 107
            });
            Cards.Add(new Card() {
                Title = "Mablung",
                Id = "57b3f5fb-71c8-4844-b59e-9f67182b88db",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                ThreatCost = 10,
                Quantity = 1,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Willpower = 2,
                HitPoints = 4,
                Attack = 2,
                Defense = 2,
                Text = "Response: After you optionally engage an enemy, add 1 resource to Mablung’s resource pool.",
                Number = 108
            });
            Cards.Add(new Card() {
                Title = "Haldir of Lorien",
                Id = "80450689-4e6e-4ed8-ad86-f2613930a2df",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                IsUnique = true,
                Keywords = new List<string>() { "Ranged." },
                ThreatCost = 9,
                Quantity = 1,
                Traits = new List<string>() { "Silvan.", " Scout." },
                Willpower = 2,
                HitPoints = 3,
                Attack = 2,
                Defense = 2,
                Text = "While you are engaged with an enemy, Haldir of Lorien does not exhaust to quest.",
                Number = 109
            });
            Cards.Add(new Card() {
                Title = "Rumil",
                Id = "e171073a-1f48-45cb-8db6-48cbd1ae8365",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                Keywords = new List<string>() { "Ranged." },
                ResourceCost = 4,
                Quantity = 3,
                Traits = new List<string>() { "Silvan.", " Scout." },
                Willpower = 2,
                HitPoints = 3,
                Attack = 2,
                Defense = 1,
                Text = "Response: After you play Rumil from your hand, choose an enemy engaged with you. Deal X damage to that enemy where X is the number of Ranged characters you control.",
                Number = 110
            });
            Cards.Add(new Card() {
                Title = "Gwaihir",
                Id = "88bd4a75-f2e1-417d-99ee-bbac847beebc",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                Keywords = new List<string>() { "Gwaihir cannot have restricted attachments." },
                ResourceCost = 5,
                Quantity = 3,
                Traits = new List<string>() { "Creature.", " Eagle.", " Noble." },
                Willpower = 2,
                HitPoints = 4,
                Attack = 3,
                Defense = 1,
                Text = "Response: After Gwaihir enters play, search your discard pile for an Eagle ally and put it into play under your control. At the end of the round, add that Eagle ally to your hand.",
                Number = 111
            });
            Cards.Add(new Card() {
                Title = "Firefoot",
                Id = "dc44e79e-41b1-44cf-8acc-7fdfada789f7",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a Tactics hero." },
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Mount." },
                Text = "Attached hero gets +2 Attack.If attached hero is Éomer, while he is attacking alone, excess damage from this attack may be dealt to a non-unique enemy engaged with you.",
                Number = 112
            });
            Cards.Add(new Card() {
                Title = "Charge!",
                Id = "4c72adaa-8693-4ffc-8c59-ea05545b4343",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Quantity = 3,
                Text = "Action: Until the end of the phase, each character with a Mount attachment gets +2 Attack.",
                Number = 113
            });
            Cards.Add(new Card() {
                Title = "Elven Mail",
                Id = "5ae714ca-79ce-4690-8b4d-9a4ab09fe87a",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Keywords = new List<string>() { "Restricted." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Item.", " Armor." },
                Text = "Attach to a Noldor or Silvan character. Attached character gets +2 hit points and gains Sentinel.",
                Number = 114
            });
            Cards.Add(new Card() {
                Title = "Keen Eyes",
                Id = "166a6d23-f6a0-4863-b327-2300c857eea7",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a Scout character." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Skill." },
                Text = "While attached character is committed to a quest, add 1 progress to the first location revealed by the encounter deck each round.",
                Number = 115
            });
            Cards.Add(new Card() {
                Title = "Herald of Anórien",
                Id = "b125c1c1-9131-4314-89cc-2f59a01b8ce3",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Gondor." },
                Willpower = 1,
                HitPoints = 1,
                Attack = 0,
                Defense = 1,
                Text = "Response: When you play Herald of Anórien, you may give it Doomed 2 to choose a player. That player may put into play 1 ally from his hand. Return that ally to its owners hand at the end of the round, if it is still in play.",
                Number = 116
            });
            Cards.Add(new Card() {
                Title = "Vigilant Watch",
                Id = "9a9b1033-6b07-466b-aec0-77cedffde63b",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a Ranger character." },
                ResourceCost = 0,
                Quantity = 3,
                Traits = new List<string>() { "Skill." },
                Text = "Response: After attached character commits to the quest, look at the top card of the encounter deck. If it is an enemy, draw a card.",
                Number = 117
            });
            Cards.Add(new Card() {
                Title = "Yrch!",
                Id = "a66c2073-78c9-4c53-b93f-bbe21c363c08",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: Return a Silvan ally you control to your hand to deal 1 damage to each enemy engaged with you.",
                Number = 118
            });
            Cards.Add(new Card() {
                Title = "Mirkwood Woodsman",
                Id = "2ba078c8-c212-4d82-8e73-225af9360d04",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Woodsman." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 0,
                Defense = 0,
                Text = "Response: When you play Mirkwood Woodsman, you may give it Doomed 1 to choose a card in the staging area. Until the end of the round, the chosen card does not contribute its Threat.",
                Number = 119
            });
            Cards.Add(new Card() {
                Title = "Unyielding",
                Id = "494fcf11-1aee-4680-ae33-c0210ddc3180",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Response: When an enemy makes an attack, declare an exhausted Sentinel character as the defender against that attack.",
                Number = 120
            });
            Cards.Add(new Card() {
                Title = "Warden of Helm's Deep",
                Id = "3e014d48-1346-4ffb-a566-370d48d446b7",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Keywords = new List<string>() { "Sentinel." },
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Rohan.", " Warrior." },
                Willpower = 0,
                HitPoints = 2,
                Attack = 1,
                Defense = 3,
                Number = 121
            });
            Cards.Add(new Card() {
                Title = "Sworn to Serve",
                Id = "5fb1b599-e9a6-4391-bc14-dcfe6ee9502b",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                Keywords = new List<string>() { "Attach to an ally you control." },
                ResourceCost = 0,
                Quantity = 3,
                Text = "The first player gains control of attached character.",
                Number = 122
            });
            Cards.Add(new Card() {
                Title = "Lust for Battle",
                Id = "bf954d66-1770-453f-ab93-9d1192cb16ec",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a Warrior character." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Skill." },
                Text = "Response: After you optionally engage an enemy, attached character gets +1 Attack and +1 Defense until the end of the round.",
                Number = 123
            });
            Cards.Add(new Card() {
                Title = "Orophin",
                Id = "275dce87-7eb4-475b-ab30-75ae3bf565f7",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                IsUnique = true,
                Keywords = new List<string>() { "Ranged." },
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Silvan.", " Scout." },
                Willpower = 2,
                HitPoints = 2,
                Attack = 1,
                Defense = 1,
                Text = "Response: After Orophin enters play, return a Silvan ally from your discard pile to your hand.",
                Number = 124
            });
            Cards.Add(new Card() {
                Title = "Nenya",
                Id = "cd953d5f-081d-4385-abfd-e17662c17281",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to Galadriel." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Artifact.", " Item.", " Ring." },
                Text = "Galadriel gains a Lore resource icon. Quest Action: Exhaust Nenya and Galadriel to add her Willpower to another character's Willpower until the end of the phase.",
                Number = 125
            });
            Cards.Add(new Card() {
                Title = "Leaf Brooch",
                Id = "39e2274b-eff2-45ad-84bb-920098e0e285",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                Keywords = new List<string>() { "Attach to a hero.", " Limit one per hero." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Item." },
                Text = "The first event card you play each round that matches the attached hero's sphere gains Secrecy 1.",
                Number = 126
            });
            Cards.Add(new Card() {
                Title = "The White Council",
                Id = "1c449ee7-e485-4bfb-bb27-c2dcd680b830",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                ResourceCost = 0,
                Quantity = 3,
                Text = "X is the number of players in the game. Action: Each player chooses one: ready a character, discard a shadow card, look at the top card of the encounter deck, or shuffle 1 card from your discard pile into your deck.",
                Number = 127
            });
        }
    }
}
