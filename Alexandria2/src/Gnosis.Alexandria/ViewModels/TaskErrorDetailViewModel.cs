using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Gnosis.Alexandria.ViewModels
{
    public class TaskErrorDetailViewModel
        : ITaskDetailViewModel
    {
        public TaskErrorDetailViewModel(TaskError error)
        {
            this.error = error;
        }

        private readonly TaskError error;

        public string Type
        {
            get { return "Error"; }
        }

        public Brush Foreground
        {
            get { return Brushes.DarkRed; }
        }

        public int Count
        {
            get { return error.Count; }
        }

        public int Maximum
        {
            get { return error.Maximum; }
        }

        public string Description
        {
            get { return error.Description; }
        }

        public Visibility MoreVisibility
        {
            get { return Visibility.Visible; }
        }

        public string Message
        {
            get { return error.Exception.Message; }
        }

        public string Trace
        {
            get { return error.Exception.StackTrace; }
        }
    }
}
