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
        }
    }
}