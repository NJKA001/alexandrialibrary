using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class FolderSource : SourceBase
    {
        public FolderSource()
            : this(Guid.NewGuid())
        {
        }

        public FolderSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/folder.png";
        }
    }
}
