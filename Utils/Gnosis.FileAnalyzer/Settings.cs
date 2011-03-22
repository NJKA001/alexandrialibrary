using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.FileAnalyzer
{
    public class Settings
    {
        public Settings()
        {
            SearchDirectory = @"\\vmware-host\Shared Folders\Documents\My Music";
            LogFileName = "fa.log";
            LogFileDirectory = @".\";
            IncludeSubdirectories = true;
            OverwriteLog = true;
        }

        public string SearchDirectory { get; set; }
        public string LogFileName { get; set; }
        public string LogFileDirectory { get; set; }
        public bool IncludeSubdirectories { get; set; }
        public bool OverwriteLog { get; set; }

        public string LogFilePath
        {
            get
            {
                return string.Format("{0}{1}", LogFileDirectory, LogFileName);
            }
        }
    }
}
