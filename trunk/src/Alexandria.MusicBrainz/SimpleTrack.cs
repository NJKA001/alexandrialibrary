
/***************************************************************************
 *  SimpleTrack.cs
 *
 *  Copyright (C) 2005 Novell
 *  Written by Aaron Bockover (aaron@aaronbock.net)
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Diagnostics;

namespace AlexandriaOrg.Alexandria.MusicBrainz
{
    public class SimpleTrack
    {
		#region Private Fields
        private string artist;
        private string title;
        private string album;
        private string asin;
        private int track_num;
        private int track_count;
        private int index;
        private int length;
        #endregion
        
        #region Constructors
		internal SimpleTrack()
		{
		}
		
        public SimpleTrack(int index, int length)
        {
            this.index = index;
            this.length = length;
        }
        #endregion
                
        #region Public Properties
        public string Artist
        {
			get {return artist;}
			internal set {artist = value;}
        }
                
        public string Album
        {
			get {return album;}
			internal set {album = value;}
        }
                        
        public string Asin
        {
			get {return asin;}
            internal set {asin = value;}
        }
        
        public string Title
        {
			get {return title;}
            internal set {title = value;}
        }
        
        public int Index
        {
			get {return index;}
            internal set {index = value;}
        }
                
        public int TrackNumber
        {
            get {return track_num;}
            internal set {track_num = value;}
        }
                
        public int TrackCount
        {
            get {return track_count;}
            internal set {track_count = value;}
        }
        
        public int Length
        {
            get {return length;}
            internal set {length = value;}
        }
        
        public int Minutes
        {
            get {return length / 60;}
        }
        
        public int Seconds
        {
            get {return length % 60;}
        }
        #endregion

		#region Public Methods
		public override string ToString()
		{
			return String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}: {1} - {2} ({3:00}:{4:00})",
				Index, Artist, Title, Minutes, Seconds);
		}
		#endregion
    }
}