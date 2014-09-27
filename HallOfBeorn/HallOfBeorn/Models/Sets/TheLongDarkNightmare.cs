using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TheLongDarkNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Long Dark Nightmare";
            Abbreviation = "TLDN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2056;

            Cards.Add(new Card()
            {
                Title = "The Long Dark Nightmare",
                HasSecondImage = true,
                ImageType = Models.ImageType.Png,
                Id = "A4144835-46A7-460A-8D58-CC1C25D973ED",
                CardType = CardType.Nightmare_Setup,
                Text =
@"You are playing Nightmare mode.

Durin's Greaves gains surge.

Lost: The first player must choose and discard an ally in play.",
                FlavorText = 
@"The world is grey, the mountains old,
The forge's fire is ashen cold
No harp is wrung, no hammer falls:
The darkness dwells in Durin's halls
The shadow lies upon his tomb
In Moria, in Khazad-dûm
-Gimli, The Fellowship of the Ring",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for The Long ~Dark scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

4x ~Goblin Follower
1x Branching Paths
3x Burning Low
2x Stray ~Goblin
2x Chance Encounter
2x The Mountains' Roots
1x Abandoned ~Mine
2x Fatigue
4x ~Goblin Sneak
3x Rock Adder

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard The Long ~Dark encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",

                EncounterSet = "The Long Dark Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Alvaro_Calvo_Escudero,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "The Long Dark").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "Goblin Follower":
                                    card.NightmareQuantity -= 4;
                                    break;
                                case "Branching Paths":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Burning Low":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Stray Goblin":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Chance Encounter":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "The Mountains' Roots":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Abandoned Mine":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Fatigue":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Goblin Sneak":
                                    card.NightmareQuantity -= 4;
                                    break;
                                case "Rock Adder":
                                    card.NightmareQuantity -= 3;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    return true;
                }
            });
        }
    }
}