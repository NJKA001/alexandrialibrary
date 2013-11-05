using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class Ninineliph : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "Fleeing from Tharbad - 1A",
                Id = "9472b675-605e-446e-a432-1146eec94001",
                CardType = CardType.Quest,
                Text = "Setup: The first player takes control of Nalir. Set the Ancient Marsh-dweller aside, out of play. Each player searches the encounter deck for a different location and adds it to the staging area. Shuffle the encounter deck.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Setup = "tt",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Through the Marsh - 2A",
                Id = "9472b675-605e-446e-a432-1146eec94003",
                CardType = CardType.Quest,
                Text = "Doomed 1.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Through the Marsh - 2A",
                Id = "9472b675-605e-446e-a432-1146eec94005",
                CardType = CardType.Quest,
                Text = "Doomed 1.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Through the Marsh - 2A",
                Id = "9472b675-605e-446e-a432-1146eec94007",
                CardType = CardType.Quest,
                Text = "Doomed 1.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Lost in the Swanfleet - 3A",
                Id = "9472b675-605e-446e-a432-1146eec94009",
                CardType = CardType.Quest,
                Text = "Doomed 1.When Revealed: Add Ancient Marsh-dweller to the staging area (from out of play or engaged with a player) and heal all damage from it.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Lost in the Swanfleet - 3A",
                Id = "9472b675-605e-446e-a432-1146eec94011",
                CardType = CardType.Quest,
                Text = "Doomed 1.When Revealed: Add Ancient Marsh-dweller to the staging area (from out of play or engaged with a player) and heal all damage from it.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Lost in the Swanfleet - 3A",
                Id = "9472b675-605e-446e-a432-1146eec94013",
                CardType = CardType.Quest,
                Text = "Doomed 1.When Revealed: Add Ancient Marsh-dweller to the staging area (from out of play or engaged with a player) and heal all damage from it.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Out of the Swamp - 4A",
                Id = "9472b675-605e-446e-a432-1146eec94015",
                CardType = CardType.Quest,
                Text = "When Revealed: Add Ancient Marsh-dweller to the staging area (from out of play or engaged with a player) and heal all damage from it. Then, Ancient Marsh-dweller makes an attack (cannot be canceled) against each player in player order.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Nalir",
                Id = "9472b675-605e-446e-a432-1146eec94017",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Dwarf." },
                Keywords = new List<string>() { "The first player gains control of Nalir." },
                Willpower = 0,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                IsUnique = true,
                Text = "Forced: At the beginning of the refresh phase, raise your threat by 1 for each player in the game. If Nalir leaves play, the players lose the game.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Ancient Marsh-dweller",
                Id = "9472b675-605e-446e-a432-1146eec94018",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Creature." },
                Keywords = new List<string>() { "Cannot have attachments." },
                EngagementCost = 45,
                Threat = 3,
                Attack = 6,
                Defense = 4,
                HitPoints = 9,
                IsUnique = true,
                Text = "Ancient Marsh-dweller gets +1 Threat and +1 Attack for each resource token on it. Forced: After a time counter is removed from the current quest, place a resource token here.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                VictoryPoints = 5,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Giant Swamp Adder",
                Id = "9472b675-605e-446e-a432-1146eec94019",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Creature." },
                Keywords = new List<string>() { "Cannot have attachments." },
                EngagementCost = 34,
                Threat = 3,
                Attack = 3,
                Defense = 3,
                HitPoints = 6,
                Text = "Forced: After a time counter is removed from the current quest, Giant Swamp Adder attacks the engaged player.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each time counter on the quest.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 4,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Neeker-breekers",
                Id = "9472b675-605e-446e-a432-1146eec94020",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Creature.", " Insect." },
                EngagementCost = 20,
                Threat = 2,
                Attack = 1,
                Defense = 1,
                HitPoints = 6,
                Text = "Forced: After a time counter is removed from the current quest, the engaged player must deal 2 damage to an ally he controls.",
                Shadow = "Shadow: Deal 1 damage to the defending character.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 3,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Fen of Reeds",
                Id = "9472b675-605e-446e-a432-1146eec94021",
                CardType = CardType.Location,
                Traits = new List<string>() { "River.", " Marsh." },
                Threat = 2,
                Text = "While Fen of Reeds is in the staging area, it gains: 'Forced: After the players advance to a quest stage, each player must exhaust a character he controls.'",
                QuestPoints = 3,
                EncounterSet = "Nin-in-eliph",
                Quantity = 3,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Finger of Glanduin",
                Id = "9472b675-605e-446e-a432-1146eec94022",
                CardType = CardType.Location,
                Traits = new List<string>() { "Marsh." },
                Threat = 3,
                Text = "While Finger of Glanduin is in the staging area, it gains: 'Forced: At the end of each round, remove 1 progress from each location in play.'",
                Shadow = "Shadow: If this attack destroys a character, remove all progress from the current quest.",
                QuestPoints = 3,
                EncounterSet = "Nin-in-eliph",
                Quantity = 3,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Hidden Eyot",
                Id = "9472b675-605e-446e-a432-1146eec94023",
                CardType = CardType.Location,
                Traits = new List<string>() { "Marsh." },
                Threat = 3,
                Text = "Response: After Hidden Eyot leaves play as explored location, place 2 time counters on the current quest. Travel: Each player must exhaust a character he controls to travel here.",
                QuestPoints = 4,
                EncounterSet = "Nin-in-eliph",
                Quantity = 2,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Sinking Bog",
                Id = "9472b675-605e-446e-a432-1146eec94024",
                CardType = CardType.Location,
                Traits = new List<string>() { "Marsh." },
                Threat = 1,
                Text = "While Stinking Bog is in the staging area, each character gets -1 Willpower, -1 Attack, and -1 Defense for each Item attached to it. This ability does not stack.",
                Shadow = "Shadow: Defending character gets -1 Defense for each attachment attached to it.",
                QuestPoints = 5,
                EncounterSet = "Nin-in-eliph",
                Quantity = 3,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Shifting Marshland",
                Id = "9472b675-605e-446e-a432-1146eec94025",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Remove 1 time counter from the current quest.",
                Shadow = "Shadow: Attacking enemy makes an additional attack after this one.",
                EncounterSet = "Nin-in-eliph",
                Quantity = 3,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Remnants of Elder Days",
                Id = "9472b675-605e-446e-a432-1146eec94026",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must search the encounter deck and discard pile for a Creature enemy and put it into play engaged with him. (Cannot be canceled.)",
                EncounterSet = "Nin-in-eliph",
                Quantity = 1,
                Number = 18
            });
        }
    }
}
