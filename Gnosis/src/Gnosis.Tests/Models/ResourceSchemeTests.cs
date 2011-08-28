using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

using NUnit.Framework;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class ResourceSchemeTests
    {
        [Test]
        public void LookupResourceSchemes()
        {
            foreach (var scheme in ResourceScheme.Schemes)
            {
                Assert.IsNotNull(scheme.Name);
                Assert.IsNotNull(scheme.Description);

                var lookup = ResourceScheme.Parse(scheme.Name);
                Assert.AreEqual(scheme, lookup);
            }

            var unrecognized = ResourceScheme.Parse("wtfbbq");
            Assert.IsNotNull(unrecognized);
            Assert.IsFalse(unrecognized.IsOfficial);
            Assert.IsFalse(unrecognized.IsRecognized);
        }
    }
}
