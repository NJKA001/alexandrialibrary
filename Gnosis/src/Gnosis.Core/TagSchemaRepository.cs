using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Tags.Id3;

namespace Gnosis.Core
{
    public class TagSchemaRepository
        : ITagSchemaFactory
    {
        public TagSchemaRepository()
        {
            Add(new Id3Schema());
        }

        private IDictionary<string, ITagSchema> byIdentifier = new Dictionary<string, ITagSchema>();

        #region ISchemaRepository Members

        public ITagSchema Get(Uri identifier)
        {
            if (identifier == null)
                throw new ArgumentNullException("identifier");

            var key = identifier.ToString();

            return byIdentifier.ContainsKey(key) ?
                byIdentifier[key]
                : TagSchema.Default;
        }

        public void Add(ITagSchema schema)
        {
            if (schema == null)
                throw new ArgumentNullException("schema");

            var key = schema.Identifier.ToString();
            if (!byIdentifier.ContainsKey(key))
                byIdentifier.Add(key, schema);

            foreach (var child in schema.Children)
                Add(child);
        }

        public void Remove(ITagSchema schema)
        {
            if (schema == null)
                throw new ArgumentNullException("schema");

            foreach (var child in schema.Children)
                Remove(child);

            var key = schema.Identifier.ToString();
            if (byIdentifier.ContainsKey(key))
                byIdentifier.Remove(key);
        }

        #endregion
    }
}
