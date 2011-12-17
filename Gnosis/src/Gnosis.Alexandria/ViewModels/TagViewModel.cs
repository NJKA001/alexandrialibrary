using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class TagViewModel
        : ITagViewModel
    {
        public TagViewModel(ITag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            this.tag = tag;
        }

        private readonly ITag tag;

        private bool isClosed;
        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public long Id
        {
            get { return tag.Id; }
        }

        public IAlgorithm Algorithm
        {
            get { return tag.Algorithm; }
        }

        public string AlgorithmName
        {
            get { return tag.Algorithm.Name; }
        }

        public ITagType Type
        {
            get { return tag.Type; }
        }

        public string TypeName
        {
            get { return tag.Type.Name; }
        }

        public string Value
        {
            get { return tag.Value; }
        }

        public bool IsClosed
        {
            get { return isClosed; }
            set
            {
                isClosed = value;
                OnPropertyChanged("IsClosed");
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
