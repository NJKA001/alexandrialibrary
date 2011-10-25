using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Tags;
using Gnosis.Core.Tags.Id3.Id3v1;

namespace Gnosis.Core.Tags
{
    public class TagDomain
        : ITagDomain
    {
        private TagDomain(int id, string name, Type[] baseTypes, object defaultValue, Func<object, bool> isValid, Func<object, ITagTuple> getTuple, Func<ITagTuple, object> getValue)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (baseTypes == null)
                throw new ArgumentNullException("baseTypes");
            if (baseTypes.Length != 7)
                throw new ArgumentException("baseTypes must have 7 items");
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
            this.baseTypes = baseTypes;
            this.defaultValue = defaultValue;
            this.isValid = isValid;
            this.getTuple = getTuple;
            this.getValue = getValue;
        }

        private readonly int id;
        private readonly string name;
        private readonly Type[] baseTypes;
        private readonly object defaultValue;
        private readonly Func<object, bool> isValid;
        private readonly Func<object, ITagTuple> getTuple;
        private readonly Func<ITagTuple, object> getValue;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public Type[] BaseTypes
        {
            get { return baseTypes; }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
        }

        public bool IsValid(object value)
        {
            return isValid(value);
        }

        public ITagTuple GetTuple(object value)
        {
            return getTuple(value);
        }

        public object GetValue(ITagTuple tuple)
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

        public static readonly ITagDomain String = new TagDomain(1, "String", new Type[] { typeof(string), null, null, null, null, null, null}, string.Empty, value => value.IsString(), value => new TagTuple(value), tuple => tuple.ToString());
        public static readonly ITagDomain StringArray = new TagDomain(2, "StringArray", new Type[] { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) }, new string[0], value => value.IsStringArray(), value => value.ToStringArray().ToTagTuple(), tuple => tuple.ToStringArray());
        public static readonly ITagDomain PositiveInteger = new TagDomain(3, "PositiveInteger", new Type[] { typeof(uint), null, null, null, null, null, null }, (uint)0, value => value.IsUInt32(), value => new TagTuple(value), tuple => tuple.ToUInt32());
        public static readonly ITagDomain Date = new TagDomain(4, "Date", new Type[] { typeof(int), typeof(int), typeof(int), null, null, null, null }, DateTime.MinValue, value => value.IsDateTime(), value => value.ToDateTime().ToTagTuple(), tuple => tuple.ToDateTime());
        public static readonly ITagDomain Duration = new TagDomain(5, "Duration", new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), null, null }, TimeSpan.Zero, value => value.IsTimeSpan(), value => new TagTuple(value), tuple => tuple.ToTimeSpan());
        public static readonly ITagDomain ByteArray = new TagDomain(6, "ByteArray", new Type[] { typeof(byte[]), null, null, null, null, null, null }, new byte[0], value => value.IsByteArray(), value => new TagTuple(value), tuple => tuple.ToByteArray());
        public static readonly ITagDomain Id3v1SimpleGenre = new TagDomain(7, "Id3v1SimpleGenre", new Type[] { typeof(string), typeof(byte), null, null, null, null, null }, Id3v1Genre.Blues, value => value.IsEnum<Id3v1Genre>(), value => value.ToEnum<Id3v1Genre>().ToTagTuple(), tuple => tuple.ToEnum<Id3v1Genre>());

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
