using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheBloodofGondor : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "Caldara",
                Id = "60725069-031c-4251-9b2c-3f368545e9ac",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                ThreatCost = 8,
                Willpower = 2,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor." },
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Anborn",
                Id = "ef8aec20-e0c9-4d02-9dea-20b6e7c2278b",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 4,
                Willpower = 1,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Quantity = 3,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Poisoned Stakes",
                Id = "30e0c679-3ced-4862-a681-b67ceb8939d3",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Traits = new List<string>() { "Trap." },
                Quantity = 3,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Children of the Sea",
                Id = "65d3b334-df93-43c6-9525-12674bbb7f06",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Quantity = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "The Hammer-stroke",
                Id = "cc2f73af-86c8-4d81-b706-14127adc0b37",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Quantity = 3,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Tome of Atanator",
                Id = "e88d7165-821c-4b04-9869-80ee1f33905c",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 4,
                Traits = new List<string>() { "Record." },
                Keywords = new List<string>() { "Attach to a Ì hero." },
                Quantity = 3,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Guthlaf",
                Id = "06baca74-330a-4038-9387-32ce8657dd8f",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 3,
                Willpower = 1,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Rohan." },
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Emery",
                Id = "314eaf40-1554-4c1a-b643-1f4353e25633",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Willpower = 1,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Gondor." },
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Squire of the Citadel",
                Id = "d4f034b0-2444-4b61-a249-8d86e8856f7c",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Willpower = 0,
                Attack = 0,
                Defense = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Gondor." },
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Well-Equipped",
                Id = "0f027b32-f63f-4d37-8305-5ba2b059289d",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                ResourceCost = 0,
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Southern Road",
                Id = "5a654416-4449-4d0c-add8-8b3ca04bb401",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "Ithilien.", " Road." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Western Road",
                Id = "01b1d42b-11a4-4030-988d-064673169fec",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "Ithilien.", " Road." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Northern Road",
                Id = "8eb1ca03-da9e-4e84-abf2-090804eeb1aa",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 3,
                Traits = new List<string>() { "Ithilien.", " Road." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Eastern Road",
                Id = "9d58de28-c518-4888-8e91-a52bb5c3e663",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 2,
                Traits = new List<string>() { "Ithilien.", " Road." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "The Dark Woods",
                Id = "eec35e8e-281e-4dc7-8a76-b4823de3cb71",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 2,
                Traits = new List<string>() { "Forest." },
                Keywords = new List<string>() { "Archery X." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 4,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Lying in Wait",
                Id = "8affd994-f7f5-4341-8437-899f9a31b094",
                CardType = CardType.Treachery,
                EncounterSet = "The Blood of Gondor",
                Quantity = 4,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Mordor Looms",
                Id = "fa3a740d-36b7-4fa9-b5d5-6d70aa50f078",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 3,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Conflict at the Crossroads",
                Id = "f4b35e95-7f32-490d-9591-ac9eb050bb7e",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 2,
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Ambush - 1A",
                Id = "23768a0d-bf6c-4405-9b09-ea8fdbfded31",
                CardType = CardType.Quest,
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Setup = "sstt",
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "Captured! - 2A",
                Id = "d2bf00ad-2e1e-4537-b662-f6f20f9a1cfe",
                CardType = CardType.Quest,
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "Orc Ambusher",
                Id = "8f106a70-0102-41ad-8752-fb5667748850",
                CardType = CardType.Enemy,
                EngagementCost = 10,
                Threat = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Orc.", " Mordor." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 4,
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "Brutal Uruk",
                Id = "2d2e4f42-009d-4fde-ab42-3cd9351a4d0f",
                CardType = CardType.Enemy,
                EngagementCost = 20,
                Threat = 3,
                Attack = 4,
                Defense = 1,
                HitPoints = 5,
                Traits = new List<string>() { "Orc.", " Uruk.", " Mordor." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 3,
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "Evil Crow",
                Id = "9d2926dc-ab14-492a-8f1e-b07c1b32b2d8",
                CardType = CardType.Enemy,
                EngagementCost = 5,
                Threat = 1,
                Attack = 1,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Creature." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 2,
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "The Cross-roads",
                Id = "9b2302dc-3084-447f-8104-a4569fd26e38",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 2,
                Traits = new List<string>() { "Ithilien." },
                Keywords = new List<string>() { "The current quest card gains siege." },
                EncounterSet = "The Blood of Gondor",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "Faramir",
                Id = "ab2ac791-b266-43b2-b6ee-0d412a1100bc",
                IsUnique = true,
                CardType = CardType.Objective,
                Willpower = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Gondor.", " Noble.", " Ranger." },
                Keywords = new List<string>() { "The first player gains control of Faramir." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                Title = "Black Numenorean",
                Id = "6a4b0ba7-c80b-4580-bc43-d5369b0c44aa",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Threat = 0,
                Attack = 5,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Mordor." },
                EncounterSet = "The Blood of Gondor",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 26
            });
            Cards.Add(new Card() {
                Title = "Lord Alcaron",
                Id = "fa4aaa97-c06d-42d5-b796-2f6200d9a404",
                IsUnique = true,
                CardType = CardType.Objective,
                Willpower = 1,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Noble." },
                Keywords = new List<string>() { "The first player gains control of Lord Alcaron." },
                EncounterSet = "The Blood of Gondor",
                Quantity = 1,
                Number = 27
            });
        }
    }
}
