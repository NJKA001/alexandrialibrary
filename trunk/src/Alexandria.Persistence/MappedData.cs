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

namespace Telesophy.Alexandria.Persistence
{
	public struct MappedData
	{
		#region Constructors
		public MappedData(IDictionary<string, Field> fields, IList<IList<object>> values)
		{
			this.fields = fields;
			this.values = values;
		}
		#endregion
		
		#region Private Fields
		private IDictionary<string, Field> fields;
		private IList<IList<object>> values;
		#endregion
		
		#region Public Properties
		public IDictionary<string, Field> Fields
		{
			get { return fields; }
		}
		
		public IList<IList<object>> Values
		{
			get { return values; }
		}
		#endregion
		
		#region Public Methods
		public void AddField(Field field)
		{
			if (!fields.ContainsKey(field.Name))
			{
				fields.Add(field.Name, field);
			}
		}
		
		public void AddValueRow(IList<object> valueRow)
		{
			if (valueRow != null)
			{
				values.Add(valueRow);
			}
		}
		#endregion
	}
}
