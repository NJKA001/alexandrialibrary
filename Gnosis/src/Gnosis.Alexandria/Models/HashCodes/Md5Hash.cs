using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.HashCodes
{
    public class Md5Hash
        : ValueBase, IHashCode
    {
        public Md5Hash(string value)
            : this(Guid.NewGuid(), value)
        {
        }

        public Md5Hash(Guid id, string value)
            : base(id)
        {
            this.value = value;
        }

        private readonly string value;

        public Uri Scheme
        {
            get { return Namespace; }
        }

        public string Value
        {
            get { return value; }
        }

        public static readonly Uri Namespace = new Uri("http://alxlib.com/domain/1/hash-schemes/md5/1");

        public static IHashCode Create(string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var hash = originalString.AsMd5Hash();

            return (hash != null) ? new Md5Hash(hash) : null;
        }

        public static IHashCode Create(Uri location)
        {
            if (location == null)
                return null;

            var hash = location.AsMd5Hash();

            return (hash != null) ? new Md5Hash(hash) : null;
        }
    }
}
