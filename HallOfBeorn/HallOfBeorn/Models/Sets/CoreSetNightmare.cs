using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class CoreSetNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Core Set - Nightmare";
            Abbreviation = "CoreNM";
            SetType = Models.SetType.Nightmare_Expansion;

            Cards.Add(new Card() {
                Title = "Passage Through Mirkwood",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5001",
                CardType = CardType.Nightmare_Setup,
                Keywords = new List<string>() { "Setup." },
                Text = "The Quest Deck has been modified for Nightmare Mode. Flip this card over and keep it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 1,
                Setup = "ss",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Journey Along the Anduin",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5003",
                CardType = CardType.Nightmare_Setup,
                Keywords = new List<string>() { "Setup." },
                Text = "The Quest Deck has been modified for Nightmare Mode. Flip this card over and keep it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Escape from Dol Guldur",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5005",
                CardType = CardType.Nightmare_Setup,
                Keywords = new List<string>() { "Setup." },
                Text = "The Quest Deck has been modified for Nightmare Mode. Flip this card over and keep it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 1,
                Setup = "ssst",
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Abandoned Camp",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5006",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 2,
                Traits = new List<string>() { "Mirkwood." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Text = "Forced: After Abandoned Camp leaves play, each player must deal 2 damage to each exhausted character he controls.",
                Shadow = "Shadow: If this attack is undefended, discard all attachment cards players control.",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "A Flooded Ford",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5007",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 6,
                Traits = new List<string>() { "Riverland." },
                Text = "Each card revealed by the encounter deck gains doomed X. X is the number of progress tokens on this card.",
                Shadow = "Shadow: If this attack is undefended, put A Flooded Ford into the staging area with 1 progress token on it.",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 2,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Anduin Troll Spawn",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5008",
                CardType = CardType.Enemy,
                EngagementCost = 26,
                Threat = 2,
                Attack = 4,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Troll." },
                Text = "If there is no Hill Troll in play, Anduin Troll Spawn gains surge.Forced: At the beginning of the combat phase, Anduin Troll Spawn engages a player engaged with a Hill Troll.",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 3,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Backtrack!",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5009",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: The topmost enemy or location card in the encounter discard pile is returned to the staging area.",
                Shadow = "Shadow: Deal and resolve the topmost Shadow effect in the encounter discard pile for this attack.",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 2,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Brown Water Rats",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5010",
                CardType = CardType.Enemy,
                EngagementCost = 1,
                Threat = 1,
                Attack = 1,
                Defense = 1,
                HitPoints = 0,
                Traits = new List<string>() { "Creature.", " Rat." },
                Keywords = new List<string>() { "Brown Water Rats cannot be damaged." },
                Text = "Forced: If the players are on stage 3 and all remaining enemies have the printed Rat trait, discard Brown Water Rats from play.",
                Shadow = "Shadow: Attacking enemy cannot be damaged this round.",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Catacomb Inspection",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5011",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Add 1 resource token to each Dol Guldur location in play.",
                Shadow = "Shadow: If there is a Dol Guldur location in the staging area, move 1 resource token from each of your heroes' resource pools to that location. (Resolve this effect for each Dol Guldur location.",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 2,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Crazed Captive",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5012",
                CardType = CardType.Enemy,
                EngagementCost = 13,
                Threat = 2,
                Attack = 1,
                Defense = 3,
                HitPoints = 1,
                Traits = new List<string>() { "Captive." },
                Keywords = new List<string>() { "Doomed 1." },
                Text = "Forced: If Crazed Captive is defeated, raise each player's threat by 7.",
                Shadow = "Shadow: If attacking enemy is not defeated this phase, raise defending player's threat by 7 at the end of the phase.",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 2,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Dark Interrogation",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5013",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must choose and discard cards from hand until he has only 2 cards in hand. (Each player with 2 or fewer cards in hand must instead reveal 1 card from the encounter deck.)",
                Shadow = "Shadow: Discard each card you control that has at least 1 copy of itself in your discard pile. (If this attack is undefended, each player must resolve this effect.)",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 2,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Dungeon Labyrinth",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5014",
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 4,
                Traits = new List<string>() { "Dol Guldur." },
                Text = "Dungeon Labyrinth gets +1 Threat and +1 quest point for each resource token on it.Forced: At the end of each round, place 1 resource token on Dungeon Labyrinth for each player in the game.",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 3,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Forest Flies",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5015",
                CardType = CardType.Enemy,
                EngagementCost = 27,
                Threat = 4,
                Attack = 1,
                Defense = 0,
                HitPoints = 3,
                Traits = new List<string>() { "Creature.", " Insect." },
                Text = "Forced: After you engage Forest Flies, deal 1 damage to each exhausted character you control.",
                Shadow = "Shadow: If attacking enemy is an Insect, deal it 2 additional Shadow cards.",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 3,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Gladden Marshlands",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5016",
                CardType = CardType.Location,
                Threat = 10,
                QuestPoints = 10,
                Traits = new List<string>() { "Marshland." },
                Text = "Action: Deal 1 damage to a hero you control to reduce Gladden Marshlands' Threat by 1 until the end of the phase. Any player may trigger this effect.",
                Shadow = "Shadow: Each enemy engaged with the defending character gets +1 Attack until the end of the phase. (+2 Attack instead if this attack is undefended.)",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 3,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Glade of the Spawn",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5017",
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 4,
                Traits = new List<string>() { "Mirkwood." },
                Text = "When Glade of the Spawn is the active location, it gains: 'Forced: After a Spider enemy enters play, each player must choose and exhaust 1 character he controls.'",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 2,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Marshland Outlaws",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5018",
                CardType = CardType.Enemy,
                EngagementCost = 21,
                Threat = 2,
                Attack = 2,
                Defense = 2,
                HitPoints = 8,
                Traits = new List<string>() { "Marshland." },
                Text = "If you are engaged with this enemy, you cannot attack or deal damage (through effects) to enemies with a title other than Marshland Outlaws.",
                Shadow = "Shadow: Raise your threat by X. X is the amount of damage dealt by this attack.",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 2,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Pursuit on the Shore",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5019",
                CardType = CardType.Treachery,
                Text = "When Revealed: Search the victory display and encounter discard pile for the enemy with the most hit points. Return that enemy to the staging area. If no enemy is returned by this effect, Pursuit on the Shore gains surge. (Cannot be canceled.)",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 2,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Sentinel of Shadow",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5020",
                CardType = CardType.Treachery,
                Text = "When Revealed: The staging area gets +X Threat until the end of the phase, where X is twice the number of players in the game. If the players quest unsuccessfully this phase, put the Nazul of Dol Guldur into play (from any out of play area), engaged with the first player. (Cannot be canceled.)",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 2,
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Smoking Blood",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5021",
                CardType = CardType.Treachery,
                Text = "When Revealed: Remove all damage from all enemies. Then, each player raises his threat by the amount of damage just removed from his engaged enemies. If no damage is removed by this effect, Smoking Blood gains surge.",
                Shadow = "Shadow: Move all damage on this enemy to the defending character. (If undefended, move the damage to the hero damaged by this attack.)",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 2,
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "Spider of Dol Guldur",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5022",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 3,
                Attack = 3,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string>() { "Creature.", " Spider.", " Dol Guldur." },
                Text = "If there are any unclaimed objectives in play, Spider of Dol Guldur gains surge.",
                Shadow = "Shadow: Attacking enemy gets +3 Attack for each unclaimed objective in play.",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 3,
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "Spiders of Mirkwood",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5023",
                CardType = CardType.Enemy,
                EngagementCost = 18,
                Threat = 3,
                Attack = 2,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Spider." },
                Text = "While it is engaged with you, Spiders of Mirkwood gets +1 Attack for each exhausted character you control.",
                Shadow = "Shadow: Choose and exhaust 1 character you control. If this attack was undefended, also deal that character 2 damage.",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 2,
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "The Spider's Web",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5024",
                CardType = CardType.Treachery,
                Text = "When Revealed: The player with the highest threat exhausts all heroes he controls. Then, attach this card to one of that player's heroes. (Counts as a condition attachment with the text, 'Each time attached hero readies, deal it 1 damage.')",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 3,
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "Torture Chamber",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5025",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 6,
                Traits = new List<string>() { "Dol Guldur." },
                Text = "If Torture Chamber has 4 or more resource tokens on it, all 'prisoners' were killed, and the players have lost the game.Forced: At the end of each round, place 1 resource on each Dol Guldur location in play.",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 2,
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "Torture Master",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5026",
                CardType = CardType.Enemy,
                EngagementCost = 45,
                Threat = 5,
                Attack = 1,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Dol Guldur.", " Orc." },
                Text = "When Revealed: Add 1 resource token to each Dol Guldur location in play.",
                Shadow = "Shadow: Any hero damaged (but not defeated) by this attack is turned face down, as a 'prisoner.' Then, if the players are on stage 3, return to stage 2B of the quest.",
                EncounterSet = "Escape from Dol Guldur - Nightmare",
                Quantity = 3,
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "Troll Attack",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5027",
                CardType = CardType.Treachery,
                Text = "When Revealed: All engaged Troll enemies attack. If no Troll enemies are engaged, Troll Attack gains surge.",
                Shadow = "Shadow: If attacking enemy is a Troll, resolve this attack against each player. (Attack is undefended against each player not engaged with this enemy.)",
                EncounterSet = "Journey Along the Anduin - Nightmare",
                Quantity = 2,
                Number = 25
            });
            Cards.Add(new Card() {
                Title = "Ungoliant's Brood",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5028",
                CardType = CardType.Enemy,
                EngagementCost = 31,
                Threat = 2,
                Attack = 3,
                Defense = 2,
                HitPoints = 5,
                Traits = new List<string>() { "Creature.", " Spider." },
                Text = "Forced: After you engage Ungoliant's Brood, your cards cannot ready for the remainder of the round.",
                Shadow = "Shadow: If this attack is undefended, exhaust all characters you control.",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 3,
                Number = 26
            });
            Cards.Add(new Card() {
                Title = "Ungoliant's Spawn",
                Id = "e84f94bf-201b-4adf-95d2-0012e0bb5029",
                CardType = CardType.Enemy,
                EngagementCost = 32,
                Threat = 3,
                Attack = 5,
                Defense = 3,
                HitPoints = 9,
                Traits = new List<string>() { "Creature.", " Spider." },
                Text = "When Revealed: Until the end of the phase, each character currently committed to the quest gets -2 Willpower and is discarded if its Willpower is 0.",
                Shadow = "Shadow: Raise the defending player's theat by 5. (Raise the defending player's threat by 10 instead if undefended.)",
                EncounterSet = "Passage Through Mirkwood - Nightmare",
                Quantity = 1,
                Number = 27
            });
        }
    }
}
