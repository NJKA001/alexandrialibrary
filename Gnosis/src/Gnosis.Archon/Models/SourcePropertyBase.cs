using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon.Models
{
    public class SourcePropertyBase<T> : ISourceProperty
    {
        protected SourcePropertyBase(Guid id, ISource source, string name)
            : this(id, source, name, default(T), null)
        {
        }

        protected SourcePropertyBase(Guid id, ISource source, string name, T @default)
            : this(id, source, name, @default, null)
        {
        }

        protected SourcePropertyBase(Guid id, ISource source, string name, T @default, Predicate<T> predicate)
        {
            this.id = id;
            this.source = source;
            this.name = name;
            this.type = typeof(T);
            this.@default = @default;
            this.value = @default;
            this.predicate = predicate;
        }

        private readonly Guid id;
        private readonly ISource source;
        private readonly string name;
        private readonly Type type;
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
