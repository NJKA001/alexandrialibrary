using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core;
using Gnosis.Core.Ietf;
using Gnosis.Core.Iso;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedBuilder
        : IFeedBuilder
    {
        public FeedBuilder(IContext context, ILogger logger, Uri location, XmlDocument xml)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (location == null)
                throw new ArgumentNullException("location");
            if (xml == null)
                throw new ArgumentNullException("xml");

            this.context = context;
            this.logger = logger;
            this.location = location;
            this.xml = xml;
        }

        private readonly IContext context;
        private readonly ILogger logger;
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
            feed.Initialize(new EntityInitialState(context, logger));
            feed.MediaType = mediaType;
            feed.Title = title;
            return feed;
        }

        #endregion
    }
}
