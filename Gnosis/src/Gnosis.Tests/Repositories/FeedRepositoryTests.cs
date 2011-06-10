using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;
using System.Data.SQLite;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Alexandria.Repositories.Feeds;

namespace Gnosis.Tests.Repositories
{
    [TestFixture]
    public class FeedRepositoryTests
    {
        private IContext context;
        private ILogger logger;
        private IFeedRepository repository;
        private IDbConnection connection;

        private readonly Uri feedLocation = new Uri("http://espn.go.com/espnradio/feeds/rss/podcast.xml?id=2864045");

        #region Helper Methods

        private IFeed GetTestFeed()
        {
            var feed = new Feed();
            feed.Initialize(new EntityInitialState(context, logger));
            feed.Authors = "Bill Simmons";
            feed.Contributors = "Joe House, Marc Stein, John Hollinger";
            feed.Copyright = "c 2009-2011";
            feed.Description = "Sports etc.";
            feed.FeedIdentifier = "12345ABC";
            feed.Generator = "espn.go.com";
            feed.Language = "en-us";
            feed.Location = feedLocation;
            feed.MediaType = "application/xml+rss";
            feed.OriginalLocation = new Uri("http://espn.go.com/espnradio/feeds/rss/podcast.xml?id=2864045");
            feed.PublishedDate = new DateTime(2009, 2, 13);
            feed.UpdatedDate = new DateTime(2011, 6, 7);
            feed.Title = "BS Report";
            feed.IconPath = new Uri("http://assets.espn.go.com/i/espnradio/podcast/bsreport_subway_300.jpg");
            feed.ImagePath = new Uri("http://assets.espn.go.com/i/espnradio/podcast/bsreport_subway_300.jpg");
            feed.AddCategory(new FeedCategory(feed.Id, UriExtensions.EmptyUri, "Sports", "Sports"));
            feed.AddCategory(new FeedCategory(feed.Id, UriExtensions.EmptyUri, "Comedy", "Comedy"));
            feed.AddLink(new FeedLink(feed.Id, "self", new Uri("http://espn.go.com/espnradio/feeds/rss/podcast.xml?id=2864045"), "application/xml+rss", 0, "en-us"));
            feed.AddLink(new FeedLink(feed.Id, "alt", new Uri("http://espn.go.com/espnradio"), "text/html", 0, "en-us"));
            feed.AddLink(new FeedLink(feed.Id, "alt", new Uri("http://espn.go.com/epsnradio?lang=es-mx"), "text/html", 0, "es-mx"));
            feed.AddMetadatum(new FeedMetadata(feed.Id, "text/plain", UriExtensions.EmptyUri, "tag", "Bill Simmons"));
            feed.AddMetadatum(new FeedMetadata(feed.Id, "application/xml", UriExtensions.EmptyUri, "marquee", "<marquee><title>BS Report</title><subtitle>with Bill Simmons</subtitle></marquee>"));

            var item = new FeedItem();
            item.Initialize(new EntityInitialState(context, logger, feed.Id, 0));
            item.Authors = "Bill Simmons";
            item.Contributors = "Joe House, Joe Mead";
            item.Copyright = "Copyright ESPN 2011";
            item.FeedItemIdentifier = "ZYZ235AMQ3792206";
            item.Summary = "Bill previews the NBA Finals with ESPN experts";
            item.PublishedDate = new DateTime(2011, 6, 5);
            item.UpdatedDate = new DateTime(2011, 6, 5);
            item.Title = "NBA Finals Preview (Part 1)";
            item.TitleMediaType = "text/plain";
            //item.AddCategory(new Models.Feeds.FeedCategory(item.Id, UriExtensions.EmptyUri, "Basketball", "Basketball"));
            item.AddLink(new FeedLink(item.Id, "self", new Uri("http://espn.go.com/espnradio/media/xyz1.mp3"), "audio/mpeg", 0, "en-us"));
            item.AddMetadatum(new FeedMetadata(item.Id, "text/plain", UriExtensions.EmptyUri, "rating", "4/5"));
            item.AddMetadatum(new FeedMetadata(item.Id, "application/xml", UriExtensions.EmptyUri, "rating", "<rating><score>4</score><max>5</max></rating>"));
            feed.AddItem(item);

            var item2 = new FeedItem();
            item2.Initialize(new EntityInitialState(context, logger, feed.Id, 0));
            item2.Authors = "Bill Simmons";
            item2.Contributors = "Joe House, Joe Mead";
            item2.Copyright = "Copyright ESPN 2011";
            item2.FeedItemIdentifier = "ZYZ235AMQ38564587";
            item2.Summary = "Bill previews the NBA Finals with ESPN experts";
            item2.PublishedDate = new DateTime(2011, 6, 5);
            item2.UpdatedDate = new DateTime(2011, 6, 5);
            item2.Title = "NBA Finals Preview (Part 2)";
            item2.TitleMediaType = "text/plain";
            item2.AddCategory(new FeedCategory(item2.Id, UriExtensions.EmptyUri, "Basketball", "Basketball"));
            item2.AddLink(new FeedLink(item2.Id, "self", new Uri("http://espn.go.com/espnradio/media/xyz2.mp3"), "audio/mpeg", 0, "en-us"));
            item2.AddMetadatum(new FeedMetadata(item2.Id, "text/plain", UriExtensions.EmptyUri, "rating", "4/5"));
            item2.AddMetadatum(new FeedMetadata(item2.Id, "application/xml", UriExtensions.EmptyUri, "rating", "<rating><score>4</score><max>5</max></rating>"));
            feed.AddItem(item2);

            return feed;
        }

