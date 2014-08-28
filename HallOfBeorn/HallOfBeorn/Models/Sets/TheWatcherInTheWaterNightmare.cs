using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TheWatcherInTheWaterNightmare : CardSet
    {
        /*
        protected override void Initialize()
        {
            Name = "The Watcher in the Water Nightmare";
            Abbreviation = "TWitWN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2055;

            Cards.Add(new Card()
            {
                Title = "The Watcher in the Water Nightmare",
                Id = "",
                CardType = Models.CardType.Nightmare_Setup,
                Text =
@"You are playing in Nightmare mode.

Setup: Remove all copies of Blinding Blizzard from the encounter deck and set them aside, out of play. During this game, player cards in the victory display do not count their victory points.

Forced: When stage 2b is revealed, shuffle each set aside copy of Blinding Blizzard into the encounter deck.",
                FlavorText = "A cold wind flowed down behind them, as they turned their backs on The Watcher in the Water, and stumbled wearily down the slope. Caradhras had defeated them.\r\n-The Fellowship of the Ring",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for The Watcher in the Water scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

1x Black Uruks
3x Mountain Warg
1x Knees of the Mountain
2x Turbulent Waters
2x Warg Lair
1x The Dimrill Stair
2x Fell Voices
2x Fallen Stones
3x Mountain Goblin

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard The Watcher in the Water encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Unknown,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "The Watcher in the Water").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "Black Uruks":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Mountain Warg":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Knees of the Mountain":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Turbulent Waters":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Warg Lair":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "The Dimrill Stair":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Fell Voices":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Fallen Stones":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Mountain Goblin":
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
            Cards.Add(new Card()
            {
                Title = "",
                Id = "",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Enemy,
                EngagementCost = 20,
                Threat = 2,
                Attack = 3,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string> { "Goblin.", "Orc.", "Snow." },
                Keywords = new List<string> { "Surge." },
                Text = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 3,
                Number = 2,
                Artist = Artist.
            });
            Cards.Add(new Card()
            {
                Title = "",
                Id = "",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 2,
                Attack = 3,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string> { "Orc." },
                Text = "",
                Shadow = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 4,
                Number = 3,
                Artist = Artist.
            });
            Cards.Add(new Card()
            {
                Title = "",
                Id = "",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Enemy,
                EngagementCost = 42,
                Threat = 4,
                Attack = 6,
                Defense = 4,
                HitPoints = 10,
                Traits = new List<string> { "Giant.", "Snow." },
                Text = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 3,
                Number = 4,
                Artist = Artist.
            });
            Cards.Add(new Card()
            {
                Title = "",
                Id = "",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Location,
                Threat = 0,
                IsVariableThreat = true,
                QuestPoints = 6,
                Traits = new List<string> { "Mountain.", "Snow." },
                Text = "",
                Shadow = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 3,
                Number = 5,
                Artist = Artist.
            });
            Cards.Add(new Card()
            {
                Title = "",
                Id = "",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 3,
                Traits = new List<string> { "Mountain.", "River." },
                Text = "",
                Shadow = "",
                FlavorText = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 3,
                Number = 6,
                Artist = Artist.
            });
            Cards.Add(new Card()
            {
                Title = "",
                Id = "",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Treachery,
                Traits = new List<string> { "Snow." },
                Text = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 2,
                Number = 7,
                Artist = Artist.
            });
            Cards.Add(new Card()
            {
                Title = "",
                Id = "",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Treachery,
                Text = "",
                FlavorText = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 2,
                Number = 8,
                Artist = Artist.
            });
        }*/
    }
}