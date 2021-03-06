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
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.Catalog
{
	[Record("Catalog")]
    public interface ICatalog : IRecord
    {
		[Field(2, FieldConstraints.Required)]
		string Name { get; }
    
		[Field(FieldType.Child, FieldRelationship.ManyToMany, "UserCatalog", "UserId", "CatalogId", FieldCascades.Save)]
		IUser User { get; set; }
		
		[Field(FieldType.Parent, FieldRelationship.ManyToMany, "CatalogAlbum", "CatalogId", "AlbumId", FieldCascades.Save, ChildType=typeof(IOldAlbum))]
        IList<IOldAlbum> Albums { get; }
        
        [Field(FieldType.Parent, FieldRelationship.ManyToMany, "CatalogArtist", "CatalogId", "ArtistId", FieldCascades.Save, ChildType=typeof(IArtist))]
        IList<IArtist> Artists { get; }
        
        [Field(FieldType.Parent, FieldRelationship.ManyToMany, "CatalogTrack", "CatalogId", "TrackId", FieldCascades.Save, FieldLoadOptions.Proxy, ChildType=typeof(IAudioTrack))]
        IList<IAudioTrack> Tracks { get; }
    }
}
