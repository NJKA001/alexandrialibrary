using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gnosis.Video.Vlc
{
    public class ThumbnailGenerator
    {
        public void GenerateThumbails(Uri outputPath, Uri videoPath)
        {
            if (outputPath == null)
                throw new ArgumentNullException("outputPath");
            if (videoPath == null)
                throw new ArgumentNullException("videoPath");

            const string fileName = "vlc";
            const string format = "--video-filter scene -V dummy --aout=dummy --scene-ratio=24 --scene-format=jpg --start-time=6 --run-time=2 --scene-prefix=thumb --scene-path=\"{0}\" \"{1}\" vlc://quit";
            var arguments = string.Format(format, outputPath.LocalPath, videoPath.LocalPath);

            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo(fileName, arguments);
                process.Start();
            }
        }
    }
}
