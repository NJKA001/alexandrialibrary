using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ICharacterSetFactory
    {
        ICharacterSet Default { get; }
        ICharacterSet GetByName(string name);
        ICharacterSet GetByEncoding(Encoding encoding);
        ICharacterSet GetByHeader(byte[] header);

        void AddCharacterSet(ICharacterSet characterSet);
    }
}
