using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Gnosis.Alexandria.ViewModels
{
    public class TaskProgressDetailViewModel
        : ITaskDetailViewModel
    {
        public TaskProgressDetailViewModel(TaskProgress progress)
        {
            this.progress = progress;
        }

        private readonly TaskProgress progress;

        public string Type
        {
            get { return "Progress"; }
        }

        public Brush Foreground
        {
            get { return Brushes.DarkBlue; }
        }

        public int Count
        {
            get { return progress.Count; }
        }

        public int Maximum
        {
            get { return progress.Maximum; }
        }

        public Visibility MoreVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public string Description
        {
            get { return progress.Description; }
        }

        public string Message
        {
            get { return string.Empty; }
        }

        public string Trace
        {
            get { return string.Empty; }
        }
    }
}
