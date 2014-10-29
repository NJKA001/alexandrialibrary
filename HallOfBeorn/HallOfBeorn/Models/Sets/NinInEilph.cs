using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class NinInEilph : CardSet
    {
        protected override void Initialize()
        {
            Name = "Nin-in-Eilph";
            Abbreviation = "NiE";
            Number = 26;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "The Ring-maker";

            Cards.Add(new Card()
            {
                Title = "Mablung",
                Id = "6DF2AB16-492E-4C7F-B095-B7D46138AB11",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Models.Sphere.Tactics,
                ThreatCost = 10,
                Willpower = 2,
                Attack = 2,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string> { "Gondor.", "Ranger." },
                Text = "Response: After you engage an enemy, add 1 resource to Mablung's resource pool. (Limit once per phase.)",
                FlavorText = "But the Captains of the West were well warned by their scouts, skilled men from Henneth Annûn led by Mablung...\r\n-The Return of the King",
                Number = 84,
                Quantity = 1,
                Artist = Artist.Sebastian_Giacobino
            });
            Cards.Add(new Card()
            {
                Title = "Follow Me",
                Id = "89EE0935-04C9-4FE7-9B6F-4C4D7893ECCE",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Text = "Action: Take control of the first player token and draw 1 card.",
                FlavorText = "\"Come! I will lead you now!\"\r\n-Aragorn, The Fellowship of the Ring",
                Number = 85,
                Quantity = 3,
                Artist = Artist.Ilich_Henriquez
            });
            Cards.Add(new Card()
            {
                Title = "Tighten Our Belts",
                Id = "FCF1D840-96A9-48D4-9D18-04E95664FE2E",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 0,
                Text = "Refresh Action: Choose a player. Each hero that player controls that did not spend any resources this round gains 1 resource. Only 1 copy of Tighten Our Belts can be played by the players each round.",
                FlavorText = "There was nothing now to be done but to tighten the belts round their empty stomachs, and hoist their empty sacks and packs... -The Hobbit",
                Number = 86,
                Quantity = 3,
                Artist = Artist.Matt_Stawicki
            });
            Cards.Add(new Card()
            {
                Title = "Galadhon Archer",
                Id = "72F9C304-0C9C-4A2E-93F3-F002159C2807",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Willpower = 0,
                Attack = 2,
                Defense = 0,
                HitPoints = 1,
                Traits = new List<string> { "Silvan.", "Warrior." },
                Keywords = new List<string> { "Ranged." },
                Text = "Response: After Galadhon Archer enters play, deal 1 damage to an enemy not engaged with you.",
                FlavorText = "\"...they say that you breathe so loud that they could shoot you in the dark.\" -Legolas, The Fellowship of the Ring",
                Number = 87,
                Quantity = 3,
                Artist = Artist.Sara_K_Diesel
            });
            Cards.Add(new Card()
            {
                Title = "Bow of the Galadhrim",
                Id = "8C207E9C-517A-422B-901A-7EC8550A0FA7",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Traits = new List<string> { "Item.", "Weapon." },
                Keywords = new List<string> { "Attach to a Silvan character with the ranged keyword.", "Restricted." },
                Text = "Attached character gets +1 Attack. (+2 Attack instead if attacking an enemy not engaged with you.)",
                FlavorText = "...longer and stronger than the bows of Mirkwood, and strung with a string of elf-hair. -The Fellowship of the Ring",
                Number = 88,
                Quantity = 3,
                Artist = Artist.Sara_Biddle
            });
            Cards.Add(new Card()
            {
                Title = "Celduin Traveler",
                Id = "8FE9C322-FA5E-41D1-9152-F866F87710A2",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Willpower = 2,
                Attack = 0,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string> { "Dale.", "Scout." },
                Keywords = new List<string> { "Secrecy 2." },
                Text = "Response: After Celduin Traveler enters play, look at the top card of the encounter deck. If it is a location, you may discard it.",
                FlavorText = "In two days going they rowed right up the Long Lake and passed out into the River Running... -The Hobbit",
                Number = 89,
                Quantity = 3,
                Artist = Artist.Melanie_Maier
            });
            Cards.Add(new Card()
            {
                Title = "Island Amid Perils",
                Id = "5B8E234E-FE99-43A2-B139-04EEC71DFE48",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Text = "Action: Return a Silvan you control to your hand to reduce your threat by X where X is the printed cost of the ally returned to your hand.",
                FlavorText = "\"...we dare not by our own trust endanger our land. We live now upon an island amid many perils...\" \r\n-Haldir, The Fellowship of the Ring",
                Number = 90,
                Quantity = 3,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card()
            {
                Title = "Mirkwood Pioneer",
                Id = "76653F75-E430-4A31-BA79-D35F1AE3B904",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Willpower = 1,
                Attack = 0,
                Defense = 0,
                HitPoints = 2,
                Traits = new List<string> { "Woodman." },
                Text = "You may give Mirkwood Pioneer doomed 1 when you play it from your hand. If you do it gains: \"Response: After you play Mirkwood Pioneer, choose a card in the staging area. Until the end of the round, the chosen card does not contribute its Threat.\"",
                Number = 91,
                Quantity = 3,
                Artist = Artist.Melanie_Maier
            });
            Cards.Add(new Card()
            {
                Title = "Wingfoot",
                Id = "3BF23B7E-44B9-4973-9C07-D288FDB24FAA",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Traits = new List<string> { "Title." },
                Keywords = new List<string> { "Attach to a Ranger hero." },
                Text = "Response: After attached hero commits to a quest. name enemy, location or treachery. If a card of the named type is revealed during this quest phase, ready attached hero.",
                FlavorText = "\"Wingfoot I name you. This deed of the three friends should be sung in many a hall. Forty leagues and five you have measured ere the forth day is ended!\"\r\n-Éomer, The Two Towers",
                Number = 92,
                Quantity = 3,
                Artist = Artist.Gabriel_Verdon
            });
            Cards.Add(new Card()
            {
                Title = "Defender of the West",
                Id = "FA189ACD-3B81-4128-850D-F69DF9FDF6A4",
                CardType = CardType.Attachment,
                Sphere = Models.Sphere.Neutral,
                ResourceCost = 1,
                Traits = new List<string> { "Title." },
                Keywords = new List<string> { "Attach to a non-objective unique ally in play." },
                Text = "The first player gains control of attached ally.\r\nDamage from undefended attacks against you may be assigned to attached ally.",
                FlavorText = "\"...if by life or death I can save you, I will.\"\r\n-Aragorn, The Fellowship of the Ring",
                Number = 93,
                Quantity = 3,
                Artist = Artist.Romana_Kendelic
            });
        }
    }
}