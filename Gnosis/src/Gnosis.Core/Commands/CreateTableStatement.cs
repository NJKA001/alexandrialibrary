using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Core.Commands
{
    public class CreateTableStatement : IStatement
    {
        private CreateTableStatement(string name)
        {
            builder.AppendFormat("create table if not exists {0} (", name);
        }

        public CreateTableStatement(TableInfo tableInfo)
            : this(tableInfo.Name)
        {
            AddColumns(tableInfo.Columns);

            foreach (var customDataType in tableInfo.CustomDataTypes)
                AddColumns(customDataType.Columns);
        }

        public CreateTableStatement(ChildInfo childInfo)
            : this(childInfo.TableName)
        {
            if (childInfo.PrimaryKey != null)
            {
                PrimaryKey(childInfo.PrimaryKey.Type, childInfo.PrimaryKey.Name, childInfo.PrimaryKey.IsAutoIncrement);
            }
            if (childInfo.ForeignKey != null)
            {
                Column(childInfo.ForeignKey.Type, childInfo.ForeignKey.Name);
            }
            if (childInfo.Sequence != null)
            {
                Column(childInfo.Sequence.Type, childInfo.Sequence.Name);
            }

            AddColumns(childInfo.BaseTable.Columns);
            foreach (var customDataType in childInfo.BaseTable.CustomDataTypes)
                AddColumns(customDataType.Columns);
        }

        private readonly StringBuilder builder = new StringBuilder();
        private bool hasColumns;

        #region Column Helper Methods

        private void AppendPrefix()
        {
            if (hasColumns)
                builder.Append(", ");
        }

        private void AddBlobColumn(string name, object defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} BLOB NOT NULL DEFAULT 0", name);

            hasColumns = true;
        }

        private void AddIntegerColumn(string name, object defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER NOT NULL DEFAULT {1}", name, defaultValue ?? 0);

            hasColumns = true;
        }

        private void AddNumericColumn(string name, object defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} NUMERIC NOT NULL DEFAULT {1}", name, defaultValue ?? 0);

            hasColumns = true;
        }

        private void AddRealColumn(string name, object defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} REAL NOT NULL DEFAULT {1}", name, defaultValue ?? 0);

            hasColumns = true;
        }

        private void AddTextColumn(string name, object defaultValue)
        {
            AppendPrefix();

            var defaultString = string.Empty;
            if (defaultValue != null)
            {
                if (defaultValue is DateTime)
                    defaultString = ((DateTime)defaultValue).ToString("s");
                else
                    defaultString = defaultValue.ToString();
            }

            builder.AppendFormat("{0} TEXT NOT NULL DEFAULT '{1}'", name, defaultString);

            hasColumns = true;
        }

        private void Column(Type type, string name)
        {
            Column(type, name, null);
        }

        private void Column(Type type, string name, object defaultValue)
        {
            var affinity = type.GetTypeAffinity();

            switch (affinity)
            {
                case TypeAffinity.Integer:
                    AddIntegerColumn(name, defaultValue);
                    break;
                case TypeAffinity.None:
                    AddBlobColumn(name, defaultValue);
                    break;
                case TypeAffinity.Numeric:
                    AddNumericColumn(name, defaultValue);
                    break;
                case TypeAffinity.Real:
                    AddRealColumn(name, defaultValue);
                    break;
                case TypeAffinity.Text:
                    AddTextColumn(name, defaultValue);
                    break;
                default:
                    break;
            }
        }

        private void BlobColumn(string name, object defaultValue)
        {
            AddBlobColumn(name, defaultValue);
        }

        private void IntegerColumn(string name, object defaultValue)
        {
            AddIntegerColumn(name, defaultValue);
        }

        private void RealColumn(string name, object defaultValue)
        {
            AddRealColumn(name, defaultValue);
        }

        private void TextColumn(string name, object defaultValue)
        {
            AddTextColumn(name, defaultValue);
        }

        #endregion

        #region Primary Key Helper Methods

        private void PrimaryKey(Type type, string name, bool isAutoIncrement)
        {
            switch (type.GetTypeAffinity())
            {
                case TypeAffinity.Integer:
                    if (isAutoIncrement)
                        PrimaryKeyIntegerAutoIncrement(name);
                    else
                        PrimaryKeyInteger(name);
                    break;
                case TypeAffinity.Numeric:
                    PrimaryKeyNumeric(name);
                    break;
                case TypeAffinity.Real:
                    PrimaryKeyReal(name);
                    break;
                case TypeAffinity.Text:
                    PrimaryKeyText(name);
                    break;
                default:
                    break;
            }
        }

        private void PrimaryKeyInteger(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER PRIMARY KEY", name);

            hasColumns = true;
        }

        private void PrimaryKeyIntegerAutoIncrement(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER PRIMARY KEY AUTOINCREMENT", name);

            hasColumns = true;
        }

        private void PrimaryKeyNumeric(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} NUMERIC PRIMARY KEY NOT NULL", name);

            hasColumns = true;
        }

        private void PrimaryKeyReal(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} REAL PRIMARY KEY NOT NULL", name);

            hasColumns = true;
        }

        private void PrimaryKeyText(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} TEXT PRIMARY KEY NOT NULL", name);

            hasColumns = true;
        }

        #endregion

        #region TableInfo Helper Methods

        private void AddColumns(IEnumerable<ColumnInfo> columns)
        {
            foreach (var column in columns)
            {
                if (column.IsPrimaryKey)
                {
                    PrimaryKey(column.ColumnType, column.Name, column.IsAutoIncrement);
                }
                else
                {
                    Column(column.ColumnType, column.Name, column.DefaultValue);
                }
            }
        }

        #endregion

        /*

        private void AddColumnsForOneToMany(OneToManyAttribute oneToMany, Type collectionType, Type itemType)
        {
            var collectionIsOrdered = collectionType.IsOrderedCollectionType();
            var collectionIsUnordered = collectionType.IsUnorderedCollectionType();
            var itemIsEntity = itemType.IsEntityType();
            var itemIsValue = itemType.IsValueType();

            if (!itemIsEntity && oneToMany.HasPrimaryKey)
            {
                if (oneToMany.PrimaryKeyType.IsTextColumn())
                {
                    PrimaryKeyText(oneToMany.PrimaryKeyName);
                }
                else if (oneToMany.PrimaryKeyType.IsIntegerColumn())
                {
                    if (oneToMany.PrimaryKeyIsAutoIncrement)
                        PrimaryKeyIntegerAutoIncrement(oneToMany.PrimaryKeyName);
                    else
                        PrimaryKeyInteger(oneToMany.PrimaryKeyName);
                }
            }
            if (oneToMany.HasForeignKey)
            {
                Column(oneToMany.ForeignKeyType, oneToMany.ForeignKeyName);
            }
            if (oneToMany.HasSequence && !collectionIsUnordered)
            {
                Column(oneToMany.SequenceType, oneToMany.SequenceName);
            }
        }

        private void AddColumnsForRootType(Type type)
        {
            AddColumnsForRootType(type, null);
        }

        private void AddColumnsForRootType(Type type, object instance)
        {
            foreach (var typeInterface in type.GetInterfaces())
            {
                AddColumns(typeInterface, instance);
            }

            AddColumns(type, instance);
        }

        private void AddColumns(Type type, object instance)
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
                    var collectionType = property.PropertyType.GetGenericTypeDefinition();
                    var genericArgs = property.PropertyType.GetGenericArguments();
                    if (genericArgs.Length > 0)
                    {
                        var itemType = genericArgs[0];
                        //commandBuilder.AddStatement(new CreateTableStatement(commandBuilder, oneToMany, collectionType, itemType));
                        
                        foreach (var foreignIndex in foreignIndices)
                        {
                            commandBuilder.AddStatement(new CreateIndexStatement(oneToMany.TableName, foreignIndex.Name, foreignIndex.IsUnique, foreignIndex.Columns));
                        }

                        foreach (var index in itemType.GetIndexInfo())
                        {
                            commandBuilder.AddStatement(new CreateIndexStatement(oneToMany.TableName, index.Name, index.IsUnique, index.Columns));
                        }
                    }
                }
                else if (includeColumn)
                {
                    AddColumn(instance, columnName, property, defaultValue, primaryKey);
                }
            }
        }

        private void AddColumn(object instance, string columnName, PropertyInfo property, object defaultValue, PrimaryKeyColumnAttribute primaryKey)
        {
            if (primaryKey != null)
            {
                if (property.PropertyType.IsIntegerColumn())
                {
                    if (primaryKey.AutoIncrement)
                        PrimaryKeyIntegerAutoIncrement(columnName);
                    else
                        PrimaryKeyInteger(columnName);
                }
                else if (property.PropertyType.IsTextColumn())
                {
                    PrimaryKeyText(columnName);
                }
            }
            else
            {
                if (property.PropertyType.IsCustomDataType())
                {
                    object dataTypeInstance = null;
                    if (instance != null)
                    {
                        dataTypeInstance = property.GetValue(instance, null);
                    }
                    else
                    {
                        if (property.PropertyType.IsTimeStampType())
                        {
                            dataTypeInstance = new TimeStamp(UriExtensions.EmptyUri);
                        }
                    }

                    AddColumnsForRootType(property.PropertyType, dataTypeInstance);
                }
                else
                {
                    Column(property.PropertyType, columnName, defaultValue);
                }
            }
        }

        */

        public override string ToString()
        {
            return builder.ToString() + ");";
        }
    }
}
