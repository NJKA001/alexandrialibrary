using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gnosis.Alexandria.Models
{
    public class LoadSourceRequest
    {
        public ISource Source { get; set; }
        public DependencyObject Handle { get; set; }
    }
}
