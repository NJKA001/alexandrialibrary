using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Controllers;

namespace LotR.States.Phases.Refresh
{
    public class RefreshPhase
        : PhaseBase, IRefreshPhase
    {
        public RefreshPhase(IGame game)
            : base(game, PhaseCode.Refresh, PhaseStep.Refresh_Start)
        {
        }

        private readonly IList<ICardReadying> readyingCards = new List<ICardReadying>();

        public IEnumerable<ICardReadying> GetReadyingCards()
        {
            return readyingCards;
        }

        public void AddReadyingCard(ICardReadying card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (readyingCards.Contains(card))
                return;

            readyingCards.Add(card);
        }

        public void RemoveReadyingCard(ICardReadying card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (!readyingCards.Contains(card))
                return;

            readyingCards.Remove(card);
        }
    }
}
