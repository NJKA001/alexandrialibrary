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
            : base(logger, task, GetName(task), GetDescription(task), "pack://application:,,,/Images/pyramid_black2.jpg")
        {
        }

        private static string GetName(CatalogMediaTask task)
        {
            return new System.IO.FileInfo(task.Target.LocalPath).FullName;
        }

        private static string GetDescription(CatalogMediaTask task)
        {
            return string.Format("Catalog: {0}", GetName(task));
        }
    }
}
