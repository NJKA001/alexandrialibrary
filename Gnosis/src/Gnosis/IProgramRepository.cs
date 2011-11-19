using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IProgramRepository
    {
        void Initialize();
        void Save(IEnumerable<IProgram> programs);
        void Delete(IEnumerable<Uri> programs);

        IProgram GetByLocation(Uri location);
        IProgram GetByTarget(Uri target);
        IEnumerable<IProgram> GetByAlbum(Uri album);
        IEnumerable<IProgram> GetByTitle(string title);
    }
}
