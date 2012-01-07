using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gnosis.Video.Vlc
{
    public class ThumbnailGenerator
    {
        public void GenerateThumbnails(string prefix, Uri outputPath, Uri videoPath, Action<object, EventArgs> completedCallback)
        {
            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentException("prefix");
            if (outputPath == null)
                throw new ArgumentNullException("outputPath");
            if (videoPath == null)
                throw new ArgumentNullException("videoPath");
            
            const string fileName = "vlc.exe";
            const string argsFormat = "--video-filter scene -V dummy --aout=dummy --scene-ratio=24 --scene-format=jpg --start-time=6 --run-time=2 --scene-prefix={0} --scene-path=\"{1}\" \"{2}\" vlc://quit";
            var arguments = string.Format(argsFormat, prefix, outputPath.LocalPath, videoPath.LocalPath);

            var process = new Process();
            
            process.StartInfo = new ProcessStartInfo(fileName, arguments);
            process.StartInfo.CreateNoWindow = true;
            if (completedCallback != null)
            {
                process.Exited += new EventHandler(completedCallback);
            }

            process.Start();
        }
    }
}
