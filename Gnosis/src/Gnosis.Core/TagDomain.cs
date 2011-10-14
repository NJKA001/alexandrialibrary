using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagDomain
        : ITagDomain
    {
        private TagDomain(int id, string name, Type baseType, object defaultValue, Func<object, bool> isValid, Func<object, TagTuple> getTuple, Func<TagTuple, object> getValue)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (baseType == null)
                throw new ArgumentNullException("baseType");
            if (defaultValue == null)
                throw new ArgumentNullException("defaultValue");
            if (isValid == null)
                throw new ArgumentNullException("isValid");
            if (getTuple == null)
                throw new ArgumentNullException("getTuple");
            if (getValue == null)
                throw new ArgumentNullException("getValue");

            this.id = id;
            this.name = name;
            this.baseType = baseType;
            this.defaultValue = defaultValue;
            this.isValid = isValid;
            this.getTuple = getTuple;
            this.getValue = getValue;
        }

        private readonly int id;
        private readonly string name;
        private readonly Type baseType;
        private readonly object defaultValue;
        private readonly Func<object, bool> isValid;
        private readonly Func<object, TagTuple> getTuple;
        private readonly Func<TagTuple, object> getValue;

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

        public TagTuple GetTuple(object value)
        {
            return getTuple(value);
        }

        public object GetValue(TagTuple tuple)
        {
            return getValue(tuple);
        }

        static TagDomain()
        {
            all.Add(String);
            all.Add(StringArray);
            all.Add(PositiveInteger);
            all.Add(Date);
            all.Add(Duration);

            foreach (var domain in all)
                byId[domain.Id] = domain;
        }

        private static readonly IList<ITagDomain> all = new List<ITagDomain>();
        private static readonly IDictionary<int, ITagDomain> byId = new Dictionary<int, ITagDomain>();

        private static string DateToName(object value)
        {
            if (!(value is DateTime))
                return string.Empty;

            var d = (DateTime)value;
            return d.ToString("s");
        }

        public static readonly ITagDomain String = new TagDomain(1, "String", typeof(string), string.Empty, value => value.IsString(), value => new TagTuple(value), tuple => tuple.ToString());
        public static readonly ITagDomain StringArray = new TagDomain(2, "StringArray", typeof(string[]), new string[0], value => value.IsStringArray(), value => value.ToStringArray().ToTagTuple(), tuple => tuple.ToStringArray());
        public static readonly ITagDomain PositiveInteger = new TagDomain(3, "PositiveInteger", typeof(uint), (uint)0, value => value.IsUInt32(), value => new TagTuple(value), tuple => tuple.ToUInt32());
        public static readonly ITagDomain Date = new TagDomain(4, "Date", typeof(DateTime), DateTime.MinValue, value => value.IsDateTime(), value => value.ToDateTime().ToTagTuple(), tuple => tuple.ToDateTime());
        public static readonly ITagDomain Duration = new TagDomain(5, "Duration", typeof(TimeSpan), TimeSpan.Zero, value => value.IsTimeSpan(), value => new TagTuple(value), tuple => tuple.ToTimeSpan());
        public static readonly ITagDomain ByteArray = new TagDomain(6, "ByteArray", typeof(byte[]), new byte[0], value => value.IsByteArray(), value => new TagTuple(value), tuple => tuple.ToByteArray());

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
