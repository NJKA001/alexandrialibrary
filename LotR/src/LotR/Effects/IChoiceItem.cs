using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public interface IChoiceItem
        : INotifyPropertyChanged
    {
        Guid ItemId { get; }
        string Text { get; }

        bool IsChosen { get; set; }
        bool IsExpanded { get; set; }
    }
}
