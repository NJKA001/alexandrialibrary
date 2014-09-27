using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class ShadowAndFlameNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Shadow and Flame Nightmare";
            Abbreviation = "SaFN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2058;

            Cards.Add(new Card()
            {
                Title = "Shadow and Flame Nightmare",
                HasSecondImage = true,
                ImageType = Models.ImageType.Png,
                Id = "9331CC57-75DA-4FE0-9961-179232C03F02",
                CardType = CardType.Nightmare_Setup,
                Text =
@"You are playing Nightmare mode.

Setup: Shuffle the 10 encounter cards with the Attack trait into a separate 'Balrog deck.'

When Durin's Bane would be dealt a shadow card, deal it from the ~Balrog deck instead of the encounter deck. ~Shadow cards dealt from the ~Balrog deck are discarded into a separate ~Balrog discard pile.

At the beginning of each combat phase (or if the ~Balrog deck runs out of cards), shuffle the ~Balrog discard pile back into the ~Balrog deck.",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for Foundations of Stone scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

3x ~Goblin ~Scout
2x ~Goblin Tunnels
3x Stray ~Goblin
3x The Mountains' Roots
3x Ranging ~Goblin
1x Fiery Sword
1x Many Thonged Whip

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard ~Shadow and ~Flame encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.

Play Tip: This scenario contains 10 treachery cards with the Attack trait that have only a 'shadow' effect, and no 'when revealed' effect. These cards are never revealed from the encounter deck and are only ever dealt as shadow cards to Durin's Bane.",

                EncounterSet = "Shadow and Flame Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Anthony_Feliciano,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "Shadow and Flame").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "Goblin Scout":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Goblin Tunnels":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Stray Goblin":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "The Mountains' Roots":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Ranging Goblin":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Fiery Sword":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Many Thonged Whip":
                                    card.NightmareQuantity -= 1;
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