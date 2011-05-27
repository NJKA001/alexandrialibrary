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

            AddRootCommandBuilder(tableInfo);
            AddChildrenCommandBuilders(tableInfo.Children);
        }

        private void AddRootCommandBuilder(TableInfo tableInfo)
        {
            var builder = new CommandBuilder();
            builder.AddStatement(new CreateTableStatement(tableInfo));
            
            //AddIndexStatements(builder, tableInfo.Name, tableInfo.Indices);
            Add(builder);
        }

        private void AddChildrenCommandBuilders(IEnumerable<ChildInfo> children)
        {
            foreach (var childInfo in children)
            {
                var builder = new CommandBuilder();
                builder.AddStatement(new CreateTableStatement(childInfo));

                //AddIndexStatements(builder, childInfo.TableName, childInfo.ForeignIndices);
                //AddIndexStatements(builder, childInfo.TableName, childInfo.BaseTable.Indices);
                Add(builder);

                AddChildrenCommandBuilders(childInfo.BaseTable.Children);
            }
        }

        //private void AddIndexStatements(ICommandBuilder builder, string tableName, IEnumerable<IndexInfo> indices)
        //{
        //    foreach (var indexInfo in indices)
        //    {
        //        builder.AddStatement(new CreateIndexStatement(tableName, indexInfo));
        //    }
        //}
    }
}
