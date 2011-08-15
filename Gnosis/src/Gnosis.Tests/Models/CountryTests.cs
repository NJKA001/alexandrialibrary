using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class CountryTests
    {
        //private ILogger logger = new DebugLogger();

        [Test]
        public void LookupCountry()
        {
            var map2 = new Dictionary<string, ICountry>();
            var map3 = new Dictionary<string, ICountry>();
            var mapTld = new Dictionary<string, ICountry>();
            var mapNum = new Dictionary<int, ICountry>();
            var mapName = new Dictionary<string, ICountry>();

            foreach (var country in Country.GetCountries())
            {
                //logger.Debug("country name=" + country.Name + " alpha2=" + country.Alpha2Code + " tld=" + country.TopLevelDomain + " year=" + country.Year);

                map2.Add(country.Alpha2Code, country);
                mapName.Add(country.Name, country);
                mapTld.Add(country.TopLevelDomain, country);
                Assert.AreEqual(country, Country.GetCountryByCode(country.Alpha2Code));
                Assert.AreEqual(country, Country.GetCountryByName(country.Name));
                Assert.AreEqual(country, Country.GetCountryByTopLevelDomain(country.TopLevelDomain));

                if (!string.IsNullOrEmpty(country.Alpha3Code))
                {
                    map3.Add(country.Alpha3Code, country);
                    Assert.AreEqual(country, Country.GetCountryByCode(country.Alpha3Code));
                }

                if (country.Number > -1)
                {
                    mapNum.Add(country.Number, country);
                    Assert.AreEqual(country, Country.GetCountryByNumber(country.Number));
                }
            }

            Assert.AreEqual(Country.Unknown, Country.GetCountryByCode("ZZ"));
            Assert.AreEqual(Country.Unknown, Country.GetCountryByCode(null));
            Assert.AreEqual(Country.Unknown, Country.GetCountryByName("The Land of Oz"));
            Assert.AreEqual(Country.Unknown, Country.GetCountryByNumber(-1));
            Assert.AreEqual(Country.Unknown, Country.GetCountryByTopLevelDomain(".zz"));

            Assert.AreEqual(249, map2.Count);
            Assert.AreEqual(249, mapName.Count);
            Assert.AreEqual(249, mapTld.Count);
            Assert.AreEqual(17, map3.Count);
            Assert.AreEqual(17, mapNum.Count);
        }
    }
}
