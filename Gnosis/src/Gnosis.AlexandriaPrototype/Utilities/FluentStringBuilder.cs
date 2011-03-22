using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnosis.Alexandria.Utilities.Interfaces;

namespace Gnosis.Alexandria.Utilities
{
    public class FluentStringBuilder : IStringBuilder
    {
        public FluentStringBuilder() 
            : this(", ", " ", string.Empty, string.Empty)
        {
        }

        public FluentStringBuilder(string partDelimiter, string tokenDelimiter)
            : this(partDelimiter, tokenDelimiter, string.Empty, string.Empty)
        {

        }

        public FluentStringBuilder(string partDelimiter, string tokenDelimiter, string prefix, string suffix)
        {
            PartDelimiter = partDelimiter;
            TokenDelimiter = tokenDelimiter;
            Prefix = prefix;
            Suffix = suffix;
        }

        private readonly StringBuilder _builder = new StringBuilder();

        public string PartDelimiter { get; set; }
        public string TokenDelimiter { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public IStringBuilder Append(string value)
        {
            _builder.Append(value);
            return this;
        }

        public IStringBuilder Append(IStringBuilder builder)
        {
            if (builder == null)
                return this;

            _builder.Append(builder.ToString());
            return this;
        }

        public IStringBuilder AppendPart(params string[] tokens)
        {
            if (_builder.Length > 0)
                Append(PartDelimiter);

            var first = true;
            foreach (var token in tokens)
            {
                if (!first)
                    Append(TokenDelimiter);

                Append(token);
                first = false;
            }

            return this;
        }

        public IStringBuilder AppendFormat(string format, params object[] args)
        {
            _builder.AppendFormat(format, args);
            return this;
        }

        public IStringBuilder AppendLine(string value)
        {
            _builder.AppendLine(value);
            return this;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", Prefix, _builder.ToString(), Suffix);
        }
    }
}
