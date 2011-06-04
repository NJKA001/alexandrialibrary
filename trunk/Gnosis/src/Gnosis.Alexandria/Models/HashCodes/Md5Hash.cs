﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.HashCodes
{
    public class Md5Hash
        : ValueBase, IHashCode
    {
        public Md5Hash(Guid parent, string value)
            : base(parent)
        {
            this.value = value;
        }

        public Md5Hash(Guid id, Guid parent, string value)
            : base(id, parent)
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

        public static IHashCode Create(Guid parent, string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var hash = originalString.AsMd5Hash();

            return (hash != null) ? new Md5Hash(parent, hash) : null;
        }

        public static IHashCode Create(Guid parent, Uri location)
        {
            if (location == null)
                return null;

            var hash = location.AsMd5Hash();

            return (hash != null) ? new Md5Hash(parent, hash) : null;
        }
    }
}