using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Artist
        : Entity
    {
        private ArtistType type;
        private string name;
        private short year;

        private readonly ObservableCollection<Work> works = new ObservableCollection<Work>();

        public ArtistType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                var safe = value ?? string.Empty;
                if (name != safe)
                {
                    name = safe;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public short Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    NotifyPropertyChanged("Year");
                }
            }
        }

        public IEnumerable<Work> Works { get { return works; } }

        public void AddWork(Work work)
        {
            if (work == null)
                throw new ArgumentNullException("work");

            if (works.Contains(work))
                return;

            works.Add(work);
        }

        public void RemoveWork(Work work)
        {
            if (work == null)
                throw new ArgumentNullException("work");

            if (!works.Contains(work))
                return;

            works.Remove(work);
        }

        //public bool Equals(Artist other)
        //{
        //    if (other == null)
        //        return false;

        //    return Type == other.Type && Name == other.Name && Year == other.Year;
        //}
    }
}
