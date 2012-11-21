using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public abstract class Answer<TSource>
        : ChoiceItemBase<TSource>, IAnswer
        where TSource : class, ISource
    {
        protected Answer(string text, TSource source, object item, Type itemType, IQuestion followUp)
            : base(text, source)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (itemType == null)
                throw new ArgumentNullException("itemType");

            this.item = item;
            this.itemType = itemType;
            this.followUp = followUp;
        }

        private readonly object item;
        private readonly Type itemType;

        private IQuestion parent;
        private IQuestion followUp;
        private bool isChosen;

        public Type ItemType
        {
            get { return itemType; }
        }

        public IQuestion Parent
        {
            get { return parent; }
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

        public T GetObject<T>()
            where T : class
        {
            return item as T;
        }

        public T GetPrimative<T>()
            where T : struct
        {
            return (T)item;
        }

        public abstract void Execute(IGame game, IEffectHandle handle);

        public void SetParent(IQuestion parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            this.parent = parent;
        }

        public void SetFollowUp(IQuestion followUp)
        {
            if (followUp == null)
                throw new ArgumentNullException("followUp");

            this.followUp = followUp;
        }
    }

    public class Answer<TSource, TItem>
        : Answer<TSource>
        where TSource : class, ISource
    {
        public Answer(string text, TSource source, TItem item, Action<IGame, IEffectHandle, TItem> executeFunction)
            : this(text, source, item, executeFunction, null)
        {
        }

        public Answer(string text, TSource source, TItem item, Action<IGame, IEffectHandle, TItem> executeFunction, IQuestion followUp)
            : base(text, source, item, typeof(TItem), followUp)
        {
            this.typedItem = item;
            this.executeFunction = executeFunction;
        }

        private readonly Action<IGame, IEffectHandle, TItem> executeFunction;
        private readonly TItem typedItem;

        public override void Execute(IGame game, IEffectHandle handle)
        {
            if (executeFunction == null)
                return;

            executeFunction(game, handle, typedItem);
        }
    }
}
