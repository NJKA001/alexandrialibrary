using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface ITrackController : IRepository<ITrack>
    {
        ITrack ReadFromTag(string path);
        void LoadDirectory(DirectoryInfo directory);
    }
}
