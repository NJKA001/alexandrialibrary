using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IDocRepository
    {
        void Initialize();
        void Save(IEnumerable<IDoc> docs);
        void Delete(IEnumerable<Uri> docs);

        IDoc GetByLocation(Uri location);
        IDoc GetByTarget(Uri target);
        IEnumerable<IDoc> GetByAlbum(Uri album);
        IEnumerable<IDoc> GetByTitle(string title);
    }
}
