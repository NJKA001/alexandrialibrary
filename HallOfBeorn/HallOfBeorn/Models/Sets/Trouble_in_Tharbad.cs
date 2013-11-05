using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TroubleinTharbad : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "Swift Escort - 1A",
                Id = "9472b675-605e-446e-a432-1146eec93001",
                CardType = CardType.Quest,
                Text = "Setup: Set Bellach and The Crossing of Tharbad aside, out of play. Search the encounter deck for 1 copy of Spy From Mordor per player, and add them to the staging area. Make Tharbad Square the active location. The first player takes control of Nalir, as an ally. Shuffle the encounter deck.",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 1,
                Setup = "ttttttlt",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Escape From Tharbad - 2A",
                Id = "9472b675-605e-446e-a432-1146eec93003",
                CardType = CardType.Quest,
                Text = "When Revealed: Add Bellach and The Crossing of Tharbad to the staging area. Then, each player searches the encounter deck for an Orc enemy and reveals it, adding it to the staging area.",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Nalir",
                Id = "9472b675-605e-446e-a432-1146eec93005",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Dwarf." },
                Keywords = new List<string>() { "The first player gains control of Nalir." },
                Willpower = 0,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                IsUnique = true,
                Text = "Forced: At the beginning of the refresh phase, raise your threat by 1 for each player in the game.&#10;&#10;If Nalir leaves play, the players lose the game.",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Tharbad Square",
                Id = "9472b675-605e-446e-a432-1146eec93006",
                CardType = CardType.Location,
                Traits = new List<string>() { "City." },
                Keywords = new List<string>() { "Players cannot reduce their threat." },
                Threat = 1,
                Text = "While Tharbad Square is in the victory display, the current quest gains 'Forced: After a player card effect reduces a player's threat, remove it from the game.'",
                QuestPoints = 4,
                EncounterSet = "Trouble in Tharbad",
                Quantity = 1,
                VictoryPoints = 4,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Bellach",
                Id = "9472b675-605e-446e-a432-1146eec93007",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Mordor.", " Spy." },
                EngagementCost = 50,
                Threat = 0,
                Attack = 5,
                Defense = 4,
                HitPoints = 7,
                IsUnique = true,
                Text = "X is the number of players in the game.Orc enemies get -30 engagement cost and +1 Threat.&#10;&#10;Forced: After Bellach is destroyed, shuffle him into the encounter deck.",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 1,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "The Crossing of Tharbad",
                Id = "9472b675-605e-446e-a432-1146eec93008",
                CardType = CardType.Location,
                Traits = new List<string>() { "City.", " Ruins." },
                Threat = 2,
                IsUnique = true,
                Text = "Immune to player card effects. Cannot leave the staging area.&#10;&#10;The Crossing of Tharbad gets +2 quest points for each player in the game.&#10;&#10;When The Crossing of Tharbad is explored, the players win the game.",
                QuestPoints = 10,
                EncounterSet = "Trouble in Tharbad",
                Quantity = 1,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Spy From Mordor",
                Id = "9472b675-605e-446e-a432-1146eec93009",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Mordor.", " Spy." },
                EngagementCost = 40,
                Threat = 2,
                Attack = 3,
                Defense = 2,
                HitPoints = 4,
                Text = "Forced: When Spy From Mordor attacks, remove 1 time counter from the current quest.",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 4,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Bellach's Marauders",
                Id = "9472b675-605e-446e-a432-1146eec93010",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Orc." },
                EngagementCost = 30,
                Threat = 3,
                Attack = 5,
                Defense = 4,
                HitPoints = 5,
                Text = "While Bellach's Marauders is engaged with a player, it gains, 'Forced: After a time counter is removed from the current quest, deal Bellach's Marauders 2 shadow cards.'",
                Shadow = "Shadow: If this attack destroys a character, remove 1 time counter from the current quest.",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Constant Tail",
                Id = "9472b675-605e-446e-a432-1146eec93011",
                CardType = CardType.Treachery,
                Text = "When Revealed: Return all engaged enemies to the staging area. Until the end of the round, players cannot optionally engage enemies. If no enemies were returned to the staging area by this effect, this card gains surge.",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Conspicuous Lot",
                Id = "9472b675-605e-446e-a432-1146eec93012",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player raises his threat by 1 for each ally he controls. Then, if any player's threat is 20 or less, this card gains surge.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. (+2 Attack instead if the defending player's threat is less than 20.)",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "City of Peril",
                Id = "9472b675-605e-446e-a432-1146eec93013",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must either discard the ally he controls with the highest printed cost, or search the encounter deck and discard pile for an enemy and add it to the staging area.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. (+2 Attack instead if the defending player's threat is less than 20.)",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Cornered",
                Id = "9472b675-605e-446e-a432-1146eec93014",
                CardType = CardType.Treachery,
                Text = "When Revealed: The first player either removes 1 time counter from the current quest or each enemy gets -20 engagement cost and +1 Attack until the end of the round.",
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Streets of Tharbad",
                Id = "9472b675-605e-446e-a432-1146eec93015",
                CardType = CardType.Location,
                Traits = new List<string>() { "City." },
                Threat = 3,
                Text = "Progress cannot be placed on Streets of Tharbad while it is in the staging area.&#10;&#10;While Streets of Tharbad is the active location, enemies have an engagement cost of 0.",
                QuestPoints = 1,
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Tharbad Hideout",
                Id = "9472b675-605e-446e-a432-1146eec93016",
                CardType = CardType.Location,
                Traits = new List<string>() { "City." },
                Keywords = new List<string>() { "Surge." },
                Threat = 1,
                Text = "While Tharbad Hideout is the active location, it gains: 'Time counters cannot be removed from the current quest.&#10;&#10;Forced: At the beginning of the quest phase, place 1 progress on Tharbad Hideout.'",
                QuestPoints = 1,
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Ruins of the Second Age",
                Id = "9472b675-605e-446e-a432-1146eec93017",
                CardType = CardType.Location,
                Traits = new List<string>() { "City.", " Ruins." },
                Threat = 1,
                Text = "While Ruins of the Second Age is in the staging area, City locations get +1 Threat. &#10;&#10;While Ruins of the Second Age is the active location, deal engaged enemies an additional shadow card at the beginning of the combat phase.",
                QuestPoints = 5,
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Seedy Inn",
                Id = "9472b675-605e-446e-a432-1146eec93018",
                CardType = CardType.Location,
                Traits = new List<string>() { "City." },
                Threat = 1,
                Text = "When Revealed: Search the encounter deck and discard pile for a Spy enemy and add it to the staging area. Shuffle the encounter deck.",
                Shadow = "Shadow: Raise the defending player's threat by X, where X is the amount of damage dealt by this attack.",
                QuestPoints = 4,
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Hidden Alleyway",
                Id = "9472b675-605e-446e-a432-1146eec93019",
                CardType = CardType.Location,
                Traits = new List<string>() { "City." },
                Threat = 3,
                Text = "Travel: Raise each player's threat by X to travel here. X is the number of enemies in play. &#10;&#10;Forced: After traveling to Hidden Alleyway, add 1 time counter to the current quest. ",
                QuestPoints = 5,
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Decrepit Rooftops",
                Id = "9472b675-605e-446e-a432-1146eec93020",
                CardType = CardType.Location,
                Traits = new List<string>() { "City." },
                Threat = 2,
                Text = "Forced: After traveling to Decrepit Rooftops, return all engaged enemies to the staging area. &#10;&#10;Forced: While Decrepit Rooftops is the active location, enemies get +2 Threat and do not make engagement checks.",
                QuestPoints = 3,
                EncounterSet = "Trouble in Tharbad",
                Quantity = 2,
                Number = 18
            });
        }
    }
}
