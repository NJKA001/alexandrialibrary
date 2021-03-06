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

namespace Telesophy.Alexandria.Messaging
{
	public class MessageCollection : KeyedCollection<Guid, IMessage>
	{
		#region Constructors
		public MessageCollection()
		{
		}
		#endregion

		#region Protected Methods
		protected override Guid GetKeyForItem(IMessage item)
		{
			if (item != null)
				return item.Id;
			else return Guid.Empty;
		}
		#endregion

		#region Public Properties
		public new IMessage this[Guid id]
		{
			get
			{
				if (base.Contains(id))
				{
					return base[id];
				}
				else throw new KeyNotFoundException();
			}
		}

		public new IMessage this[int index]
		{
			get
			{
				if (index >= 0 && index < base.Count)
				{
					return base[index];
				}
				else throw new IndexOutOfRangeException();
			}
		}
		#endregion		
	}
}
