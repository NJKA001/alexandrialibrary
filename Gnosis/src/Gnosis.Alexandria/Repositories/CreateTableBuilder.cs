using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Repositories
{
    public class CreateTableBuilder : IStatementBuilder
    {
        public CreateTableBuilder(string name)
        {
            builder = new StringBuilder();
            builder.AppendFormat("create table if not exists {0} (", name);
        }

        public CreateTableBuilder(CreateCommandBuilder commandBuilder, string name, Type type, object instance)
            : this(name)
        {
            this.commandBuilder = commandBuilder;
            AddColumnsForRootType(type, instance);
        }

        private CreateTableBuilder(CreateCommandBuilder commandBuilder, OneToManyAttribute oneToMany, Type collectionType, Type itemType)
            : this(oneToMany.TableName)
        {
            this.commandBuilder = commandBuilder;
            AddColumnsForOneToMany(oneToMany, collectionType, itemType);
            AddColumnsForRootType(itemType);
        }

        private readonly CreateCommandBuilder commandBuilder;
        private readonly StringBuilder builder;
        private bool hasColumns;

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

        private void AddColumnsForOneToMany(OneToManyAttribute oneToMany, Type collectionType, Type itemType)
        {
            var collectionIsOrdered = collectionType.IsOrderedCollectionType();
            var collectionIsUnordered = collectionType.IsUnorderedCollectionType();
            var itemIsEntity = itemType.IsEntityType();
            var itemIsValue = itemType.IsValueType();

            if (oneToMany.HasPrimaryKey && !itemIsEntity)
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
            if ((oneToMany.HasSequence || collectionIsOrdered) && !collectionIsUnordered)
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
                        commandBuilder.AddStatement(new CreateTableBuilder(commandBuilder, oneToMany, collectionType, itemType));
                        
                        foreach (var foreignIndex in foreignIndices)
                        {
                            commandBuilder.AddStatement(new CreateIndexBuilder(oneToMany.TableName, foreignIndex.Name, foreignIndex.IsUnique, foreignIndex.Columns));
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

        public CreateTableBuilder PrimaryKeyInteger(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER PRIMARY KEY", name);

            hasColumns = true;

            return this;
        }

        public CreateTableBuilder PrimaryKeyIntegerAutoIncrement(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER PRIMARY KEY AUTOINCREMENT", name);

            hasColumns = true;

            return this;
        }

        public CreateTableBuilder PrimaryKeyText(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} TEXT PRIMARY KEY NOT NULL", name);

            hasColumns = true;

            return this;
        }

        public CreateTableBuilder Column(Type type, string name)
        {
            return Column(type, name, null);
        }

        public CreateTableBuilder Column(Type type, string name, object defaultValue)
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

            return this;
        }

        public CreateTableBuilder BlobColumn(string name, object defaultValue)
        {
            AddBlobColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder IntegerColumn(string name, object defaultValue)
        {
            AddIntegerColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder RealColumn(string name, object defaultValue)
        {
            AddRealColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder TextColumn(string name, object defaultValue)
        {
            AddTextColumn(name, defaultValue);
            return this;
        }

        public override string ToString()
        {
            return builder.ToString() + ");";
        }
    }
}
