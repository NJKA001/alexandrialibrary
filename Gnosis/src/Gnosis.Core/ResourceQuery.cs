using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class ResourceQuery
        : IResourceQuery
    {
        public ResourceQuery(string query)
        {
            if (query == null)
                throw new ArgumentNullException(query);

            this.query = query;

            foreach (var pair in query.Split(new char[] { ';', '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var tokens = pair.Split('=');
                if (tokens == null || tokens.Length != 2 || string.IsNullOrEmpty(tokens[0]) || string.IsNullOrEmpty(tokens[1]))
                    continue;

                if (!map.ContainsKey(tokens[0]))
                    map[tokens[0]] = new List<string> { tokens[1] };
                else
                    map[tokens[0]].Add(tokens[1]);
            }
        }

        private readonly string query;
        private readonly IDictionary<string, IList<string>> map = new Dictionary<string, IList<string>>();

        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Parameters
        {
            get { return map.Select(x => new KeyValuePair<string, IEnumerable<string>>(x.Key, x.Value)); }
        }

        public bool ContainsKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return map.ContainsKey(key);
        }

        public IEnumerable<string> GetValues(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return map.ContainsKey(key) ?
                map[key]
                : Enumerable.Empty<string>();
        }
    }
}
