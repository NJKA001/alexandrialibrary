using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class FileSystemSource : SourceBase
    {
        public FileSystemSource()
            : this(Guid.NewGuid())
        {
        }

        public FileSystemSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/filesystem.png";
            AddChild(new ProxySource(Guid.Empty){Parent = this});
        }
    }
}
