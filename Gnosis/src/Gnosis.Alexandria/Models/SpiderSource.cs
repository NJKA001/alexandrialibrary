using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gnosis.Alexandria.Models
{
    public class SpiderSource : SourceBase
    {
        public SpiderSource()
            : this(Guid.NewGuid())
        {
        }

        public SpiderSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/spider.png";
            AddChild(new ProxySource(Guid.Empty) { Parent = this });
        }

        public override Visibility PatternVisibility
        {
            get { return Visibility.Visible; }
        }
    }
}