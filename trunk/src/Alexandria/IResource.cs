using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IResource
	{
		Guid Guid { get; }
		Uri Uri { get; }		
		IResourceFormat Format { get; }
	}
}
