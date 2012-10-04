using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IStagingArea
    {
        IDeck<IEncounterCard> EncounterDeck { get; }
        IDeck<IEncounterCard> EncounterDiscardPile { get; }
        IEncounterCard RevealedEncounterCard { get; }
        IEnumerable<IEncounterInPlay> CardsInStagingArea { get; }

        void RevealEncounterCards(byte numberOfCards);
        void CancelRevealedCard(ICancelEffect effect);

        void AddToStagingArea(IEncounterInPlay card);
        void RemoveFromStagingArea(IEncounterInPlay card);
        void AddToEncounterDiscardPile(IEnumerable<IEncounterCard> cards);
        void AddToTopOfEncounterDeck(IEnumerable<IEncounterCard> cards);
        void AddToBottomOfEncounterDeck(IEnumerable<IEncounterCard> cards);

        byte GetTotalThreat();
    }
}
