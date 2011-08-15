using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Region
        : IRegion
    {
        private Region(int code, string name)
        {
            this.code = code;
            this.name = name;
        }

        private readonly int code;
        private readonly string name;

        #region IRegion Members

        public int Code
        {
            get { return code; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        public override string ToString()
        {
            return code.ToString();
        }

        static Region()
        {
            InitializeRegions();

            foreach (var region in regions)
            {
                byCode.Add(region.Code, region);
                byName.Add(region.Name.ToUpper(), region);
            }
        }

        private static readonly IList<IRegion> regions = new List<IRegion>();
        private static readonly IDictionary<int, IRegion> byCode = new Dictionary<int, IRegion>();
        private static readonly IDictionary<string, IRegion> byName = new Dictionary<string, IRegion>();

        #region InitializeRegions

        private static void InitializeRegions()
        {
            regions.Add(World);
            regions.Add(Africa);
            regions.Add(NorthernAfrica);
            regions.Add(EasternAfrica);
            regions.Add(MiddleAfrica);
            regions.Add(SouthernAfrica);
            regions.Add(WesternAfrica);
            regions.Add(Americas);
            regions.Add(LatinAmericaAndTheCaribbean);
            regions.Add(Caribbean);
            regions.Add(CentralAmerica);
            regions.Add(SouthAmerica);
            regions.Add(NorthernAmerica);
            regions.Add(Asia);
            regions.Add(CentralAsia);
            regions.Add(EasternAsia);
            regions.Add(SouthernAsia);
            regions.Add(SouthEasternAsia);
            regions.Add(WesternAsia);
            regions.Add(Europe);
            regions.Add(EasternEurope);
            regions.Add(NorthernEurope);
            regions.Add(ChannelIslands);
            regions.Add(SouthernEurope);
            regions.Add(WesternEurope);
            regions.Add(Oceania);
            regions.Add(AustraliaAndNewZealand);
            regions.Add(Melanesia);
            regions.Add(Micronesia);
            regions.Add(Polynesia);
            regions.Add(Unknown);
        }

        #endregion

        #region Public Static Methods

        public static IRegion GetRegionByCode(int code)
        {
            return byCode.ContainsKey(code) ? byCode[code] : Unknown;
        }

        public static IRegion GetRegionByName(string name)
        {
            if (name == null)
                return Unknown;

            var upper = name.ToUpper();

            return byName.ContainsKey(upper) ? byName[upper] : Unknown;
        }

        public static IEnumerable<IRegion> GetRegions()
        {
            return regions;
        }

        #endregion

        #region Regions

        public static readonly IRegion World = new Region(001, "World");
        public static readonly IRegion Africa = new Region(002, "Africa");
        public static readonly IRegion NorthernAfrica = new Region(015, "Northern Africa");
        public static readonly IRegion EasternAfrica = new Region(014, "Eastern Africa");
        public static readonly IRegion MiddleAfrica = new Region(017, "Middle Africa");
        public static readonly IRegion SouthernAfrica = new Region(018, "Southern Africa");
        public static readonly IRegion WesternAfrica = new Region(011, "Western Africa");
        public static readonly IRegion Americas = new Region(019, "Americas");
        public static readonly IRegion LatinAmericaAndTheCaribbean = new Region(419, "Latin America and the Caribbean");
        public static readonly IRegion Caribbean = new Region(029, "Caribbean");
        public static readonly IRegion CentralAmerica = new Region(013, "Central America");
        public static readonly IRegion SouthAmerica = new Region(005, "South America");
        public static readonly IRegion NorthernAmerica = new Region(021, "Northern America");
        public static readonly IRegion Asia = new Region(142, "Asia");
        public static readonly IRegion CentralAsia = new Region(143, "Central Asia");
        public static readonly IRegion EasternAsia = new Region(030, "Eastern Asia");
        public static readonly IRegion SouthernAsia = new Region(034, "Southern Asia");
        public static readonly IRegion SouthEasternAsia = new Region(035, "South-Eastern Asia");
        public static readonly IRegion WesternAsia = new Region(145, "Western Asia");
        public static readonly IRegion Europe = new Region(150, "Europe");
        public static readonly IRegion EasternEurope = new Region(151, "Eastern Europe");
        public static readonly IRegion NorthernEurope = new Region(154, "Northern Europe");
        public static readonly IRegion ChannelIslands = new Region(830, "Channel Islands");
        public static readonly IRegion SouthernEurope = new Region(039, "Southern Europe");
        public static readonly IRegion WesternEurope = new Region(155, "Western Europe");
        public static readonly IRegion Oceania = new Region(009, "Oceania");
        public static readonly IRegion AustraliaAndNewZealand = new Region(053, "Australia and New Zealand");
        public static readonly IRegion Melanesia = new Region(054, "Melanesia");
        public static readonly IRegion Micronesia = new Region(057, "Micronesia");
        public static readonly IRegion Polynesia = new Region(061, "Polynesia");
        public static readonly IRegion Unknown = new Region(0, "Unknown");

        #endregion
    }
}
