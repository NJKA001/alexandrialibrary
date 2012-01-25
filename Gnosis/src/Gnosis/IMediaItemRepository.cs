using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaItemRepository
    {
        void Initialize();
        void Save<T>(IEnumerable<T> items) where T : class, IMediaItem;
        void Delete<T>(IEnumerable<Uri> items) where T : class, IMediaItem;

        T GetByLocation<T>(Uri location) where T : class, IMediaItem;
        T GetByCreatorAndName<T>(Uri creator, string name) where T : class, IMediaItem;
        IEnumerable<T> GetByCatalog<T>(Uri catalog) where T : class, IMediaItem;
        IEnumerable<T> GetByCreator<T>(Uri creator) where T : class, IMediaItem;
        IEnumerable<T> GetByName<T>(string name) where T : class, IMediaItem;
        IEnumerable<T> GetByTarget<T>(Uri target) where T : class, IMediaItem;

        IEnumerable<T> GetByTag<T>(TagDomain domain, string pattern) where T : class, IMediaItem;
        IEnumerable<T> GetByTag<T>(TagDomain domain, string pattern, IAlgorithm algorithm) where T : class, IMediaItem;
    }
}
