﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Objectives;
using LotR.Cards.Player;

namespace LotR.States
{
    public class AttachableObjectiveInPlay
        : AttachableInPlay, IObjectiveInPlay, IEncounterInPlay
    {
        public AttachableObjectiveInPlay(IGame game, IAttachableObjectiveCard card, IAttachmentHostInPlay attachedTo)
            : base(game, card, attachedTo)
        {
        }

        private readonly IList<IEncounterInPlay> guards = new List<IEncounterInPlay>();

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

        IEncounterCard ICardInPlay<IEncounterCard>.Card
        {
            get { return Card as IEncounterCard; }
        }

        IObjectiveCard ICardInPlay<IObjectiveCard>.Card
        {
            get { return Card as IObjectiveCard; }
        }
    }
}
