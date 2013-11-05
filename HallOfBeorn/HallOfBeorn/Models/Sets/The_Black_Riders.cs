using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheBlackRiders : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "Sam Gamgee",
                Id = "4124136c-8c86-4f86-830c-94c8c76df161",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 8,
                Willpower = 3,
                Attack = 1,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Hobbit." },
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Merry",
                Id = "052b1f85-8b9c-4bb0-a735-bdbd5ac1b2c4",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                ThreatCost = 6,
                Willpower = 2,
                Attack = 0,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit." },
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Pippin",
                Id = "ce96b767-c569-48b8-a998-d8009b0143c7",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                ThreatCost = 6,
                Willpower = 2,
                Attack = 1,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit." },
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Fatty Bolger",
                Id = "5d75d4dd-7300-43d7-87f2-963271c9c904",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                ThreatCost = 7,
                Willpower = 1,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Hobbit." },
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Frodo Baggins",
                Id = "3217a119-6b86-47dd-b451-c5e45be3f874",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Fellowship,
                ThreatCost = 0,
                Willpower = 2,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit.", " Ring-bearer." },
                Quantity = 1,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "The One Ring",
                Id = "c9575426-59ea-4863-bf6a-407e9eae3f33",
                IsUnique = true,
                CardType = CardType.Objective,
                Traits = new List<string>() { "Artifact.", " Item.", " Ring." },
                Quantity = 1,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Barliman Butterbur",
                Id = "77f58774-86e7-4449-b31e-3833700b3e60",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Willpower = 1,
                Attack = 0,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Bree." },
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Elf-stone",
                Id = "9bb32f2c-29fb-43ba-b7ba-2227b28f7b58",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Traits = new List<string>() { "Artifact.", " Item." },
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Farmer Maggot",
                Id = "9d8ccd1a-48d3-4123-bcca-3c0ab88347ec",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 3,
                Willpower = 1,
                Attack = 2,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit." },
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Halfling Determination",
                Id = "8e7e5c8d-0ea4-46df-ae38-d8d2fee7ca8b",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Dagger of Westernesse",
                Id = "418e6de7-af19-4ea7-bfbe-2a02838c6de4",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Traits = new List<string>() { "Artifact.", " Item.", " Weapon." },
                Keywords = new List<string>() { "Attach to the hero.", " Restricted." },
                Quantity = 3,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Bill the Pony",
                Id = "1f7fc118-94a7-48a0-bd0c-9c15a36ddc23",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Willpower = 1,
                Attack = 1,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Creature.", " Pony." },
                Keywords = new List<string>() { "Cannot have attachments." },
                Quantity = 3,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Take No Notice",
                Id = "768ae041-2d15-44a3-a928-62838536a160",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Quantity = 3,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Smoke Rings",
                Id = "9418c634-54c6-47de-9aae-798038a4a35b",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Quantity = 3,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Frodo's Intuition",
                Id = "96350b97-5c68-4033-bb2f-4305696a7ae7",
                CardType = CardType.Event,
                Sphere = Sphere.Fellowship,
                ResourceCost = 2,
                Quantity = 3,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Hobbit Cloak",
                Id = "8e49ea86-375a-472e-b497-16a1164ae27f",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Traits = new List<string>() { "Item." },
                Keywords = new List<string>() { "Attach to a Hobbit hero.", " Limit 1 per hero." },
                Quantity = 3,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Hobbit Pipe",
                Id = "9c455b1a-a2d4-44f7-a9d3-9a3134c21a2a",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Traits = new List<string>() { "Item." },
                Keywords = new List<string>() { "Attach to a Hobbit.", " Limit 1 per character." },
                Quantity = 3,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Mr Underhill",
                Id = "d0ed393c-162a-4715-bed4-e338c24e9e36",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Title." },
                Keywords = new List<string>() { "Permanent." },
                VictoryPoints = 1,
                Quantity = 1,
                Setup = "t",
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Valiant Warrior",
                Id = "ff574390-bd68-4277-9065-dd9dbf552d00",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Title." },
                Keywords = new List<string>() { "Permanent." },
                Quantity = 1,
                Setup = "t",
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "Noble Hero",
                Id = "af49e5ea-c6a2-4be4-bbf3-ac53c100e887",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Title." },
                Keywords = new List<string>() { "Permanent." },
                Quantity = 1,
                Setup = "t",
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "Skilled Healer",
                Id = "1d1ab8a3-ad76-4992-ae5c-6a89fd0ed463",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Title." },
                Keywords = new List<string>() { "Permanent." },
                Quantity = 1,
                Setup = "t",
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "Tireless Ranger",
                Id = "ef014a91-c2d9-44ca-acd0-cc1a339c051f",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Title." },
                Keywords = new List<string>() { "Permanent." },
                Quantity = 1,
                Setup = "t",
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "Gildor Inglorian",
                Id = "6c04a40a-0666-4b4f-a768-ddff46857cf0",
                IsUnique = true,
                CardType = CardType.Objective,
                Willpower = 3,
                Attack = 2,
                Defense = 3,
                HitPoints = 3,
                Traits = new List<string>() { "Noldor." },
                Keywords = new List<string>() { "The first player gains control of Gildor Inglorian." },
                Quantity = 1,
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "Overcome by Terror",
                Id = "cdc303cb-bb1a-4409-a479-aa4155bd8ed5",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "Flight to the Ford",
                Quantity = 1,
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "Fear of Discovery",
                Id = "074cad39-ce55-4dc1-9775-e95363682ef7",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "Flight to the Ford",
                Quantity = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                Title = "Eaten Alive!",
                Id = "e8f413bf-3593-47fc-8813-4553d55dbb2a",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "Flight to the Ford",
                Quantity = 1,
                Number = 26
            });
            Cards.Add(new Card() {
                Title = "Panicked",
                Id = "50c19cd1-3094-4bf7-b2dc-e21b145827b7",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "Flight to the Ford",
                Quantity = 1,
                Number = 27
            });
            Cards.Add(new Card() {
                Title = "Weight of the Ring",
                Id = "40c95b3d-6216-4def-bb93-f44871f3ec70",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "Flight to the Ford",
                Quantity = 1,
                Number = 28
            });
            Cards.Add(new Card() {
                Title = "The Ring Draws Them",
                Id = "1c5f4854-c10c-430b-9c40-2d84c5997b8f",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "A Shadow of the past",
                Quantity = 1,
                Number = 29
            });
            Cards.Add(new Card() {
                Title = "Gandalf's Delay",
                Id = "d95a6076-3ead-4ff6-a1ba-ca320d2bd4e1",
                CardType = CardType.Objective,
                EncounterSet = "A Shadow of the past",
                Quantity = 1,
                Setup = "s",
                Number = 30
            });
            Cards.Add(new Card() {
                Title = "A Shadow of the Past",
                Id = "307411f3-4a45-44ae-bc64-4e16deb97d10",
                CardType = CardType.Campaign,
                Keywords = new List<string>() { "You are playing Campaign Mode." },
                EncounterSet = "The Lord of the Rings Part 1",
                Quantity = 1,
                Setup = "-----",
                Number = 31
            });
            Cards.Add(new Card() {
                Title = "A Knife in the Dark",
                Id = "2efa631a-eb16-4078-84a5-18c7033b86f3",
                CardType = CardType.Campaign,
                Keywords = new List<string>() { "You are playing Campaign Mode." },
                EncounterSet = "The Lord of the Rings Part 2",
                Quantity = 1,
                Setup = "-----------",
                Number = 32
            });
            Cards.Add(new Card() {
                Title = "Flight to the Ford",
                Id = "0ee09c42-34c9-41aa-aa98-b582608b15bb",
                CardType = CardType.Campaign,
                Keywords = new List<string>() { "You are playing Campaign Mode." },
                EncounterSet = "The Lord of the Rings Part 3",
                Quantity = 1,
                Setup = "----------",
                Number = 33
            });
            Cards.Add(new Card() {
                Title = "Three is Company - 1A",
                Id = "6b34adb4-b510-4db9-aaba-9a1876626dce",
                CardType = CardType.Quest,
                EncounterSet = "A Shadow of the Past",
                Quantity = 1,
                Setup = "ttsl",
                Number = 34
            });
            Cards.Add(new Card() {
                Title = "A Shortcut to Mushrooms - 2A",
                Id = "40c7a675-d48f-4e71-8818-5f0ffb9fa0dd",
                CardType = CardType.Quest,
                EncounterSet = "A Shadow of the Past",
                Quantity = 1,
                Number = 35
            });
            Cards.Add(new Card() {
                Title = "Escape to Buckland - 3A",
                Id = "7371cfea-03d0-4bc0-98a1-d99e0acdbf88",
                CardType = CardType.Quest,
                EncounterSet = "A Shadow of the Past",
                Quantity = 1,
                Number = 36
            });
            Cards.Add(new Card() {
                Title = "Trouble in Bree - 1A",
                Id = "d0b6992d-9dc4-41fa-a483-b4ad0a3d60b3",
                CardType = CardType.Quest,
                EncounterSet = "A Knife in the Dark",
                Quantity = 1,
                Setup = "ttttsstttt",
                Number = 37
            });
            Cards.Add(new Card() {
                Title = "Into the Wild - 2A",
                Id = "28a4721b-3696-4fae-9eea-01b5a250c892",
                CardType = CardType.Quest,
                EncounterSet = "A Knife in the Dark",
                Quantity = 1,
                Number = 38
            });
            Cards.Add(new Card() {
                Title = "The Ringwraiths Attack - 3A",
                Id = "257f9b44-2744-4f02-bbf6-115d12b52e8d",
                CardType = CardType.Quest,
                EncounterSet = "A Knife in the Dark",
                Quantity = 1,
                Number = 39
            });
            Cards.Add(new Card() {
                Title = "Pursued by the Enemy - 1A",
                Id = "f9824e39-fda8-4e4b-bc74-83b4bd40c72d",
                CardType = CardType.Quest,
                EncounterSet = "Flight to the Ford",
                Quantity = 1,
                Setup = "ttttstttt",
                Number = 40
            });
            Cards.Add(new Card() {
                Title = "Race To Rivendell - 2A",
                Id = "6b2673af-c764-4599-a265-6e43d3b75310",
                CardType = CardType.Quest,
                EncounterSet = "Flight to the Ford",
                Quantity = 1,
                Number = 41
            });
            Cards.Add(new Card() {
                Title = "An Evil Wound",
                Id = "71a2ca21-ff50-4a8a-b30b-7c8eaf6bf6b6",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Morgul." },
                Keywords = new List<string>() { "Attach to the Ring-bearer.", " Attached hero cannot be healed." },
                EncounterSet = "Flight to the Ford",
                Quantity = 1,
                Number = 42
            });
            Cards.Add(new Card() {
                Title = "The Last Bridge",
                Id = "bc43ae57-6010-4924-919c-b37d1e33b958",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 4,
                Traits = new List<string>() { "Bridge.", " River." },
                Keywords = new List<string>() { "Immune to player card effects." },
                EncounterSet = "Flight to the Ford",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 43
            });
            Cards.Add(new Card() {
                Title = "The Troll's Camp",
                Id = "36dd0d2a-ba57-4ca0-b750-967c958e8bfb",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 2,
                Traits = new List<string>() { "Forest." },
                EncounterSet = "Flight to the Ford",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 44
            });
            Cards.Add(new Card() {
                Title = "Pain Assaulted Him",
                Id = "98bf5fdc-ff74-48e2-88c8-0e4134fd6984",
                CardType = CardType.Treachery,
                EncounterSet = "Flight to the Ford",
                Quantity = 3,
                Number = 45
            });
            Cards.Add(new Card() {
                Title = "Ford of Bruinen",
                Id = "3cd4bb66-1621-4534-aa25-421d0d8b7189",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 6,
                Traits = new List<string>() { "River." },
                Keywords = new List<string>() { "Immune to player card effects." },
                EncounterSet = "Flight to the Ford",
                VictoryPoints = 5,
                Quantity = 1,
                Number = 46
            });
            Cards.Add(new Card() {
                Title = "The Enemy is Upon Us!",
                Id = "0e12b3e5-d330-4a13-be7b-5a07a57a2273",
                CardType = CardType.Treachery,
                EncounterSet = "Flight to the Ford",
                Quantity = 2,
                Number = 47
            });
            Cards.Add(new Card() {
                Title = "Stricken Dumb",
                Id = "821a078b-ae42-4b69-bd49-cc235916be77",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                EncounterSet = "Flight to the Ford",
                Quantity = 3,
                Number = 48
            });
            Cards.Add(new Card() {
                Title = "The Nine are Abroad",
                Id = "98a2fb10-f2fc-462c-9f2e-8945419d9c4f",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                EncounterSet = "Flight to the Ford",
                Quantity = 2,
                Number = 49
            });
            Cards.Add(new Card() {
                Title = "Ettenmoors",
                Id = "ae435070-552a-43a9-816f-9f23f1a6dbba",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 3,
                Traits = new List<string>() { "Forest." },
                EncounterSet = "Flight to the Ford",
                Quantity = 3,
                Number = 50
            });
            Cards.Add(new Card() {
                Title = "The Old Road",
                Id = "edae55ef-8025-4553-9ff8-e81526effd59",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 4,
                Traits = new List<string>() { "Road." },
                EncounterSet = "Flight to the Ford",
                Quantity = 3,
                Number = 51
            });
            Cards.Add(new Card() {
                Title = "Fell Rider",
                Id = "5c31f690-9802-4638-8316-5380ef32575b",
                CardType = CardType.Enemy,
                EngagementCost = 30,
                Threat = 2,
                Attack = 4,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string>() { "Nazgul." },
                Keywords = new List<string>() { "Cannot have non-Morgul attachments." },
                EncounterSet = "Flight to the Ford",
                Quantity = 4,
                Number = 52
            });
            Cards.Add(new Card() {
                Title = "Squint-Eyed Southerner",
                Id = "795579db-249d-4c9d-850f-6f39bc96c063",
                CardType = CardType.Enemy,
                EngagementCost = 33,
                Threat = 2,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Spy." },
                EncounterSet = "A Knife in the Dark",
                Quantity = 3,
                Number = 53
            });
            Cards.Add(new Card() {
                Title = "Bill Ferny",
                Id = "6a3e1c91-accd-476b-9ecc-ace35274a052",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 38,
                Threat = 3,
                Attack = 1,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Bree.", " Spy." },
                Keywords = new List<string>() { "Players cannot optionally engage Bill Ferny." },
                EncounterSet = "A Knife in the Dark",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 54
            });
            Cards.Add(new Card() {
                Title = "Shady Bree-Lander",
                Id = "0e1451ba-5fa6-4a13-869b-d23d9ecef15e",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Bree.", " Spy." },
                EncounterSet = "A Knife in the Dark",
                Quantity = 4,
                Number = 55
            });
            Cards.Add(new Card() {
                Title = "Rider of Mordor",
                Id = "33a575b4-e373-46db-b11e-90f8fc41ef6e",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Threat = 4,
                Attack = 4,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Nazgul." },
                Keywords = new List<string>() { "Cannot have non-Morgul attachments." },
                EncounterSet = "A Knife in the Dark",
                Quantity = 2,
                Number = 56
            });
            Cards.Add(new Card() {
                Title = "Weather Hills",
                Id = "ddb20145-e570-4099-8d8a-11d64895939b",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 2,
                Traits = new List<string>() { "Forest.", " Hills." },
                EncounterSet = "A Knife in the Dark",
                Quantity = 3,
                Number = 57
            });
            Cards.Add(new Card() {
                Title = "Chetwood",
                Id = "df23bd50-5ca3-4a0e-a83c-a349632b0ff4",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 2,
                Traits = new List<string>() { "Forest." },
                EncounterSet = "A Knife in the Dark",
                Quantity = 3,
                Number = 58
            });
            Cards.Add(new Card() {
                Title = "The Prancing Pony",
                Id = "984d2a01-f12a-4e8b-800c-6a04b1904049",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "Bree." },
                EncounterSet = "A Knife in the Dark",
                VictoryPoints = 1,
                Quantity = 2,
                Number = 59
            });
            Cards.Add(new Card() {
                Title = "Midgewater",
                Id = "90d620e5-d7be-42e6-9756-b92761ecd8c9",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 6,
                Traits = new List<string>() { "Marshland." },
                EncounterSet = "A Knife in the Dark",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 60
            });
            Cards.Add(new Card() {
                Title = "Weathertop",
                Id = "ad09f848-9430-41c8-8723-671f08b79078",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 4,
                Traits = new List<string>() { "Hills.", " Ruins." },
                Keywords = new List<string>() { "Immune to player card effects." },
                EncounterSet = "A Knife in the Dark",
                VictoryPoints = 4,
                Quantity = 1,
                Number = 61
            });
            Cards.Add(new Card() {
                Title = "Unwanted Attention",
                Id = "d47ece86-2645-4d35-8ab7-c439d12abe65",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                EncounterSet = "A Knife in the Dark",
                Quantity = 3,
                Number = 62
            });
            Cards.Add(new Card() {
                Title = "Black Breath",
                Id = "e6721e44-5706-4a0f-a895-ced75f9c6219",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                EncounterSet = "A Knife in the Dark",
                Quantity = 2,
                Number = 63
            });
            Cards.Add(new Card() {
                Title = "Have You Seen Baggins?",
                Id = "3104ef1f-4f1c-4603-87cb-f3a70f5e6710",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril.", " Surge." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 2,
                Number = 64
            });
            Cards.Add(new Card() {
                Title = "Crawling Towards Him",
                Id = "79b5dd16-2427-4950-94e9-46905eebe56d",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril.", " Hide 2." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 3,
                Number = 65
            });
            Cards.Add(new Card() {
                Title = "Hunting For The Ring",
                Id = "a46578d6-303e-4f35-9a11-8b9f807427ba",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Doomed 2." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 3,
                Number = 66
            });
            Cards.Add(new Card() {
                Title = "Black Rider",
                Id = "57107fd1-0092-41d3-ae9d-8ff6c06933eb",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Threat = 4,
                Attack = 5,
                Defense = 4,
                HitPoints = 6,
                Traits = new List<string>() { "Nazgul." },
                Keywords = new List<string>() { "Hide 2.", " Cannot have non-Morgul attachments." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 5,
                Number = 67
            });
            Cards.Add(new Card() {
                Title = "Evil Crow",
                Id = "ff597a85-6a02-4301-a9dc-4dcb4cf9b626",
                CardType = CardType.Enemy,
                EngagementCost = 25,
                Threat = 2,
                Attack = 0,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Creature." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 3,
                Number = 68
            });
            Cards.Add(new Card() {
                Title = "Bag End",
                Id = "0d30fbf1-6e8f-480f-9f2f-3196777f6e11",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 3,
                Traits = new List<string>() { "Shire." },
                EncounterSet = "A Shadow of the Past",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 69
            });
            Cards.Add(new Card() {
                Title = "Woody End",
                Id = "b194fe1c-18c8-47d2-a674-8e209339c7e2",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 1,
                Traits = new List<string>() { "Forest." },
                EncounterSet = "A Shadow of the Past",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 70
            });
            Cards.Add(new Card() {
                Title = "Stock-Brook",
                Id = "2b38254c-e992-4fb5-89e8-e7f636658ce0",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 5,
                Traits = new List<string>() { "Forest.", " Stream." },
                EncounterSet = "A Shadow of the Past",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 71
            });
            Cards.Add(new Card() {
                Title = "Bamfurlong",
                Id = "82c68020-eb2e-4853-9919-df3066d13721",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 2,
                Traits = new List<string>() { "Shire." },
                EncounterSet = "A Shadow of the Past",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 72
            });
            Cards.Add(new Card() {
                Title = "The Marish",
                Id = "409bc89e-b5af-4626-ade6-60bcfcb290b7",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 4,
                Traits = new List<string>() { "Marshland." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 2,
                Number = 73
            });
            Cards.Add(new Card() {
                Title = "Stock Road",
                Id = "8df24501-bfd8-4602-92e1-61f5d5cf7035",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 3,
                Traits = new List<string>() { "Road." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 3,
                Number = 74
            });
            Cards.Add(new Card() {
                Title = "Green Hill Country",
                Id = "98dff367-a068-4aac-a46b-7dc201aff42d",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 3,
                Traits = new List<string>() { "Forest." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 3,
                Number = 75
            });
            Cards.Add(new Card() {
                Title = "Bucklebury Ferry",
                Id = "53ac8ba6-0487-4b1f-91e9-cf7ca9e7c1d0",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 9,
                Traits = new List<string>() { "River." },
                Keywords = new List<string>() { "Immune to player card effects." },
                EncounterSet = "A Shadow of the Past",
                Quantity = 1,
                Number = 76
            });
            Cards.Add(new Card() {
                Title = "Power In Their Terror",
                Id = "4765cbea-5ae8-4e39-a811-a67f1f6ca2a0",
                CardType = CardType.Treachery,
                EncounterSet = "The Nazgul",
                Quantity = 3,
                Number = 77
            });
            Cards.Add(new Card() {
                Title = "Pale Blade",
                Id = "4b36df3f-d75b-4b3a-9324-9ab31c10d7b9",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "The Nazgul",
                Quantity = 3,
                Number = 78
            });
            Cards.Add(new Card() {
                Title = "The Witch-king",
                Id = "c5657436-d6c8-400d-ba1f-a723799ac685",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 45,
                Threat = 5,
                Attack = 6,
                Defense = 5,
                HitPoints = 9,
                Traits = new List<string>() { "Nazgul." },
                Keywords = new List<string>() { "Immune to player card effects." },
                EncounterSet = "The Nazgul",
                Quantity = 1,
                Number = 79
            });
            Cards.Add(new Card() {
                Title = "Ringwraith",
                Id = "2d819f76-77dd-47a2-9a3f-ac8b8018f0df",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Threat = 3,
                Attack = 5,
                Defense = 4,
                HitPoints = 5,
                Traits = new List<string>() { "Nazgul." },
                Keywords = new List<string>() { "Cannot have non-Morgul attachments." },
                EncounterSet = "The Nazgul",
                Quantity = 4,
                Number = 80
            });
            Cards.Add(new Card() {
                Title = "Black Steed",
                Id = "09aeff64-6e0d-4dfa-af21-2e7805441376",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "The Black Riders",
                Quantity = 3,
                Number = 81
            });
            Cards.Add(new Card() {
                Title = "Rode Like a Gale",
                Id = "1851fafb-7f9a-46d7-a0f0-7fb2ad9ee567",
                CardType = CardType.Treachery,
                EncounterSet = "The Black Riders",
                Quantity = 2,
                Number = 82
            });
            Cards.Add(new Card() {
                Title = "Lure of the Ring",
                Id = "48384e79-d397-4dcd-9862-8a72acb448d0",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "The Ring",
                Quantity = 3,
                Number = 83
            });
            Cards.Add(new Card() {
                Title = "Pathless Country",
                Id = "32416ec8-b50d-4cd2-a62f-10f97c87be73",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 3,
                Traits = new List<string>() { "Forest." },
                EncounterSet = "Hunted",
                Quantity = 3,
                Number = 84
            });
            Cards.Add(new Card() {
                Title = "Piercing Cry",
                Id = "7e470346-1828-49c0-b241-58d0dbf27b4b",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                EncounterSet = "Hunted",
                Quantity = 3,
                Number = 85
            });
        }
    }
}
