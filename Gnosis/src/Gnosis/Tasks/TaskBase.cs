using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tasks
{
    #region TaskBase

    public abstract class TaskBase
        : ITask
    {
        protected TaskBase(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;

            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += WorkCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }

        #region Private Members

        private TaskStatus status = TaskStatus.Ready;
        private TaskError error = default(TaskError);
        private TaskProgress progress = default(TaskProgress);
        private ITaskItem item;

        private readonly IList<Action> startedCalledbacks = new List<Action>();
        private readonly IList<Action> stoppedCallbacks = new List<Action>();
        private readonly IList<Action> cancelledCallbacks = new List<Action>();
        private readonly IList<Action> pausedCallbacks = new List<Action>();
        private readonly IList<Action> resumedCallbacks = new List<Action>();
        private readonly IList<Action<TaskError>> errorCallbacks = new List<Action<TaskError>>();
        private readonly IList<Action<TaskProgress>> progressCallbacks = new List<Action<TaskProgress>>();
        private readonly IList<Action<ITaskItem>> itemChangedCallbacks = new List<Action<ITaskItem>>();
        private readonly IList<Action> failedCallbacks = new List<Action>();
        private readonly IList<Action> completedCallbacks = new List<Action>();

        private readonly BackgroundWorker worker = new BackgroundWorker();

        private void OnStarted()
        {
            foreach (var callback in startedCalledbacks)
                callback();
        }

        private void OnStopped()
        {
            foreach (var callback in stoppedCallbacks)
                callback();
        }

        private void OnCancelled()
        {
            foreach (var callback in cancelledCallbacks)
                callback();
        }

        private void OnPaused()
        {
            foreach (var callback in pausedCallbacks)
                callback();
        }

        private void OnResumed()
        {
            foreach (var callback in resumedCallbacks)
                callback();
        }

        private void OnError()
        {
            foreach (var callback in errorCallbacks)
                callback(error);
        }

        private void OnProgressUpdated()
        {
            foreach (var callback in progressCallbacks)
                callback(progress);
        }

        private void OnCompleted()
        {
            foreach (var callback in completedCallbacks)
                callback();
        }

        private void OnFailed()
        {
            foreach (var callback in failedCallbacks)
                callback();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DoWork();
            }
            catch (Exception ex)
            {
                var description = "TaskBase.DoWork failed with an unhandled exception";
                logger.Error(description, ex);
                UpdateError(1, 1, description, ex);
                Fail();
            }
        }

        private void Wait(TimeSpan timeout)
        {
            if (status != TaskStatus.Running)
                return;

            var start = DateTime.Now;
            while (status == TaskStatus.Running)
            {
                System.Threading.Thread.Sleep(100);
                if (timeout > TimeSpan.Zero && DateTime.Now.Subtract(start) > timeout)
                    throw new TimeoutException(string.Format("Timeout occured in TaskHandle: {0} seconds elapsed", timeout.TotalSeconds));
            }
        }

        #endregion

        protected readonly ILogger logger;

        protected abstract void DoWork();

        protected void ChangeItem(ITaskItem item)
        {
            this.item = item;

            foreach (var callback in itemChangedCallbacks)
                callback(item);
        }

        protected void BlockIfPaused()
        {
            while (status == TaskStatus.Paused)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        protected bool IsActive()
        {
            while (status == TaskStatus.Paused)
            {
                System.Threading.Thread.Sleep(100);
            }

            return (status == TaskStatus.Running);
        }

        protected virtual void WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (status == TaskStatus.Running)
                {
                    status = TaskStatus.Completed;

                    OnCompleted();
                }
                else
                {
                    UpdateProgress(0, 100, string.Empty);
                }
            }
            catch (Exception ex)
            {
                logger.Error("TaskHandle.WorkCompleted", ex);
            }
        }

        protected virtual void UpdateProgress(int count, int maximum, string description)
        {
            this.progress = new TaskProgress(count, maximum, description);

            OnProgressUpdated();
        }

        protected virtual void UpdateError(int count, int maximum, string description, Exception exception)
        {
            error = new TaskError(count, maximum, description, exception);

            OnError();
        }

        protected virtual ITaskItem GetPreviousItem()
        {
            return null;
        }

        protected virtual ITaskItem GetNextItem()
        {
            return null;
        }

        protected virtual void Fail()
        {
            status = TaskStatus.Failed;

            OnFailed();
        }

        public TaskStatus Status
        {
            get { return status; }
        }

        public TaskError Error
        {
            get { return error; }
        }

        public TaskProgress Progress
        {
            get { return progress; }
        }

        public ITaskItem Item
        {
            get { return item; }
        }

        public void AddStartedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            startedCalledbacks.Add(callback);
        }

        public void AddStoppedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            stoppedCallbacks.Add(callback);
        }

        public void AddCancelledCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            cancelledCallbacks.Add(callback);
        }

        public void AddPausedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            pausedCallbacks.Add(callback);
        }

        public void AddResumedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            resumedCallbacks.Add(callback);
        }

        public void AddErrorCallback(Action<TaskError> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            errorCallbacks.Add(callback);
        }

        public void AddProgressCallback(Action<TaskProgress> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            progressCallbacks.Add(callback);
        }

        public void AddItemChangedCallback(Action<ITaskItem> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            itemChangedCallbacks.Add(callback);
        }

        public void AddFailedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            failedCallbacks.Add(callback);
        }

        public void AddCompletedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            completedCallbacks.Add(callback);
        }

        public void Reset()
        {
            if (status == TaskStatus.Cancelled || status == TaskStatus.Completed || status == TaskStatus.Failed)
            {
                status = TaskStatus.Ready;
                progress = default(TaskProgress);
                error = default(TaskError);
            }
        }

        public void Start()
        {
            if (status != TaskStatus.Ready)
                throw new InvalidOperationException("Cannot start a task that is not ready. status: " + status);

            status = TaskStatus.Running;

            OnStarted();

            worker.RunWorkerAsync();
        }

        public void StartSynchronously()
        {
            if (status != TaskStatus.Ready)
                return;

            Start();

            Wait(TimeSpan.Zero);
        }

        public void StartSynchronously(TimeSpan timeout)
        {
            if (status != TaskStatus.Ready)
                return;

            Start();

            Wait(timeout);
        }

        public void Stop()
        {
            if (status != TaskStatus.Running && status != TaskStatus.Paused)
                return;

            status = TaskStatus.Ready;

            worker.CancelAsync();

            OnStopped();

            UpdateProgress(0, 100, string.Empty);
        }

        public void Pause()
        {
            if (status != TaskStatus.Running)
                return;

            status = TaskStatus.Paused;

            OnPaused();
        }

        public void Resume()
        {
            if (status != TaskStatus.Paused)
                return;

            status = TaskStatus.Running;

            OnResumed();
        }

        public void Cancel()
        {
            try
            {
                status = TaskStatus.Cancelled;

                worker.CancelAsync();

                OnCancelled();
            }
            catch (Exception ex)
            {
                logger.Error("  TaskHandle.Cancel", ex);
            }
        }

        public void PreviousItem()
        {
            var previous = GetPreviousItem();

            ChangeItem(previous);
        }

        public void NextItem()
        {
            var next = GetNextItem();

            ChangeItem(next);
        }
    }

    #endregion

    #region TaskBase<T>

    public abstract class TaskBase<T>
        : TaskBase, ITask<T>
    {
        protected TaskBase(ILogger logger)
            : base(logger)
        {
        }

        private readonly IList<Action<T>> resultsCallbacks = new List<Action<T>>();

        public void AddResultsCallback(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            resultsCallbacks.Add(callback);
        }

        public void UpdateResults(T results)
        {
            foreach (var callback in resultsCallbacks)
                callback(results);
        }
    }

    #endregion
}
