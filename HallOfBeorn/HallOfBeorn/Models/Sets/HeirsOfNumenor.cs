using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class HeirsofNumenor : CardSet
    {
        protected override void Initialize()
        {
            Name = "Heirs of Númenor";
            Abbreviation = "HoN";
            Number = 15;
            SetType = Models.SetType.Deluxe_Expansion;

            Cards.Add(new Card() {
                ImageName = "M1721",
                Title = "Alcaron's Scroll",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9001",
                CardType = CardType.Objective,
                IsUnique = true,
                Quantity = 1,
                Traits = new List<string>() { "Scroll." },
                Text = "If unattached, return Alcaron's Scroll to the staging area and it gains: 'Action: Exhaust a hero to attach Alcaron's Scroll to that hero.'Forced: When the hero with Alcaron's Scroll attached is damaged by an enemy attack, attach Alcaron's Scroll to that enemy.",
                EncounterSet = "Peril in Pelargir",
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1714",
                Title = "A Watchful Peace",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9002",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Response: After a location leaves play as an explored location, return it to the top of the encounter deck.",
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1733",
                Title = "Battering Ram",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9003",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Doomed 2." },
                Quantity = 3,
                Traits = new List<string>() { "Besieger." },
                HitPoints = 5,
                Attack = 7,
                Defense = 3,
                Text = "When Revealed: If the active location is a Battleground, deal 3 damage to it.",
                EncounterSet = "The Siege of Cair Andros",
                EngagementCost = 33,
                Shadow = "Shadow: Deal 2 damage to The Approach if it is in play. Otherwise, attacking enemy gets +2 Attack.",
                Threat = 2,
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1710",
                Title = "Behind Strong Walls",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9004",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: Ready a defending Gondor character. That character gets +1 Defense until the end of the phase.",
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1703",
                Title = "Beregond",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9005",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                Keywords = new List<string>() { "Sentinel." },
                ThreatCost = 10,
                Quantity = 1,
                Traits = new List<string>() { "Gondor.", " Warrior." },
                Willpower = 0,
                HitPoints = 4,
                Attack = 1,
                Defense = 4,
                Text = "Lower the cost to play Weapon and Armor attachments on Beregond by 2.",
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1731",
                Title = "Blocking Wargs",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9006",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Quantity = 3,
                Text = "When Revealed: Deal 1 damage to each character committed to the quest.",
                EncounterSet = "Into Ithilien",
                Shadow = "Shadow: Deal 1 damage to the defending character.",
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1715",
                Title = "Blood of Númenor",
                NormalizedTitle = "Blood of Numenor",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9007",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                Keywords = new List<string> { "Attach to a Gondor or Dúnedain hero." },
                NormalizedKeywords = new List<string>() { "Attach to a Gondor or Dunedain hero." },
                ResourceCost = 0,
                Quantity = 3,
                Traits = new List<string>() { "Condition." },
                Text = "Action: Spend 1 resource from attached hero's resource pool to give attached hero +1 Defense for each resource in its resource pool until the end of the phase. (Limit once per phase.)",
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1704",
                Title = "Boromir",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9008",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                IsUnique = true,
                ThreatCost = 11,
                Quantity = 1,
                Traits = new List<string>() { "Gondor.", " Warrior.", " Noble." },
                Willpower = 1,
                HitPoints = 5,
                Attack = 3,
                Defense = 2,
                Text = "While Boromir has at least 1 resource in his resource pool, Gondor allies get +1 Attack.",
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1727",
                Title = "Celador",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9009",
                CardType = CardType.Objective,
                IsUnique = true,
                Quantity = 1,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Willpower = 2,
                HitPoints = 3,
                Attack = 2,
                Defense = 2,
                Text = "While Celador is in the staging area, he is committed to the current quest.Forced: After players quest unsuccessfully or a character leaves play, deal 1 damage to Celador.If Celador leaves play, remove him from the game.",
                EncounterSet = "Into Ithilien",
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1706",
                Title = "Citadel Custodian",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9010",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 5,
                Quantity = 3,
                Traits = new List<string>() { "Gondor." },
                Willpower = 1,
                HitPoints = 3,
                Attack = 0,
                Defense = 1,
                Text = "Lower the cost to play Citadel Custodian by 1 for each Gondor ally in play.",
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1740",
                Title = "City Street",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9011",
                CardType = CardType.Location,
                Keywords = new List<string>() { "Surge." },
                Quantity = 3,
                Traits = new List<string>() { "City." },
                Text = "While City Street is in the staging area, players cannot travel to a location that does not have the title City Street.",
                QuestPoints = 2,
                EncounterSet = "Streets of Gondor",
                Shadow = "Shadow: attacking enemy gets +2 Attack.",
                Threat = 2,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1726",
                Title = "Collateral Damage",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9012",
                CardType = CardType.Treachery,
                Quantity = 4,
                Text = "When Revealed: Discard 2 cards from the top of the encounter deck. Discard an additional 2 cards for each copy of Collateral Damage in the discard pile. Then, raise each player's threat by 2 for each location discarded by this effect.",
                EncounterSet = "Peril in Pelargir",
                Shadow = "Shadow: Any damage dealt by this attack is dealt to the hero with Alcaron's Scroll attached, if able.",
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1712",
                Title = "Damrod",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9013",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                IsUnique = true,
                ResourceCost = 4,
                Quantity = 3,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Willpower = 1,
                HitPoints = 2,
                Attack = 2,
                Defense = 2,
                Text = "Action: Discard Damrod from play to lower your threat by 1 for each enemy in the staging area.",
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1709",
                Title = "Defender of Rammas",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9014",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Gondor.", " Warrior." },
                Willpower = 0,
                HitPoints = 1,
                Attack = 1,
                Defense = 4,
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1720",
                Title = "Envoy of Pelargir",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9015",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Gondor." },
                Willpower = 1,
                HitPoints = 1,
                Attack = 1,
                Defense = 0,
                Text = "Response: After Envoy of Pelargir enters play, add 1 resource to a Gondor or Noble hero's resource pool.",
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1705",
                Title = "Errand-rider",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9016",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Gondor." },
                Willpower = 0,
                HitPoints = 2,
                Attack = 0,
                Defense = 0,
                Text = "Action: Exhaust Errand-rider to move 1 resource from the resource pool of a hero you control to another hero's resource pool.",
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1752",
                Title = "Forest Bat",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9017",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Creature." },
                HitPoints = 1,
                Attack = 1,
                Defense = 1,
                Text = "When Revealed: The first player deals 2 damage to a questing hero and removes that hero from the quest.",
                EncounterSet = "Creatures of the Forest",
                EngagementCost = 18,
                Shadow = "Shadow: Defending player raises his threat by X where X is the attacking enemy's Threat.",
                Threat = 1,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1755",
                Title = "Haradrim Elite",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9018",
                CardType = CardType.Enemy,
                Quantity = 2,
                Traits = new List<string>() { "Harad." },
                HitPoints = 3,
                Attack = 4,
                Defense = 3,
                Text = "Forced: When Haradrim Elite enters play, it makes an immediate attack from the staging area against the first player.",
                EncounterSet = "Southrons",
                EngagementCost = 27,
                Shadow = "Shadow: This enemy attacks again after this attack resolves. (Deal a new shadow card for that attack.)",
                Threat = 3,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1725",
                Title = "Harbor Storehouse",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9019",
                CardType = CardType.Location,
                Quantity = 2,
                Traits = new List<string>() { "City." },
                Text = "Forced: Each time a location is discarded from the top of the encounter deck, raise each player's threat by 1.",
                QuestPoints = 4,
                EncounterSet = "Peril in Pelargir",
                Shadow = "Shadow: Deal attacking enemy an additional shadow card for each Thug enemy in play.",
                Threat = 1,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1722",
                Title = "Harbor Thug",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9020",
                CardType = CardType.Enemy,
                Quantity = 5,
                Traits = new List<string>() { "Thug." },
                HitPoints = 3,
                Attack = 3,
                Defense = 1,
                Text = "Forced: When the player whose hero has Alcaron's Scroll attached raises his threat, Harbor Thug engages that player.",
                EncounterSet = "Peril in Pelargir",
                EngagementCost = 25,
                Shadow = "Shadow: Any damage dealt by this attack is dealt to the hero with Alcaron's Scroll attached, if able.",
                Threat = 2,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1767",
                Title = "The Leaping Fish - 1A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9021",
                CardType = CardType.Quest,
                Setup = "lt",
                Quantity = 1,
                QuestPoints = 6,
                Text = "Setup: Search the encounter deck for The Leaping Fish and Alcaron's Scroll. Make The Leaping Fish the active location and attach Alcaron's Scroll to a hero.",
                EncounterSet = "Peril in Pelargir",
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1768",
                Title = "Fighting in the Streets - 2A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9023",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 13,
                EncounterSet = "Peril in Pelargir",
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1769",
                Title = "Escape to the Quays - 3A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9025",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 15,
                Text = "When Revealed: Each player searches the encounter deck and discard pile for 1 enemy and adds it to the staging area.",
                EncounterSet = "Peril in Pelargir",
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1770",
                Title = "Ambush in Ithilien - 1A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9027",
                CardType = CardType.Quest,
                Setup = "sl",
                Quantity = 1,
                QuestPoints = 15,
                Text = "Setup: Add Celador to the staging area. Search the encounter deck for a copy of Ithilien Road and make it the active location. Each player must search the encounter deck for a copy of Southron Company and add it to the staging area. Shuffle the encounter deck.",
                EncounterSet = "Into Ithilien",
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1771",
                Title = "Southron Counter-attack - 2A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9029",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 9,
                EncounterSet = "Into Ithilien",
                Number = 25
            });
            Cards.Add(new Card() {
                ImageName = "M1772",
                Title = "The Hidden Way - 3A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9031",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 12,
                EncounterSet = "Into Ithilien",
                Number = 26
            });
            Cards.Add(new Card() {
                ImageName = "M1773",
                Title = "Approaching Cair Andros - 4A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9033",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 15,
                EncounterSet = "Into Ithilien",
                Number = 27
            });
            Cards.Add(new Card() {
                ImageName = "M1774",
                Title = "The Defense - 1A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9035",
                CardType = CardType.Quest,
                Setup = "sss",
                Quantity = 1,
                QuestPoints = 9,
                Text = "Setup: Add The Approach, The Citadel, and The Banks to the staging area. Shuffle the encounter deck.",
                EncounterSet = "The Siege of Cair Andros",
                Number = 28
            });
            Cards.Add(new Card() {
                ImageName = "M1775",
                Title = "Reinforcing the Banks - 2A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9037",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 9,
                EncounterSet = "The Siege of Cair Andros",
                Number = 29
            });
            Cards.Add(new Card() {
                ImageName = "M1776",
                Title = "Breakthrough at the Approach - 3A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9039",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 7,
                EncounterSet = "The Siege of Cair Andros",
                Number = 30
            });
            Cards.Add(new Card() {
                ImageName = "M1777",
                Title = "Breakthrough at the Citadel - 4A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9041",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 5,
                EncounterSet = "The Siege of Cair Andros",
                Number = 31
            });
            Cards.Add(new Card() {
                ImageName = "M1778",
                Title = "The Last Battle - 5A",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9043",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 15,
                EncounterSet = "The Siege of Cair Andros",
                Number = 32
            });
            Cards.Add(new Card() {
                ImageName = "M1716",
                Title = "Hunter of Lamedon",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9045",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Gondor.", " Outlands." },
                Willpower = 1,
                HitPoints = 1,
                Attack = 1,
                Defense = 1,
                Text = "Response: After you play Hunter of Lamedon from your hand, reveal the top card of your deck. If it is an Outlands card, add it to your hand. Otherwise, discard it.",
                Number = 33
            });
            Cards.Add(new Card() {
                ImageName = "M1728",
                Title = "Ithilien Guardian",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9046",
                CardType = CardType.Objective,
                Quantity = 2,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Willpower = 2,
                HitPoints = 2,
                Attack = 2,
                Defense = 1,
                Text = "While Ithilien Guardian is in the staging area, he is committed to the current quest.When Revealed: Add Ithilien Guardian to the staging area and Ithilien Guardian gains surge.",
                EncounterSet = "Into Ithilien",
                Shadow = "Shadow: Deal 2 damage to the attacking enemy.",
                Number = 34
            });
            Cards.Add(new Card() {
                ImageName = "M1730",
                Title = "Ithilien Road",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9047",
                CardType = CardType.Location,
                Quantity = 2,
                Traits = new List<string>() { "Forest.", " Road." },
                Text = "While Ithilien Road is the active location, the engagement cost of each enemy in the staging area is 0.",
                QuestPoints = 4,
                EncounterSet = "Into Ithilien",
                Shadow = "Shadow: Return attacking enemy to the staging area after it attacks.",
                Threat = 4,
                Number = 35
            });
            Cards.Add(new Card() {
                ImageName = "M1717",
                Title = "Ithilien Tracker",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9048",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Willpower = 0,
                HitPoints = 3,
                Attack = 1,
                Defense = 0,
                Text = "Action: Exhaust Ithilien Tracker to lower the Threat of the next enemy added to the staging area to 0 until the end of the phase.",
                Number = 36
            });
            Cards.Add(new Card() {
                ImageName = "M1758",
                Title = "Lieutenant of Mordor",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9049",
                CardType = CardType.Enemy,
                Quantity = 1,
                Traits = new List<string>() { "Mordor." },
                HitPoints = 5,
                Attack = 5,
                Defense = 2,
                Text = "Allies cannot defend against Lieutenant of Mordor.When Revealed: Resolve the 'when revealed' effect on the topmost treachery card in the encounter discard pile, if able. This effect cannot be canceled.",
                EncounterSet = "Mordor Elite",
                EngagementCost = 33,
                Threat = 2,
                VictoryPoints = 3,
                Number = 37
            });
            Cards.Add(new Card() {
                ImageName = "M1713",
                Title = "Light the Beacons",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9050",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 5,
                Quantity = 3,
                Text = "Action: All characters get +2 Defense and do not exhaust to defend until the end of the round.",
                Number = 38
            });
            Cards.Add(new Card() {
                ImageName = "M1743",
                Title = "Local Trouble",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9051",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Attach this card to the hero with the highest threat cost without a copy of Local Trouble attached. (Counts as a Condition attachment with the text: 'When attached hero exhausts, readies, or triggers an ability, raise its controller's threat by 1.')",
                EncounterSet = "Streets of Gondor",
                Number = 39
            });
            Cards.Add(new Card() {
                ImageName = "M1745",
                Title = "Lossarnach Bandit",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9052",
                CardType = CardType.Enemy,
                Quantity = 2,
                Traits = new List<string>() { "Brigand." },
                HitPoints = 3,
                Attack = 3,
                Defense = 3,
                Text = "Forced: When Lossarnach Bandit engages a player, that player discards 1 resource from each of his heroes' resource pools. (2 resources instead if Lossarnach Bandit was not optionally engaged.)",
                EncounterSet = "Brigands",
                EngagementCost = 24,
                Threat = 3,
                Number = 40
            });
            Cards.Add(new Card() {
                ImageName = "M1750",
                Title = "Lost Companion",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9053",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Each player removes 1 character he controls from the quest, if able. Then, if any player has no characters committed to the quest, remove all characters from the quest.",
                EncounterSet = "Brooding Forest",
                Number = 41
            });
            Cards.Add(new Card() {
                ImageName = "M1742",
                Title = "Lost in the City",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9054",
                CardType = CardType.Treachery,
                Quantity = 1,
                Text = "When Revealed: Each player must search the encounter deck and discard pile for 1 City location and add it to the staging area, if able. Shuffle the encounter deck. This effect cannot be canceled.",
                EncounterSet = "Streets of Gondor",
                Shadow = "Shadow: Defending player discards his hand.",
                Number = 42
            });
            Cards.Add(new Card() {
                ImageName = "M1747",
                Title = "Lurking in Shadows",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9055",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Return all Brigand enemies engaged with players to the staging area. If this effect returned no Brigand enemies to the staging area, Lurking in Shadows gains surge.",
                EncounterSet = "Brigands",
                Shadow = "Shadow: Return attacking enemy to the staging area after it attacks.",
                Number = 43
            });
            Cards.Add(new Card() {
                ImageName = "M1741",
                Title = "Market Square",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9056",
                CardType = CardType.Location,
                Keywords = new List<string>() { "Immune to player card effects." },
                Quantity = 2,
                Traits = new List<string>() { "City." },
                Text = "Travel: Each player must spend 1 resource from one of his heroes' resource pools to travel here.",
                QuestPoints = 1,
                EncounterSet = "Streets of Gondor",
                Shadow = "Shadow: Defending player discards all resources in his heroes' resource pools.",
                Threat = 3,
                Number = 44
            });
            Cards.Add(new Card() {
                ImageName = "M1718",
                Title = "Master of Lore",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9057",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Gondor." },
                Willpower = 1,
                HitPoints = 1,
                Attack = 0,
                Defense = 1,
                Text = "Action: Exhaust Master of ~Lore to name a card type. Lower the cost for you to play the next Lore card of that type by 1 until the end of the phase (to a minimum of 1).",
                FlavorText = "\"If Cirith Ungol is named, old men and masters of lore will blanch and fall silent.\" -Faramir, The Two Towers",
                Number = 45
            });
            Cards.Add(new Card() {
                ImageName = "M1751",
                Title = "Morgul Spider",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9058",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Creature.", " Spider." },
                HitPoints = 5,
                Attack = 4,
                Defense = 1,
                Text = "When Revealed: Until the end of the round, Morgul Spider gets +1 Attack for each character not currently committed to a quest.",
                EncounterSet = "Creatures of the Forest",
                EngagementCost = 25,
                Threat = 3,
                Number = 46
            });
            Cards.Add(new Card() {
                ImageName = "M1756",
                Title = "Mûmak",
                NormalizedTitle = "Mumak",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9059",
                CardType = CardType.Enemy,
                Quantity = 1,
                Traits = new List<string>() { "Harad.", " Creature." },
                HitPoints = 12,
                Attack = 7,
                Defense = 3,
                Text = "No attachments can be attached to Mumak.Mumak cannot take more than 3 damage each round.",
                EncounterSet = "Southrons",
                EngagementCost = 38,
                Threat = 4,
                Number = 47
            });
            Cards.Add(new Card() {
                ImageName = "M1707",
                Title = "Mutual Accord",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9060",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Until the end of the phase, each Gondor card in play gains the Rohan trait, and each Rohan card in play gains the Gondor trait.",
                Number = 48
            });
            Cards.Add(new Card() {
                ImageName = "M1759",
                Title = "Orc Arbalesters",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9061",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Archery X." },
                Quantity = 3,
                Traits = new List<string>() { "Mordor.", " Orc." },
                HitPoints = 5,
                Attack = 1,
                Defense = 2,
                Text = "X is the number of different resource icons (Leadership, Tactics, Spirit, or Lore) on heroes in play.",
                EncounterSet = "Mordor Elite",
                EngagementCost = 35,
                Shadow = "Shadow: attacking enemy gets +2 Attack.",
                Threat = 2,
                Number = 49
            });
            Cards.Add(new Card() {
                ImageName = "M1765",
                Title = "Orc Arsonist",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9062",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Mordor.", " Orc." },
                HitPoints = 3,
                Attack = 3,
                Defense = 2,
                Text = "Forced: When Orc Arsonist engages a player, deal 1 shadow card to each enemy engaged with that player.",
                EncounterSet = "Ravaging Orcs",
                EngagementCost = 30,
                Shadow = "Shadow: attacking enemy gets +1 Attack. Deal it another shadow card.",
                Threat = 3,
                Number = 50
            });
            Cards.Add(new Card() {
                ImageName = "M1738",
                Title = "Orc Assault",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9063",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Each character gets -2 Attack and -2 Defense until the end of the round.",
                EncounterSet = "The Siege of Cair Andros",
                Shadow = "Shadow: Deal 2 damage to all Battleground locations in play.",
                Number = 51
            });
            Cards.Add(new Card() {
                ImageName = "M1764",
                Title = "Orc Rabble",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9064",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Mordor.", " Orc." },
                HitPoints = 3,
                Attack = 1,
                Defense = 1,
                Text = "Forced: When Orc Rabble is dealt a shadow card, it gets +2 Attack until the end of the phase.",
                EncounterSet = "Ravaging Orcs",
                EngagementCost = 28,
                Shadow = "Shadow: Deal the attacking enemy an additional shadow card for each player in the game.",
                Threat = 2,
                Number = 52
            });
            Cards.Add(new Card() {
                ImageName = "M1732",
                Title = "Orc Scramblers",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9065",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Surge." },
                Quantity = 3,
                Traits = new List<string>() { "Orc.", " Besieger." },
                HitPoints = 2,
                Attack = 2,
                Defense = 1,
                Text = "When Revealed: Deal 1 damage to each Battleground location in play, if able.",
                EncounterSet = "The Siege of Cair Andros",
                EngagementCost = 15,
                Shadow = "Shadow: Deal 2 damage to The Citadel if it is in play. Otherwise, attacking enemy gets +2 Attack.",
                Threat = 1,
                Number = 53
            });
            Cards.Add(new Card() {
                ImageName = "M1760",
                Title = "Orc Vanguard",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9066",
                CardType = CardType.Enemy,
                Quantity = 2,
                Traits = new List<string>() { "Mordor.", " Orc." },
                HitPoints = 5,
                Attack = 8,
                Defense = 3,
                Text = "While Orc Vanguard is in the staging area, resources cannot be spent from the resource pools of heroes who have a Leadership, Spirit, or Lore resource icon.",
                EncounterSet = "Mordor Elite",
                EngagementCost = 40,
                Shadow = "Shadow: Deal the attacking enemy two additional shadow cards.",
                Threat = 2,
                Number = 54
            });
            Cards.Add(new Card() {
                ImageName = "M1761",
                Title = "Orc War Camp",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9067",
                CardType = CardType.Location,
                Quantity = 2,
                Traits = new List<string>() { "Mordor." },
                Text = "If an Orc enemy is in play, progress tokens cannot be placed on Orc War Camp while it is in the staging area.",
                QuestPoints = 2,
                EncounterSet = "Mordor Elite",
                Shadow = "Shadow: attacking enemy gets +1 Attack for each shadow card dealt to it.",
                Threat = 5,
                Number = 55
            });
            Cards.Add(new Card() {
                ImageName = "M1748",
                Title = "Overgrown Trail",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9068",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Forest." },
                Text = "Action: Exhaust a Ranger character to place 3 progress tokens on Overgrown Trail.",
                QuestPoints = 6,
                EncounterSet = "Brooding Forest",
                Shadow = "Shadow: Remove X progress tokens from the current quest, where X is the attacking enemy's Threat.",
                Threat = 4,
                Number = 56
            });
            Cards.Add(new Card() {
                ImageName = "M1724",
                Title = "Pelargir Docks",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9069",
                CardType = CardType.Location,
                Quantity = 2,
                Traits = new List<string>() { "City.", " River." },
                Text = "While Pelargir Docks is the active location, enemies get +1 Attack and +1 Defense.",
                QuestPoints = 3,
                EncounterSet = "Peril in Pelargir",
                Threat = 4,
                Number = 57
            });
            Cards.Add(new Card() {
                ImageName = "M1739",
                Title = "Pickpocket",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9070",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Brigand." },
                HitPoints = 2,
                Attack = 1,
                Defense = 0,
                Text = "Forced: When Pickpocket attacks, the defending player discards 1 resource from one of his heroes' resource pools and 1 card at random from his hand.",
                EncounterSet = "Streets of Gondor",
                EngagementCost = 28,
                Shadow = "Shadow: Defending player discards 1 of his attachments. (Discard all of his attachments instead if undefended.)",
                Threat = 3,
                Number = 58
            });
            Cards.Add(new Card() {
                ImageName = "M1719",
                Title = "Ranger Spikes",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9071",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Trap." },
                Text = "Play Ranger Spikes into the staging area unattached.If unattached, attach Ranger Spikes to the next eligible enemy that enters the staging area.Players do not make engagement checks against attached enemy. Attached enemy gets -2 Threat.",
                Number = 59
            });
            Cards.Add(new Card() {
                ImageName = "M1766",
                Title = "Scourge of Mordor",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9072",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Each player discards the top card of his deck. Until the end of the phase, raise the total Threat in the staging area by X, where X is the total cost of all cards discarded by this effect.",
                EncounterSet = "Ravaging Orcs",
                Shadow = "Shadow: attacking enemy gets +1 Attack. Deal it another shadow card.",
                Number = 60
            });
            Cards.Add(new Card() {
                ImageName = "M1749",
                Title = "Secluded Glade",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9073",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Forest." },
                Text = "Immune to player card effects.",
                QuestPoints = 3,
                EncounterSet = "Brooding Forest",
                Shadow = "Shadow: Remove X progress tokens from the current quest, where X is the attacking enemy's Threat.",
                Threat = 3,
                Number = 61
            });
            Cards.Add(new Card() {
                ImageName = "M1734",
                Title = "Siege Raft",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9074",
                CardType = CardType.Enemy,
                Quantity = 2,
                Traits = new List<string>() { "Besieger." },
                HitPoints = 6,
                Attack = 4,
                Defense = 1,
                Text = "When Revealed: Deal 2 damage to the lowest Threat Battleground location in play, if able.",
                EncounterSet = "The Siege of Cair Andros",
                EngagementCost = 30,
                Shadow = "Shadow: Deal 2 damage to The Banks if it is in play. Otherwise, attacking enemy gets +2 Attack.",
                Threat = 3,
                Number = 62
            });
            Cards.Add(new Card() {
                ImageName = "M1729",
                Title = "Southron Company",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9075",
                CardType = CardType.Enemy,
                Quantity = 4,
                Traits = new List<string>() { "Harad." },
                HitPoints = 5,
                Attack = 3,
                Defense = 1,
                Text = "Southron Company gets +2 Threat and +2 Attack while the current quest card has the battle or siege keyword.",
                EncounterSet = "Into Ithilien",
                EngagementCost = 34,
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+2 Attack instead if the current quest card has a keyword.)",
                Threat = 1,
                Number = 63
            });
            Cards.Add(new Card() {
                ImageName = "M1754",
                Title = "Southron Mercenaries",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9076",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Archery X." },
                Quantity = 3,
                Traits = new List<string>() { "Harad." },
                HitPoints = 4,
                Attack = 3,
                Defense = 2,
                Text = "X is the number of players in the game.",
                EncounterSet = "Southrons",
                EngagementCost = 35,
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+3 Attack instead if it has the Harad trait.)",
                Threat = 2,
                Number = 64
            });
            Cards.Add(new Card() {
                ImageName = "M1757",
                Title = "Southron Support",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9077",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Doomed 3." },
                Quantity = 2,
                Text = "When Revealed: Each player must search the encounter deck and discard pile for 1 Harad enemy and add it to the staging area, if able. Shuffle the encounter deck.",
                EncounterSet = "Southrons",
                Number = 65
            });
            Cards.Add(new Card() {
                ImageName = "M1711",
                Title = "Spear of the Citadel",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9078",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Keywords = new List<string>() { "Attach to a Tactics character.", " Restricted." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Item.", " Weapon." },
                Text = "Limit 1 per character.Response: After attached character is declared as a defender, deal 1 damage to the attacking enemy.",
                Number = 66
            });
            Cards.Add(new Card() {
                ImageName = "M1736",
                Title = "The Approach",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9079",
                CardType = CardType.Location,
                Quantity = 1,
                Traits = new List<string>() { "Cair Andros.", " Battleground." },
                Text = "If the Approach has 7 or more damage, remove it from the game (do not collect its victory points).Response: After The Approach leaves play as an explored location, remove stage 3 from the quest deck, if able.",
                QuestPoints = 7,
                EncounterSet = "The Siege of Cair Andros",
                Threat = 2,
                VictoryPoints = 2,
                Number = 67
            });
            Cards.Add(new Card() {
                ImageName = "M1737",
                Title = "The Banks",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9080",
                CardType = CardType.Location,
                Quantity = 1,
                Traits = new List<string>() { "Cair Andros.", " Battleground." },
                Text = "If The Banks has 3 or more damage, remove it from the game (do not collect its victory points).Response: After The Banks leaves play as an explored location, remove stage 2 from the quest deck, if able.",
                QuestPoints = 3,
                EncounterSet = "The Siege of Cair Andros",
                Threat = 1,
                VictoryPoints = 1,
                Number = 68
            });
            Cards.Add(new Card() {
                ImageName = "M1735",
                Title = "The Citadel",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9081",
                CardType = CardType.Location,
                Quantity = 1,
                Traits = new List<string>() { "Cair Andros.", " Battleground." },
                Text = "If The Citadel has 11 or more damage, remove it from the game (do not collect its victory points).Response: After The Citadel leaves play as an explored location, remove stage 4 from the quest deck, if able.",
                QuestPoints = 11,
                EncounterSet = "The Siege of Cair Andros",
                Threat = 3,
                VictoryPoints = 3,
                Number = 69
            });
            Cards.Add(new Card() {
                ImageName = "M1723",
                Title = "The Leaping Fish",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9082",
                CardType = CardType.Location,
                IsUnique = true,
                Quantity = 1,
                Traits = new List<string>() { "City." },
                Text = "If Alcaron's Scroll is attached to a hero, The Leaping Fish gains: 'Forced: At the beginning of the quest phase discard X cards from the top of the encounter deck where X is the number of players in the game. Add each enemy discarded by this effect to the staging area.'",
                QuestPoints = 6,
                EncounterSet = "Peril in Pelargir",
                Threat = 2,
                VictoryPoints = 3,
                Number = 70
            });
            Cards.Add(new Card() {
                ImageName = "M1762",
                Title = "The Master's Malice",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9083",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Each player chooses 1 sphere of influence (Leadership, Tactics, Spirit, or Lore). Each character a player controls that does not belong to his chosen sphere takes 3 damage.",
                EncounterSet = "Mordor Elite",
                Number = 71
            });
            Cards.Add(new Card() {
                ImageName = "M1763",
                Title = "The Power of Mordor",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9084",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Doomed 3." },
                Quantity = 1,
                Text = "When Revealed: Count the number of encounter cards in the staging area and shuffle them into the encounter deck. Then, reveal an equal number of cards from the encounter deck and add them to the staging area. This effect cannot be canceled.",
                EncounterSet = "Mordor Elite",
                Number = 72
            });
            Cards.Add(new Card() {
                ImageName = "M1746",
                Title = "Umbar Assassin",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9085",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Archery 2." },
                Quantity = 1,
                Traits = new List<string>() { "Brigand." },
                HitPoints = 5,
                Attack = 5,
                Defense = 1,
                Text = "Forced: When Umbar Assassin engages a player, that player must deal 3 damage to a hero he controls. (Discard that hero instead if Umbar Assassin was not optionally engaged.)",
                EncounterSet = "Brigands",
                EngagementCost = 40,
                Threat = 4,
                Number = 73
            });
            Cards.Add(new Card() {
                ImageName = "M1753",
                Title = "Watcher in the Wood",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9086",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Raise each player's threat by the number of questing characters. (If the current quest has the battle or siege keyword, Watcher in the Wood gains surge.)",
                EncounterSet = "Creatures of the Forest",
                Shadow = "Shadow: Each player raises his threat by the number of enemies engaged with him.",
                Number = 74
            });
            Cards.Add(new Card() {
                ImageName = "M1708",
                Title = "Wealth of Gondor",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9087",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Choose a Gondor hero. Add 1 resource to that hero's resource pool.",
                Number = 75
            });
            Cards.Add(new Card() {
                ImageName = "M1744",
                Title = "Zealous Traitor",
                Id = "4823aae3-46ef-4a75-89f9-cbd3aa1b9088",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Brigand." },
                HitPoints = 2,
                Attack = 3,
                Defense = 2,
                Text = "Forced: When Zealous Traitor engages a player, that player must deal 1 damage to each ally he controls. (2 damage instead if Zealous Traitor was not optionally engaged.)",
                EncounterSet = "Brigands",
                EngagementCost = 17,
                Threat = 2,
                Number = 76
            });
        }
    }
}
