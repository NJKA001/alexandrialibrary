using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheHuntforGollum : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Hunt for Gollum";
            Number = 2;
            SetType = Models.SetType.Adventure_Pack;

            Cards.Add(new Card() {
                ImageName = "M1148",
                Title = "Bilbo Baggins",
                Id = "51223bd0-ffd1-11df-a976-0801201c9001",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                ThreatCost = 9,
                IsUnique = true,
                Attack = 1,
                Defense = 2,
                Willpower = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit." },
                Text = "The first player draws 1 additional card in the resource phase.",
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1149",
                Title = "Dunedain Mark",
                Id = "51223bd0-ffd1-11df-a976-0801201c9002",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Traits = new List<string>() { "Signal." },
                Text = "Attached hero gains +1 Attack.Action: Pay 1 resource from attached hero's pool to attach Dunedain Mark to another hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Quantity = 3,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1150",
                Title = "Campfire Tales",
                Id = "51223bd0-ffd1-11df-a976-0801201c9003",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Text = "Action: Each player draws 1 card.",
                Quantity = 3,
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1154",
                Title = "Mustering the Rohirrim",
                Id = "51223bd0-ffd1-11df-a976-0801201c9004",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Text = "Action: Search the top 10 cards of your deck for any 1 Rohan ally card and add it to your hand. Then, shuffle the other cards back into your deck.",
                Quantity = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1155",
                Title = "Rivendell Minstrel",
                Id = "51223bd0-ffd1-11df-a976-0801201c9005",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Attack = 0,
                Defense = 0,
                Willpower = 2,
                HitPoints = 1,
                Traits = new List<string>() { "Noldor." },
                Text = "Response: After you play Rivendell Minstrel from your hand, search your deck for 1 Song card and add it to your hand. Shuffle your deck.",
                Quantity = 3,
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1157",
                Title = "Song of Kings",
                Id = "51223bd0-ffd1-11df-a976-0801201c9006",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                ResourceCost = 1,
                Traits = new List<string>() { "Song." },
                Text = "Attached hero gains a Leadership resource icon.",
                Keywords = new List<string>() { "Attach to a hero." },
                Quantity = 3,
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1156",
                Title = "Strider's Path",
                Id = "51223bd0-ffd1-11df-a976-0801201c9007",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Text = "Response: After a location is revealed from the encounter deck, immediately travel to that location without resolving its Travel effect. If another location is currently active, return it to the staging area.",
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1152",
                Title = "The Eagles Are Coming!",
                Id = "51223bd0-ffd1-11df-a976-0801201c9008",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Traits = new List<string>() { "Eagle." },
                Text = "Action: Search the top 5 cards of your deck for any number of Eagle cards and add them to your hand. Shuffle the other cards back into your deck.",
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1153",
                Title = "Westfold Horse-Breaker",
                Id = "51223bd0-ffd1-11df-a976-0801201c9009",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Attack = 0,
                Defense = 1,
                Willpower = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Rohan." },
                Text = "Action: Discard Westfold Horse-Breaker to choose and ready a hero.",
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1151",
                Title = "Winged Guardian",
                Id = "51223bd0-ffd1-11df-a976-0801201c9010",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Attack = 0,
                Defense = 4,
                Willpower = 0,
                HitPoints = 1,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Text = "Winged Guardian cannot have restricted attachments.Forced: After an attack in which Winged Guardian defends resolves, pay 1 Tactics resource or discard Winged Guardian from play.",
                Keywords = new List<string>() { "Sentinel." },
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1169",
                Title = "False Lead",
                Id = "51223bd0-ffd1-11df-a976-0801201c9011",
                CardType = CardType.Treachery,
                Text = "When Revealed: The first player chooses and shuffles a card with the printed Clue trait back into the encounter deck. If there are no Clue cards in play, False Lead gains surge.",
                EncounterSet = "The Hunt for Gollum",
                Quantity = 2,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1170",
                Title = "Flooding",
                Id = "51223bd0-ffd1-11df-a976-0801201c9012",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Disaster." },
                Text = "When Revealed: Remove all progress tokens from all Riverland locations.",
                Shadow = "Shadow: Resolve the 'when revealed' effect of this card.",
                Keywords = new List<string>() { "Doomed 1.", " Surge." },
                EncounterSet = "The Hunt for Gollum",
                Quantity = 2,
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1167",
                Title = "Goblintown Scavengers",
                Id = "51223bd0-ffd1-11df-a976-0801201c9013",
                CardType = CardType.Enemy,
                EngagementCost = 12,
                Attack = 1,
                Defense = 0,
                HitPoints = 3,
                Traits = new List<string>() { "Goblin.", " Orc." },
                Text = "When Revealed: Discard the top card of each player's deck. Until the end of the phase, increase Goblintown Scavenger's Threat by the total printed cost of all cards discarded in this way.",
                Threat = 1,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 2,
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1168",
                Title = "Hunters from Mordor",
                Id = "51223bd0-ffd1-11df-a976-0801201c9014",
                CardType = CardType.Enemy,
                EngagementCost = 34,
                Attack = 2,
                Defense = 2,
                HitPoints = 6,
                Traits = new List<string>() { "Mordor." },
                Text = "Hunters from Mordor get +2 Attack and +2 Threat for each Clue card in play.",
                Shadow = "Shadow: Deal 1 damage to each hero with a Clue card attached. (3 damage instead if this attack is undefended.)",
                Threat = 2,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 5,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1171",
                Title = "Old Wives' Tales",
                Id = "51223bd0-ffd1-11df-a976-0801201c9015",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Gossip." },
                Text = "When Revealed: Discard 1 resource from each hero's resource pool, if able. Exhaust any hero that could not discard a resource from its pool.",
                EncounterSet = "The Hunt for Gollum",
                Quantity = 3,
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1164",
                Title = "River Ninglor",
                Id = "51223bd0-ffd1-11df-a976-0801201c9016",
                CardType = CardType.Location,
                Traits = new List<string>() { "Riverland." },
                Text = "While River Ninglor is the active location, remove 1 progress token from it and from the current quest at the end of each round.",
                Shadow = "Shadow: Remove 1 progress token from the current quest. (2 progress tokens instead if this attack is undefended.)",
                Threat = 2,
                QuestPoints = 4,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 2,
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1161",
                Title = "Signs of Gollum",
                Id = "51223bd0-ffd1-11df-a976-0801201c9017",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Clue." },
                Text = "Response: After the players quest successfully, the players may claim Signs of Gollum if it has no attached encounters. When claimed, attach Signs of Gollum to any hero committed to the quest. (Counts as a Condition attachment with: 'Forced: After attached hero is damaged or leaves play, return this card to the top of the encounter deck.')",
                Keywords = new List<string>() { "Guarded." },
                EncounterSet = "The Hunt for Gollum",
                Quantity = 4,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1165",
                Title = "The East Bank",
                Id = "51223bd0-ffd1-11df-a976-0801201c9018",
                CardType = CardType.Location,
                Traits = new List<string>() { "Riverland." },
                Text = "While The East Bank is the active location, ally cards cost 1 additional matching resource to play from hand.",
                Shadow = "Shadow: If you do not control at least 1 hero with a Clue card attached, return this enemy to the staging area after its attack resolves.",
                Threat = 3,
                QuestPoints = 3,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 2,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1163",
                Title = "The Eaves of Mirkwood",
                Id = "51223bd0-ffd1-11df-a976-0801201c9019",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Text = "While The Eaves of Mirkwood is the active location, encounter card effects cannot be canceled.",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 3,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1162",
                Title = "The Old Ford",
                Id = "51223bd0-ffd1-11df-a976-0801201c9020",
                CardType = CardType.Location,
                Traits = new List<string>() { "Riverland." },
                Text = " ",
                Shadow = "Shadow: Discard from play all allies with a printed cost lower than the number of Riverland locations in play.",
                Keywords = new List<string>() { "X is the number of ally cards in play." },
                Threat = 0,
                QuestPoints = 2,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 2,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1166",
                Title = "The West Bank",
                Id = "51223bd0-ffd1-11df-a976-0801201c9021",
                CardType = CardType.Location,
                Traits = new List<string>() { "Riverland." },
                Text = "While The West Bank is the active location, attachment and event cards cost 1 additional matching resource to play from hand.",
                Shadow = "Shadow: If you do not control at least 1 hero with a Clue card attached, double this enemy's base Attack for this attack.",
                Threat = 3,
                QuestPoints = 3,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 2,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1158",
                Title = "The Hunt Begins - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801201c9022",
                CardType = CardType.Quest,
                Text = "Setup: Reveal 1 card per player from the encounter deck, and add it to the staging area.",
                EncounterSet = "The Hunt for Gollum",
                Quantity = 1,
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1159",
                Title = "A New Terror Abroad - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801201c9024",
                CardType = CardType.Quest,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 1,
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1160",
                Title = "On the Trail - 3A",
                Id = "51223bd0-ffd1-11df-a976-0801201c9026",
                CardType = CardType.Quest,
                EncounterSet = "The Hunt for Gollum",
                Quantity = 1,
                Number = 24
            });
        }
    }
}
