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
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Select A Directory To Catalog";
            dialog.ShowNewFolderButton = true;
            dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            var result = dialog.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            var catalogViewModel = taskController.GetCatalogViewModel(dialog.SelectedPath);
            taskResultView.Catalog(catalogViewModel);
        }
    }
}
