using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITagRepository
    {
        Gnosis.ITag GetById(long id);
        IEnumerable<Gnosis.ITag> GetByTarget(Uri target);
        IEnumerable<Gnosis.ITag> GetByTarget(Uri target, TagDomain domain);
        IEnumerable<Gnosis.ITag> GetByTarget(Uri target, ITagType type);
        IEnumerable<Gnosis.ITag> GetByAlgorithm(IAlgorithm algorithm, TagDomain domain, string pattern);

        ITask<IEnumerable<Gnosis.ITag>> Search(IAlgorithm algorithm, string pattern);

        void Initialize();
        void Delete(IEnumerable<long> ids);
        void Save(IEnumerable<Gnosis.ITag> tags);
        void Overwrite(Uri target, ITagType type, IEnumerable<Gnosis.ITag> tags);
    }
}
