using System;

namespace Alexandria.LastFM.LastRipper
{
	public class PathSettings
	{
		public static string TempFilePath
		{
			get
			{
				return PathSettings.TempPath + "rip.mp3";
			}
		}

		public static string TempPath
		{
			get
			{
				string path = string.Format("{0}Alexandria{1}LastRipper{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
				
				if (!System.IO.Directory.Exists(path))
					System.IO.Directory.CreateDirectory(path);
				
				return path;
			}
		}
	}
}
