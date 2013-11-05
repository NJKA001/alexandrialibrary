using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class BoarandRaven : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "The Raven Tribe - 1A",
                Id = "9472b675-605e-446e-a432-1146eec96001",
                CardType = CardType.Quest,
                Text = "Setup: The first player takes control of Boar-clan War-chief. Set Raven Chief and Raven Chief's Camp aside, out of play. Make Dunland Battlefield the active location. Create the Dunland deck (see insert) and set it next to the quest deck. Reveal 1 card from the Dunland deck for each player in the game and add them to the staging area.",
                EncounterSet = "Boar and Raven",
                Quantity = 1,
                Setup = "tttl",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Battle for Dunland - 2A",
                Id = "9472b675-605e-446e-a432-1146eec96003",
                CardType = CardType.Quest,
                Text = "When Revealed: Shuffle the Dunland deck discard pile into the Dunland deck. Then, reveal 1 card from the Dunland deck for each player in the game.",
                EncounterSet = "Boar and Raven",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "The Last Stage - 3A",
                Id = "9472b675-605e-446e-a432-1146eec96005",
                CardType = CardType.Quest,
                Text = "When Revealed: Add Raven Chief and Raven Chief's Camp to the staging area. Reveal X cards from the Dunland deck where X is the number of players minus 1.",
                EncounterSet = "Boar and Raven",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Boar-clan War-chief",
                Id = "9472b675-605e-446e-a432-1146eec96007",
                IsUnique = true,
                CardType = CardType.Objective,
                Willpower = 1,
                Attack = 3,
                Defense = 2,
                HitPoints = 5,
                Traits = new List<string>() { "Dunland.", " Boar-clan." },
                Keywords = new List<string>() { "The first player gains control of Boar-clan War-chief." },
                Text = "Immune to player card effects. Boar-clan War-chief does not exhaust to defend.If Boar-clan War-chief leaves play, the players lose the game.",
                EncounterSet = "Boar and Raven",
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Raven Village",
                Id = "9472b675-605e-446e-a432-1146eec96008",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 3,
                Traits = new List<string>() { "Dunland." },
                Keywords = new List<string>() { "Time 2.", " Forced: After the last time counter is removed from this location, reveal the top card of the Dunland deck and add it to the staging area." },
                Shadow = "Shadow: Deal attacking enemy 2 additional shadow cards from the Dunland deck.",
                EncounterSet = "Boar and Raven",
                Quantity = 4,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Raven War-camp",
                Id = "9472b675-605e-446e-a432-1146eec96009",
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 4,
                Traits = new List<string>() { "Dunland." },
                Keywords = new List<string>() { "X is the number of players in the game.", "Time 3." },
                Text = "Forced: After the last time counter is removed from this location, starting with the first player, each player draws the top card of the Dunland deck and puts it into play engaged with him.",
                EncounterSet = "Boar and Raven",
                Quantity = 4,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Dunland Battlefield",
                Id = "9472b675-605e-446e-a432-1146eec96010",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 5,
                Traits = new List<string>() { "Dunland." },
                Keywords = new List<string>() { "Time 3.", " Forced: After the last time counter is removed from this location, each player assigns X damage among characters he controls where X is the number of cards in his hand." },
                Shadow = "Shadow: Defending character takes 1 damage.",
                EncounterSet = "Boar and Raven",
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Raven Country",
                Id = "9472b675-605e-446e-a432-1146eec96011",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 6,
                Keywords = new List<string>() { "Surge." },
                Text = "While this location is in the staging area, it gains:'Forced: After a player draws any number of cards, remove a time counter from a location in play, if able.'This ability does not stack.",
                EncounterSet = "Boar and Raven",
                Quantity = 2,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Raven Chief's Camp",
                Id = "9472b675-605e-446e-a432-1146eec96012",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 8,
                Traits = new List<string>() { "Dunland.", " Raven-clan." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Text = "Time 3. Forced: After the last time counter is removed from this location, exhaust each damaged character. Place 3 time counters on this location.",
                EncounterSet = "Boar and Raven",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Raven Warrior",
                Id = "9472b675-605e-446e-a432-1146eec96013",
                CardType = CardType.Enemy,
                EngagementCost = 36,
                Threat = 3,
                Attack = 4,
                Defense = 2,
                HitPoints = 5,
                Traits = new List<string>() { "Dunland.", " Raven-clan." },
                Text = "Forced: After Raven Warrior engages a player, remove X time counters from locations in play. X is the number of cards in the engaged player's hand.",
                Shadow = "Shadow: Remove a time counter from a location in play.",
                EncounterSet = "Boar and Raven",
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Raven Runner",
                Id = "9472b675-605e-446e-a432-1146eec96014",
                CardType = CardType.Enemy,
                EngagementCost = 26,
                Threat = 1,
                Attack = 3,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Dunland.", " Raven-clan." },
                Keywords = new List<string>() { "Surge." },
                Text = "Forced: After Raven Runner engages a player, that player draws a card and raises his threat by 1.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. If this attack destroys a character, remove 1 time counter from the active location.",
                EncounterSet = "Boar and Raven",
                Quantity = 3,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Raising the Cry",
                Id = "9472b675-605e-446e-a432-1146eec96015",
                CardType = CardType.Treachery,
                Text = "When Revealed: Remove 1 time counter from each location in play. Place X time counters on each location in play with no time counters on it. X is the Time X value on that location. If there are no locations in the staging area, Raising the Cry gains surge.",
                EncounterSet = "Boar and Raven",
                Quantity = 3,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Fierce Folk",
                Id = "9472b675-605e-446e-a432-1146eec96016",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player draws 3 cards. Each player discards 3 random cards from hand.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. If this attack destroys a character, remove 1 time counter from the active location.",
                EncounterSet = "Boar and Raven",
                Quantity = 2,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Raven Chief",
                Id = "9472b675-605e-446e-a432-1146eec96017",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 4,
                Attack = 5,
                Defense = 5,
                HitPoints = 9,
                Traits = new List<string>() { "Dunland.", " Raven-clan." },
                Keywords = new List<string>() { "Cannot have attachments.", "Raven Chief engages the first player." },
                Text = "Forced: When Raven Chief attacks, remove 1 time counter from the active location, if able.",
                EncounterSet = "Boar and Raven",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Lost Ground",
                Id = "9472b675-605e-446e-a432-1146eec96018",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Doomed 2." },
                Text = "When Revealed: Remove all progress from each location in the staging area. Add 1 Threat to the total Threat of the staging area for each progress removed this way. If no progress was removed by this effect, Lost Ground gains surge.",
                Shadow = "Shadow: Defending player discards an attachment he controls.",
                EncounterSet = "Boar and Raven",
                Quantity = 2,
                Number = 15
            });
        }
    }
}
