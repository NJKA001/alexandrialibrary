using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Tag
        : ITag
    {
        public Tag(string name, string nameSoundsLike, string nameAmericanized, ITagType type, Uri target, int searchWeight)
            : this(name, nameSoundsLike, nameAmericanized, type, target, searchWeight, 0)
        {
        }

        public Tag(string name, string nameSoundsLike, string nameAmericanized, ITagType type, Uri target, int searchWeight, long id)
        {
            this.name = name;
            this.nameSoundsLike = nameSoundsLike;
            this.nameAmericanized = nameAmericanized;
            this.type = type;
            this.target = target;
            this.searchWeight = searchWeight;
            this.id = id;
        }

        private readonly long id;
        private readonly string name;
        private readonly string nameSoundsLike;
        private readonly string nameAmericanized;
        private readonly ITagType type;
        private readonly Uri target;
        private readonly int searchWeight;

        #region ITag Members

        public long Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string NameSoundsLike
        {
            get { return nameSoundsLike; }
        }

        public string NameAmericanized
        {
            get { return nameAmericanized; }
        }

        public ITagType Type
        {
            get { return type; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public int SearchWeight
        {
            get { return searchWeight; }
        }

        #endregion
    }
}
