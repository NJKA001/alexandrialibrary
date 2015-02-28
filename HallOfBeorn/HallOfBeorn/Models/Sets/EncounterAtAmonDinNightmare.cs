using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class EncounterAtAmonDinNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Encounter at Amon Dîn Nightmare";
            NormalizedName = "Encounter at Amon Din Nightmare";
            Abbreviation = "EaADN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2058;

            Cards.Add(new Card()
            {
                Title = "Encounter at Amon Dîn Nightmare",
                HasSecondImage = true,
                Id = "0D3A05E0-7224-4964-AEDE-9DCD0F40DBB4",
                CardType = CardType.Nightmare_Setup,
                Text =
@"You are playing Nightmare mode.

Ghulat cannot take damage unless there is at least 15 progress tokens on Protect the Villagers.

Setup: Place 1 progress token on Rescued Villagers.

Forced: At the beginning of the quest phase, either discard 1 villager token from Rescued Villagers, or reveal an encounter card.",
                FlavorText = "\"They are burning or despoiling all that is left in the vale.\" -Éomer, The Two Towers",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for the Encounter at Amon Dîn scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

2x Burning Farmhouse
2x Gondorian Hamlet
1x Marauding ~Orc
1x ~Orc Rabble
3x ~Orc Ravager
2x Craven ~Eagle
2x Trapped Inside
2x Panicked!
2x Scourge of ~Mordor

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard the Encounter at Amon Dîn encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effects remain active throughout the game, which is now ready to begin.",
                EncounterSet = "Encounter at Amon Dîn Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Mariusz_Gandzel,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "Encounter at Amon Dîn").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "Burning Farmhouse":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Gondorian Hamlet":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Marauding Orc":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Orc Rabble":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Orc Ravager":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Craven Eagle":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Trapped Inside":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Panicked!":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Scourge of Mordor":
                                    card.NightmareQuantity -= 2;
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