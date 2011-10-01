using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagRepository
    {
        ITag Lookup(long id);
        IEnumerable<ITag> All();
        IEnumerable<ITag> Search(IAlgorithm algorithm, Uri type);
        IEnumerable<ITag> Search(IAlgorithm algorithm, Uri type, string name);
        IEnumerable<ITag> Search(IAlgorithm algorithm, string name);

        void Initialize();
        void Delete(ITag tag);
        void Delete(IEnumerable<ITag> tags);
        void Save(ITag tag);
        void Save(IEnumerable<ITag> tags);
    }
}
