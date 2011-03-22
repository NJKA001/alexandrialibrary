using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Handlers
{
    public abstract class Handler<T> : IHandler<T>
        where T : IMessage
    {
        protected Handler()
        {
        }

        protected abstract void HandleMessage(T message);

        public virtual void Handle(T message)
        {
            HandleMessage(message);
        }

        public Type Type
        {
            get { return typeof(T); }
        }
    }
}
