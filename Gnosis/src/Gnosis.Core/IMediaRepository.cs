using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaRepository
    {
        IMedia Lookup(Uri location);
        IEnumerable<IMedia> All();

        void Delete(IMedia media);
        void Delete(IEnumerable<IMedia> media);
        void Save(IMedia media);
        void Save(IEnumerable<IMedia> media);
    }
}
