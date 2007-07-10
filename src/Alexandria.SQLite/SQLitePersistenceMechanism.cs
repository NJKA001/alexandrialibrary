#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;
using Alexandria;
using Alexandria.Persistence;

namespace Alexandria.SQLite
{
	public class SQLitePersistenceMechanism : IPersistenceMechanism
	{
		#region Constructors
		public SQLitePersistenceMechanism(string databasePath)
		{
			this.databasePath = databasePath;
		}
		#endregion

		#region Private Constant Fields
		private const string NULL_STRING = "NULL";
		private const string RECORD_TYPE_ID = "_RecordTypeId";
		#endregion

		#region Private Fields
		string databasePath;
		IPersistenceBroker broker;
		#endregion

		#region Private Methods

		#region GetConnectionString
		private string GetConnectionString()
		{
			bool databaseIsNew = false;
			databaseIsNew = (!File.Exists(databasePath));
			return string.Format("Data Source={0};New={1};UTF8Encoding=True;Version=3", databasePath, databaseIsNew);
		}
		#endregion

		#region NormalizeFromDatabaseValue
		private object NormalizeFromDatabaseValue(Type type, object value)
		{
			if (type == typeof(int))
			{
				if (value == DBNull.Value)
					return 0;
			}
			if (type == typeof(DateTime))
			{
				if (value == DBNull.Value)
					return DateTime.MinValue;
				else return Convert.ToDateTime(value.ToString());
			}
			else if (type == typeof(TimeSpan))
			{
				if (value == null || value == DBNull.Value)
					return TimeSpan.Zero;
				else return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(value));
			}
			else if (type == typeof(Version))
			{
				return new Version(value.ToString());
			}
			else if (type == typeof(Uri))
			{
				return new Uri(value.ToString());
			}

			return value;
		}
		#endregion

		#region GetSQLiteFieldValue
		private string GetSQLiteFieldValue(IRecord record, FieldMap fieldMap, object value)
		{
			if (fieldMap.Attribute.Type == FieldType.Basic)
			{
				Type type = fieldMap.Property.PropertyType;
				
			
				if (value == DBNull.Value || value == null)
					return NULL_STRING;

				if (type == typeof(short) || type == typeof(int) || type == typeof(long))
				{
					return value.ToString();
				}
				else if (type == typeof(decimal) || type == typeof(float) || type == typeof(double))
				{
					return value.ToString();
				}
				else if (type == typeof(Guid))
				{
					return string.Format("'{0}'", value.ToString().ToLowerInvariant());
				}
				else if (type == typeof(DateTime))
				{
					DateTime date = (DateTime)value;
					if (date == DateTime.MinValue)
						return NULL_STRING;
					else return date.ToFileTime().ToString();
				}
				else if (type == typeof(TimeSpan))
				{
					TimeSpan span = (TimeSpan)value;
					return span.TotalMilliseconds.ToString();
				}
				else if (type == typeof(Version))
				{
					return string.Format("'{0}'", value);
				}
				else if (type == typeof(Uri))
				{
					return string.Format("'{0}'", value);
				}				
			}
			else
			{
				if (fieldMap.Attribute.Type == FieldType.Parent)
				{
					return string.Format("'{0}'", record.Id);
				}
				else if (fieldMap.Attribute.Type == FieldType.Child)
				{
					if (record.Parent != null)
						return string.Format("'{0}'", record.Parent.Id);
					else return NULL_STRING;
				}				
			}
			
			return string.Format("'{0}'", value);
		}
		#endregion

		#region GetSQLiteFieldName
		private string GetSQLiteFieldName(FieldMap fieldMap)
		{
			string dbFieldName = string.Empty;
			
			if (!string.IsNullOrEmpty(fieldMap.Attribute.FieldName))
			{
				dbFieldName = fieldMap.Attribute.FieldName;
			}
			else
			{
				dbFieldName = fieldMap.Property.Name;	
			}
			
			return dbFieldName;
		}
		#endregion

		#region GetSQLiteFieldType
		private string GetSQLiteFieldType(Type type)
		{
			//integer, real, text
			string dbType = "TEXT";
			
			if (type == typeof(bool) ||
				type == typeof(sbyte) ||
				type == typeof(byte) ||
				type == typeof(short) ||
				type == typeof(ushort) ||
				type == typeof(int) ||
				type == typeof(uint) ||
				type == typeof(long) ||
				type == typeof(ulong) ||
				type == typeof(DateTime) ||
				type == typeof(TimeSpan))
			dbType = "INTEGER";
			
			if (type == typeof(float) ||
				type == typeof(double))
			dbType = "REAL";
			
			return dbType;
		}
		#endregion

		#region GetSQLiteFieldConstraints
		private string GetSQLiteFieldConstraints(FieldConstraints constraints)
		{
			StringBuilder dbConstraints = new StringBuilder(string.Empty);
			if ((constraints & FieldConstraints.Required) == FieldConstraints.Required) dbConstraints.Append(" NOT NULL");
			if ((constraints & FieldConstraints.Unique) == FieldConstraints.Unique) dbConstraints.Append(" UNIQUE");
			
			return dbConstraints.ToString();
		}
		#endregion

		#endregion

		#region Internal Methods

