using System;
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
