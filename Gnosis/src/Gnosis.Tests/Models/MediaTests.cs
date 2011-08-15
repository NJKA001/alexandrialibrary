using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class MediaTests
    {
        [Test]
        public void LookupMedia()
        {
            var byName = new Dictionary<string, IMedia>();

            foreach (var media in Media.GetMedia())
            {
                Assert.IsNotNull(media);
                Assert.AreEqual(media, Media.Parse(media.Name));
                byName.Add(media.Name, media);
            }
        }
    }
}
