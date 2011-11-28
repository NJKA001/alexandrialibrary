using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Gnosis.Alexandria.Models
{
    public class Tag
        : ValueBase<ITag>, ITag
    {
        public Tag()
        {
            AddInitializer(value => this.scheme = value.ToUri(), tag => tag.Scheme);
            AddInitializer(value => this.value = value.ToString(), tag => tag.Value);
        }

        public Tag(Guid parent, Uri scheme, string value)
        {
            AddInitializer(x => this.scheme = scheme, tag => tag.Scheme);
            AddInitializer(x => this.value = value, tag => tag.Value);

            Initialize(parent);
        }

        private Uri scheme;
        private string value;

        #region ITag Members

        public Uri Scheme
        {
            get { return scheme; }
        }

        public string Value
        {
            get { return value; }
        }

        #endregion

        #region Double Metaphone

        public static readonly Uri SchemeDoubleMetaphone = new Uri("http://alxlib.com/domain/1/hash-schemes/double-metaphone/1");

        public static ITag CreateDoubleMetaphoneHash(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var value = originalString.ToDoubleMetaphoneString();
            
            return (!string.IsNullOrEmpty(value)) ? new Tag(parent, SchemeDoubleMetaphone, value) : null;
        }

        #endregion

        #region MD5

        public static readonly Uri SchemeMd5 = new Uri("http://alxlib.com/domain/1/hash-schemes/md5/1");

        public static ITag CreateMd5Hash(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var value = originalString.ToMd5Hash();

            return (!string.IsNullOrEmpty(value)) ? new Tag(parent, SchemeMd5, value) : null;
        }

        public static ITag CreateMd5Hash(Guid parent, Uri location)
        {
            if (location == null)
                return null;

            var value = location.ToMd5Hash();

            return (!string.IsNullOrEmpty(value)) ? new Tag(parent, SchemeMd5, value) : null;
        }

        #endregion

        #region NameHash

        public static Uri SchemeAmericanizedGraph = new Uri("http://alxlib.com/domain/1/algorithms/americanized/1");

        public static ITag CreateAmericanizedGraph(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var value = originalString.ToAmericanizedString();

            return (!string.IsNullOrEmpty(value)) ? new Tag(parent, SchemeAmericanizedGraph, value) : null;
        }

        #endregion
    }
}
