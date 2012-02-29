using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests.Entities
{
    public abstract class EntityBase
        : IEntity
    {
        public uint Id { get; set; }
    }
}
