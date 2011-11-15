using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Application.Xml;

namespace Gnosis.Tests.Unit.Application.Xml
{
    [TestFixture]
    public class StyleMediaItems
    {
        [Test]
        public void CanBeParsedByName()
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
