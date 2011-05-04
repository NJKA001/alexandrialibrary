using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Gnosis.Alexandria.Models
{
    public class ModelContext
        : IContext
    {
        public ModelContext(Uri currentUser)
        {
            this.currentUser = currentUser;
            this.dispatcher = Dispatcher.CurrentDispatcher;
        }

        public ModelContext(Uri currentUser, Dispatcher dispatcher)
        {
            this.currentUser = currentUser;
            this.dispatcher = dispatcher;
        }

        private readonly Dispatcher dispatcher;
        private Uri currentUser;

        private DateTime GetCurrentDateTime()
        {
            return DateTime.Now.ToUniversalTime();
        }

        public Uri CurrentUser
        {
            get { return currentUser; }
        }

        public void ChangeCurrentUser(Uri user)
        {
            currentUser = user;
        }

        public void Invoke(Action action)
        {
            if (dispatcher.CheckAccess())
            {
                action.Invoke();
            }
            else
            {
                dispatcher.Invoke(action, DispatcherPriority.DataBind, null);
            }
        }

        public ITimeStamp GetCreatedTimeStamp()
        {
            return new TimeStamp(currentUser);
        }

        public ITimeStamp GetAccessedTimeStamp(ITimeStamp timeStamp)
        {
            return new TimeStamp(timeStamp.CreatedBy, timeStamp.CreatedDate, currentUser, GetCurrentDateTime(), timeStamp.LastModifiedBy, timeStamp.LastModifiedDate);
        }

        public ITimeStamp GetModifiedTimeStamp(ITimeStamp timeStamp)
        {
            return new TimeStamp(timeStamp.CreatedBy, timeStamp.CreatedDate, timeStamp.LastAccessedBy, timeStamp.LastAccessedDate, currentUser, GetCurrentDateTime());
        }
    }
}
