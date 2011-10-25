using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Geography;

namespace Gnosis.Tests.Geography
{
    [TestFixture]
    public class RegionItems
    {
        const int count = 31;

        [Test]
        public void CanBeReadByCode()
        {
            var mapCode = new Dictionary<int, IRegion>();

            foreach (var region in Region.GetRegions())
            {
                mapCode.Add(region.Code, region);
                Assert.AreEqual(region, Region.GetRegionByCode(region.Code));
            }

            Assert.AreEqual(count, mapCode.Count);
            Assert.AreEqual(Region.Unknown, Region.GetRegionByCode(888));
        }

        [Test]
        public void CanBeReadByName()
        {
            var mapName = new Dictionary<string, IRegion>();

            foreach (var region in Region.GetRegions())
            {
                mapName.Add(region.Name, region);
                Assert.AreEqual(region, Region.GetRegionByName(region.Name));
            }

            Assert.AreEqual(count, mapName.Count);
            Assert.AreEqual(Region.Unknown, Region.GetRegionByName(null));
            Assert.AreEqual(Region.Unknown, Region.GetRegionByName("Narnia"));
        }
    }
}
