using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Time;

using NUnit.Framework;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class Rfc822DateTimeTests
    {
        [Test]
        public void ParseDateTimeWithNumericOffset()
        {
            var s = "Wed, 29 Jun 2011 11:47:00 -0500";
            var rfc822Date = DateTime.MinValue;
            Rfc822DateTime.TryParse(s, out rfc822Date);
            var date = new DateTime(2011, 6, 29, 16, 47, 00);
            Assert.AreEqual(date, rfc822Date);
        }

        [Test]
        public void ParseDateTimeWithTimeZoneName()
        {
            var s = "June 21, 2011 16:54:20 PDT";
            var rfc822Date = DateTime.MinValue;
            Rfc822DateTime.TryParse(s, out rfc822Date);
            var date = new DateTime(2011, 6, 21, 23, 54, 20);
            Assert.AreEqual(date, rfc822Date);
        }
    }
}
