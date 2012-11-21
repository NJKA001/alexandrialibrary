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
        
        IEncounterInPlay RevealedEncounterCard { get; }
        IEnumerable<IEncounterInPlay> CardsInStagingArea { get; }
        void ChangeEncounterDeck(IDeck<IEncounterCard> encounterDeck);

        void RevealEncounterCards(byte numberOfCards);
        void CancelRevealedCard(ICancelEffect effect);
        void RemoveRevealedCard();

        void AddToStagingArea(IEncounterCard card);
        void RemoveFromStagingArea(IEncounterInPlay card);

        void AddToEncounterDiscardPile(IEnumerable<IEncounterCard> cards);
        void AddToTopOfEncounterDeck(IEnumerable<IEncounterCard> cards);
        void AddToBottomOfEncounterDeck(IEnumerable<IEncounterCard> cards);

        byte GetTotalThreat();
    }
}
