using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{	
	internal class SoundCollection : IEnumerable<Sound>, IList<Sound>
	{
		#region Contructors
		internal SoundCollection(Sound parentSound, bool initialize)
		{
			this.parentSound = parentSound;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private Sound parentSound;
		private Result currentResult = Result.Ok;
		private SortedDictionary<uint, Sound> subSounds = new SortedDictionary<uint, Sound>();
		private int totalCount;
		#endregion
				
		#region Internal Properties
		
		#region ParentSound
		internal Sound ParentSound
		{
			get {return parentSound;}
		}
		#endregion
		
		#region CurrentResult
		internal Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region Count
		internal int Count
		{
			get {return subSounds.Count;}
		}
		#endregion
		
		#endregion
		
		#region Internal Methods
		
		#region Refresh
		internal void Refresh()
		{
			currentResult = NativeMethods.FMOD_Sound_GetNumSubSounds(parentSound.Handle, ref totalCount);
			
			//Sound subSound;
			//IntPtr subSoundHandle;
			
			subSounds.Clear();
			//subSounds.Capacity = totalCount + 1;
			
			for (int i = 0; i < totalCount; i++)
			{				
				IntPtr subSoundHandle = new IntPtr();
				currentResult = NativeMethods.FMOD_Sound_GetSubSound(parentSound.Handle, i, ref subSoundHandle);
				Sound subSound = new Sound(parentSound);
				//subSound.Handle = new IntPtr();
				subSound.Handle = subSoundHandle;
				subSounds.Add((uint)i + 1, subSound);
			}
		}
		#endregion
		
		#region SetSubSound
		internal void SetSubSound(uint index, Sound subSound)
		{
			if (index > 0 && index <= subSounds.Count)
			{
				IntPtr subSoundHandle = subSound.Handle;
				currentResult = NativeMethods.FMOD_Sound_SetSubSound(parentSound.Handle, (int)index - 1, subSoundHandle);
				subSounds[index] = subSound;
			}
			//TODO: handle invalid index on else			
		}
		#endregion
		
		#region Stitch
		/// <summary>
		/// For any sound that has subsounds, this function will determine the order of playback of these subsounds, and it will play / stitch together the subsounds without gaps.
		/// </summary>
		/// <remarks>This is a very useful feature for those users wanting to do seamless / gapless stream playback. (ie sports commentary, gapless playback media players etc).</remarks>
		internal void Stitch()
		{
			int count = subSounds.Count;
			
			if (count > 0)
			{
				int[] subSoundIntList = new int[count];
				
				for(uint i = 0; i < count-1; i++)
				{
					IntPtr subSoundHandle = subSounds[i].Handle;
					subSoundIntList[i] = subSoundHandle.ToInt32();					
				}

				currentResult = NativeMethods.FMOD_Sound_SetSubSoundSentence(parentSound.Handle, subSoundIntList, count);
			}						
		}
		#endregion
		
		#region Remove
		internal bool Remove(uint key)
		{
			return subSounds.Remove(key);
		}
		#endregion
		
		#endregion

		#region IEnumerable<Sound> Members
		public IEnumerator<Sound> GetEnumerator()
		{
			foreach(Sound sound in subSounds.Values)
				yield return sound;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (Sound sound in subSounds.Values)
				yield return sound;
		}
		#endregion

		#region IList<Sound> Members

		public int IndexOf(Sound item)
		{
			if (subSounds != null && subSounds.Count > 0)
			{
				for(uint i=0;i<subSounds.Count;i++)
					if (subSounds[i] == item) return (int)i;
			}
			return -1;
		}

		public void Insert(int index, Sound item)
		{
			subSounds[(uint)index] = item;
		}

		public void RemoveAt(int index)
		{
			subSounds.Remove((uint)index);
		}

		public Sound this[int index]
		{
			get { return subSounds[(uint)index]; }
			set	{ subSounds[(uint)index] = value; }
		}

		#endregion

		#region ICollection<Sound> Members

		public void Add(Sound item)
		{
			subSounds[(uint)subSounds.Count+1] = item;
		}

		public void Clear()
		{
			subSounds.Clear();
		}

		public bool Contains(Sound item)
		{		
			return (IndexOf(item) > -1);
		}

		public void CopyTo(Sound[] array, int arrayIndex)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		int ICollection<Sound>.Count
		{
			get { return subSounds.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(Sound item)
		{
			for(uint i=0; i<subSounds.Count;i++)
			{
				if (subSounds[i] == item)
				{
					subSounds.Remove(i);
					return true;
				}
			}
			return false;
		}

		#endregion
	}
}