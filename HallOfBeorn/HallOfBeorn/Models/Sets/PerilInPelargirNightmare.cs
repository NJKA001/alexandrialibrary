using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class PerilInPelargirNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Peril in Pelargir Nightmare";
            Abbreviation = "PiPN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2053;

            Cards.Add(new Card()
            {
                Title = "Peril in Pelargir Nightmare",
                HasSecondImage = true,
                Id = "432019A2-BF0A-493B-A13E-D50D55AB3467",
                CardType = CardType.Nightmare_Setup,
                Text =
@"You are playing Nightmare mode.

Forced; At the end of the combat phase, if Alcaron's Scroll is in the staging area, attach it to the enemy in play with the highest Defense.

Forced: At the beginning of the refresh phase, if Alcaron's Scroll is attached to an enemy, place 1 progress token on that enemy.

If at any point there are 3 or more progress tokens on any enemy, that enemy has escaped with the scroll, and the players lose the game.",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for the Peril in Pelargir scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

4x Collateral Damage
2x ~Harbor Storehouse
3x Pickpocket
1x ~Harbor Thug

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard Peril in Pelargir encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effects remain active throughout the game, which is now ready to begin.",
                EncounterSet = "Peril in Pelargir Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Dleoblack,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "Peril in Pelargir").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "Collateral Damage":
                                    card.NightmareQuantity -= 4;
                                    break;
                                case "Harbor Storehouse":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Pickpocket":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Harbor Thug":
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
                Title = "Cunning Informant",
                Id = "65120EFB-8D2B-4236-99D4-CAF749735ED4",
                CardType = CardType.Enemy,
                EngagementCost = 21,
                Threat = 3,
                Attack = 2,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string> { "Brigand." },
                Text = 
@"While Alcaron's ~Scroll is attached to a hero, Cunning Informant gets +3 Attack.

During the encounter phase, if Alcaron's ~Scroll is attached to a hero, that hero's controller is considered to have +10 threat for the purpose of engagement checks.",
                EncounterSet = "Peril in Pelargir Nightmare",
                Number = 2,
                Quantity = 2,
                Artist = Artist.Jason_Ward
            });
        }
    }
}