using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Importing
{
    public interface IMediaImporter
    {
        void SetDirectoryPathFilter(Func<string, bool> directoryFilter);
        void SetMediaPathFilter(Func<string, bool> mediaPathFilter);
        void SetMediaFilter(Func<IMedia, bool> mediaFilter);

        void SetDirectoryCallback(Action<string> directoryCallback);
        void SetImportCallback(Action<IImportInfo> importCallback);
        void SetCompletedCallback(Action completedCallback);

        void Import(string path);
    }
}
