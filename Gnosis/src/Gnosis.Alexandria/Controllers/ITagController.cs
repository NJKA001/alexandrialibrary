using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TagLib;

using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Controllers
{
    public interface ITagController
    {
        File GetFile(string path);
        Tag GetTag(string path);
        void LoadPicture(IOldTrack track);
        void SaveTag(IOldTrack track);
        void AddPicture(IOldTrack track, string path);
        void AddPicture(IOldTrack track, IPicture picture);
    }
}
