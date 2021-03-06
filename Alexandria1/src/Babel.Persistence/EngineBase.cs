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
	public abstract class EngineBase
		<ConnectionType, TransactionType, CommandType, ParameterType>
		: IEngine
		where ConnectionType: IDbConnection
		where TransactionType : IDbTransaction
		where CommandType : IDbCommand
		where ParameterType : IDataParameter
	{
		#region Constructors
		protected EngineBase(string name)
		{
			this.name = name;
		}
		#endregion
		
		#region Private Constants
		private const string SORT_ORDER_ASCENDING = "ASC";
		private const string SORT_ORDER_DESCENDING = "DESC";
		#endregion
		
		#region Private Fields
		private string name;
		private IDataConverter dataConverter;
		#endregion
	
		#region Protected Properties
		protected abstract string DatabaseDirectory { get; }
		#endregion
	
		#region Protected Methods
		protected abstract string GetConnectionString(ISchema schema);
		
		protected virtual ConnectionType GetConnection(ISchema schema)
		{
			if (schema != null)
			{
				return GetConnection(GetConnectionString(schema));
			}
			
			return default(ConnectionType);
		}
		
		protected abstract ConnectionType GetConnection(string connectionString);
		
		protected abstract TransactionType GetTransaction(ConnectionType connection);
		
		protected abstract CommandType GetCommand(ConnectionType connection, TransactionType transaction, string commandText);

		protected abstract CommandType GetCommand(ConnectionType connection, TransactionType transaction, string commandText, IList<ParameterType> parameters);
		
		protected virtual CommandType GetSelectCommand(ConnectionType connection, TransactionType transaction, Entity entity, IQuery query)
		{
			StringBuilder sql = new StringBuilder("SELECT ");

			sql.Append(entity.GetFieldList());
			
			sql.AppendFormat(" FROM {0}", entity.Name);

			IList<ParameterType> parameters;
			sql.Append(GetWhereClause(query, out parameters));
			sql.Append(GetOrderByClause(entity, query));

			return GetCommand(connection, transaction, sql.ToString(), parameters);
		}

		protected virtual CommandType GetSelectCommand(ConnectionType connection, TransactionType transaction, Map map, IQuery query)
		{
			StringBuilder sql = new StringBuilder("SELECT ");

			StringBuilder orderBy = new StringBuilder(string.Empty);
			if (map.Branches.Count > 0)
				orderBy.Append(" ORDER BY ");

			sql.Append(map.Leaf.GetFieldList(map));
			
			sql.AppendFormat(" FROM {0}", map.Root.Name);
			
			const string JOIN_FORMAT = " {0} JOIN {1} ON {2}.{3} = {1}.{4}";
			
			int i=0;
			const string COMMA = ", ";
			foreach (Association branch in map.Branches)
			{
				i++;
				string joinType = (branch.IsRequired) ? "INNER" : "LEFT OUTER";
			
				// LEFT INNER JOIN MediaSetItems ON MediaSet.Id = MediaSetItems.ParentId
				// LEFT INNER JOIN MediaItem ON MediaSetItems.ChildId = MediaItem.Id
				
				sql.AppendFormat(JOIN_FORMAT, joinType, branch.Name, branch.Parent.Name, branch.Parent.Identifier.Name, branch.ParentFieldName);
				sql.AppendFormat(JOIN_FORMAT, joinType, branch.Child.Name, branch.Name, branch.ChildFieldName, branch.Child.Identifier.Name);
				
				if (i > 1) orderBy.Append(COMMA);
				orderBy.AppendFormat("{0}.{1} {2}", branch.Name, branch.SequenceFieldName, SORT_ORDER_ASCENDING);
			}
			
			IList<ParameterType> parameters;
			sql.Append(GetWhereClause(query, out parameters));
			sql.Append(orderBy.ToString());
			
			return GetCommand(connection, transaction, sql.ToString(), parameters);
		}
		
		protected virtual CommandType GetSaveCommand(ConnectionType connection, TransactionType transaction, Tuple tuple)
		{
			string saveFormat = "REPLACE INTO {0} ({1}) VALUES ({2})";
			
			StringBuilder fields = new StringBuilder();
			StringBuilder values = new StringBuilder();
			IList<ParameterType> parameters = new List<ParameterType>();
			
			
			const string COMMA = ", ";									
			int i = 0;
			
			foreach (KeyValuePair<string, object> pair in tuple)
			{
				i++;
				
				if (i > 1)
				{
					fields.Append(COMMA);
					values.Append(COMMA);
				}
				
				fields.Append(pair.Key);
				
				string parameterName = string.Format("@{0}", pair.Key);
				ParameterType parameter = GetParameter(parameterName, DataConverter.GetEngineValue(pair.Value));
				parameters.Add(parameter);
				
				values.Append(parameterName);
			}
			
			string commandText = string.Format(saveFormat, tuple.Name, fields, values);
			return GetCommand(connection, transaction, commandText, parameters);
		}
		
		protected virtual CommandType GetDeleteCommand(ConnectionType connection, TransactionType transaction, Tuple tuple)
		{
			string deleteFormat = "DELETE FROM {0} WHERE {1} = {2}";

			IList<ParameterType> parameters = new List<ParameterType>();

			string parameter1Name = string.Format("@{0}", tuple.IdentifierName);
			object parameter1Value = DataConverter.GetEngineValue(tuple.IdentifierValue);
			ParameterType parameter1 = GetParameter(parameter1Name, parameter1Value);
			parameters.Add(parameter1);

			string commandText = string.Format(deleteFormat, tuple.Name, tuple.IdentifierName, parameter1Name);
			return GetCommand(connection, transaction, commandText, parameters);
		}
		
		protected virtual CommandType GetDeleteCommand(ConnectionType connection, TransactionType transaction, Association association, object parentId, DateTime timeStamp)
		{
			string deleteFormat = "DELETE FROM {0} WHERE {1} = {2} AND {3} <> {4}";
			
			IList<ParameterType> parameters = new List<ParameterType>();
			
			string parameter1Name = string.Format("@{0}", association.ParentFieldName);
			object parameter1Value = DataConverter.GetEngineValue(parentId);
			ParameterType parameter1 = GetParameter(parameter1Name, parameter1Value);
			parameters.Add(parameter1);
			
			string parameter2Name = string.Format("@{0}", association.DateModifiedFieldName);
			object parameter2Value = DataConverter.GetEngineValue(timeStamp);
			ParameterType parameter2 = GetParameter(parameter2Name, parameter2Value);
			parameters.Add(parameter2);
			
			string commandText = string.Format(deleteFormat, association.Name, association.ParentFieldName, parameter1Name, association.DateModifiedFieldName, parameter2Name);
			return GetCommand(connection, transaction, commandText, parameters);
		}
		
		protected virtual CommandType GetDeleteCommand(ConnectionType connection, TransactionType transaction, string name, Query query)
		{
			return default(CommandType);
		}
		
		protected abstract ParameterType GetParameter(string name, object value);
		
		protected abstract void CreateEntityTables(Entity entity, ConnectionType connection, TransactionType transaction);
				
		protected virtual string GetWhereClause(IQuery query, out IList<ParameterType> parameters)
		{
			parameters = new List<ParameterType>();
		
			if (query != null && query.Filters.Count > 0)
			{
				StringBuilder clause = new StringBuilder(" WHERE");

				int i = 0;
				foreach (IExpression filter in query.Filters)
				{
					i++;
					string name = string.Format("@{0}", i);

					if (filter.LinkingOperator != null)
					{
						clause.AppendFormat(" {0}", filter.LinkingOperator);
					}

					clause.AppendFormat(" {0} {1} {2}", filter.LeftOperand, filter.ComparisonOperator, name);

					object value = filter.RightOperand;
					if (filter.ComparisonOperator.Name.Equals("LIKE", StringComparison.InvariantCultureIgnoreCase))
						value = string.Format("%{0}%", filter.RightOperand);
					
					parameters.Add(GetParameter(name, value));
				}
				
				return clause.ToString();
			}
			
			return string.Empty;
		}
		
		private string GetSortDirection(bool isAscending)
		{
			return (isAscending) ? SORT_ORDER_ASCENDING : SORT_ORDER_DESCENDING;
		}
		
		protected virtual string GetOrderByClause(Entity entity, IQuery query)
		{
			//if (query != null && query.
			
			if (entity != null && entity.DefaultSortOrder != null && entity.DefaultSortOrder.Count > 0)
			{
				StringBuilder clause = new StringBuilder(" ORDER BY");
				
				const string COMMA = ",";
				int i = 0;
				foreach (KeyValuePair<Field, bool> pair in entity.DefaultSortOrder)
				{
					i++;
					if (i > 1)
						clause.Append(COMMA);
					
					clause.AppendFormat(" {0}.{1} {2}", entity.Name, pair.Key.Name, GetSortDirection(pair.Value));
				}
				
				return clause.ToString();
			}
			
			return string.Empty;
		}
		#endregion
	
		#region IEngine Members
		public string Name
		{
			get { return name; }
		}

		public IDataConverter DataConverter
		{
			get { return dataConverter; }
			set { dataConverter = value; }
		}

		public virtual void Initialize(ISchema schema)
		{
			if (schema != null)
			{
				using (ConnectionType connection = GetConnection(schema))
				{
					connection.Open();
					TransactionType transaction = default(TransactionType);

					try
					{
						transaction = GetTransaction(connection);

						foreach (Entity entity in schema.Entities)
						{
							CreateEntityTables(entity, connection, transaction);
						}

						transaction.Commit();
					}
					catch (Exception ex)
					{
						if (transaction != null)
							transaction.Rollback();

						throw ex;
					}
				}
			}
		}

		public virtual ICollection<T> List<T>(Aggregate<T> aggregate, IQuery query)
		{
			ICollection<T> list = new List<T>();
			
			if (aggregate != null)
			{
				using (ConnectionType connection = GetConnection(aggregate.Schema))
				{
					connection.Open();
					TransactionType transaction = default(TransactionType);
				
					try
					{						
						DataSet dataSet = new DataSet(aggregate.Name);
						
						DataTable rootTable = aggregate.Root.GetDataTable(aggregate.Root.Name);
						
						dataSet.Tables.Add(rootTable);
						CommandType rootSelect = GetSelectCommand(connection, transaction, aggregate.Root, query);
						IDataReader rootReader = rootSelect.ExecuteReader();
						while (rootReader.Read())
						{
							aggregate.Root.AddDataRow(rootTable, rootReader, DataConverter, null);
						}
						
						foreach (Map map in aggregate.Maps)
						{
							DataTable table = null;
							if (!dataSet.Tables.Contains(map.Name))
							{
								table = map.Leaf.GetDataTable(map);
								dataSet.Tables.Add(table);
							}
							else table = dataSet.Tables[map.Name];
							
							CommandType entitySelect = GetSelectCommand(connection, transaction, map, query);
							IDataReader entityReader = entitySelect.ExecuteReader();
							while (entityReader.Read())
							{
								object id = entityReader[map.Root.Identifier.Name];
							
								if (id != null && id != DBNull.Value)
								{
									map.Leaf.AddDataRow(table, entityReader, DataConverter, map);
								}
							}							
						}
						
						list = aggregate.List(dataSet);
					}
					catch (Exception ex)
					{							
						throw ex;
					}
				}
			}
			
			return list;
		}

		public virtual void Save<T>(Aggregate<T> aggregate, IEnumerable<T> models)
		{
			if (aggregate != null)
			{
				using (ConnectionType connection = GetConnection(aggregate.Schema))
				{
					connection.Open();
					TransactionType transaction = default(TransactionType);
				
					try
					{
						transaction = GetTransaction(connection);
						DateTime timeStamp = DateTime.Now;
						
						IList<Tuple> tuples = aggregate.GetTuples(models, timeStamp);
						foreach (Tuple tuple in tuples)
						{
							CommandType saveCommand = GetSaveCommand(connection, transaction, tuple);
							saveCommand.ExecuteNonQuery();
						
							if (tuple.Association != null)
							{
								object parentId = tuple.GetAssociatedParentId();
								CommandType deleteCommand = GetDeleteCommand(connection, transaction, tuple.Association, parentId, timeStamp);
								deleteCommand.ExecuteNonQuery();
							}
						}
						
						transaction.Commit();
					}
					catch (Exception ex)
					{
						if (transaction != null)
							transaction.Rollback();
							
						throw ex;
					}
				}
			}
		}

		public virtual void Delete<T>(Aggregate<T> aggregate, IEnumerable<T> models)
		{
			if (aggregate != null)
			{
				using (ConnectionType connection = GetConnection(aggregate.Schema))
				{
					connection.Open();
					TransactionType transaction = default(TransactionType);

					try
					{
						transaction = GetTransaction(connection);
						DateTime timeStamp = DateTime.MinValue;

						IList<Tuple> tuples = aggregate.GetTuples(models, timeStamp);
						foreach (Tuple tuple in tuples)
						{
							CommandType deleteCommand = GetDeleteCommand(connection, transaction, tuple);
							deleteCommand.ExecuteNonQuery();

							if (tuple.Association != null)
							{
								object parentId = tuple.GetAssociatedParentId();
								CommandType subDeleteCommand = GetDeleteCommand(connection, transaction, tuple.Association, parentId, timeStamp);
								subDeleteCommand.ExecuteNonQuery();
							}
						}

						transaction.Commit();
					}
					catch (Exception ex)
					{
						if (transaction != null)
							transaction.Rollback();

						throw ex;
					}
				}
			}
		}
		#endregion
	}
}
