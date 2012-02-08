using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Gnosis.Alexandria.Models
{
    public interface IPlaybackStatus : INotifyPropertyChanged
    {
        string ImagePath { get; set; }
        bool IsPlaying { get; set; }
    }
}
