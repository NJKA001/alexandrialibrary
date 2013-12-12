using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class ReturntoMirkwood : CardSet
    {
        protected override void Initialize()
        {
            Name = "Return to Mirkwood";
            Abbreviation = "RtM";
            Number = 7;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "Shadows of Mirkwood";

            Cards.Add(new Card() {
                ImageName = "M1276",
                Title = "Ambush - 4A",
                Id = "51223bd0-ffd1-11df-a976-0801206c9001",
                CardType = CardType.Quest,
                Quantity = 1,
                EncounterSet = "Return to Mirkwood",
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1269",
                Title = "Astonishing Speed",
                Id = "51223bd0-ffd1-11df-a976-0801206c9003",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Quantity = 3,
                ResourceCost = 3,
                Text = "Action: Until the end of the phase, all Rohan characters get +2 Willpower.",
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1489",
                Title = "Attercop, Attercop",
                Id = "51223bd0-ffd1-11df-a976-0801206c9004",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Creature.", " Spider." },
                Quantity = 3,
                EngagementCost = 44,
                Attack = 8,
                Defense = 4,
                HitPoints = 6,
                Text = "Forced: At the beginning of the encounter phase, Attercop, Attercop automatically engages the player guarding Gollum, regardless of his threat.",
                Threat = 2,
                EncounterSet = "Return to Mirkwood",
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1263",
                Title = "Dain Ironfoot",
                Id = "51223bd0-ffd1-11df-a976-0801206c9005",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 1,
                ThreatCost = 11,
                IsUnique = true,
                Attack = 2,
                Defense = 3,
                Willpower = 1,
                HitPoints = 5,
                Text = "While Dain Ironfoot is ready, Dwarf characters get +1 Attack and +1 Willpower.",
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1265",
                Title = "Dawn Take You All",
                Id = "51223bd0-ffd1-11df-a976-0801206c9006",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Quantity = 3,
                ResourceCost = 2,
                Text = "Play after shadow cards have been dealt, before any attacks have resolved.Combat Action: Each player may choose and discard 1 facedown shadow card from an enemy with which he is engaged.",
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1279",
                Title = "Dry Watercourse",
                Id = "51223bd0-ffd1-11df-a976-0801206c9007",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Quantity = 3,
                Text = "While Dry Watercourse is the active location, all treachery card effects that target the player guarding Gollum also target each other player.",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "Return to Mirkwood",
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1264",
                Title = "Dúnedain Signal",
                NormalizedTitle = "Dunedain Signal",
                Id = "51223bd0-ffd1-11df-a976-0801206c9008",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Signal." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attached hero gains sentinel.\r\nAction: Pay 1 resource from attached hero's pool to attach Dunedain Signal to another hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1266",
                Title = "Eagles of the Misty Mountains",
                Id = "51223bd0-ffd1-11df-a976-0801206c9009",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Quantity = 3,
                ResourceCost = 4,
                Attack = 2,
                Defense = 2,
                Willpower = 2,
                HitPoints = 4,
                Text = "Eagles of the Misty Mountains cannot have restricted attachments. Eagles of the Misty Mountains gets +1 Attack and +1 Defense for each facedown attachment it has.Response: After another Eagle character leaves play, you may attach that card facedown to Eagles of the Misty Mountains.",
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1274",
                Title = "Escape Attempt - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801206c9010",
                CardType = CardType.Quest,
                Quantity = 1,
                EncounterSet = "Return to Mirkwood",
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1277",
                Title = "Gollum",
                Id = "51223bd0-ffd1-11df-a976-0801206c9012",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Creature." },
                Quantity = 1,
                IsUnique = true,
                Attack = 0,
                Defense = 0,
                Willpower = 0,
                HitPoints = 5,
                Text = "Damage from undefended attacks against you must be dealt to Gollum. If Gollum is destroyed, or if the player guarding Gollum is eliminated, the players have lost the game.Forced: At the end of each round, raise the threat of the player guarding Gollum by 3. Then, that player may choose a new player to guard Gollum.",
                EncounterSet = "Return to Mirkwood",
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1282",
                Title = "Gollum's Anguish",
                Id = "51223bd0-ffd1-11df-a976-0801206c9013",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Tantrum." },
                Quantity = 2,
                Text = "When Revealed: Raise the threat of the player guarding Gollum by 8. That player must choose a new player to guard Gollum, if able.",
                Shadow = "Shadow: Raise the threat of the player guarding Gollum by 4.",
                EncounterSet = "Return to Mirkwood",
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1283",
                Title = "Gollum's Bite",
                Id = "51223bd0-ffd1-11df-a976-0801206c9014",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Tantrum." },
                Quantity = 2,
                Text = "When Revealed: Deal 4 damage to a hero controlled by the player guarding Gollum. That player must choose a new player to guard Gollum, if able.",
                Shadow = "Shadow: Deal 2 damage to a hero controlled by the player guarding Gollum.",
                EncounterSet = "Return to Mirkwood",
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1285",
                Title = "Mirkwood Bats",
                Id = "51223bd0-ffd1-11df-a976-0801206c9015",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Creature." },
                Quantity = 4,
                EngagementCost = 22,
                Attack = 1,
                Defense = 1,
                HitPoints = 1,
                Text = "Forced: After Mirkwood Bats engages a player, deal 1 damage to each character controlled by the player guarding Gollum.",
                Keywords = new List<string>() { "Surge." },
                Threat = 1,
                EncounterSet = "Return to Mirkwood",
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1270",
                Title = "Mirkwood Runner",
                Id = "51223bd0-ffd1-11df-a976-0801206c9016",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Silvan.", " Scout." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 2,
                Defense = 0,
                Willpower = 1,
                HitPoints = 2,
                Text = "While Mirkwood Runner is attacking alone, the defending enemy does not count its Defense.",
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1271",
                Title = "Rumour from the Earth",
                Id = "51223bd0-ffd1-11df-a976-0801206c9017",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Look at the top card of the encounter deck. Then, you may pay 1 Lore resource to return Rumour from the Earth to your hand.",
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1272",
                Title = "Shadow of the Past",
                Id = "51223bd0-ffd1-11df-a976-0801206c9018",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                Quantity = 3,
                ResourceCost = 2,
                Text = "Action: Move the top card of the encounter discard pile to the top of the encounter deck.",
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1267",
                Title = "Support of the Eagles",
                Id = "51223bd0-ffd1-11df-a976-0801206c9019",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Boon." },
                Quantity = 3,
                ResourceCost = 3,
                Text = "Action: Exhaust Support of the Eagles to choose an Eagle ally. Until the end of the phase, attached hero adds that ally's Attack or Defense (choose 1) to its own.",
                Keywords = new List<string>() { "Attach to a Tactics hero." },
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1278",
                Title = "The Spider's Ring",
                Id = "51223bd0-ffd1-11df-a976-0801206c9020",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Quantity = 4,
                Text = "While The Spider's Ring is the active location, the player guarding Gollum cannot change.",
                Shadow = "Shadow: If this attack is undefended, return any current active location to the staging area. The Spider's Ring becomes the active location.",
                Threat = 3,
                QuestPoints = 2,
                EncounterSet = "Return to Mirkwood",
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1273",
                Title = "Through the Forest - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801206c9021",
                CardType = CardType.Quest,
                Quantity = 1,
                Setup = "t",
                Text = "Setup: Search the encounter deck for Gollum. Choose a player to guard Gollum at the start of the game, and place Gollum in front of that player. Then shuffle the encounter deck. Reveal 1 card per player from the encounter deck, and add it to the staging area.",
                EncounterSet = "Return to Mirkwood",
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1275",
                Title = "To the Elvin King's Halls - 3A",
                Id = "51223bd0-ffd1-11df-a976-0801206c9023",
                CardType = CardType.Quest,
                Quantity = 1,
                EncounterSet = "Return to Mirkwood",
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1284",
                Title = "Wasted Provisions",
                Id = "51223bd0-ffd1-11df-a976-0801206c9025",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Tantrum." },
                Quantity = 3,
                Text = "When Revealed: Discard the top 10 cards from the deck of the player guarding Gollum. That player must choose a new player to guard Gollum, if able.",
                Shadow = "Shadow: Discard the top 5 cards from the deck of the player guarding Gollum.",
                EncounterSet = "Return to Mirkwood",
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1268",
                Title = "West Road Traveller",
                Id = "51223bd0-ffd1-11df-a976-0801206c9026",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Rohan." },
                Quantity = 3,
                ResourceCost = 2,
                Attack = 0,
                Defense = 0,
                Willpower = 2,
                HitPoints = 1,
                Text = "Response: After you play West Road Traveller from your hand, switch the active location with any other location in the staging area.",
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1281",
                Title = "Wood Elf Path",
                Id = "51223bd0-ffd1-11df-a976-0801206c9027",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Quantity = 3,
                Text = "Response: After the players travel to Wood Elf Path, the player guarding Gollum may choose a new player to guard him.",
                Threat = 1,
                QuestPoints = 3,
                EncounterSet = "Return to Mirkwood",
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1280",
                Title = "Woodman's Glade",
                Id = "51223bd0-ffd1-11df-a976-0801206c9028",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Quantity = 3,
                Text = "Travel: The player guarding Gollum must exhaust a hero he controls to travel to Woodman's Glade.Response: After exploring Woodman's Glade, reduce the threat of each player not guarding Gollum by 2.",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "Return to Mirkwood",
                Number = 24
            });
        }
    }
}
