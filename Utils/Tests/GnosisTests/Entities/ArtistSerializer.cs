using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests.Entities
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

        public override string Serialize(Artist entity)
        {
            return string.Format("{0}\t{1}", entity.Id, entity.Name);
        }

        public override string SerializeUpdate(Artist entity, string field, object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            switch (field)
            {
                case "Name":
                    return string.Format("{0}\t{1}\t{2}", entity.Id, field, value);
                default:
                    return null;
            }
        }

        public override void ApplyUpdate(Artist entity, string field, string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            switch (field)
            {
                case "Name":
                    entity.Name = value;
                    break;
                default:
                    break;
            }
        }
    }
}
