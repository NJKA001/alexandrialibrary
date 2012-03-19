using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITaskDetailViewModel
    {
        string Type { get; }
        Brush Foreground { get; }
        int Count { get; }
        int Maximum { get; }
        string Description { get; }
        Visibility MoreVisibility { get; }
        string Message { get; }
        string Trace { get; }
    }
}
