using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases.Any;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public abstract class AttachmentHostInPlay
        : CardInPlay<IAttachmentHostCard>, IAttachmentHostInPlay
    {
        public AttachmentHostInPlay(IGame game, IAttachmentHostCard card)
            : base(game, card)
        {
        }

        private readonly IList<IAttachableInPlay> attachments = new List<IAttachableInPlay>();

        public IEnumerable<IAttachableInPlay> Attachments
        {
            get { return attachments; }
        }

        public void AddAttachment(IAttachableInPlay attachment)
        {
            attachments.Add(attachment);
        }

        public void RemoveAttachment(IAttachableInPlay attachment)
        {
            if (!attachments.Contains(attachment))
                return;

            attachments.Remove(attachment);
        }

        public override bool HasEffect<T>()
        {
            if (base.HasEffect<T>())
                return true;

            foreach (var attachment in attachments)
            {
                if (attachment.HasEffect<T>())
                    return true;
            }

            return false;
        }

        public override bool HasTrait(Trait trait)
        {
            if (base.HasTrait(trait))
                return true;

            var check = new CheckForTrait(game, this, trait);

            foreach (var attachment in attachments.Where(x => x.Card.HasEffect<IDuringCheckForTrait>()))
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
