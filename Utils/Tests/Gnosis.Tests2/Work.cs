using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Work
        : Entity
    {
        private WorkType type;
        private Work parent;
        private Artist artist;
        private string name;
        private short year;
        private uint number;
        private readonly ObservableCollection<Work> children = new ObservableCollection<Work>();

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

        public Work Parent
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

        public Artist Artist
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

        public IEnumerable<Work> Children { get { return children; } }

        public void AddChild(Work child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            if (children.Contains(child))
                return;

            children.Add(child);
        }

        public void RemoveChild(Work child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            if (!children.Contains(child))
                return;

            children.Remove(child);
        }

        //public bool Equals(Work other)
        //{
        //    if (other == null)
        //        return false;

        //    return Type == other.Type && Parent == other.Parent && Artist == other.Artist && Name == other.Name && Year == other.Year && Number == other.Number;
        //}
    }
}
