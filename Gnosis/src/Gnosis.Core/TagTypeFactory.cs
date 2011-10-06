using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagTypeFactory
        : ITagTypeFactory
    {
        public TagTypeFactory()
        {
        }

        private readonly IDictionary<long, ITagType> byId = new Dictionary<long, ITagType>();

        public ITagType Create(long id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : TagType.Default;
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
