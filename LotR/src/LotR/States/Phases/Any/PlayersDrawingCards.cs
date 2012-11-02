using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class PlayersDrawingCards
        : StateBase, IPlayersDrawingCards
    {
        public PlayersDrawingCards(IGame game, IDictionary<Guid, Tuple<bool, uint>> playerDrawOptions)
            : base(game)
        {
            if (playerDrawOptions == null)
                throw new ArgumentNullException("playerDrawOptions");

            this.playerDrawOptions = playerDrawOptions;
        }

        private readonly IDictionary<Guid, Tuple<bool, uint>> playerDrawOptions;

        public IEnumerable<Guid> Players
        {
            get { return playerDrawOptions.Keys; }
        }

        public uint GetNumberOfCards(Guid playerId)
        {
            return playerDrawOptions.ContainsKey(playerId) ?
                playerDrawOptions[playerId].Item2
                : (byte)0;
        }

        public bool PlayerCanDrawCards(Guid playerId)
        {
            return playerDrawOptions.ContainsKey(playerId) ?
                playerDrawOptions[playerId].Item1
                : false;
        }

        public void SetNumberOfCards(Guid playerId, uint numberOfCards)
        {
            if (!playerDrawOptions.ContainsKey(playerId))
                return;

            var existing = playerDrawOptions[playerId];
            playerDrawOptions[playerId] = new Tuple<bool, uint>(existing.Item1, numberOfCards);
        }

        public void EnabledPlayerCardDraw(Guid playerId)
        {
            if (!playerDrawOptions.ContainsKey(playerId))
                return;

            var existing = playerDrawOptions[playerId];
            playerDrawOptions[playerId] = new Tuple<bool, uint>(true, existing.Item2);
        }

        public void DisablePlayerCardDraw(Guid playerId)
        {
            if (!playerDrawOptions.ContainsKey(playerId))
                return;

            var existing = playerDrawOptions[playerId];
            playerDrawOptions[playerId] = new Tuple<bool, uint>(false, existing.Item2);
        }
    }
}
