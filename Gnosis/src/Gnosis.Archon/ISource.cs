using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public interface ISource
    {
        Guid Id { get; }
        string Path { get; set; }
        string ImagePath { get; set; }
        ICollection<byte> ImageData { get; set; }
        object ImageSource { get; }
        ISource Parent { get; set; }
        string Name { get; set; }
        string NameHash { get; }
        string NameMetaphone { get; }
        IEnumerable<ISourceProperty> Properties { get; }
        IEnumerable<ISource> Children { get; }

        void AddProperty(ISourceProperty property);
        void AddChild(ISource child);

        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
    }
}