        private void ModifyTestFeed(IFeed feed)
        {
            feed.Title = "Some other title";
            feed.FeedIdentifier = "NEW-ID-5634634636-NEW";
            feed.Generator = "other-generator";

            var firstCategory = feed.Categories.FirstOrDefault();
            feed.RemoveCategory(firstCategory);


            feed.RemoveItem(feed.Items.LastOrDefault());
            feed.Items.FirstOrDefault().RemoveMetadatum(feed.Items.FirstOrDefault().Metadata.FirstOrDefault());
            feed.Items.FirstOrDefault().AddCategory(new FeedCategory(feed.Items.FirstOrDefault().Id, new Uri("http://example.com/scheme"), "Misc.", "Misc."));
            feed.AddMetadatum(new FeedMetadata(feed.Id, "text/html", new Uri("http://other.com/1/different-scheme"), "review", "<html><body><p>This feed is awesome</p><p>I <b>love</b> it so much!</p></body></html>"));
            //feedRepository.Save(new List<Models.Feeds.IFeed> { feed });
        }

        #endregion

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            context = new SingleThreadedContext();
            logger = new DebugLogger();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
        }

        [SetUp]
        public void SetUp()
        {
            connection = new SQLiteConnection("Data Source=:memory:;Version=3;");
            connection.Open();

            repository = new FeedRepository(context, logger, connection);
            repository.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }

        [Test]
        public void TestCreateFeed()
        {
            var initialFeeds = repository.Search();
            Assert.AreEqual(0, initialFeeds.Count());

            var feed = GetTestFeed();

            Assert.IsTrue(feed.IsNew());

            repository.Save(new List<IFeed> { feed });

            Assert.IsFalse(feed.IsNew());

            var createdFeed = repository.LookupByLocation(feedLocation);

            Assert.IsNotNull(createdFeed);

            repository.Delete(new List<IFeed> { feed });

            var deletedFeed = repository.LookupByLocation(feedLocation);

            Assert.IsNull(deletedFeed);
        }

        [Test]
        public void TestChangeFeed()
        {
            var feed = GetTestFeed();
            var id = feed.Id;

            repository.Save(new List<IFeed> { feed });

            const string title = "New Title";
            const string description = "Updated Description";
            const string copyright = "Copyright 2000";
            const string authors = "Tweedle Dee and Tweedle Dumb";
            const string contributors = "Larry, Moe and Curly";
            const string feedIdentifier = "1234WXYZ7890ABCD"; 
            const string generator = "some-generator-name-XYZ";
            const string iconPath = "http://example.com/icons/path.jpg";
            const string imagePath = "http://exmaple.com/images/path.jpg";
            const string language = "es-es";
            const string location = "http://example.com/feeds/rss/example.xml";
            const string mediaType = "application/xml";
            var publishedDate = new DateTime(2011, 2, 13);
            var updatedDate = new DateTime(2011, 4, 27);

            feed.Title = title;
            feed.Description = description;
            feed.Copyright = copyright;
            feed.Authors = authors;
            feed.Contributors = contributors;
            feed.FeedIdentifier = feedIdentifier;
            feed.Generator = generator;
            feed.IconPath = new Uri(iconPath);
            feed.ImagePath = new Uri(imagePath);
            feed.Language = language;
            feed.Location = new Uri(location);
            feed.MediaType = mediaType;
            feed.OriginalLocation = new Uri(location);
            feed.PublishedDate = publishedDate;
            feed.UpdatedDate = updatedDate;

            Assert.IsTrue(feed.IsChanged());
            Assert.IsFalse(feed.IsNew());

            var category = new FeedCategory(id, new Uri("http://example.com/some-random-scheme/xyz"), "Test Name", "Test Label");
            feed.AddCategory(category);
            Assert.IsTrue(category.IsNew());

            var oldTimeStamp = feed.TimeStamp;
            repository.Save(new List<IFeed>{ feed});
            var newTimeStamp = feed.TimeStamp;
            Assert.IsFalse(feed.IsChanged());
            Assert.AreNotEqual(oldTimeStamp, newTimeStamp);
            
            var changedFeed = repository.Lookup(id);
            Assert.IsNotNull(changedFeed);
            Assert.AreEqual(title, changedFeed.Title);
            Assert.AreEqual(description, changedFeed.Description);
            Assert.AreEqual(copyright, changedFeed.Copyright);
            Assert.AreEqual(authors, changedFeed.Authors);
            Assert.AreEqual(contributors, changedFeed.Contributors);
            Assert.AreEqual(feedIdentifier, changedFeed.FeedIdentifier);
            Assert.AreEqual(generator, changedFeed.Generator);
            Assert.AreEqual(iconPath, changedFeed.IconPath.ToString());
            Assert.AreEqual(imagePath, changedFeed.ImagePath.ToString());
            Assert.AreEqual(language, changedFeed.Language);
            Assert.AreEqual(location, changedFeed.Location.ToString());
            Assert.AreEqual(mediaType, changedFeed.MediaType);
            Assert.AreEqual(publishedDate, changedFeed.PublishedDate);
            Assert.AreEqual(updatedDate, changedFeed.UpdatedDate);
            Assert.IsNotNull(changedFeed.Categories.Where(x => x.Name == "Test Name").FirstOrDefault());
            Assert.AreEqual(3, changedFeed.Categories.Count());
        }
    }
}
