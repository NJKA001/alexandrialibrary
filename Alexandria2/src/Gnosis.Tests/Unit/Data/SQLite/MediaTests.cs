﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Data.SQLite;
using Gnosis.Utilities;

namespace Gnosis.Tests.Unit.Data.SQLite
{
    public abstract class MediaTestBase
    {
        protected MediaTestBase()
        {
            logger = new DebugLogger();
            mediaFactory = new MediaFactory(logger);

            connection = new SQLiteConnectionFactory().Create("Data Source=:memory:;Version=3;");
            connection.Open();
            repository = new SQLiteMediaRepository(logger, mediaFactory, connection);
            repository.Initialize();
        }

        private readonly IDbConnection connection;
        protected readonly ILogger logger;
        protected readonly ICharacterSetFactory characterSetFactory;
        protected readonly IMediaRepository repository;
        //protected readonly IMediaFactory mediaFactory;
        protected readonly IMediaFactory mediaFactory;

        #region Test Values

        protected readonly Uri uri1 = new Uri("http://arstechnica.com/index.ars");
        protected readonly Uri uri2 = new Uri(@"C:\Users\dpoage\Music\Tool\Undertown\Bottom.mp3");
        protected readonly Uri uri3 = new Uri("http://flickr.com/users/dpoage/example.jpg");
        protected readonly Uri uri4 = new Uri(@"C:\Users\dpoage\Pictures\icon.gif");

        #endregion

        protected IMedia CreateMedia(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            //var type = mediaFactory.GetByLocation(location);

            //if (type == null)
            //    throw new InvalidOperationException("Could not determine media type for location: " + location.ToString());

            return mediaFactory.Create(location);
        }

        protected void Cleanup()
        {
            connection.Close();
        }
    }

    [TestFixture]
    public class SavedMedia
        : MediaTestBase
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Cleanup();
        }

        [Test]
        public void CanBeRead()
        {
            var media1 = CreateMedia(uri1);
            var media2 = CreateMedia(uri2);
            var media3 = CreateMedia(uri3);

            Assert.IsNotNull(media1);
            Assert.IsNotNull(media1.Type);
            System.Diagnostics.Debug.WriteLine("media1 type=" + media1.Type.Name);

            repository.Save(new List<IMedia> { media1, media2, media3 });

            var check1 = repository.Lookup(uri1);
            var check2 = repository.Lookup(uri2);
            var check3 = repository.Lookup(uri3);

            Assert.IsNotNull(check1);
            Assert.IsNotNull(check2);
            Assert.IsNotNull(check3);
        }

        [Test]
        public void CanBeDeleted()
        {
            var media1 = CreateMedia(uri1);
            var media2 = CreateMedia(uri2);
            var media3 = CreateMedia(uri3);

            repository.Save(new List<IMedia> { media1, media2, media3 });

            repository.Delete(new List<Uri> { uri1, uri3 });

            var check1 = repository.Lookup(uri1);
            var check2 = repository.Lookup(uri2);
            var check3 = repository.Lookup(uri3);

            Assert.IsNull(check1);
            Assert.IsNotNull(check2);
            Assert.IsNull(check3);
        }
    }
}
