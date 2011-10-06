using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Tags.Id3;

namespace Gnosis.Core
{
    public class TagSchemaFactory
        : ITagSchemaFactory
    {
        public TagSchemaFactory()
        {
            Add(TagSchema.Default);
            Add(Id3Schemas.Id3v1Schema);
            Add(Id3Schemas.Id3v2Schema);
        }

        private IDictionary<long, ITagSchema> byId = new Dictionary<long, ITagSchema>();

        #region ISchemaRepository Members

        public ITagSchema Create(long id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : TagSchema.Default;
        }

        public void Add(ITagSchema schema)
        {
            if (schema == null)
                throw new ArgumentNullException("schema");

            byId.Add(schema.Id, schema);
        }

        public void Remove(ITagSchema schema)
        {
            if (schema == null)
                throw new ArgumentNullException("schema");

            if (byId.ContainsKey(schema.Id))
                byId.Remove(schema.Id);
        }

        #endregion
    }
}
