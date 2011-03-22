using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Controllers.Interfaces
{
    public interface ITabController : IController
    {
        TabControl TabControl { get; set; }
        void AddTab(ITabView view);
        void RemoveTab(Guid id);
        void SelectTab(Guid id);
    }
}
