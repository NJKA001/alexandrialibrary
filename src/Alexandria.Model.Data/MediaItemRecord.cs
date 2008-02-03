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

using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaItemRecord : RecordBase<IMediaItem>
	{
		public MediaItemRecord(ISchema schema) : base("MediaItem", schema)
		{
			Fields.Add("Id", typeof(Guid), ConstraintType.Identifier);
			Fields.Add("Status", typeof(string));
			Fields.Add("Source", typeof(string));
			Fields.Add("Type", typeof(string));
			Fields.Add("Number", typeof(int));
			Fields.Add("Title", typeof(string));
			Fields.Add("Artist", typeof(string));
			Fields.Add("Album", typeof(string));
			Fields.Add("Duration", typeof(TimeSpan));
			Fields.Add("Date", typeof(DateTime));
			Fields.Add("Format", typeof(string));
			Fields.Add("Path", typeof(Uri), ConstraintType.Unique);
			LinkFields[typeof(IMediaSet)] = new Field(this, "MediaSetId", typeof(Guid));
		}

		public override IMediaItem GetModel(Tuple tuple)
		{
			IMediaItem model = new AudioTrack();

			if (tuple != null)
			{
				model.Id = tuple.GetValue<Guid>(Fields["Id"]);
				model.Type = tuple.GetValue<string>(Fields["Type"]);
			}

			return model;
		}

		public override Tuple GetTuple(IMediaItem model)
		{
			Tuple tuple = new Tuple();

			if (model != null)
			{
				tuple.Data[Fields["Id"]] = model.Id;
				tuple.Data[Fields["Type"]] = model.Type;
			}

			return tuple;
		}
	}
}
