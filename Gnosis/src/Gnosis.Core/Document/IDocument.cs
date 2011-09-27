using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document
{
    public interface IDocument
        : IMedia
    {
        void Load();
    }
}
