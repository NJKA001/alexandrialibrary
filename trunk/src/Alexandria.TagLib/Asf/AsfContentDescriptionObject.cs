/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
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

namespace Alexandria.TagLib
{
   public class AsfContentDescriptionObject : AsfObject
   {
      //////////////////////////////////////////////////////////////////////////
      // private properties
      //////////////////////////////////////////////////////////////////////////
      private string title;
      private string author;
      private string copyright;
      private string description;
      private string rating;
      
      
      //////////////////////////////////////////////////////////////////////////
      // public methods
      //////////////////////////////////////////////////////////////////////////
		public AsfContentDescriptionObject (AsfFile file, long position) : base (file, position)
		{
			if (file != null)
			{
				if (!Guid.Equals (AsfGuid.AsfContentDescriptionObject))
				throw new TagLibException(TagLibError.AsfObjectGuidIncorrect);

				if (OriginalSize < 34)
				throw new TagLibException(TagLibError.AsfObjectSizeTooSmall);

				short title_length = file.ReadWord();
				short author_length = file.ReadWord();
				short copyright_length = file.ReadWord();
				short description_length = file.ReadWord();
				short rating_length = file.ReadWord();

				title = file.ReadUnicode(title_length);
				author = file.ReadUnicode(author_length);
				copyright = file.ReadUnicode(copyright_length);
				description = file.ReadUnicode(description_length);
				rating = file.ReadUnicode(rating_length);
			}
		}
      
		public AsfContentDescriptionObject () : base (AsfGuid.AsfContentDescriptionObject)
		{
			title       = string.Empty;
			author      = string.Empty;
			copyright   = string.Empty;
			description = string.Empty;
			rating      = string.Empty;
		}
      
      public override ByteVector Render ()
      {
         ByteVector title_bytes       = RenderUnicode (title);
         ByteVector author_bytes      = RenderUnicode (author);
         ByteVector copyright_bytes   = RenderUnicode (copyright);
         ByteVector description_bytes = RenderUnicode (description);
         ByteVector rating_bytes      = RenderUnicode (rating);
         
         ByteVector output = new ByteVector ();
         output += RenderWord ((short) title_bytes.Count);
         output += RenderWord ((short) author_bytes.Count);
         output += RenderWord ((short) copyright_bytes.Count);
         output += RenderWord ((short) description_bytes.Count);
         output += RenderWord ((short) rating_bytes.Count);
         output += title_bytes;
         output += author_bytes;
         output += copyright_bytes;
         output += description_bytes;
         output += rating_bytes;
         
         return Render (output);
      }
      
      //////////////////////////////////////////////////////////////////////////
      // public properties
      //////////////////////////////////////////////////////////////////////////
      public string Title       {get {return title;}       set {title = value;}}
      public string Author      {get {return author;}      set {author = value;}}
      public string Copyright   {get {return copyright;}   set {copyright = value;}}
      public string Description {get {return description;} set {description = value;}}
      public string Rating      {get {return rating;}      set {rating = value;}}
   }
}
