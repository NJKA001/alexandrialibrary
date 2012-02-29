using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests.Entities
{
    public class TrackSerializer
        : SerializerBase<Track>
    {
        public override Track Deserialize(string[] data)
        {
            return new Track()
            {
                Id = uint.Parse(data[0]),
                Name = data[1],
                Album = uint.Parse(data[2]),
                Artist = uint.Parse(data[3]),
                Disc = byte.Parse(data[4]),
                Number = byte.Parse(data[5]),
                Duration = ushort.Parse(data[6])
            };
        }

        public override string Serialize(Track item)
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", item.Id, item.Name, item.Album, item.Artist, item.Disc, item.Number, item.Duration);
        }

        public override string SerializeUpdate(Track entity, string field, object value)
        {
            throw new NotImplementedException();
        }

        public override void ApplyUpdate(Track entity, string field, string value)
        {
            throw new NotImplementedException();
        }
    }
}
