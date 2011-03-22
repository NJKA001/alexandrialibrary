using System;
using System.Collections.Generic;
using System.Linq;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite;
using Gnosis.Babel.SQLite.Persist.Inserting;

namespace Gnosis.Alexandria.Models.Repositories
{
    public class CountryRepository : RepositoryBase<ICountry>, ICountryRepository
    {
        public CountryRepository(IStore store, ICache<ICountry> cache, IFactory<ICountry> factory, ISchema<ICountry> schema, ISchemaMapper<ICountry> schemaMapper, IModelMapper<ICountry> modelMapper, IPersistMapper<ICountry> persistMapper, IQueryMapper<ICountry> queryMapper, IFactory<ICommand> commandFactory, ISQLiteStatementFactory statementFactory, IFactory<IBatch> batchFactory, IFactory<IQuery<ICountry>> queryFactory)
            : base(store, cache, factory, schema, schemaMapper, modelMapper, persistMapper, queryMapper, commandFactory, statementFactory, batchFactory, queryFactory)
        {
        }

        #region Cache Methods

        private void AddCountryToCache(ICountry country)
        {
            Cache.Put(country.Id, country);
        }

        private void AddCountryToCache(long id, string name, string code)
        {
            var country = Factory.Create();
            country.Populate(new Dictionary<string, object> { { "Name", name }, { "Code", code } });
            country.Initialize(id);
            AddCountryToCache(country);
        }

        private void AddCountryToCache(long id, string name, string code, DateTime fromDate)
        {
            var country = Factory.Create();
            country.Populate(new Dictionary<string, object> {{"Name", name}, {"Code", code}, {"FromDate", fromDate} });
            country.Initialize(id);
            AddCountryToCache(country);
        }

        private void AddCountryToCache(long id, string name, string code, DateTime fromDate, DateTime toDate)
        {
            var country = Factory.Create();
            country.Populate(new Dictionary<string, object> { { "Name", name }, { "Code", code }, { "FromDate", fromDate }, {"ToDate", toDate} });
            country.Initialize(id);
            AddCountryToCache(country);
        }

