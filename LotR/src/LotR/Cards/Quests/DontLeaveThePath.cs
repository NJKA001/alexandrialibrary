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
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Quests
{
    public class DontLeaveThePath
        : PassageThroughMirkwoodQuestCardBase
    {
        public DontLeaveThePath()
            : base("Don't Leave The Path", 121, 3, 0)
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

            public override IEffectHandle GetHandle(IGame game)
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

                var choice = new PlayersChooseCards<IEnemyCard>("Each player must search the encounter deck and discard pile for 1 Spider of their choice", source, game.Players, 1, availableSpiders);
                return new EffectHandle(this, choice);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var spiderChoices = handle.Choice as IPlayersChooseCards<IEnemyCard>;
                if (spiderChoices == null)
                    { handle.Cancel(GetCancelledString()); return; }

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

                handle.Resolve(GetCompletedStatus());
            }
        }

        private class PassivePlayersMustDefeatUngoliantsSpawn
            : PassiveCardEffectBase, IBeforeStageDefeated, IDuringCheckGameStatus
        {
            public PassivePlayersMustDefeatUngoliantsSpawn(DontLeaveThePath source)
                : base("The Players must find and defeat Ungoliant's Spawn to win this game.", source)
            {
            }

            public void BeforeStageDefeated(IQuestStatus questStatus)
            {
                if (questStatus.Game.QuestArea.ActiveQuest == null || questStatus.Game.QuestArea.ActiveQuest.Card.Id != source.Id)
                    return;

                var ungoliantsSpawn = questStatus.Game.VictoryDisplay.Cards.Where(x => x.Title == "Ungoliant's Spawn").FirstOrDefault() as IEnemyCard;
                if (ungoliantsSpawn != null)
                    return;

                questStatus.IsStageDefeated = false;
            }

            public void DuringCheckGameStatus(IGameStatus gameStatus)
            {
                if (gameStatus.Game.QuestArea.ActiveQuest == null || gameStatus.Game.QuestArea.ActiveQuest.Card.Id != source.Id)
                    return;

                var ungoliantsSpawn = gameStatus.Game.VictoryDisplay.Cards.Where(x => x.Title == "Ungoliant's Spawn").FirstOrDefault() as IEnemyCard;

                gameStatus.IsPlayerVictory = (ungoliantsSpawn != null);
            }
        }
    }
}
