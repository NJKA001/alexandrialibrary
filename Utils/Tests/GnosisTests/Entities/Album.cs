using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests.Entities
{
    public class Album
        : EntityBase
    {
        public string Name;
        public uint Artist;
        public ushort Year;
        public ushort AlbumType;
    }
}
