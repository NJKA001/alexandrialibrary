using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gnosis.Alexandria.Models
{
    public class LoadSourceRequest
    {
        public LoadSourceRequest(DependencyObject handle)
        {
            this.handle = handle;
        }

        private readonly DependencyObject handle;

        public ISource Source { get; set; }
        
        public void Invoke(Action action)
        {
            handle.Dispatcher.Invoke(action);
        }
    }
}
