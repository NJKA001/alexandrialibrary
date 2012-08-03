using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public class MediaType
        : IMediaType
    {
        private string name;

        private void InitializeTypes()
        {
            try
            {
                Supertype = MediaSupertype.application;
                Subtype = string.Empty;

                if (name != null)
                {
                    var firstSlash = name.IndexOf('/');
                    if (firstSlash > -1)
                    {
                        var superToken = name.Substring(0, firstSlash);
                        var parsedSupertype = MediaSupertype.application;
                        Enum.TryParse<MediaSupertype>(superToken, out parsedSupertype);

                        Supertype = parsedSupertype;

                        if (firstSlash + 1 < name.Length && name.Length - (firstSlash + 1) > 0)
                        {
                            Subtype = name.Substring(firstSlash + 1, name.Length - (firstSlash + 1));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                InitializeTypes();
            }
        }

        public string Description
        {
            get;
            set;
        }

        public MediaSupertype Supertype
        {
            get;
            private set;
        }

        public string Subtype
        {
            get;
            private set;
        }

        public IEnumerable<string> FileExtensions
        {
            get;
            set;
        }

        public IEnumerable<string> LegacyMediaTypes
        {
            get;
            set;
        }

        public IEnumerable<byte[]> MagicNumbers
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Enum.GetName(typeof(MediaSupertype), Supertype).ToLower(), Subtype);
        }
    }
}
