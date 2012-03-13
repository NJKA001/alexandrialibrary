using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Artist
        : Entity
    {
        private readonly ObservableCollection<Album> albums = new ObservableCollection<Album>();

        IEnumerable<Album> Albums { get { return albums; } }
    }
}
