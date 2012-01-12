using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public struct SizeInfo
    {
        public SizeInfo(TimeSpan duration, uint height, uint width)
        {
            this.duration = duration;
            this.height = height;
            this.width = width;
        }

        private TimeSpan duration;
        private uint height;
        private uint width;

        public TimeSpan Duration
        {
            get { return duration; }
        }

        public uint Height
        {
            get { return height; }
        }

        public uint Width
        {
            get { return width; }
        }

        public static readonly SizeInfo Default = new SizeInfo(TimeSpan.Zero, 0, 0);
    }
}
