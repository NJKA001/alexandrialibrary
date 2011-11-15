using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Commands
{
    public class Parameter
        : IParameter
    {
        public Parameter(string name, object value, bool isMultiValue)
        {
            this.name = name;
            this.value = value;
            this.isMultiValue = isMultiValue;
        }

        public Parameter(string name)
            : this(name, null, false)
        {
        }

        public Parameter(string name, IEntity entity)
            : this(name, entity.Id.ToString(), false)
        {
        }

        public Parameter(string name, IValue value)
            : this(name, value.Id.ToString(), false)
        {
        }

        //public Parameter(string name, ILanguage language)
        //    : this(name, language.ToString(), false)
        //{
        //}

        //public Parameter(string name, ILanguageTag languageTag)
        //    : this(name, languageTag.ToString(), false)
        //{
        //}

        //public Parameter(string name, ICountry country)
        //    : this(name, country.ToString(), false)
        //{
        //}

        //public Parameter(string name, IRegion region)
        //    : this(name, region.ToString(), false)
        //{
        //}

        public Parameter(string name, Guid value)
            : this(name, value.ToString(), false)
        {
        }

        public Parameter(string name, bool value)
            : this(name, value ? 1 : 0, false)
        {
        }

        public Parameter(string name, Uri value)
            : this(name, value.ToString(), false)
        {
        }

        public Parameter(string name, DateTime value)
            : this(name, value.ToString("s"), false)
        {
        }

        public Parameter(string name, TimeSpan value)
            : this(name, value.Ticks, false)
        {
        }

        //public Parameter(string name, IEnumerable<string> values)
        //    : this(name, values.ToNamesString(), true)
        //{
        //}

        private static object GetEnumerableValue(IEnumerable values)
        {
            if (values == null)
                return string.Empty;

            if (values.GetType() == typeof(string))
                return values.ToString();

            if (values.GetType() == typeof(byte[]))
                return values;

            var names = new List<string>();

            foreach (var value in values)
                names.Add(value.ToString());

            return names.ToNamesString();
        }

        public Parameter(string name, IEnumerable values)
            : this(name, GetEnumerableValue(values), true)
        {
        }

        //public Parameter(string name, IEnumerable<ILanguage> languages)
        //    : this(name, languages.ToNamesString(), true)
        //{
        //}

        //public Parameter(string name, IEnumerable<ICountry> countries)
        //    : this(name, countries.ToNamesString(), true)
        //{
        //}

        private readonly string name;
        private readonly object value;
        private readonly bool isMultiValue;

        public string Name
        {
            get { return name; }
        }

        public object Value
        {
            get { return value; }
        }
    }
}
