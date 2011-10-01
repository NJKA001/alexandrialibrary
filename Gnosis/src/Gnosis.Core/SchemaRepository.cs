using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class SchemaRepository
        : ISchemaRepository
    {
        private IDictionary<string, ISchema> byIdentifier = new Dictionary<string, ISchema>();

        #region ISchemaRepository Members

        public ISchema Get(Uri identifier)
        {
            if (identifier == null)
                throw new ArgumentNullException("identifier");

            var key = identifier.ToString();

            return byIdentifier.ContainsKey(key) ?
                byIdentifier[key]
                : Schema.Default;
        }

        public void Add(ISchema schema)
        {
            if (schema == null)
                throw new ArgumentNullException("schema");

            var key = schema.Identifier.ToString();
            if (!byIdentifier.ContainsKey(key))
                byIdentifier.Add(key, schema);

            foreach (var child in schema.Children)
                Add(child);
        }

        public void Remove(ISchema schema)
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
