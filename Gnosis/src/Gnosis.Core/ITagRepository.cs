using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagRepository
    {
        ITag GetById(long id);
        IEnumerable<ITag> GetByTarget(Uri target);
        IEnumerable<ITag> GetByTarget(Uri target, ITagSchema schema);
        IEnumerable<ITag> GetByTarget(Uri target, ITagType type);
        IEnumerable<ITag> GetByAlgorithm(IAlgorithm algorithm, ITagDomain domain, string name);

        void Initialize();
        void Delete(ITag tag);
        void Delete(IEnumerable<ITag> tags);
        void Save(ITag tag);
        void Save(IEnumerable<ITag> tags);
    }
}
