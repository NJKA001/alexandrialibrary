using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IClipRepository
    {
        void Initialize();
        void Save(IEnumerable<IClip> clips);
        void Delete(IEnumerable<Uri> clips);

        IClip GetByLocation(Uri location);
        IClip GetByTarget(Uri target);
        IEnumerable<IClip> GetByAlbum(Uri album);
        IEnumerable<IClip> GetByTitle(string title);
    }
}
