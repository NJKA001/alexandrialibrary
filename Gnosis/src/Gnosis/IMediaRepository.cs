using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaRepository
    {
        IMedia Lookup(Uri location);
        IEnumerable<IMedia> ByLocation(string pattern);

        void Initialize();
        void Delete(IEnumerable<Uri> locations);
        void Save(IEnumerable<IMedia> media);
    }
}
