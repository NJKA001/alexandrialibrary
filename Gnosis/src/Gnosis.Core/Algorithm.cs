using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Algorithm
        : IAlgorithm
    {
        private Algorithm(int id, string name, Func<string, string> convertString, Func<byte[], string> convertBytes)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.id = id;
            this.name = name;
            this.convertString = convertString;
            this.convertBytes = convertBytes;
        }

        private readonly int id;
        private readonly string name;
        private readonly Func<string, string> convertString;
        private readonly Func<byte[], string> convertBytes;

        #region IAlgorithm Members

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Execute(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return convertString != null ?
                convertString(value)
                : null;
        }

        public string Execute(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return convertBytes != null ?
                convertBytes(value)
                : null;
        }

        #endregion

        static Algorithm()
        {
            InitializeAlgorithms();

            foreach (var algorithm in algorithms)
            {
                byId[algorithm.Id] = algorithm;
                byName[algorithm.Name] = algorithm;
            }
        }

        private static void InitializeAlgorithms()
        {
            algorithms.Add(Default);
            algorithms.Add(DoubleMetaphone);
            algorithms.Add(Americanized);
            algorithms.Add(Md5);
            algorithms.Add(Sha1);
        }

        private static readonly IList<IAlgorithm> algorithms = new List<IAlgorithm>();
        private static readonly IDictionary<int, IAlgorithm> byId = new Dictionary<int, IAlgorithm>();
        private static readonly IDictionary<string, IAlgorithm> byName = new Dictionary<string, IAlgorithm>();

        #region Algorithms

        public static IEnumerable<IAlgorithm> GetAll()
        {
            return algorithms;
        }

        public static IAlgorithm Default = new Algorithm(1, "Default", x => x, null);
        public static IAlgorithm DoubleMetaphone = new Algorithm(2, "Double Metaphone", x => x.ToDoubleMetaphoneString(), null);
        public static IAlgorithm Americanized = new Algorithm(3, "Americanized", x => x.ToAmericanizedString(), null);
        public static IAlgorithm Md5 = new Algorithm(4, "MD5", x => x.ToMd5Hash(), x => x.ToMd5Hash());
        public static IAlgorithm Sha1 = new Algorithm(5, "SHA1", x => x.ToSha1Hash(), x => x.ToSha1Hash());

        #endregion

        public static IAlgorithm Parse(int id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : null;
        }

        public static IAlgorithm Parse(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return byName.ContainsKey(name) ?
                byName[name]
                : null;
        }
    }
}
