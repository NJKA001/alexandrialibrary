#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
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
using System.Reflection;

namespace Alexandria.Persistence
{
	public class RecordMap
	{
		#region Constructors
		public RecordMap(Type type, RecordAttribute recordAttribute, RecordTypeAttribute recordTypeAttribute, IDictionary<int, FieldMap> basicFieldMaps, IList<FieldMap> advancedFieldMaps, IList<IndexMap> indexMaps, IList<LinkRecord> linkRecords)
		{
			this.type = type;
			this.recordAttribute = recordAttribute;
			this.recordTypeAttribute = recordTypeAttribute;
			this.basicFieldMaps = basicFieldMaps;
			this.advancedFieldMaps = advancedFieldMaps;
			this.indexMaps = indexMaps;
			this.linkRecords = linkRecords;
		}
		#endregion
		
		#region Private Fields
		private Type type;
		private RecordAttribute recordAttribute;
		private RecordTypeAttribute recordTypeAttribute;
		private FactoryMap factoryMap;
		private IDictionary<int, FieldMap> basicFieldMaps;
		private IList<FieldMap> advancedFieldMaps;
		private IList<IndexMap> indexMaps;
		private IList<LinkRecord> linkRecords;		
		#endregion
		
		#region Public Properties
		public Type Type
		{
			get { return type; }
		}
		
		public RecordAttribute RecordAttribute
		{
			get { return recordAttribute; }
		}
		
		public RecordTypeAttribute RecordTypeAttribute
		{
			get { return recordTypeAttribute; }
		}
		
		public FactoryMap FactoryMap
		{
			get { return factoryMap; }
			set { factoryMap = value; }
		}
		
		public IDictionary<int, FieldMap> BasicFieldMaps
		{
			get { return basicFieldMaps; }
		}
		
		public IList<FieldMap> AdvancedFieldMaps
		{
			get { return advancedFieldMaps; }
		}
		
		public IList<IndexMap> IndexMaps
		{
			get { return indexMaps; }
		}
		
		public IList<LinkRecord> LinkRecords
		{
			get { return linkRecords; }
		}
		
		public string Name
		{
			get
			{
				if (recordAttribute != null)
					return recordAttribute.Name;
				else return null;
			}
		}
		#endregion
		
		#region Public Methods
		public FieldMap GetFieldMapByName(string name)
		{
			foreach(FieldMap fieldMap in basicFieldMaps.Values)
			{
				if (!string.IsNullOrEmpty(fieldMap.Attribute.FieldName))
				{
					if (fieldMap.Attribute.FieldName == name)
						return fieldMap;
				}
				else
				{
					if (fieldMap.Property.Name == name)
						return fieldMap;
				}
			}
			return null;
		}
		
		public FieldMap GetFieldMapByProperty(PropertyInfo property)
		{
			foreach(FieldMap fieldMap in basicFieldMaps.Values)
			{
				if (property.Name == fieldMap.Property.Name)
					return fieldMap;
			}
			return null;
		}
		#endregion
	}
}
