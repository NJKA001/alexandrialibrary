using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR.Effects.Choices
{
    public abstract class ChoiceItemBase
        : IChoiceItem, INotifyPropertyChanged
    {
        protected ChoiceItemBase(string text)
            : this(text, false)
        {
        }

        protected ChoiceItemBase(string text, bool isOptional)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            this.text = text;
            this.isOptional = isOptional;
        }

        private readonly string text;
        private readonly bool isOptional;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Text
        {
            get { return text; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
