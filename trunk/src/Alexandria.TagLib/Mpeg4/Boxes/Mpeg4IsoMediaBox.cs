using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4IsoMediaBox : Mpeg4Box
	{
		#region Constructors
		public Mpeg4IsoMediaBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
		}
		#endregion
		
		#region Public Properties
		// This box has children, and no readable data.
		public override bool HasChildren
		{
			get { return true; }
		}

		public override ByteVector Data
		{
			get { return null; }
			set { }
		}
		#endregion
	}
}