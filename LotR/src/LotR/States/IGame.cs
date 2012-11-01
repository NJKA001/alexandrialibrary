using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States.Areas;
using LotR.States.Phases;
using LotR.States.Phases.Combat;
using LotR.States.Phases.Quest;

namespace LotR.States
{
    public interface IGame
        : ISource, INotifyPropertyChanged
    {
        byte CurrentRound { get; }
        IPhase CurrentPhase { get; }
        IQuestArea QuestArea { get; }
        IStagingArea StagingArea { get; }
        IVictoryDisplay VictoryDisplay { get; }
        IEnumerable<IPlayer> Players { get; }
        IPlayer FirstPlayer { get; }

        void AddEffect(IEffect effect);
        void ResolveEffect(IEffect effect, IPayment payment, IChoice choice);
        void Setup(IQuestArea questArea, IEnumerable<IPlayer> players);

        T GetCardInPlay<T>(Guid cardId) where T : class, ICardInPlay;
    }
}
