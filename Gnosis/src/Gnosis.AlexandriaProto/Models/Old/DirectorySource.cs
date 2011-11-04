using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class DirectorySource : SourceBase
    {
        public DirectorySource()
            : this(Guid.NewGuid())
        {
        }

        public DirectorySource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/directory.png";
            AddChild(new ProxySource(Guid.Empty){ Parent = this });
        }
    }
}
