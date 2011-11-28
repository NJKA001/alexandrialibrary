using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public class TaskController
        : ITaskController
    {
        public TaskController(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;
        }

        private readonly ILogger logger;
        private readonly ObservableCollection<ITaskViewModel> taskViewModels = new ObservableCollection<ITaskViewModel>();

        public IEnumerable<ITaskViewModel> Tasks
        {
            get { return taskViewModels; }
        }

        public void AddTask(ITaskViewModel task)
        {
            task.AddCancelCallback(x => RemoveTask(x));
            taskViewModels.Add(task);
        }

        public void RemoveTask(ITaskViewModel task)
        {
            if (taskViewModels.Contains(task))
                taskViewModels.Remove(task);
        }
    }
}
