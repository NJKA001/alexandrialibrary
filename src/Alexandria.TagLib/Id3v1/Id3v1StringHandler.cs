/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v1tag.cpp from TagLib
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;
using System.Collections;

namespace Alexandria.TagLib
{
	public class Id3v1StringHandler
	{		  
		#region Public Methods
		public virtual string Parse(ByteVector data)
		{
			if (data != null)
			{
				string output = data.ToString(StringType.Latin1).Trim();
				int i = output.IndexOf('\0');
				return (i >= 0) ? output.Substring(0, i) : output;
			}
			else throw new ArgumentNullException("data");
		}
		
		public virtual ByteVector Render(string value)
		{
			return ByteVector.FromString(value, StringType.Latin1);
		}
		#endregion
	}
}
