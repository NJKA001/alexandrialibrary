using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Document.Xml;

namespace Gnosis.Tests.Core.Document.Xml
{
    [TestFixture]
    public class StyleMediaTests
    {
        [Test]
        public void LookupStyleMedia()
        {
            var byName = new Dictionary<string, IStyleMedia>();

            foreach (var media in StyleMedia.GetMedia())
            {
                Assert.IsNotNull(media);
                Assert.AreEqual(media, StyleMedia.Parse(media.Name));
                byName.Add(media.Name, media);
            }
        }
    }
}
