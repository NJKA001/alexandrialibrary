using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IPlaylistItemViewModel
        : IMetadataViewModel, IPlayableViewModel
    {
        TaskItem ToTaskItem();
    }
}
