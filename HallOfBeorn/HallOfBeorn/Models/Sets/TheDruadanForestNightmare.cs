using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TheDruadanForestNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Drúadan Forest Nightmare";
            NormalizedName = "The Druadan Forest Nightmare";
            Abbreviation = "TDFN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2057;

            Cards.Add(new Card()
            {
                Title = "The Drúadan Forest Nightmare",
                HasSecondImage = true,
                Id = "2795E34C-6744-425F-8696-1A27A66E3742",
                CardType = CardType.Nightmare_Setup,
                Text = 
@"You are playing Nightmare mode.

Heroes do not collect resources during the resource phase.

Setup: Add resources to each hero's resource pool until each hero has 5 resources. Search the encounter deck for 1 copy of Gard of Poisons and add it to the staging area. Shuffle the encounter deck.

Forced: After stage 2B is revealed, each hero with fewer than 3 resources gains resources until is has 3.",
                FlavorText = "\"Remnants of an older time they be, living few and secretly, wild and wary as the beasts.\" -Elfhelm, The Return of the King",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for The Drúadan ~Forest scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

2x Lost Companion
3x Secluded Glade
1x Overgrown Trail
1x Drû-buri-Drû
1x Drúadan Elite
1x Drúadan Hunter
1x Stars in the Sky
1x Men in the ~Dark

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard The Drúadan ~Forest encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effects remain active throughout the game, which is now ready to begin.",
                EncounterSet = "The Drúadan Forest Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Smirtouille,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "The Drúadan Forest").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "Lost Companion":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Secluded Glade":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Overgrown Trail":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Drû-buri-Drû":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Drúadan Elite":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Drúadan Hunter":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Stars in the Sky":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Men in the Dark":
                                    card.NightmareQuantity -= 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    return true;
                }
            });
            Cards.Add(new Card()
            {
                Title = "Drû-buri-Drû",
                Id = "BCE2E476-6F9D-4AAA-A749-6FA7126FDE5B",
                CardType = Models.CardType.Enemy,
                EngagementCost = 1,
                Threat = 4,
                Attack = 5,
                Defense = 4,
                HitPoints = 3,
                Traits = new List<string> { "Wose." },
                Text = "Drû-buri-Drû cannot take more than 1 damage each round.\r\nWhile Drû-buri-Drû is in play, other Wose enemies get +1 Attack and +1 Defense.\r\nUnless Drû-buri-Drû is in the victory display, the players cannot win the game.",
                EncounterSet = "The Drúadan Forest Nightmare",
                Quantity = 1,
                Number = 2,
                Artist = Artist.Jason_Ward
            });
        }
    }
}