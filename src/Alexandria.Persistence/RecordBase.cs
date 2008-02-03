#region License (MIT)
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
using System.Collections.ObjectModel;

namespace Telesophy.Alexandria.Persistence
{
	public abstract class RecordBase : IRecord
	{
		#region Constructors
		public RecordBase(string name, ISchema schema, Type dataType)
		{
			this.name = name;
			this.schema = schema;
			this.dataType = dataType;
			this.fields = new FieldCollection(this);
			this.linkFields = new Dictionary<Type, Field>();
			this.constraints = new ConstraintCollection(this);
		}
		#endregion

		#region Private Fields
		private string name;
		private ISchema schema;
		private Type dataType;
		private FieldCollection fields;
		private IDictionary<Type, Field> linkFields;
		private ConstraintCollection constraints;
		#endregion

		#region IRecord Members
		public string Name
		{
			get { return name; }
		}

		public ISchema Schema
		{
			get { return schema; }
		}

		public Type DataType
		{
			get { return dataType; }
		}

		public FieldCollection Fields
		{
			get { return fields; }
		}

		public IDictionary<Type, Field> LinkFields
		{
			get { return linkFields; }
		}

		public ConstraintCollection Constraints
		{
			get { return constraints; }
		}

		public FieldCollection GetIdentifierFields()
		{
			FieldCollection identifierFields = new FieldCollection(this);

			foreach (Constraint constraint in Constraints)
			{
				if (constraint.Type == ConstraintType.Identifier)
				{
					foreach (Field field in constraint.Fields)
						identifierFields.Add(field);
				}
			}

			return identifierFields;
		}
		#endregion
		
		#region Overrides
		public override bool Equals(object obj)
		{
			if (obj != null && obj is RecordBase)
			{
				RecordBase other = (RecordBase)obj;
				return (this.ToString() == other.ToString());
			}

			return false;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}", schema.Name, name);
		}
		#endregion

		#region Static Members
		public static bool operator ==(RecordBase r1, RecordBase r2)
		{
			if (r1 != null && r2 != null)
			{
				return r1.Equals(r2);
			}

			return false;
		}

		public static bool operator !=(RecordBase r1, RecordBase r2)
		{
			if (r1 != null && r2 != null)
			{
				return !r1.Equals(r2);
			}

			return false;
		}
		#endregion
	}

	public abstract class RecordBase<Model> : RecordBase, IRecord<Model>
	{
		#region Constructors
		public RecordBase(string name, ISchema schema) : base(name, schema, typeof(Model))
		{
		}
		#endregion
		
		#region IRecord<Model> Members
		public abstract Model GetModel(Tuple tuple);
		
		public abstract Tuple GetTuple(Model model);
		#endregion
	}
}
