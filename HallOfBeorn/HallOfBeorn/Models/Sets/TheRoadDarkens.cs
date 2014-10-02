using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TheRoadDarkens : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Road Darkens";
            Abbreviation = "RD";
            Number = 1004;
            SetType = Models.SetType.Saga_Expansion;
            Cycle = "The Lord of the Rings";

            Cards.Add(new Card()
            {
                Title = "Frodo Baggins",
                Id = "BC8E79AD-1B4A-4B63-A7CA-966E28D39403",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Hero,
                Sphere = Models.Sphere.Fellowship,
                ThreatCost = 0,
                Willpower = 2,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string> { "Hobbit.", "Ring-bearer." },
                Text = "Response: After Frodo ~Baggins exhausts to defend an attack, exhaust The One ~Ring and spend 1 Fellowship resource to target the attacking enemy. Then, this attack deals no damage and each player raises his threat by 2.",
                FlavorText = "\"But there is more about you now than appears on the surface.\" -Bilbo, The Fellowship of the Ring",
                Number = 1,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Gandalf",
                Id = "92724E6A-0F32-4996-8C58-451858A96C36",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Hero,
                Sphere = Models.Sphere.Neutral,
                ThreatCost = 14,
                Willpower = 3,
                Attack = 3,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string> { "Istari." },
                Text = "Play with the top card of your deck faceup. Once per phase, you may play the top card of your deck as if it was in your hand. When playing a card this way, Gandalf is considered to have the printed Leadership, Lore, Tactics, and Spirit icons.",
                FlavorText = "\"I am a servant of the Secret Fire, wielder of the flame of Anor.\" -The Fellowship of the Ring",
                Number = 2,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Galadriel",
                Id = "B081B837-C996-4F1C-AD97-0CCBCD3D9A8C",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Ally,
                Sphere = Models.Sphere.Leadership,
                ResourceCost = 3,
                Willpower = 3,
                Attack = 0,
                Defense = 0,
                HitPoints = 3,
                Traits = new List<string> { "Noldor.", "Noble." },
                Keywords = new List<string> { "At the end of the round, discard [Card] from play." },
                Text = "Response: After you play Galadriel from your hand, search the top 5 cards of your deck for an attachment of cost 3 or less and put it into play. Put the remaining cards back in any order.",
                Number = 3,
                Quantity = 3
            });
            Cards.Add(new Card()
            {
                Title = "Boromir",
                Id = "13C3F24D-27BE-485D-8CAB-5A639802CDDF",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Ally,
                Sphere = Models.Sphere.Tactics,
                ResourceCost = 4,
                Willpower = 1,
                Attack = 3,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string> { "Gondor.", "Warrior." },
                Text = "Boromir gets +2 Defense while defending against an enemy with an engagement cost higher than your threat.\r\nResponse: After Boromir takes any amount of damage, ready him.",
                Number = 4,
                Quantity = 3
            });
            Cards.Add(new Card()
            {
                Title = "Elrond",
                Id = "47BF974D-65B3-4AB7-AD01-B0AC41D9ADE4",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Willpower = 3,
                Attack = 2,
                Defense = 3,
                HitPoints = 3,
                Traits = new List<string> { "Noldor.", "Healer." },
                Keywords = new List<string> { "At the end of the round, discard [Card] from play." },
                Text = "Response: After Elrond enters play, choose one: heal all damage on a hero, discard a Condition attachment, or each player draws 1 card.",
                Number = 5,
                Quantity = 3
            });
            Cards.Add(new Card()
            {
                Title = "Bilbo Baggins",
                Id = "8382124A-14F3-46B4-A24C-DA520299A81B",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Ally,
                Sphere = Models.Sphere.Spirit,
                ResourceCost = 2,
                Willpower = 2,
                Attack = 0,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string> { "Hobbit." },
                Text = "Response: After Bilbo ~Baggins enters play, search your deck for a Pipe attachment and add it to your hand. Shuffle your deck.",
                FlavorText = "\"Elves may thrive on speech alone, and Dwarves endure great weariness, but I am an old Hobbit, and I miss my meal at noon.\" -The Fellowship of the Ring",
                Number = 6,
                Quantity = 3
            });
            Cards.Add(new Card()
            {
                Title = "Flame of Anor",
                Id = "AF117DE3-66FE-4E0E-A9FB-AD1618CFDEA9",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Event,
                Sphere = Models.Sphere.Neutral,
                ResourceCost = 1,
                Traits = new List<string> { "Spell." },
                Text = "Action: Add Flame of Anor to the victory display and discard the top card of your deck to ready an Istari character you control. That character gets +X Attack until the end of the phase where X is the discarded card's cost.",
                FlavorText = "\"You cannot pass!\" -Gandalf, The Fellowship of the Ring",
                VictoryPoints = 1,
                Number = 7,
                Quantity = 3
            });
            Cards.Add(new Card()
            {
                Title = "Gandalf's Staff",
                Id = "5B66215C-663A-4F31-BD5B-34EB484F8146",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                ResourceCost = 2,
                Traits = new List<string> { "Artifact.", "Item.", "Staff." },
                Keywords = new List<string> { "Attach to Gandalf.", "Restricted." },
                Text = "Action: Exhaust Gandalf's Staff to (choose one): choose a player to draw 1 card, add 1 resource to a hero's resource pool, or discard a shadow card from a non-unique enemy.",
                FlavorText = "...he held his staff aloft, and from its tip there came a feint radiance. -The Fellowship of the Ring",
                Number = 8,
                Quantity = 3
            });
            Cards.Add(new Card()
            {
                Title = "Wizard Pipe",
                Id = "DD619FB9-8CCE-4306-978A-B6E89E243A5A",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                Sphere = Models.Sphere.Neutral,
                ResourceCost = 1,
                Traits = new List<string> { "Item.", "Pipe." },
                Keywords = new List<string> { "Attach to an Istari character.", "Limit 1 per character." },
                Text = "Action: Exhaust Wizard Pipe to exchange a card in your hand with the top card of your deck.",
                Number = 9,
                Quantity = 3
            });
            Cards.Add(new Card()
            {
                Title = "Fellowship of the Ring",
                Id = "FC5068A4-CBD5-4831-A7AE-32DDB306DAD1",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                Sphere = Models.Sphere.Fellowship,
                ResourceCost = 2,
                Traits = new List<string> { "Fellowship." },
                Keywords = new List<string> { "Attach to the Ring-bearer." },
                Text = "Each hero gets +1 Willpower.\r\nForced: After a character is destroyed, discard Fellowship of the Ring.",
                Number = 10,
                Quantity = 3
            });
            Cards.Add(new Card()
            {
                Title = "Andúril",
                Id = "A4E77553-5376-452A-974B-601C35FEB5BA",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Number = 11,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Glamdring",
                Id = "921B7E59-0CFC-4B12-A95A-CB37A49587A0",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Number = 12,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Leaf-wrapped Lembas",
                Id = "AA0E8E20-FA34-4ED6-8BF4-C4BE15CA17F2",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Number = 13,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Lórien Rope",
                Id = "DF542407-56E4-4643-BD32-5BE349AA86FF",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Number = 14,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Mithril Shirt",
                Id = "2F44B05B-B233-414D-97AB-21EC1FE7F71D",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Number = 15,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Phial of Galadriel",
                Id = "5AFC42BF-3E61-4FB8-AF2C-AB40E27AFF3F",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Number = 16,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Sting",
                Id = "4CA6EBFE-C48F-43C9-867A-EDB7A50D37CE",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Number = 17,
                Quantity = 1
            });
            Cards.Add(new Card()
            {
                Title = "Three Golden Hairs",
                Id = "19CB3A5F-5D58-41E5-846D-733261B554A6",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Number = 18,
                Quantity = 1
            });
        }
    }
}