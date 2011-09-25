using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class SourcePropertyBase<T> : ISourceProperty, INotifyPropertyChanged
    {
        protected SourcePropertyBase(ISource source, string name)
            : this(Guid.NewGuid(), source, name, default(T), new Predicate<T>(x => { return true; }))
        {
        }

        protected SourcePropertyBase(Guid id, ISource source, string name)
            : this(id, source, name, default(T), new Predicate<T>(x => { return true; }))
        {
        }

        protected SourcePropertyBase(ISource source, string name, T @default)
            : this(Guid.NewGuid(), source, name, @default, new Predicate<T>(x => { return true; }))
        {
        }

        protected SourcePropertyBase(Guid id, ISource source, string name, T @default)
            : this(id, source, name, @default, new Predicate<T>(x => { return true; }))
        {
        }

        protected SourcePropertyBase(ISource source, string name, T @default, Predicate<T> predicate)
            : this(Guid.NewGuid(), source, name, @default, predicate)
        {
        }

        protected SourcePropertyBase(Guid id, ISource source, string name, T @default, Predicate<T> predicate)
        {
            this.id = id;
            this.source = source;
            this.name = name;
            this.baseType = typeof(T);
            this.@default = @default;
            Value = @default;
            this.predicate = predicate;
        }

        private readonly Guid id;
        private readonly ISource source;
        private readonly string name;
        private readonly Type baseType;
        private readonly object @default;
        private object value;
        private Predicate<T> predicate;

        #region ISourceProperty Members

        public Guid Id
        {
            get { return id; }
        }

        public ISource Source
        {
            get { return source; }
        }

        public string Name
        {
            get { return name; }
        }

        public Type BaseType
        {
            get { return baseType; }
        }

        public object Default
        {
            get { return @default; }
        }

        public virtual object Value
        {
            get { return value; }
            set
            {
                if (!IsValid(value))
                    throw new ArgumentException("Value is not valid for property " + Name);

                if (this.value != value)
                {
                    this.value = value;
                    ValueHash = value != null ? value.ToString().ToAmericanizedString() : string.Empty;
                    ValueMetaphone = value != null ? value.ToString().ToDoubleMetaphoneString() : string.Empty;
                    OnPropertyChanged("Value");
                    OnPropertyChanged("ValueHash");
                    OnPropertyChanged("ValueMetaphone");
                }
            }
        }

        public string ValueHash
        {
            get;
            private set;
        }

        public string ValueMetaphone
        {
            get;
            private set;
        }

        public virtual bool IsValid(object value)
        {
            T check = default(T);
            try
            {
                check = (T)value;
            }
            catch
            {
                return false;
            }

            return predicate(check);
        }

        #endregion

        #region INotifyPropertyChanged Members

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
