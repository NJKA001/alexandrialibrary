using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheHillsofEmynMuil : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Hills of Emyn Muil";
            Abbreviation = "THoEM";
            Number = 5;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "Shadows of Mirkwood";

            Cards.Add(new Card() {
                ImageName = "M1230",
                Title = "Amon Hen",
                Id = "51223bd0-ffd1-11df-a976-0801204c9001",
                CardType = CardType.Location,
                Traits = new List<string>() { "Emyn Muil." },
                Quantity = 1,
                IsUnique = true,
                Text = "While Amon Hen is the active location, players cannot play events.",
                Keywords = new List<string>() { "X is double the number of players in the game." },
                Threat = 0,
                QuestPoints = 5,
                EncounterSet = "The Hills of Emyn Muil",
                VictoryPoints = 5,
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1231",
                Title = "Amon Lhaw",
                Id = "51223bd0-ffd1-11df-a976-0801204c9002",
                CardType = CardType.Location,
                Traits = new List<string>() { "Emyn Muil." },
                Quantity = 1,
                IsUnique = true,
                Text = "While Amon Lhaw is the active location, treat all attachments as if their printed text boxes were blank.",
                Keywords = new List<string>() { "X is double the number of players in the game." },
                Threat = 0,
                QuestPoints = 5,
                EncounterSet = "The Hills of Emyn Muil",
                VictoryPoints = 5,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1219",
                Title = "Brand son of Bain",
                Id = "51223bd0-ffd1-11df-a976-0801204c9003",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Dale." },
                Quantity = 1,
                ThreatCost = 10,
                IsUnique = true,
                Attack = 3,
                Defense = 2,
                Willpower = 2,
                HitPoints = 3,
                Text = "Response: After Brand son of Bain attacks and defeats an enemy engaged with another player, choose and ready one of that player's characters.",
                Keywords = new List<string>() { "Ranged." },
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1222",
                Title = "Descendant of Thorondor",
                Id = "51223bd0-ffd1-11df-a976-0801204c9004",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Quantity = 3,
                ResourceCost = 4,
                Attack = 2,
                Defense = 1,
                Willpower = 1,
                HitPoints = 2,
                Text = "Descendant of Thorondor cannot have restricted attachments.Response: After Descendant of Thorondor enters or leaves play, deal 2 damage to any 1 enemy in the staging area.",
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1226",
                Title = "Gildor Inglorion",
                Id = "51223bd0-ffd1-11df-a976-0801204c9005",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Noldor." },
                Quantity = 3,
                ResourceCost = 5,
                IsUnique = true,
                Attack = 2,
                Defense = 3,
                Willpower = 3,
                HitPoints = 3,
                Text = "Action: Exhaust Gildor Inglorion to look at the top 3 cards of your deck. Switch one of those cards with a card from your hand. Then, return the 3 cards to the top of your deck, in any order.",
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1227",
                Title = "Gildor's Counsel",
                Id = "51223bd0-ffd1-11df-a976-0801204c9006",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 3,
                Text = "Play during the Quest phase, before characters are committed to the Quest.Action: Reveal 1 less card from the encounter deck this phase. (To a minimum of 1.)",
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1238",
                Title = "Impassable Chasm",
                Id = "51223bd0-ffd1-11df-a976-0801204c9007",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Hazard." },
                Quantity = 4,
                Text = "When Revealed: If there is an active location, remove all progress tokens from that location and return it to the staging area. If no location is moved by this effect, this card gains surge.",
                EncounterSet = "The Hills of Emyn Muil",
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1220",
                Title = "Keen-eyed Took",
                Id = "51223bd0-ffd1-11df-a976-0801204c9008",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Hobbit." },
                Quantity = 3,
                ResourceCost = 2,
                Attack = 0,
                Defense = 0,
                Willpower = 1,
                HitPoints = 2,
                Text = "Response: After Keen-eyed Took enters play, reveal the top card of each player's deck.Action: Return Keen-eyed Took to your hand to discard the top card of each player's deck.",
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1223",
                Title = "Meneldor's Flight",
                Id = "51223bd0-ffd1-11df-a976-0801204c9009",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Choose an Eagle ally. Return that character to its owner's hand.",
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1241",
                Title = "Orc Horse Thieves",
                Id = "51223bd0-ffd1-11df-a976-0801204c9010",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Mordor.", " Orc." },
                Quantity = 3,
                EngagementCost = 35,
                Attack = 1,
                Defense = 2,
                HitPoints = 6,
                Text = "Orc Horse Thieves get +1 Attack for each location in the staging area.",
                Keywords = new List<string>() { "Doomed 2." },
                Threat = 3,
                EncounterSet = "The Hills of Emyn Muil",
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1234",
                Title = "Rauros Falls",
                Id = "51223bd0-ffd1-11df-a976-0801204c9011",
                CardType = CardType.Location,
                Traits = new List<string>() { "Emyn Muil." },
                Quantity = 2,
                Text = "While Rauros Falls is the active location, all characters must commit to the current quest during the quest phase.",
                Shadow = "Shadow: After this attack resolves, return attacking enemy to the staging area.",
                Threat = 2,
                QuestPoints = 4,
                EncounterSet = "The Hills of Emyn Muil",
                VictoryPoints = 3,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1221",
                Title = "Rear Guard",
                Id = "51223bd0-ffd1-11df-a976-0801204c9012",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Quest Action: Discard a Leadership ally to give each hero committed to this quest +1 Willpower until the end of the phase.",
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1225",
                Title = "Ride to Ruin",
                Id = "51223bd0-ffd1-11df-a976-0801204c9013",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Discard a Rohan ally to choose a location. Place 3 progress tokens on that location.",
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1239",
                Title = "Rockslide",
                Id = "51223bd0-ffd1-11df-a976-0801204c9014",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Hazard." },
                Quantity = 3,
                Text = "When Revealed: Deal 2 damage to each character committed to this quest.",
                Shadow = "Shadow: Remove defending character from combat. This attack is considered undefended.",
                EncounterSet = "The Hills of Emyn Muil",
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1240",
                Title = "Slick Footing",
                Id = "51223bd0-ffd1-11df-a976-0801204c9015",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Hazard." },
                Quantity = 3,
                Text = "When Revealed: Remove 1 progress token from each location in play. Then, discard the top card of each player's deck for each progress token removed by this effect.",
                EncounterSet = "The Hills of Emyn Muil",
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1228",
                Title = "Song of Travel",
                Id = "51223bd0-ffd1-11df-a976-0801204c9016",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Song." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attached hero gains a Spirit resource icon.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1232",
                Title = "The East Wall of Rohan",
                Id = "51223bd0-ffd1-11df-a976-0801204c9017",
                CardType = CardType.Location,
                Traits = new List<string>() { "Emyn Muil." },
                Quantity = 2,
                Text = "While The East Wall of Rohan is the active location, non-Rohan characters cost 2 additional matching resources to play.",
                Threat = 4,
                QuestPoints = 2,
                EncounterSet = "The Hills of Emyn Muil",
                VictoryPoints = 3,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1237",
                Title = "The Highlands",
                Id = "51223bd0-ffd1-11df-a976-0801204c9018",
                CardType = CardType.Location,
                Traits = new List<string>() { "Emyn Muil." },
                Quantity = 4,
                Text = "Travel: In order to travel to The Highlands, the players must reveal the top card of the encounter deck, and add it to the staging area.",
                Threat = 1,
                QuestPoints = 1,
                EncounterSet = "The Hills of Emyn Muil",
                VictoryPoints = 1,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1229",
                Title = "The Hills of Emyn Muil - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801204c9019",
                CardType = CardType.Quest,
                Quantity = 1,
                Setup = "ss",
                Text = "Setup: Search the encounter deck for Amon Hen and Amon Lhaw, and add them to the staging area. Then shuffle the encounter deck.",
                EncounterSet = "The Hills of Emyn Muil",
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1233",
                Title = "The North Stair",
                Id = "51223bd0-ffd1-11df-a976-0801204c9021",
                CardType = CardType.Location,
                Traits = new List<string>() { "Emyn Muil." },
                Quantity = 2,
                Text = "Forced: After traveling to The North Stair, move the top card of the encounter discard pile to the staging area. Resolve any 'when revealed' effects on that card.",
                Threat = 3,
                QuestPoints = 3,
                EncounterSet = "The Hills of Emyn Muil",
                VictoryPoints = 3,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1236",
                Title = "The Outer Ridge",
                Id = "51223bd0-ffd1-11df-a976-0801204c9022",
                CardType = CardType.Location,
                Traits = new List<string>() { "Emyn Muil." },
                Quantity = 3,
                Text = "While The Outer Ridge is the active location, each location in the staging area gets +1 Threat.",
                Shadow = "Shadow: After this attack resolves, return attacking enemy to the staging area.",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "The Hills of Emyn Muil",
                VictoryPoints = 2,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1224",
                Title = "The Riddermark's Finest",
                Id = "51223bd0-ffd1-11df-a976-0801204c9023",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Creature.", " Rohan." },
                Quantity = 3,
                ResourceCost = 2,
                Attack = 1,
                Defense = 0,
                Willpower = 1,
                HitPoints = 2,
                Text = "Action: Exhaust and discard The Riddermark's Finest to place 2 progress tokens on any location.",
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1235",
                Title = "The Shores of Nen Hithoel",
                Id = "51223bd0-ffd1-11df-a976-0801204c9024",
                CardType = CardType.Location,
                Traits = new List<string>() { "Emyn Muil." },
                Quantity = 3,
                Text = "Travel: The first player must discard 1 event card from his hand to travel to this location.",
                Shadow = "Shadow: After this attack resolves, return attacking enemy to the staging area.",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "The Hills of Emyn Muil",
                VictoryPoints = 2,
                Number = 23
            });
        }
    }
}