        private void CacheCountries()
        {
            AddCountryToCache(Country.Unknown);
            AddCountryToCache(2, "Afghanistan", "AF", new DateTime(1919, 8, 19));
            AddCountryToCache(3, "Åland Islands", "AX", new DateTime(1920, 1, 1));
            AddCountryToCache(4, "Albania", "AL", new DateTime(1912, 11, 28));
            AddCountryToCache(5, "Algeria", "DZ", new DateTime(1962, 7, 5));
            AddCountryToCache(6, "American Samoa", "AS", new DateTime(1899, 1, 1));
            AddCountryToCache(7, "Andorra", "AD", new DateTime(1278, 1, 1));
            AddCountryToCache(8, "Angola", "AO", new DateTime(1975, 11, 11));
            AddCountryToCache(9, "Anguilla", "AI", new DateTime(1980, 1, 1));
            AddCountryToCache(10, "Antarctica", "AQ", new DateTime(1951, 12, 1));
            AddCountryToCache(11, "Antigua and Barbuda", "AG");
            AddCountryToCache(12, "Argentina", "AR");
            AddCountryToCache(13, "Armenia", "AM");
            AddCountryToCache(14, "Aruba", "AW");
            AddCountryToCache(15, "Australia", "AU");
            AddCountryToCache(16, "Austria", "AT");
            AddCountryToCache(17, "Azerbaijan", "AZ");
            AddCountryToCache(18, "Bahamas", "BS");
            AddCountryToCache(19, "Bahrain", "BH");
            AddCountryToCache(20, "Bangladesh", "BD");
            AddCountryToCache(21, "Barbados", "BB");
            AddCountryToCache(22, "Belarus", "BY");
            AddCountryToCache(23, "Belgium", "BE");
            AddCountryToCache(24, "Belize", "BZ");
            AddCountryToCache(25, "Benin", "BJ");
            AddCountryToCache(26, "Bermuda", "BM");
            AddCountryToCache(27, "Bhutan", "BT");
            AddCountryToCache(28, "Bolivia, Plurinational State of", "BO");
            AddCountryToCache(29, "Bosnia and Herzegovina", "BA");
            AddCountryToCache(30, "Botswana", "BW");
            AddCountryToCache(31, "Bouvet Island", "BV");
            AddCountryToCache(32, "Brazil", "BR");
            AddCountryToCache(33, "British Indian Ocean Territory", "IO");
            AddCountryToCache(34, "Brunei Darussalam", "BN");
            AddCountryToCache(35, "Bulgaria", "BG");
            AddCountryToCache(36, "Burkina Faso", "BF");
            AddCountryToCache(37, "Burundi", "BI");
            AddCountryToCache(38, "Cambodia", "KH");
            AddCountryToCache(39, "Cameroon", "CM");
            AddCountryToCache(40, "Canada", "CA");
            AddCountryToCache(41, "Cape Verde", "CV");
            AddCountryToCache(42, "Cayman Islands", "KY");
            AddCountryToCache(43, "Central African Republic", "CF");
            AddCountryToCache(44, "Chad", "TD");
            AddCountryToCache(45, "Chile", "CL");
            AddCountryToCache(46, "China", "CN");
            AddCountryToCache(47, "Christmas Island", "CX");
            AddCountryToCache(48, "Cocos (Keeling) Islands", "CC");
            AddCountryToCache(49, "Colombia", "CO");
            AddCountryToCache(50, "Comoros", "KM");
            AddCountryToCache(51, "Congo", "CG");
            AddCountryToCache(52, "Congo, The Democratic Republic of the", "CD");
            AddCountryToCache(53, "Cook Islands", "CK");
            AddCountryToCache(54, "Costa Rica", "CR");
            AddCountryToCache(55, "Côte d'Ivoire", "CI");
            AddCountryToCache(56, "Croatia", "HR");
            AddCountryToCache(57, "Cuba", "CU");
            AddCountryToCache(58, "Cyprus", "CY");
            AddCountryToCache(59, "Czech Republic", "CZ");
            AddCountryToCache(60, "Denmark", "DK");
            AddCountryToCache(61, "Djibouti", "DJ");
            AddCountryToCache(62, "Dominica", "DM");
            AddCountryToCache(63, "Dominican Republic", "DO");
            AddCountryToCache(64, "Ecuador", "EC");
            AddCountryToCache(65, "Egypt", "EG");
            AddCountryToCache(66, "El Salvador", "SV");
            AddCountryToCache(67, "Equatorial Guinea", "GQ");
            AddCountryToCache(68, "Eritrea", "ER");
            AddCountryToCache(69, "Estonia", "EE");
            AddCountryToCache(70, "Ethiopa", "ET");
            AddCountryToCache(71, "Falkland Islands (Malvinas)", "FK");
            AddCountryToCache(72, "Faroe Islands", "FO");
            AddCountryToCache(73, "Fiji", "FJ");
            AddCountryToCache(74, "Finland", "FI");
            AddCountryToCache(75, "France", "FR");
            AddCountryToCache(76, "French Guiana", "GF");
            AddCountryToCache(77, "French Polynesia", "PF");
            AddCountryToCache(78, "French Southern Territories", "TF");
            AddCountryToCache(79, "Gabon", "GA");
            AddCountryToCache(80, "Gambia", "GM");
            AddCountryToCache(81, "Georgia", "GE");
            AddCountryToCache(82, "Germany", "DE");
            AddCountryToCache(83, "Ghana", "GH");
            AddCountryToCache(84, "Gibraltar", "GI");
            AddCountryToCache(85, "Greece", "GR");
            AddCountryToCache(86, "Greenland", "GL");
            AddCountryToCache(87, "Grenada", "GD");
            AddCountryToCache(88, "Guadeloupe", "GP");
            AddCountryToCache(89, "Guam", "GU");
            AddCountryToCache(90, "Guatemala", "GT");
            AddCountryToCache(91, "Guernsey", "GG");
            AddCountryToCache(92, "Guinea", "GN");
            AddCountryToCache(93, "Guinea-Bissau", "GW");
            AddCountryToCache(94, "Guyana", "GY");
            AddCountryToCache(95, "Haiti", "HT");
            AddCountryToCache(96, "Heard Island and McDonald Islands", "HM");
            AddCountryToCache(97, "Holy See (Vatican City State)", "VA");
            AddCountryToCache(98, "Honduras", "HN");
            AddCountryToCache(99, "Hong Kong", "HK");
            AddCountryToCache(100, "Hungary", "HU");
            AddCountryToCache(101, "Iceland", "IS");
            AddCountryToCache(102, "India", "IN");
            AddCountryToCache(103, "Indonesia", "ID");
            AddCountryToCache(104, "Iran, Islamic Republic of", "IR");
            AddCountryToCache(105, "Iraq", "IQ");
            AddCountryToCache(106, "Ireland", "IE");
            AddCountryToCache(107, "Isle of Man", "IM");
            AddCountryToCache(108, "Israel", "IL");
            AddCountryToCache(109, "Italy", "IT");
            AddCountryToCache(110, "Jamaica", "JM");
            AddCountryToCache(111, "Japan", "JP");
            AddCountryToCache(112, "Jersey", "JE");
            AddCountryToCache(113, "Jordan", "JO");
            AddCountryToCache(114, "Kazakhstan", "KZ");
            AddCountryToCache(115, "Kenya", "KE");
            AddCountryToCache(116, "Kiribati", "KI");
            AddCountryToCache(117, "Korea, Democratic People's Republic of", "KP");
            AddCountryToCache(118, "Korea, Republic of (South Korea)", "KR");
            AddCountryToCache(119, "Kuwait", "KW");
            AddCountryToCache(120, "Kyrgyzstan", "KG");
            AddCountryToCache(121, "Loa People's Democratic Republic", "LA");
            AddCountryToCache(122, "Latvia", "LV");
            AddCountryToCache(123, "Lebanon", "LB");
            AddCountryToCache(124, "Lesotho", "LS");
            AddCountryToCache(125, "Liberia", "LR");
            AddCountryToCache(126, "Libyan Arab Jamahiriya", "LY");
            AddCountryToCache(127, "Liechtenstein", "LI");
            AddCountryToCache(128, "Lithuania", "LT");
            AddCountryToCache(129, "Luxemboug", "LU");
            AddCountryToCache(130, "Macao", "MO");
            AddCountryToCache(131, "Macedonia, The Former Yugoslav Republic of", "MK");
            AddCountryToCache(132, "Madagascar", "MG");
            AddCountryToCache(133, "Malawi", "MW");
            AddCountryToCache(134, "Malaysia", "MY");
            AddCountryToCache(135, "Maldives", "MV");
            AddCountryToCache(136, "Mali", "ML");
            AddCountryToCache(137, "Malta", "MT");
            AddCountryToCache(138, "Marshall Islands", "MH");
            AddCountryToCache(139, "Martinique", "MQ");
            AddCountryToCache(140, "Mauritania", "MR");
            AddCountryToCache(141, "Mauritius", "MU");
            AddCountryToCache(142, "Mayotte", "YT");
            AddCountryToCache(143, "Mexico", "MX");
            AddCountryToCache(144, "Micronesia, Federated State of", "FM");
            AddCountryToCache(145, "Moldova, Republic of", "MD");
            AddCountryToCache(146, "Monaco", "MC");
            AddCountryToCache(147, "Mongolia", "MN");
            AddCountryToCache(148, "Montenegro", "ME");
            AddCountryToCache(149, "Montserrat", "MS");
            AddCountryToCache(150, "Morocco", "MA");
            AddCountryToCache(151, "Mozambique", "MZ");
            AddCountryToCache(152, "Myanmar", "MM");
            AddCountryToCache(153, "Namibia", "NA");
            AddCountryToCache(154, "Nauru", "NR");
            AddCountryToCache(155, "Nepal", "NP");
            AddCountryToCache(156, "Netherlands", "NL");
            AddCountryToCache(157, "Netherlands Antilles", "AN");
            AddCountryToCache(158, "New Caledonia", "NC");
            AddCountryToCache(159, "New Zealand", "NZ");
            AddCountryToCache(160, "Nicaragua", "NI");
            AddCountryToCache(161, "Niger", "NE");
            AddCountryToCache(162, "Nigeria", "NG");
            AddCountryToCache(163, "Niue", "NU");
            AddCountryToCache(164, "Norfolk Island", "NF");
            AddCountryToCache(165, "Northern Mariana Islands", "MP");
            AddCountryToCache(166, "Norway", "NO");
            AddCountryToCache(167, "Oman", "OM");
            AddCountryToCache(168, "Pakistan", "PK");
            AddCountryToCache(169, "Palau", "PW");
            AddCountryToCache(170, "Palestinian Territory, Occupied", "PS");
            AddCountryToCache(171, "Panama", "PA");
            AddCountryToCache(172, "Papua New Guinea", "PG");
            AddCountryToCache(173, "Paraguay", "PY");
            AddCountryToCache(174, "Peru", "PE");
            AddCountryToCache(175, "Philippines", "PH");
            AddCountryToCache(176, "Pitcairn", "PN");
            AddCountryToCache(177, "Poland", "PL");
            AddCountryToCache(178, "Portugal", "PT");
            AddCountryToCache(179, "Puerto Rico", "PR");
            AddCountryToCache(180, "Qatar", "QA");
            AddCountryToCache(181, "Réunion", "RE");
            AddCountryToCache(182, "Romania", "RO");
            AddCountryToCache(183, "Russian Federation", "RU");
            AddCountryToCache(184, "Rwanda", "RW");
            AddCountryToCache(185, "Saint Barthélemy", "BL");
            AddCountryToCache(186, "Saint Helena, Ascension and Tristan da Cunha", "SH");
            AddCountryToCache(187, "Saint Kitts and Nevis", "KN");
            AddCountryToCache(188, "Saint Lucia", "LC");
            AddCountryToCache(189, "Saint Martin", "MF");
            AddCountryToCache(190, "Saint Pierre and Miquelon", "PM");
            AddCountryToCache(191, "Saint Vincent and the Grenadines", "VC");
            AddCountryToCache(192, "Samoa", "WS");
            AddCountryToCache(193, "San Marino", "SM");
            AddCountryToCache(194, "Sao Tome and Principe", "ST");
            AddCountryToCache(195, "Saudi Arabia", "SA");
            AddCountryToCache(196, "Senegal", "SN");
            AddCountryToCache(197, "Serbia", "RS");
            AddCountryToCache(198, "Seychelles", "SC");
            AddCountryToCache(199, "Sierra Leone", "SL");
            AddCountryToCache(200, "Singapore", "SG");
            AddCountryToCache(201, "Slovakia", "SK");
            AddCountryToCache(202, "Slovenia", "SI");
            AddCountryToCache(203, "Solomon Islands", "SB");
            AddCountryToCache(204, "Somalia", "SO");
            AddCountryToCache(205, "South Africa", "ZA");
            AddCountryToCache(206, "South Georgia and the South Sandwich Islands", "GS");
            AddCountryToCache(207, "Spain", "ES");
            AddCountryToCache(208, "Sri Lanka", "LK");
            AddCountryToCache(209, "Sudan", "SD");
            AddCountryToCache(210, "Suriname", "SR");
            AddCountryToCache(211, "Svalbard and Jan Mayen", "SJ");
            AddCountryToCache(212, "Swaziland", "SZ");
            AddCountryToCache(213, "Sweden", "SE");
            AddCountryToCache(214, "Switzerland", "CH");
            AddCountryToCache(215, "Syrian Arab Republic", "SY");
            AddCountryToCache(216, "Taiwan, Province of China", "TW");
            AddCountryToCache(217, "Tajikistan", "TJ");
            AddCountryToCache(218, "Tanzania, United Republic of", "TZ");
            AddCountryToCache(219, "Thailand", "TH");
            AddCountryToCache(220, "Timor-Leste", "TL");
            AddCountryToCache(221, "Togo", "TG");
            AddCountryToCache(222, "Tokelau", "TK");
            AddCountryToCache(223, "Tonga", "TO");
            AddCountryToCache(224, "Trinidad and Tobago", "TT");
            AddCountryToCache(225, "Tunisia", "TN");
            AddCountryToCache(226, "Turkey", "TR");
            AddCountryToCache(227, "Turkmenistan", "TM");
            AddCountryToCache(228, "Turks and Caicos Islands", "TC");
            AddCountryToCache(229, "Tuvalu", "TV");
            AddCountryToCache(230, "Uganda", "UG");
            AddCountryToCache(231, "Ukraine", "UA");
            AddCountryToCache(232, "United Arab Emirates", "AE");
            AddCountryToCache(233, "United Kingdom", "GB", new DateTime(1707, 5, 1));
            AddCountryToCache(234, "United States of America", "US", new DateTime(1776, 7, 4));
            AddCountryToCache(235, "United States Minor Outlying Islands", "UM", new DateTime(1986, 1, 1));
            AddCountryToCache(236, "Uruguay", "UY");
            AddCountryToCache(237, "Uzbekistan", "UZ");
            AddCountryToCache(238, "Vanuatu", "VU");
            AddCountryToCache(239, "Venezuela, Bolivarian Republic of", "VE");
            AddCountryToCache(240, "Viet Nam", "VN");
            AddCountryToCache(241, "Virgin Islands, British", "VG");
            AddCountryToCache(242, "Virgin Islands, United States", "VI");
            AddCountryToCache(243, "Wallis and Futuna", "WF");
            AddCountryToCache(244, "Western Sahara", "EH");
            AddCountryToCache(245, "Yemen", "YE");
            AddCountryToCache(246, "Zambia", "ZM");
            AddCountryToCache(247, "Zimbabwe", "ZW");
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();

            CacheCountries();

            var batch = BatchFactory.Create();

            var commands = new List<ICommand>();
            foreach (var country in Cache.GetAll())
            {
                var command = CommandFactory.Create();

                command.AddStatement(
                    Insert
                    .OrReplace
                    .Into(Schema.Name)
                    .Columns(Schema.Fields.Select(x => x.Getter))
                    .Values(country, Schema.Fields.Select(x => x.Getter))
                    );

                commands.Add(command);
            }

            commands.Each(x => batch.AddCommand(x));

            Store.Execute(batch);
        }

        public override ICountry GetOne(object id)
        {
            return Cache.GetOne(id);
        }

        public override ICollection<ICountry> GetAll()
        {
            return Cache.GetAll();
        }
    }
}
