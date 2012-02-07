using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public interface IMarqueeController
    {
        int NumberOfPages { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }

        IEnumerable<IMarqueeViewModel> GetItems();

        void RefreshItems();
        void UpdateFilter(string filter);
    }
}
