using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Modifiers;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public class CardInPlay<T>
        : StateBase, ICardInPlay<T>
        where T : ICard
    {
        public CardInPlay(IGame game, T card)
            : base(game, GetStateId(card))
        {
            if (card == null)
                throw new ArgumentNullException("card");

            this.Card = card;
        }

        private byte damage;
        private byte resources;

        private static Guid GetStateId(T card)
        {
            return card != null ? card.Id : Guid.Empty;
        }

        public T Card
        {
            get;
            private set;
        }

        public ICard BaseCard
        {
            get { return Card; }
        }

        public string Title
        {
            get { return Card.Title; }
        }

        public byte Damage
        {
            get { return damage; }
            set
            {
                if (damage == value)
                    return;
                
                damage = value;
                OnPropertyChanged("Damage");
            }
        }

        public byte Resources
        {
            get { return resources; }
            set
            {
                if (resources == value)
                    return;

                resources = value;
                OnPropertyChanged("Resources");
            }
        }

        public IPlayer GetController(IGame game)
        {
            return game.Players.Where(x => x.IsTheControllerOf(this)).FirstOrDefault();
        }

        public virtual bool HasEffect<TEffect>()
            where TEffect : IEffect
        {
            return Card.HasEffect<TEffect>();
        }

        public virtual bool HasTrait(Trait trait)
        {
            return Card.PrintedTraits.Contains(trait);
        }
    }
}
