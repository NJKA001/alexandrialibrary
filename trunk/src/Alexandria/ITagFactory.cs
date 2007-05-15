using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITagFactory : IPlugin
	{
		IList<ITagPluginCapability> Capabilities { get; }
		ITagPluginCapability GetCapability<T>() where T : ITag;
		ITagPluginCapability GetCapability<T>(ITagFormat format) where T: ITag;
		ITag CreateTag(ILocation location);
		ITag CreateTag(ILocation location, ITagFormat format);
		T CreateTag<T>(ILocation location) where T : ITag;
		T CreateTag<T>(ILocation location, ITagFormat format) where T: ITag;
	}
}
