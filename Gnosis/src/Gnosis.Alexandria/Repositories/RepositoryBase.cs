using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

using log4net;

using Gnosis.Core;
using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase<T>
    {
        protected RepositoryBase(IContext context)
        {
            this.context = context;
            //this.database = "Catalog.db";
            //this.rootTable = rootTable;

            Initialize();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(RepositoryBase<T>));
        private readonly IContext context;
        private string database = "Catalog.db";
        //private string rootTable;

        private void Initialize()
        {
            try
            {
                var instance = Create();
                var commandText = GetCreateCommandText(typeof(T), instance);

                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = commandText; //GetInitializeText();
                        //command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RepositoryBase.Initialize", ex);
            }
        }

        private bool IsDataTypeColumn(Type type)
        {
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is DataTypeAttribute)
                    return true;
            }

            return false;
        }

        private bool IsOneToManyColumn(PropertyInfo property)
        {
            foreach (var attribute in property.GetCustomAttributes(true))
            {
                if (attribute is OneToManyAttribute)
                    return true;
            }

            return false;
        }

        private void AddColumn(CreateTableBuilder tableBuilder, object instance, string columnName, PropertyInfo property, object defaultValue, PrimaryKeyColumnAttribute primaryKey)
        {
            if (primaryKey != null)
            {
                if (property.PropertyType.IsIntegerColumn())
                {
                    if (primaryKey.AutoIncrement)
                        tableBuilder.PrimaryKeyIntegerAutoIncrement(columnName);
                    else
                        tableBuilder.PrimaryKeyInteger(columnName);
                }
                else if (property.PropertyType.IsTextColumn())
                {
                    tableBuilder.PrimaryKeyText(columnName);
                }
            }
            else
            {
                if (IsDataTypeColumn(property.PropertyType))
                {
                    var dataType = property.GetValue(instance, null);
                    AddColumnsForRootType(tableBuilder, property.PropertyType, dataType);
                }
                else if (property.PropertyType.IsTextColumn())
                {
                    tableBuilder.TextColumn(columnName, defaultValue);
                }
                else if (property.PropertyType.IsIntegerColumn())
                {
                    tableBuilder.IntegerColumn(columnName, defaultValue);
                }
                else if (property.PropertyType.IsRealColumn())
                {
                    tableBuilder.RealColumn(columnName, defaultValue);
                }
                else if (property.PropertyType.IsBlobColumn())
                {
                    tableBuilder.BlobColumn(columnName, defaultValue);
                }
            }
        }

        private void AddColumnsForRootType(CreateTableBuilder tableBuilder, Type type, object instance)
        {
            AddColumns(tableBuilder, type, instance);

            foreach (var typeInterface in type.GetInterfaces())
            {
                AddColumns(tableBuilder, typeInterface, instance);
            }
        }

        private void AddColumns(CreateTableBuilder tableBuilder, Type type, object instance)
        {
            foreach (var property in type.GetProperties())
            {
                bool includeColumn = true;
                string columnName = property.Name;
                object defaultValue = null;
                PrimaryKeyColumnAttribute primaryKey = null;
                OneToManyAttribute oneToMany = null;

                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is ColumnIgnoreAttribute)
                    {
                        includeColumn = false;
                        continue;
                    }

                    if (attribute is OneToManyAttribute)
                        oneToMany = attribute as OneToManyAttribute;

                    if (attribute is PrimaryKeyColumnAttribute)
                        primaryKey = attribute as PrimaryKeyColumnAttribute;

                    if (attribute is ColumnAttribute)
                    {
                        var column = attribute as ColumnAttribute;
                        columnName = column.Name;
                        defaultValue = column.DefaultValue != null ? column.DefaultValue : property.GetValue(instance, null);
                    }
                }

                if (oneToMany != null)
                {
                    var x = property.PropertyType;
                    //TODO: Create foreign table based on OneToManyAttribute
                }
                else if (includeColumn)
                {
                    AddColumn(tableBuilder, instance, columnName, property, defaultValue, primaryKey);
                }
            }
        }

        private string GetCreateCommandText(Type type, object instance)
        {
            var builder = new StringBuilder();

            TableAttribute table = null;
            CreateTableBuilder tableBuilder = null;
            var keys = new List<KeyAttribute>();
            var indexBuilders = new List<CreateIndexBuilder>();

            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is TableAttribute)
                {
                    table = attribute as TableAttribute;
                }
                else if (attribute is KeyAttribute)
                {
                    keys.Add(attribute as KeyAttribute);
                }
            }

            if (table != null)
            {
                tableBuilder = new CreateTableBuilder(table.Name);
                AddColumnsForRootType(tableBuilder, type, instance);

                foreach (var key in keys)
                {
                    var indexBuilder = new CreateIndexBuilder(key.Name, table.Name);
                    foreach (var column in key.Columns)
                        indexBuilder.Column(column);

                    indexBuilders.Add(indexBuilder);
                }
            }

            builder.AppendLine(tableBuilder.ToString());
            foreach (var indexBuilder in indexBuilders)
                builder.AppendLine(indexBuilder.ToString());

            return builder.ToString();
        }

        protected abstract T Create();
        //protected abstract string GetInitializeText();

        protected IContext Context
        {
            get { return context; }
        }

        protected static void AddParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        protected IDbConnection GetConnection()
        {
            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", database));
        }
    }
}
