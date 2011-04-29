using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface IModelContext
    {
        ITimeStamp GetCreatedTimeStamp();
        ITimeStamp GetAccessedTimeStamp(ITimeStamp timeStamp);
        ITimeStamp GetModifiedTimeStamp(ITimeStamp timeStamp);
        Uri GetCurrentUser();
    }
}
