using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TheAntleredCrown : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Antlered Crown";
            Abbreviation = "TAC";
            Number = 28;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "The Ring-maker";

            Cards.Add(new Card()
            {
                Title = "Erkenbrand",
                Id = "4845B24F-B80E-4E1E-AF65-B066A9CC5285",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 10,
                Willpower = 1,
                Attack = 2,
                Defense = 3,
                HitPoints = 4,
                Traits = new List<string> { "Rohan.", "Warrior." },
                Keywords = new List<string> { "Sentinel." },
                Text = "While Erkenbrand is defending, he gains: \"Response: Deal 1 damage to Erkenbrand to cancel a shadow effect just triggered.\"",
                FlavorText = "Down from the hills leaped Erkenbrand, lord of Westfold. -The Two Towers",
                Number = 137,
                Quantity = 1,
                Artist = Artist.Sebastian_Giacobino
            });
            Cards.Add(new Card()
            {
                Title = "Warden of Helm's Deep",
                Id = "7A0EF66E-78D4-4491-9794-4C2D422153D5",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 3,
                Willpower = 0,
                Attack = 1,
                Defense = 3,
                HitPoints = 2,
                Traits = new List<string> { "Rohan.", "Warrior." },
                Keywords = new List<string> { "Sentinel."  },
                Text = "",
                FlavorText = "They now learned to their joy that Erkenbrand had left many men to hold Helm's Gate, and more had since escaped thither. -The Two Towers",
                Number = 138,
                Quantity = 3,
                Artist = Artist.Jarreau_Wimberly
            });
            Cards.Add(new Card()
            {
                Title = "The Day's Rising",
                IsUnique = true,
                Id = "8EFF9151-549A-4A15-B68F-051925DC9A25",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Traits = new List<string> { "Song." },
                Text = "Attach to a hero with sentinel.\r\nResponse: After attached hero defends against an attack and takes no damage while defending that attack, exhaust Day's Rising to add 1 resource to the attached hero's resource pool.",
                FlavorText = "Out of doubt, out of dark to the day's rising\r\nI came singing in the sun, sword unsheathing.\r\n-Éomer, The Return of the King",
                Number = 139,
                Quantity = 3,
                Artist = Artist.Jarreau_Wimberly
            });
            Cards.Add(new Card()
            {
                Title = "Captain of Gondor",
                IsUnique = true,
                Id = "B4C7608C-3DE1-4ED0-9A70-DCA3754A05ED",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 1,
                Traits = new List<string> { "Title." },
                Text = "Attach to a Warrior hero.\r\nResponse: After you optionally engage an enemy, exhaust ~Captain of ~Gondor to give attached hero +1 Attack and +1 Defense until the end of the round.",
                FlavorText = "\"Boromir it was that drove the enemy at last back from this western shore...\" -Beregond, The Return of the King",
                Number = 140,
                Quantity = 3,
                Artist = Artist.Jarreau_Wimberly
            });
            Cards.Add(new Card()
            {
                Title = "Booming Ent",
                Id = "2F6D2218-B8A6-4392-B990-10072C01B166",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                ResourceCost = 2,
                Willpower = 0,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string> { "Ent." },
                Text = "Cannot have restricted attachments. Enters play exhausted.\r\nBooming ~Ent gets +1 Attack for each damaged Ent character you control.",
                FlavorText = "\"...a man that hacks once at an Ent never gets a chance of a second blow.\" -Merry, The Two Towers",
                Number = 141,
                Quantity = 3,
                Artist = Artist.Jarreau_Wimberly
            });
            Cards.Add(new Card()
            {
                Title = "Ride Them Down",
                Id = "EE872CB0-4A05-478B-8AFE-2B2C7D62E471",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 2,
                Text = "Quest Action: Choose a non-unique enemy in the staging area. Until the end of the phase, progress that would be placed on the quest from questing successfully is instead assigned as damage to the chosen enemy. (Progress must still be placed on any active location first.)",
                FlavorText = "Like thunder they broke upon the enemy... -The Return of the King",
                Number = 142,
                Quantity = 3,
                Artist = Artist.Emile_Denis
            });
            Cards.Add(new Card()
            {
                Title = "Shadows Give Way",
                Id = "165E6952-E91F-488C-AF2A-7B386EBA06C3",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                Text = "You must use resources from 3 different heroes' pools to pay for this card.\r\nAction: Discard each shadow card from each enemy in play.",
                FlavorText = "At that moment he caught a flash of white and silver coming from the North, like a small star down on the dusky fields. -The Return of the King",
                Number = 143,
                Quantity = 3,
                Artist = Artist.Jordy_Lakiere
            });
            Cards.Add(new Card()
            {
                Title = "Don't Be Hasty!",
                Id = "DCC98174-CD80-4F70-8606-359D40475BA5",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 0,
                Text = "Response: When an encounter card is revealed but before resolving any of that card's keywords or \"when revealed\" effects, choose a character comited to the quest. Ready that character and remove it from the quest.",
                FlavorText = "\"Don't be hasty, that is my motto.\" -Treebeard, The Two Towers",
                Number = 144,
                Quantity = 3,
                Artist = Artist.Mike_Nash
            });
            Cards.Add(new Card()
            {
                Title = "Waters of Nimrodel",
                Id = "84206FDC-00CF-466D-9747-D556CF67BD95",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Keywords = new List<string> { "Doomed 3." },
                Text = "Action: Heal all damage on each character in play.",
                FlavorText = "For a moment Frodo stood near the brink and let the water flow over his tired feet. It was cold but its touch was clean, and as he went on and it mounted to his knees, he felt that the strain of travel and all weariness was washed from his limbs. -The Fellowship of the Ring",
                Number = 145,
                Quantity = 3,
                Artist = Artist.Jose_Vega
            });
            Cards.Add(new Card()
            {
                Title = "Treebeard",
                IsUnique = true,
                Id = "5B45DA78-E5E1-4F6B-A0B7-B2234A2C927D",
                CardType = CardType.Ally,
                Sphere = Sphere.Neutral,
                ResourceCost = 4,
                Willpower = 2,
                Attack = 4,
                Defense = 3,
                HitPoints = 5,
                Traits = new List<string> { "Ent." },
                Text = "Cannot have restricted attachments. Treebeard enters play exhausted and collects 1 resource each resource phase. These resources can be used to pay for Ent cards played from your hand.\r\nAction: Pay 2 resources from Treebeard's pool to ready an Ent character.",
                Number = 146,
                Quantity = 3,
                Artist = Artist.Mike_Nash
            });
        }
    }
}