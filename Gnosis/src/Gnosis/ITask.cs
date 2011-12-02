using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITask
    {
        TaskProgress Progress { get; }
        TaskStatus Status { get; }
        TaskError LastError { get; }
        IEnumerable<ITaskItem> Items { get; }
        ITaskItem CurrentItem { get; }
        bool SupportsPlayback { get; }

        void AddStartedCallback(Action callback);
        void AddStoppedCallback(Action callback);
        void AddCancelledCallback(Action callback);
        void AddPausedCallback(Action callback);
        void AddResumedCallback(Action callback);
        void AddErrorCallback(Action<TaskError> callback);
        void AddCompletedCallback(Action callback);
        void AddProgressCallback(Action<TaskProgress> callback);
        void AddFailedCallback(Action callback);

        void Reset();
        void Start();
        void StartSynchronously();
        void StartSynchronously(TimeSpan timeout);
        void Stop();
        void Pause();
        void Resume();
        void Cancel();
        void PreviousItem();
        void NextItem();
    }

    public interface ITask<T>
        : ITask
    {
        void AddResultsCallback(Action<T> callback);

        void UpdateResults(T results);
    }
}
