using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface ICommandBuilder
    {
        void AddQuotedParameter(string name, object value);
        void AddUnquotedParameter(string name, object value);

        void Append(string value);
        void AppendFormat(string format, params object[] args);
        void AppendFormatLine(string format, params object[] args);
        void AppendLine();
        void AppendLine(string value);

        string CommandText { get; }
        int CommandTimeout { get; set; }
        CommandType CommandType { get; set; }        

        IDbCommand ToCommand(IDbConnection connection);
    }
}
