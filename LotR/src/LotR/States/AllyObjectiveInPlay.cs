using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Objectives;
using LotR.Cards.Player;
using LotR.Cards.Player.Allies;

namespace LotR.States
{
    public class AllyObjectiveInPlay
        : CardInPlay<IAllyObjectiveCard>, IObjectiveInPlay, IEncounterInPlay, IAllyInPlay, ICharacterInPlay
    {
        public AllyObjectiveInPlay(IGame game, IAllyObjectiveCard card)
            : base(game, card)
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

        ICharacterCard ICardInPlay<ICharacterCard>.Card
        {
            get { return Card as ICharacterCard; }
        }

        IAllyCard ICardInPlay<IAllyCard>.Card
        {
            get { return Card as IAllyCard; }
        }

        public bool CanPayFor(ICostlyCard costlyCard)
        {
            return false;
        }
    }
}
