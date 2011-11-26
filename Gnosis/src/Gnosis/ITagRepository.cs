using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITagRepository
    {
        ITag GetById(long id);
        IEnumerable<ITag> GetByTarget(Uri target);
        IEnumerable<ITag> GetByTarget(Uri target, TagDomain domain);
        IEnumerable<ITag> GetByTarget(Uri target, ITagType type);
        IEnumerable<ITag> GetByAlgorithm(IAlgorithm algorithm, TagDomain domain, string pattern);

        ITask<IEnumerable<ITag>> Search(IAlgorithm algorithm, string pattern);

        void Initialize();
        void Delete(IEnumerable<long> ids);
        void Save(IEnumerable<ITag> tags);
    }
}
