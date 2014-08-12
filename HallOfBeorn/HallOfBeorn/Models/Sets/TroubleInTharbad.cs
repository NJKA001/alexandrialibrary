using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TroubleInTharbad : CardSet
    {
        protected override void Initialize()
        {
            Name = "Trouble in Tharbad";
            Abbreviation = "TiT";
            Number = 25;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "The Ring-maker";
            PublicSlug = "trouble-in-tharbad";

            Cards.Add(new Card()
            {
                Title = "Haldir of Lórien",
                NormalizedTitle = "Haldir of Lorien",
                ImageType = ImageType.Jpg,
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/haldir-of-lorien-trouble-in-tharbad-56.jpg",
                Id = "15DCE26E-339E-48CA-8CF8-19A5F5BF5DD1",
                CardType = CardType.Hero,
                Sphere = Models.Sphere.Lore,
                IsUnique = true,
                ThreatCost = 9,
                Willpower = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string> { "Silvan.", "Ranger.", "Scout." },
                Keywords = new List<string> { "Ranged." },
                Text = "Combat Action: If you have not engaged an enemy this round, exhaust Haldir of Lórien to declare him as an attacker (and resolve his attack) against an enemy not engaged with you. Limit once per round.",
                FlavorText = "\"We allow no strangers to spy out the secrets of the Naith...\" -The Fellowship of the Ring",
                Number = 56,
                Quantity = 1,
                Artist = Artist.Sebastian_Giacobino
            });
            Cards.Add(new Card()
            {
                Title = "Herald of Anórien",
                NormalizedTitle = "Herald of Anorien",
                ImageType = ImageType.Jpg,
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/hearld-of-anorien-trouble-in-tharbad-57.jpg",
                Id = "3B790E51-01A5-4305-A273-8694964AFDAD",
                CardType = CardType.Ally,
                Sphere = Models.Sphere.Leadership,
                ResourceCost = 2,
                Willpower = 1,
                Attack = 0,
                Defense = 1,
                HitPoints = 1,
                Traits = new List<string> { "Gondor." },
                Text = "You may give Herald of Anórien doomed 2 when you play it from your hand. If you do, it gains: \"Response: After you play Herald of Anórien, choose a player. That player may put into play 1 ally from his hand with a printed cost 2 or lower.\"",
                FlavorText = "\"Send the heralds forth! Let them summon all who dwell nigh!\" -Théoden, The Two Towers",
                Number = 57,
                Quantity = 3,
                Artist = Artist.Adam_Lane
            });
            Cards.Add(new Card()
            {
                Title = "O Lórien!",
                NormalizedTitle = "O Lorien!",
                ImageType = ImageType.Jpg,
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/o-lorien-trouble-in-tharbad-58.jpg",
                Id = "879112FB-689B-45AA-BD4A-DE7FDB1D31AB",
                CardType = CardType.Attachment,
                Sphere = Models.Sphere.Leadership,
                ResourceCost = 1,
                Traits = new List<string> { "Song." },
                Keywords = new List<string> { "Attach to a hero." },
                Text = "Action: Exhaust O Lorien! to lower the cost of the next Silvan ally played this phase by 1 (to a minimum of 1).",
                FlavorText = "\"I sang of leaves, of leaves of gold, and\r\nleaves of gold there grew:\r\nOf wind I sang, a wind there came and in the\r\nbranches blew.\" -Galadriel, The Fellowship of the Ring",
                Number = 58,
                Quantity = 3,
                Artist = Artist.Carolina_Eade
            });
            Cards.Add(new Card()
            {
                Title = "Gwaihir",
                ImageType = Models.ImageType.Jpg,
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/gwaihir-trouble-in-tharbad-59.jpg",
                Id = "AAD1F231-94C0-4AF7-BD97-F1FA4A04A561",
                CardType = Models.CardType.Ally,
                Sphere = Models.Sphere.Tactics,
                ResourceCost = 5,
                Willpower = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string> { "Creature.", "Eagle." },
                Keywords = new List<string> { "Cannot have restricted attachments." },
                Text = "Response: After Gwaihir enters play, search your discard pile for an Eagle ally and put it into play under your control. At the end of the round, if that ally is still in play, add it to your hand.",
                FlavorText = "\"The North Wind blows, but we shall outfly it.\" -Gwaihir, The Return of the King",
                Number = 59,
                Quantity = 3,
                Artist = Artist.Jake_Murray
            });
            Cards.Add(new Card()
            {
                Title = "Pursuing the Enemy",
                ImageType = ImageType.Jpg,
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/pursuing-the-enemy-trouble-in-tharbad-60.jpg",
                Id = "29FFD74F-E9E7-43EA-A259-7A22420B94BF",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Text = "Action: Return a Silvan ally you control to your hand to choose a player. Deal 1 damage to each enemy engaged with that player.",
                FlavorText = "The marauding orcs had been waylaid and almost all destroyed; the remnant had fled westward towards the mountains, and were being pursued. -The Fellowship of the Ring",
                Number = 60,
                Quantity = 3,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card()
            {
                Title = "Courage Awakened",
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/courage-awakened-trouble-in-tharbad-61.jpg",
                Id = "EE30C4EE-61A8-4A34-A516-B95CB2672F23",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Text = "Action: Choose a hero. That hero gets +2 Willpower until the end of the phase. Then, if your threat is 20 or less and this is the first time you played a copy of Courage Awakened this round, return this card to your hand instead of discarding it.",
                FlavorText = "But the courage that had been awakened in him was now too strong: he could not leave his friends so easily. -The Fellowship of the Ring",
                Number = 61,
                Quantity = 3,
                Artist = Artist.Romana_Kendelic
            });
            Cards.Add(new Card()
            {
                Title = "Free to Choose",
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/free-to-choose-trouble-in-tharbad-62.jpg",
                Id = "B200B774-99B8-43F6-BFBA-2EF9063270D3",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Text = "Response: After your threat is raised by an encounter card effect, reduce your threat by an equal amount.",
                FlavorText = "Suddenly he was aware of himself again. Frodo, neither the Voice nor the Eye; free to choose, and with one remaining instant in which to do so. -The Fellowship of the Ring",
                Number = 62,
                Quantity = 3,
                Artist = Artist.Romana_Kendelic
            });
            Cards.Add(new Card()
            {
                Title = "Galadhrim Minstrel",
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/galadhrim-minstrel-trouble-in-tharbad-63.jpg",
                Id = "7894DA0E-F6DF-4BFF-8128-B02E66371728",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Willpower = 1,
                Attack = 0,
                Defense = 0,
                HitPoints = 1,
                Traits = new List<string> { "Silvan.", "Minstrel." },
                Text = "Response: After Galadhrim Minstrel enters play, search the top five cards of your deck for an event card and add it to your hand. Shuffle the other cards back into your deck.",
                FlavorText = "...the language was that of Elven-song and spoke of things little known on Middle-earth. -The Fellowship of the Ring",
                Number = 63,
                Quantity = 3,
                Artist = Artist.Arden_Beckwith
            });
            Cards.Add(new Card()
            {
                Title = "Lembas",
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/lembas-trouble-in-tharbad-64.jpg",
                Id = "81A13DA7-EFE9-483F-B53D-17EAD498D5B2",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Traits = new List<string> { "Item." },
                Keywords = new List<string> { "Play only if you control a Noldor or Silvan hero.", "Attach to a hero." },
                Text = "Action: Discard Lembas to ready attached hero and heal 3 damage from it.",
                FlavorText = "\"...it is more strengthing than any food made by Men, and it is more pleasant than cram, by all accounts. -Lórien Elf, The Fellowship of the Ring",
                Number = 64,
                Quantity = 3,
                Artist = Artist.Sara_Biddle
            });
            Cards.Add(new Card()
            {
                Title = "Defender of the Naith",
                PublicImageURL = "http://www.cardgamedb.com/forums/uploads/lotr/defender-of-naith-trouble-in-tharbad-65.jpg",
                Id = "EEB587DB-D1F6-49A5-91A3-136F14B97320",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                ResourceCost = 3,
                Willpower = 0,
                Attack = 1,
                Defense = 2,
                HitPoints = 2,
                Traits = new List<string> { "Silvan.", "Warrior." },
                Keywords = new List<string> { "Sentinel." },
                Text = "Response: After a Silvan ally you control leaves play, ready Defender of the Naith.",
                FlavorText = "\"We have been keeping watch on the rivers, ever since we saw a great troop of Orcs going North toward Moria...\" -Haldir, The Fellowship of the Ring",
                Number = 65,
                Quantity = 3,
                Artist = Artist.Christine_Griffin
            });
        }
    }
}