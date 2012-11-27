using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Enemies;
using LotR.Effects;

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

            private void AddSpiderToTheStagingArea(IGame game, IEffectHandle handle, IEnemyCard spider)
            {
                if (game.StagingArea.EncounterDeck.Cards.Any(x => x.Id == spider.Id))
                {
                    game.StagingArea.EncounterDeck.RemoveFromDeck(spider);
                    game.StagingArea.AddToStagingArea(spider);
                    handle.Resolve(string.Format("'{0}' added to the staging area from the encounter deck", spider.Title));
                    return;
                }
                else if (game.StagingArea.EncounterDeck.DiscardPile.Any(x => x.Id == spider.Id))
                {
                    game.StagingArea.EncounterDeck.RemoveFromDiscardPile(new List<IEncounterCard> { spider });
                    game.StagingArea.AddToStagingArea(spider);
                    handle.Resolve(string.Format("'{0}' added to the staging area from the encounter discard pile", spider.Title));
                }

                handle.Cancel(string.Format("'{0}' was not added to the staging area because it could not be located", spider.Title));
            }

            private string GetSpiderDescription(IGame game, IEnemyCard spider)
            {
                return (game.StagingArea.EncounterDeck.DiscardPile.Any(x => x.Id == spider.Id)) ?
                    string.Format("{0} (Discard Pile", spider.Title)
                    : string.Format("{0} (Encounter Deck", spider.Title);
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var spiders = 
                    game.StagingArea.EncounterDeck.Cards.OfType<IEnemyCard>().Where(x => x.PrintedTraits.Contains(Trait.Spider))
                        .Concat(game.StagingArea.EncounterDeck.DiscardPile.OfType<IEnemyCard>().Where(x => x.PrintedTraits.Contains(Trait.Spider))).ToList();

                if (spiders.Count == 0)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder("Each player must search the encounter deck and discard pile for 1 Spider card of his choice", game, game.FirstPlayer)
                        .Question("Each player must choose a Spider")
                            .Answer("Yes", 1);
                                
                foreach (var player in game.Players)
                {
                    builder.Question(string.Format("{0}, which Spider card do you want to add to the staging area?", player.Name))
                        .Answers(spiders, (item) => GetSpiderDescription(game, item), (source, handle, spider) => AddSpiderToTheStagingArea(source, handle, spider));
                }

                return new EffectHandle(this, builder.ToChoice());
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
