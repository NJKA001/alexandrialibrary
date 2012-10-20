using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Enemies;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.Cards.Quests
{
    public class DontLeaveThePath
        : QuestCardBase
    {
        public DontLeaveThePath()
            : base("Don't Leave The Path", CardSet.Core, 121, ScenarioCode.Passage_Through_Mirkwood, new List<EncounterSet> { EncounterSet.Passage_Through_Mirkwood, EncounterSet.Dol_Guldur_Orcs, EncounterSet.Spiders_of_Mirkwood }, 3, 0, 0)
        {
            AddEffect(new WhenRevealedEachPlayerSearchesForASpider(this));
            AddEffect(new PassivePlayersMustDefeatUngoliantsSpawn(this));
        }

        private class WhenRevealedEachPlayerSearchesForASpider
            : WhenRevealedEffectBase
        {
            public WhenRevealedEachPlayerSearchesForASpider(DontLeaveThePath source)
                : base("Each player must search the encounter deck and discard pile for 1 Spider card of his choice, and add it to the staging area.", source)
            {
            }

            public override IChoice GetChoice(IGame game)
            {
                var stagingArea = game.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return null;

                var players = game.GetStates<IPlayer>();
                if (players.Count() == 0)
                    return null;

                var allSpiders = 
                    stagingArea.EncounterDeck.Cards.OfType<IEnemyCard>().Where(x => x.PrintedTraits.Contains(Trait.Spider))
                        .Concat(stagingArea.EncounterDeck.DiscardPile.OfType<IEnemyCard>().Where(x => x.PrintedTraits.Contains(Trait.Spider))).ToList();

                if (allSpiders.Count == 0)
                    return null;

                var availableSpiders = new Dictionary<Guid, IList<IEnemyCard>>();

                foreach (var player in players)
                {
                    availableSpiders.Add(player.StateId, allSpiders);
                }

                return new PlayersChooseCards<IEnemyCard>("Each player must search the encounter deck and discard pile for 1 Spider of their choice", Source, players, 1, availableSpiders);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var spiderChoices = choice as IPlayersChooseCards<IEnemyCard>;
                if (spiderChoices == null)
                    return;

                var stagingArea = game.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                foreach (var player in spiderChoices.Players)
                {
                    var spider = spiderChoices.GetChosenCards(player.StateId).FirstOrDefault();
                    if (spider == null)
                        continue;

                    if (stagingArea.EncounterDeck.Cards.Any(x => x.Id == spider.Id))
                    {
                        stagingArea.EncounterDeck.RemoveFromDeck(spider);
                        stagingArea.AddToStagingArea(spider);
                    }
                    else if (stagingArea.EncounterDeck.DiscardPile.Any(x => x.Id == spider.Id))
                    {
                        stagingArea.EncounterDeck.RemoveFromDiscardPile(new List<IEncounterCard> { spider });
                        stagingArea.AddToStagingArea(spider);
                    }
                }
            }
        }

        private class PassivePlayersMustDefeatUngoliantsSpawn
            : PassiveCardEffectBase, IBeforeStageDefeated
        {
            public PassivePlayersMustDefeatUngoliantsSpawn(DontLeaveThePath source)
                : base("The Players must find and defeat Ungoliant's Spawn to win this game.", source)
            {
            }

            public void BeforeStageDefeated(ICurrentQuestStage state)
            {
                var game = state.GetStates<IGame>().FirstOrDefault();
                if (game == null)
                    return;

                var stagingArea = game.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                var ungoliantsSpawn = stagingArea.EncounterDeck.DiscardPile.Where(x => x.Title == "Ungoliant's Spawn").FirstOrDefault() as IEnemyInPlay;
                if (ungoliantsSpawn != null)
                    return;

                state.StageIsDefeated = false;
            }
        }
    }
}
