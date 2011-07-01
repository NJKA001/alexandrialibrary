using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class ContentType
        : IContentType
    {
        private ContentType(IMediaType type)
            : this(type, null, null)
        {
        }

        private ContentType(IMediaType type, ICharacterSet charSet)
            : this(type, charSet, null)
        {
        }

        private ContentType(IMediaType type, ICharacterSet charSet, string boundary)
        {
            this.type = type;
            this.boundary = boundary;
            this.charSet = charSet;
        }

        private readonly IMediaType type;
        private readonly ICharacterSet charSet;
        private readonly string boundary;

        private const string charSetFieldName = "charset=";
        private const string boundaryFieldName = "boundary=";

        #region IContentType Members

        public IMediaType Type
        {
            get { return type; }
        }

        public ICharacterSet CharSet
        {
            get { return charSet; }
        }

        public string Boundary
        {
            get { return boundary; }
        }

        #endregion

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(type.ToString());

            if (charSet != null && charSet != CharacterSet.Unknown)
                builder.AppendFormat(";{0}{1}", charSetFieldName, charSet.ToString());

            if (!string.IsNullOrEmpty(boundary))
                builder.AppendFormat(";{0}{1}", boundaryFieldName, boundary);

            return builder.ToString();
        }

        public static IContentType Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new ContentType(MediaType.Unknown);

            var tokens = value.Split(new string[] { "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens == null || tokens.Length == 0)
                return new ContentType(MediaType.Unknown);

            var type = MediaType.Unknown;
            ICharacterSet charSet = null;
            string boundary = null;

            var token = string.Empty;
            for (var i = 0; i < tokens.Length; i++)
            {
                token = tokens[i].Trim();
                if (i == 0)
                {
                    type = MediaType.Parse(token);
                }
                else
                {
                    if (token.Contains(charSetFieldName) && token.Length > 8)
                    {
                        charSet = CharacterSet.Parse(token.Substring(8).Trim());
                    }
                    else if (token.Contains(boundaryFieldName) && token.Length > 9)
                    {
                        boundary = token.Substring(9);
                    }
                }
            }

            return new ContentType(type, charSet, boundary);
        }
    }
}
