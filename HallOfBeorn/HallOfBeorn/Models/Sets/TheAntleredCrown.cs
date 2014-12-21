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
        }
    }
}