using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR
{
    public abstract class CardInPlayBase<T>
        : ICardInPlay
        where T : ICard
    {
        protected CardInPlayBase(T card)
        {
            this.Card = card;
        }

        private readonly IList<IAttachmentInPlay> attachments = new List<IAttachmentInPlay>();
        private readonly IList<IModifier> modifiers = new List<IModifier>();

        private byte resources;

        public Guid CardId
        {
            get { return Card.Id; }
        }

        public ICard Card
        {
            get;
            private set;
        }

        public IEnumerable<IAttachmentInPlay> Attachments
        {
            get { return attachments; }
        }

        public void AddAttachment(IAttachmentInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            attachments.Add(attachment);
        }

        public void RemoveAttachment(IAttachmentInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (!attachments.Contains(attachment))
                return;

            attachments.Remove(attachment);
        }

        public byte Resources
        {
            get { return resources; }
            private set
            {
                if (value < 0)
                    resources = 0;
                else
                    resources = value;
            }
        }

        public void AddResources(byte value)
        {
            Resources += value;
        }

        public void RemoveResources(byte value)
        {
            Resources -= value;
        }

        public IEnumerable<IModifier> Modifiers
        {
            get { return modifiers; }
        }

        public void AddModifier(IModifier modifier)
        {
            if (modifier == null)
                throw new ArgumentNullException("modifier");

            modifiers.Add(modifier);
        }

        public void RemoveModifier(IModifier modifier)
        {
            if (modifier == null)
                throw new ArgumentNullException("modifier");

            if (!modifiers.Contains(modifier))
                return;

            modifiers.Remove(modifier);
        }

        #region INotifyPropertyChanged Members

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
