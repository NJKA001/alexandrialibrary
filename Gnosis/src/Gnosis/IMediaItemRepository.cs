using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaItemRepository<T>
        where T : IMediaItem
    {
        void Initialize();
        void Save(IEnumerable<T> items);
        void Delete(IEnumerable<Uri> items);

        T GetByLocation(Uri location);
        T GetByCreatorAndName(Uri creator, string name);
        IEnumerable<T> GetByCatalog(Uri catalog);
        IEnumerable<T> GetByCreator(Uri creator);
        IEnumerable<T> GetByName(string name);
        IEnumerable<T> GetByTarget(Uri target);
    }
}
