using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core;
using Gnosis.Core.W3c;
using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedBuilder
        : IFeedBuilder
    {
        public FeedBuilder(Uri location, XmlDocument xml)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (xml == null)
                throw new ArgumentNullException("xml");

            this.location = location;
            this.xml = xml;
        }

        private readonly Uri location;
        private readonly XmlDocument xml;

        private string mediaType = string.Empty;
        private string title = string.Empty;
        private string authors = string.Empty;
        private string contributors = string.Empty;
        private string description = string.Empty;
        private ILanguageTag language = LanguageTag.Empty;

        private void ParseRssFeed()
        {
        }

        private void ParseAtomFeed()
        {
        }

        #region IFeedBuilder Members

        public IFeed ToFeed()
        {
            


            var feed = new Feed();
            feed.Initialize(new EntityInitialState());
            feed.MediaType = mediaType;
            feed.Title = title;
            return feed;
        }

        #endregion
    }
}
