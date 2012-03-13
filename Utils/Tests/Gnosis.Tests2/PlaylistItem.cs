using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class PlaylistItem
        : Entity
    {
        private ushort number;
        private ushort duration;
        private string target;

        public ushort Number
        {
            get { return number; }
            set
            {
                number = value;
                NotifyPropertyChanged("Number");
            }
        }

        public ushort Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                NotifyPropertyChanged("Duration");
            }
        }

        public string Target
        {
            get { return target; }
            set
            {
                target = value;
                NotifyPropertyChanged("Target");
            }
        }
    }
}
