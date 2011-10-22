using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITask
    {
        int Progress { get; }
        TaskStatus Status { get; }
        Exception Error { get; }

        void AddStartedCallback(Action callback);
        void AddCancelledCallback(Action callback);
        void AddPausedCallback(Action callback);
        void AddResumedCallback(Action callback);
        void AddCompletedCallback(Action callback);
        void AddProgressCallback(Action<int> callback);
        void AddFailedCallback(Action<Exception> callback);

        void Start();
        void StartSynchronously();
        void StartSynchronously(TimeSpan timeout);
        void Pause();
        void Resume();
        void Cancel();
    }

    public interface ITask<T>
        : ITask
    {
        void AddResultsCallback(Action<T> callback);

        void UpdateResults(T results);
    }
}
