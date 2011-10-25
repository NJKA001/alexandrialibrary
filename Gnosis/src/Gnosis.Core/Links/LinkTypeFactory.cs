using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Links.Html;

namespace Gnosis.Core.Links
{
    public class LinkTypeFactory
        : ILinkTypeFactory
    {
        public LinkTypeFactory()
        {
            Add(LinkType.Default);
            Add(LinkType.ThumbnailImage);

            foreach (var htmlLinkType in HtmlLinkType.GetAll())
                Add(htmlLinkType);
        }

        private readonly IDictionary<int, ILinkType> byId = new Dictionary<int, ILinkType>();

        public ILinkType Create(int id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : LinkType.Default;
        }

        public void Add(ILinkType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            byId.Add(type.Id, type);
        }

        public void Remove(ILinkType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (byId.ContainsKey(type.Id))
                byId.Remove(type.Id);
        }
    }
}
