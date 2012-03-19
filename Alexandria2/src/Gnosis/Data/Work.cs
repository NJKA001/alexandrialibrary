using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public class Work
    : Entity, IWork
    {
        public Work()
        {
        }

        public Work(WorkType type, IWork parent, IArtist artist, string name, short year, uint number)
        {
            this.type = type;
            this.parent = parent;
            this.artist = artist;
            this.name = name ?? string.Empty;
            this.year = year;
            this.number = number;
        }

        private WorkType type;
        private IWork parent;
        private IArtist artist;
        private string name;
        private short year;
        private uint number;
        private readonly ObservableCollection<IWork> children = new ObservableCollection<IWork>();

        public WorkType Type
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

        public IWork Parent
        {
            get { return parent; }
            set
            {
                if (parent != value)
                {
                    parent = value;
                    NotifyPropertyChanged("Parent");
                }
            }
        }

        public IArtist Artist
        {
            get { return artist; }
            set
            {
                if (artist != value)
                {
                    artist = value;
                    NotifyPropertyChanged("Artist");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
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

        public uint Number
        {
            get { return number; }
            set
            {
                if (number != value)
                {
                    number = value;
                    NotifyPropertyChanged("Number");
                }
            }
        }

        public IEnumerable<IWork> Children { get { return children; } }

        public void AddChild(IWork child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            if (children.Contains(child))
                return;

            children.Add(child);
        }

        public void RemoveChild(IWork child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            if (!children.Contains(child))
                return;

            children.Remove(child);
        }
    }
}
