﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class ModelContext
        : IContext
    {
        public ModelContext(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        private readonly Dispatcher dispatcher;

        private DateTime GetCurrentDateTime()
        {
            return DateTime.Now.ToUniversalTime();
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
    }
}