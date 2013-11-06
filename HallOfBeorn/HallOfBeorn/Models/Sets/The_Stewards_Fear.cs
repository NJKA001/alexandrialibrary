using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheStewardsFear : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Steward's Fear";

            Cards.Add(new Card() {
                Title = "Hirluin the Fair",
                Id = "85699571-9cc3-4ccc-98b0-0e1664893ef5",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 8,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Outlands." },
                Text = "You may use resources from Hirluin the Fair's resource pool to pay for Outlands ally cards of any sphere.",
                Keywords = new List<string>() { "Ranged." },
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Warrior of Lossarnach",
                Id = "2e84d805-365c-47ea-9c4f-e3f75daeb9a6",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 2,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Outlands." },
                Text = "Each Outlands character you control gets +1 Defense.",
                Quantity = 3,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Gaining Strength",
                Id = "add42229-f5d1-4c39-a5dd-ec00ad7d0d76",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 0,
                Text = "Action: Discard 2 resources from your hero's resource pool to add 3 resources to that hero's resource pool.",
                Quantity = 3,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Knights of the Swan",
                Id = "c00844d6-1c3c-4e8c-a46c-8de15b8408df",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Attack = 0,
                Defense = 0,
                Willpower = 0,
                HitPoints = 1,
                Traits = new List<string>() { "Outlands." },
                Text = "Each Outlands character you control gets +1 Attack.",
                Quantity = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Gondorian Shield",
                Id = "09134509-191b-4903-b4b5-5e650f8143c1",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Traits = new List<string>() { "Armor.", " Item." },
                Text = "Attached hero gains +1 Defense. (+2 Defense instead if attached hero has the Gondor trait.)",
                Keywords = new List<string>() { "Attach to a hero.", " Restricted.", " Limit 1 per hero." },
                Quantity = 3,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Ethir Swordsman",
                Id = "1c149f93-9e3b-42fa-878c-80b29563a283",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Outlands." },
                Text = "Each Outlands character you control gets +1 Willpower.",
                Quantity = 3,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Ring of Barahir",
                Id = "0ef9e385-b7e5-4676-a690-2dd8031aa8c8",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                IsUnique = true,
                Traits = new List<string>() { "Artifact.", " Item.", " Ring." },
                Text = "Attached hero gets +1 hit point for each Artifact attachment attached to it.If attached hero is Aragorn, he also gains a Lore resource icon.",
                Keywords = new List<string>() { "Attach to a hero." },
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Anfalas Herdsman",
                Id = "4cb4741d-c9d8-4d62-ab4f-50fa80c59fbb",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Attack = 0,
                Defense = 0,
                Willpower = 0,
                HitPoints = 1,
                Traits = new List<string>() { "Outlands." },
                Text = "Each Outlands character you control gets +1 hit point.",
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Mithrandir's Advice",
                Id = "3c47d373-62bc-4321-9dc1-9b86b2046f68",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Text = "Action: Draw 1 card for each hero you control with a printed Lore resource icon.",
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "A Good Harvest",
                Id = "e02f317e-736c-40a0-8d73-45509fce9ef8",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                ResourceCost = 0,
                Text = "Action: Name a sphere. Until the end of the phase, you can spend resources of any sphere when paying for cards that belong to the named sphere.",
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Underworld Dissident",
                Id = "eb9f723d-0f74-4d10-89e7-b046313a1e91",
                CardType = CardType.Enemy,
                EngagementCost = 25,
                Attack = 2,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Brigand." },
                Text = "Underworld Dissident gets +1 Attack for each Underworld location in play.",
                Shadow = "Shadow: If this attack is undefended, remove 1 token from the current quest.",
                Threat = 3,
                EncounterSet = "The Steward's Fear",
                Quantity = 5,
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Daughter of Berúthiel",
                Id = "408148e1-3391-4734-aeac-3e3a7b69c1c1",
                CardType = CardType.Enemy,
                EngagementCost = 10,
                IsUnique = true,
                Attack = 5,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string>() { "Gondor.", " Villain." },
                Text = "Daughter of Berúthiel cannot be optionally engaged.Forced: After Daughter of Berúthiel attacks, return her to the staging area. The engaged player may raise his threat by 4 to prevent this effect.",
                Threat = 3,
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "The Hand of Castamir",
                Id = "21f520fd-5efe-41f9-9f02-7a65e36eb31f",
                CardType = CardType.Enemy,
                EngagementCost = 30,
                IsUnique = true,
                Attack = 4,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string>() { "Gondor.", " Villain." },
                Text = "Forced: After The Hand of Castamir attacks, reveal the top card of the encounter deck, resolve its effects, and discard it.Forced: After a treachery card is revealed from the encounter deck, The Hand of Castamir makes an immediate attack against the first player.",
                Threat = 3,
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Telemnar's Bane",
                Id = "c460b9c0-0239-4a74-99cb-3c302429db44",
                CardType = CardType.Enemy,
                EngagementCost = 20,
                IsUnique = true,
                Attack = 3,
                Defense = 2,
                HitPoints = 7,
                Traits = new List<string>() { "Gondor.", " Villain." },
                Text = "Forced: When Telemnar's Bane attacks, discard the top 3 cards of each player's deck.Forced: After a player plays an event card, Telemnar's Bane makes an immediate attack against the first player.",
                Threat = 3,
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Sewers",
                Id = "03587572-2760-48b5-9b08-e01ccdc9788f",
                CardType = CardType.Location,
                Traits = new List<string>() { "Underworld.", " City." },
                Text = "When Revealed: Place the top card of the underworld deck facedown underneath the active location, if able.",
                Keywords = new List<string>() { "Underworld 1." },
                Threat = 3,
                QuestPoints = 3,
                EncounterSet = "The Steward's Fear",
                Quantity = 3,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Roots of Mindolluin",
                Id = "5e39ebfc-287a-49d5-9369-49dbfb0fd665",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "Underworld." },
                Text = "Each location in the staging area gets +2 quest points.",
                Keywords = new List<string>() { "Underworld 1." },
                Threat = 2,
                QuestPoints = 5,
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Storehouse",
                Id = "b89a5a01-88c0-434a-a061-1654c6f84477",
                CardType = CardType.Location,
                Traits = new List<string>() { "Underworld.", " City." },
                Text = " ",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. (+2 Attack instead if undefended.)",
                Keywords = new List<string>() { "Underworld 1." },
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "The Steward's Fear",
                Quantity = 2,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Houses of the Dead",
                Id = "35055e3e-d806-4c21-ad37-8177d70be329",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "Underworld.", " City." },
                Text = "Forced: After Houses of the Dead becomes the active location, exhaust all characters.",
                Keywords = new List<string>() { "Underworld 2." },
                Threat = 4,
                QuestPoints = 4,
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "The Fourth Star",
                Id = "b2acb892-ab3b-4037-8751-c036f9f6f8bd",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "City." },
                Text = "Response: When The Fourth Star leaves play as an explored location, each player may draw 1 card.",
                Keywords = new List<string>() { "Underworld X.", " X is the number of players in the game." },
                Threat = 3,
                QuestPoints = 5,
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "Unwelcome Discovery",
                Id = "fc4f9f69-7870-4449-b89b-cd7b4dc158d0",
                CardType = CardType.Treachery,
                Text = "When Revealed: Reveal 1 card from the underworld deck and add it to the staging area. If it is a Clue card, discard it instead.",
                Keywords = new List<string>() { "Surge." },
                EncounterSet = "The Steward's Fear",
                Quantity = 2,
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "A Knife in the Back",
                Id = "c4218a3e-6a6b-47a0-88c5-cdbcda3fb5eb",
                CardType = CardType.Treachery,
                Text = "When Revealed: Randomly select 1 ally controlled by the first player, if able. The first player deals X damage to one of his heroes, where X is that ally's Attack. Then discard that ally. If no ally is discarded by this effect, A Knife in the Back gains surge.",
                EncounterSet = "The Steward's Fear",
                Quantity = 3,
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "False Lead",
                Id = "fe7448a2-e2e2-4e81-95f4-2ac585160433",
                CardType = CardType.Treachery,
                Text = "When Revealed: End the quest phase without resolving the quest.",
                Shadow = "Shadow: If this attack destroys a character, remove all tokens from the current quest.",
                EncounterSet = "The Steward's Fear",
                Quantity = 2,
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "A Scrap of History",
                Id = "a8494d53-c8a4-4f74-b3e1-2e4183aeea2a",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Clue." },
                Text = "Action: Exhaust a hero to claim this objective, add it to the victory display, and place 1 resource token on the current quest.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                VictoryPoints = 1,
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "Secret Map",
                Id = "dce8ff38-bcc7-49b4-ba27-ae3e2cac1d32",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Clue." },
                Text = "Action: Exhaust a hero to claim this objective and attach it to that hero.Action: If attached to a hero, add Secret Map to the victory display to place 3 progress tokens on the active location.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                VictoryPoints = 1,
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "A Prisoner",
                Id = "23c33968-01db-4f79-94bd-45ea1e0d50d0",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Clue." },
                Text = "Action: Exhaust a hero to claim this objective, add it to the victory display, and place 2 resource tokens on the current quest.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                VictoryPoints = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                Title = "Unholy Alliance",
                Id = "2bf5a4d1-9d74-4e1a-8725-cf4a3a47dde9",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Plot." },
                Text = "Reveal 1 additional card from the encounter deck and add it to the staging area during the staging step each round.If a hero is destroyed by an enemy attack, the players have lost the game.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 26
            });
            Cards.Add(new Card() {
                Title = "Up in Flames",
                Id = "1dcf16e8-8882-4b61-93ac-a7da643c52bd",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Plot." },
                Text = "Forced: At the end of the round, place 1 resource token on Up in Flames and discard the top X cards of each player's deck. X is the number of resource tokens on Up in Flames.If any player has no cards in his deck, the players have lost the game.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 27
            });
            Cards.Add(new Card() {
                Title = "Poisoned Counsels",
                Id = "4bcc7bde-4223-411e-a612-9459a1502743",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Plot." },
                Text = "At the end of each round, raise each player's threat by 2.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 28
            });
            Cards.Add(new Card() {
                Title = "Conspiracy - 1A",
                Id = "4e12f20f-aec3-4311-a656-10a517fd97fe",
                CardType = CardType.Quest,
                Setup = "ttttttt-",
                Text = "Setup: Create the underworld deck. Remove Roots of Mindolluin from the encounter deck and set it aside, out of play. Shuffle all Villain cards and randomly set one aside, out of play, without looking at it. Remove the others from the game. Repeat this with all Plot cards.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 29
            });
            Cards.Add(new Card() {
                Title = "The Grand Design - 2A",
                Id = "21ee317d-9aca-43be-9782-521539827cb8",
                CardType = CardType.Quest,
                Text = "When Revealed: Reveal the set aside Plot card and add it to the staging area.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 30
            });
            Cards.Add(new Card() {
                Title = "The Confrontation - 3A",
                Id = "8239e81a-f779-4c7c-b586-cd5ad732f061",
                CardType = CardType.Quest,
                Text = "When Revealed: Reveal the set aside Villain card and add it to the staging area.",
                EncounterSet = "The Steward's Fear",
                Quantity = 1,
                Number = 31
            });
        }
    }
}
