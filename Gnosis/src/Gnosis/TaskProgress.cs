using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public struct TaskProgress
    {
        public TaskProgress(int count, int maximum, string description)
        {
            this.count = count;
            this.maximum = maximum;
            this.description = description;
        }

        private readonly int count;
        private readonly int maximum;
        private readonly string description;

        public int Count { get { return count; } }
        public int Maximum { get { return maximum; } }
        public string Description { get { return description; } }
    }
}
