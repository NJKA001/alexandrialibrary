using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class SoundLoop	
	{
		#region Constructors
		public SoundLoop(uint start, TimeUnits startUnit, uint end, TimeUnits endUnit, int count)
		{
			this.start = start;
			this.startUnit = startUnit;
			this.end = end;
			this.endUnit = endUnit;
			this.count = count;
		}
		#endregion
	
		#region Private Fields
		private uint start;
		private TimeUnits startUnit;
		private uint end;
		private TimeUnits endUnit;
		private int count;
		#endregion
		
		#region Public Properties
				
		#region Start
		public uint Start
		{
			get {return start;}
			set {start = value;}
		}
		#endregion
		
		#region StartUnit
		public TimeUnits StartUnit
		{
			get {return startUnit;}
			set {startUnit = value;}
		}
		#endregion
		
		#region End
		public uint End
		{
			get {return end;}
			set {end = value;}
		}
		#endregion
		
		#region EndUnit
		public TimeUnits EndUnit
		{
			get	{return endUnit;}
			set {endUnit = value;}
		}
		#endregion
		
		#region Count
		public int Count
		{
			get {return count;}
			set {count = value;}
		}
		#endregion
		
		#endregion
	}
}
