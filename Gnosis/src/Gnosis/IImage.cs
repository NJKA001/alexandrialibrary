using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IImage
        : IMedia
    {
        bool IsLoaded { get; }
        
        void Load();
        byte[] GetData();
        object GetImageSource();
    }
}
