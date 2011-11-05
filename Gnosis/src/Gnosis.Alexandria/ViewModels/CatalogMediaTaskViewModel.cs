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
            : base(logger, task, "Building Media Catalog", "pack://application:,,,/Images/blank_scroll.png", "pack://application:,,,/Images/anubis_horus_scroll.jpg")
        {
        }
    }
}
