using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Cards.Encounter.Locations;
using LotR.Effects;
using LotR.Effects.Payments;
using LotR.Games.Phases;
using LotR.Games.Phases.Setup;

namespace LotR.Cards.Quests
{
    public class FliesAndSpiders
        : QuestCardBase
    {
        public FliesAndSpiders()
            : base("Flies and Spiders", CardSet.Core, 119, 1, 0, 0)
        {
            FlavorText = "You are traveling throug Mirkwood Forest, carrying an urgent message from King Thranduil to the Lady Galadriel of Lorien. As you move along the dark trail, the spiders gather around you";
        }

        public class SetupForestSpiderAndOldForestRoadInStagingArea
            : PassiveEffect, ISetupEffect
        {
            public SetupForestSpiderAndOldForestRoadInStagingArea(FliesAndSpiders source)
                : base("Search the encounter deck for 1 copy of the Forest Spider and 1 copy of the Old Forest Road, and then add them to the staging area Then, shuffle the encounter deck.", source)
            {
            }

            public override bool PaymentAccepted(IPhaseStep step, IPayment payment)
            {
                var forestSpider = step.Phase.Round.Game.StagingArea.EncounterDeck.GetFirst(x => x.Title == "Forest Spider") as IEnemyCard;
                if (forestSpider == null)
                    return false;

                var oldForestRoad = step.Phase.Round.Game.StagingArea.EncounterDeck.GetFirst(x => x.Title == "Old Forest Road") as ILocationCard;
                if (oldForestRoad == null)
                    return false;

                step.Phase.Round.Game.StagingArea.AddToStagingArea(forestSpider);
                step.Phase.Round.Game.StagingArea.AddToStagingArea(oldForestRoad);

                return true;
            }
        }
    }
}
