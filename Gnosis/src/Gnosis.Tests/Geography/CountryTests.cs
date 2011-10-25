using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Geography;

namespace Gnosis.Tests.Geography
{
    [TestFixture]
    public class CountryItems
    {
        [Test]
        public void CanBeReadByAlpha2Code()
        {
            var map2 = new Dictionary<string, ICountry>();
            
            foreach (var country in Country.GetCountries())
            {
                //logger.Debug("country name=" + country.Name + " alpha2=" + country.Alpha2Code + " tld=" + country.TopLevelDomain + " year=" + country.Year);

                map2.Add(country.Alpha2Code, country);                
                Assert.AreEqual(country, Country.GetCountryByCode(country.Alpha2Code));
            }

            Assert.AreEqual(Country.Unknown, Country.GetCountryByCode("ZZ"));
            Assert.AreEqual(Country.Unknown, Country.GetCountryByCode(null));
            Assert.AreEqual(249, map2.Count);
        }

        [Test]
        public void CanBeReadByAlpha3Code()
        {
            var map3 = new Dictionary<string, ICountry>();

            foreach (var country in Country.GetCountries().Where(x => !string.IsNullOrEmpty(x.Alpha3Code)))
            {
                map3.Add(country.Alpha3Code, country);
                Assert.AreEqual(country, Country.GetCountryByCode(country.Alpha3Code));
            }

            Assert.AreEqual(17, map3.Count);
        }

        [Test]
        public void CanBeReadByTopLevelDomain()
        {
            var mapTld = new Dictionary<string, ICountry>();

            foreach (var country in Country.GetCountries().Where(x => !string.IsNullOrEmpty(x.TopLevelDomain)))
            {
                mapTld.Add(country.TopLevelDomain, country);
                Assert.AreEqual(country, Country.GetCountryByTopLevelDomain(country.TopLevelDomain));
            }

            Assert.AreEqual(249, mapTld.Count);
            Assert.AreEqual(Country.Unknown, Country.GetCountryByTopLevelDomain(".zz"));
        }

        [Test]
        public void CanBeReadByNumber()
        {
            var mapNum = new Dictionary<int, ICountry>();

            foreach (var country in Country.GetCountries().Where(x => x.Number > -1))
            {
                mapNum.Add(country.Number, country);
                Assert.AreEqual(country, Country.GetCountryByNumber(country.Number));
            }

            Assert.AreEqual(17, mapNum.Count);
            Assert.AreEqual(Country.Unknown, Country.GetCountryByNumber(-1));
        }

        [Test]
        public void CanBeReadByName()
        {
            var mapName = new Dictionary<string, ICountry>();

            foreach (var country in Country.GetCountries())
            {
                mapName.Add(country.Name, country);
                Assert.AreEqual(country, Country.GetCountryByName(country.Name));
            }

            Assert.AreEqual(249, mapName.Count);
            Assert.AreEqual(Country.Unknown, Country.GetCountryByName("The Land of Oz"));
        }
    }
}
