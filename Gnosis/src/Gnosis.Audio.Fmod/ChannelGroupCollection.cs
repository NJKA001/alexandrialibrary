using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class ChannelGroupCollection : IEnumerable<ChannelGroup>
	{
		#region Contrustors
		public ChannelGroupCollection(IntPtr channelGroupHeadHandle, bool initialize)
		{
			this.channelGroupHeadHandle = channelGroupHeadHandle;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private IntPtr channelGroupHeadHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private List<ChannelGroup> channelGroups = new List<ChannelGroup>();
		private int totalCount;
		#endregion
		
		#region Private Properties

		#region Items
		/// <summary>
		/// A read-only collection of ChannelGroups
		/// </summary>
		/// <remarks>This list cannot be added to directly, use AddChannelGroup() instead</remarks>
		private System.Collections.ObjectModel.ReadOnlyCollection<ChannelGroup> ChannelGroups
		{
			get {return channelGroups.AsReadOnly();}
		}
		#endregion
		
		#endregion
		
		#region Public Properties
		
		#region ChannelGroupHeadHandle
		public IntPtr ChannelGroupHeadHandle
		{
			get {return channelGroupHeadHandle;}
		}
		#endregion
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
				
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public void Refresh()
		{
			currentResult = NativeMethods.FMOD_ChannelGroup_GetNumGroups(channelGroupHeadHandle, ref totalCount);
			
			ChannelGroup channelGroup = null;
			IntPtr channelGroupHandle = IntPtr.Zero;

			channelGroups.Clear();
			channelGroups.Capacity = totalCount+1;
			
			for(int i = 0; i < totalCount; i++)
			{
				channelGroup = null;
				channelGroupHandle = IntPtr.Zero;
				
				try
				{
					currentResult = NativeMethods.FMOD_ChannelGroup_GetGroup(channelGroupHeadHandle, i, ref channelGroupHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					// It's not clear from the documentation which exceptions are thrown
					// TODO: test to determine which exceptions _GetGroup throws
					currentResult = Result.InvalidParameterError;
				}
				
				if (currentResult == Result.Ok)
				{
					channelGroup = new ChannelGroup();
					channelGroup.Handle = channelGroupHandle;					
				}
				
				channelGroups.Add(channelGroup);
			}
		}
		#endregion
		
		#region Add
		/// <summary>
		/// Adds a channel group as a child of the current head channel group
		/// </summary>
		/// <param name="channelGroup">The channel group to add</param>
		public void Add(ChannelGroup channelGroup)
		{
			if (channelGroup != null)
			{
				currentResult = NativeMethods.FMOD_ChannelGroup_AddGroup(channelGroupHeadHandle, channelGroup.Handle);
				Refresh();
			}
		}
		#endregion
		
		#endregion				
	
		#region IEnumerable<ChannelGroup> Members
		public IEnumerator<ChannelGroup> GetEnumerator()
		{
			foreach(ChannelGroup channelGroup in ChannelGroups)
				yield return channelGroup;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (ChannelGroup channelGroup in ChannelGroups)
				yield return channelGroup;

		}
		#endregion
	}
}
