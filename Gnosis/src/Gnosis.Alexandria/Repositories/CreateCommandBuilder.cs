using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Repositories
{
    public class CreateCommandBuilder : CommandBuilder
    {
        public CreateCommandBuilder(Type type, object instance)
        {
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is TableAttribute)
                {
                    table = attribute as TableAttribute;
                }
                else if (attribute is IndexAttribute)
                {
                    indices.Add(attribute as IndexAttribute);
                }
            }

            if (table != null)
            {
                tableBuilder = new CreateTableBuilder(table.Name);
                Add(tableBuilder);
                AddColumnsForRootType(tableBuilder, type, instance);

                foreach (var index in indices)
                {
                    var indexBuilder = new CreateIndexBuilder(index.Name, index.IsUnique, table.Name);
                    foreach (var column in index.Columns)
                        indexBuilder.Column(column);

                    indexBuilders.Add(indexBuilder);
                }

                Add(tableBuilder);
                foreach (var indexBuilder in indexBuilders)
                    Add(indexBuilder);
            }
        }

        private readonly TableAttribute table;
        private readonly CreateTableBuilder tableBuilder;
        private readonly IList<IndexAttribute> indices = new List<IndexAttribute>();
        private readonly IList<CreateIndexBuilder> indexBuilders = new List<CreateIndexBuilder>();

        private void AddColumnsForRootType(CreateTableBuilder createTable, Type type, object instance)
        {
            foreach (var typeInterface in type.GetInterfaces())
            {
                AddColumns(createTable, typeInterface, instance);
            }

            AddColumns(createTable, type, instance);
        }

        private void AddColumns(CreateTableBuilder createTable, Type type, object instance)
        {
            foreach (var property in type.GetProperties())
            {
                bool includeColumn = true;
                string columnName = property.Name;
                object defaultValue = null;
                PrimaryKeyColumnAttribute primaryKey = null;
                OneToManyAttribute oneToMany = null;
                var foreignIndices = new List<ForeignIndexAttribute>();

                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is ColumnIgnoreAttribute)
                    {
                        includeColumn = false;
                        continue;
                    }

                    if (attribute is OneToManyAttribute)
                        oneToMany = attribute as OneToManyAttribute;

                    if (attribute is ForeignIndexAttribute)
                    {
                        foreignIndices.Add(attribute as ForeignIndexAttribute);
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
                                if (oneToMany.PrimaryKeyIsAutoIncrement)
                                    createItemTable.PrimaryKeyIntegerAutoIncrement(oneToMany.PrimaryKeyName);
                                else
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
                        Add(createItemTable);

                        foreach (var foreignIndex in foreignIndices)
                        {
                            var createForeignIndex = new CreateIndexBuilder(foreignIndex.Name, foreignIndex.IsUnique, oneToMany.Name);
                            foreach (var foreignColumn in foreignIndex.Columns)
                                createForeignIndex.Column(foreignColumn);

                            Add(createForeignIndex);
                        }

                        AddColumnsForRootType(createItemTable, itemType, null);
                    }
                }
                else if (includeColumn)
                {
                    AddColumn(createTable, instance, columnName, property, defaultValue, primaryKey);
                }
            }
        }

        private void AddColumn(CreateTableBuilder createTable, object instance, string columnName, PropertyInfo property, object defaultValue, PrimaryKeyColumnAttribute primaryKey)
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

                    AddColumnsForRootType(createTable, property.PropertyType, dataTypeInstance);
                }
                else
                {
                    createTable.Column(property.PropertyType, columnName, defaultValue);
                }
            }
        }
    }
}
