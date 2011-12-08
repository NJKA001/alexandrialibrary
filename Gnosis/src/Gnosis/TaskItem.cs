using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public struct TaskItem
    {
        public TaskItem(Uri id, uint number, string name, TimeSpan duration, Uri target, IMediaType targetType, bool hasPrevious, bool hasNext, object image)
        {
            this.id = id;
            this.number = number;
            this.name = name;
            this.duration = duration;
            this.target = target;
            this.targetType = targetType;
            this.hasPrevious = hasPrevious;
            this.hasNext = hasNext;
            this.image = image;
        }

        private readonly Uri id;
        private readonly uint number;
        private readonly string name;
        private readonly TimeSpan duration;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private bool hasPrevious;
        private bool hasNext;
        private readonly object image;
        
        public Uri Id
        {
            get { return id; }
        }

        public uint Number
        {
            get { return number; }
        }

        public string Name
        {
            get { return name; }
        }

        public TimeSpan Duration
        {
            get { return duration; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public IMediaType TargetType
        {
            get { return targetType; }
        }

        public bool HasPrevious
        {
            get { return hasPrevious; }
        }

        public bool HasNext
        {
            get { return hasNext; }
        }

        public object Image
        {
            get { return image; }
        }
    }
}
