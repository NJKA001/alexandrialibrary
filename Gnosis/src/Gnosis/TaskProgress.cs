using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public struct TaskProgress
    {
        public TaskProgress(int number, string description)
        {
            this.number = number;
            this.description = description;
        }

        private readonly int number;
        private readonly string description;

        public int Number { get { return number; } }
        public string Description { get { return description; } }
    }
}
