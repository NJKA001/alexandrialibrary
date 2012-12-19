using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Quest
{
    public class QuestOutcome
        : StateBase, IQuestOutcome
    {
        public QuestOutcome(IGame game)
            : base(game)
        {
        }

        private readonly IList<IEncounterInPlay> revealedEncounterCards = new List<IEncounterInPlay>();
        private readonly Dictionary<Guid, byte> threatIncreaseByPlayer = new Dictionary<Guid, byte>();

        private uint totalWillpower;
        private uint totalThreat;
        
        private bool isQuestSuccessful = false;
        private bool isQuestFailed = false;
        private bool isQuestIndeterminate = false;

        public IEnumerable<Guid> Players
        {
            get { return Game.Players.Select(x => x.StateId); }
        }

        public IEnumerable<IEncounterInPlay> RevealedEncounterCards
        {
            get { return revealedEncounterCards; }
        }

        public uint TotalWillpower
        {
            get { return totalWillpower; }
            private set
            {
                if (totalWillpower == value)
                    return;

                totalWillpower = value;
                OnPropertyChanged("TotalWillpower");
            }
        }

        public uint TotalThreat
        {
            get { return totalThreat; }
            private set
            {
                if (totalThreat == value)
                    return;

                totalThreat = value;
                OnPropertyChanged("TotalThreat");
            }
        }

        public bool IsQuestSuccessful
        {
            get { return isQuestSuccessful; }
            private set
            {
                if (isQuestSuccessful == value)
                    return;

                isQuestSuccessful = value;
                OnPropertyChanged("IsQuestSuccessful");
            }
        }

        public bool IsQuestFailed
        {
            get { return isQuestFailed; }
            private set
            {
                if (isQuestFailed == value)
                    return;

                isQuestFailed = value;
                OnPropertyChanged("IsQuestFailed");
            }
        }

        public bool IsQuestIndeterminate
        {
            get { return isQuestIndeterminate; }
            private set
            {
                if (isQuestIndeterminate == value)
                    return;

                isQuestIndeterminate = value;
                OnPropertyChanged("IsQuestIndeterminate");
            }
        }

        public byte GetThreatIncrease(Guid playerId)
        {
            return threatIncreaseByPlayer.ContainsKey(playerId) ?
                threatIncreaseByPlayer[playerId]
                : (byte)0;
        }

        public void SetThreatIncrease(Guid playerId, byte value)
        {
            threatIncreaseByPlayer[playerId] = value;
        }

        public void Resolve(uint totalWillpower, uint totalThreat)
        {
            if (isQuestFailed || isQuestSuccessful || isQuestIndeterminate)
                throw new InvalidOperationException("The quest outcome has already been resolved, you cannot resolve the same outcome twice");

            TotalWillpower = totalWillpower;
            TotalThreat = totalThreat;

            if (totalWillpower > totalThreat)
                IsQuestSuccessful = true;
            else if (totalWillpower < totalThreat)
                IsQuestFailed = true;
            else
                IsQuestIndeterminate = true;
        }

        public void EncounterCardRevealed(IEncounterInPlay encounterCardInPlay)
        {
            if (encounterCardInPlay == null)
                throw new ArgumentNullException("encounterCardInPlay");

            revealedEncounterCards.Add(encounterCardInPlay);
        }
    }
}
