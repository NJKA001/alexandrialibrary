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
        void Delete(IEnumerable<Guid> artists);

        IArtist GetById(Guid id);
        IEnumerable<IArtist> GetByName(string name);
    }
}
