using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITask
    {
        TaskStatus Status { get; }
        TaskError Error { get; }
        TaskProgress Progress { get; }
        TaskItem Item { get; }

        void AddStartedCallback(Action callback);
        void AddStoppedCallback(Action callback);
        void AddCancelledCallback(Action callback);
        void AddPausedCallback(Action callback);
        void AddResumedCallback(Action callback);
        void AddErrorCallback(Action<TaskError> callback);
        void AddProgressCallback(Action<TaskProgress> callback);
        void AddItemChangedCallback(Action<TaskItem> callback);
        void AddFailedCallback(Action callback);
        void AddCompletedCallback(Action callback);

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

        void BeginProgressUpdate();
        void UpdateProgress(int value);
    }

    public interface ITask<T>
        : ITask
    {
        void AddResultsCallback(Action<T> callback);

        void UpdateResults(T results);
    }
}
