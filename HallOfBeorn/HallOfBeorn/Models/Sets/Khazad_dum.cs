using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class Khazaddum : CardSet
    {
        protected override void Initialize()
        {
            Name = "Khazad-dûm";

            Cards.Add(new Card() {
                Title = "A Foe Beyond",
                Id = "51223bd0-ffd1-11df-a976-0801207c9001",
                CardType = CardType.Treachery,
                EncounterSet = "Flight from Moria",
                Quantity = 4,
                Text = "When Revealed: The last player deals damage equal to The Nameless Fear's attack to a hero he controls. This effect cannot be canceled.",
                Shadow = "Shadow: Deal damage equal to The Nameless Fear's Attack to the defending character.",
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "A Presence in the Dark - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9002",
                CardType = CardType.Quest,
                EncounterSet = "Flight from Moria",
                Quantity = 1,
                Setup = "stttt",
                Text = "Setup: Prepare the quest deck. Add The Nameless Fear to the staging area. Remove all copies of A Foe Beyond from the encounter deck. Then, shuffle 1 copy of A Foe Beyond per player back into the encounter deck.",
                VictoryPoints = 2,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "A Way Up - 3A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9004",
                CardType = CardType.Quest,
                EncounterSet = "Into the Pit",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Abandoned Tools",
                Id = "51223bd0-ffd1-11df-a976-0801207c9006",
                CardType = CardType.Objective,
                EncounterSet = "Flight from Moria",
                Traits = new List<string>() { "Tools." },
                Quantity = 1,
                Text = "Action: Exhaust a hero to claim this objective if it has no encounters attached. Then, attach Abandoned Tools to that hero. (If detached, return Abandoned Tools to the staging area.)",
                Keywords = new List<string>() { "Guarded.", " Restricted." },
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Ancestral Knowledge",
                Id = "51223bd0-ffd1-11df-a976-0801207c9007",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Exhaust a Dwarf character to place 2 progress tokens on the active location. (4 progress tokens instead if it is an Underground or Mountain location.)",
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Bifur",
                Id = "51223bd0-ffd1-11df-a976-0801207c9008",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 1,
                ThreatCost = 7,
                IsUnique = true,
                Attack = 1,
                Defense = 2,
                Willpower = 2,
                HitPoints = 3,
                Text = "Action: Pay 1 resource from a hero's resource pool to add 1 resource to Bifur's resource pool. Any player may trigger this ability. (Limit once per round.)",
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Bitter Wind",
                Id = "51223bd0-ffd1-11df-a976-0801207c9009",
                CardType = CardType.Treachery,
                EncounterSet = "Misty Mountains",
                Quantity = 3,
                Text = "When Revealed: The first player must discard 3 resources from each hero he controls.",
                Shadow = "Shadow: Defending player must discard 2 resources from each hero he controls.",
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Black Uruks",
                Id = "51223bd0-ffd1-11df-a976-0801207c9010",
                CardType = CardType.Enemy,
                EncounterSet = "Misty Mountains",
                Traits = new List<string>() { "Uruk.", " Orc." },
                Quantity = 4,
                EngagementCost = 32,
                Attack = 3,
                Defense = 3,
                HitPoints = 2,
                Text = "When Revealed: The first player must choose and discard an attachment from a questing character, if able.",
                Shadow = "Shadow: If this attack is undefended, deal 2 additional shadow cards to attacking enemy.",
                Threat = 2,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Book of Mazarbul",
                Id = "51223bd0-ffd1-11df-a976-0801207c9011",
                CardType = CardType.Objective,
                EncounterSet = "The Seventh Level",
                Traits = new List<string>() { "Item.", " Artifact." },
                Quantity = 1,
                IsUnique = true,
                Text = "Action: Exhaust a hero to claim this objective. Then, attach Book of Mazarbul to that hero. (If detached, return Book of Mazarbul to the staging area.)Attached hero cannot attack and does not exhaust to commit to a quest.",
                Keywords = new List<string>() { "Restricted." },
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Boots from Erebor",
                Id = "51223bd0-ffd1-11df-a976-0801207c9012",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Item." },
                Quantity = 3,
                ResourceCost = 0,
                Text = "Limit 1 Boots from Erebor per character.Attached character gets +1 hit point.",
                Keywords = new List<string>() { "Attach to a Dwarf or Hobbit character." },
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Branching Paths",
                Id = "51223bd0-ffd1-11df-a976-0801207c9013",
                CardType = CardType.Location,
                EncounterSet = "Twists and Turns",
                Traits = new List<string>() { "Underground.", " Dark." },
                Quantity = 3,
                Text = "While Branching Paths is in the staging area, each Dark location gets +1 Threat.Forced: After Branching Paths leaves play as an explored location, look at the top 3 cards of the encounter deck. Players must choose 1 of those to reveal and add to the staging area, moving the other 2 to the bottom of the deck.",
                Threat = 1,
                QuestPoints = 3,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Bridge of Khazad-dûm",
                Id = "51223bd0-ffd1-11df-a976-0801207c9014",
                CardType = CardType.Location,
                EncounterSet = "Into the Pit",
                Traits = new List<string>() { "Underground.", " Bridge." },
                Quantity = 1,
                IsUnique = true,
                Text = "While Bridge of Khazad-dûm is the active location, players cannot play cards.",
                Threat = 3,
                QuestPoints = 3,
                VictoryPoints = 2,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Burning Low",
                Id = "51223bd0-ffd1-11df-a976-0801207c9015",
                CardType = CardType.Treachery,
                EncounterSet = "Twists and Turns",
                Quantity = 3,
                Text = "When Revealed: Each enemy and location currently in the staging area gets +1 Threat until the end of the phase. (+3 Threat instead if it is a Dark location.) Players may exhaust a Cave Torch to cancel this effect.",
                Shadow = "Shadow: attacking enemy gets +2 Attack.",
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Cave In",
                Id = "51223bd0-ffd1-11df-a976-0801207c9016",
                CardType = CardType.Treachery,
                EncounterSet = "Hazards of the Pit",
                Traits = new List<string>() { "Hazard." },
                Quantity = 3,
                Text = "When Revealed: Remove all progress tokens from the current quest card and active location. If Cave In removed no progress tokens, it gains surge.",
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Cave Torch",
                Id = "51223bd0-ffd1-11df-a976-0801207c9017",
                CardType = CardType.Objective,
                EncounterSet = "Twists and Turns",
                Traits = new List<string>() { "Light." },
                Quantity = 1,
                Text = "Action: Exhaust Cave Torch to place up to 3 progress tokens on a Dark location.Forced: After Cave Torch exhausts, discard the top card of the encounter deck. If that card is an enemy, add it to the staging area.",
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Cave-troll",
                Id = "51223bd0-ffd1-11df-a976-0801207c9018",
                CardType = CardType.Enemy,
                EncounterSet = "The Seventh Level",
                Traits = new List<string>() { "Troll." },
                Quantity = 2,
                EngagementCost = 33,
                Attack = 6,
                Defense = 4,
                HitPoints = 7,
                Text = "For each excess point of combat damage dealt by Cave-troll (damage that is dealt beyond the remaining hit points of the character damaged by its attack) you must damage another character you control.",
                Threat = 4,
                VictoryPoints = 2,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Chance Encounter",
                Id = "51223bd0-ffd1-11df-a976-0801207c9019",
                CardType = CardType.Treachery,
                EncounterSet = "Deeps of Moria",
                Quantity = 3,
                Text = "When Revealed: Put the top enemy in the encounter discard pile into play, engaged with the first player. If this effect put no enemies into play, Chance Encounter gains surge.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+3 Attack instead if engaged with the first player.)",
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Chieftain of the Pit",
                Id = "51223bd0-ffd1-11df-a976-0801207c9020",
                CardType = CardType.Enemy,
                EncounterSet = "Plundering Goblins",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 1,
                EngagementCost = 27,
                Attack = 5,
                Defense = 2,
                HitPoints = 4,
                Text = "When Revealed: Chieftain of the Pit gets +3 Attack until the end of the round.",
                Shadow = "Shadow: attacking enemy attacks again after this attack. Deal it another shadow card for the next attack.",
                Threat = 2,
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Crumbling Ruin",
                Id = "51223bd0-ffd1-11df-a976-0801207c9021",
                CardType = CardType.Treachery,
                EncounterSet = "Hazards of the Pit",
                Traits = new List<string>() { "Hazard." },
                Quantity = 2,
                Text = "When Revealed: Each player must exhaust a character and discard the top card of his deck, if able. If the printed cost of the discarded card is equal to or higher than the remaining hit points of the exhausted character, discard the exhausted character.",
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "Dark and Dreadful",
                Id = "51223bd0-ffd1-11df-a976-0801207c9022",
                CardType = CardType.Treachery,
                EncounterSet = "Hazards of the Pit",
                Quantity = 2,
                Text = "When Revealed: Deal 1 damage to each exhausted character. (2 damage instead if the active location is a Dark location.)",
                Shadow = "Shadow: Deal 1 damage to the defending character. (Attacking enemy gets +2 Attack instead if this attack is undefended.)",
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "Dreadful Gap",
                Id = "51223bd0-ffd1-11df-a976-0801207c9023",
                CardType = CardType.Location,
                EncounterSet = "Hazards of the Pit",
                Traits = new List<string>() { "Underground.", " Hazard." },
                Quantity = 1,
                Text = "When Revealed: Immediately travel to Dreadful Gap. If another location is currently active, return it to the staging area.X is the number of characters in play.",
                Threat = 2,
                QuestPoints = 0,
                VictoryPoints = 3,
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "Durin's Song",
                Id = "51223bd0-ffd1-11df-a976-0801207c9024",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Song." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Choose a Dwarf hero. That hero gets +2 Willpower, +2 Attack, and +2 Defense until the end of the round.",
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "Dwalin",
                Id = "51223bd0-ffd1-11df-a976-0801207c9025",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 1,
                ThreatCost = 9,
                IsUnique = true,
                Attack = 2,
                Defense = 2,
                Willpower = 1,
                HitPoints = 4,
                Text = "Response: After Dwalin attacks and destroys an Orc enemy, lower your threat by 2.",
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "Dwarrowdelf Axe",
                Id = "51223bd0-ffd1-11df-a976-0801207c9026",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Item.", " Weapon." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attached character gets +1 Attack.Response: After attached character attacks, deal 1 damage to the defending enemy.",
                Keywords = new List<string>() { "Attach to a Dwarf character.", " Restricted." },
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "East-gate",
                Id = "51223bd0-ffd1-11df-a976-0801207c9027",
                CardType = CardType.Location,
                EncounterSet = "Into the Pit",
                Traits = new List<string>() { "Gate." },
                Quantity = 1,
                IsUnique = true,
                Text = "Players cannot optionally engage enemies and no engagement checks are made.Forced: After East-gate leaves play as an explored location, add First Hall to the staging area.",
                Keywords = new List<string>() { "Immune to card effects." },
                Threat = 7,
                QuestPoints = 7,
                VictoryPoints = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                Title = "Entering the Mines - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9028",
                CardType = CardType.Quest,
                EncounterSet = "Into the Pit",
                Quantity = 1,
                Setup = "lttt",
                Text = "Setup: Search the encounter deck for East-gate and Cave Torch. Put East-gate into play as the active location, and have the first player attach Cave Torch to a hero of his choice. Set First Hall and Bridge of Khazad-dum aside, out of play. Shuffle the encounter deck.",
                Number = 26
            });
            Cards.Add(new Card() {
                Title = "Erebor Record Keeper",
                Id = "51223bd0-ffd1-11df-a976-0801207c9030",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 1,
                Attack = 0,
                Defense = 0,
                Willpower = 1,
                HitPoints = 1,
                Text = "Erebor Record Keeper cannot attack or defend.Action: Exhaust Erebor Record Keeper and pay 1 Lore resource to choose and ready a Dwarf character.",
                Number = 27
            });
            Cards.Add(new Card() {
                Title = "Ever Onward",
                Id = "51223bd0-ffd1-11df-a976-0801207c9031",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Quantity = 3,
                ResourceCost = 3,
                Text = "Response: After players quest unsuccessfully, choose a player. That player does not raise his threat.",
                Number = 28
            });
            Cards.Add(new Card() {
                Title = "First Hall",
                Id = "51223bd0-ffd1-11df-a976-0801207c9032",
                CardType = CardType.Location,
                EncounterSet = "Into the Pit",
                Traits = new List<string>() { "Underground." },
                Quantity = 1,
                IsUnique = true,
                Text = "Travel: Each player must raise his threat by 3 to travel here.Forced: After First Hall leaves play as an explored location, add Bridge of Khazad-dum to the staging area.",
                Threat = 2,
                QuestPoints = 2,
                VictoryPoints = 1,
                Number = 29
            });
            Cards.Add(new Card() {
                Title = "Fouled Well",
                Id = "51223bd0-ffd1-11df-a976-0801207c9033",
                CardType = CardType.Location,
                EncounterSet = "Hazards of the Pit",
                Traits = new List<string>() { "Underground.", " Dark.", " Hazard." },
                Quantity = 3,
                Text = "When Revealed: Each player may choose and discard 1 card at random from his hand. If all players did not discard 1 card, Fouled Well gains surge.",
                Threat = 3,
                QuestPoints = 5,
                Number = 30
            });
            Cards.Add(new Card() {
                Title = "Goblin Archer",
                Id = "51223bd0-ffd1-11df-a976-0801207c9034",
                CardType = CardType.Enemy,
                EncounterSet = "Plundering Goblins",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 2,
                EngagementCost = 48,
                Attack = 1,
                Defense = 3,
                HitPoints = 1,
                Text = "Characters with ranged are eligible to attack Goblin Archer while it is in the staging area.Forced: After an enemy is revealed from the encounter deck, the first player must deal 1 damage to 1 character he controls.",
                Keywords = new List<string>() { "Players cannot optionally engage Goblin Archer." },
                Threat = 2,
                Number = 31
            });
            Cards.Add(new Card() {
                Title = "Goblin Follower",
                Id = "51223bd0-ffd1-11df-a976-0801207c9035",
                CardType = CardType.Enemy,
                EncounterSet = "Twists and Turns",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 4,
                EngagementCost = 33,
                Attack = 3,
                Defense = 2,
                HitPoints = 4,
                Text = "When Revealed: Goblin Follower engages the last player.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+2 Attack instead if attacking the last player.)",
                Threat = 1,
                Number = 32
            });
            Cards.Add(new Card() {
                Title = "Goblin Patrol - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9036",
                CardType = CardType.Quest,
                EncounterSet = "Into the Pit",
                Quantity = 1,
                Number = 33
            });
            Cards.Add(new Card() {
                Title = "Goblin Scout",
                Id = "51223bd0-ffd1-11df-a976-0801207c9038",
                CardType = CardType.Enemy,
                EncounterSet = "Goblins of the Deep",
                Traits = new List<string>() { "Goblin.", " Orc.", " Scout." },
                Quantity = 3,
                EngagementCost = 37,
                Attack = 1,
                Defense = 0,
                HitPoints = 2,
                Text = "Each player with a threat of 25 or higher cannot optionally engage Goblin Scout.",
                Threat = 3,
                Number = 34
            });
            Cards.Add(new Card() {
                Title = "Goblin Spearman",
                Id = "51223bd0-ffd1-11df-a976-0801207c9039",
                CardType = CardType.Enemy,
                EncounterSet = "Plundering Goblins",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 5,
                EngagementCost = 15,
                Attack = 2,
                Defense = 2,
                HitPoints = 2,
                Text = "Goblin Spearman gets +2 Attack if its attack is undefended.",
                Shadow = "Shadow: Add Goblin Spearman to the staging area.",
                Threat = 2,
                Number = 35
            });
            Cards.Add(new Card() {
                Title = "Goblin Swordsman",
                Id = "51223bd0-ffd1-11df-a976-0801207c9040",
                CardType = CardType.Enemy,
                EncounterSet = "Goblins of the Deep",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 5,
                EngagementCost = 20,
                Attack = 3,
                Defense = 1,
                HitPoints = 2,
                Text = "Goblin Swordsman gets +2 Attack if its attack is undefended.",
                Shadow = "Shadow: Add Goblin Swordsman to the staging area.",
                Threat = 1,
                Number = 36
            });
            Cards.Add(new Card() {
                Title = "Goblin Tunnels",
                Id = "51223bd0-ffd1-11df-a976-0801207c9041",
                CardType = CardType.Location,
                EncounterSet = "Goblins of the Deep",
                Traits = new List<string>() { "Underground.", " Dark." },
                Quantity = 2,
                Text = "While Goblin Tunnels is in the staging area, it gains: 'Forced: After a Goblin is revealed from the encounter deck, remove a progress token from the current quest card.'",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+3 Attack instead if attacking enemy is a Goblin.)",
                Threat = 2,
                QuestPoints = 7,
                Number = 37
            });
            Cards.Add(new Card() {
                Title = "Great Cave-troll",
                Id = "51223bd0-ffd1-11df-a976-0801207c9042",
                CardType = CardType.Enemy,
                EncounterSet = "Deeps of Moria",
                Traits = new List<string>() { "Troll." },
                Quantity = 2,
                EngagementCost = 38,
                Attack = 7,
                Defense = 3,
                HitPoints = 10,
                Text = "No attachments can be played on Great Cave-troll.",
                Keywords = new List<string>() { "Immune to ranged damage." },
                Threat = 2,
                VictoryPoints = 3,
                Number = 38
            });
            Cards.Add(new Card() {
                Title = "Hidden Threat",
                Id = "51223bd0-ffd1-11df-a976-0801207c9043",
                CardType = CardType.Treachery,
                EncounterSet = "The Seventh Level",
                Quantity = 2,
                Text = "When Revealed: Each player must raise his threat by 1 for each enemy in the staging area. Then, the last player discards an attachment he controls.",
                Number = 39
            });
            Cards.Add(new Card() {
                Title = "Khazad! Khazad!",
                Id = "51223bd0-ffd1-11df-a976-0801207c9044",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Choose a Dwarf character. Until the end of the phase, that character gets +3 Attack.",
                Number = 40
            });
            Cards.Add(new Card() {
                Title = "Knees of the Mountain",
                Id = "51223bd0-ffd1-11df-a976-0801207c9045",
                CardType = CardType.Location,
                EncounterSet = "Misty Mountains",
                Traits = new List<string>() { "Mountain." },
                Quantity = 1,
                Text = "While Knees of the Mountain is in the staging area, it gains: 'Forced: After an enemy engages a player, it gets +1 Attack until the end of the round.'",
                Threat = 2,
                QuestPoints = 3,
                Number = 41
            });
            Cards.Add(new Card() {
                Title = "Lightless Passage",
                Id = "51223bd0-ffd1-11df-a976-0801207c9046",
                CardType = CardType.Location,
                EncounterSet = "Twists and Turns",
                Traits = new List<string>() { "Underground.", " Dark." },
                Quantity = 2,
                Text = "Travel: Players must exhaust a Cave Torch to travel here.",
                Shadow = "Shadow: Cancel all combat damage dealt to attacking enemy.",
                Threat = 4,
                QuestPoints = 4,
                Number = 42
            });
            Cards.Add(new Card() {
                Title = "Many Roads",
                Id = "51223bd0-ffd1-11df-a976-0801207c9047",
                CardType = CardType.Treachery,
                EncounterSet = "Twists and Turns",
                Quantity = 1,
                Text = "When Revealed: Shuffle all locations in the encounter discard pile back into the encounter deck.",
                Keywords = new List<string>() { "Surge." },
                Number = 43
            });
            Cards.Add(new Card() {
                Title = "Massing in the Deep",
                Id = "51223bd0-ffd1-11df-a976-0801207c9048",
                CardType = CardType.Treachery,
                EncounterSet = "Deeps of Moria",
                Quantity = 2,
                Text = "When Revealed: Reveal X additional cards from the encounter deck and add them to the staging area. X is the number of players in the game.",
                Shadow = "Shadow: attacking enemy gets +X Attack. X is the number of players in the game.",
                Keywords = new List<string>() { "Doomed 1." },
                Number = 44
            });
            Cards.Add(new Card() {
                Title = "Mountain Warg",
                Id = "51223bd0-ffd1-11df-a976-0801207c9049",
                CardType = CardType.Enemy,
                EncounterSet = "Misty Mountains",
                Traits = new List<string>() { "Creature." },
                Quantity = 3,
                EngagementCost = 30,
                Attack = 4,
                Defense = 2,
                HitPoints = 4,
                Text = "If Mountain Warg is dealt a shadow card with no effect, return Mountain Warg to the staging area after it attacks.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+2 Attack instead if a Mountain is the active location.)",
                Threat = 2,
                Number = 45
            });
            Cards.Add(new Card() {
                Title = "Narvi's Belt",
                Id = "51223bd0-ffd1-11df-a976-0801207c9050",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Item." },
                Quantity = 3,
                ResourceCost = 2,
                IsUnique = true,
                Text = "Action: Exhaust Narvi's Belt to give attached hero a Leadership, Lore, Tactics, or Spirit icon until the end of the phase.",
                Keywords = new List<string>() { "Attach to a Dwarf hero." },
                Number = 46
            });
            Cards.Add(new Card() {
                Title = "New Devilry",
                Id = "51223bd0-ffd1-11df-a976-0801207c9051",
                CardType = CardType.Treachery,
                EncounterSet = "Flight from Moria",
                Quantity = 3,
                Text = "When Revealed: If the players are not on stage 1, shuffle the current quest card into the quest deck, then reveal a new quest card. Otherwise, New Devilry gains surge.",
                Shadow = "Shadow: If this attack is undefended, raise your threat by The Nameless Fear's Threat.",
                Number = 47
            });
            Cards.Add(new Card() {
                Title = "Orc Drummer",
                Id = "51223bd0-ffd1-11df-a976-0801207c9052",
                CardType = CardType.Enemy,
                EncounterSet = "Deeps of Moria",
                Traits = new List<string>() { "Orc.", " Summoner." },
                Quantity = 1,
                EngagementCost = 50,
                Attack = 1,
                Defense = 3,
                HitPoints = 1,
                Text = "While Orc Drummer is in the staging area, each enemy gets +X Threat. X is the number of players in the game.",
                Threat = 1,
                Number = 48
            });
            Cards.Add(new Card() {
                Title = "Orc Horn Blower",
                Id = "51223bd0-ffd1-11df-a976-0801207c9053",
                CardType = CardType.Enemy,
                EncounterSet = "The Seventh Level",
                Traits = new List<string>() { "Orc.", " Summoner." },
                Quantity = 1,
                EngagementCost = 45,
                Attack = 1,
                Defense = 1,
                HitPoints = 3,
                Text = "When Revealed: Reveal 1 card from the encounter deck and add it to the staging area.",
                Keywords = new List<string>() { "Surge." },
                Threat = 2,
                Number = 49
            });
            Cards.Add(new Card() {
                Title = "Patrol Leader",
                Id = "51223bd0-ffd1-11df-a976-0801207c9054",
                CardType = CardType.Enemy,
                EncounterSet = "Into the Pit",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 2,
                EngagementCost = 30,
                Attack = 4,
                Defense = 3,
                HitPoints = 4,
                Text = "Forced: Before Patrol Leader is dealt damage, discard the top card of the encounter deck. If the discarded card is an enemy, cancel that damage.",
                Shadow = "Shadow: Cancel all damage dealt to this enemy.",
                Threat = 3,
                Number = 50
            });
            Cards.Add(new Card() {
                Title = "Plundered Armoury",
                Id = "51223bd0-ffd1-11df-a976-0801207c9055",
                CardType = CardType.Location,
                EncounterSet = "Plundering Goblins",
                Traits = new List<string>() { "Underground." },
                Quantity = 2,
                Text = "While Plundered Armoury is in the staging area, enemies get +1 Attack.Response: After Plundered Armoury leaves play as an explored location, each player may attach a Weapon or Armour attachment from his hand to 1 character he controls.",
                Threat = 3,
                QuestPoints = 2,
                Number = 51
            });
            Cards.Add(new Card() {
                Title = "Search for an Exit - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9056",
                CardType = CardType.Quest,
                EncounterSet = "Flight from Moria",
                Quantity = 1,
                Text = "While Search for an Exit is the active quest card, only flip it to side 2B at the beginning of the staging step.",
                VictoryPoints = 2,
                Number = 52
            });
            Cards.Add(new Card() {
                Title = "Search for an Exit - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9058",
                CardType = CardType.Quest,
                EncounterSet = "Flight from Moria",
                Quantity = 1,
                Text = "While Search for an Exit is the active quest card, only flip it to side 2B at the beginning of the staging step.",
                Number = 53
            });
            Cards.Add(new Card() {
                Title = "Search for an Exit - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9060",
                CardType = CardType.Quest,
                EncounterSet = "Flight from Moria",
                Quantity = 1,
                Text = "While Search for an Exit is the active quest card, only flip it to side 2B at the beginning of the staging step.",
                Number = 54
            });
            Cards.Add(new Card() {
                Title = "Search for an Exit - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9062",
                CardType = CardType.Quest,
                EncounterSet = "Flight from Moria",
                Quantity = 1,
                Text = "While Search for an Exit is the active quest card, only flip it to side 2B at the beginning of the staging step.",
                VictoryPoints = 2,
                Number = 55
            });
            Cards.Add(new Card() {
                Title = "Search for an Exit - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9064",
                CardType = CardType.Quest,
                EncounterSet = "Flight from Moria",
                Quantity = 1,
                Text = "While Search for an Exit is the active quest card, only flip it to side 2B at the beginning of the staging step.",
                VictoryPoints = 1,
                Number = 56
            });
            Cards.Add(new Card() {
                Title = "Search for an Exit - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9066",
                CardType = CardType.Quest,
                EncounterSet = "Flight from Moria",
                Quantity = 1,
                Text = "While Search for an Exit is the active quest card, only flip it to side 2B at the beginning of the staging step.",
                VictoryPoints = 1,
                Number = 57
            });
            Cards.Add(new Card() {
                Title = "Search for an Exit - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9068",
                CardType = CardType.Quest,
                EncounterSet = "Flight from Moria",
                Quantity = 1,
                Text = "While Search for an Exit is the active quest card, only flip it to side 2B at the beginning of the staging step.",
                VictoryPoints = 1,
                Number = 58
            });
            Cards.Add(new Card() {
                Title = "Search for the Chamber - 1A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9070",
                CardType = CardType.Quest,
                EncounterSet = "The Seventh Level",
                Quantity = 1,
                Setup = "t",
                Text = "Setup: Search the encounter deck for Book of Mazarbul, and have the first player attach it to a hero of his choice. Shuffle the encounter deck.",
                Number = 59
            });
            Cards.Add(new Card() {
                Title = "Shadow of Fear",
                Id = "51223bd0-ffd1-11df-a976-0801207c9072",
                CardType = CardType.Treachery,
                EncounterSet = "Flight from Moria",
                Quantity = 3,
                Text = "When Revealed: The first player attaches Shadow of Fear to one of his heroes. (Counts as a Condition attachment with the text: 'Limit 1 per hero. Attached hero cannot exhaust or ready and its text box is treated as if it were blank. Action: Pay 3 resources from attached hero's pool to discard this card.')",
                Number = 60
            });
            Cards.Add(new Card() {
                Title = "Signs of Conflict",
                Id = "51223bd0-ffd1-11df-a976-0801207c9073",
                CardType = CardType.Treachery,
                EncounterSet = "Into the Pit",
                Quantity = 5,
                Text = " ",
                Shadow = "Shadow: Defending player raises his threat by 2.",
                Keywords = new List<string>() { "Doomed 2.", " Surge." },
                Number = 61
            });
            Cards.Add(new Card() {
                Title = "Stairs of Nain",
                Id = "51223bd0-ffd1-11df-a976-0801207c9074",
                CardType = CardType.Location,
                EncounterSet = "Into the Pit",
                Traits = new List<string>() { "Underground." },
                Quantity = 2,
                Text = "Travel: The first player must exhaust 1 character he controls to travel here.",
                Shadow = "Shadow: Defending player must choose and exhaust 1 character he controls.",
                Threat = 2,
                QuestPoints = 4,
                Number = 62
            });
            Cards.Add(new Card() {
                Title = "Stray Goblin",
                Id = "51223bd0-ffd1-11df-a976-0801207c9075",
                CardType = CardType.Enemy,
                EncounterSet = "Deeps of Moria",
                Traits = new List<string>() { "Goblin.", " Orc." },
                Quantity = 3,
                EngagementCost = 29,
                Attack = 0,
                Defense = 2,
                HitPoints = 2,
                Text = "X is the number of players in the game.",
                Shadow = "Shadow: attacking enemy gets +X Attack. X is the number of players in the game.",
                Threat = 0,
                Number = 63
            });
            Cards.Add(new Card() {
                Title = "Sudden Pitfall",
                Id = "51223bd0-ffd1-11df-a976-0801207c9076",
                CardType = CardType.Treachery,
                EncounterSet = "Hazards of the Pit",
                Traits = new List<string>() { "Hazard." },
                Quantity = 1,
                Text = "When Revealed: The first player must discard 1 questing character he controls, if able. This effect cannot be canceled.",
                Shadow = "Shadow: Discard the defending character from play.",
                Number = 64
            });
            Cards.Add(new Card() {
                Title = "The Fate of Balin - 2A",
                Id = "51223bd0-ffd1-11df-a976-0801207c9077",
                CardType = CardType.Quest,
                EncounterSet = "The Seventh Level",
                Quantity = 1,
                Number = 65
            });
            Cards.Add(new Card() {
                Title = "The Mountains' Roots",
                Id = "51223bd0-ffd1-11df-a976-0801207c9079",
                CardType = CardType.Location,
                EncounterSet = "Deeps of Moria",
                Traits = new List<string>() { "Underground." },
                Quantity = 3,
                Text = "X is the number of players in the game.",
                Shadow = "Shadow: attacking enemy gets +X Attack. X is the number of players in the game.",
                Threat = 0,
                QuestPoints = 0,
                Number = 66
            });
            Cards.Add(new Card() {
                Title = "The Nameless Fear",
                Id = "51223bd0-ffd1-11df-a976-0801207c9080",
                CardType = CardType.Enemy,
                EncounterSet = "Flight from Moria",
                Traits = new List<string>() { "Flame.", " Shadow." },
                Quantity = 1,
                EngagementCost = 50,
                IsUnique = true,
                Attack = 0,
                Defense = 0,
                HitPoints = 27,
                Text = "The Nameless Fear cannot engage or be engaged.X is the number of victory points in the victory display.",
                Keywords = new List<string>() { "Immune to player card effects." },
                Threat = 0,
                Number = 67
            });
            Cards.Add(new Card() {
                Title = "Turbulent Waters",
                Id = "51223bd0-ffd1-11df-a976-0801207c9081",
                CardType = CardType.Location,
                EncounterSet = "Misty Mountains",
                Traits = new List<string>() { "Mountain." },
                Quantity = 2,
                Text = "While Turbulent Waters is the active location, players cannot optionally engage enemies.",
                Threat = 3,
                QuestPoints = 2,
                Number = 68
            });
            Cards.Add(new Card() {
                Title = "Undisturbed Bones",
                Id = "51223bd0-ffd1-11df-a976-0801207c9082",
                CardType = CardType.Treachery,
                EncounterSet = "Plundering Goblins",
                Quantity = 3,
                Text = "When Revealed: Each player must deal X damage to 1 ally he controls. X is the number of allies he controls.",
                Shadow = "Shadow: If the defending character is an ally, discard it from play.",
                Number = 69
            });
            Cards.Add(new Card() {
                Title = "Untroubled by Darkness",
                Id = "51223bd0-ffd1-11df-a976-0801207c9083",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Quantity = 3,
                ResourceCost = 2,
                Text = "Action: Each Dwarf character gets +1 Willpower until the end of the phase. (+2 Willpower instead if the active location is an Underground or Dark location.)",
                Number = 70
            });
            Cards.Add(new Card() {
                Title = "Upper Hall",
                Id = "51223bd0-ffd1-11df-a976-0801207c9084",
                CardType = CardType.Location,
                EncounterSet = "The Seventh Level",
                Traits = new List<string>() { "Underground." },
                Quantity = 3,
                Keywords = new List<string>() { "Doomed 2." },
                Threat = 3,
                QuestPoints = 4,
                Number = 71
            });
            Cards.Add(new Card() {
                Title = "Veteran of Nanduhirion",
                Id = "51223bd0-ffd1-11df-a976-0801207c9085",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Dwarf.", " Warrior." },
                Quantity = 3,
                ResourceCost = 4,
                Attack = 3,
                Defense = 2,
                Willpower = 0,
                HitPoints = 3,
                Text = "Veteran of Nanduhirion enters play with 1 damage on it.",
                Number = 72
            });
            Cards.Add(new Card() {
                Title = "Warg Lair",
                Id = "51223bd0-ffd1-11df-a976-0801207c9086",
                CardType = CardType.Location,
                EncounterSet = "Misty Mountains",
                Traits = new List<string>() { "Mountain." },
                Quantity = 2,
                Text = "When Revealed: Search the encounter deck and discard pile for 1 copy of Mountain Warg and add it to the staging area, if able. Shuffle the encounter deck.Response: After Warg Lair leaves play as an explored location, each player draws 1 card.",
                Threat = 1,
                QuestPoints = 3,
                Number = 73
            });
            Cards.Add(new Card() {
                Title = "Watchful Eyes",
                Id = "51223bd0-ffd1-11df-a976-0801207c9087",
                CardType = CardType.Treachery,
                EncounterSet = "Goblins of the Deep",
                Quantity = 3,
                Text = "When Revealed: The first player attaches Watchful Eyes to one of his heroes. (Counts as a Condition attachment with the text: 'Limit 1 per hero. Forced: If attached hero is exhausted at the end of the combat phase, reveal 1 card from the encounter deck and add it to the staging area.')",
                Number = 74
            });
            Cards.Add(new Card() {
                Title = "Zigil Miner",
                Id = "51223bd0-ffd1-11df-a976-0801207c9088",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 2,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 1,
                Text = "Action: Exhaust Zigil Miner and name a number to discard the top 2 cards of your deck. If at least one of those cards has cost equal to the named number, choose a hero you control. For each card that matches the named number, add 1 resource to that hero's resource pool.",
                Number = 75
            });
            Cards.Add(new Card() {
                Title = "Zigil Mineshaft",
                Id = "51223bd0-ffd1-11df-a976-0801207c9089",
                CardType = CardType.Location,
                EncounterSet = "Twists and Turns",
                Traits = new List<string>() { "Underground.", " Dark." },
                Quantity = 3,
                Text = "Action: Raise each player's threat by 1 to place 1 progress token on Zigil Mineshaft.",
                Threat = 5,
                QuestPoints = 5,
                Number = 76
            });
        }
    }
}
