using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels
{
    public class CatalogMediaTaskViewModel
        : TaskViewModel
    {
        public CatalogMediaTaskViewModel(ILogger logger, CatalogMediaTask task)
            : base(logger, task, "Catalog", GetDescription(task), "pack://application:,,,/Images/pyramid_black2.jpg")
        {
        }

        private static string GetDescription(CatalogMediaTask task)
        {
            return new System.IO.FileInfo(task.Target.LocalPath).FullName;
        }
    }
}
