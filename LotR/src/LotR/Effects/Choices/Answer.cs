using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public class Answer<TSource, TItem>
        : ChoiceItemBase<TSource>, IAnswer
        where TSource : class, ISource
        where TItem : class
    {
        public Answer(string text, TSource source, TItem item, Action<IGame> executeFunction)
            : this(text, source, item, executeFunction, null)
        {
        }

        public Answer(string text, TSource source, TItem item, Action<IGame> executeFunction, IQuestion followUp)
            : base(text, source)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (executeFunction == null)
                throw new ArgumentNullException("executeFunction");

            this.item = item;
            this.itemType = typeof(TItem);
            this.executeFunction = executeFunction;
            this.followUp = followUp;
        }

        private readonly TItem item;
        private readonly Type itemType;
        private readonly Action<IGame> executeFunction;
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

        public void Execute(IGame game)
        {
            executeFunction(game);
        }
    }
}
