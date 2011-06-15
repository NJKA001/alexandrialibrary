using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class HashCode
        : ValueBase, IHashCode
    {
        public HashCode()
        {
            AddInitializer("Scheme", value => this.scheme = value.ToUri());
            AddInitializer("Value", value => this.value = value.ToString());
        }

        public HashCode(Guid parent, Uri scheme, string value)
        {
            AddInitializer("Scheme", x => this.scheme = scheme);
            AddInitializer("Value", x => this.value = value);

            Initialize(parent);
        }

        private Uri scheme;
        private string value;

        public Uri Scheme
        {
            get { return scheme; }
        }

        public string Value
        {
            get { return value; }
        }

        #region Double Metaphone

        public static readonly Uri SchemeDoubleMetaphone = new Uri("http://alxlib.com/domain/1/hash-schemes/double-metaphone/1");

        public static IHashCode CreateDoubleMetaphoneHash(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            return new HashCode(parent, SchemeDoubleMetaphone, originalString.AsDoubleMetaphone());
        }

        #endregion

        #region MD5

        public static readonly Uri SchemeMd5 = new Uri("http://alxlib.com/domain/1/hash-schemes/md5/1");

        public static IHashCode CreateMd5Hash(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var hash = originalString.AsMd5Hash();

            return (hash != null) ? new HashCode(parent, SchemeMd5, hash) : null;
        }

        public static IHashCode CreateMd5Hash(Guid parent, Uri location)
        {
            if (location == null)
                return null;

            var hash = location.AsMd5Hash();

            return (hash != null) ? new HashCode(parent, SchemeMd5, hash) : null;
        }

        #endregion

        #region NameHash

        public static Uri SchemeNameHash = new Uri("http://alxlib.com/domain/1/hash-schemes/name/1");

        public static IHashCode CreateNameHash(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            return new HashCode(parent, SchemeNameHash, originalString.AsNameHash());
        }

        #endregion
    }
}
