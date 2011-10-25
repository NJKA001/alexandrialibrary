using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags
{
    public class TagSchema
        : ITagSchema
    {
        public TagSchema(int id, string name, IAlgorithm algorithm)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");

            this.id = id;
            this.name = name;
            this.algorithm = algorithm;
        }

        private readonly int id;
        private readonly string name;
        private readonly IAlgorithm algorithm;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public IAlgorithm Algorithm
        {
            get { return algorithm; }
        }

        static TagSchema()
        {
            all.Add(Default);
            all.Add(Americanized);
            all.Add(Id3v1);
            all.Add(Id3v2);

            foreach (var schema in all)
                byId[schema.Id] = schema;
        }

        private static readonly IList<ITagSchema> all = new List<ITagSchema>();
        private static readonly IDictionary<int, ITagSchema> byId = new Dictionary<int, ITagSchema>();

        public static readonly ITagSchema Default = new TagSchema(1, "Default", Core.Algorithm.Default);
        public static readonly ITagSchema Americanized = new TagSchema(21, "Americanized", Core.Algorithm.Americanized);
        public static readonly ITagSchema Id3v1 = new TagSchema(101, "ID3v1", Core.Algorithm.Default);
        public static readonly ITagSchema Id3v2 = new TagSchema(102, "ID3v2", Core.Algorithm.Default);

        public static IEnumerable<ITagSchema> GetAll()
        {
            return all;
        }

        public static ITagSchema Parse(int id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : null;
        }
    }
}
