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
            : base(logger, task, "Cataloging Media", "pack://application:,,,/Images/Sphinx.png")
        {
        }
    }
}
