using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GnosisTests.Entities;

namespace GnosisTests.Serialization
{
    public class ArtistSerializer
        : SerializerBase<Artist>
    {
        public override Artist Deserialize(string[] data)
        {
            return new Artist()
            {
                Id = uint.Parse(data[0]),
                Name = data[1]
            };
        }

        public override string Serialize(Artist item)
        {
            return string.Format("{0}\t{1}", item.Id, item.Name);
        }
    }
}
