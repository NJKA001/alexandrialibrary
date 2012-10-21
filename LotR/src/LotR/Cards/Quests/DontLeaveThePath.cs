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
                var allSpiders = 
                    game.StagingArea.EncounterDeck.Cards.OfType<IEnemyCard>().Where(x => x.PrintedTraits.Contains(Trait.Spider))
                        .Concat(game.StagingArea.EncounterDeck.DiscardPile.OfType<IEnemyCard>().Where(x => x.PrintedTraits.Contains(Trait.Spider))).ToList();

                if (allSpiders.Count == 0)
                    return null;

                var availableSpiders = new Dictionary<Guid, IList<IEnemyCard>>();

                foreach (var player in game.Players)
                {
                    availableSpiders.Add(player.StateId, allSpiders);
                }

                return new PlayersChooseCards<IEnemyCard>("Each player must search the encounter deck and discard pile for 1 Spider of their choice", Source, game.Players, 1, availableSpiders);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var spiderChoices = choice as IPlayersChooseCards<IEnemyCard>;
                if (spiderChoices == null)
                    return;

                foreach (var player in spiderChoices.Players)
                {
                    var spider = spiderChoices.GetChosenCards(player.StateId).FirstOrDefault();
                    if (spider == null)
                        continue;

                    if (game.StagingArea.EncounterDeck.Cards.Any(x => x.Id == spider.Id))
                    {
                        game.StagingArea.EncounterDeck.RemoveFromDeck(spider);
                        game.StagingArea.AddToStagingArea(spider);
                    }
                    else if (game.StagingArea.EncounterDeck.DiscardPile.Any(x => x.Id == spider.Id))
                    {
                        game.StagingArea.EncounterDeck.RemoveFromDiscardPile(new List<IEncounterCard> { spider });
                        game.StagingArea.AddToStagingArea(spider);
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

            public void BeforeStageDefeated(IQuestStage currentQuestStage)
            {
                var ungoliantsSpawn = currentQuestStage.Game.StagingArea.EncounterDeck.DiscardPile.Where(x => x.Title == "Ungoliant's Spawn").FirstOrDefault() as IEnemyInPlay;
                if (ungoliantsSpawn != null)
                    return;

                currentQuestStage.StageIsDefeated = false;
            }
        }
    }
}
