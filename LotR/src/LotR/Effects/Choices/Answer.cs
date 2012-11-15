using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Choices
{
    public class Answer<TItem>
        : ChoiceItemBase, IAnswer
        where TItem : class
    {
        public Answer(string text, TItem item)
            : this(text, item, null)
        {
        }

        public Answer(string text, TItem item, IQuestion followUp)
            : base(text)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this.item = item;
            this.itemType = typeof(TItem);
            this.followUp = followUp;
        }

        private readonly TItem item;
        private readonly Type itemType;
        private readonly IQuestion followUp;

        private bool isChosen;

        public Type ItemType
        {
            get { return itemType; }
        }

        public IQuestion FollowUp
        {
            get { return followUp; }
        }

        public bool IsChosen
        {
            get { return isChosen; }
            set
            {
                if (isChosen == value)
                    return;

                isChosen = value;
                OnPropertyChanged("IsChosen");
            }
        }

        public T GetItem<T>()
            where T : class
        {
            return item as T;
        }
    }
}
