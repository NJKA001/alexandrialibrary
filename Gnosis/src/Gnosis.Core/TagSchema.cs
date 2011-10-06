using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagSchema
        : ITagSchema
    {
        public TagSchema(long id, string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.id = id;
            this.name = name;
        }

        private readonly long id;
        private readonly string name;

        public long Id
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
        }

        private static readonly IList<ITagSchema> all = new List<ITagSchema>();

        public static readonly ITagSchema Default = new TagSchema(1, "Default");
        public static readonly ITagSchema Id3v1 = new TagSchema(2, "ID3v1");
        public static readonly ITagSchema Id3v2 = new TagSchema(3, "ID3v2");

        public static IEnumerable<ITagSchema> GetAll()
        {
            return all;
        }
    }
}
