using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter;
using LotR.Effects;

namespace LotR.States.Areas
{
    public interface IStagingArea
        : IArea
    {
        IDeck<IEncounterCard> EncounterDeck { get; }
        
        IEncounterCard RevealedEncounterCard { get; }
        IEnumerable<IEncounterInPlay> CardsInStagingArea { get; }
        IEnumerable<IEncounterCard> ExaminedEncounterCards { get; }
        void AddExaminedEncounterCards(IEnumerable<IEncounterCard> cards);
        void RemoveExaminedEncounterCards(IEnumerable<IEncounterCard> cards);
        void ChangeEncounterDeck(IDeck<IEncounterCard> encounterDeck);

        void RevealEncounterCards(byte numberOfCards);
        void CancelRevealedCard(ICancelEffect effect);

        void AddToStagingArea(IEncounterCard card);
        void RemoveFromStagingArea(IEncounterCard card);

        void AddToEncounterDiscardPile(IEnumerable<IEncounterCard> cards);
        void AddToTopOfEncounterDeck(IEnumerable<IEncounterCard> cards);
        void AddToBottomOfEncounterDeck(IEnumerable<IEncounterCard> cards);

        byte GetTotalThreat();
    }
}
