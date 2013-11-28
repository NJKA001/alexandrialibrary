using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class AJourneytoRhosgobel : CardSet
    {
        protected override void Initialize()
        {
            Name = "A Journey To Rhosgobel";
            Number = 4;
            SetType = Models.SetType.Adventure_Pack;

            Cards.Add(new Card() {
                ImageName = "M1197",
                Title = "Prince Imrahil",
                Id = "51223bd0-ffd1-11df-a976-0801203c9001",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 11,
                IsUnique = true,
                Attack = 3,
                Defense = 2,
                Willpower = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Gondor.", " Noble." },
                Text = "Response: After a character leaves play, ready Prince Imrahil. (Limit once per round.)",
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1204",
                Title = "Haldir of Lorien",
                Id = "51223bd0-ffd1-11df-a976-0801203c9002",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 4,
                IsUnique = true,
                Attack = 2,
                Defense = 2,
                Willpower = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Silvan." },
                Keywords = new List<string>() { "Ranged.", " Sentinel." },
                Quantity = 3,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1206",
                Title = "Radagast",
                Id = "51223bd0-ffd1-11df-a976-0801203c9003",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                ResourceCost = 5,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Istari." },
                Text = "Radagast collects 1 resource each resource phase. These resources can be used to pay for Creature cards played from your hand.Action: Spend X resources from Radagast's pool to heal X wounds on any 1 Creature.",
                Quantity = 3,
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1200",
                Title = "Landroval",
                Id = "51223bd0-ffd1-11df-a976-0801203c9004",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 5,
                IsUnique = true,
                Attack = 3,
                Defense = 1,
                Willpower = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Text = "Landroval cannot have restricted attachments.Response: After a hero card is destroyed, return Landroval to his owner's hand to put that hero back into play, with 1 damage token on it. (Limit once per game.)",
                Keywords = new List<string>() { "Sentinel." },
                Quantity = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1203",
                Title = "Ancient Mathom",
                Id = "51223bd0-ffd1-11df-a976-0801203c9005",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Traits = new List<string>() { "Mathom." },
                Text = "Response: After attached location is explored, the first player draws 3 cards.",
                Keywords = new List<string>() { "Attach to a location." },
                Quantity = 3,
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1205",
                Title = "Infighting",
                Id = "51223bd0-ffd1-11df-a976-0801203c9006",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Text = "Action: Move any number of damage from one enemy to another.",
                Quantity = 3,
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1199",
                Title = "Parting Gifts",
                Id = "51223bd0-ffd1-11df-a976-0801203c9007",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 0,
                Text = "Action: Move any number of resource tokens from a Leadership hero's resource pool to any other hero's resource pool.",
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1198",
                Title = "Dunedain Quest",
                Id = "51223bd0-ffd1-11df-a976-0801203c9008",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Traits = new List<string>() { "Signal." },
                Text = "Attached hero gains +1 Willpower.Action: Pay 1 resource from attached hero's pool to attach Dunedain Quest to another hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1202",
                Title = "Escort from Edoras",
                Id = "51223bd0-ffd1-11df-a976-0801203c9009",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Attack = 0,
                Defense = 0,
                Willpower = 2,
                HitPoints = 1,
                Traits = new List<string>() { "Rohan." },
                Text = "While committed to a quest, Escort from Edoras gets +2 Willpower.Forced: After resolving a quest to which Escort from Edoras was committed, discard Escort from Edoras from play.",
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1201",
                Title = "To the Eyrie",
                Id = "51223bd0-ffd1-11df-a976-0801203c9010",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Text = "Response: After an ally is destroyed, exhaust 1 Eagle character to move that ally from the discard pile to its owner's hand.",
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1216",
                Title = "Festering Wounds",
                Id = "51223bd0-ffd1-11df-a976-0801203c9011",
                CardType = CardType.Treachery,
                Text = "When Revealed: Deal 2 damage to each wounded character.",
                Shadow = "Shadow: Deal 1 damage to each wounded character. (2 damage instead if this attack is undefended.)",
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 2,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1210",
                Title = "Athelas",
                Id = "51223bd0-ffd1-11df-a976-0801203c9012",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Item." },
                Text = "Action: Exhaust a hero to claim this objective if it has no encounters attached. Then, attach Athelas to that hero.",
                Keywords = new List<string>() { "Guarded." },
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 4,
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1218",
                Title = "Black Forest Bats",
                Id = "51223bd0-ffd1-11df-a976-0801203c9013",
                CardType = CardType.Enemy,
                EngagementCost = 26,
                Attack = 1,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Creature." },
                Text = "Only Eagle characters or characters with ranged can attack or defend against Black Forest Bats.",
                Shadow = "Shadow: If this attack is undefended, the damage must be placed on Wilyador.",
                Threat = 1,
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 5,
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1214",
                Title = "Exhaustion",
                Id = "51223bd0-ffd1-11df-a976-0801203c9014",
                CardType = CardType.Treachery,
                Text = "When Revealed: Deal 2 damage to each exhausted character.",
                Shadow = "Shadow: Deal 1 damage to each exhausted character.",
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 4,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1213",
                Title = "Forest Grove",
                Id = "51223bd0-ffd1-11df-a976-0801203c9015",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "Response: After the players explore Forest Grove, search the encounter deck and discard pile for 1 Athelas objective, and add it to the staging area. Then, shuffle the encounter deck.",
                Threat = 2,
                QuestPoints = 3,
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 4,
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1217",
                Title = "Mirkwood Flock",
                Id = "51223bd0-ffd1-11df-a976-0801203c9016",
                CardType = CardType.Enemy,
                EngagementCost = 32,
                Attack = 2,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Creature." },
                Text = "Only Eagle characters or characters with ranged can attack or defend against Mirkwood Flock.",
                Shadow = "Shadow: If this attack is undefended, the damage must be placed on Wilyador.",
                Threat = 1,
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 4,
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1212",
                Title = "Rhosgobel",
                Id = "51223bd0-ffd1-11df-a976-0801203c9017",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "Forest." },
                Text = "While Rhosgobel is in the staging area, Wilyador cannot be healed.Travel: Players must complete stage one of this quest before they can travel to Rhosgobel.",
                Keywords = new List<string>() { "X is the number of players in the game." },
                Threat = 0,
                QuestPoints = 4,
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 1,
                VictoryPoints = 4,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1215",
                Title = "Swarming Insects",
                Id = "51223bd0-ffd1-11df-a976-0801203c9018",
                CardType = CardType.Treachery,
                Text = "When Revealed: Deal 1 damage to each character without any attachments.",
                Shadow = "Shadow: If a character (including Wilyador) has more damage than each other character, deal 3 additional damage to that character.",
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 4,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1211",
                Title = "Wilyador",
                Id = "51223bd0-ffd1-11df-a976-0801203c9019",
                CardType = CardType.Objective,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 20,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Text = "Forced: At the end of each round, Wilyador suffers 2 damage.Wilyador cannot be healed of more than 5 wounds by a single effect. If Wilyador leaves play, the players have lost the game.",
                Keywords = new List<string>() { "No attachments.", " The first player gains control of Wilyador, as an ally." },
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 1,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1207",
                Title = "The Wounded Eagle - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801203c9020",
                CardType = CardType.Quest,
                Setup = "ss",
                Text = "Setup: Search the encounter deck for Rhosgobel and Wilyador, and add them to the staging area with 2 damage tokens on Wilyador. Then, shuffle the encounter deck.",
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 1,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1208",
                Title = "Radagast's Request - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801203c9022",
                CardType = CardType.Quest,
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 1,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1209",
                Title = "Return to Rhosgobel - 3A",
                Id = "51223bd0-ffd1-11df-a976-0801203c9024",
                CardType = CardType.Quest,
                EncounterSet = "A Journey to Rhosgobel",
                Quantity = 1,
                Number = 22
            });
        }
    }
}
