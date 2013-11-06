using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheDunlandTrap : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Dunland Trap";

            Cards.Add(new Card() {
                Title = "The Road to Tharbad - 1A",
                Id = "9472b675-605e-446e-a432-1146eec91001",
                CardType = CardType.Quest,
                Text = "Setup: Set Boar-clan War-chief and Deep Ravine aside, out of play. Make the The Old South Road the active location. Each players searches the encounter deck for a Boar-clan enemy and puts it into play engaged with him. Shuffle the encounter deck.",
                EncounterSet = "The Dunland Trap",
                Quantity = 1,
                Setup = "ttl",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "A Well Laid Trap - 2A",
                Id = "9472b675-605e-446e-a432-1146eec91003",
                CardType = CardType.Quest,
                Text = "When Revealed: Make Deep Ravine the active location. Each player discards each Item and Mount attachment he controls. Each player chooses 1 ally he controls and discards each other ally he controls.",
                EncounterSet = "The Dunland Trap",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "No Way Out - 3A",
                Id = "9472b675-605e-446e-a432-1146eec91005",
                CardType = CardType.Quest,
                Text = "When Revealed: Put Boar-clan War-chief into play engaged with the first player.",
                EncounterSet = "The Dunland Trap",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Boar-clan War-chief",
                Id = "9472b675-605e-446e-a432-1146eec91007",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 3,
                Attack = 5,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Dunland.", " Boar-clan." },
                Keywords = new List<string>() { "Indestructible.", " Cannot have attachments." },
                Text = "Boar-clan War-chief engages the first player. Forced: After an enemy engages a player, remove 1 time counter from the current quest.",
                EncounterSet = "The Dunland Trap",
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Boar-clan Warrior",
                Id = "9472b675-605e-446e-a432-1146eec91008",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 2,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Dunland.", " Boar-clan." },
                Text = "Boar-clan Warrior gets +1 Attack and +1 Defense for each resource token on it (limit +3).Forced: After the engaged player draws any number of cards, put a resource token on Boar-clan Warrior.",
                EncounterSet = "The Dunland Trap",
                Quantity = 3,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Boar-clan Skirmisher",
                Id = "9472b675-605e-446e-a432-1146eec91009",
                CardType = CardType.Enemy,
                EngagementCost = 20,
                Threat = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Dunland.", " Boar-clan." },
                Text = "Forced: After the engaged player draws any number of cards, deal Boar-clan Skirmisher a shadow card.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each shadow card it was dealt this round.",
                EncounterSet = "The Dunland Trap",
                Quantity = 4,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Old South Road",
                Id = "9472b675-605e-446e-a432-1146eec91010",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 4,
                Traits = new List<string>() { "Road." },
                Text = "Forced: At the end of the refresh phase, remove 1 time counter from the current quest.",
                EncounterSet = "The Dunland Trap",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Hithlaegir Foothills",
                Id = "9472b675-605e-446e-a432-1146eec91011",
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 4,
                Traits = new List<string>() { "Enedwaith.", " Hills." },
                Keywords = new List<string>() { "Surge." },
                Text = "X is the number of resource tokens on this location.Forced: After a player draws any number of cards, place 1 resource token here.",
                EncounterSet = "The Dunland Trap",
                Quantity = 2,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Hills of Dunland",
                Id = "9472b675-605e-446e-a432-1146eec91012",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 3,
                Traits = new List<string>() { "Enedwaith.", " Hills." },
                Text = "While Hills of Dunland is in the staging area, it gains: 'Forced: After a player draws any number of cards, discard the top card of the encounter deck. If the discarded card is a Dunland enemy, put it into play engaged with that player. This ability does not stack with other copies of Hills of Dunland.'Travel: Each player draws a card to travel here.",
                EncounterSet = "The Dunland Trap",
                Quantity = 4,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Plains of Enedwaith",
                Id = "9472b675-605e-446e-a432-1146eec91013",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 2,
                Traits = new List<string>() { "Enedwaith.", " Plains." },
                Text = "While Plains of Enedwaith is the active location, players do not draw a card during the resource phase.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack (+2 Attack instead if undefended).",
                EncounterSet = "The Dunland Trap",
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Hithlaegir Stream",
                Id = "9472b675-605e-446e-a432-1146eec91014",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 4,
                Traits = new List<string>() { "Enedwaith.", " River." },
                Text = "While Hithlaegir Stream is the active location, players draw 2 cards during the resource phase instead of 1.",
                Shadow = "Shadow: Defending player discards a random card from his hand. Deal this enemy another shadow card.",
                EncounterSet = "The Dunland Trap",
                Quantity = 4,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Surrounded!",
                Id = "9472b675-605e-446e-a432-1146eec91015",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player draws a card. Each player discards each ally from his hand. If no allies are discarded this way, Surrounded! gains surge.",
                Shadow = "Shadow: Until the end of the round, attacking enemy cannot take damage.",
                EncounterSet = "The Dunland Trap",
                Quantity = 3,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Dunlending Ambush",
                Id = "9472b675-605e-446e-a432-1146eec91016",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Attach to the active location. (Counts as a Condition attachment with the text: 'Limit one per location. Forced: After attached location leaves play as an explored location, each player searches the encounter deck and discard pile for a Dunland enemy and puts it into play engaged with him.')",
                EncounterSet = "The Dunland Trap",
                Quantity = 3,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Deep Ravine",
                Id = "9472b675-605e-446e-a432-1146eec91017",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 6,
                Traits = new List<string>() { "Enedwaith.", " Hills.", " River." },
                Text = "Dunland enemies get +1 Attack and +1 Defense.",
                EncounterSet = "The Dunland Trap",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 14
            });
        }
    }
}
