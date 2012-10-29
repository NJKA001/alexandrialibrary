using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Cards.Encounter.Locations;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Cards.Quests
{
    public class FliesAndSpiders
        : QuestCardBase
    {
        public FliesAndSpiders()
            : base("Flies and Spiders", CardSet.Core, 119, ScenarioCode.Passage_Through_Mirkwood, new List<EncounterSet> { EncounterSet.Passage_Through_Mirkwood, EncounterSet.Dol_Guldur_Orcs, EncounterSet.Spiders_of_Mirkwood }, 1, 8, 0)
        {
            FlavorText = "You are traveling through Mirkwood Forest, carrying an urgent message from King Thranduil to the Lady Galadriel of Lorien. As you move along the dark trail, the spiders gather around you...";
            BacksideFlavorText = "The nastiest things they saw were the cobwebs; dark dense cobwebs the treads extraordinarily thick, often stretched from tree to tree, or tangles in the lower branches on either side of them. There were none stretched across the path, but whether because some magic kept it clear, or for what other reason they could not guess.\r\n-The Hobbit";

            AddEffect(new SetupForestSpiderAndOldForestRoadInStagingArea(this));
        }

        public class SetupForestSpiderAndOldForestRoadInStagingArea
            : SetupEffectBase, ISetupEffect
        {
            public SetupForestSpiderAndOldForestRoadInStagingArea(FliesAndSpiders source)
                : base("Search the encounter deck for 1 copy of the Forest Spider and 1 copy of the Old Forest Road, and then add them to the staging area. Then, shuffle the encounter deck.", source)
            {
            }

            public override void Setup(IGame game)
            {
                var forestSpider = game.StagingArea.EncounterDeck.Cards.Where(x => x.Title == "Forest Spider").FirstOrDefault() as IEnemyCard;
                if (forestSpider == null)
                    return;

                var oldForestRoad = game.StagingArea.EncounterDeck.Cards.Where(x => x.Title == "Old Forest Road").FirstOrDefault() as ILocationCard;
                if (oldForestRoad == null)
                    return;

                game.StagingArea.EncounterDeck.RemoveFromDeck(forestSpider);
                game.StagingArea.EncounterDeck.RemoveFromDeck(oldForestRoad);

                game.StagingArea.AddToStagingArea(forestSpider);
                game.StagingArea.AddToStagingArea(oldForestRoad);

                game.StagingArea.EncounterDeck.Shuffle();
            }
        }
    }
}
