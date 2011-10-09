using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagDomain
        : ITagDomain
    {
        private TagDomain(int id, string name, Type baseType, object defaultValue, Func<object, bool> isValid, Func<object, string> getName, Func<string, object> getValue)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (baseType == null)
                throw new ArgumentNullException("baseType");
            if (defaultValue == null)
                throw new ArgumentNullException("defaultValue");
            if (isValid == null)
                throw new ArgumentNullException("isValid");
            if (getName == null)
                throw new ArgumentNullException("getName");
            if (getValue == null)
                throw new ArgumentNullException("getValue");

            this.id = id;
            this.name = name;
            this.baseType = baseType;
            this.defaultValue = defaultValue;
            this.isValid = isValid;
            this.getName = getName;
            this.getValue = getValue;
        }

        private readonly int id;
        private readonly string name;
        private readonly Type baseType;
        private readonly object defaultValue;
        private readonly Func<object, bool> isValid;
        private readonly Func<object, string> getName;
        private readonly Func<string, object> getValue;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public Type BaseType
        {
            get { return baseType; }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
        }

        public bool IsValid(object value)
        {
            return isValid(value);
        }

        public string GetName(object value)
        {
            return IsValid(value) ?
                getName(value)
                : getName(defaultValue);
        }

        public object GetValue(string name)
        {
            return getValue(name);
        }

        static TagDomain()
        {
            all.Add(String);
            all.Add(StringArray);
            all.Add(PositiveInteger);
            all.Add(Date);
            all.Add(Milliseconds);

            foreach (var domain in all)
                byId[domain.Id] = domain;
        }

        private static readonly IList<ITagDomain> all = new List<ITagDomain>();
        private static readonly IDictionary<int, ITagDomain> byId = new Dictionary<int, ITagDomain>();

        private static string StringArrayToName(object value)
        {
            if (!(value is string[]))
                return string.Empty;

            var a = (string[])value;
            return string.Join("; ", a);
        }

        private static string DateToName(object value)
        {
            if (!(value is DateTime))
                return string.Empty;

            var d = (DateTime)value;
            return d.ToString("s");
        }

        private static string MillisecondsToName(object value)
        {
            if (!(value is TimeSpan))
                return string.Empty;

            var t = (TimeSpan)value;
            return t.TotalMilliseconds.ToString();
        }

        public static readonly ITagDomain String = new TagDomain(1, "String", typeof(string), string.Empty, x => x != null, x => x.ToString(), x => x);
        public static readonly ITagDomain StringArray = new TagDomain(2, "StringArray", typeof(string[]), new string[0], x => x != null, x => StringArrayToName(x), x => x.ToString().Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries));
        public static readonly ITagDomain PositiveInteger = new TagDomain(3, "PositiveInteger", typeof(uint), (uint)0, x => { if (x == null) return false; uint result = 0; return uint.TryParse(x.ToString(), out result); }, x => x.ToString(), x => uint.Parse(x));
        public static readonly ITagDomain Date = new TagDomain(4, "Date", typeof(DateTime), DateTime.MinValue, x => { if (x == null) return false; var result = DateTime.MinValue; return DateTime.TryParse(x.ToString(), out result); }, x => DateToName(x), x => DateTime.Parse(x));
        public static readonly ITagDomain Milliseconds = new TagDomain(5, "Milliseconds", typeof(TimeSpan), TimeSpan.Zero, x => { if (x == null) return false; var result = 0; return int.TryParse(x.ToString(), out result); }, x => MillisecondsToName(x), x => { var ms = int.Parse(x); return new TimeSpan(0, 0, 0, 0, ms); });

        public static IEnumerable<ITagDomain> GetAll()
        {
            return all;
        }

        public static ITagDomain Parse(int id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : null;
        }
    }
}
