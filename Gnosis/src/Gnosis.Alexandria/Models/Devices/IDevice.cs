using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Devices
{
    public interface IDevice : IChangeableModel
    {
        Uri Location { get; }
        string Name { get; }
        DateTime CreationTimeUtc { get; set; }
        DateTime LastAccessedTimeUtc { get; set; }
        DateTime LastModifiedTimeUtc { get; set; }

        IEnumerable<IDeviceItem> Items { get; }

        void AddItem(IDeviceItem item);
        void RemoveItem(IDeviceItem item);
    }
}
