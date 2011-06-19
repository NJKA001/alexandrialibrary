using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.UN;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class RegionTests
    {
        [Test]
        public void LookupRegion()
        {
            const int count = 31;

            var mapCode = new Dictionary<int, IRegion>();
            var mapName = new Dictionary<string, IRegion>();

            foreach (var region in Region.GetRegions())
            {
                mapCode.Add(region.Code, region);
                Assert.AreEqual(region, Region.GetRegionByCode(region.Code));

                mapName.Add(region.Name, region);
                Assert.AreEqual(region, Region.GetRegionByName(region.Name));
            }

            Assert.AreEqual(Region.Unknown, Region.GetRegionByCode(888));
            Assert.AreEqual(Region.Unknown, Region.GetRegionByName(null));
            Assert.AreEqual(Region.Unknown, Region.GetRegionByName("Narnia"));

            Assert.AreEqual(count, mapCode.Count);
            Assert.AreEqual(count, mapName.Count);
        }
    }
}
