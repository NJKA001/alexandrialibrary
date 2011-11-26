using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags
{
    public class Tag
        : ITag
    {
        public Tag(Uri target, ITagType type, string value)
            : this(target, type, value, Algorithms.Algorithm.Default, new byte[0], 0)
        {
        }

        public Tag(Uri target, ITagType type, string value, IAlgorithm algorithm)
            : this(target, type, value, algorithm, new byte[0], 0)
        {
        }

        public Tag(Uri target, ITagType type, string value, IAlgorithm algorithm, byte[] data)
            : this(target, type, value, algorithm, data, 0)
        {
        }

        public Tag(Uri target, ITagType type, string value, IAlgorithm algorithm, byte[] data, long id)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (type == null)
                throw new ArgumentNullException("type");
            if (value == null)
                throw new ArgumentNullException("value");
            if (data == null)
                throw new ArgumentNullException("data");

            this.target = target;
            this.algorithm = algorithm;
            this.type = type;
            this.value = value;
            this.data = data;
            this.id = id;
        }

        private readonly long id;
        private readonly Uri target;
        private readonly IAlgorithm algorithm;
        private readonly ITagType type;
        private readonly string value;
        private readonly byte[] data;

        public long Id
        {
            get { return id; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public IAlgorithm Algorithm
        {
            get { return algorithm; }
        }

        public ITagType Type
        {
            get { return type; }
        }

        public string Value
        {
            get { return value; }
        }

        public byte[] Data
        {
            get { return data; }
        }
    }
}
