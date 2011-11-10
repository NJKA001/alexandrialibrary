using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Image;

namespace Gnosis.Alexandria.ViewModels
{
    public class AudioViewModel
        : IAudioViewModel
    {
        public AudioViewModel(int number, string title, TimeSpan duration)
            : this(number, title, duration, new PngImage(new Uri("pack://application:,,,/Images/File Audio MP3-01.png")))
        {
        }

        public AudioViewModel(int number, string title, TimeSpan duration, IImage image)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (image == null)
                throw new ArgumentNullException("image");

            this.number = number;
            this.title = title;
            this.duration = duration;
            this.image = image;
        }

        private readonly int number;
        private readonly string title;
        private readonly TimeSpan duration;
        private readonly IImage image;

        public int Number
        {
            get { return number; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Duration
        {
            get { return string.Format("{0:00}:{1:00}", Math.Truncate(duration.TotalMinutes), Math.Truncate(duration.TotalSeconds % 60)); }
        }

        public IImage Image
        {
            get { return image; }
        }
    }
}
