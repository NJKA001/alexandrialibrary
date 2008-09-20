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

namespace Alexandria.Plugins
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
	public class PluginSettingAttribute : Attribute
	{
		#region Constructors
		public PluginSettingAttribute(string description)
		{
			this.description = description;
		}

		public PluginSettingAttribute(string description, PluginSettingType type) : this(description)
		{
			this.type = type;
		}

		public PluginSettingAttribute(string description, string textMask) : this(description)
		{
			this.textMask = textMask;
		}
		
		public PluginSettingAttribute(string description, bool isReadOnly) : this(description)
		{
			this.isReadOnly = isReadOnly;
		}
		#endregion

		#region Private Fields
		private string description;
		private PluginSettingType type;
		private string textMask;
		private bool isReadOnly;
		#endregion

		#region Public Properties
		public string Description
		{
			get { return description; }
		}
		
		public string TextMask
		{
			get { return textMask; }
			set { textMask = value; }
		}
		
		public bool IsReadOnly
		{
			get { return isReadOnly; }
			set { isReadOnly = value; }
		}
		#endregion
	}
}