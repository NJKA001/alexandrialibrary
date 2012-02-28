using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GnosisTests.Entities;

namespace GnosisTests.Serialization
{
    public class AlbumSerializer
        : SerializerBase<Album>
    {
        public override Album Deserialize(string[] data)
        {
            return new Album()
            {
                Id = uint.Parse(data[0]),
                Name = data[1],
                Artist = uint.Parse(data[2]),
                Year = ushort.Parse(data[3]),
                AlbumType = ushort.Parse(data[4])
            };
        }

        public override string Serialize(Album item)
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}", item.Id, item.Name, item.Artist, item.Year, item.AlbumType);
        }
    }
}
