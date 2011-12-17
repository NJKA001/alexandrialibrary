using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public interface ITagController
    {
        IEnumerable<ITagViewModel> GetTags(Uri target);
    }
}
