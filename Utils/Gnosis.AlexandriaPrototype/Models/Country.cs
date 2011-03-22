using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class Country : Immutable, ICountry
    {
        public Country()
        {
        }

        private Country(object id) : this()
        {
            Initialize(id);
        }

        private string _name = "Unknown";
        private string _nameHash = "Unknown".AsNameHash();
        private string _abbreviation = string.Empty;
        private string _code = "XA";
        private DateTime _fromDate = DateTime.MinValue;
        private DateTime _toDate = DateTime.MaxValue;

        protected override void  OnPopulate(IEnumerable<KeyValuePair<string,object>> data)
        {
 	        foreach (var pair in data)
            {
                switch (pair.Key)
                {
                    case "Name":
                        _name = pair.Value.AsString();
                        _nameHash = _name.AsNameHash();
                        break;
                    case "NameHash":
                        _nameHash = pair.Value.AsString();
                        break;
                    case "Abbreviation":
                        _abbreviation = pair.Value.AsString();
                        break;
                    case "Code":
                        _code = pair.Value.AsString();
                        break;
                    case "FromDate":
                        _fromDate = pair.Value.AsDateTime();
                        break;
                    case "ToDate":
                        _toDate = pair.Value.AsDateTime();
                        break;
                    default:
                        break;
                }
            }
        }

        public string Name
        {
            get { return _name; }
        }

        public string NameHash
        {
            get { return _nameHash; }
        }

        public string Abbreviation
        {
            get { return _abbreviation; }
        }

        public string Code
        {
            get { return _code; }
        }

        public DateTime FromDate
        {
            get { return _fromDate; }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
        }

        public static readonly ICountry Unknown = new Country(1L);
    }
}
