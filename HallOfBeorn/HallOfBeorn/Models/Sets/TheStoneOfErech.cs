using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheStoneofErech : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Stone of Erech";
            Abbreviation = "TSoE";
            Number = 2003;
            SetType = Models.SetType.GenCon_Expansion;
            Cycle = "GenCon";

            Cards.Add(new Card() {
                ImageName = "M1928",
                Title = "Banks of Morthond",
                Id = "14f7c98c-b425-4fac-850a-729d8fcdaa0c",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale.", " Riverland." },
                Quantity = 2,
                Threat = 2,
                QuestPoints = 7,
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1930",
                Title = "Blackroot Graves",
                Id = "fc54b033-56cc-4ca9-8e0f-7f1c7b82bd6a",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale." },
                Quantity = 3,
                Threat = 4,
                QuestPoints = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1915",
                Title = "Derufin",
                Id = "f93a8fda-7383-41c9-b86d-c5ba1fec760d",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Gondor." },
                Quantity = 1,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 3,
                HitPoints = 2,
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1936",
                Title = "Driven by Fear",
                Id = "cdf8dee9-b2ac-4e5f-bf4c-0b9c15cac142",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Quantity = 2,
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1920",
                Title = "Dusk",
                Id = "d3c68791-c3f9-4e8d-9515-2496f9ca1895",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Night." },
                Quantity = 1,
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1919",
                Title = "Eventide",
                Id = "36a22f2b-56d9-4148-a962-f7fbdf60f3a1",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Night." },
                Quantity = 1,
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1935",
                Title = "Groping Horror",
                Id = "728f17db-d741-4f1b-92da-8aac3d78432d",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Quantity = 2,
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1932",
                Title = "Haunted Valley",
                Id = "7a3411df-76a0-432b-a64a-d499b0b1ae50",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale." },
                Quantity = 2,
                Threat = 2,
                QuestPoints = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1921",
                Title = "Midnight",
                Id = "0c15f99d-9066-4e7c-b364-104a0083d997",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Night." },
                Quantity = 1,
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1933",
                Title = "Midnight Throng",
                Id = "c29c98de-5660-498c-a5cc-a1800985d221",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Quantity = 2,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1934",
                Title = "Murmurs of Dread",
                Id = "f985bda2-5029-4c34-91f7-721f830cbd9c",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Quantity = 2,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1927",
                Title = "Regretful Shade",
                Id = "f079c496-20bf-4ce4-a5ca-d644172d83fe",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 3,
                EngagementCost = 23,
                Attack = 2,
                Defense = 1,
                HitPoints = 3,
                Keywords = new List<string>() { "Spectral.", " Surge." },
                Threat = 1,
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1922",
                Title = "Relic from the Dark Years",
                Id = "7b0eb4e5-962c-4041-baea-3a09a83fb996",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Item.", " Artifact." },
                Quantity = 1,
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1924",
                Title = "Restless Dead",
                Id = "65f48337-cec0-41a1-84ff-490c4f563ce4",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 4,
                EngagementCost = 12,
                Attack = 3,
                Defense = 0,
                HitPoints = 2,
                Keywords = new List<string>() { "Spectral." },
                Threat = 2,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1926",
                Title = "Shadow Host Captain",
                Id = "a7d52939-2989-44d2-b81b-571db0ee8c16",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 2,
                EngagementCost = 42,
                Attack = 4,
                Defense = 1,
                HitPoints = 6,
                Keywords = new List<string>() { "Spectral." },
                Threat = 3,
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1923",
                Title = "Shadow-man",
                Id = "8ece887c-6ca7-467d-80d4-3f28afa13433",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 2,
                EngagementCost = 27,
                Attack = 4,
                Defense = 1,
                HitPoints = 5,
                Keywords = new List<string>() { "Spectral." },
                Threat = 2,
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1931",
                Title = "Shadow of Dwimorberg",
                Id = "02cae27a-3b0c-4396-aea3-8fc2bdd4ffe8",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale.", " Mountain." },
                Quantity = 2,
                Threat = 1,
                QuestPoints = 4,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1918",
                Title = "Tarlang's Neck",
                Id = "88df7c56-ee11-48f0-b6a0-0486e0a8ce92",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale.", " Road." },
                Quantity = 1,
                IsUnique = true,
                Threat = 3,
                QuestPoints = 6,
                VictoryPoints = 1,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1913",
                Title = "Terror of the Dead - 2A",
                Id = "ecc8fd11-b2b9-4adc-91eb-6fcc1212e7ce",
                CardType = CardType.Quest,
                EncounterSet = "The Stone of Erech",
                Quantity = 1,
                QuestPoints = 8,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1937",
                Title = "The Dead Ride Behind",
                Id = "5e7c36de-a2bb-44e3-951b-80123788d571",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Quantity = 2,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1912",
                Title = "The Disappearance - 1A",
                Id = "e4af5d7f-9af5-41bb-b22a-cce8b91ac791",
                CardType = CardType.Quest,
                EncounterSet = "The Stone of Erech",
                Quantity = 1,
                QuestPoints = 6,
                Setup = "ltttttt",
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1938",
                Title = "The Gloaming",
                Id = "d33ef89b-13c9-49b2-a921-a527f27a415a",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Quantity = 2,
                Keywords = new List<string>() { "Surge." },
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1916",
                Title = "The Lord of the Dead",
                Id = "7b9dd852-ed19-4a00-b09e-f6c2cee549fa",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 2,
                EngagementCost = 30,
                IsUnique = true,
                Attack = 6,
                Defense = 3,
                HitPoints = 9,
                Keywords = new List<string>() { "Spectral.", " Cannot have attachments." },
                Threat = 5,
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1914",
                Title = "The Shadow Host - 3A",
                Id = "efd4cef9-ce8b-4985-8825-853ba98baa2b",
                CardType = CardType.Quest,
                EncounterSet = "The Stone of Erech",
                Quantity = 1,
                QuestPoints = 14,
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1917",
                Title = "The Stone of Erech",
                Id = "822ca699-4763-4b8c-baff-d8d828c002a9",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale." },
                Quantity = 1,
                IsUnique = true,
                Keywords = new List<string>() { "Immune to player card effects.", },
                Text =
@"X is twice the number of players in the game.

While The Stone of Erech is that active location, characters with less than 2 Willpower cannot ready.

Travel: Exhaust each character with less than 2 Willpower.",
                Threat = 0,
                IsVariableThreat = true,
                QuestPoints = 6,
                VictoryPoints = 5,
                Number = 25
            });
            Cards.Add(new Card() {
                ImageName = "M1929",
                Title = "Vale of Shadows",
                Id = "c57661b7-62f9-4906-b2eb-73af91fd8252",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale." },
                Quantity = 2,
                Threat = 1,
                QuestPoints = 3,
                Number = 26
            });
            Cards.Add(new Card() {
                ImageName = "M1925",
                Title = "Whisperer",
                Id = "43bb02ff-9fcc-47e8-822c-ffcd620c9fc4",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 3,
                EngagementCost = 35,
                Attack = 2,
                Defense = 2,
                HitPoints = 4,
                Keywords = new List<string>() { "Spectral." },
                Threat = 2,
                Number = 27
            });
        }
    }
}
