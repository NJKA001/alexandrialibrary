using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class Artist : MutableDeletable, IArtist
    {
        public Artist()
        {
        }

        private Artist(long id)
            : this()
        {
            Initialize(id);
        }

        private string _name = "Unknown";
        private string _nameHash = "Unknown".AsNameHash();
        private string _abbreviation = string.Empty;
        private ICountry _nationality = Country.Unknown;
        private DateTime _fromDate = DateTime.MinValue;
        private DateTime _toDate = DateTime.MaxValue;
        private string _note = string.Empty;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    _nameHash = _name.AsNameHash();
                    IsChanged = true;
                }
            }
        }

        public string NameHash
        {
            get { return _nameHash; }
        }

        public string Abbreviation
        {
            get { return _abbreviation; }
            set
            {
                if (_abbreviation != value)
                {
                    _abbreviation = value;
                    IsChanged = true;
                }
            }
        }

        public ICountry Nationality
        {
            get { return _nationality; }
            set
            {
                if (_nationality != value)
                {
                    _nationality = value;
                    IsChanged = true;
                }
            }
        }

        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                if (_fromDate != value)
                {
                    _fromDate = value;
                    IsChanged = true;
                }
            }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                if (_toDate != value)
                {
                    _toDate = value;
                    IsChanged = true;
                }
            }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                if (_note != value)
                {
                    _note = value;
                    IsChanged = true;
                }
            }
        }

        public static readonly IArtist Unknown = new Artist(1L);
        public static readonly IArtist Various = new Artist(2L) { Name = "Various" };
    }
}
