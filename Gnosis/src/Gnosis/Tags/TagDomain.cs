using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tags;
using Gnosis.Tags.Id3.Id3v1;

namespace Gnosis.Tags
{
    public class TagDomain
        : ITagDomain
    {
        private TagDomain(int id, string name, Type baseType, object defaultValue, Func<object, bool> isValid, Func<object, string> getToken, Func<string, object> getValue)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (baseType == null)
                throw new ArgumentNullException("baseType");
            if (defaultValue == null)
                throw new ArgumentNullException("defaultValue");
            if (isValid == null)
                throw new ArgumentNullException("isValid");
            if (getToken == null)
                throw new ArgumentNullException("getToken");
            if (getValue == null)
                throw new ArgumentNullException("getValue");

            this.id = id;
            this.name = name;
            this.baseType = baseType;
            this.defaultValue = defaultValue;
            this.isValid = isValid;
            this.getToken = getToken;
            this.getValue = getValue;
        }

        private readonly int id;
        private readonly string name;
        private readonly Type baseType;
        private readonly object defaultValue;
        private readonly Func<object, bool> isValid;
        private readonly Func<object, string> getToken;
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

        public string GetToken(object value)
        {
            return baseType == typeof(byte[]) && value is byte[] ?
                string.Empty :
                getToken(value);
        }

        public byte[] GetData(object value)
        {
            return baseType == typeof(byte[]) && value is byte[] ?
                (byte[])value :
                new byte[0];
        }

        public object GetValue(string token, byte[] data)
        {
            return baseType == typeof(byte[]) ?
                (object)data :
                getValue(token);
        }

        static TagDomain()
        {
            all.Add(String);
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

        public static readonly ITagDomain String = new TagDomain(1, "String", typeof(string), string.Empty, value => value.IsString(), value => value.ToString(), token => token);
        public static readonly ITagDomain PositiveInteger = new TagDomain(2, "PositiveInteger", typeof(uint), (uint)0, value => value.IsUInt32(), value => value.ToString(), token => token.ToUInt32());
        public static readonly ITagDomain Date = new TagDomain(3, "Date", typeof(DateTime), DateTime.MinValue, value => value.IsDateTime(), value => value.ToDateTime().ToString("o"), token => token.ToDateTime());
        public static readonly ITagDomain Duration = new TagDomain(4, "Duration", typeof(TimeSpan), TimeSpan.Zero, value => value.IsTimeSpan(), value => value.ToTimeSpan().Ticks.ToString(), token => TimeSpan.FromTicks(token.ToInt64()));
        public static readonly ITagDomain ByteArray = new TagDomain(5, "ByteArray", typeof(byte[]), new byte[0], value => value.IsByteArray(), value => string.Empty, token => token);
        public static readonly ITagDomain Id3v1SimpleGenre = new TagDomain(101, "Id3v1SimpleGenre", typeof(Id3v1Genre), Id3v1Genre.Blues, value => value.IsEnum<Id3v1Genre>(), value => value.ToEnum<Id3v1Genre>().ToString(), token => token.ToEnum<Id3v1Genre>());

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
