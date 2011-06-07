using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using log4net;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public abstract class EntityBase
        : IEntity
    {
        protected EntityBase(IContext context)
        {
            this.context = context;
            this.id = Guid.NewGuid();
            this.timeStamp = DateTime.Now.ToUniversalTime();
            this.isNew = true;
        }

        protected EntityBase(IContext context, Guid id, DateTime timeStamp)
        {
            this.context = context;
            this.id = id;
            this.timeStamp = timeStamp;
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(EntityBase));

        private readonly IContext context;
        private readonly Guid id;
        private DateTime timeStamp;
        private bool isNew;
        private bool isChanged;
        private readonly IList<IChild> children = new List<IChild>();
        private readonly IList<IChild> removedChildren = new List<IChild>();
        private readonly IList<IValue> values = new List<IValue>();
        private readonly IList<IValue> removedValues = new List<IValue>();

        protected IContext Context
        {
            get { return context; }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                catch (Exception ex)
                {
                    log.Error("EntityBase.OnPropertyChanged", ex);
                }
            }
        }

        protected void OnEntityChanged(Action action, string propertyName)
        {
            context.Invoke(action);

            isChanged = true;

            OnPropertyChanged(propertyName);
        }

        protected void AddChild(Action action, IChild child, string propertyName)
        {
            if (!children.Contains(child))
            {
                context.Invoke(action);
                children.Add(child);
                OnPropertyChanged(propertyName);
            }
        }

        protected void RemoveChild(Action action, IChild child, string propertyName)
        {
            if (children.Contains(child))
            {
                context.Invoke(action);
                children.Remove(child);
                removedChildren.Add(child);
                OnPropertyChanged(propertyName);
            }
        }

        protected void AddValue(Action action, IValue value, string propertyName)
        {
            if (!values.Contains(value))
            {
                context.Invoke(action);
                values.Add(value);
                OnPropertyChanged(propertyName);
            }
        }

        protected void RemoveValue(Action action, IValue value, string propertyName)
        {
            if (values.Contains(value))
            {
                context.Invoke(action);
                values.Remove(value);
                removedValues.Add(value);
                OnPropertyChanged(propertyName);
            }
        }

        public Guid Id
        {
            get { return id; }
        }

        public DateTime TimeStamp
        {
            get { return timeStamp; }
        }

        public bool IsNew()
        {
            return isNew;
        }

        public bool IsChanged()
        {
            return isChanged;
        }

        public virtual IEnumerable<IChild> GetChildren(EntityInfo childInfo)
        {
            return removedChildren.Concat(children);
        }

        public virtual IEnumerable<IValue> GetValues(ValueInfo valueInfo)
        {
            return removedValues.Concat(values);
        }

        public virtual void Save(DateTime timeStamp)
        {
            this.timeStamp = timeStamp;
            isNew = false;
            isChanged = false;
            removedChildren.Clear();
            removedValues.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
