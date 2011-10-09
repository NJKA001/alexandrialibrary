using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagSchema
        : ITagSchema
    {
        public TagSchema(int id, string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.id = id;
            this.name = name;
        }

        private readonly int id;
        private readonly string name;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        static TagSchema()
        {
            all.Add(Default);
            all.Add(Id3v1);
            all.Add(Id3v2);

            foreach (var schema in all)
                byId[schema.Id] = schema;
        }

        private static readonly IList<ITagSchema> all = new List<ITagSchema>();
        private static readonly IDictionary<int, ITagSchema> byId = new Dictionary<int, ITagSchema>();

        public static readonly ITagSchema Default = new TagSchema(1, "Default");
        public static readonly ITagSchema Id3v1 = new TagSchema(2, "ID3v1");
        public static readonly ITagSchema Id3v2 = new TagSchema(3, "ID3v2");

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
