using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IPicRepository
    {
        void Initialize();
        void Save(IEnumerable<IPic> tracks);
        void Delete(IEnumerable<Uri> tracks);

        IPic GetByLocation(Uri location);
        IPic GetByTarget(Uri target);
        IEnumerable<IPic> GetByAlbum(Uri album);
        IEnumerable<IPic> GetByTitle(string title);
    }
}
