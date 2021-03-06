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
using System.Text;
using Alexandria;
using Alexandria.Media;

namespace Alexandria.Media
{
	public class BasePlaylist : IPlaylist
	{
		#region Constructors
		protected BasePlaylist(Uri path, IMediaFormat format)
		{
			this.path = path;
			this.format = format;
		}
		
		protected BasePlaylist(Uri path, IMediaFormat format, string name, Version version) : this(path, format)
		{
			this.name = name;
			this.version = version;
		}
		#endregion

		#region Private Fields
		private Uri path;
		private IMediaFormat format;
		private string name;
		private Version version;
		private IList<IPlaylistItem> items = new List<IPlaylistItem>();
		private Guid id = Guid.NewGuid();
		#endregion				
		
		#region Public Properties
		public string Name
		{
			get {return name;}
			protected set {name = value;}
		}
		
		public Version Version
		{
			get {return version;}
			protected set {version = value;}
		}
		#endregion
		
		#region IMedia Members
		public Guid Id
		{
			get { return id; }
		}

		public Uri Path
		{
			get { return path; }
		}

		public IMediaFormat Format
		{
			get { return format; }
		}
		#endregion

		#region IPlaylist Members
		public IList<IPlaylistItem> Items
		{
			get { return items; }
		}
		
		public virtual void Load()
		{
		}

		public virtual void Save()
		{
		}
		#endregion
	}
}
