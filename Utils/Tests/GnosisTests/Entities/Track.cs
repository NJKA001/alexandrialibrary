using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests.Entities
{
    public class Track
        : EntityBase
    {
        public string Name;
        public uint Album;
        public uint Artist;
        public byte Disc;
        public byte Number;
        public ushort Duration;
    }
}
