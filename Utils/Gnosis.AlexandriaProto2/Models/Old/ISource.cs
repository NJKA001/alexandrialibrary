﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gnosis.Alexandria.Models
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
        string Creator { get; set; }
        string Summary { get; set; }
        DateTime Date { get; set; }
        int Number { get; set; }
        string ImagePattern { get; set; }
        string ChildPattern { get; set; }
        string PagePattern { get; set; }

        IEnumerable<ISourceProperty> Properties { get; }
        IEnumerable<ISource> Children { get; }

        void AddProperty(ISourceProperty property);
        void RemoveProperty(ISourceProperty property);
        void AddChild(ISource child);
        void RemoveChild(ISource child);

        string Marquee { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        bool IsBeingEdited { get; set; }
        Visibility DisplayVisibility { get; }
        Visibility EditVisibility { get; }
        Visibility PatternVisibility { get; }

        void DeselectAll();
        bool IsDescendantOf(ISource source);
    }
}
