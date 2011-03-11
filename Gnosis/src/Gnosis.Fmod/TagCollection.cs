using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class TagCollection : IEnumerable<Tag>
	{
		#region Constructors
		public TagCollection(IntPtr soundHandle, bool initialize)
		{
			this.soundHandle = soundHandle;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private IntPtr soundHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private List<Tag> tags = new List<Tag>();
		private int totalCount;
		private int updatedCount;
		#endregion
		
		#region Private Properties
		private System.Collections.ObjectModel.ReadOnlyCollection<Tag> Tags
		{
			get {return tags.AsReadOnly();}
		}
		#endregion
				
		#region Public Properties
		
		#region SoundHandle
		public IntPtr SoundHandle
		{
			get {return soundHandle;}
		}
		#endregion
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region Count
		public int Count
		{
			get {return tags.Count;}
		}
		#endregion
		
		#region UpdatedCount
		public int UpdatedCount
		{
			get {return updatedCount;}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public void Refresh()
		{
			currentResult = NativeMethods.FMOD_Sound_GetNumTags(soundHandle, ref totalCount, ref updatedCount);
			
			tags.Clear();
			tags.Capacity = totalCount + 1;
			
			Tag tag = new Tag();
			
			for (int i = 0; i < totalCount; i++)
			{
				try
				{
					currentResult = NativeMethods.FMOD_Sound_GetTag(soundHandle, null, i, ref tag);
				}
				catch (Exception ex)
				{
					// Failed to read tag
					throw new ApplicationException("Failed to read metadata tag", ex);
				}

				tags.Add(tag);
			}
		}
		#endregion
		
		#endregion

		#region IEnumerable<Tag> Members\
		public IEnumerator<Tag> GetEnumerator()
		{
			foreach(Tag tag in Tags)
				yield return tag;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (Tag tag in Tags)
				yield return tag;
		}
		#endregion
	}
}
