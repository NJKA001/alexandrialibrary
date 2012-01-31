using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace Gnosis
{
    public class MediaType
        : IMediaType
    {
        protected internal MediaType(MediaSupertype supertype, string subtype, bool isDefault)
            : this(supertype, subtype, isDefault, null, new List<string>(), new List<string>(), new List<byte[]>())
        {
        }

        protected internal MediaType(MediaSupertype supertype, string subtype, bool isDefault, Func<Uri, IContentType, IMedia> createFunction)
            : this(supertype, subtype, isDefault, createFunction, new List<string>(), new List<string>(), new List<byte[]>())
        {
        }

        protected internal MediaType(MediaSupertype supertype, string subtype, bool isDefault, Func<Uri, IContentType, IMedia> createFunction, IEnumerable<string> fileExtensions)
            : this(supertype, subtype, isDefault, createFunction, fileExtensions, new List<string>(), new List<byte[]>())
        {
        }

        protected internal MediaType(MediaSupertype supertype, string subtype, bool isDefault, Func<Uri, IContentType, IMedia> createFunction, IEnumerable<string> fileExtensions, IEnumerable<string> legacyTypes)
            : this(supertype, subtype, isDefault, createFunction, fileExtensions, legacyTypes, new List<byte[]>())
        {
        }

        protected internal MediaType(MediaSupertype supertype, string subtype, bool isDefault, Func<Uri, IContentType, IMedia> createFunction, IEnumerable<string> fileExtensions, IEnumerable<string> legacyTypes, IEnumerable<byte[]> magicNumbers)
        {
            if (subtype == null)
                throw new ArgumentNullException("subtype");
            if (fileExtensions == null)
                throw new ArgumentNullException("fileExtensions");
            if (legacyTypes == null)
                throw new ArgumentNullException("legacyTypes");
            if (magicNumbers == null)
                throw new ArgumentNullException("magicNumbers");

            this.supertype = supertype;
            this.subtype = subtype;
            this.isDefault = isDefault;
            this.createFunction = createFunction;
            this.fileExtensions = fileExtensions;
            this.legacyTypes = legacyTypes;
            this.magicNumbers = magicNumbers;
        }

        private readonly MediaSupertype supertype;
        private readonly string subtype;
        private readonly bool isDefault;
        private readonly Func<Uri, IContentType, IMedia> createFunction;
        private readonly IEnumerable<string> fileExtensions;
        private readonly IEnumerable<string> legacyTypes;
        private readonly IEnumerable<byte[]> magicNumbers;
        
        #region IMediaType Members

        public bool IsDefault
        {
            get { return isDefault; }
        }

        public MediaSupertype Supertype
        {
            get { return supertype; }
        }

        public string Subtype
        {
            get { return subtype; }
        }

        public IEnumerable<string> FileExtensions
        {
            get { return fileExtensions; }
        }

        public IEnumerable<string> LegacyTypes
        {
            get { return legacyTypes; }
        }

        public IEnumerable<byte[]> MagicNumbers
        {
            get { return magicNumbers; }
        }

        public IMedia CreateMedia(Uri location, IContentType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            return createFunction != null ?
                createFunction(location, type)
                : null;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}/{1}", supertype.ToString().ToLower(), subtype.ToLower());
        }
    }
}
