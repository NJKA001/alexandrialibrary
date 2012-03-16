using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Link
        : INotifyPropertyChanged
    {
        public Link()
        {
        }

        public Link(string name, Relationship relationship, Source source, string target)
        {
            this.name = name;
            this.relationship = relationship;
            this.source = source;
            this.target = target;
        }

        private bool isChanged;

        private string name = string.Empty;
        private Relationship relationship;
        private Source source;
        private string target = string.Empty;

        private void NotifyPropertyChanged(string propertyName)
        {
            isChanged = true;

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsChanged
        {
            get { return isChanged; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                var safe = value ?? string.Empty;
                if (name != safe)
                {
                    name = safe;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public Relationship Relationship
        {
            get { return relationship; }
            set
            {
                if (relationship != value)
                {
                    relationship = value;
                    NotifyPropertyChanged("Relationship");
                }
            }
        }

        public Source Source
        {
            get { return source; }
            set
            {
                if (source != value)
                {
                    source = value;
                    NotifyPropertyChanged("Source");
                }
            }
        }

        public string Target
        {
            get { return target; }
            set
            {
                var safe = value ?? string.Empty;
                if (target != safe)
                {
                    target = safe;
                    NotifyPropertyChanged("Target");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
