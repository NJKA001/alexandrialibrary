using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface ICatalogController
    {
        ITask<IEnumerable<IMedia>> BuildCatalog(Uri location);
        ITask<IEnumerable<IMedia>> BuildCatalog(string path);
    }
}
