using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Devices
{
    public interface IMovableDeviceItem : IDeviceItem
    {
        void Move(Uri location);
    }
}
