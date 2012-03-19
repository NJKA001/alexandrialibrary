using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags
{
    public class TagTypeFactory
        : ITagTypeFactory
    {
        public TagTypeFactory()
        {
            foreach (var type in TagType.GetAll())
                Add(type);
        }

        private readonly IDictionary<int, ITagType> byId = new Dictionary<int, ITagType>();

        public ITagType Create(int id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : TagType.DefaultString;
        }

        public void Add(ITagType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            byId.Add(type.Id, type);
        }

        public void Remove(ITagType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (byId.ContainsKey(type.Id))
                byId.Remove(type.Id);
        }
    }
}
