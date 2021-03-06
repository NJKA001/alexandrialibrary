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
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Persistence;

namespace Alexandria.Metadata
{
	[Record("Artist")]
	[RecordType("E6B5DC0E-7FE4-47da-AF9F-EB283129E224")]
	public abstract class BaseArtist : BaseMetadata, IArtist
	{
		#region Constructors
		public BaseArtist(string id, string path, string name, DateTime beginDate, DateTime endDate) : this(new Guid(id), new Uri(path), name, beginDate, endDate)
		{
		}

		[Factory("E6B5DC0E-7FE4-47da-AF9F-EB283129E224")]
		public BaseArtist(Guid id, Uri path, string name, DateTime beginDate, DateTime endDate) : base(id, path, name)
		{
			this.beginDate = beginDate;
			this.endDate = endDate;
		}
		#endregion
	
		#region Private Fields
		private DateTime beginDate;
		private DateTime endDate;
		#endregion
	
		#region IArtist Members
		public DateTime BeginDate
		{
			get { return beginDate; }
		}
		
		public DateTime EndDate
		{
			get { return endDate; }
		}
		#endregion
	}
}
