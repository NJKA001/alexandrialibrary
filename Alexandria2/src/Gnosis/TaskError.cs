using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public struct TaskError
    {
        public TaskError(int count, int maximum, string description, Exception exception)
        {
            this.count = count;
            this.maximum = maximum;
            this.description = description;
            this.exception = exception;
        }

        private readonly int count;
        private readonly int maximum;
        private readonly string description;
        private readonly Exception exception;

        public int Count
        {
            get { return count; }
        }

        public int Maximum
        {
            get { return maximum; }
        }

        public string Description
        {
            get { return description; }
        }

        public Exception Exception
        {
            get { return exception; }
        }
    }
}
