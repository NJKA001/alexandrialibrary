using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class FeedItem
        : Entity
    {
        private DateTime published;
        private int number;
        private string summary;

        public DateTime Published
        {
            get { return published; }
            set
            {
                published = value;
                NotifyPropertyChanged("Published");
            }
        }

        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                NotifyPropertyChanged("Number");
            }
        }

        public string Summary
        {
            get { return summary; }
            set
            {
                summary = value;
                NotifyPropertyChanged("Summary");
            }
        }
    }
}
