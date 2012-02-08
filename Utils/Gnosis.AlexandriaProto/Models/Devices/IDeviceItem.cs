using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Devices
{
    public interface IDeviceItem : IEntity
    {
        Uri Location { get; }
        string Name { get; }
        DateTime CreationTimeUtc { get; set; }
        DateTime LastAccessedTimeUtc { get; set; }
        DateTime LastModifiedTimeUtc { get; set; }
        IDeviceItemAttributes Attributes { get; }

        IEnumerable<IDeviceItem> Items { get; }

        void AddItem(IDeviceItem item);
        void RemoveItem(IDeviceItem item);
    }
}
