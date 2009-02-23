#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2009 Dan Poage
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

using Telesophy.Alexandria.Core;

namespace Telesophy.Alexandria.Model
{
    public class MediaSet : Entity, IMediaSet
    {
        #region Constructors

        public MediaSet()
        {
        }

        #endregion

        #region Private Members

        private IList<MediaItem> items = new List<MediaItem>();
        
		#endregion

        #region IMediaSet Members

        public virtual string Source
        {
            get; set;
        }

        public virtual string Type
        {
            get; set;
        }

        public virtual int Number
        {
            get; set;
        }

        public virtual string Title
        {
            get; set;
        }

        public virtual string Artist
        {
            get; set;
        }

        public virtual DateTime Date
        {
            get; set;
        }

        public virtual string Format
        {
            get; set;
        }

        public virtual Uri Path
        {
            get; set;
        }

        public virtual IList<MediaItem> Items
        {
            get { return items; }
			set { items = value; } // as IList<MediaItem>; }
        }

        #endregion
	}
}