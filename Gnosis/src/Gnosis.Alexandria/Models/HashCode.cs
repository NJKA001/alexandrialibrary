using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class HashCode
        : ValueBase<IHashCode>, IHashCode
    {
        public HashCode()
        {
            AddInitializer(value => this.scheme = value.ToUri(), hashCode => hashCode.Scheme);
            AddInitializer(value => this.value = value.ToString(), hashCode => hashCode.Value);
        }

        public HashCode(Guid parent, Uri scheme, string value)
        {
            AddInitializer(x => this.scheme = scheme, hashCode => hashCode.Scheme);
            AddInitializer(x => this.value = value, hashCode => hashCode.Value);

            Initialize(parent);
        }

        private Uri scheme;
        private string value;

        #region IHashCode Members

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

        public static IHashCode CreateDoubleMetaphoneHash(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var value = originalString.AsDoubleMetaphone();
            
            return (!string.IsNullOrEmpty(value)) ? new HashCode(parent, SchemeDoubleMetaphone, value) : null;
        }

        #endregion

        #region MD5

        public static readonly Uri SchemeMd5 = new Uri("http://alxlib.com/domain/1/hash-schemes/md5/1");

        public static IHashCode CreateMd5Hash(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var value = originalString.AsMd5Hash();

            return (!string.IsNullOrEmpty(value)) ? new HashCode(parent, SchemeMd5, value) : null;
        }

        public static IHashCode CreateMd5Hash(Guid parent, Uri location)
        {
            if (location == null)
                return null;

            var value = location.AsMd5Hash();

            return (!string.IsNullOrEmpty(value)) ? new HashCode(parent, SchemeMd5, value) : null;
        }

        #endregion

        #region NameHash

        public static Uri SchemeNameHash = new Uri("http://alxlib.com/domain/1/hash-schemes/name/1");

        public static IHashCode CreateNameHash(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var value = originalString.AsNameHash();

            return (!string.IsNullOrEmpty(value)) ? new HashCode(parent, SchemeNameHash, value) : null;
        }

        #endregion
    }
}