		#region GetSQLiteConnection
		internal SQLiteConnection GetSQLiteConnection()
		{
			return GetSQLiteConnection(GetConnectionString());
		}

		internal SQLiteConnection GetSQLiteConnection(string connectionString)
		{
			return new SQLiteConnection(connectionString);
		}
		#endregion

		#endregion

		#region IPersistenceMechanism Members
		public string Name
		{
			get { return "SQLite Embedded Database"; }
		}

		public bool IsOpen
		{
			get { return (broker != null); }
		}

		public IPersistenceBroker Broker
		{
			get { return broker; }
			set { broker = value; }
		}

		public void Open()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Close()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public DbConnection GetConnection()
		{
			return GetSQLiteConnection();
		}

		public void InitializeRecordMap(RecordMap recordMap, DbTransaction transaction)
		{
			
			string createFormat = "CREATE TABLE IF NOT EXISTS {0} ({1})";
			StringBuilder columns = new StringBuilder();
			for(int i=1;i<=recordMap.BasicFieldMaps.Count;i++)
			{
				if (i > 1) columns.Append(", ");
				
				string fieldName = GetSQLiteFieldName(recordMap.BasicFieldMaps[i]);
				string fieldType = GetSQLiteFieldType(recordMap.BasicFieldMaps[i].Property.PropertyType);
				string fieldConstraints = GetSQLiteFieldConstraints(recordMap.BasicFieldMaps[i].Attribute.Constraints);
				
				columns.AppendFormat("{0} {1}{2}", fieldName, fieldType, fieldConstraints);
			}
		
			// Add the RecordTypeId
			columns.AppendFormat(", {0} TEXT NOT NULL", RECORD_TYPE_ID);
			
			string commandText = string.Format(createFormat, recordMap.RecordAttribute.Name, columns);
			SQLiteCommand createTable = new SQLiteCommand(commandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
			createTable.ExecuteNonQuery();
			
			foreach(LinkRecord linkRecord in recordMap.LinkRecords)
			{
				if (linkRecord.FieldMap.Attribute.Relationship == FieldRelationship.ManyToMany)
				{
					if (linkRecord.FieldMap.Attribute.Type == FieldType.Parent)
					{
						string linkCommandText = string.Format("CREATE TABLE IF NOT EXISTS {0} ({1} NOT NULL, {2} NOT NULL, UNIQUE ({1}, {2}))", linkRecord.Name, linkRecord.ParentFieldName, linkRecord.ChildFieldName);
						SQLiteCommand createLinkTable = new SQLiteCommand(linkCommandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
						createLinkTable.ExecuteNonQuery();
					}
				}
			}
		}

		//public DataTable GetRecordData(string recodName, string fieldName, string value)
		//{
			//using (SQLiteConnection connection = GetSQLiteConnection())
			//{
				//return null;
			//}
			//throw new Exception("The method or operation is not implemented.");
		//}

		public void SaveRecord(IRecord record, DbTransaction transaction)
		{
			if (record == null)
				throw new ArgumentNullException("record");
			
			RecordTypeAttribute recordTypeAttribute = broker.GetRecordTypeAttribute(record.GetType());
			if (recordTypeAttribute == null)
				throw new ApplicationException("SQLite could not lookup record type");
				
			RecordMap recordMap = broker.RecordMaps[recordTypeAttribute.Id];
			if (recordMap == null)
				throw new ApplicationException("SQLite could not lookup record map");
		
			string saveFormat = "REPLACE INTO {0} ({1}) VALUES ({2})";
				
			StringBuilder columns = new StringBuilder();
			StringBuilder values = new StringBuilder();
			
			for(int i=1; i<=recordMap.BasicFieldMaps.Count; i++)
			{
				if (i > 1)
				{
					columns.Append(", ");
					values.Append(", ");
				}

				columns.Append(GetSQLiteFieldName(recordMap.BasicFieldMaps[i]));

				object propertyValue = recordMap.BasicFieldMaps[i].Property.GetValue(record, null);
				string fieldValue = GetSQLiteFieldValue(record, recordMap.BasicFieldMaps[i], propertyValue);
				values.Append(fieldValue);
			}
			
			//Add the RecordTypeId
			columns.AppendFormat(", {0}", RECORD_TYPE_ID);
			values.AppendFormat(", '{0}'", recordMap.RecordTypeAttribute.Id);
			
			string commandText = string.Format(saveFormat, recordMap.RecordAttribute.Name, columns, values);
			
			SQLiteCommand saveCommand = new SQLiteCommand(commandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
			saveCommand.ExecuteNonQuery();
			
			foreach(FieldMap fieldMap in recordMap.AdvancedFieldMaps)
			{		
				//TODO: find a way to cast to a generic IList<IRecord>			
				IList list = fieldMap.Property.GetValue(record, null) as IList;
				if (list != null)
				{						
					foreach(IRecord childRecord in list)
					{						
						SaveRecord(childRecord, transaction);
					}
				}
				else
				{
					IRecord childRecord = (IRecord)fieldMap.Property.GetValue(record, null);
					SaveRecord(childRecord, transaction);
				}
			}
		}

		public void FillDataTable(RecordMap recordMap, DataTable table, string idValue)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public object GetDatabaseValue(Type type, object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public object GetRecordValue(Type type, object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
