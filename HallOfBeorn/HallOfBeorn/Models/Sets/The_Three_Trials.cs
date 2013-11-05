using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheThreeTrials : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "The Trials Begin - 1A",
                Id = "9472b675-605e-446e-a432-1146eec92001",
                CardType = CardType.Quest,
                Text = "Setup: Set aside all 3 Guardian enemies, all 3 Key objectives, all 3 Cursed locations, and Hallowed Circle.",
                EncounterSet = "The Three Trials",
                Quantity = 1,
                Setup = "tttttttttt",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "The Trial of Strength - 2A",
                Id = "9472b675-605e-446e-a432-1146eec92002",
                CardType = CardType.Quest,
                Text = "When Revealed: Randomly choose 1 of the remaining set aside Guardian enemies and 1 of the remaining set aside Cursed locations, reveal them, and add them to the staging area. Attach the set aside Key objective that shares a Trait with the just revealed Guardian enemy to that Guardian enemy.",
                EncounterSet = "The Three Trials",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "The Trial of Perseverance - 2A",
                Id = "9472b675-605e-446e-a432-1146eec92003",
                CardType = CardType.Quest,
                Text = "When Revealed: Randomly choose 1 of the remaining set aside Key objectives and 1 of the remaining set aside Cursed locations, reveal them, and add them to the staging area, attaching the Key to the Cursed location. Reveal and add the set aside Guardian enemy that shares a Trait with the just revealed Key to the staging area.",
                EncounterSet = "The Three Trials",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "The Trial of Intuition - 2A",
                Id = "9472b675-605e-446e-a432-1146eec92004",
                CardType = CardType.Quest,
                Text = "When Revealed: Randomly choose 1 of the remaining set aside Guardian enemies and 1 of the remaining set aside Cursed locations, reveal them, and add them to the staging area. Shuffle the encounter discard pile and the set aside Key objective that shares a Trait with the just revealed Guardian enemy into the encounter deck.",
                EncounterSet = "The Three Trials",
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "The Antlered Crown - 3A",
                Id = "9472b675-605e-446e-a432-1146eec92005",
                CardType = CardType.Quest,
                Text = "When Revealed: Add Hallowed Circle and each Guardian enemy in the victory display to the staging area.",
                EncounterSet = "The Three Trials",
                Quantity = 1,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Boar's Guardian",
                Id = "9472b675-605e-446e-a432-1146eec92006",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 1,
                Attack = 4,
                Defense = 4,
                HitPoints = 10,
                Traits = new List<string>() { "Guardian.", " Boar." },
                Keywords = new List<string>() { "Cannot have attachments." },
                Text = "Time 2. Forced: After the last time counter is removed from Boar’s Guardian, the engaged player must either discard a card at random from his hand, or discard an ally he controls. Then, place 2 time counters on Boar’s Guardian.",
                EncounterSet = "The Three Trials",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Wolf's Guardian",
                Id = "9472b675-605e-446e-a432-1146eec92007",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 2,
                Attack = 5,
                Defense = 3,
                HitPoints = 12,
                Traits = new List<string>() { "Guardian.", " Wolf." },
                Keywords = new List<string>() { "Cannot have attachments." },
                Text = "Time 3. Forced: After the last time counter is removed from Wolf’s Guardian, it makes an immediate attack against the engaged player. Then, place 3 time counters on Wolf’s Guardian.",
                EncounterSet = "The Three Trials",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Raven's Guardian",
                Id = "9472b675-605e-446e-a432-1146eec92008",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 3,
                Attack = 3,
                Defense = 5,
                HitPoints = 8,
                Traits = new List<string>() { "Guardian.", " Raven." },
                Keywords = new List<string>() { "Cannot have attachments." },
                Text = "Time 4. Forced: After the last time counter is removed from Raven’s Guardian, deal 1 damage to each character controlled by the engaged player. Then, place 4 time counters on Raven’s Guardian.",
                EncounterSet = "The Three Trials.",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Key of the Wolf",
                Id = "9472b675-605e-446e-a432-1146eec92009",
                IsUnique = true,
                CardType = CardType.Objective,
                Traits = new List<string>() { "Item.", " Key.", " Wolf." },
                Text = "When Key of the Wolf is unattached, the first player must claim it and attach it to a hero he controls. If detached, attach it to a different hero in play.Forced: If Key of the Wolf would be placed into the discard pile, add it to the staging area instead.",
                EncounterSet = "The Three Trials",
                Quantity = 1,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Key of the Boar",
                Id = "9472b675-605e-446e-a432-1146eec92010",
                IsUnique = true,
                CardType = CardType.Objective,
                Traits = new List<string>() { "Item.", " Key.", " Boar." },
                Text = "When Key of the Boar is unattached, the first player must claim it and attach it to a hero he controls. If detached, attach it to a different hero in play.Forced: If Key of the Boar would be placed into the discard pile, add it to the staging area instead.",
                EncounterSet = "The Three Trials",
                Quantity = 1,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Key of the Raven",
                Id = "9472b675-605e-446e-a432-1146eec92011",
                IsUnique = true,
                CardType = CardType.Objective,
                Traits = new List<string>() { "Item.", " Key.", " Raven." },
                Text = "When Key of the Raven is unattached, the first player must claim it and attach it to a hero he controls. If detached, attach it to a different hero in play.Forced: If Key of the Raven would be placed into the discard pile, add it to the staging area instead.",
                EncounterSet = "The Three Trials",
                Quantity = 1,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Hill Barrow",
                Id = "9472b675-605e-446e-a432-1146eec92012",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 6,
                Traits = new List<string>() { "Forest.", " Cursed." },
                Text = "Forced: When a Guardian enemy attacks, deal it 1 additional shadow card.",
                EncounterSet = "The Three Trials",
                VictoryPoints = 2,
                Quantity = 1,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Labyrinthine Barrow",
                Id = "9472b675-605e-446e-a432-1146eec92013",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 7,
                Traits = new List<string>() { "Forest.", " Underground.", " Cursed." },
                Text = "The players, as a group, cannot have more than 5 allies in play. (If there are more than 5 allies in play, discard allies until only 5 remain).",
                EncounterSet = "The Three Trials",
                VictoryPoints = 2,
                Quantity = 1,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Stone Barrow",
                Id = "9472b675-605e-446e-a432-1146eec92014",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 8,
                Traits = new List<string>() { "Forest.", " Cursed." },
                Text = "Forced: After a Guardian enemy attacks, raise the defending player’s threat by X, where X is that enemy’s Threat.",
                EncounterSet = "The Three Trials",
                VictoryPoints = 2,
                Quantity = 1,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Hallowed Circle",
                Id = "9472b675-605e-446e-a432-1146eec92015",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 12,
                Traits = new List<string>() { "Forest.", " Underground." },
                Text = "Immune to player card effects. X is the number of players.Forced: After Hallowed Circle becomes the active location, each Guardian enemy attacks the character who controls the Key objective with a Trait shared by that enemy.",
                EncounterSet = "The Three Trials",
                VictoryPoints = 5,
                Quantity = 1,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Spirit of the Wild",
                Id = "9472b675-605e-446e-a432-1146eec92016",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 1,
                Attack = 1,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Spirit." },
                Text = "Spirit of the Wild gets +1 Threat and +1 Attack for each Key objective the players control.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each Key objective the players control.",
                EncounterSet = "The Three Trials",
                Quantity = 5,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Cursed Forest",
                Id = "9472b675-605e-446e-a432-1146eec92017",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 3,
                Traits = new List<string>() { "Forest." },
                Text = "Travel: Search the encounter deck and discard pile for a Spirit enemy and add it to the staging area.",
                Shadow = "Shadow: If this attack destroys a character, remove a time counter from attacking enemy, if able.",
                EncounterSet = "The Three Trials",
                Quantity = 2,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Foothills of Caradhras",
                Id = "9472b675-605e-446e-a432-1146eec92018",
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 3,
                Traits = new List<string>() { "Hills." },
                Keywords = new List<string>() { "X is the number of players." },
                Text = "While Foothills of Caradhras is the active location, it gains 'Forced: After a Guardian enemy is dealt damage, remove 1 time counter from it.'",
                Shadow = "Shadow: If attacking enemy is a Guardian, heal 2 damage from it.",
                EncounterSet = "The Three Trials",
                Quantity = 3,
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Curse of the Wild Men",
                Id = "9472b675-605e-446e-a432-1146eec92019",
                CardType = CardType.Treachery,
                Text = "When Revealed: Deal 1 damage to each non-unique character in play. If the players control 3 Key objectives, Curse of the Wild Men gains surge.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each Key objective the players control.",
                EncounterSet = "The Three Trials",
                Quantity = 2,
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "The Guardian's Fury",
                Id = "9472b675-605e-446e-a432-1146eec92020",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each Guardian enemy makes an immediate attack (deal and resolve a shadow card for each attack). If there are no Guardian enemies in play, return a random Guardian enemy from the victory display to play.",
                Shadow = "Shadow: If attacking enemy is a Guardian, it makes an additional attack after this one.",
                EncounterSet = "The Three Trials",
                Quantity = 2,
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "Wild Tenacity",
                Id = "9472b675-605e-446e-a432-1146eec92021",
                CardType = CardType.Treachery,
                Text = "When Revealed: Remove X time counters from each enemy in play with the Time keyword, where X is the number of players. Players who control a Key objective cannot cancel Wild Tenacity.",
                Shadow = "Shadow: If attacking enemy is a Guardian, deal it two additional shadow cards for this attack.",
                EncounterSet = "The Three Trials",
                Quantity = 3,
                Number = 21
            });
        }
    }
}
