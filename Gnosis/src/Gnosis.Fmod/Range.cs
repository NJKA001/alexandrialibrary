using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	public sealed class Range
	{
		#region Constructors
		public Range(float minimum, float maximum)
		{
			this.minimum = minimum;
			this.maximum = maximum;
		}
		#endregion

		#region Private Fields
		private float minimum;
		private float maximum;
		#endregion
		
		#region Public Properties
				
		#region Minimum
		public float Minimum
		{
			get {return minimum;}
			set {minimum = value;}
		}
		#endregion
		
		#region Maximum
		public float Maximum
		{
			get {return maximum;}
			set {maximum = value;}
		}
		#endregion
				
		#endregion		
	}
}
