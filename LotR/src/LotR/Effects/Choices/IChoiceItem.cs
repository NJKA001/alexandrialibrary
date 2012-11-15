using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR.Effects.Choices
{
    public interface IChoiceItem
        : INotifyPropertyChanged
    {
        string Text { get; }
    }
}
