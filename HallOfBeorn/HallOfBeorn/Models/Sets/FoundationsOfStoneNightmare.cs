using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class FoundationsOfStoneNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Foundations of Stone Nightmare";
            Abbreviation = "FoSN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2057;

            Cards.Add(new Card()
            {
                Title = "Foundations of Stone Nightmare",
                HasSecondImage = true,
                ImageType = Models.ImageType.Png,
                Id = "42D3C22B-7DAA-4C93-A7EE-2AA5927E8304",
                CardType = CardType.Nightmare_Setup,
                Text =
@"You are playing Nightmare mode.

Forced: When stage 3b is revealed, discard all Mount and Artifact cards from play. Each player must also discard allies he controls until the total printed cost of allies he controls is 6 or less.",
                FlavorText = "\"Far, far below the deepest delving of the Dwarves, the world is gnawed by nameless things. Even Sauron knows them not.\"\r\n-Gandalf, The Two Towers",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for Foundations of Stone scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

3x ~Goblin ~Scout
2x ~Goblin Swordsman
1x ~Goblin Follower
3x Burning Low
1x Branching Paths
1x Many Roads
3x ~Cave In
3x Drowned Treasury
1x Mitril Lode
4x Moria Bats

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard Foundations of Stone encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",

                EncounterSet = "Foundations of Stone Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Alvaro_Calvo_Escudero,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "Foundations of Stone").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "Goblin Scout":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Goblin Swordsman":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Goblin Follower":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Burning Low":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Branching Paths":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Many Roads":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Cave In":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Drowned Treasury":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Mithril Lode":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Moria Bats":
                                    card.NightmareQuantity -= 4;
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