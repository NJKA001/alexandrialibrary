using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Tag
        : INotifyPropertyChanged
    {
        public Tag()
        {
        }

        public Tag(string name, Category category, Source source, string target)
        {
            this.name = name;
            this.category = category;
            this.source = source;
            this.target = target;
        }

        private bool isChanged;

        private string name = string.Empty;
        private Category category;
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

        public Category Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    NotifyPropertyChanged("Category");
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
