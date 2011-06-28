using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITreeNode
        : INotifyPropertyChanged
    {
        string Name { get; }
        object ImageSource { get; }
        bool IsBeingRenamed { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        Visibility Visibility { get; }

        void StartRenaming();
        void CancelRenaming();
        void FinishRenaming(string name);
    }
}
