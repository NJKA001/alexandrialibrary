using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class MediaTypeTests
    {
        [Test]
        public void LookupMediaType()
        {
            const int total = 4;
            var list = new List<IMediaType>();

            foreach (var mediaType in MediaType.GetMediaTypes())
            {
                list.Add(mediaType);

                Assert.AreEqual(mediaType, MediaType.Parse(mediaType.ToString()));

                var byTypeList = MediaType.GetMediaTypesByType(mediaType.Type);
                Assert.IsNotNull(byTypeList);
                Assert.IsTrue(byTypeList.Contains(mediaType));

                foreach (var fileExtension in mediaType.FileExtensions)
                {
                    var types = MediaType.GetMediaTypesByFileExtension(fileExtension);
                    Assert.IsNotNull(types);
                    Assert.IsTrue(types.Contains(mediaType));
                }
            }

            Assert.AreEqual(total, list.Count());
        }
    }
}
