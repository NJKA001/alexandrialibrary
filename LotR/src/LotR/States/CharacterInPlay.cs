using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects.Phases.Any;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public abstract class CharacterInPlay<T>
        : PlayerCardInPlay<T>, ICharacterInPlay, IAttachmentHostInPlay
        where T : IPlayerCard, IAttachmentHostCard
    {
        public CharacterInPlay(IGame game, T card)
            : base(game, card)
        {
        }

        private readonly IDictionary<Guid, IAttachableInPlay> attachments = new Dictionary<Guid, IAttachableInPlay>();

        ICharacterCard ICardInPlay<ICharacterCard>.Card
        {
            get { return Card as ICharacterCard; }
        }

        IAttachmentHostCard ICardInPlay<IAttachmentHostCard>.Card
        {
            get { return Card as IAttachmentHostCard; }
        }

        public IEnumerable<IAttachableInPlay> Attachments
        {
            get { return attachments.Values; }
        }

        public void AddAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (attachments.ContainsKey(attachment.StateId))
                return;

            attachments.Add(attachment.StateId, attachment);
        }

        public void RemoveAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (!attachments.ContainsKey(attachment.StateId))
                return;

            attachments.Remove(attachment.StateId);
        }

        public virtual bool CanPayFor(ICostlyCard costlyCard)
        {
            if (costlyCard == null)
                throw new ArgumentNullException("costlyCard");

            if (Card.HasEffect<IDuringCheckForResourceMatch>())
            {
                var check = new CheckForResourceMatch(game, costlyCard);

                foreach (var effect in Card.Text.Effects.OfType<IDuringCheckForResourceMatch>())
                {
                    effect.DuringCheckForResourceMatch(check);
                }

                return check.IsResourceMatch;
            }

            return false;
        }

        public override bool HasEffect<TEffect>()
            //where TEffect : IEffect
        {
            if (base.HasEffect<TEffect>())
                return true;

            foreach (var attachment in attachments.Values)
            {
                if (attachment.HasEffect<TEffect>())
                    return true;
            }

            return false;
        }

        public override bool HasTrait(Trait trait)
        {
            if (base.HasTrait(trait))
                return true;

            var check = new CheckForTrait(game, this, trait);

            foreach (var attachment in attachments.Values.Where(x => x.Card.HasEffect<IDuringCheckForTrait>()))
            {
                foreach (var effect in attachment.Card.Text.Effects.OfType<IDuringCheckForTrait>())
                {
                    effect.DuringCheckForTrait(check);
                }
            }

            return check.HasTrait;
        }
    }
}
