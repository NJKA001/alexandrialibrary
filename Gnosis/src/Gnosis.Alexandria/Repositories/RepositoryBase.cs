using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

using log4net;

using Gnosis.Core;
using Gnosis.Core.Attributes;
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

        private void AddColumnsForRootType(CommandBuilder builder, CreateTableBuilder createTable, Type type, object instance)
        {
            foreach (var typeInterface in type.GetInterfaces())
            {
                AddColumns(builder, createTable, typeInterface, instance);
            }

            AddColumns(builder, createTable, type, instance);
        }

        private void AddColumns(CommandBuilder builder, CreateTableBuilder createTable, Type type, object instance)
        {
            foreach (var property in type.GetProperties())
            {
                bool includeColumn = true;
                string columnName = property.Name;
                object defaultValue = null;
                PrimaryKeyColumnAttribute primaryKey = null;
                OneToManyAttribute oneToMany = null;
                var foreignIndices = new List<KeyAttribute>();

                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is ColumnIgnoreAttribute)
                    {
                        includeColumn = false;
                        continue;
                    }

                    if (attribute is OneToManyAttribute)
                        oneToMany = attribute as OneToManyAttribute;

                    if (attribute is ForeignIndexAttribute || attribute is ForeignUniqueIndexAttribute)
                    {
                        foreignIndices.Add(attribute as KeyAttribute);
                    }

                    if (attribute is PrimaryKeyColumnAttribute)
                        primaryKey = attribute as PrimaryKeyColumnAttribute;

                    if (attribute is ColumnAttribute)
                    {
                        var column = attribute as ColumnAttribute;
                        columnName = column.Name;
                        defaultValue = (column.DefaultValue != null || instance == null) ? column.DefaultValue : property.GetValue(instance, null);
                    }
                }

                if (oneToMany != null)
                {
                    var genericArgs = property.PropertyType.GetGenericArguments();
                    if (genericArgs.Length > 0)
                    {
                        var itemType = genericArgs[0];
                        var createItemTable = new CreateTableBuilder(oneToMany.Name);
                        if (oneToMany.HasPrimaryKey)
                        {
                            if (oneToMany.PrimaryKeyType.IsTextColumn())
                            {
                                createItemTable.PrimaryKeyText(oneToMany.PrimaryKeyName);
                            }
                            else if (oneToMany.PrimaryKeyType.IsIntegerColumn())
                            {
                                createItemTable.PrimaryKeyInteger(oneToMany.PrimaryKeyName);
                            }
                        }
                        if (oneToMany.HasForeignKey)
                        {
                            createItemTable.Column(oneToMany.ForeignKeyType, oneToMany.ForeignKeyName);
                        }
                        if (oneToMany.HasSequence)
                        {
                            createItemTable.Column(oneToMany.SequenceType, oneToMany.SequenceName);
                        }
                        builder.Add(createItemTable);

                        foreach (var foreignIndex in foreignIndices)
                        {
                            var createForeignIndex = new CreateIndexBuilder(foreignIndex.Name, oneToMany.Name);
                            foreach (var foreignColumn in foreignIndex.Columns)
                                createForeignIndex.Column(foreignColumn);

                            builder.Add(createForeignIndex);
                        }

                        AddColumnsForRootType(builder, createItemTable, itemType, null);
                    }
                }
                else if (includeColumn)
                {
                    AddColumn(builder, createTable, instance, columnName, property, defaultValue, primaryKey);
                }
            }
        }

        private void AddColumn(CommandBuilder builder, CreateTableBuilder createTable, object instance, string columnName, PropertyInfo property, object defaultValue, PrimaryKeyColumnAttribute primaryKey)
        {
            if (primaryKey != null)
            {
                if (property.PropertyType.IsIntegerColumn())
                {
                    if (primaryKey.AutoIncrement)
                        createTable.PrimaryKeyIntegerAutoIncrement(columnName);
                    else
                        createTable.PrimaryKeyInteger(columnName);
                }
                else if (property.PropertyType.IsTextColumn())
                {
                    createTable.PrimaryKeyText(columnName);
                }
            }
            else
            {
                if (property.PropertyType.IsDataTypeColumn())
                {
                    object dataTypeInstance = null;
                    if (instance != null)
                    {
                        dataTypeInstance = property.GetValue(instance, null);
                    }
                    else
                    {
                        if (property.PropertyType is ITimeStamp)
                        {
                            dataTypeInstance = new TimeStamp(UriExtensions.EmptyUri);
                        }
                    }
                    
                    AddColumnsForRootType(builder, createTable, property.PropertyType, dataTypeInstance);
                }
                else
                {
                    createTable.Column(property.PropertyType, columnName, defaultValue);
                }
            }
        }

        private string GetCreateCommandText(Type type, object instance)
        {
            CommandBuilder builder = null;

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
                builder = new CommandBuilder(tableBuilder);
                AddColumnsForRootType(builder, tableBuilder, type, instance);

                foreach (var key in keys)
                {
                    var indexBuilder = new CreateIndexBuilder(key.Name, table.Name);
                    foreach (var column in key.Columns)
                        indexBuilder.Column(column);

                    indexBuilders.Add(indexBuilder);
                }

                builder.Add(tableBuilder);
                foreach (var indexBuilder in indexBuilders)
                    builder.Add(indexBuilder);
            }

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
