﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Mapping
{
	public abstract class ClassMapBase<T>
		: IClassMap<T>
		where T : IEntity
	{
		protected ClassMapBase(IContext context, string table)
		{
			_context = context;
			_table = table;
		}

		private const string TYPE_INTEGER = "INTEGER";
		private const string TYPE_REAL = "REAL";
		private const string TYPE_TEXT = "TEXT";

		private readonly IContext _context;
		private readonly string _table;

		private string GetDataType(Type type)
		{
			switch (type.Name)
			{
				case "Byte":
				case "SByte":
				case "Int16":
				case "UInt16":
				case "Int32":
				case "UInt32":
				case "Int64":
				case "UInt64":
					return TYPE_INTEGER;
				case "Float":
				case "Double":
				case "Decimal":
					return TYPE_REAL;
				default:
					return TYPE_TEXT;
			}
		}

		protected IDictionary<string, Type> Columns = new Dictionary<string, Type>();

		protected IContext Context
		{
			get { return _context; }
		}

		protected abstract T CreateInstance(long id);
		protected abstract object GetValue(T entity, string column);
		protected abstract void SetValue(T entity, string column, object value);

		#region IClassMap<T> Members

		public string Key
		{
			get { return "Id"; }
		}

		public string Table
		{
			get { return _table; }
		}

		public virtual string GetValue(object value)
		{
			if (value == null)
				return "''";

			if (value.GetType() == typeof(DateTime))
				return string.Format("{0:yyyy-MM-dd}", value);

			var type = GetDataType(value.GetType());

			switch (type)
			{
				case TYPE_INTEGER:
				case TYPE_REAL:
					return value.ToString();
				case TYPE_TEXT:
				default:
					return string.Format("'{0}'", value);
			}
		}

		public virtual string GetInitializeCommandText()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("CREATE TABLE IF NOT EXISTS {0} ({1} {2} PRIMARY KEY AUTOINCREMENT", Table, Key, GetDataType(typeof(int)));

			foreach (KeyValuePair<string, Type> column in Columns)
			{
				builder.AppendFormat(", {0} {1} NOT NULL", column.Key, GetDataType(column.Value));
			}

			builder.Append(")");

			return builder.ToString();
		}

		public virtual IEnumerable<string> GetSaveCommandTexts(T entity)
		{
			List<string> texts = new List<string>();

			var cols = new StringBuilder();
			cols.AppendFormat("REPLACE INTO {0} ({1}", Table, Key);
			var vals = new StringBuilder();
			vals.Append(") VALUES (");

			vals.Append(entity.Id > 0 ? GetValue(entity.Id) : "NULL");

			foreach (KeyValuePair<string, Type> column in Columns)
			{
				cols.AppendFormat(", {0}", column.Key);
				vals.AppendFormat(", {0}", GetValue(GetValue(entity, column.Key)));
			}
			
			vals.Append(")");

			texts.Add(cols.ToString() + vals.ToString());

			return texts;
		}

		public virtual IEnumerable<string> GetDeleteCommandTexts(long id)
		{
			List<string> texts = new List<string>();

			var builder = new StringBuilder();
			builder.AppendFormat("DELETE FROM {0} WHERE {1} = {2}", Table, Key, GetValue(id));

			texts.Add(builder.ToString());

			return texts;
		}

		public virtual IList<T> Load(IDataReader reader)
		{
			List<T> list = new List<T>();

			if (reader != null)
			{
				while (reader.Read())
				{
					long id = Convert.ToInt64(reader[Key]);

					T entity = CreateInstance(id);

					foreach (KeyValuePair<string, Type> column in Columns)
					{
						SetValue(entity, column.Key, reader[column.Key]);
					}

					list.Add(entity);
				}
			}
			return list;
		}

		#endregion
	}
}
