using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using log4net;

namespace Gnosis.Alexandria.Models
{
    public abstract class ChangeableModelBase
        : ModelBase, IChangeableModel
    {
        protected ChangeableModelBase(IModelContext modelContext)
            : base(modelContext)
        {
            this.timeStamp = modelContext.GetCreatedTimeStamp();
        }

        protected ChangeableModelBase(IModelContext modelContext, Guid id, ITimeStamp timeStamp)
            : base(modelContext, id)
        {
            this.timeStamp = modelContext.GetAccessedTimeStamp(timeStamp);
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(ChangeableModelBase));

        private ITimeStamp timeStamp;
        private bool isChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            timeStamp = ModelContext.GetModifiedTimeStamp(timeStamp);
            isChanged = true;

            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                catch (Exception ex)
                {
                    log.Error("ChangeableModelBase.OnPropertyChanged", ex);
                }
            }
        }

        public ITimeStamp TimeStamp
        {
            get { return timeStamp; }
        }

        public bool IsChanged
        {
            get { return isChanged; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
