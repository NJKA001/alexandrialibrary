using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITagTuple
    {
        object[] ToArray();
        byte[] ToByteArray();
        DateTime ToDateTime();
        T ToEnum<T>() where T : struct;
        string[] ToStringArray();
        TimeSpan ToTimeSpan();
        uint ToUInt32();
    }
}
