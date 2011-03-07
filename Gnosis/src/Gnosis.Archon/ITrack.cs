using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public interface ITrack : INotifyPropertyChanged
    {
        string Path { get; set; }
        string ImagePath { get; set; }
        ICollection<byte> ImageData { get; set; }
        object ImageSource { get; }
        string Title { get; set; }
        string Artist { get; set; }
        string Album { get; set; }
        uint Number { get; set; }
        DateTime ReleaseDate { get; set; }
        bool IsSelected { get; set; }
    }
}
