using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class ChannelCollection : IEnumerable<Channel>
	{
		#region Constructors
		public ChannelCollection(IntPtr channelGroupHeadHandle, bool initialize)
		{
			this.channelGroupHeadHandle = channelGroupHeadHandle;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private IntPtr channelGroupHeadHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private List<Channel> channels = new List<Channel>();
		private int totalCount;
		#endregion
		
		#region Private Properties
		private System.Collections.ObjectModel.ReadOnlyCollection<Channel> Channels
		{
			get {return channels.AsReadOnly();}
		}
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public void Refresh()
		{
			currentResult = NativeMethods.FMOD_ChannelGroup_GetNumChannels(channelGroupHeadHandle, ref totalCount);
			
			Channel channel;
			IntPtr channelHandle;
			
			channels.Clear();
			channels.Capacity = totalCount + 1;
			
			for(int i = 0; i < totalCount; i++)
			{
				channel = null;
				channelHandle = IntPtr.Zero;
				
				try
				{
					currentResult = NativeMethods.FMOD_ChannelGroup_GetChannel(channelGroupHeadHandle, i, ref channelHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					// It's not clear from the documentation which exceptions are thrown
					// TODO: test to determine which exceptions _GetChannel throws
					currentResult = Result.InvalidParameterError;
				}
				
				if (currentResult == Result.Ok)
				{
					channel = new Channel();
					channel.Handle = channelHandle;
				}

				channels.Add(channel);
			}
		}
		#endregion
		
		#endregion

		#region IEnumerable<Channel> Members
		public IEnumerator<Channel> GetEnumerator()
		{
			foreach(Channel channel in Channels)			
				yield return channel;			
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (Channel channel in Channels)			
				yield return channel;			
		}
		#endregion
	}
}
