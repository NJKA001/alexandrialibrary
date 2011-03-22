using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Gnosis.Archon
{
    public class PlaybackStatus : INotifyPropertyChanged
    {
        private bool isPlaying = false;
        private string imagePath = playPath;
        private const string playPath = "pack://application:,,,/Images/play.png";
        private const string pausePath = "pack://application:,,,/Images/pause.png";

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                if (isPlaying != value)
                {
                    isPlaying = value;
                    OnPropertyChanged("IsPlaying");
                    ImagePath = isPlaying ? pausePath : playPath;
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
