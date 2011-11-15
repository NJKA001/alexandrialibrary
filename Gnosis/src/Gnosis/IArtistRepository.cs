using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IArtistRepository
    {
        void Initialize();
        void Save(IEnumerable<IArtist> artists);
        void Delete(IEnumerable<Uri> artists);

        IArtist GetByLocation(Uri location);
        IEnumerable<IArtist> GetByName(string name);
    }
}
