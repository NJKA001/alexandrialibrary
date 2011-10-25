using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaRepository
    {
        IMedia Lookup(Uri location);
        
        void Initialize();
        void Delete(IEnumerable<Uri> locations);
        void Save(IEnumerable<IMedia> media);
    }
}
