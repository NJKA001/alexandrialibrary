using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheRoadDarkens : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "Gandalf",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898001",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Neutral,
                ThreatCost = 14,
                Willpower = 3,
                Attack = 3,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Istari." },
                Text = "Turn the top card of your deck faceup. Once per phase, you may play the top card of your deck as if it were in your hand. When playing a card this way, you may spend resources from Gandalf’s resource pool to pay for it.",
                EncounterSet = "The Road Darkens",
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Frodo Baggins",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898002",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Fellowship,
                ThreatCost = 0,
                Willpower = 2,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit.", " Ring-bearer." },
                Text = "Response: After Frodo Baggins exhausts to defend an attack, exhaust The One Ring and spend 1 Fellowship resource to cancel all damage from this attack. Then, raise your threat by 2.",
                EncounterSet = "The Road Darkens",
                Quantity = 1,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "The One Ring",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898003",
                IsUnique = true,
                CardType = CardType.Objective,
                Traits = new List<string>() { "Artifact.", " Item.", " Ring." },
                Text = "Setup: The first player claims The One Ring and attaches it to the Ring-bearer.Attached hero does not count against the hero limit. The first player gains control of attached hero.If The One Ring leaves play, the players lose the game.",
                EncounterSet = "The Road Darkens",
                Quantity = 1,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Anduril",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898004",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Artifact.", " Item.", " Weapon." },
                Keywords = new List<string>() { "Attach to a Noble hero.", " Restricted." },
                Text = "Attached hero gets +1 Willpower, +1 Attack, and +1 Defense.Response: After attached hero is declared as an attacker, exhaust Anduril to discard a card from your hand. Attached hero gets +X Attack for this attack where X is the discarded card’s cost.",
                EncounterSet = "The Road Darkens",
                Quantity = 1,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Mithril Shirt",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898005",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Artifact.", " Item." },
                Keywords = new List<string>() { "Attach to the Ring-bearer." },
                Text = "Attached hero gets +1 Defense and +1 hit point.",
                EncounterSet = "The Road Darkens",
                Quantity = 1,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Glamdring",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898006",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Artifact.", " Item.", " Weapon." },
                Keywords = new List<string>() { "Attach to a hero or Gandalf.", " Restricted." },
                Text = "Attached hero gets +2 Attack.Response: After attached character destroys an enemy, draw a card.",
                EncounterSet = "The Road Darkens",
                Quantity = 1,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Sting",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898007",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Item.", " Artifact.", " Weapon." },
                Keywords = new List<string>() { "Attach to a Hobbit hero.", " Restricted." },
                Text = "Attached hero gets +1 Willpower, +1 Attack, and +1 Defense. Response: After attached hero exhausts to defend an attack, discard the top card of the encounter deck. Deal damage to the attacking enemy equal to the discarded card’s Threat.",
                EncounterSet = "The Road Darkens",
                Quantity = 1,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Galadriel",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898008",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 4,
                Willpower = 3,
                Attack = 0,
                Defense = 0,
                HitPoints = 3,
                Traits = new List<string>() { "Noldor.", " Noble." },
                Text = "At the end of the round, discard Galadriel from play.Response: After you play Galadriel from your hand, search the top 10 cards of your deck for an Item attachment and attach it to a hero.",
                EncounterSet = "The Road Darkens",
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Boromir",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898009",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 4,
                Willpower = 1,
                Attack = 3,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Gondor.", " Warrior." },
                Text = "Boromir gets +2 Defense against enemies with engagement cost higher than your threat.Response: After Boromir takes any amount of damage, ready him.",
                EncounterSet = "The Road Darkens",
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Elrond",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898010",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Willpower = 3,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Noldor.", " Healer." },
                Text = "At the end of the round, discard Elrond from play.Response: After Elrond enters play, choose one: heal all damage from a hero, discard a Condition attachment, each player draws 1 card.",
                EncounterSet = "The Road Darkens",
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Bilbo Baggins",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898011",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Willpower = 2,
                Attack = 0,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit." },
                Text = "Response: After Bilbo Baggins enters play, search your deck for a Pipe attachment and add it to your hand.",
                EncounterSet = "The Road Darkens",
                Quantity = 3,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Phial of Galadriel",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898012",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Artifact.", " Item." },
                Keywords = new List<string>() { "Attach to the Ring-bearer." },
                Text = "Action: Add Phial of Galadriel to the victory display to give each enemy engaged with you -3 Attack until the end of the phase.Victory 3.",
                EncounterSet = "The Road Darkens",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Three Golden Hairs",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898013",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Item." },
                Keywords = new List<string>() { "Attach to a hero." },
                Text = "Action: Add Three Golden Hairs to the victory display to lower each player’s threat by 3.Victory 3.",
                EncounterSet = "The Road Darkens",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Elven Rope",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898014",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Item." },
                Keywords = new List<string>() { "Attach to a hero." },
                Text = "Action: Add Elven Rope to the victory display to give each location in play -1 Threat until the end of the phase.Victory 3.",
                EncounterSet = "The Road Darkens",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Elven Waybread",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898015",
                IsUnique = true,
                CardType = CardType.Attachment,
                ResourceCost = 0,
                Traits = new List<string>() { "Item." },
                Keywords = new List<string>() { "Attach to a hero." },
                Text = "Action: Add Elven Waybread to the victory display to choose a player. That player readies each hero he controls.Victory 3.",
                EncounterSet = "The Road Darkens",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Gandalf's Staff",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898016",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                ResourceCost = 2,
                Traits = new List<string>() { "Artifact.", " Item." },
                Keywords = new List<string>() { "Attach to Gandalf.", " Restricted." },
                Text = "Action: Exhaust Gandalf’s Staff to (choose one): choose a player to draw 1 card, add 1 resource to a hero’s resource pool, or discard a shadow card from a non-unique enemy.",
                EncounterSet = "The Road Darkens",
                Quantity = 3,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "The Secret Fire",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898017",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                ResourceCost = 1,
                Traits = new List<string>() { "Spell." },
                Text = "Action: Discard the top card of your deck to ready an Istari character you control. That character gets +X Attack until the end of the phase where X is the discarded card’s cost. Add this card to the victory display.Victory 1.",
                EncounterSet = "The Road Darkens",
                VictoryPoints = 1,
                Quantity = 3,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Fellowship of the Ring",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898018",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Fellowship,
                ResourceCost = 2,
                Traits = new List<string>() { "Condition." },
                Keywords = new List<string>() { "Attach to the Ring-bearer." },
                Text = "Forced: Each hero gets +1 Willpower. After a character leaves play, discard Fellowship of the Ring.",
                EncounterSet = "The Road Darkens",
                Quantity = 3,
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Wizard's Pipe",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898019",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                ResourceCost = 1,
                Traits = new List<string>() { "Item.", " Pipe." },
                Keywords = new List<string>() { "Attach to an Istari.", " Limit 1 per character." },
                Text = "Action: Exhaust Wizard’s Pipe to exchange a card in your hand with the top card of your deck.",
                EncounterSet = "The Road Darkens",
                Quantity = 3,
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "The Council of Elrond - 1A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898020",
                CardType = CardType.Quest,
                Text = "Setup: Set Redhorn Pass, Doors of Durin and Watcher in the Water aside, out of play. Shuffle the encounter deck.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Setup = "tttt",
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "The Nine Walkers - 2A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898021",
                CardType = CardType.Quest,
                Text = "When Revealed: Make Rehdorn Pass the active location. The first players reveals cards from the encounter deck until there is at least X Threat in the staging area. X is twice the number of players in the game.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "The Hunt is Up! - 3A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898022",
                CardType = CardType.Quest,
                Text = "When Revealed: Each each player searches the encounter deck and discard pile for an enemy and adds it to the staging area. One of the enemies must be Great Warg Chief, if able.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "The Gates of Moria - 4A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898023",
                CardType = CardType.Quest,
                Text = "When Revealed: Add Watcher in the Water to the staging area. Make Doors of Durin the active location. Then, each player searches the encounter deck and discard pile for a location and adds it to the staging area.  Shuffle the encounter deck.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "The Ring Goes South",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898024",
                CardType = CardType.Campaign,
                Keywords = new List<string>() { "You are playing Campaign Mode." },
                Text = "Setup: Shuffle the 4 TRGS burdens into the encounter deck. The first player may attach the Boon cards Sting, Glamdring, Anduril, and Mithril Shirt to a hero or heroes of his choice. Shuffle each of the (TRGS) burden cards into the encounter deck.",
                EncounterSet = "The Lord of the Rings Part 4",
                Quantity = 1,
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "Doors of Durin",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898025",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 9,
                Traits = new List<string>() { "Gate." },
                Text = "Immune to player card effects. Progress must be placed on each other active location before it can be placed here.If there are 9 damage tokens here, the players lose the game.When Doors of Durin is explored, the players win the game.",
                EncounterSet = "The Ring Goes South",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                Title = "Watcher in the Water",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898026",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 5,
                Attack = 7,
                Defense = 5,
                HitPoints = 12,
                Traits = new List<string>() { "Creature." },
                Keywords = new List<string>() { "Indestructible.", " Immune to player card effects." },
                Text = "Watcher in the Water engages the first player and contributes its threat to the staging area.Forced: After Watcher in the Water attacks and destroys a character, deal 1 damage to each active location.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 26
            });
            Cards.Add(new Card() {
                Title = "Tree-crowned Hill",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898027",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 6,
                Traits = new List<string>() { "Hills." },
                Text = "While Tree-crowned Hill is the active location, each enemy gets -1 Threat.Forced: When Tree-crowned Hill is explored, exhaust X characters where X is the number of damage here.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 27
            });
            Cards.Add(new Card() {
                Title = "Great Warg Chief",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898028",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 30,
                Threat = 4,
                Attack = 5,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string>() { "Creature." },
                Keywords = new List<string>() { "Cannot have attachments." },
                Text = "Forced: After Great Warg Chief engages you, discard cards from the encounter deck until an enemy is discarded. Put that enemy into play engaged with you.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 28
            });
            Cards.Add(new Card() {
                Title = "Crebain from Dunland",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898029",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 3,
                Attack = 1,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Creature." },
                Keywords = new List<string>() { "Peril." },
                Text = "When Revealed: Exhaust a hero you control or engage Crebain from Dunland.Forced: After Crebain from Dunland engages a player, reveal the top card of the encounter deck.",
                EncounterSet = "The Ring Goes South",
                Quantity = 2,
                Number = 29
            });
            Cards.Add(new Card() {
                Title = "Hound of Sauron",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898030",
                CardType = CardType.Enemy,
                EngagementCost = 45,
                Threat = 1,
                Attack = 4,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Creature." },
                Keywords = new List<string>() { "Surge." },
                Text = "Hound of Sauron gets -5 engagement cost for each enemy in the staging area.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack (+2 instead if was engaged this round).",
                EncounterSet = "The Ring Goes South",
                Quantity = 4,
                Number = 30
            });
            Cards.Add(new Card() {
                Title = "Howling Warg",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898031",
                CardType = CardType.Enemy,
                EngagementCost = 38,
                Threat = 2,
                Attack = 3,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Creature." },
                Text = "Forced: When Howling Warg attacks, place 1 damage on the active location.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. If this attack destroys a character, place 1 damage on the active location.",
                EncounterSet = "The Ring Goes South",
                Quantity = 4,
                Number = 31
            });
            Cards.Add(new Card() {
                Title = "Hills of Hollin",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898032",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "Hills." },
                Text = "While Hills of Hollin is the active location, each enemy in the staging area gets +5 engagement cost.Forced: When Hills of Hollin leaves play, each player raises his threat by 1 for each damage here.",
                EncounterSet = "The Ring Goes South",
                Quantity = 3,
                Number = 32
            });
            Cards.Add(new Card() {
                Title = "Redhorn Foothills",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898033",
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 5,
                Traits = new List<string>() { "Hills." },
                Text = "Forced: When Redhorn Foothills is explored, each player must discard X cards from his hand at random where X is the number of damage here.While Redhorn Foothills is the active location, it gains: 'Forced: After an enemy is revealed from the encounter deck, it gets -5 engagement cost until the end of the round.'",
                EncounterSet = "The Ring Goes South",
                Quantity = 3,
                Number = 33
            });
            Cards.Add(new Card() {
                Title = "Eregion",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898034",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 4,
                Traits = new List<string>() { "Hills." },
                Text = "Forced: When Eregion leaves play, the players as a group must discard X allies from play where X is the number of damage here.",
                Shadow = "Shadow: After this attack, attacking enemy engages the first player then makes an immediate attack.",
                EncounterSet = "The Ring Goes South",
                Quantity = 3,
                Number = 34
            });
            Cards.Add(new Card() {
                Title = "Redhorn Pass",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898035",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 6,
                Traits = new List<string>() { "Mountain." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Text = "Forced: When Redhorn Pass is explored, each player raises his threat by 1 for each damage here.Victory 1.",
                EncounterSet = "The Ring Goes South",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 35
            });
            Cards.Add(new Card() {
                Title = "Regiments of Black Crows",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898036",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Deal 1 damage to each location in play.",
                EncounterSet = "The Ring Goes South",
                Quantity = 2,
                Number = 36
            });
            Cards.Add(new Card() {
                Title = "Snowdrifts",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898037",
                CardType = CardType.Treachery,
                Text = "When Revealed: Attach to the active location.  Counts as a Condition attachment with the text, 'Cancel each progress after 3 that would be placed on this location each round.'",
                EncounterSet = "The Ring Goes South",
                Quantity = 2,
                Number = 37
            });
            Cards.Add(new Card() {
                Title = "Bitter Cold",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898038",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                Text = "When Revealed: Raise your threat by 1 for each ally you control.  If you control no allies, Bitter Cold gains surge.",
                Shadow = "Shadow: Defending player discards a non-objective attachment he controls.",
                EncounterSet = "The Ring Goes South",
                Quantity = 2,
                Number = 38
            });
            Cards.Add(new Card() {
                Title = "Storm of Howls",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898039",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                Text = "When Revealed: Choose an enemy in the staging area to engage you. Then, the engaged enemy makes an immediate attack. If no attack is made this way, Storm of Howls gains surge.",
                Shadow = "Shadow: Attacking enemy makes an additional attack after this one.",
                EncounterSet = "The Ring Goes South",
                Quantity = 3,
                Number = 39
            });
            Cards.Add(new Card() {
                Title = "Pursued by the Enemy",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898040",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril.", " Surge." },
                Text = "When Revealed: Attach to the hero you control with the fewest printed hit points. Counts as a Condition attachment with the text: “Undefended damage from any enemy in play must be assigned to attached hero.”",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 40
            });
            Cards.Add(new Card() {
                Title = "Lost and Helpless",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898041",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril.", " Surge." },
                Text = "When Revealed: Discard all resources from the resource pool of each hero you control.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 41
            });
            Cards.Add(new Card() {
                Title = "Followed by Night",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898042",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril.", " Surge." },
                Text = "When Revealed: Return the topmost enemy from the encounter discard pile to play engaged with you.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 42
            });
            Cards.Add(new Card() {
                Title = "Ill Fate",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898043",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril.", " Surge." },
                Text = "When Revealed: Discard a random card from your hand.",
                EncounterSet = "The Ring Goes South",
                Quantity = 1,
                Number = 43
            });
            Cards.Add(new Card() {
                Title = "The Long Dark of Moria - 1A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898044",
                CardType = CardType.Quest,
                Text = "Setup: Set The Balrog, The Great Bridge, and Chamber of Marzabul aside out of play. Add Doom, Doom, Doom to the staging area and place 10 damage tokens on it. Each player adds 1 different location to the staging area. Shuffles the encounter deck.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Setup = "tttts",
                Number = 44
            });
            Cards.Add(new Card() {
                Title = "Drums in the Deep - 2A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898045",
                CardType = CardType.Quest,
                Text = "When Revealed: Add Chamber of Marzabul to the staging area.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 45
            });
            Cards.Add(new Card() {
                Title = "The Bridge of Khazad-dum - 3A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898046",
                CardType = CardType.Quest,
                Text = "When Revealed: Add The Great Bridge to the staging area. Each player reveals 1 encounter card and removes 1 damage token from Doom, Doom, Doom.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 46
            });
            Cards.Add(new Card() {
                Title = "The Balrog",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898047",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 5,
                Attack = 8,
                Defense = 6,
                HitPoints = 20,
                Traits = new List<string>() { "Balrog." },
                Keywords = new List<string>() { "Immune to player card effects.", " Cannot be optionally engaged." },
                Text = "While in the staging area, The Balrog is considered to be engaged with the first player.Forced: When The Balrog attacks, the defending player deals 5 damage to a character he controls.Victory 20.",
                EncounterSet = "Journey in the Dark",
                VictoryPoints = 20,
                Quantity = 1,
                Number = 47
            });
            Cards.Add(new Card() {
                Title = "Huge Orc-chieftain",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898048",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 40,
                Threat = 4,
                Attack = 4,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Keywords = new List<string>() { "Cannot have attachments." },
                Text = "Allies cannot defend against Huge Orc-chieftan.Forced: At the beginning of the engagement phase, Huge Orc-chieftain engages the first player.  ",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 48
            });
            Cards.Add(new Card() {
                Title = "Great Cave-troll",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898049",
                CardType = CardType.Enemy,
                EngagementCost = 36,
                Threat = 3,
                Attack = 6,
                Defense = 4,
                HitPoints = 6,
                Traits = new List<string>() { "Troll." },
                Keywords = new List<string>() { "Cannot have attachments." },
                Text = "For each excess point of combat damage dealt by Great Cave-troll (damage that is dealt beyond the remaining hit points of the character damaged by its attack) remove 1 progress from the current quest.",
                EncounterSet = "Journey in the Dark",
                Quantity = 2,
                Number = 49
            });
            Cards.Add(new Card() {
                Title = "Uruk from Mordor",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898050",
                CardType = CardType.Enemy,
                EngagementCost = 34,
                Threat = 2,
                Attack = 4,
                Defense = 2,
                HitPoints = 5,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Text = "When Revealed: Uruk from Mordor makes an attack against the first player. ",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. If this attack destroys a character, attacking enemy makes an additional attack.",
                EncounterSet = "Journey in the Dark",
                Quantity = 3,
                Number = 50
            });
            Cards.Add(new Card() {
                Title = "Moria Orc",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898051",
                CardType = CardType.Enemy,
                EngagementCost = 38,
                Threat = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Orc." },
                Keywords = new List<string>() { "Peril." },
                Text = "When Revealed: Either remove 1 damage from Doom, Doom, Doom, or reveal an additional encounter card.",
                Shadow = "Shadow: Each enemy engaged with you gets +1 Attack until the end of the phase.",
                EncounterSet = "Journey in the Dark",
                Quantity = 3,
                Number = 51
            });
            Cards.Add(new Card() {
                Title = "Moria Archer",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898052",
                CardType = CardType.Enemy,
                EngagementCost = 42,
                Threat = 0,
                Attack = 3,
                Defense = 3,
                HitPoints = 4,
                Traits = new List<string>() { "Orc." },
                Keywords = new List<string>() { "Peril.", " Archery X." },
                Text = "X is the number of players in the game. When Revealed: Assign X damage among characters you control.",
                EncounterSet = "Journey in the Dark",
                Quantity = 4,
                Number = 52
            });
            Cards.Add(new Card() {
                Title = "The Great Bridge",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898053",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 5,
                QuestPoints = 5,
                Traits = new List<string>() { "Underground.", " Bridge." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Text = "Response: After The Great Bridge leaves play as an explored location, the first player may discard a hero to deal X damage to The Balrog where X is that hero’s threat cost.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 53
            });
            Cards.Add(new Card() {
                Title = "Chamber of Marzabul",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898054",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 4,
                Traits = new List<string>() { "Underground." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Text = "The players cannot advance while Chamber of Marzabul is in play.Travel: Each player searches the encounter deck and discard pile for a different enemy and adds it to the staging area to travel here. One of the chosen enemies must be Huge Orc-chieftain, if able.Victory 4.",
                EncounterSet = "Journey in the Dark",
                VictoryPoints = 4,
                Quantity = 1,
                Number = 54
            });
            Cards.Add(new Card() {
                Title = "Ancient Guardroom",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898055",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 3,
                Traits = new List<string>() { "Underground." },
                Text = "While Ancient Guardroom is the active location, each enemy in play gets -1 Attack.Travel: Discard the top card of the encounter deck to travel here. Each player raises his threat by X where X is the discarded card’s Threat.",
                EncounterSet = "Journey in the Dark",
                Quantity = 2,
                Number = 55
            });
            Cards.Add(new Card() {
                Title = "Many-pillared Hall",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898056",
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 6,
                Traits = new List<string>() { "Underground." },
                Text = "Many-pillard Hall gets +1 threat strength for each Many-pillared Hall in play.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack (+2 Attack instead if the defending player’s threat is 35 or higher).",
                EncounterSet = "Journey in the Dark",
                Quantity = 4,
                Number = 56
            });
            Cards.Add(new Card() {
                Title = "Mines of Moria",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898057",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "Underground." },
                Text = "Progress that would be placed on the current quest, must be placed on a Mines of Moria instead.",
                Shadow = "Shadow: Defending player discards an attachment he controls (2 attachments instead if his threat is 35 or higher).",
                EncounterSet = "Journey in the Dark",
                Quantity = 4,
                Number = 57
            });
            Cards.Add(new Card() {
                Title = "Darkened Stairway",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898058",
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 2,
                Traits = new List<string>() { "Underground." },
                Text = "Forced: After Darkened Stairway leaves play as an explored location, discard the top card of the encounter deck. If the discarded card is a location, add it to the staging area.",
                EncounterSet = "Journey in the Dark",
                Quantity = 2,
                Number = 58
            });
            Cards.Add(new Card() {
                Title = "They Are Coming!",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898059",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Doomed 1." },
                Text = "When Revealed: Starting with the first player, each player discards cards from the top of the encounter deck until an enemy is discarded, then that player reveals that enemy and adds it to the staging area.",
                EncounterSet = "Journey in the Dark",
                Quantity = 2,
                Number = 59
            });
            Cards.Add(new Card() {
                Title = "Fool of a Took!",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898060",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge.", " Doomed 1." },
                Text = "When Revealed: Remove 1 damage token from Doom, Doom, Doom.",
                Shadow = "Shadow: After this attack, attacking enemy makes an additional attack against the first player.",
                EncounterSet = "Journey in the Dark",
                Quantity = 2,
                Number = 60
            });
            Cards.Add(new Card() {
                Title = "Deep Fissure",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898061",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                Text = "When Revealed: Remove a character you control from the quest and discard the top card of the encounter deck. If the discarded card’s Threat is equal to or greater than that character’s printed Willpower, discard that character.",
                EncounterSet = "Journey in the Dark",
                Quantity = 4,
                Number = 61
            });
            Cards.Add(new Card() {
                Title = "Dark and Dreadful",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898062",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each players assigns X damage among characters he controls where X is the number of exhausted characters he controls.",
                Shadow = "Shadow: Assign X damage among characters you control. X is the number of enemies engaged with you.",
                EncounterSet = "Journey in the Dark",
                Quantity = 2,
                Number = 62
            });
            Cards.Add(new Card() {
                Title = "Journey in the Dark",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898063",
                CardType = CardType.Campaign,
                Keywords = new List<string>() { "You are playing Campaign Mode." },
                EncounterSet = "The Lord of the Rings Part 5",
                Quantity = 1,
                Number = 63
            });
            Cards.Add(new Card() {
                Title = "Doom, Doom, Doom",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898064",
                CardType = CardType.Objective,
                Text = "Forced: At the end of each quest phase, remove 1 damage token from Doom, Doom, Doom.Forced: After the last damage token is removed from Doom, Doom, Doom, add The Balrog to the staging area.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 64
            });
            Cards.Add(new Card() {
                Title = "Overcome by Grief",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898065",
                CardType = CardType.Objective,
                Keywords = new List<string>() { "Permanent." },
                Text = "Setup: Attach to a hero you control.Forced: After a character you control leaves play, raise your threat by 1.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 65
            });
            Cards.Add(new Card() {
                Title = "Grievous Wound",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898066",
                CardType = CardType.Objective,
                Keywords = new List<string>() { "Permanent." },
                Text = "Setup: Attach to a hero you control.Forced: After attached hero takes any amount of damage, raise your threat by 1.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 66
            });
            Cards.Add(new Card() {
                Title = "Lust for Power",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898067",
                CardType = CardType.Objective,
                Keywords = new List<string>() { "Permanent." },
                Text = "Setup: Attach to a hero you control.Forced: After attached hero gains a resource, raise your threat by 1.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 67
            });
            Cards.Add(new Card() {
                Title = "Shadow of Fear",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898068",
                CardType = CardType.Objective,
                Keywords = new List<string>() { "Permanent." },
                Text = "Setup: Attach to a hero you control.Forced: After you engage an enemy, raise your threat by 1.",
                EncounterSet = "Journey in the Dark",
                Quantity = 1,
                Number = 68
            });
            Cards.Add(new Card() {
                Title = "The Great River - 1A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898069",
                CardType = CardType.Quest,
                Text = "Setup: Set The Ring-bearer objective, Seat of Seeing, and Parth Galen aside, out of play. Add The Argonath and Sarn Gebir to the staging area. Shuffle the encounter deck.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 1,
                Setup = "ttttss",
                Number = 69
            });
            Cards.Add(new Card() {
                Title = "The Company Divided - 2A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898070",
                CardType = CardType.Quest,
                Text = "When Revealed: Treat The One Ring’s text box as blank and remove the Ring-bearer and each card attached to it from the game. Reduce each enemy’s engagement cost to 0 until the end of the engagement phase. Skip the travel phase this round.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 1,
                Number = 70
            });
            Cards.Add(new Card() {
                Title = "The Seat of Amon Hen - 3A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898071",
                CardType = CardType.Quest,
                Text = "Forced: After stage 2B is discarded, if the total Threat of encounter cards in this staging area is less than 4, reveal 1 encounter card.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 1,
                Number = 71
            });
            Cards.Add(new Card() {
                Title = "Guard the Hobbits - 3A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898072",
                CardType = CardType.Quest,
                Text = "Forced: After stage 2B is discarded, if the total Threat of encounter cards in this staging area is less than 4, reveal 1 encounter card.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 1,
                Number = 72
            });
            Cards.Add(new Card() {
                Title = "Orc-hunting - 3A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898073",
                CardType = CardType.Quest,
                Text = "Forced: After stage 2B is discarded, if the total Threat of encounter cards in this staging area is less than 4, reveal 1 encounter card.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 1,
                Number = 73
            });
            Cards.Add(new Card() {
                Title = "Searching the Woods - 3A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898074",
                CardType = CardType.Quest,
                Text = "Forced: After stage 2B is discarded, if the total Threat of encounter cards in this staging area is less than 4, reveal 1 encounter card.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 1,
                Number = 74
            });
            Cards.Add(new Card() {
                Title = "The Ring-bearer Sets Out - 4A",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898075",
                CardType = CardType.Quest,
                Text = "When Revealed: Take control of the first player token. Add Parth Galen to the staging area.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 1,
                Number = 75
            });
            Cards.Add(new Card() {
                Title = "The Breaking of the Fellowship",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898076",
                CardType = CardType.Campaign,
                Keywords = new List<string>() { "You are playing Campaign Mode." },
                Text = "Setup: Starting with the first player, each player chooses 1 of the Lothlorien boon cards and attaches it to a hero he controls.",
                EncounterSet = "The Lord of the Rings Part 6",
                Quantity = 1,
                Number = 76
            });
            Cards.Add(new Card() {
                Title = "Parth Galen",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898077",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "River.", " Lawn." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Text = "Travel: Search the encounter deck and discard pile for an enemy and add it to the staging area to travel here. Shuffle the encounter deck.While Parth Galen is in play, the players cannot win.Victory 1.",
                EncounterSet = "The Breaking of the Fellowship",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 77
            });
            Cards.Add(new Card() {
                Title = "Seat of Seeing",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898078",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 1,
                Traits = new List<string>() { "Hill." },
                Text = "While Seat of Seeing is the active location it gains, 'Response: After a player commits characters to quest at this stage, he raises his threat by 1 to look at the top 4 cards of the encounter deck and puts them back in any order.'Victory 3.",
                EncounterSet = "The Breaking of the Fellowship",
                VictoryPoints = 3,
                Quantity = 1,
                Number = 78
            });
            Cards.Add(new Card() {
                Title = "The Argonath",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898079",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 2,
                Traits = new List<string>() { "River." },
                Text = "Immune to player card effects. X is the number of players in the game.  The players cannot travel here while Sarn Gebir is in play. While The Argonath is the active location, skip the combat phase.  Victory 1.",
                EncounterSet = "The Breaking of the Fellowship",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 79
            });
            Cards.Add(new Card() {
                Title = "Sarn Gebir",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898080",
                IsUnique = true,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 4,
                Traits = new List<string>() { "River." },
                Keywords = new List<string>() { "Immune to player card effects." },
                Text = "Forced: When Sarn Gebir is explored, deal 1 damage to each exhausted character.  Victory 1.",
                EncounterSet = "The Breaking of the Fellowship",
                VictoryPoints = 1,
                Quantity = 1,
                Number = 80
            });
            Cards.Add(new Card() {
                Title = "The Ring-bearer",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898081",
                IsUnique = true,
                CardType = CardType.Objective,
                Traits = new List<string>() { "Ring-bearer." },
                Text = "If The Ring-bearer would be placed in the discard pile, add it to the staging area instead.Forced: At the end of the combat phase, place 1 damage token on The Ring-bearer for each enemy in the staging area. Then, if there are 10 or more damage tokens here, the players lose the game.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 3,
                Number = 81
            });
            Cards.Add(new Card() {
                Title = "Uruk-hai Archer",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898082",
                CardType = CardType.Enemy,
                EngagementCost = 44,
                Threat = 0,
                Attack = 4,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Keywords = new List<string>() { "Peril.", " Toughness 1.", " Archery X." },
                Text = "X is the number of players at this stage.When Revealed: Assign X damage among characters you control.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 4,
                Number = 82
            });
            Cards.Add(new Card() {
                Title = "Uruk-hai Hunter",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898083",
                CardType = CardType.Enemy,
                EngagementCost = 33,
                Threat = 1,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Keywords = new List<string>() { "Surge.", " Toughness 1.", " Archery 1." },
                Text = "Uruk-hai Hunter gets +2 Attack when attacking a character with fewer printed hit points than its printed hit points.",
                Shadow = "Shadow: Discard a non-objective attachment you control.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 4,
                Number = 83
            });
            Cards.Add(new Card() {
                Title = "Uruk-hai Captain",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898084",
                CardType = CardType.Enemy,
                EngagementCost = 36,
                Threat = 3,
                Attack = 5,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Keywords = new List<string>() { "Toughness 2.", " Cannot have attachments." },
                Text = "Allies with fewer printed hit points than Uruk-hai Captain cannot defend against Uruk-hai Captain.",
                Shadow = "Shadow: Attacking enemy makes an additional attack after this one.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 2,
                Number = 84
            });
            Cards.Add(new Card() {
                Title = "Uruk-hai Prowler",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898085",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 2,
                Attack = 4,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Keywords = new List<string>() { "Peril.", " Toughness 1." },
                Text = "When Revealed: Either choose a player to reveal an encounter card, or Uruk-hai Prowler makes an immediate attack against you.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 3,
                Number = 85
            });
            Cards.Add(new Card() {
                Title = "Slopes of Amon Hen",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898086",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 3,
                Traits = new List<string>() { "Forest.", " Hill." },
                Text = "Travel: Engage an enemy in any player’s staging area to travel here.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack (+2 Attack instead if the defending character has fewer printed hit points).",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 3,
                Number = 86
            });
            Cards.Add(new Card() {
                Title = "Wooded Shoreline",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898087",
                CardType = CardType.Location,
                Threat = 0,
                QuestPoints = 4,
                Traits = new List<string>() { "Forest.", " River." },
                Keywords = new List<string>() { "X is the number of players at this stage." },
                Text = "While Wooded Shoreline is in the staging area, it gains archery X.Travel: Search the encounter deck and discard pile for an enemy and add it this staging area to travel here. Shuffle the encounter deck.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 3,
                Number = 87
            });
            Cards.Add(new Card() {
                Title = "River Anduin",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898088",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 5,
                Traits = new List<string>() { "River." },
                Text = "While River Anduin is in the staging area it gains: 'Forced: At the end of the travel phase, each player at this stage raises his threat by 1. Then, River Anduin moves to the staging area to the left, if able.'",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 3,
                Number = 88
            });
            Cards.Add(new Card() {
                Title = "Orcs of the White Hand",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898089",
                CardType = CardType.Treachery,
                Text = "When Revealed: Remove all damage from each enemy at this stage. Each enemy at this stage gets +1 Threat, +1 Attack, and +1 Defense until the end of the round.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack (+2 Attack instead if the defending character has fewer printed hit points).",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 3,
                Number = 89
            });
            Cards.Add(new Card() {
                Title = "Growing Threat",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898090",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril.", " Doomed 1." },
                Text = "When Revealed: Move each enemy engaged with you to the first player’s staging area. If no enemies were moved to the first player’s staging area this way, Growing Threat gains surge.",
                Shadow = "Shadow: After this attack, move attacking enemy to the first player’s staging area.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 2,
                Number = 90
            });
            Cards.Add(new Card() {
                Title = "Fallen into Evil",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898091",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Peril." },
                Text = "When Revealed: Attach to a hero you control. Lose control of that hero and move it to the first player’s staging area. (Counts as a Condition attachment with the text: 'Treat attached hero’s text box as blank. Attached hero loses the hero card type and gains the enemy card type with Threat equal to its Willpower and engagement cost equal to its threat cost.')",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 1,
                Number = 91
            });
            Cards.Add(new Card() {
                Title = "Black Feathered Arrows",
                Id = "5d2630ae-4ee7-48be-a8b6-b0c1e4898092",
                CardType = CardType.Treachery,
                Text = "When Revealed: Until the end of the round, add 1 to the archery total for each ally currently at this stage.",
                Shadow = "Shadow: If the defending character has fewer printed hit points than the attacking enemy, this attack is considered undefended.",
                EncounterSet = "The Breaking of the Fellowship",
                Quantity = 2,
                Number = 92
            });
        }
    }
}
