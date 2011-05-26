using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;

namespace Gnosis.Core.Batches
{
    public class InitializeTypeBatch : Batch
    {
        public InitializeTypeBatch(Func<IDbConnection> getConnection, Type type)
            : base(getConnection)
        {
            var tableInfo = type.GetTableInfo();

            Add(GetRootCommandBuilder(tableInfo, type.GetDefaultInstance()));
            
            foreach (var childInfo in tableInfo.Children)
            {
                Add(GetChildCommandBuilder(tableInfo, childInfo));
            }
        }

        private static ICommandBuilder GetRootCommandBuilder(TableInfo tableInfo, object instance)
        {
            var builder = new CommandBuilder();
            builder.AddStatement(new CreateTableStatement(tableInfo, instance));
            foreach (var indexInfo in tableInfo.Indices)
            {
                builder.AddStatement(new CreateIndexStatement(tableInfo, indexInfo));
            }
            return builder;
        }

        private static ICommandBuilder GetChildCommandBuilder(TableInfo tableInfo, ChildInfo childInfo)
        {
            var builder = new CommandBuilder();
            builder.AddStatement(new CreateTableStatement(tableInfo, childInfo, childInfo.ChildType.GetDefaultInstance()));
            return builder;
        }
    }
}
