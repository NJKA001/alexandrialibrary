using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Tags.Id3;
using Gnosis.Core.Tags.Id3.Id3v1;
using Gnosis.Core.Tags.Id3.Id3v2;

namespace Gnosis.Core.Tags
{
    public class TagTypeFactory
        : ITagTypeFactory
    {
        public TagTypeFactory()
        {
            foreach (var defType in TagType.GetAllDefault())
                Add(defType);

            foreach (var id3v1Type in Id3v1TagType.GetAll())
                Add(id3v1Type);

            foreach (var id3v2Type in Id3v2TagType.GetAll())
                Add(id3v2Type);
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
