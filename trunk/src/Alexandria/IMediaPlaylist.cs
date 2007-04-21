using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaPlaylist : IMedia
	{
		IList<ILocation> Items { get; }
	}
}
