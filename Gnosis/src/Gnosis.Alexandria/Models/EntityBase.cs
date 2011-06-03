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

        protected IContext Context
        {
            get { return context; }
        }

        protected void OnEntityChanged(Action action, string propertyName)
        {
            context.Invoke(action);

            isChanged = true;

            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                catch (Exception ex)
                {
                    log.Error("EntityBase.OnEntityChanged", ex);
                }
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

        public virtual IEnumerable<IChild> GetChildren(ChildInfo childInfo)
        {
            return new List<IChild>();
        }

        public virtual IEnumerable<IValue> GetValues(ValueInfo valueInfo)
        {
            return new List<IValue>();
        }

        public virtual void Save(DateTime timeStamp)
        {
            this.timeStamp = timeStamp;
            isNew = false;
            isChanged = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
