using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class CelebrimborsForge : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "The Ruins of Ost-in-edhil - 1A",
                Id = "9472b675-605e-446e-a432-1146eec95001",
                CardType = CardType.Quest,
                Setup = "ssst",
                Quantity = 1,
                Text = "Setup: Add Bellach, The Orc's Search, and The Secret Chamber to the staging area. Attach Celebrimbor's Mould to The Secret Chamber. Each player adds a different Ost-in-edhil location to the staging area. Shuffle the encounter deck.",
                EncounterSet = "Celebrimbor's Forge",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "The Enemy's Servant - 2A",
                Id = "9472b675-605e-446e-a432-1146eec95003",
                CardType = CardType.Quest,
                Quantity = 1,
                Text = "When Revealed: Trigger each Scout effect in play. Bellach makes an attack against the player who controls Celebrimbor's Mould.",
                EncounterSet = "Celebrimbor's Forge",
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Collapsed Tower",
                Id = "9472b675-605e-446e-a432-1146eec95008",
                CardType = CardType.Location,
                Quantity = 2,
                Traits = new List<string>() { "Ost-in-edhil.", " Ruins." },
                Text = "Scour: Remove all progress from each location in play.",
                QuestPoints = 4,
                EncounterSet = "Celebrimbor's Forge",
                Shadow = "Shadow: Defending player discards an attachment he controls.",
                Threat = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "City Remains",
                Id = "9472b675-605e-446e-a432-1146eec95009",
                CardType = CardType.Location,
                Quantity = 4,
                Traits = new List<string>() { "Ost-in-edhil.", " Ruins." },
                Text = "Forced: After placing any amount of progress here, trigger the topmost Scour effect in the discard pile.Travel: Place 1 damage on this location to travel here.",
                QuestPoints = 3,
                EncounterSet = "Celebrimbor's Forge",
                Threat = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Ancient Foundation",
                Id = "9472b675-605e-446e-a432-1146eec95010",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Ost-in-edhil.", " Ruins." },
                Text = "Scour: Assign X damage among locations in play. X is the number of players in the game. This ability does not stack.",
                QuestPoints = 3,
                EncounterSet = "Celebrimbor's Forge",
                Shadow = "Shadow: Excess damage dealt by this attack must be dealt to the active location, if able.",
                Threat = 2,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Ruined Square",
                Id = "9472b675-605e-446e-a432-1146eec95011",
                CardType = CardType.Location,
                Quantity = 4,
                Traits = new List<string>() { "Ost-in-edhil.", " Ruins." },
                Text = "Forced: After Ruined Square enters the staging area, place 1 damage here.",
                QuestPoints = 2,
                EncounterSet = "Celebrimbor's Forge",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each damaged location in play.",
                Threat = 2,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "The Orc's Search",
                Id = "9472b675-605e-446e-a432-1146eec95012",
                CardType = CardType.Objective,
                IsUnique = true,
                Quantity = 1,
                Text = "When a location has damage equal to its printed quest points, place it under The Orc's Search.Forced: At the end of the refresh phase, raise each player's threat by 1 for each card underneath The Orc's Search.",
                EncounterSet = "Celebrimbor's Forge",
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Spy of Sauron",
                Id = "9472b675-605e-446e-a432-1146eec95013",
                CardType = CardType.Enemy,
                Quantity = 4,
                Traits = new List<string>() { "Mordor.", " Orc." },
                HitPoints = 4,
                Attack = 3,
                Defense = 2,
                Text = "When Revealed: Deal 1 damage to each location in the staging area.Scour: Deal 1 damage to the active location.",
                EncounterSet = "Celebrimbor's Forge",
                EngagementCost = 40,
                Shadow = "Shadow: Deal 1 damage to the active location.",
                Threat = 2,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Prowling Orc",
                Id = "9472b675-605e-446e-a432-1146eec95014",
                CardType = CardType.Enemy,
                Quantity = 2,
                Traits = new List<string>() { "Mordor.", " Orc." },
                HitPoints = 5,
                Attack = 5,
                Defense = 3,
                Text = "Forced: After Prowling Orc attacks, deal 1 damage to the active location. Scour: Prowling Orc engages the player who controls Celebrimbor's Mould and makes an immediate attack.",
                EncounterSet = "Celebrimbor's Forge",
                EngagementCost = 45,
                Threat = 2,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "The Secret Chamber",
                Id = "9472b675-605e-446e-a432-1146eec95015",
                CardType = CardType.Location,
                IsUnique = true,
                Keywords = new List<string>() { "X is the nuber of players in the game.", " Immune to player card effects." },
                Quantity = 1,
                Traits = new List<string>() { "Ost-in-edhil.", " Ruins." },
                Text = "Travel: Place 1 damage on this loaction to travel here.If The Secret Chamber is placed under The Orc's Search, the players lose the game.",
                QuestPoints = 6,
                EncounterSet = "Celebrimbor's Forge",
                Threat = 0,
                VictoryPoints = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Discovered!",
                Id = "9472b675-605e-446e-a432-1146eec95016",
                CardType = CardType.Treachery,
                Quantity = 3,
                Text = "When Revealed: Trigger each Scour effect in play.",
                EncounterSet = "Celebrimbor's Forge",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. If this attack destroys a character, raise each player's threat by 1 for each card under The Orc's Search.",
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Desecrated Ruins",
                Id = "9472b675-605e-446e-a432-1146eec95017",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Quantity = 1,
                Text = "When Revealed: Attach to a location in the staging area. Counts as a Condition attachment with the text: 'Forced: After a time counter is removed from the current quest, deal 1 damage token to attached location.'",
                EncounterSet = "Celebrimbor's Forge",
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Bellach",
                Id = "9472b675-605e-446e-a432-1146eec95018",
                CardType = CardType.Enemy,
                IsUnique = true,
                Keywords = new List<string>() { "X is the number of cards under The Orc's Search." },
                Quantity = 1,
                Traits = new List<string>() { "Mordor." },
                HitPoints = 6,
                Attack = 5,
                Defense = 4,
                Text = "While at stage 1, immune to player card effects and cannot be engaged.Scour: Each player must search the encounter deck and discard pile for an Orc enemy and add it to the staging area.",
                EncounterSet = "Celebrimbor's Forge",
                EngagementCost = 50,
                Threat = 0,
                VictoryPoints = 5,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Celebrimbor's Mould",
                Id = "9472b675-605e-446e-a432-1146eec95019",
                CardType = CardType.Objective,
                IsUnique = true,
                Quantity = 1,
                Traits = new List<string>() { "Artifact.", " Item." },
                Text = "If Celebrimbor's Mould is free of encounters, the first player claims it and attaches it to a hero he controls.If Celebrimbor's Mould leaves play, the players lose the game.",
                EncounterSet = "Celebrimbor's Forge",
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Spies from Mordor",
                Id = "9472b675-605e-446e-a432-1146eec95020",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Deal 3 damage to the active location. If there is no active location, Spies from Morder gains surge.",
                EncounterSet = "Celebrimbor's Forge",
                Shadow = "Shadow: Excess damage dealt by this attack must be dealt to the active location, if able.",
                Number = 15
            });
        }
    }
}
