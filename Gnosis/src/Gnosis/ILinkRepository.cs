using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ILinkRepository
    {
        ILink GetById(long id);
        IEnumerable<ILink> GetBySource(Uri source);
        IEnumerable<ILink> GetBySource(Uri source, string relationship);
        IEnumerable<ILink> GetByTarget(Uri target);
        IEnumerable<ILink> GetByTarget(Uri target, string relationship);
        IEnumerable<ILink> GetBySourceAndTarget(Uri source, Uri target);
        IEnumerable<ILink> GetBySourceAndTarget(Uri source, Uri target, string relationship);

        void Initialize();
        void Delete(IEnumerable<long> ids);
        void Save(IEnumerable<ILink> links);
    }
}
