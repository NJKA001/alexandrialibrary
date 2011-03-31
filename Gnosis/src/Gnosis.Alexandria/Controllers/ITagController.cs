using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TagLib;

namespace Gnosis.Alexandria.Controllers
{
    public interface ITagController
    {
        File GetFile(string path);
        Tag GetTag(string path);
        void LoadPicture(ITrack track);
        void SaveTag(ITrack track);
    }
}
