using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMetadataRepository
    {
        void Initialize();
        void Save<T>(IEnumerable<T> items) where T : class, IMetadata;
        void Delete<T>(IEnumerable<Uri> items) where T : class, IMetadata;

        T GetByLocation<T>(Uri location) where T : class, IMetadata;
        T GetByCreatorAndName<T>(Uri creator, string name) where T : class, IMetadata;
        IEnumerable<T> GetByCatalog<T>(Uri catalog) where T : class, IMetadata;
        IEnumerable<T> GetByCreator<T>(Uri creator) where T : class, IMetadata;
        IEnumerable<T> GetByName<T>(string name) where T : class, IMetadata;
        IEnumerable<T> GetByTarget<T>(Uri target) where T : class, IMetadata;

        IEnumerable<T> GetByTag<T>(TagDomain domain, string pattern) where T : class, IMetadata;
        IEnumerable<T> GetByTag<T>(TagDomain domain, string pattern, IAlgorithm algorithm) where T : class, IMetadata;
    }
}
