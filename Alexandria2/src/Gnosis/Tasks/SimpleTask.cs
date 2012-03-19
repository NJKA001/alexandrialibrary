using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tasks
{
    public class SimpleTask
        : TaskBase
    {
        public SimpleTask(ILogger logger, Action workFunction)
            : base(logger)
        {
            if (workFunction == null)
                throw new ArgumentNullException("workFunction");

            this.workFunction = workFunction;
        }

        private Action workFunction;

        protected override void DoWork()
        {
            try
            {
                workFunction();
                UpdateProgress(1, 0,  "Simple Task Completed");
            }
            catch (Exception ex)
            {
                UpdateError(1, 0, "Simple Task Failed", ex);
            }
        }
    }
}
