using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon.Models
{
    public class SourcePropertyBase<T> : ISourceProperty
    {
        protected SourcePropertyBase(string name)
            : this(name, default(T), null)
        {
        }

        protected SourcePropertyBase(string name, T @default)
            : this(name, @default, null)
        {
        }

        protected SourcePropertyBase(string name, T @default, Predicate<T> predicate)
        {
            this.name = name;
            this.type = typeof(T);
            this.@default = @default;
            this.value = @default;
            this.predicate = predicate;
        }

        private readonly string name;
        private readonly Type type;
        private readonly object @default;
        private object value;
        private Predicate<T> predicate;

        #region ISourceProperty Members

        public string Name
        {
            get { return name; }
        }

        public Type Type
        {
            get { return type; }
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

                this.value = value;
            }
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

            return (predicate != null) ? predicate(check) : true;
        }

        #endregion
    }
}
