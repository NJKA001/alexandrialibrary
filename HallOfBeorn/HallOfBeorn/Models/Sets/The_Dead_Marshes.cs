using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheDeadMarshes : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Dead Marshes";
            Number = 6;
            SetType = Models.SetType.Adventure_Pack;

            Cards.Add(new Card() {
                ImageName = "M1255",
                Title = "A Wisp of Pale Sheen",
                Id = "51223bd0-ffd1-11df-a976-0801205c9001",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Escape." },
                Quantity = 3,
                Text = "When Revealed: Place 2 resource tokens on Gollum. Any player may exhaust a Lore hero to reduce this effect to 1 resource token.Escape: 4",
                EncounterSet = "The Dead Marshes",
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1242",
                Title = "Boromir",
                Id = "51223bd0-ffd1-11df-a976-0801205c9002",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Gondor.", " Noble.", " Warrior." },
                Quantity = 1,
                ThreatCost = 11,
                IsUnique = true,
                Attack = 3,
                Defense = 2,
                Willpower = 1,
                HitPoints = 5,
                Text = "Action: Raise your threat by 1 to ready Boromir.Action: Discard Boromir to deal 2 damage to each enemy engaged with a single player.",
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1244",
                Title = "Dunedain Cache",
                Id = "51223bd0-ffd1-11df-a976-0801205c9003",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Item." },
                Quantity = 3,
                ResourceCost = 2,
                Text = "Attached hero gains ranged.Action: Pay 1 resource from attached hero's pool to attach Dunedain Cache to another hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1243",
                Title = "Dunedain Watcher",
                Id = "51223bd0-ffd1-11df-a976-0801205c9004",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Dunedain." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 2,
                Text = "Response: Discard Dunedain Watcher from play to cancel the shadow effects of a card just triggered.",
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1247",
                Title = "Elfhelm",
                Id = "51223bd0-ffd1-11df-a976-0801205c9005",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Rohan.", " Warrior." },
                Quantity = 3,
                ResourceCost = 4,
                IsUnique = true,
                Attack = 2,
                Defense = 2,
                Willpower = 1,
                HitPoints = 3,
                Text = "While Elfhelm is ready, he gains: 'Response: After your threat is raised as the result of questing unsuccessfully, or by an encounter or quest card effect, reduce your threat by 1.'",
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1250",
                Title = "Fast Hitch",
                Id = "51223bd0-ffd1-11df-a976-0801205c9006",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Skill." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Exhaust Fast Hitch to ready attached character.",
                Keywords = new List<string>() { "Attach to a Hobbit character." },
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1262",
                Title = "Fens and Mires",
                Id = "51223bd0-ffd1-11df-a976-0801205c9007",
                CardType = CardType.Location,
                Traits = new List<string>() { "Dead Marshes." },
                Quantity = 4,
                Text = "Forced: After the players travel to this location, place 1 resource token on Gollum.Escape: 2",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "The Dead Marshes",
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1259",
                Title = "Giant Marsh Worm",
                Id = "51223bd0-ffd1-11df-a976-0801205c9008",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Creature." },
                Quantity = 4,
                EngagementCost = 36,
                Attack = 3,
                Defense = 2,
                HitPoints = 6,
                Text = "Forced: Remove 2 damage from Giant Marsh Worm at the end of each round.Escape: 2",
                Threat = 1,
                EncounterSet = "The Dead Marshes",
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1254",
                Title = "Gollum",
                Id = "51223bd0-ffd1-11df-a976-0801205c9009",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Gollum." },
                Quantity = 1,
                IsUnique = true,
                Text = "If Gollum ever has 8 or more resource tokens on him, shuffle him back into the encounter deck.Forced: At the end of the quest phase, the party must make an escape test, dealing 1 card per player from the encounter deck. If this test is failed, place 2 resource tokens on Gollum.",
                EncounterSet = "The Dead Marshes",
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1260",
                Title = "Impassable Bog",
                Id = "51223bd0-ffd1-11df-a976-0801205c9010",
                CardType = CardType.Location,
                Traits = new List<string>() { "Dead Marshes." },
                Quantity = 4,
                Text = "When Revealed: Place 1 resource token on Gollum for each location card in the staging area.Escape: 2",
                Threat = 1,
                QuestPoints = 12,
                EncounterSet = "The Dead Marshes",
                VictoryPoints = 7,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1252",
                Title = "Into the Marshes - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801205c9011",
                CardType = CardType.Quest,
                Quantity = 1,
                Setup = "s",
                Text = "Setup: Search the encounter deck for Gollum, and add it to the staging area. Shuffle the encounter deck, then reveal 1 card per player from the encounter deck and add it to the staging area.",
                EncounterSet = "The Dead Marshes",
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1256",
                Title = "Nightfall",
                Id = "51223bd0-ffd1-11df-a976-0801205c9013",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Escape." },
                Quantity = 3,
                Text = "When Revealed: The first player makes an escape test, dealing 2 cards from the encounter deck. If this test is failed, place 1 resource token on Gollum and raise each player's threat by 2.Escape: 2",
                EncounterSet = "The Dead Marshes",
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1249",
                Title = "Silvan Tracker",
                Id = "51223bd0-ffd1-11df-a976-0801205c9014",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Silvan." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 3,
                Text = "Response: After a Silvan character readies during the refresh phase, heal 1 damage from that character.",
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1251",
                Title = "Song of Battle",
                Id = "51223bd0-ffd1-11df-a976-0801205c9015",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Song." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attached hero gains a Tactics resource icon.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1246",
                Title = "Song of Mocking",
                Id = "51223bd0-ffd1-11df-a976-0801205c9016",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Song." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Exhaust Song of Mocking to choose another hero. Until the end of the phase, attached hero takes all damage assigned to the chosen hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1253",
                Title = "The Capture - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801205c9017",
                CardType = CardType.Quest,
                Quantity = 1,
                EncounterSet = "The Dead Marshes",
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1261",
                Title = "The Heart of the Marshes",
                Id = "51223bd0-ffd1-11df-a976-0801205c9019",
                CardType = CardType.Location,
                Traits = new List<string>() { "Dead Marshes." },
                Quantity = 4,
                Text = "While The Heart of the Marshes is the active location, all cards dealt from the encounter deck for escape tests get +1 Escape. (Cards receive this bonus even if they do not have a printed escape value.)Escape: 1",
                Threat = 3,
                QuestPoints = 4,
                EncounterSet = "The Dead Marshes",
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1258",
                Title = "The Lights of the Dead",
                Id = "51223bd0-ffd1-11df-a976-0801205c9020",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Escape." },
                Quantity = 4,
                Text = "When Revealed: Each player must make an escape test, dealing 2 cards from the encounter deck for each test. Each player that fails this test places 1 resource token on Gollum, and raises his threat by 1.Escape: 5",
                EncounterSet = "The Dead Marshes",
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1257",
                Title = "Through the Mist",
                Id = "51223bd0-ffd1-11df-a976-0801205c9021",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Escape." },
                Quantity = 3,
                Text = "When Revealed: The first player makes an escape test counting Attack instead of Willpower, dealing 2 cards from the encounter deck. If this test is failed, place 1 resource token on Gollum and raise each player's threat by 1.Escape: 3",
                EncounterSet = "The Dead Marshes",
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1245",
                Title = "Vassal of the Windlord",
                Id = "51223bd0-ffd1-11df-a976-0801205c9022",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Quantity = 3,
                ResourceCost = 1,
                Attack = 3,
                Defense = 0,
                Willpower = 0,
                HitPoints = 1,
                Text = "Vassal of the Windlord cannot have restricted attachments.Forced: After an attack in which Vassal of the Windlord attacked resolves, discard Vassal of the Windlord from play.",
                Keywords = new List<string>() { "Ranged." },
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1248",
                Title = "We Do Not Sleep",
                Id = "51223bd0-ffd1-11df-a976-0801205c9023",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Quantity = 3,
                ResourceCost = 5,
                Text = "Action: Until the end of the phase, Rohan characters do not exhaust to commit to quests.",
                Number = 21
            });
        }
    }
}
