using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IAlbum
    {
        Guid Id { get; }
        string Title { get; }
        DateTime Released { get; }
        
        Guid Artist { get; }
        string ArtistName { get; }
        
        IImage Thumbnail { get; }
    }
}
