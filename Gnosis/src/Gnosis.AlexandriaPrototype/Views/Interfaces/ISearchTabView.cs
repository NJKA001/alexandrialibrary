using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Views.Interfaces
{
    public interface ISearchTabView : ITabView
    {
        string Search { get; set; }
    }
}
