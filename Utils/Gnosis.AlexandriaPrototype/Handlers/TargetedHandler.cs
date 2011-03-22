using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers
{
    public abstract class TargetedHandler<T> : Handler<T>
        where T : ITargetedMessage
    {
        protected TargetedHandler()
        {
        }

        public Guid Host { get; set; }

        public override void Handle(T message)
        {
            if (message.Target == Host)
                HandleMessage(message);
        }
    }
}
