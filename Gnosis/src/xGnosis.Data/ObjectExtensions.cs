using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data.Commands;

namespace Gnosis.Data
{
    public static class ObjectExtensions
    {
        public static IParameter ToParameter(this object self)
        {
            var name = "@" + Guid.NewGuid().ToString().Replace("-", string.Empty);

            if (self == null)
                return new Parameter(name);

            if (self is IEntity)
                return new Parameter(name, self as IEntity);
            if (self is IValue)
                return new Parameter(name, self as IValue);
            //if (self is ILanguage)
            //    return new Parameter(name, self as ILanguage);
            //if (self is ILanguageTag)
            //    return new Parameter(name, self as ILanguageTag);
            //if (self is ICountry)
            //    return new Parameter(name, self as ICountry);
            //if (self is IRegion)
            //    return new Parameter(name, self as IRegion);
            //if (self is IEnumerable<ILanguage>)
            //    return new Parameter(name, self as IEnumerable<ILanguage>);
            //if (self is IEnumerable<ILanguage>)
            //    return new Parameter(name, self as IEnumerable<ICountry>);
            if (self is IEnumerable<string>)
                return new Parameter(name, self as IEnumerable<string>);
            if (self is IEnumerable)
                return new Parameter(name, self as IEnumerable);
            if (self.GetType() == typeof(bool))
                return new Parameter(name, (bool)self);
            if (self.GetType() == typeof(Guid))
                return new Parameter(name, (Guid)self);
            if (self.GetType() == typeof(Uri))
                return new Parameter(name, self as Uri);
            if (self.GetType() == typeof(DateTime))
                return new Parameter(name, (DateTime)self);
            if (self.GetType() == typeof(TimeSpan))
                return new Parameter(name, (TimeSpan)self);
            if (self.GetType().IsEnum)
                return new Parameter(name, (int)self, false);

            return new Parameter(name, self, false);
        }

    }
}
