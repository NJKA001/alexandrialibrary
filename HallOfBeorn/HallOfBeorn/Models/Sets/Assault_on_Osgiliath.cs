using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class AssaultonOsgiliath : CardSet
    {
        protected override void Initialize()
        {
            Name = "Assault on Osgiliath";
            Number = 19;
            SetType = Models.SetType.Adventure_Pack;

            Cards.Add(new Card() {
                ImageName = "M1859",
                Title = "Faramir",
                Id = "323ebfa3-57e5-4394-9f55-284b2f7ee0be",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                ThreatCost = 11,
                Willpower = 2,
                Attack = 2,
                Defense = 2,
                HitPoints = 5,
                Traits = new List<string>() { "Gondor.", " Ranger.", " Noble." },
                Keywords = new List<string>() { "Ranged." },
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1860",
                Title = "Sword of Morthond",
                Id = "98ba9e54-d6c3-41ff-b886-81a29e29eb64",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Traits = new List<string>() { "Item.", " Weapon." },
                Keywords = new List<string>() { "Attach to a Gondor ally." },
                Quantity = 3,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1861",
                Title = "Men of the West",
                Id = "a2d440c4-6150-4b6f-9a36-faa51ace7908",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                IsVariableCost = true,
                Traits = new List<string>() { "Outlands." },
                Text = "Action: Return X Outlands allies from your discard pile to your hand.",
                Quantity = 3,
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1862",
                Title = "Knight of Minas Tirith",
                Id = "237b31e7-d0b0-4c1e-bd4a-40a175f7d7d1",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 3,
                Willpower = 0,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Warrior." },
                Quantity = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1863",
                Title = "Gondorian Fire",
                Id = "a7f12d87-5f28-46ca-a301-0ac48ca5e471",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Traits = new List<string>() { "Gondor." },
                Keywords = new List<string>() { "Attach to a Gondor or Dunedain hero." },
                Text = "Action: Spend 1 resource from attached hero's resource pool to give attached hero +1 Attack for each resource in its resource pool until the end of the phase. (Limit once per phase.)",
                Quantity = 3,
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1864",
                Title = "Pelargir Shipwright",
                Id = "d8d1e7b4-3639-4ca0-bc83-daa7f78554b2",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Willpower = 0,
                Attack = 1,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Craftsman." },
                Quantity = 3,
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1865",
                Title = "Map of Earnil",
                Id = "72cb5c31-c62f-4870-a5f4-099cdec1d4a7",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                ResourceCost = 4,
                Traits = new List<string>() { "Record." },
                Keywords = new List<string>() { "Attach to a Spirit hero." },
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1866",
                Title = "Ranger Bow",
                Id = "3fa0b17f-a7d1-4f0c-a779-c20cb6084e78",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Traits = new List<string>() { "Weapon." },
                Keywords = new List<string>() { "Attach to a Ranger character.", " Restricted." },
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1867",
                Title = "Forest Patrol",
                Id = "50aa4aab-6daa-4cb5-bfb1-a13db03c1a23",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Traits = new List<string>() { "Trap." },
                Keywords = new List<string>() { "Play only if you control at least 1 Ranger character." },
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1868",
                Title = "Palantir",
                Id = "1e9a6c59-8dc7-4dc4-a5a4-f5f4f9ccdc55",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                ResourceCost = 1,
                Traits = new List<string>() { "Artifact.", " Item." },
                Keywords = new List<string>() { "Attach to a Noble hero." },
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1869",
                Title = "Retake the City - 1A",
                Id = "c46c37c3-b2e7-4e28-9466-e3a371417c8b",
                CardType = CardType.Quest,
                EncounterSet = "Assault on Osgiliath",
                Quantity = 1,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1870",
                Title = "Uruk Lieutenant",
                Id = "bfa3b9da-d0e3-4ad6-8d1d-f32975f69551",
                CardType = CardType.Enemy,
                EngagementCost = 40,
                Threat = 2,
                Attack = 6,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Orc.", " Uruk.", " Mordor." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 2,
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1871",
                Title = "Uruk Soldier",
                Id = "f89b8962-1081-4f61-bc08-b2ca4eb08ba8",
                CardType = CardType.Enemy,
                EngagementCost = 30,
                Threat = 1,
                Attack = 4,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Orc.", " Uruk.", " Mordor." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 4,
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1872",
                Title = "Southron Phalanx",
                Id = "1c95d49a-5638-4f31-a42f-f93a5d1f50db",
                CardType = CardType.Enemy,
                EngagementCost = 25,
                Threat = 1,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Harad." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 3,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1873",
                Title = "Southron Commander",
                Id = "264adf61-31e1-46db-a655-7fee1d4282ae",
                CardType = CardType.Enemy,
                EngagementCost = 37,
                Threat = 3,
                Attack = 5,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string>() { "Harad." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 2,
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1874",
                Title = "West Gate",
                Id = "5a3d198a-5c72-42ec-b5ae-f183194632cf",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 5,
                Traits = new List<string>() { "Osgiliath." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 1,
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1875",
                Title = "The King's Library",
                Id = "fa9df618-126b-4ea9-95d7-22cd5dad6d2f",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 3,
                Traits = new List<string>() { "Osgiliath." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 1,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1876",
                Title = "Ancient Harbor",
                Id = "0c44ed2b-976a-4b17-b7e7-e64b20f74fd7",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 5,
                Traits = new List<string>() { "Osgiliath." },
                Keywords = new List<string>() { "The players cannot travel here." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 1,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1877",
                Title = "The Old Bridge",
                Id = "e46a15c0-abbe-4a2e-8951-0e9950aa0288",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 6,
                Traits = new List<string>() { "Osgiliath." },
                Keywords = new List<string>() { "The players cannot travel here." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 1,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1878",
                Title = "West Quarter",
                Id = "b5c7bf41-7642-4d25-9794-cb247f540182",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 3,
                Traits = new List<string>() { "Osgiliath." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 2,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1879",
                Title = "East Quarter",
                Id = "93bf98f7-fe0f-4c03-bd93-2f8d5753acce",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 3,
                Traits = new List<string>() { "Osgiliath." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 2,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1880",
                Title = "Ruined Square",
                Id = "fb301d7d-7936-49b6-a938-40ff0ca549cb",
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 2,
                Traits = new List<string>() { "Osgiliath." },
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 3,
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1881",
                Title = "Ruined Tower",
                Id = "ae5727cb-4d88-4a28-99be-84a565292b67",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 2,
                Traits = new List<string>() { "Osgiliath." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 3,
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1882",
                Title = "Pinned Down",
                Id = "03b6d303-7722-4319-9623-e182b50c90b2",
                CardType = CardType.Treachery,
                EncounterSet = "Assault on Osgiliath",
                Quantity = 2,
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1883",
                Title = "Street Fighting",
                Id = "2573b8e3-d3a1-47c0-acc8-e6151f223383",
                CardType = CardType.Treachery,
                EncounterSet = "Assault on Osgiliath",
                Quantity = 2,
                Number = 25
            });
            Cards.Add(new Card() {
                ImageName = "M1884",
                Title = "Counter-attack",
                Id = "1f1ad7bb-68ca-4b48-b7fa-516e5314e272",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "Assault on Osgiliath",
                Quantity = 2,
                Number = 26
            });
        }
    }
}
