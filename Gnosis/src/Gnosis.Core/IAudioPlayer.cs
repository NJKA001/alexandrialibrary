#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
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
#endregion

using System;
using System.Collections.Generic;

namespace Gnosis.Core
{
	public interface IAudioPlayer : IDisposable
	{
		IAudioStreamFactory AudioStreamFactory { get; set; }
		IAudioStream CurrentAudioStream { get; }
		TimeSpan Duration { get; }
		TimeSpan Elapsed { get; }
		bool IsMuted { get; }
		bool MuteToggles { get; set; }
		bool PlayToggles { get; set; }
		bool SeekIsPending { get; }
		float Volume { get; }
		EventHandler<MediaStateChangedEventArgs> BufferStateChanged { get; set; }
		EventHandler<EventArgs> CurrentAudioStreamChanged { get; set; }
		EventHandler<EventArgs> CurrentAudioStreamEnded { get; set; }
		EventHandler<MediaStateChangedEventArgs> NetworkStateChanged { get; set; }
		EventHandler<MediaStateChangedEventArgs> PlaybackStateChanged { get; set; }
		EventHandler<MediaStateChangedEventArgs> SeekStateChanged { get; set; }
		EventHandler<AudioStateChangedEventArgs> VolumeChanged { get; set; }
		void BeginSeek();
		void CancelSeek();
		void LoadAudioStream(Uri path);
		void LoadAudioStream(IAudioStream audioStream);
		void Mute();
		void Pause();
		void Play();
		void RefreshPlayerStates();
		void Resume();
		void Seek(int position);
		void SetVolume(float volume);
		void Stop();
		void Unmute();
	}
}