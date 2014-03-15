using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheHobbitOntheDoorstep : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Hobbit: On the Doorstep";
            Abbreviation = "THOtD";
            Number = 1002;
            SetType = Models.SetType.Saga_Expansion;
            Cycle = "The Hobbit";

            Cards.Add(new Card() {
                ImageName = "M1625",
                Title = "Balin",
                Id = "bf2cb5a8-2ec6-4366-89f6-7e3010686a85",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                IsUnique = true,
                ThreatCost = 9,
                Quantity = 1,
                Traits = new List<string>() { "Dwarf." },
                Willpower = 2,
                HitPoints = 4,
                Attack = 1,
                Defense = 2,
                Text = "Response: Pay 1 resource from Balin's resource pool to cancel a shadow effect just triggered during an attack. Then, deal the attacking enemy another shadow card. (Limit once per attack.)",
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1626",
                Title = "Bard the Bowman",
                Id = "e2cf87be-ccdc-48e9-8127-57bee67d4a0c",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                Keywords = new List<string>() { "Ranged." },
                ThreatCost = 11,
                Quantity = 1,
                Traits = new List<string>() { "Esgaroth.", " Warrior." },
                Willpower = 2,
                HitPoints = 4,
                Attack = 3,
                Defense = 2,
                Text = "When Bard the Bowman makes a ranged attack, the enemy he attacks gets -2 Defense until the end of the phase.",
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1630",
                Title = "Bifur",
                Id = "c4b959f5-92a9-4f46-8656-f9da08a5cb86",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                IsUnique = true,
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Dwarf." },
                Willpower = 1,
                HitPoints = 3,
                Attack = 2,
                Defense = 1,
                Text = "While you control at least 5 Dwarf characters, Bifur gains: 'Response: After you play Bifur from your hand, draw 2 cards.'",
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1624",
                Title = "Bilbo Baggins",
                Id = "4e61b160-2104-497f-9977-4b353e54fbb7",
                CardType = CardType.Hero,
                Sphere = Sphere.Baggins,
                IsUnique = true,
                ThreatCost = 0,
                Quantity = 1,
                Traits = new List<string>() { "Hobbit.", " Burglar." },
                Willpower = 1,
                HitPoints = 3,
                Attack = 1,
                Defense = 1,
                Text = "Bilbo ~Baggins does not count against the hero limit and cannot gain resources from non-treasure cards.\r\nThe first player gains control of Bilbo ~Baggins.\r\nAction: Spend 1 Baggins resource to search your deck for a treasure card and add it to your hand.\r\nIf Bilbo ~Baggins leaves play, the players have lost the game.",
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1642",
                Title = "Bilbo's Magic Ring",
                Id = "9dd6bf3e-e090-4ebf-a056-765c94e8a199",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to Bilbo ~Baggins." },
                ResourceCost = 0,
                Quantity = 1,
                Traits = new List<string>() { "Artifact.", "Item.", "Ring." },
                Text = "Action: Exhaust Bilbo's Magic ~Ring and raise your threat by 1 to gain 1 Baggins resource. (Limit once per round.)\r\n\r\nResponse: After Bilbo ~Baggins exhausts to defend an attack, exhaust Bilbo's Magic ~Ring and raise your threat by 3 to cancel all damage from this attack.",
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1638",
                Title = "Black Arrow",
                Id = "ae16236d-886e-4f65-9129-5ead1d373bfe",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                IsUnique = true,
                Keywords = new List<string>() { "Limit 1 per deck.", "Attach to a hero with Ranged." },
                ResourceCost = 0,
                Quantity = 3,
                Traits = new List<string>() { "Artifact.", " Item." },
                Text = "Response: After attached hero declares an attack, add Black Arrow to the victory display to give attached hero +5 Attack for this attack.",
                VictoryPoints = 1,
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1628",
                Title = "Bombur",
                Id = "66f66c2e-36e7-4d97-aebb-1ca413f9edeb",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                IsUnique = true,
                ThreatCost = 8,
                Quantity = 1,
                Traits = new List<string>() { "Dwarf." },
                Willpower = 0,
                HitPoints = 5,
                Attack = 1,
                Defense = 2,
                Text = "When counting the number of Dwarf characters you control, Bombur counts as two.",
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1633",
                Title = "Desperate Alliance",
                Id = "7465c18b-440e-4a90-8ac0-c50c0de03b5d",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Choose a hero you control. Until the end of the phase, give control of that hero and all resources in that hero's resource pool to another player. (Limit 1 per phase.)",
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1631",
                Title = "Dwalin",
                Id = "c66638a7-95ae-4913-bf11-2e8dcf8d6104",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                IsUnique = true,
                Keywords = new List<string>() { "Sentinel." },
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Dwarf." },
                Willpower = 1,
                HitPoints = 3,
                Attack = 1,
                Defense = 2,
                Text = "While you control at least 5 Dwarf characters, lower the cost to play Dwalin by 2.",
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1640",
                Title = "Expert Treasure-hunter",
                Id = "2d550e71-f2a6-41f2-8aac-4533a965eb71",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Keywords = new List<string>() { "Attach to a hero." },
                ResourceCost = 0,
                Quantity = 3,
                Traits = new List<string>() { "Skill." },
                Text = "Response: After attached hero quests successfully, name a card type and discard the top card of your deck. If the discarded card is the named type, take it into your hand.",
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1629",
                Title = "Glóin",
                NormalizedTitle = "Gloin",
                Id = "6778eb01-6b07-4dbe-87f1-e854ab548813",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                IsUnique = true,
                ResourceCost = 3,
                Quantity = 3,
                Traits = new List<string>() { "Dwarf." },
                Willpower = 2,
                HitPoints = 3,
                Attack = 1,
                Defense = 1,
                Text = "While you control at least 5 Dwarf characters, Gloin gains: 'Response: After you play Gloin from your hand, choose a hero. Add 2 resources to that hero's resource pool.'",
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1637",
                Title = "Great Yew Bow",
                Id = "833664a0-6bab-4e93-b5f1-88e7b6456569",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Keywords = new List<string>() { "Attach to a hero with the printed Ranged keyword.", " Restricted." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Item.", " Weapon." },
                Text = "Combat Action: Choose an enemy in the staging area. Exhaust Great Yew Bow and attached hero to make a ranged attack against that enemy. Declare attached hero as the attacker. No other attackers can be declared for this attack.",
                Number = 14
            });
            Cards.Add(new Card() {
                ImageName = "M1641",
                Title = "King Under the Mountain",
                Id = "9152834a-5355-42ba-9592-1a3c1940d1a8",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a Dwarf hero." },
                ResourceCost = 2,
                Quantity = 3,
                Traits = new List<string>() { "Title." },
                Text = "Action: Exhaust King Under the Mountain to look at the top 2 cards of your deck. Add 1 to your hand and discard the other.",
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1644",
                Title = "Mithril Shirt",
                Id = "12d51424-0edd-4977-9df1-5f6a7a5a96e1",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to Bilbo ~Baggins." },
                ResourceCost = 0,
                Quantity = 1,
                Traits = new List<string>() { "Artifact.", " Erebor.", " Item." },
                Text = "Attached hero gets +1 Defense and +1 hit point.",
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1627",
                Title = "Óin",
                NormalizedTitle = "Oin",
                Id = "b21a2af5-7443-4630-9503-1b334ae51c6e",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                IsUnique = true,
                ThreatCost = 8,
                Quantity = 1,
                Traits = new List<string>() { "Dwarf." },
                Willpower = 2,
                HitPoints = 4,
                Attack = 1,
                Defense = 1,
                Text = "While you control at least 5 Dwarf characters, Oin gets +1 Attack and gains the Tactics resource icon.",
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1634",
                Title = "Ravens of the Mountain",
                Id = "6d442a61-e9e9-4729-bac1-da3d76af6afa",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: Exhaust a hero you control to shuffle the encounter deck and look at its top card. Place progress tokens on the current quest equal to the revealed card's Threat. Then, put that card back on top of the encounter deck.",
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1632",
                Title = "Straight Shot",
                Id = "c2a79823-e4f9-4449-b467-5916d7e58979",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Quantity = 3,
                Text = "Action: Exhaust a Weapon attachment to choose a non-unique enemy with 0 Defense. Discard the chosen enemy.",
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1643",
                Title = "The Arkenstone",
                Id = "ae774680-c6e9-49eb-96b8-fcdebe90b49d",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a hero." },
                ResourceCost = 0,
                Quantity = 1,
                Traits = new List<string>() { "Artifact.", " Erebor.", " Item." },
                Text = "Action: Exhaust The Arkenstone and raise your threat by X to lower by X the cost of the next ally you play this phase that matches the attached hero's sphere.",
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1636",
                Title = "The Lucky Number",
                Id = "65407141-e013-4d1c-8f70-4328e707d0cc",
                CardType = CardType.Event,
                Sphere = Sphere.Baggins,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: Choose a character in play (other than Bilbo ~Baggins). Add Bilbo ~Baggins' total Willpower, Attack, and Defense to that character's Willpower, Attack, and Defense respectively until the end of the phase.",
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1645",
                Title = "Thror's Battle Axe",
                Id = "857d6dc8-ba1e-4839-8e96-a8a0136a2302",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                ResourceCost = 0,
                Quantity = 1,
                Traits = new List<string>() { "Artifact.", " Erebor.", " Item." },
                Text = "Attached hero gets +2 Attack.Response: After attached hero attacks and destroys an enemy, deal 1 damage to another enemy engaged with you.",
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1647",
                Title = "Thror's Golden Cup",
                Id = "ff7b0b9d-f8ae-4464-9db3-7205c5ae4db7",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a hero." },
                ResourceCost = 0,
                Quantity = 1,
                Traits = new List<string>() { "Artifact.", " Erebor.", " Item." },
                Text = "Action: Add Thror's Golden Cup to the victory display to draw 3 cards.",
                VictoryPoints = 3,
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1646",
                Title = "Thror's Hunting Bow",
                Id = "7adc49c0-640d-4934-89c5-312ab584b77c",
                CardType = CardType.Treasure,
                Sphere = Sphere.Neutral,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a hero.", " Restricted." },
                ResourceCost = 0,
                Quantity = 1,
                Traits = new List<string>() { "Artifact.", " Erebor.", " Item." },
                Text = "Attached hero gains Ranged.When making a ranged attack, attached hero gets +2 Attack.",
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1639",
                Title = "Thror's Key",
                Id = "f4490261-317e-4e9e-9440-a5c58dcddcbf",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                IsUnique = true,
                Keywords = new List<string>() { "Attach to a hero." },
                ResourceCost = 1,
                Quantity = 3,
                Traits = new List<string>() { "Artifact.", " Item." },
                Text = "Response: After a location is added to the staging area, attach Thror's Key to that location. While attached to a location, Thror's Key gains: 'Treat attached location's printed text box as blank, except for traits.'",
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1635",
                Title = "To me! O my kinsfolk!",
                Id = "16ae4f56-d8bb-41b8-a992-1da3b6ec1fdb",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Quantity = 3,
                Text = "Action: If you control at least 1 Dwarf hero, put a Dwarf ally from your discard pile into play under your control. Put that ally on the bottom of your deck at the end of the phase.",
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1690",
                Title = "Into Mirkwood",
                StageNumber = 1,
                Id = "1866ce74-8552-4ac5-8b0a-1e7a6f3b4338",
                CardType = CardType.Quest,
                Setup = "t",
                Quantity = 1,
                QuestPoints = 9,
                Text = "Setup: Each player may shuffle 1 copy Bilbo's Magic ~Ring into his deck. Search the encounter deck for The Spider's Glade and set it aside, out of play. Then, shuffle the encounter deck and reveal 1 encounter card per player.",
                OppositeText = "Unconscious characters cannot quest, attack, defend, collect resources, trigger abilities, be poisoned, or ready (except by effects that target unconscious character).",
                EncounterSet = "Flies and Spiders",
                Number = 67
            });
            Cards.Add(new Card() {
                ImageName = "M1691",
                Title = "Lost in the Dark",
                StageNumber = 2,
                Id = "a4ffb8cd-fd95-4b47-95d1-637bd2bd78a1",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = null,
                EncounterSet = "Flies and Spiders",
                OppositeText = "When Revealed: Reveal stage 3 and create a separate staging area for the first player use that stage. If there are not other players in the game, discard this stage and each card in the stage area. All other players advance to stage 4.",
                Number = 68
            });
            Cards.Add(new Card() {
                ImageName = "M1692",
                Title = "Captured by Spiders",
                StageNumber = 3,
                Id = "d99e07f6-7a57-4b5f-a108-13bd7ce6fa4b",
                CardType = CardType.Quest,
                Quantity = 1,
                Text = "When Revealed: Remove all resources from each hero's resource pool. Then, make each character you control (except Bilbo ~Baggins) unconscious.",
                EncounterSet = "Flies and Spiders",
                OppositeText = 
@"Unconscious characters cannot quest, attack, defend, collect resources, trigger abilities, be poisoned, or ready (except by effects that target unconscious character).

Do not pass the first player token. When this stage is complete, do not advance to stage 4 until the end of the quest phase. (Combine staging areas if necessary.)

Action: Spend 2 Baggins resources to ready and unconscious character you control.",
                Number = 69
            });
            Cards.Add(new Card() {
                ImageName = "M1693",
                Title = "Battle with the Spiders",
                StageNumber = 4,
                Id = "1ee75e10-bcc5-4243-ab28-3dc1978a50af",
                CardType = CardType.Quest,
                Quantity = 1,
                Text = "When Revealed: Add The Spider's Glade to the staging area.",
                OppositeText = 
@"Unconscious characters cannot quest, attack, defend, collect resources, trigger abilities, be poisoned, or ready (except by effects that target unconscious character).

Progress cannot be placed on this stage while The Spider's Glade is in play.

Action: Spend 2 Baggins resources to ready an unconscious character you control.

If the players defeat this stage, they have won the game and discovered the treasure card Bilbo's Magic Ring.",
                EncounterSet = "Flies and Spiders",
                Number = 70
            });
            Cards.Add(new Card() {
                ImageName = "M1694",
                Title = "A Warm Welcome",
                StageNumber = 1,
                Id = "57fc374c-ca6d-4556-9181-c1b578d7a01d",
                CardType = CardType.Quest,
                Setup = "sstt",
                Quantity = 1,
                QuestPoints = null,
                Text = "Setup: Search the encounter deck for Smaug the Golden, Smaug the Magnificent, The Lonely Mountain, and A Bare Patch. Set Smaug the Magnificent and A Bare Patch aside, out of play. Add Smaug the Golden and The Lonely Mountain to the staging area. Shuffle the five Erebor treasure cards and stack them facedown under The Lonely Mountain.",
                OppositeText =
@"Skip the Quest phase.

Forced: At the end of the round, advance to stage 2.
",
                EncounterSet = "The Lonely Mountain",
                Number = 71
            });
            Cards.Add(new Card() {
                ImageName = "M1695",
                Title = "The Burglar's Turn",
                StageNumber = 2,
                Id = "4355352b-307e-407b-98f9-3d4fb2a14a49",
                CardType = CardType.Quest,
                Quantity = 1,
                EncounterSet = "The Lonely Mountain",
                OppositeText =
@"Forced: After questing successfully, place 1 progress on Smaug the Golden. Then, he first player makes a burgle attempt. If successful, the first player takes the top card from under The Lonely Mountain into his hand. Then, the players may advance to stage 3.

Forced: After the first player makes an unsuccessful burgle attempt, Smaug the Golden attacks the first player.",
                Number = 72
            });
            Cards.Add(new Card() {
                ImageName = "M1696",
                Title = "Inside Information",
                StageNumber = 3,
                Id = "a944e2e1-56e4-461d-b703-db991b673b9b",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 20,
                Text = "When Revealed: Set Smaug the Golden aside, out of play. Add Smaug the Magnificent and A Bare Patch to the staging area.",
                OppositeText =
@"Players cannot defeat this stage while Smaug the Magnificent is in play.

Forced: If the total number of progress on this stage is greater than the total remaining hit points on Smaug the Magnificent, remove Smaug the Magnificent from play.

If the players defeat this stage, they have won the game and discovered any treasure successfully burgled from The Lonely Mountain.",
                EncounterSet = "The Lonely Mountain",
                Number = 73
            });
            Cards.Add(new Card() {
                ImageName = "M1697",
                Title = "Thieves! Fire! Murder!",
                NormalizedTitle = "Thieves Fire Murder",
                StageNumber = 4,
                Id = "240a9f5b-17dc-4182-b854-3d2dd5a33f68",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 7,
                Text = "When Revealed: Remove all progress from Smaug the Golden and set him aside, out of play. Then, add Smaug the Magnificent to the staging area.",
                OppositeText =
@"Forced: After placing the 7th progress token on this stage, remove all damage from Smaug the Magnificent and set him aside, out of play. Then, add Smaug the Golden to the staging area and reset the quest deck to stage 2B.",
                EncounterSet = "The Lonely Mountain",
                Number = 74
            });
            Cards.Add(new Card() {
                ImageName = "M1698",
                Title = "The Gathering of the Clouds",
                StageNumber = 1,
                Id = "dfd05c9f-1dc1-4af6-a22f-fe39969c6371",
                CardType = CardType.Quest,
                Setup = "s",
                Quantity = 1,
                QuestPoints = null,
                Text = "Setup: Search the encounter deck for Bolg and add it to the staging area. Then, shuffle the encounter deck and reveal 1 encounter card per player.",
                OppositeText = "When Revealed: Put stage 2, stage 3 and stage 4 into play at the same time. Then remove this stage from play.",
                EncounterSet = "The Battle of Five Armies",
                Number = 75
            });
            Cards.Add(new Card() {
                ImageName = "M1699",
                Title = "The Eastern Spur",
                StageNumber = 2,
                Id = "aa3355cb-57e1-4d8b-8a1b-9be22581a9f3",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 8,
                EncounterSet = "The Battle of Five Armies",
                OppositeText = 
@"Players cannot advance from this stage unless stage 3 and stage 4 are complete.

Forced: If there is no progress on this stage at the end of the round, each player must discard 1 random card from his hand.",
                Number = 76
            });
            Cards.Add(new Card() {
                ImageName = "M1700",
                Title = "Battle in the Dale",
                StageNumber = 3,
                Id = "81e81787-13b1-4bbf-a9ef-b8516deebbbc",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 8,
                Keywords = new List<string> { "Battle. (Characters use Attack instead of Willpower when questing here.)" },
                EncounterSet = "The Battle of Five Armies",
                OppositeText = 
@"Players cannot advance from this stage unless stage 2 and stage 4 are complete.

Forced: If there is no progress on this stage at the end of the round, the first player adds the topmost enemy in the discard pile to the staging area, if able.",
                Number = 77
            });
            Cards.Add(new Card() {
                ImageName = "M1701",
                Title = "The Defense of Ravenhill",
                StageNumber = 4,
                Id = "54ed3453-1df4-458d-bc7d-e1f7c10de237",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 8,
                Keywords = new List<string> { "Siege. (Characters use Defense instead of Willpower when questing here.)" },
                EncounterSet = "The Battle of Five Armies",
                OppositeText =
@"Players cannot advance from this stage unless stage 2 and stage 3 are complete.

Forced: If there is no progress on this stage at the end of the round, each player must discard all resources from each hero's resource pool.",
                Number = 78
            });
            Cards.Add(new Card() {
                ImageName = "M1702",
                Title = "The Clouds Burst",
                StageNumber = 5,
                Id = "82514713-7cc4-4b76-a5f2-42a69ccff76b",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = null,
                EncounterSet = "The Battle of Five Armies",
                OppositeText =
@"When Revealed: Each player must search the encounter deck and discard pile for a copy of Bodyguard of Bolg and add it to the staging area, if able. Shuffle the encounter deck.

Progress cannot be placed in this stage. While Bolg is in the staging area, it is considered to be engaged with the first player.

If Bolg is destroyed, the players win the game.",
                Number = 79
            });
            Cards.Add(new Card() {
                ImageName = "M1662",
                Title = "A Bare Patch",
                Id = "9a37677f-6bb6-4ff1-bf0e-8d0798e268c9",
                CardType = CardType.Objective,
                Quantity = 1,
                Traits = new List<string>() { "Information." },
                Text = "While attached, Smaug the Magnificent gets -3 Defense.Response: After the players quest successfully, the first player makes a burgle attempt. If successful, attach A Bare Patch to Smaug the Magnificent.",
                EncounterSet = "The Lonely Mountain",
                Number = 39
            });
            Cards.Add(new Card() {
                ImageName = "M1673",
                Title = "Belching Fire",
                Id = "6f999889-bd70-42c9-ad2a-b159a5e2f73c",
                CardType = CardType.Treachery,
                Quantity = 4,
                Text = "When Revealed: Each Dragon enemy in the staging area makes an immediate attack against the player with the highest total Willpower committed to the quest. (Do not deal a shadow card for this attack.)Burgle: The first player shuffles his deck and reveals the top card. He may discard 1 card from his hand that matches the revealed card's type and cost.",
                EncounterSet = "The Lonely Mountain",
                Number = 50
            });
            Cards.Add(new Card() {
                ImageName = "M1677",
                Title = "Bodyguard of Bolg",
                Id = "66812347-fac2-4a76-aa69-ac0861071c0d",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Bolg cannot take damage." },
                Quantity = 4,
                Traits = new List<string>() { "Goblin.", " Orc." },
                HitPoints = 5,
                Attack = 4,
                Defense = 2,
                Text = "Forced: When this enemy attacks, deal it an additional shadow card for each quest stage in play with no progress tokens on it.",
                EncounterSet = "The Battle of Five Armies",
                EngagementCost = 35,
                Threat = 2,
                Number = 54
            });
            Cards.Add(new Card() {
                ImageName = "M1676",
                Title = "Bolg",
                Id = "624d5999-1a76-4783-b4e4-d9b596242d18",
                CardType = CardType.Enemy,
                IsUnique = true,
                Keywords = new List<string>() { "Immune to player card effects." },
                Quantity = 1,
                Traits = new List<string>() { "Goblin.", " Orc." },
                HitPoints = 8,
                Attack = 6,
                Defense = 3,
                Text = "X is the number of players in the game.Cannot leave the staging area.The first Goblin revealed each round gains surge.",
                EncounterSet = "The Battle of Five Armies",
                EngagementCost = 50,
                Threat = 0,
                Number = 53
            });
            Cards.Add(new Card() {
                ImageName = "M1656",
                Title = "Caught in Webs",
                Id = "7a165c5c-caba-4af1-880e-845a124344d7",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Choose a hero to be removed from the quest and make that hero unconscious. If no hero was removed from the quest by this effect, this card gains surge.",
                EncounterSet = "Flies and Spiders",
                Number = 33
            });
            Cards.Add(new Card() {
                ImageName = "M1652",
                Title = "Crazy Cob",
                Id = "b06030a9-7ab9-48a6-b7da-5ffd8ce3a1a6",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Venom." },
                Quantity = 3,
                Traits = new List<string>() { "Creature.", " Spider." },
                HitPoints = 2,
                Attack = 3,
                Defense = 2,
                Text = "When Revealed: This enemy attacks the character with the most poison attached.",
                EncounterSet = "Flies and Spiders",
                EngagementCost = 30,
                Shadow = "Shadow: Defending character gets 1 poison.",
                Threat = 2,
                Number = 29
            });
            Cards.Add(new Card() {
                ImageName = "M1665",
                Title = "Croaking Crows",
                Id = "babeedb8-f7cd-4e8a-a5c4-fbfdd57238fb",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Surge." },
                Quantity = 3,
                Traits = new List<string>() { "Creature." },
                HitPoints = 2,
                Attack = 1,
                Defense = 1,
                Text = "Forced: When a location is revealed from the encounter deck, put 1 progress on Smaug the Golden.\r\nBurgle: The first player shuffles his deck and reveals the top card. He may discard 1 card from his hand that matches the revealed card's sphere and type.",
                EncounterSet = "The Lonely Mountain",
                EngagementCost = 33,
                Threat = 1,
                Number = 42
            });
            Cards.Add(new Card() {
                ImageName = "M1659",
                Title = "Dark Bats",
                Id = "323842f5-457f-4dd4-95b8-eb19c24664cb",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Creature." },
                HitPoints = 1,
                Attack = 2,
                Defense = 0,
                Text = "Forced: After Dark Bats damages a character, attach it to that character. (While attached, counts as a Condition attachment with the text: 'Attached character gets -1 Willpower, -1 Attack, and -1 Defense.')",
                EncounterSet = "Wilderland",
                EngagementCost = 15,
                Shadow = "Shadow: Attach this card to the defending character.",
                Threat = 2,
                Number = 36
            });
            Cards.Add(new Card() {
                ImageName = "M1669",
                Title = "Desolation of Smaug",
                Id = "435e12a1-d479-42f6-a40d-8067615ec9e1",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Wasteland." },
                Text = "While Desolation of Smaug is the active location, the first treachery card revealed each round gains surge.Burgle: The first player shuffles his deck and reveals the top card. He may discard 2 cards from his hand that match the revealed card's cost.",
                QuestPoints = 3,
                EncounterSet = "The Lonely Mountain",
                Threat = 3,
                Number = 46
            });
            Cards.Add(new Card() {
                ImageName = "M1674",
                Title = "Dragon-Spell",
                Id = "db6355c7-3edd-4573-a63c-1b6fc08bb8fa",
                CardType = CardType.Treachery,
                Quantity = 2,
                Traits = new List<string>() { "Enchantment." },
                Text = "When Revealed: Attach to the hero with the most resources without Dragon-Spell attached. (Counts as a Condition attachment with the text: 'Attached hero gets -1 Willpower for each resource in its resource pool. Forced: When attached hero spends resources, attached hero takes X damage. X is equal to the number of resources spent.')",
                EncounterSet = "The Lonely Mountain",
                Number = 51
            });
            Cards.Add(new Card() {
                ImageName = "M1654",
                Title = "Fat Spider",
                Id = "5b038648-7a06-4014-b207-a5d68c99e76d",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Venom." },
                Quantity = 4,
                Traits = new List<string>() { "Creature.", " Spider." },
                HitPoints = 6,
                Attack = 1,
                Defense = 1,
                Text = "This enemy gets +3 Attack while engaged with a player who controls at least 1 poisoned character.",
                EncounterSet = "Flies and Spiders",
                EngagementCost = 28,
                Shadow = "Shadow: Defending character gets 1 poison. (Give 2 poison to a character you control if undefended.)",
                Threat = 2,
                Number = 31
            });
            Cards.Add(new Card() {
                ImageName = "M1660",
                Title = "Fighting Among Friends",
                Id = "ffe307bf-dd8e-4df3-9355-55564137d311",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: The first player names a sphere. Then, all non-Baggins characters who do not belong to the named sphere are removed from the quest.",
                EncounterSet = "Wilderland",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. (+2 Attack instead if undefended.)",
                Number = 37
            });
            Cards.Add(new Card() {
                ImageName = "M1650",
                Title = "Forest Stream",
                Id = "818e2f2f-4070-4056-85bd-5e729ccbad30",
                CardType = CardType.Location,
                Quantity = 2,
                Traits = new List<string>() { "Forest.", " River." },
                Text = "While Forest Stream is the active location, poisoned characters cannot quest.Response: After Forest Stream leaves play as an explored location, discard 4 poison.",
                QuestPoints = 4,
                EncounterSet = "Flies and Spiders",
                Threat = 2,
                Number = 27
            });
            Cards.Add(new Card() {
                ImageName = "M1649",
                Title = "Giant Web",
                Id = "eb353293-1a80-445c-b4a9-422c83dadfd5",
                CardType = CardType.Location,
                Quantity = 4,
                Traits = new List<string>() { "Forest." },
                Text = "While Giant Web is the active location, each character gets -1 Willpower, -1 Attack, and -1 Defense for each poison it has.Response: After Giant Web leaves play as an explored location, ready an unconscious character.",
                QuestPoints = 3,
                EncounterSet = "Flies and Spiders",
                Threat = 3,
                Number = 26
            });
            Cards.Add(new Card() {
                ImageName = "M1688",
                Title = "Goblins are Upon You!",
                Id = "ac4724db-a3cc-4984-bef5-f51b0f4421f8",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Each player must search the encounter deck and discard pile for a Goblin enemy and put it into play, engaged with him. Then, shuffle the encounter deck. (This effect cannot be canceled.)",
                EncounterSet = "The Battle of Five Armies",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each Goblin enemy engaged with you.",
                Number = 65
            });
            Cards.Add(new Card() {
                ImageName = "M1667",
                Title = "Great Hall",
                Id = "1c65b78b-6b68-458f-9510-a00a9576a042",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Underground." },
                Text = "While Great Hall is the active location, it gains: 'Forced: Put 1 progress on Smaug the Golden at the end of each round.'Burgle: The first player shuffles his deck and reveals the top card. He may discard 3 cards from his hand that match the revealed card's type.",
                QuestPoints = 4,
                EncounterSet = "The Lonely Mountain",
                Threat = 4,
                Number = 44
            });
            Cards.Add(new Card() {
                ImageName = "M1679",
                Title = "Gundabad Climber",
                Id = "771c6c83-a602-4c35-92f6-5ac349c4ec8c",
                CardType = CardType.Enemy,
                Quantity = 4,
                Traits = new List<string>() { "Goblin.", " Orc." },
                HitPoints = 2,
                Attack = 2,
                Defense = 2,
                Text = "When Revealed: Remove 1 progress from every quest stage in play. (2 progress instead if 3 or more players in the game.)",
                EncounterSet = "The Battle of Five Armies",
                EngagementCost = 33,
                Threat = 1,
                Number = 56
            });
            Cards.Add(new Card() {
                ImageName = "M1678",
                Title = "Gundabad Wolf Rider",
                Id = "058a5508-6b20-4305-94ea-53d5d2f33595",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Goblin.", " Orc." },
                HitPoints = 3,
                Attack = 3,
                Defense = 0,
                Text = "Forced: When this enemy attacks, remove 1 progress from the current quest.",
                EncounterSet = "The Battle of Five Armies",
                EngagementCost = 15,
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each quest stage in play with no progress on it.",
                Threat = 1,
                Number = 55
            });
            Cards.Add(new Card() {
                ImageName = "M1687",
                Title = "Hatred Rekindled",
                Id = "87d2f0c4-8874-4910-9bb4-4f2d06c64168",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Each enemy engaged with a player makes an immediate attack. Do not deal shadow cards for attacks made by this effect. If no attacks were made by this effect, this card gains surge.",
                EncounterSet = "The Battle of Five Armies",
                Shadow = "Shadow: Attacking enemy makes an additional attack immediately after this one.",
                Number = 64
            });
            Cards.Add(new Card() {
                ImageName = "M1689",
                Title = "Heedless of Order",
                Id = "19e2470a-94c6-4f14-b824-7d2f952b60fd",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Quantity = 2,
                Text = "When Revealed: Choose a different quest to be the current quest until the end of the round, if able. If the current quest did not change as a result of this effect, this card gains Doomed 3.",
                EncounterSet = "The Battle of Five Armies",
                Number = 66
            });
            Cards.Add(new Card() {
                ImageName = "M1658",
                Title = "Hidden Path",
                Id = "3f78ed57-49cc-4886-b84f-3fe886203794",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Forest.", " Mountain." },
                Text = "While Hidden Path is the active location, locations in the staging area get -1 Threat.Travel: Spend 2 Baggins resources to travel here.",
                QuestPoints = 1,
                EncounterSet = "Wilderland",
                Threat = 3,
                Number = 35
            });
            Cards.Add(new Card() {
                ImageName = "M1672",
                Title = "Known to an Ounce",
                Id = "da46fcb3-2810-4c21-9070-43969532ef9b",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Each player discards 2 random cards from hand. Until the end of the phase, each Dragon enemy in play gets +1 Threat for each event discarded by this effect.Burgle: The first player shuffles his deck and reveals the top card. He may discard 1 card from his hand that matches the revealed card's sphere and cost.",
                EncounterSet = "The Lonely Mountain",
                Number = 49
            });
            Cards.Add(new Card() {
                ImageName = "M1653",
                Title = "Lazy Lob",
                Id = "8815a340-fc16-41c5-ac2c-d647876c2bbd",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Venom." },
                Quantity = 3,
                Traits = new List<string>() { "Creature.", " Spider." },
                HitPoints = 3,
                Attack = 3,
                Defense = 0,
                Text = "When Revealed: Each character committed to the quest gets 1 poison.",
                EncounterSet = "Flies and Spiders",
                EngagementCost = 33,
                Shadow = "Shadow: If defending character is poisoned, make that character unconscious.",
                Threat = 3,
                Number = 30
            });
            Cards.Add(new Card() {
                ImageName = "M1675",
                Title = "Misty Mountain Eagle",
                Id = "6e193811-584d-4888-a1b8-5296522b5cd1",
                CardType = CardType.Objective_Ally,
                Quantity = 1,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Willpower = 3,
                HitPoints = 2,
                Attack = 3,
                Defense = 3,
                Text = "Action: The first player may spend 2 Baggins resources to (choose one): take control of Misty ~Mountain ~Eagle, or discard this ally to discard a non-unique enemy in the staging area.",
                EncounterSet = "The Battle of Five Armies",
                Shadow = "Shadow: Deal 3 damage to attacking enemy.",
                Number = 52
            });
            Cards.Add(new Card() {
                ImageName = "M1683",
                Title = "Northern Slopes",
                Id = "29005ca1-107a-4473-8769-ce7d944e9844",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Mountain." },
                Text = "While Northern Slopes is in the staging area, each Goblin enemy gets +1 Attack and +1 Defense.While Northern Slopes is the active location, it gains: 'Forced: When a character leaves play, remove 1 progress from the current quest, if able.'",
                QuestPoints = 3,
                EncounterSet = "The Battle of Five Armies",
                Threat = 3,
                Number = 60
            });
            Cards.Add(new Card() {
                ImageName = "M1651",
                Title = "Old Tomnoddy",
                Id = "b0d31a1c-2155-4a97-a5ff-912e32a4809e",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Venom." },
                Quantity = 3,
                Traits = new List<string>() { "Creature.", " Spider." },
                HitPoints = 4,
                Attack = 5,
                Defense = 2,
                Text = "Forced: At the end of each round, each player must give a hero he controls 1 poison.",
                EncounterSet = "Flies and Spiders",
                EngagementCost = 37,
                Threat = 3,
                Number = 28
            });
            Cards.Add(new Card() {
                ImageName = "M1657",
                Title = "Poisoned by Spiders",
                Id = "31ada226-448d-48f8-9f9a-c1f5046ad041",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Each character takes damage equal to the number of poison it has. If no characters were damaged by this effect, this card gains surge.",
                EncounterSet = "Flies and Spiders",
                Shadow = "Shadow: Discard an unconscious character you control.",
                Number = 34
            });
            Cards.Add(new Card() {
                ImageName = "M1671",
                Title = "Pretending to Sleep",
                Id = "b7b62c58-7d5d-483d-807e-f4d7e2869579",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Quantity = 3,
                Text = "When Revealed: Put 1 progress token on Smaug the Golden. Then, spend X Baggins resources or advance to stage 4A. X is the number of progress tokens on Smaug the Golden.Burgle: The first player shuffles his deck and reveals the top card. He may discard 2 cards from his hand that match the revealed card's type.",
                EncounterSet = "The Lonely Mountain",
                Number = 48
            });
            Cards.Add(new Card() {
                ImageName = "M1685",
                Title = "Ravenhill",
                Id = "f388bf8b-461c-4478-8155-74775bf27714",
                CardType = CardType.Location,
                IsUnique = true,
                Keywords = new List<string>() { "Immune to player card effects." },
                Quantity = 1,
                Traits = new List<string>() { "Ruins.", " Mountain." },
                Text = "Forced: Remove 2 progress from stage 4B at the end of each round, if able.Travel: Spend 3 Baggins resources to travel here. (1 Baggins resource instead if stage 4B is the current quest.)",
                QuestPoints = 2,
                EncounterSet = "The Battle of Five Armies",
                Threat = 2,
                VictoryPoints = 3,
                Number = 62
            });
            Cards.Add(new Card() {
                ImageName = "M1682",
                Title = "Ravenous Warg",
                Id = "f29b2736-9499-4e1d-9e53-96be59f78741",
                CardType = CardType.Enemy,
                Quantity = 3,
                Traits = new List<string>() { "Creature." },
                HitPoints = 3,
                Attack = 3,
                Defense = 1,
                Text = "Forced: When Ravenous Warg attacks, the defending player must declare the character he controls with the most damage as the defender. (Even if exhausted.)",
                EncounterSet = "The Battle of Five Armies",
                EngagementCost = 30,
                Shadow = "Shadow: Defending character gets -1 Defense for each quest stage in play with no progress on it.",
                Threat = 2,
                Number = 59
            });
            Cards.Add(new Card() {
                ImageName = "M1684",
                Title = "Ruins of Dale",
                Id = "ee451e81-d7ee-40a6-9079-7f6538a26a26",
                CardType = CardType.Location,
                IsUnique = true,
                Keywords = new List<string>() { "Immune to player card effects." },
                Quantity = 1,
                Traits = new List<string>() { "Ruins.", " Valley." },
                Text = "Forced: Remove 2 progress from stage 3B at the end of each round, if able.Travel: Spend 3 Baggins resources to travel here. (1 Baggins resource instead if stage 3B is the current quest.)",
                QuestPoints = 2,
                EncounterSet = "The Battle of Five Armies",
                Threat = 2,
                VictoryPoints = 3,
                Number = 61
            });
            Cards.Add(new Card() {
                ImageName = "M1668",
                Title = "Secret Entrance",
                Id = "de8d68fc-15c4-4689-933e-1b2ca638cd78",
                CardType = CardType.Location,
                Quantity = 2,
                Traits = new List<string>() { "Mountain." },
                Text = "While Secret Entrance is the active location, The Lonely Mountain's Threat is reduced by half (round up).Travel: The first player must exhaust a hero and name a card type to travel here. Then, reveal the top card of the encounter deck. If the revealed card is not the named type, return Secret Entrance to the staging area. (The players cannot travel again this round.)",
                QuestPoints = 1,
                EncounterSet = "The Lonely Mountain",
                Threat = 1,
                Number = 45
            });
            Cards.Add(new Card() {
                ImageName = "M1663",
                Title = "Smaug the Golden",
                Id = "81b31016-bcc3-4445-8f4d-64187e99aff3",
                CardType = CardType.Enemy,
                IsUnique = true,
                Keywords = new List<string>() { "Indestructible.", " Immune to player card effects." },
                Quantity = 1,
                Traits = new List<string>() { "Dragon." },
                HitPoints = 20,
                Attack = 6,
                Defense = 9,
                Text = "Cannot be engaged.X is the number of non-Burglar characters committed to the quest.Forced: After the 6th progress token is placed here, the players advance to stage 4A.",
                EncounterSet = "The Lonely Mountain",
                EngagementCost = 50,
                Threat = 0,
                Number = 40
            });
            Cards.Add(new Card() {
                ImageName = "M1664",
                Title = "Smaug the Magnificent",
                Id = "65ad784f-d82e-4659-8d1c-53d2e4d19336",
                CardType = CardType.Enemy,
                IsUnique = true,
                Keywords = new List<string>() { "Indestructible.", " Immune to player card effects." },
                Quantity = 1,
                Traits = new List<string>() { "Dragon." },
                HitPoints = 20,
                Attack = 8,
                Defense = 8,
                Text = "Cannot leave the staging area (except by quest effect) but is considered to be engaged with the first player.Forced: If Smaug the Magnificent is dealt a shadow card with a burgle effect, he makes an additional attack immediately after this one.",
                EncounterSet = "The Lonely Mountain",
                EngagementCost = 50,
                Threat = 6,
                Number = 41
            });
            Cards.Add(new Card() {
                ImageName = "M1686",
                Title = "The Front Gate",
                Id = "19f36ec5-7ad4-4dab-80a7-4a7a6c716941",
                CardType = CardType.Location,
                IsUnique = true,
                Keywords = new List<string>() { "Immune to player card effects." },
                Quantity = 1,
                Traits = new List<string>() { "Ruins.", " Mountain." },
                Text = "Forced: Remove 2 progress from stage 2B at the end of each round, if able.Travel: Spend 3 Baggins resources to travel here. (1 Baggins resource instead if stage 2B is the current quest.)",
                QuestPoints = 2,
                EncounterSet = "The Battle of Five Armies",
                Threat = 2,
                VictoryPoints = 3,
                Number = 63
            });
            Cards.Add(new Card() {
                ImageName = "M1666",
                Title = "The Lonely Mountain",
                Id = "0554f2f9-c578-4644-9c28-b617e846b787",
                CardType = CardType.Location,
                IsUnique = true,
                Quantity = 1,
                Traits = new List<string>() { "Mountain." },
                Text = "While at least 1 card is stacked underneath, The Lonely Mountain is immune to player card effects, cannot leave the staging area, and gets -2 Threat for each card stacked under it.Action: While making a burgle attempt, the first player may spend 2 Baggins resources to take the revealed card into hand and reveal the next card of his deck.",
                QuestPoints = 4,
                EncounterSet = "The Lonely Mountain",
                Threat = 12,
                VictoryPoints = 10,
                Number = 43
            });
            Cards.Add(new Card() {
                ImageName = "M1648",
                Title = "The Spider's Glade",
                Id = "4d423c39-09c1-4c0b-b46f-6983fae4c07b",
                CardType = CardType.Location,
                IsUnique = true,
                Keywords = new List<string>() { "Immune to player card effects." },
                Quantity = 1,
                Traits = new List<string>() { "Forest." },
                Text = "Travel: Exhaust Bilbo ~Baggins to travel here.Forced: At the start of each quest phase, search the encounter deck for 1 Spider enemy and add it to the staging area. (2 Spider enemies instead if 3 or more players are at this stage.) Shuffle the encounter deck.",
                QuestPoints = 9,
                EncounterSet = "Flies and Spiders",
                Threat = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                ImageName = "M1680",
                Title = "Vanguard of Bolg",
                Id = "56e98bd5-c952-436e-b552-832031d4bb20",
                CardType = CardType.Enemy,
                Quantity = 4,
                Traits = new List<string>() { "Goblin.", " Orc." },
                HitPoints = 4,
                Attack = 1,
                Defense = 1,
                Text = "This enemy gets +1 Threat, +1 Attack, and +1 Defense for each quest stage in play with no progress on it.",
                EncounterSet = "The Battle of Five Armies",
                EngagementCost = 20,
                Shadow = "Shadow: If this attack destroys a character, remove all progress from the current quest.",
                Threat = 1,
                Number = 57
            });
            Cards.Add(new Card() {
                ImageName = "M1661",
                Title = "Weighed Down",
                Id = "10c41b09-adf9-4a78-a67a-a9a1e04d7e8d",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Until the end of the round, exhausted characters get -1 Willpower, -1 Attack, and -1 Defense.",
                EncounterSet = "Wilderland",
                Shadow = "Shadow: If the defending character is destroyed by this attack, its controller must raise his threat by 5.",
                Number = 38
            });
            Cards.Add(new Card() {
                ImageName = "M1670",
                Title = "Western Slopes",
                Id = "1e54fa6e-c2a1-4456-ac48-8912ecc4722a",
                CardType = CardType.Location,
                Quantity = 3,
                Traits = new List<string>() { "Mountain." },
                Text = "Response: When Western Slopes leaves play as an explored location (choose one): the first player draws 2 cards, or remove 1 progress token from Smaug the Golden.Burgle: The first player shuffles his deck and reveals the top card. He may discard 2 cards from his hand that match the revealed card's sphere.",
                QuestPoints = 5,
                EncounterSet = "The Lonely Mountain",
                Threat = 2,
                Number = 47
            });
            Cards.Add(new Card() {
                ImageName = "M1655",
                Title = "Wicked Spider",
                Id = "2c20ea14-e7bc-4a38-a809-8834d659adf5",
                CardType = CardType.Enemy,
                Keywords = new List<string>() { "Venom." },
                Quantity = 5,
                Traits = new List<string>() { "Creature.", " Spider." },
                HitPoints = 3,
                Attack = 4,
                Defense = 1,
                Text = "Forced: When this enemy attacks, defending player must attach 1 poison to a character he controls.",
                EncounterSet = "Flies and Spiders",
                EngagementCost = 25,
                Shadow = "Shadow: Defending character gets 2 poison.",
                Threat = 1,
                Number = 32
            });
            Cards.Add(new Card() {
                ImageName = "M1681",
                Title = "Gundabad Archer",
                Id = "fca6840a-ae90-42c7-b0cb-79ef8c7de099",
                CardType = CardType.Enemy,
                Quantity = 2,
                Traits = new List<string>() { "Goblin.", " Orc." },
                HitPoints = 3,
                Attack = 2,
                Defense = 1,
                Text = "When Revealed: Each player must deal 2 damage to a character he controls.",
                EncounterSet = "The Battle of Five Armies",
                EngagementCost = 40,
                Shadow = "Shadow: Deal 1 damage to defending character for each quest stage in play with no progress on it.",
                Threat = 2,
                Number = 58
            });
        }
    }
}
