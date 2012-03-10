using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ILinkRepository
    {
        Gnosis.ILink GetById(long id);
        IEnumerable<Gnosis.ILink> GetBySource(Uri source);
        IEnumerable<Gnosis.ILink> GetBySource(Uri source, string relationship);
        IEnumerable<Gnosis.ILink> GetByTarget(Uri target);
        IEnumerable<Gnosis.ILink> GetByTarget(Uri target, string relationship);
        IEnumerable<Gnosis.ILink> GetBySourceAndTarget(Uri source, Uri target);
        IEnumerable<Gnosis.ILink> GetBySourceAndTarget(Uri source, Uri target, string relationship);

        void Initialize();
        void Delete(IEnumerable<long> ids);
        void Save(IEnumerable<Gnosis.ILink> links);
    }
}
