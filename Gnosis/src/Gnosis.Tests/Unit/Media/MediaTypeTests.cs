using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Media
{
    [TestFixture]
    public class MediaTypeItems
    {
        public MediaTypeItems()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            characterSetFactory = new CharacterSetFactory();
            //mediaFactory = new MediaFactory(characterSetFactory);
            contentTypeFactory = new ContentTypeFactory(logger, characterSetFactory);
        }

        private ILogger logger;
        private ICharacterSetFactory characterSetFactory;
        //private IMediaFactory mediaFactory;
        private IContentTypeFactory contentTypeFactory;

        /*
        [Test]
        public void CanBeReadByName()
        {
            var list = new List<string>();

            foreach (var mediaType in mediaFactory.GetMediaTypes())
            {
                Assert.IsNotNull(mediaType);
                list.Add(mediaType);

                Assert.AreEqual(mediaType, contentTypeFactory.GetByCode(mediaType).Name);
                //Assert.AreEqual(mediaType, contentTypeFactory.GetByCode(mediaType.Supertype.ToString().ToLower() + "/" + mediaType.Subtype));

            }
        }*/

        /*
        [Test]
        public void CanBeReadByFileExtension()
        {
            foreach (var mediaType in mediaFactory.GetMediaTypes())
            {
                //foreach (var fileExtension in mediaType.FileExtensions)
                //{
                //    var types = mediaTypeFactory.GetByFileExtension(fileExtension);
                //    Assert.IsNotNull(types);
                //    Assert.IsTrue(types.Contains(mediaType));
                //}
            }
        }
        */

        [Test]
        public void CanBeReadBySupertype()
        {
            //foreach (var mediaType in mediaTypeFactory.GetAll())
            //{
            //    var byTypeList = mediaTypeFactory.GetBySupertype(mediaType.Supertype);
            //    Assert.IsNotNull(byTypeList);
            //    Assert.IsTrue(byTypeList.Contains(mediaType));
            //}
        }

        [Test]
        public void CanBeReadByMagicNumber()
        {
            //var byMagicNumber = new Dictionary<byte[], IMediaType>();

            //foreach (var mediaType in mediaTypeFactory.GetAll())
            //{
            //    foreach (var magicNumber in mediaType.MagicNumbers)
            //    {
            //        byMagicNumber.Add(magicNumber, mediaType);
            //        var check = mediaTypeFactory.GetByMagicNumber(magicNumber);
            //        Assert.AreEqual(mediaType, check);
            //    }
            //}
        }

        [Test]
        public void CanGetMediaTypeForLocalPng()
        {
            var path = @".\Files\radiohead.png";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("image/png", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForLocalPngWithInvalidFileExtension()
        {
            var path = @".\Files\radiohead";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("image/png", mediaType.Name);
        }

        //TODO: Choose another remote PNG resource, the one at this URL is SLOW
        //[Test]
        //public void GetMediaTypeForRemotePng()
        //{
        //    var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/280px-PNG_transparency_demonstration_1.png");
        //    var mediaType = location.ToMediaType();
        //    Assert.AreEqual(MediaType.ImagePng, mediaType);
        //}

        [Test]
        public void CanGetMediaTypeForLocalGif()
        {
            var path = @".\Files\nin.gif";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("image/gif", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForLocalGifWithInvalidFileExtension()
        {
            var path = @".\Files\nin";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("image/gif", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForLocalJpeg()
        {
            var path = @".\Files\Undertow.jpg";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("image/jpeg", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForLocalJpegWithInvalidFileExtension()
        {
            var path = @".\Files\Undertow";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("image/jpeg", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForLocalMp3ByMagicNumber()
        {
            var path = @".\Files\03 - Antes De Las Seis.mp3";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("audio/mpeg", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForLocalMp3ByMagicNumber2()
        {
            var path = @".\Files\13 - Loca (Featuring Dizzee Rascal).mp3";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("audio/mpeg", mediaType.Name);
        }
    }
}
