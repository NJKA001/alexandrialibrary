using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using log4net;

namespace Gnosis.Alexandria.Models
{
    public abstract class EntityBase
        : IEntity
    {
        protected EntityBase(IContext context)
        {
            this.context = context;
            this.id = Guid.NewGuid();
            this.timeStamp = context.GetCreatedTimeStamp();
            this.isNew = true;
        }

        protected EntityBase(IContext context, Guid id, ITimeStamp timeStamp)
        {
            this.context = context;
            this.id = id;
            this.timeStamp = context.GetAccessedTimeStamp(timeStamp);
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(EntityBase));

        private readonly IContext context;
        private readonly Guid id;
        private ITimeStamp timeStamp;
        private bool isNew;
        private bool isChanged;

        protected IContext Context
        {
            get { return context; }
        }

        protected void OnEntityChanged(Action action, string propertyName)
        {
            context.Invoke(action);

            timeStamp = context.GetModifiedTimeStamp(timeStamp);
            isChanged = true;

            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                catch (Exception ex)
                {
                    log.Error("ChangeableModelBase.OnEntityChanged", ex);
                }
            }
        }

        public Guid Id
        {
            get { return id; }
        }

        public ITimeStamp TimeStamp
        {
            get { return timeStamp; }
        }

        public bool IsNew
        {
            get { return isNew; }
        }

        public bool IsChanged
        {
            get { return isChanged; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
