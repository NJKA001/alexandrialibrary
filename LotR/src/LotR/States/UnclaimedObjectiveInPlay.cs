using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Objectives;

namespace LotR.States
{
    public class UnclaimedObjectiveInPlay
        : CardInPlay<IObjectiveCard>, IObjectiveInPlay, IEncounterInPlay
    {
        public UnclaimedObjectiveInPlay(IGame game, IObjectiveCard card)
            : base(game, card)
        {
        }

        private readonly IList<IEncounterInPlay> guards = new List<IEncounterInPlay>();

        IEncounterCard ICardInPlay<IEncounterCard>.Card
        {
            get { return Card as IEncounterCard; }
        }

        public bool HasGuards
        {
            get { return guards.Count > 0; }
        }

        public IEnumerable<IEncounterInPlay> Guards
        {
            get { return guards; }
        }

        public void AddGuard(IEncounterInPlay guard)
        {
            if (guard == null)
                throw new ArgumentNullException("guard");

            if (guards.Contains(guard))
                return;

            guards.Add(guard);
        }

        public void RemoveGuard(IEncounterInPlay guard)
        {
            if (guard == null)
                throw new ArgumentNullException("guard");

            if (!guards.Contains(guard))
                return;

            guards.Remove(guard);
        }
    }
}
