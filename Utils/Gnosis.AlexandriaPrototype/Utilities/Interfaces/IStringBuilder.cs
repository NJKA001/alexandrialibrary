using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Utilities.Interfaces
{
    public interface IStringBuilder
    {
        string PartDelimiter { get; set; }
        string TokenDelimiter { get; set; }
        string Prefix { get; set; }
        string Suffix { get; set; }
        IStringBuilder Append(string value);
        IStringBuilder Append(IStringBuilder builder);
        IStringBuilder AppendFormat(string format, params object[] args);
        IStringBuilder AppendLine(string value);
        IStringBuilder AppendPart(params string[] tokens);

        string ToString();
    }
}
