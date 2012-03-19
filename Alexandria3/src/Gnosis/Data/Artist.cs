using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public class Artist
        : Entity, IArtist
    {
        public Artist()
        {
        }

        public Artist(ArtistType type, string name, short year)
        {
            this.type = type;
            this.name = name ?? string.Empty;
            this.year = year;
        }

        private ArtistType type;
        private string name;
        private short year;

        private readonly ObservableCollection<IWork> works = new ObservableCollection<IWork>();

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

        public IEnumerable<IWork> Works { get { return works; } }

        public void AddWork(IWork work)
        {
            if (work == null)
                throw new ArgumentNullException("work");

            if (works.Contains(work))
                return;

            works.Add(work);
        }

        public void RemoveWork(IWork work)
        {
            if (work == null)
                throw new ArgumentNullException("work");

            if (!works.Contains(work))
                return;

            works.Remove(work);
        }
    }
}
