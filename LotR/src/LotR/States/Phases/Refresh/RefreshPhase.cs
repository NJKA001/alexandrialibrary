using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Refresh
{
    public class RefreshPhase
        : PhaseBase, IRefreshPhase
    {
        public RefreshPhase(IGame game, IEnumerable<ICardReadying> readyingCards)
            : base(game, PhaseCode.Refresh, PhaseStep.Refresh_Start)
        {
            if (readyingCards == null)
                throw new ArgumentNullException("readyingCards");

            this.readyingCards = readyingCards;
        }

        private readonly IEnumerable<ICardReadying> readyingCards;

        public IEnumerable<ICardReadying> GetCardsReadying()
        {
            return readyingCards;
        }
    }
}
