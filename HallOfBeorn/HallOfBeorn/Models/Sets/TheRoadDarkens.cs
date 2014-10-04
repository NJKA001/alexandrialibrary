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
                IsUnique = true,
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
                Quantity = 1,
                Artist = Artist.Sebastian_Giacobino
            });
            Cards.Add(new Card()
            {
                Title = "Gandalf",
                IsUnique = true,
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
                Quantity = 1,
                Artist = Artist.Matt_Stewart
            });
            Cards.Add(new Card()
            {
                Title = "Galadriel",
                IsUnique = true,
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
                IsUnique = true,
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
                IsUnique = true,
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
                IsUnique = true,
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
                Quantity = 3,
                Artist = Artist.Diego_Gisbert_Llorens 
            });
            Cards.Add(new Card()
            {
                Title = "Gandalf's Staff",
                IsUnique = true,
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
                Quantity = 3,
                Artist = Artist.Victor_Maury
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
                Quantity = 3,
                Artist = Artist.Tiziano_Baracchi
            });
            Cards.Add(new Card()
            {
                Title = "Fellowship of the Ring",
                IsUnique = true,
                Id = "FC5068A4-CBD5-4831-A7AE-32DDB306DAD1",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                Sphere = Models.Sphere.Fellowship,
                ResourceCost = 2,
                Traits = new List<string> { "Fellowship." },
                Keywords = new List<string> { "Attach to the Ring-bearer." },
                Text = "Each hero gets +1 Willpower.\r\nForced: After a character is destroyed, discard Fellowship of the Ring.",
                Number = 10,
                Quantity = 3,
                Artist = Artist.Michael_Komarck
            });
            Cards.Add(new Card()
            {
                Title = "Sting",
                IsUnique = true,
                Id = "4CA6EBFE-C48F-43C9-867A-EDB7A50D37CE",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                Sphere = Models.Sphere.Neutral,
                ResourceCost = 2,
                Traits = new List<string> { "Artifact.", "Item.", "Weapon." },
                Keywords = new List<string> { "Attach to a Hobbit hero.", "Restricted." },
                Text = 
@"Attached hero gets +1 Willpower, +1 Attack, and +1 Defense.

Response: After attached hero exhausts to defend an attack, discard the top card of the encounter deck. Deal damage to the attacking enemy equal to the discarded card's Threat.",
                Number = 11,
                Quantity = 1,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card()
            {
                Title = "Mithril Shirt",
                IsUnique = true,
                Id = "2F44B05B-B233-414D-97AB-21EC1FE7F71D",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                ResourceCost = 1,
                Traits = new List<string> { "Artifact.", "Item.", "Armor." },
                Keywords = new List<string> { "Attach to Ring-bearer." },
                Text = "Attached hero gets +1 Defense and +1 hit point.",
                FlavorText = "\"I should feel happier if I knew you were wearing it. I have a fancy it would turn even the knives of the Black Riders...\" -Bilbo, The Fellowship of the Ring",
                Number = 12,
                Quantity = 1,
                Artist = Artist.Sara_Betsy
            });
            Cards.Add(new Card()
            {
                Title = "Glamdring",
                IsUnique = true,
                Id = "921B7E59-0CFC-4B12-A95A-CB37A49587A0",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                ResourceCost = 2,
                Traits = new List<string> { "Artifact.", "Item.", "Weapon." },
                Keywords = new List<string> { "Attach to a Warrior hero or Gandalf.", "Resticted." },
                Text = 
@"Attached character gets +2 Attack.

Response: After attached character destroys an Orc enemy, draw 1 card.",
                FlavorText = "\"...at his side was the elven-sword Glamdring, the mate of Orcrist that lay now upon the breast of Thorin under the Lonely Mountain.\" -The Fellowship of the Ring",
                Number = 13,
                Quantity = 1,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card()
            {
                Title = "Andúril",
                IsUnique = true,
                NormalizedTitle = "Anduril",
                Id = "A4E77553-5376-452A-974B-601C35FEB5BA",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                ResourceCost = 3,
                Traits = new List<string> { "Artifact.", "Item.", "Weapon." },
                Keywords = new List<string> { "Attach to a Noble hero or Aragorn.", "Restricted." },
                Text = 
@"Attached hero gets +1 Willpower, +1 Attack, and +1 Defense.

Response: After an attack in which the attached hero defended resolves, exhaust Andúril to target the enemy that just attacked. Declare attached hero as an attacker against that enemy (and resolve the attack).",
                Number = 14,
                Quantity = 1,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card()
            {
                Title = "Phial of Galadriel",
                IsUnique = true,
                Id = "5AFC42BF-3E61-4FB8-AF2C-AB40E27AFF3F",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                ResourceCost = 0,
                Traits = new List<string> { "Item.", "Gift." },
                Text = 
@"Setup: Attach to a hero in play.

Add Phial of Galadriel to the victory display and remove it from the campaign pool, to give each enemy engaged with you -4 Attack until the end of the round.",
                FlavorText = "\"May it be a light to you in dark places, when all other lights go out.\" -Galadriel, The Fellowship of the Ring",
                VictoryPoints = 3,
                Number = 15,
                Quantity = 1,
                Artist = Artist.Cynthia_Sheppard
            });
            Cards.Add(new Card()
            {
                Title = "Three Golden Hairs",
                IsUnique = true,
                Id = "19CB3A5F-5D58-41E5-846D-733261B554A6",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                ResourceCost = 0,
                Traits = new List<string> { "Item.", "Gift." },
                Text = 
@"Setup: Attach to a hero in play.

Action: Add Three Golden Hairs to the victory display and remove it from the campaign pool, to lower each player's threat by 3. Then, each player draws 3 cards.",
                FlavorText = "\"...your hands shall flow with gold, and yet over you gold shall have no dominion.\" -Galadriel, The Fellowship of the Ring",
                VictoryPoints = 3,
                Number = 16,
                Quantity = 1,
                Artist = Artist.Rovina_Cai
            });
            Cards.Add(new Card()
            {
                Title = "Lórien Rope",
                IsUnique = true,
                NormalizedTitle = "Lorien Rope",
                Id = "DF542407-56E4-4643-BD32-5BE349AA86FF",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                ResourceCost = 0,
                Traits = new List<string> { "Item.", "Gift." },
                Text =
@"Setup: Attach to a hero in play.

Action: Add Lórien Rope to the victory display, and remove it from the campaign pool to give each location in the staging area -2 Threat until the end of the phase.",
                FlavorText = "\"What a piece of luck you had that rope!\" -Frodo, The Two Towers",
                VictoryPoints = 3,
                Number = 17,
                Quantity = 1,
                Artist = Artist.Rick_Price
            });
            Cards.Add(new Card()
            {
                Title = "Leaf-wrapped Lembas",
                IsUnique = true,
                Id = "AA0E8E20-FA34-4ED6-8BF4-C4BE15CA17F2",
                ImageType = Models.ImageType.Jpg,
                CardType = CardType.Attachment,
                CampaignCardType = Models.CampaignCardType.Boon,
                ResourceCost = 0,
                Traits = new List<string> { "Item.", "Gift." },
                Text =
@"Setup: Attach to a hero in play.

Action: Add Leaf-wrapped Lembas to the victory display, and remove it from the campaign pool, to ready all heroes in play.",
                FlavorText = "Often in their hearts they thanked the lady of Lórien for the gift of lembas, for they could eat of it and find new strength even as they ran. -The Two Towers",
                VictoryPoints = 3,
                Number = 18,
                Quantity = 1,
                Artist = Artist.Owen_William_Weber
            });
            Cards.Add(new Card()
            {
                Title = "The Ring Goes South",
                CardType = Models.CardType.Campaign,
                CampaignCardType = Models.CampaignCardType.Campaign,
                SlugIncludesType = true,
                Id = "DFF44707-3468-4E8F-AB49-DF4E5E6D93FC",
                OppositeTitle = "The Lord of the Rings Part 4",
                Text = 
@"You are playing in Campaign Mode.

Setup: Each player may change hero cards he controls without incurring the +1 threat penalty. The players have earned the boon cards: Sting, Mithril Shirt, Glamdring, and Andúril. Each player chooses one and adds it to his hand. If any of those boon cards remain unchosen, shuffle them into the first player's deck.",
                FlavorText = "\"I think that this task is appointed for you, Frodo; and that if you do not find a way, no one will.\" -Elrond, The Fellowship of the Ring",
                OppositeText = "Resolution: If Lust for the Ring is attached to a hero, the players have earned that burden.",
                OppositeFlavorText = "They were just in time. Sam and Frodo were only a few steps up, and Gandalf had just begun to climb, when the groping tentacles writhed across the narrow shore and fingered the cliff-wall and the doors. One came wriggling over the threshold, glistening in the starlight. Gandalf turned and paused. If he was considering what word would close the gate again from within, there was no need. Many coiling arms seized the doors on either side, and with horrible strength, swung them round. With a shattering echo they slammed, and all light was lost. -The Fellowship of the Ring",
                EncounterSet = "The Ring Goes South",
                Number = 19,
                Quantity = 1,
                Artist = Artist.Ignacio_Bazan_Lazcano
            });
            Cards.Add(new Card()
            {
                Title = "The Council of Elrond",
                Id = "D49C15DD-1D36-4716-A754-B192CC59D5E3",
                ImageType = Models.ImageType.Jpg,
                HasSecondImage = true,
                CardType = CardType.Quest,
                StageNumber = 1,
                EncounterSet = "The Ring Goes South",
                Text = "Setup: Set Lust for the Ring, Redhorn Pass, Doors of Durin and Watcher in the Water aside, out of play. Shuffle the encounter deck.",
                FlavorText = "In the House of Elrond, the evil wound that Frodo received on Weathertop is healed and he is reunited with his uncle Bilbo. The One Ring cannot remain hidden in Rivendell for long, so Lord Elrond summons a council to decide what should be done to protect the Free Peoples of Middle-earth.",
                QuestPoints = null,
                OppositeText = "Forced: At the end of the planning phase, each player places the top card of his deck faceup in front of him, in player order, until there are a total of 4 faceup cards between the players. The first player chooses 1 faceup card to be played for 0 cost, 1 to add to its owner's hand, 1 to discard, and 1 to shuffle into its owner's deck. Then, either shuffle Lust for the Ring into the encounter deck, or raise each player's threat by 5. Advance to stage 2.",
                Number = 20,
                Quantity = 1,
                Artist = Artist.Ignacio_Bazan_Lazcano
            });
            Cards.Add(new Card()
            {
                Title = "The Nine Walkers",
                Id = "855DC3F7-9DB8-48BB-9BA1-ABDCEEAE1074",
                CardType = CardType.Quest,
                StageNumber = 2,
                EncounterSet = "The Ring Goes South",
                Text = "When Revealed: Make Redhorn Pass the active location. The first player reveals cards from the encounter deck until there is at least X Threat in the staging area. X is twice the number of players in the game.",
                FlavorText = "The Council decides The One Ring must be cast into Mount Doom. Frodo is appointed eight companions to help him compete this quest and the Company of the Ring sets out from Rivendell into Hollin on their way to Mordor...",
                QuestPoints = 8,
                OppositeText = "During the travel phase, the players must travel to a location, if able.\r\nForced: After an enemy engages a player, place 1 damage on the active location, if able.",
                OppositeFlavorText = "\"Hollin is no longer wholesome for us: it is being watched.\" -Aragorn, The Fellowship of the Ring",
                Number = 21,
                Quantity = 1,
                Artist = Artist.Matt_Bradbury
            });
            Cards.Add(new Card()
            {
                Title = "The Hunt is Up!",
                Id = "15A0614C-85DB-4FA1-8BD6-7C53CC9357A0",
                CardType = CardType.Quest,
                StageNumber = 3,
                EncounterSet = "The Ring Goes South",
                Text = "When Revealed: Each player searches the encounter deck and discard pile for an enemy and adds it to the staging area. One of those enemies must be Great Warg Chief, if able. Shuffle the encounter deck.",
                FlavorText = "Suddenly Aragorn leapt to his feet, \"How the wind howls!\" he cried. \"It is howling with wolf-voices. The Wargs have come west of the Mountains!\" -The Fellowship of the Ring",
                QuestPoints = 12,
                OppositeText = "During the travel phase, the players must travel to a location, if able.\r\nForced: After an enemy engages a player, place 1 damage on the active location, if able.",
                OppositeFlavorText = "The Fellowship is being hunted by Wargs!",
                Number = 22,
                Quantity = 1,
                Artist = Artist.Mark_Bulahao
            });
            Cards.Add(new Card()
            {
                Title = "The Gates of Moria",
                Id = "855B100F-5BE9-4651-91D8-C559DAAE5E77",
                CardType = CardType.Quest,
                StageNumber = 4,
                EncounterSet = "The Ring Goes South",
                Text = "When Revealed: Make Doors of Durin the active location. Add Watcher in the Water to the staging area. Discard all tokens from the Ring-bearer and place it (and each card attached to it) facedown under Watcher in the Water.",
                FlavorText = "Pursued by evil Wargs and foul weather, the Company of the Ring decides to enter the Mines of Moria. But as they seek the hidden gate, a foul creature seizes the Ring-bearer with one of its many groping arms...",
                QuestPoints = null,
                OppositeText = "There can be 2 active locations. During the travel phase, the players must travel to a location, if able.\r\nForced: After an enemy engages a player, place 1 damage on each active location.\r\nIf Doors of Durin is explored, the players win the game.",
                OppositeFlavorText = "The Fellowship is being hunted by Wargs!",
                Number = 23,
                Quantity = 1,
                Artist = Artist.Mark_Bulahao
            });
            Cards.Add(new Card()
            {
                Title = "Watcher in the Water",
                Id = "88F28954-B4E4-4C79-AF22-578B474BF750",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 5,
                Attack = 6,
                Defense = 5,
                HitPoints = 12,
                Traits = new List<string> { "Creature." },
                Keywords = new List<string> { "Indestructible.", "Immune to player card effects." },
                Text = 
@"Cannot leave the staging area but is considered to be engaged with the first player.

Forced: After placing the 6th damage here, the first player takes control of the Ring-bearer, exhausted with 1 damage on it.",
                EncounterSet = "The Ring Goes South",
                Number = 24,
                Quantity = 1,
                Artist = Artist.Florian_Devos
            });
            Cards.Add(new Card()
            {
                Title = "Great Warg Chief",
                Id = "17C446FC-31C8-458F-9F89-036E82A728EC",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 30,
                Threat = 4,
                Attack = 5,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string> { "Creature.", "Warg." },
                Keywords = new List<string> { "Cannot have attachments." },
                Text =
@"Forced: After Great Warg Chief enages you, discard cards from the encounter deck until a Warg enemy is discarded. Put that enemy into play engaged with you.",
                EncounterSet = "The Ring Goes South",
                Number = 25,
                Quantity = 1,
                Artist = Artist.Alvaro_Calvo_Escudero
            });
            Cards.Add(new Card()
            {
                Title = "Hound of Sauron",
                Id = "5BB5992A-37AC-406D-8448-0ABC11A3AB1C",
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 1,
                Attack = 4,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string> { "Creature.", "Warg." },
                Keywords = new List<string> { "Surge." },
                Text = "Hound of Sauron gets -5 engagement cost for each enemy in the staging area.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack (+2 Attack instead if it was engaged with round).",
                EncounterSet = "The Ring Goes South",
                Number = 26,
                Quantity = 4,
                Artist = Artist.Aurelien_Hubert
            });
            Cards.Add(new Card()
            {
                Title = "Howling Warg",
                Id = "0EB61219-35A9-4F46-8E08-8101A352477F",
                CardType = CardType.Enemy,
                EngagementCost = 38,
                Threat = 2,
                Attack = 3,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string> { "Creature.", "Warg." },
                Keywords = new List<string> { "Surge." },
                Text = "Forced: After Howling Warg attacks, place 1 damage on an active location.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. If this attack destroys a character, place 1 damage on an active location.",
                EncounterSet = "The Ring Goes South",
                Number = 27,
                Quantity = 4,
                Artist = Artist.Dylan_Pierpont
            });
            Cards.Add(new Card()
            {
                Title = "Crebain from Dunland",
                Id = "CAC621B7-8DA4-4891-A2FB-6CDA4CBF1FED",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 3,
                Attack = 1,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string> { "Creature." },
                Keywords = new List<string> { "Peril." },
                Text = 
@"When Revealed: Either exhaust a hero you control, or engaged Crebain from Dunland.

Forced: After Crebain from Dunland engages a player, reveal the top card of the encounter deck.",
                EncounterSet = "The Ring Goes South",
                Number = 28,
                Quantity = 2,
                Artist = Artist.Aurelien_Hubert
            });
            Cards.Add(new Card()
            {
                Title = "Redhorn Pass",
                IsUnique = true,
                Id = "BB618ACA-4B66-4C3C-A7F0-D973812492B4",
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 6,
                Traits = new List<string> { "Mountain." },
                Keywords = new List<string> { "Immune to player card effects." },
                Text = "Forced: When Redhorn Pass is explored, each player assigns X damage among characters he controls. X is the number of damage here.",
                FlavorText = "\"We may well be seen by watchers on that narrow path...\" -Gandalf, The Fellowship of the Ring",
                VictoryPoints = 1,
                EncounterSet = "The Ring Goes South",
                Number = 29,
                Quantity = 1,
                Artist = Artist.Katy_Grierson
            });
            Cards.Add(new Card()
            {
                Title = "Doors of Durin",
                IsUnique = true,
                Id = "FFD1FDEF-1A8F-4550-A66E-2ACC91F9EE91",
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 9,
                Traits = new List<string> { "Gate." },
                Keywords = new List<string> { "Immune to player card effects." },
                Text = 
@"Progress cannot be placed on each other active location before it can be placed here. Progress cannot be placed here unless the first player controls the Ring-bearer.
                
If there are 9 damage tokens, the players lose the game.",
                VictoryPoints = 1,
                EncounterSet = "The Ring Goes South",
                Number = 30,
                Quantity = 1,
                Artist = Artist.Nate_Abell
            });
            Cards.Add(new Card()
            {
                Title = "Tree-crowned Hill",
                Id = "",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 6,
                Traits = new List<string> { "Hills." },
                Text =
@"While Tree-crowned hill is the active location, each enemy gets -1 Threat.
                
Forced: When Tree-crowned hill is explored, the players, as a group, exhaust X characters in play. X is the number of damage here.",
                FlavorText = "For their defense in the night the Company climbed to the top of the small hill... -The Fellowship of the Ring",
                VictoryPoints = 1,
                EncounterSet = "The Ring Goes South",
                Number = 30,
                Quantity = 1,
                Artist = Artist.Ferdinand_Dumago_Ladera
            });
            Cards.Add(new Card()
            {
                Title = "Lust for the Ring",
                Id = "BF5429AB-9AD8-4BD1-AFB0-A781D0846B6B",
                CardType = CardType.Treachery,
                CampaignCardType = CampaignCardType.Burden,
                Keywords = new List<string> { "Peril.", "Surge." },
                Text = "When Revealed: Attach to a non-Fellowship hero in play. Counts as a Condition attachment with the text: \"Forced: After The One ~Ring exhausts, raise each player's threat by 1 and reduce attached hero's Willpower to 0 until the end of the round.\"",
                EncounterSet = "The Ring Goes South",
                Number = 84,
                Quantity = 1,
                Artist = Artist.Arden_Beckwith
            });
            Cards.Add(new Card()
            {
                Title = "Shadow of Fear",
                Id = "BAFE055D-1A5A-45E7-A34B-929B247D7D1A",
                CardType = CardType.Treachery,
                CampaignCardType = CampaignCardType.Burden,
                Keywords = new List<string> { "Doomed 1.", "Surge." },
                Text = "When Revealed: Treat the printed text box of each character in play as blank (except for Traits) until the end of the round.",
                Shadow = "Shadow: Defending character does not count its Defense.",
                EncounterSet = "Journey in the Dark",
                Number = 85,
                Quantity = 1,
                Artist = Artist.Mark_Behm
            });
            Cards.Add(new Card()
            {
                Title = "Pursued by the Enemy",
                Id = "9C98A2E1-01D7-4E47-B7E5-DD40E297FE04",
                CardType = CardType.Treachery,
                CampaignCardType = CampaignCardType.Burden,
                Keywords = new List<string> { "Doomed 1.", "Surge." },
                Text = "When Revealed: Each enemy engaged with a player, and not in the staging area, makes an immediate attack.",
                Shadow = "Shadow: Attacking enemy makes an additional attack after this one.",
                EncounterSet = "Journey in the Dark",
                Number = 86,
                Quantity = 1,
                Artist = Artist.Claudio_Pozas
            });
            Cards.Add(new Card()
            {
                Title = "Overcome by Grief",
                Id = "12A3B17C-F6C0-4493-B340-D5834CA375B9",
                CardType = CardType.Objective,
                CampaignCardType = CampaignCardType.Burden,
                Traits = new List<string> { "Despair." },
                Text = "Setup: Add Overcome by Grief to the staging area.\r\nForced: After a character is destroyed, if Overcome by Grief is unattached, attach to a hero. (Counts as a Condition attachment with the text: \"Forced: After a character you control is destroyed, exhaust attached hero. Until the end of the round, attached hero cannot ready.\")",
                EncounterSet = "Journey in the Dark",
                Number = 87,
                Quantity = 1,
                Artist = Artist.Guillaume_Ducos
            });
            Cards.Add(new Card()
            {
                Title = "Followed by Night",
                Id = "876E404F-F85A-4218-B7D8-6466ACA84ABC",
                CardType = CardType.Objective,
                CampaignCardType = CampaignCardType.Burden,
                Keywords = new List<string> { "Peril.", "Surge." },
                Text = "When Revealed: Put the topmost enemy of the encounter deck discard pile into play engaged with you.",
                FlavorText = "\"Quite apart from murder by night on his own account, he may put any enemy that is about on our track.\" -Aragorn, The Fellowship of the Ring",
                EncounterSet = "Breaking of the Fellowship",
                Number = 88,
                Quantity = 1,
                Artist = Artist.Gabriel_Verdon
            });
            Cards.Add(new Card()
            {
                Title = "Pursued by the Enemy",
                Id = "B8C72B5E-3A9A-4161-A167-4F8EB59FDD84",
                CardType = CardType.Treachery,
                CampaignCardType = CampaignCardType.Burden,
                Keywords = new List<string> { "Doomed 1.", "Surge." },
                Text = "When Revealed: Each enemy engaged with a player, and not in the staging area, makes an immediate attack.",
                Shadow = "Shadow: Attacking enemy makes an additional attack after this one.",
                EncounterSet = "Breaking of the Fellowship",
                Number = 89,
                Quantity = 1,
                Artist = Artist.Claudio_Pozas
            });
            Cards.Add(new Card()
            {
                Title = "Ill Fate",
                Id = "ED9A877D-E9FD-4DCC-A346-E6958CF1E8C6",
                CardType = CardType.Treachery,
                CampaignCardType = CampaignCardType.Burden,
                Keywords = new List<string> { "Peril.", "Surge." },
                Text = "When Revealed: Attach to a hero you control. (Counts as a Condition attachment with the text: \"Forced: After a character you control is destroyed, raise your threat by 2.\")",
                FlavorText = "\"Alas! An ill fate is on me this day, and all that I do goes amiss.\" -Aragorn, The Fellowship of the Ring",
                EncounterSet = "Breaking of the Fellowship",
                Number = 90,
                Quantity = 1,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card()
            {
                Title = "The One Ring",
                Id = "90F20B2B-F4AB-40D6-AB04-864ECD053790",
                IsUnique = true,
                CardType = CardType.Objective,
                Traits = new List<string> { "Artifact.", "Item.", "Ring." },
                Text =
@"Setup: The first player claims The One ~Ring and attaches it to the Ring-bearer.

Attached hero does not count against the hero limit. The first player gains control of attached hero.

If The One Ring leaves play, the players lose the game.",
                Number = 91,
                Quantity = 1,
                Artist = Artist.Mike_Nash
            });
        }
    }
}