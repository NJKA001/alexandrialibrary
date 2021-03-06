﻿#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Telesophy.Babel.Persistence
{
	public abstract class Entity : NamedItem
	{
		#region Constructors
		protected Entity(string name, Type type) : base(name)
		{
			this.type = type;
		}
		#endregion
		
		#region Private Constants
		private const string PARENT_LINK_FIELD_NAME = "_ParentId";
		private const string DATE_MODIFIED_FIELD_NAME = "_DateModified";
		#endregion
			
		#region Private Fields
		private Schema schema;
		private Type type;
		private NamedItemCollection<Field> fields = new NamedItemCollection<Field>();
		private NamedItemCollection<Association> associations = new NamedItemCollection<Association>();
		private Dictionary<Field, bool> defaultSortOrder = new Dictionary<Field, bool>();		
		#endregion
		
		#region Public Properties
		public Schema Schema
		{
			get { return schema; }
		}
		
		public Type Type
		{
			get { return type; }
		}

		public abstract Field Identifier { get; }
		
		public NamedItemCollection<Field> Fields
		{
			get { return fields; }
		}
		
		public NamedItemCollection<Association> Associations
		{
			get { return associations; }
		}

		public Dictionary<Field, bool> DefaultSortOrder
		{
			get { return defaultSortOrder; }
		}
		
		public virtual string ParentLinkFieldName
		{
			get { return PARENT_LINK_FIELD_NAME; }
		}
		
		public virtual string DateModifiedFieldName
		{
			get { return DATE_MODIFIED_FIELD_NAME; }
		}
		#endregion
		
		#region Public Methods
		public virtual void Initialize(Schema schema)
		{
			this.schema = schema;
		}
		
		public virtual DataTable GetDataTable(string name)
		{
			DataTable table = new DataTable(name);
			
			foreach (Field field in Fields)
			{
				table.Columns.Add(field.Name, field.Type);
			}
			
			table.Columns.Add(DateModifiedFieldName, typeof(DateTime));
			
			return table;
		}
		
		public virtual DataTable GetDataTable(Map map)
		{
			DataTable table = GetDataTable(map.Name);
			table.Columns.Add(ParentLinkFieldName, map.Root.Identifier.Type);
			return table;
		}

		public virtual string GetFieldList()
		{
			StringBuilder list = new StringBuilder();
			
			const string COMMA = ", ";
			const string FORMAT_FIELD = "{0}.{1}";
			
			int i = 0;
			foreach (Field field in Fields)
			{
				i++;
				if (i > 1) list.Append(COMMA);
				list.AppendFormat(FORMAT_FIELD, Name, field.Name);
			}
			
			//list.Append(COMMA);
			//list.AppendFormat(FORMAT_FIELD, Name, DateModifiedFieldName);
			
			return list.ToString();
		}
		
		public virtual string GetFieldList(Map map)
		{
			string list = GetFieldList();
			if (!string.IsNullOrEmpty(list))
			{
				list += string.Format(", {0}", ParentLinkFieldName);
			}
			
			return list;
		}

		public virtual void AddDataRow(DataTable table, IDataRecord record, IDataConverter dataConverter, Map map)
		{
			if (table != null && record != null)
			{
				DataRow row = table.NewRow();
			
				foreach (Field field in Fields)
				{
					row[field.Name] = dataConverter.GetEntityValue(record[field.Name], field.Type);
				}
				
				if (record.GetOrdinal(DateModifiedFieldName) > -1)
				{
					row[DateModifiedFieldName] = dataConverter.GetEntityValue(record[DateModifiedFieldName], typeof(DateTime));
				}
				
				if (map != null && record.GetOrdinal(ParentLinkFieldName) > -1)
				{
					row[ParentLinkFieldName] = dataConverter.GetEntityValue(record[ParentLinkFieldName], map.Root.Identifier.Type);
				}
				
				table.Rows.Add(row);
			}
		}
		#endregion
		
		#region Public Overrides
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Entity)
			{
				Entity other = (Entity)obj;
				return this.ToString().Equals(other.ToString());
			}
			
			return false;
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}", Schema.Namespace, Name);
		}
		#endregion
	}
	
	public abstract class Entity<T> : Entity
	{
		#region Constructors
		protected Entity(string name) : base(name, typeof(T))
		{
		}
		#endregion
	
		#region Protected Methods
		protected void AddModelToList(T model, DataRow row, Association association, IDictionary<string, ICollection<T>> list)
		{
			if (model != null)
			{
				string key = null;
				
				if (association != null)
				{
					key = row[association.ParentFieldName].ToString();
				}
				else
				{
					key = row.Table.TableName;
				}

				if (!list.ContainsKey(key) || list[key] == null)
				{
					list[key] = new List<T>();
				}

				list[key].Add(model);
			}
		}
		#endregion
	
		#region Public Methods
		public virtual IDictionary<string, ICollection<T>> GetModels(DataTable table, Association association)
		{
			IDictionary<string, ICollection<T>> list = new Dictionary<string, ICollection<T>>();

			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow row in table.Rows)
				{
					T model = GetModel(row);

					AddModelToList(model, row, association, list);
				}
			}

			return list;
		}
		
		public abstract T GetModel(DataRow row);

		public abstract Tuple GetTuple(T model);
		
		public abstract void AddDataRow(DataTable table, T model);
		#endregion
	}
}
