using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Views;
using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels.Commands
{
    public class CatalogCommandViewModel
        : CommandViewModel
    {
        public CatalogCommandViewModel()
            : base("Catalog", "Locate, identify, store and synchronize your media", "pack://application:,,,/Images/pyramid_black2.jpg")
        {
        }

        protected override void DoExecute(ITaskController taskController, TaskResultView taskResultView)
        {
        }
    }
}
