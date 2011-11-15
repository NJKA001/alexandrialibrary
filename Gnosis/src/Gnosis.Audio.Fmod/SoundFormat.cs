using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class SoundFormat : IMediaFormat
	{
		#region Constructors
		public SoundFormat(FmodSoundFormat fmodSoundFormat, FmodSoundType fmodSoundType)
		{
			this.fmodSoundFormat = fmodSoundFormat;
			this.fmodSoundType = fmodSoundType;
			
			switch(fmodSoundType)
			{
				case FmodSoundType.Aac:
					name = "AAC";
					description = "Advanced Audio Codec";
					contentTypes.Add(new ContentType("audio/aac"));
					contentTypes.Add(new ContentType("audio/m4a"));
					fileExtensions.Add("aac");
					fileExtensions.Add("m4a");
					break;
				case FmodSoundType.Aiff:
					break;
				case FmodSoundType.Asf:
					break;
				case FmodSoundType.At3:
					break;
				case FmodSoundType.Cdda:
					name = "CDDA";
					description = "Compact Disc Digital Audio";
					break;
				case FmodSoundType.Dls:
					break;
				case FmodSoundType.Flac:
					name = "FLAC";
					description = "Free Lossless Audio Codec";
					contentTypes.Add(new ContentType("audio/flac"));
					fileExtensions.Add("flac");
					break;
				case FmodSoundType.Fsb:
					break;
				case FmodSoundType.GameCubeAdpcm:
					break;
				case FmodSoundType.IT:
					break;
				case FmodSoundType.Midi:
					break;
				case FmodSoundType.Mod:
					break;
				case FmodSoundType.Mpeg:
					name = "MP3";
					description = "MPEG III Layer I";
					contentTypes.Add(new ContentType("audio/mp3"));
					fileExtensions.Add("mp3");
					break;
				case FmodSoundType.OggVorbis:
					name = "Vorbis";
					description = "Ogg Vorbis";
					contentTypes.Add(new ContentType("audio/vorbis"));
					fileExtensions.Add("ogg");
					break;
				case FmodSoundType.Playlist:
					break;
				case FmodSoundType.Raw:
					break;
				case FmodSoundType.S3m:
					break;
				case FmodSoundType.Sf2:
					break;
				case FmodSoundType.Vag:
					break;
				case FmodSoundType.Wav:
					name = "WAV";
					description = "PCM Wave Audio";
					contentTypes.Add(new ContentType("audio/wav"));
					fileExtensions.Add("wav");
					break;
				case FmodSoundType.XM:
					break;
				case FmodSoundType.Xma:
					break;
				default:
					break;
			}
		}
		#endregion
		
		#region Private Fields
		private FmodSoundFormat fmodSoundFormat = FmodSoundFormat.None;
		private FmodSoundType fmodSoundType = FmodSoundType.Unknown;
		private string name;
		private string description;
		private List<ContentType> contentTypes = new List<ContentType>();
		private List<string> fileExtensions = new List<string>();
		private List<ICodec> codecs = new List<ICodec>();
		#endregion

		#region IMediaFormat Members
		public string Name
		{
			get { return name; }
		}

		public string Description
		{
			get { return description; }
		}

		public IList<ContentType> ContentTypes
		{
			get { return contentTypes; }
		}

		public IList<string> FileExtensions
		{
			get { return fileExtensions; }
		}

		public IList<ICodec> Codecs
		{
			get { return codecs; }
		}
		#endregion
	}
}
