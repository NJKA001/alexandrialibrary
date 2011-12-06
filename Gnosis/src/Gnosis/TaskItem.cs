using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class TaskItem
        : ITaskItem
    {
        public TaskItem(Uri id, uint number, string name, TimeSpan duration, object image)
        {
            this.id = id;
            this.number = number;
            this.name = name;
            this.duration = duration;
            this.image = image;
        }

        private readonly Uri id;
        private readonly uint number;
        private readonly string name;
        private readonly TimeSpan duration;
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

        public object Image
        {
            get { return image; }
        }
    }
}
