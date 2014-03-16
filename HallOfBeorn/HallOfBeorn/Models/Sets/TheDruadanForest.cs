using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheDruadanForest : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Drúadan Forest";
            NormalizedName = "The Druadan Forest";
            Abbreviation = "TDF";
            Number = 17;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "Against the Shadow";

            Cards.Add(new Card() {
                ImageName = "M1810",
                Title = "Mirlonde",
                Id = "536c80ba-ad8b-447e-b378-1684508eb0f9",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                ThreatCost = 8,
                IsUnique = true,
                Attack = 2,
                Defense = 1,
                Willpower = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Silvan." },
                Text = "Each hero you control with a printed Lore resource icon gets -1 threat cost.",
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1811",
                Title = "Forlong",
                Id = "c6ae1840-dd7d-46ea-baf8-6d30614506de",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 3,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Outlands." },
                Text = "While you control Outlands allies that belong to 4 different spheres, ready Forlong at the beginning of each phase.",
                Quantity = 3,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1812",
                Title = "Strength of Arms",
                Id = "91f28bdf-4b78-4750-9853-65e783e4cb15",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Text = "Play only if each hero you control has a printed Leadership resource icon.Action: Ready each ally in play.",
                Quantity = 3,
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1813",
                Title = "Mighty Prowess",
                Id = "4ed8bd53-0453-4490-a4bb-20a7d793c17f",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Traits = new List<string>() { "Skill." },
                Text = "Response: After attached hero attacks and destroys an enemy, deal 1 damage to another enemy that shares a Trait with the enemy just destroyed.",
                Keywords = new List<string>() { "Attach to a Tactics hero.", " Limit 1 per hero." },
                Quantity = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1814",
                Title = "Trained for War",
                Id = "5c7a6b89-0439-4b46-b9f1-09189f981a0d",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Text = "Play only if each hero you control has the printed Tactics resource icon.Action: Until the end of the phase, if the current quest has no keyword it gains battle. (Characters quest using Attack instead of Willpower.)",
                Quantity = 3,
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1815",
                Title = "Silvan Refugee",
                Id = "e4fd6e25-982a-464f-82d8-812269864d46",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Attack = 0,
                Defense = 0,
                Willpower = 2,
                HitPoints = 1,
                Traits = new List<string>() { "Silvan." },
                Text = "Forced: After a character leaves play, discard Silvan Refugee from play.",
                Quantity = 3,
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1816",
                Title = "Against the Shadow",
                Id = "0801c2c0-2bf7-4a0a-838a-13740f6cdbaf",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Text = "Play only if each hero you control has the printed Spirit resource icon.Action: Until the end of the phase, Spirit characters use their Willpower instead of Defense.",
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1817",
                Title = "Harbor Master",
                Id = "b7f8b82b-c448-4f43-a025-bf6b7e6f0310",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Attack = 2,
                Defense = 0,
                Willpower = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Noldor." },
                Text = "Response: After a card effect adds any number of resources to the resource pool of a hero you control, Harbor Master gains +1 Defense until the end of the round.",
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1818",
                Title = "Advance Warning",
                Id = "e678be7e-8048-458d-b4f9-25c2a718fabb",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Text = "Play only if each hero you control has a printed Lore resource icon.Action: Until the end of the phase, enemies do not make engagement checks",
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1819",
                Title = "White Tower Watchman",
                Id = "31cd848b-a1bc-4ccd-af0c-eb3cc3ba593b",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                ResourceCost = 3,
                Attack = 0,
                Defense = 2,
                Willpower = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor." },
                Text = "If each hero you control belongs to the same sphere of influence, you may assign damage from undefended enemy attacks to White Tower Watchman instead of a hero you control.",
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1823",
                Title = "Drû-buri-Drû",
                NormalizedTitle = "Dru-buri-Dru",
                Id = "19b732b9-a26a-4535-a438-7dd83cd4ecf2",
                CardType = CardType.Enemy,
                EngagementCost = 1,
                IsUnique = true,
                Attack = 5,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string>() { "Wose." },
                Text = "Allies cannot defend against Drû-buri-Drû.While Drû-buri-Drû is in the victory display, characters get +1 Willpower and +1 Defense.Unless Drû-buri-Drû is in the victory display, the players cannot win.",
                Threat = 4,
                EncounterSet = "The Druadan Forest",
                Quantity = 1,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1824",
                Title = "Drúadan Drummer",
                NormalizedTitle = "Druadan Drummer",
                Id = "e2c02f11-fdfb-402b-89de-1729d1364e4c",
                CardType = CardType.Enemy,
                EngagementCost = 40,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Wose." },
                Text = "Each Wose enemy in the staging area gets +2 Threat.",
                Shadow = "Shadow: Each Wose enemy engaged with the defending player gets +1 Attack.",
                Threat = 0,
                EncounterSet = "The Druadan Forest",
                Quantity = 3,
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1825",
                Title = "Drúadan Elite",
                NormalizedTitle = "Druadan Elite",
                Id = "607c3e44-5add-4c80-bb3b-55b016b6cd0e",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Attack = 3,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Wose." },
                Shadow = "Shadow: Return attacking enemy to the staging area after it attacks unless any player pays 1 resource.",
                Keywords = new List<string>() { "Archery 2.", " Prowl X." },
                Text = "X is the number of players in the game.",
                Threat = 2,
                EncounterSet = "The Druadan Forest",
                Quantity = 3,
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1826",
                Title = "Drúadan Hunter",
                NormalizedTitle = "Druadan Hunter",
                Id = "809ff5de-8273-4491-adfb-3a27d4b316f3",
                CardType = CardType.Enemy,
                EngagementCost = 25,
                Attack = 4,
                Defense = 3,
                HitPoints = 3,
                Traits = new List<string>() { "Wose." },
                Shadow = "Shadow: Attacking enemy gets +2 Attack.",
                Keywords = new List<string>() { "Prowl 1.", " Archery X." },
                Text = "X is the number of heroes in play with no resources in their resource pool.",
                Threat = 2,
                EncounterSet = "The Druadan Forest",
                Quantity = 4,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1827",
                Title = "Drúadan Thief",
                NormalizedTitle = "Druadan Thief",
                Id = "85045e14-3abc-4d58-824f-249b1fb3643d",
                CardType = CardType.Enemy,
                EngagementCost = 5,
                Attack = 2,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Wose." },
                Shadow = "Shadow: Return attacking enemy to the staging area after it attacks unless any player pays 1 resource.",
                Keywords = new List<string>() { "Surge.", " Prowl 1." },
                Threat = 2,
                EncounterSet = "The Druadan Forest",
                Quantity = 3,
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1828",
                Title = "Ancestral Clearing",
                Id = "0ebedc5b-fd8b-46b8-a2a1-1a6f739e8819",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "The cost to play each player card is increased by 1.Travel: Exhaust a hero to travel here.",
                Threat = 4,
                QuestPoints = 3,
                EncounterSet = "The Druadan Forest",
                Quantity = 1,
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1829",
                Title = "Garden of Poisons",
                Id = "41c3779a-a28a-44db-8b9b-bc5e81d1111a",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "Heroes cannot gain resources from card effects.Travel: Each player must pay 1 resource to travel here.",
                Threat = 2,
                QuestPoints = 3,
                EncounterSet = "The Druadan Forest",
                Quantity = 3,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1830",
                Title = "Glade of Cleansing",
                Id = "801078c6-56a9-45f2-b5a4-7e38cb5bc507",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "Each Wose enemy gains Archery 1.X is equal to the total archery value of the highest archery Wose enemy in play.",
                Threat = 0,
                QuestPoints = 5,
                EncounterSet = "The Druadan Forest",
                Quantity = 3,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1831",
                Title = "Men in the Dark",
                Id = "24e431be-cde9-4f93-943a-de5e5ca9108b",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each hero must pay 1 resource or take 1 damage. If no hero takes damage from this effect, Men in the Dark gains surge.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each hero the defending player controls with no resources.",
                EncounterSet = "The Druadan Forest",
                Quantity = 3,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1832",
                Title = "Stars in Sky",
                Id = "7cbf23fd-28c4-494c-97aa-b7fed050ad8b",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each questing hero must pay 1 resource or it is removed from the quest.",
                Shadow = "Shadow: Discard all resources from the defending character's resource pool.",
                Keywords = new List<string>() { "Prowl 2." },
                EncounterSet = "The Druadan Forest",
                Quantity = 3,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1833",
                Title = "Leaves on Tree",
                Id = "9dda3cf1-e5bd-4cab-8c3b-b29db1220249",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must pay 1 resource for each attachment he controls or discard all attachments he controls.",
                Shadow = "Shadow: Discard an attachment from the defending character.",
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "The Druadan Forest.",
                Quantity = 2,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1820",
                Title = "The Pursuit - 1A",
                Id = "71079813-3afe-41b7-8746-92dcc1f91084",
                CardType = CardType.Quest,
                Setup = "t",
                Text = "Setup: Search the encounter deck for Drû-buri-Drû and set him aside, out of play. Shuffle the encounter deck.",
                EncounterSet = "The Druadan Forest",
                Quantity = 1,
                QuestPoints = 11,
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1821",
                Title = "An Untimely End - 2A",
                Id = "346fda89-94df-410a-8027-41eacbf27238",
                CardType = CardType.Quest,
                EncounterSet = "The Druadan Forest",
                Quantity = 1,
                QuestPoints = 17,
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1822",
                Title = "The Passage Out - 3A",
                Id = "f6578eb7-1b94-458b-aba1-82b406507a4d",
                CardType = CardType.Quest,
                Text = "When Revealed: Add Drû-buri-Drû to the staging area.",
                EncounterSet = "The Druadan Forest",
                Quantity = 1,
                QuestPoints = 14,
                Number = 24
            });
        }
    }
}
