using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Core
{
    public interface ITagRepository
    {
        ITag GetById(long id);
        IEnumerable<ITag> GetByTarget(Uri target);
        IEnumerable<ITag> GetByTarget(Uri target, ITagSchema schema);
        IEnumerable<ITag> GetByTarget(Uri target, ITagType type);
        IEnumerable<ITag> GetByAlgorithm(IAlgorithm algorithm, ITagDomain domain, string pattern);

        Action Search(IAlgorithm algorithm, string pattern, Action<IEnumerable<ITag>> tagCallback, Action completedCallback);

        void Initialize();
        void Delete(IEnumerable<ITag> tags);
        void Save(IEnumerable<ITag> tags);
    }
}
