using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class FoundationsofStone : CardSet
    {
        protected override void Initialize()
        {
            Name = "Foundations of Stone";

            Cards.Add(new Card() {
                Title = "Asfaloth",
                Id = "51223bd0-ffd1-11df-a976-0801212c9001",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 2,
                IsUnique = true,
                Traits = new List<string>() { "Mount." },
                Text = "Action: Exhaust Asfaloth to place 1 progress token on any location. (2 tokens instead if attached hero is Glorfindel.)",
                Keywords = new List<string>() { "Attach to a Noldor or Silvan hero." },
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Below the Mines - 4A",
                Id = "51223bd0-ffd1-11df-a976-0801212c9002",
                CardType = CardType.Quest,
                EncounterSet = "Foundations of Stone",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Below the Mines - 4A",
                Id = "51223bd0-ffd1-11df-a976-0801212c9004",
                CardType = CardType.Quest,
                EncounterSet = "Foundations of Stone",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Below the Mines - 4A",
                Id = "51223bd0-ffd1-11df-a976-0801212c9006",
                CardType = CardType.Quest,
                EncounterSet = "Foundations of Stone",
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Below the Mines - 4A",
                Id = "51223bd0-ffd1-11df-a976-0801212c9008",
                CardType = CardType.Quest,
                EncounterSet = "Foundations of Stone",
                Quantity = 1,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Daeron's Runes",
                Id = "51223bd0-ffd1-11df-a976-0801212c9010",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Draw 2 cards. Then, discard 1 card from your hand.",
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Deep Deep Dark",
                Id = "51223bd0-ffd1-11df-a976-0801212c9011",
                CardType = CardType.Treachery,
                EncounterSet = "Foundations of Stone",
                Quantity = 4,
                Text = "When Revealed: Attach 1 card from the top of the first player's deck to each Nameless enemy in play, if able.",
                Shadow = "Shadow: If attacking enemy is Nameless, the defending player must discard his hand.",
                Keywords = new List<string>() { "Doomed 1.", " Surge." },
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Drowned Treasury",
                Id = "51223bd0-ffd1-11df-a976-0801212c9012",
                CardType = CardType.Location,
                EncounterSet = "Foundations of Stone",
                Traits = new List<string>() { "Underground.", " Underwater." },
                Quantity = 3,
                Text = "If Drowned Treasury is the active location at the end of the quest phase, each player must discard 1 character he controls.Response: After Drowned Treasury leaves play as an explored location, each player may draw 2 cards or claim 1 objective in play.",
                Threat = 2,
                QuestPoints = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Durin's Axe",
                Id = "51223bd0-ffd1-11df-a976-0801212c9013",
                CardType = CardType.Objective,
                EncounterSet = "Foundations of Stone",
                Traits = new List<string>() { "Weapon.", " Artifact." },
                Quantity = 1,
                IsUnique = true,
                Text = "Attached hero gets +3 Attack. If attached hero is a Dwarf, it gets +1 Willpower.Action: Exhaust a hero to claim this objective. Then, attach Durin's Axe to that hero as an attachment.",
                Keywords = new List<string>() { "Surge.", " Restricted." },
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Durin's Helm",
                Id = "51223bd0-ffd1-11df-a976-0801212c9014",
                CardType = CardType.Objective,
                EncounterSet = "Foundations of Stone",
                Traits = new List<string>() { "Armor.", " Artifact." },
                Quantity = 1,
                IsUnique = true,
                Text = "Attached hero gets +1 Defense. If attached hero is a Dwarf, it gets +2 hit points.Action: Exhaust a hero to claim this objective. Then, attach Durin's Helm to that hero as an attachment.",
                Keywords = new List<string>() { "Surge." },
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Elder Nameless Thing",
                Id = "51223bd0-ffd1-11df-a976-0801212c9015",
                CardType = CardType.Enemy,
                EncounterSet = "Foundations of Stone",
                Traits = new List<string>() { "Nameless." },
                Quantity = 3,
                EngagementCost = 40,
                Attack = 0,
                Defense = 4,
                HitPoints = 0,
                Text = "Forced: After Elder Nameless Thing engages a player, attach the top 3 cards of that player's deck to it.X is the printed cost of all attached cards on this card. If there are no cards attached, X is 4.",
                Threat = 4,
                VictoryPoints = 3,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Glorfindel",
                Id = "51223bd0-ffd1-11df-a976-0801212c9016",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Noldor.", " Noble.", " Warrior." },
                Quantity = 1,
                ThreatCost = 5,
                IsUnique = true,
                Attack = 3,
                Defense = 1,
                Willpower = 3,
                HitPoints = 5,
                Text = "Forced: After Glorfindel exhausts to commit to a quest, raise your threat by 1.",
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Healing Herbs",
                Id = "51223bd0-ffd1-11df-a976-0801212c9017",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Discard Healing Herbs and exhaust attached hero to heal all damage on 1 character.",
                Keywords = new List<string>() { "Attach to a Î hero." },
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Heavy Stroke",
                Id = "51223bd0-ffd1-11df-a976-0801212c9018",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Response: After a Dwarf deals X damage to an enemy during combat, deal an additional X damage to that enemy. (Limit once per phase.)",
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Imladris Stargazer",
                Id = "51223bd0-ffd1-11df-a976-0801212c9019",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Noldor." },
                Quantity = 3,
                ResourceCost = 2,
                Attack = 0,
                Defense = 1,
                Willpower = 0,
                HitPoints = 1,
                Text = "Action: Exhaust Imladris Stargazer to choose a player. That player looks at the top 5 cards of his deck and then returns them to the top of his deck in any order.",
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Light of Valinor",
                Id = "51223bd0-ffd1-11df-a976-0801212c9020",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Condition." },
                Quantity = 3,
                ResourceCost = 1,
                IsUnique = true,
                Text = "Attached hero does not exhaust to commit to a quest.",
                Keywords = new List<string>() { "Attach to a Noldor or Silvan hero." },
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Longbeard Elder",
                Id = "51223bd0-ffd1-11df-a976-0801212c9021",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 2,
                HitPoints = 2,
                Text = "Response: After Longbeard Elder commits to a quest, look at the top card of the encounter deck. If that card is a location, place 1 progress token on the current quest. Otherwise, Longbeard Elder gets -1 Willpower until the end of the phase.",
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Lost and Alone",
                Id = "51223bd0-ffd1-11df-a976-0801212c9022",
                CardType = CardType.Treachery,
                EncounterSet = "Foundations of Stone",
                Quantity = 2,
                Text = "When Revealed: Each player chooses and shuffles a hero he controls into his deck. When he draws that hero, he puts it into play.",
                Shadow = "Shadow: If attacking enemy is Nameless, the defending player must discard his hand.",
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Mithril Lode",
                Id = "51223bd0-ffd1-11df-a976-0801212c9023",
                CardType = CardType.Location,
                EncounterSet = "Foundations of Stone",
                Traits = new List<string>() { "Underground." },
                Quantity = 1,
                Text = "While Mithril Lode is the active location, it gains: 'Refresh Action: Exhaust a character you control to place X progress tokens on the current quest card, bypassing any active location. X is the exhausted character's Willpower. (Limit once per round.)'",
                Keywords = new List<string>() { "Doomed 1." },
                Threat = 2,
                QuestPoints = 5,
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "Moria Bats",
                Id = "51223bd0-ffd1-11df-a976-0801212c9024",
                CardType = CardType.Enemy,
                EncounterSet = "Foundations of Stone",
                Traits = new List<string>() { "Creature." },
                Quantity = 4,
                EngagementCost = 33,
                Attack = 1,
                Defense = 2,
                HitPoints = 1,
                Text = "Only characters with ranged can attack or defend against Moria Bats.While Moria Bats is engaged with a player, it gets +1 Attack for each other enemy engaged with that player.",
                Threat = 1,
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "Nameless Thing",
                Id = "51223bd0-ffd1-11df-a976-0801212c9025",
                CardType = CardType.Enemy,
                EncounterSet = "Foundations of Stone",
                Traits = new List<string>() { "Nameless." },
                Quantity = 5,
                EngagementCost = 27,
                Attack = 0,
                Defense = 3,
                HitPoints = 0,
                Text = "Forced: After Nameless Thing engages a player, attach the top 2 cards of that player's deck to it.X is the printed cost of all attached cards on this card. If there are no cards attached, X is 3.",
                Threat = 3,
                VictoryPoints = 1,
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "Out of the Depths - 5A",
                Id = "51223bd0-ffd1-11df-a976-0801212c9026",
                CardType = CardType.Quest,
                EncounterSet = "Foundations of Stone",
                Quantity = 1,
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "Path of Need",
                Id = "51223bd0-ffd1-11df-a976-0801212c9028",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Condition." },
                Quantity = 3,
                ResourceCost = 4,
                Text = "Heroes do not exhaust to attack, defend, or commit to a quest while attached location is the active location.",
                Keywords = new List<string>() { "Limit 1 per deck.", " Attach to a location." },
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "The Dripping Walls - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801212c9029",
                CardType = CardType.Quest,
                EncounterSet = "Foundations of Stone",
                Quantity = 1,
                Setup = "t",
                Text = "Setup: Place the Foundations of Stone encounter set aside, out of play. The first player attaches Cave Torch to a hero of his choice.",
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "The Water's Edge - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801212c9031",
                CardType = CardType.Quest,
                EncounterSet = "Foundations of Stone",
                Quantity = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                Title = "Trollshaw Scout",
                Id = "51223bd0-ffd1-11df-a976-0801212c9033",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Noldor.", " Scout." },
                Quantity = 3,
                ResourceCost = 2,
                Attack = 2,
                Defense = 1,
                Willpower = 0,
                HitPoints = 2,
                Text = "Trollshaw Scout does not exhaust to attack.Forced: After Trollshaw Scout attacks, either discard it from play or discard 1 card from your hand.",
                Keywords = new List<string>() { "Ranged." },
                Number = 26
            });
            Cards.Add(new Card() {
                Title = "Washed Away! - 3A",
                Id = "51223bd0-ffd1-11df-a976-0801212c9034",
                CardType = CardType.Quest,
                EncounterSet = "Foundations of Stone",
                Quantity = 1,
                Number = 27
            });
        }
    }
}
