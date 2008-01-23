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
	public class Schema
	{
		#region Constructors
		public Schema(string name)
		{
			this.name = name;
		}
		#endregion
		
		#region Private Fields
		private string name;
		private List<Record> records = new List<Record>();
		private Dictionary<string, Record> recordsByName = new Dictionary<string,Record>();
		#endregion
		
		#region Private Methods
		private void SynchronizeRecords()
		{
			foreach (Record record in records)
			{
				if (!recordsByName.ContainsKey(record.Name))
					recordsByName.Add(record.Name, record);
			}
		}
		#endregion
		
		#region Public Properties
		public string Name
		{
			get { return name; }
		}

		public ReadOnlyCollection<Record> Records
		{
			get { return records.AsReadOnly(); }
		}
		#endregion
		
		#region Public Methods
		public Record AddRecord(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				if (!recordsByName.ContainsKey(name))
				{
					Record record = new Record(name, this);
					records.Add(record);
					SynchronizeRecords();
					return record;
				}
			}
			
			return Record.Empty;
		}
		
		public Record GetRecord(string name)
		{
			if (recordsByName.ContainsKey(name))
				return recordsByName[name];
			else return Record.Empty;
		}
		#endregion
	}
}
