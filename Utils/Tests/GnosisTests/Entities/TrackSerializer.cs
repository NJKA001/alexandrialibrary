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
            if (value == null)
                throw new ArgumentNullException("value");

            switch (field)
            {
                case "Name":
                case "Album":
                case "Artist":
                case "Disc":
                case "Number":
                case "Duration":
                    return string.Format("{0}\t{1}\t{2}", entity.Id, field, value);
                default:
                    return null;
            }
        }

        public override void ApplyUpdate(Track entity, string field, string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            switch (field)
            {
                case "Name":
                    entity.Name = value;
                    break;
                case "Album":
                    entity.Album = uint.Parse(value);
                    break;
                case "Artist":
                    entity.Artist = uint.Parse(value);
                    break;
                case "Disc":
                    entity.Disc = byte.Parse(value);
                    break;
                case "Number":
                    entity.Number = byte.Parse(value);
                    break;
                case "Duration":
                    entity.Duration = ushort.Parse(value);
                    break;
                default:
                    break;
            }
        }
    }
}
