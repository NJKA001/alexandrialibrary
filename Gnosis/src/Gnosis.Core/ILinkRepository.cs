using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ILinkRepository
    {
        ILink GetById(long id);
        IEnumerable<ILink> GetBySource(Uri source);
        IEnumerable<ILink> GetBySource(Uri source, ILinkType type);
        IEnumerable<ILink> GetByTarget(Uri target);
        IEnumerable<ILink> GetByTarget(Uri target, ILinkType type);
        IEnumerable<ILink> GetBySourceAndTarget(Uri source, Uri target);
        IEnumerable<ILink> GetBySourceAndTarget(Uri source, Uri target, ILinkType type);

        void Initialize();
        void Delete(IEnumerable<long> ids);
        void Save(IEnumerable<ILink> links);
    }
}
