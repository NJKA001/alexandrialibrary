using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class SynchronizedText
        : ValueBase<ISynchronizedText>, ISynchronizedText
    {
        public SynchronizedText()
        {
            AddInitializer(value => this.time = value.ToInt64(), synchedText => synchedText.Time);
            AddInitializer(value => this.text = value.ToString(), synchedText => synchedText.Text);
        }

        public SynchronizedText(Guid parent, uint sequence, long time, string text)
        {
            AddInitializer(value => this.time = time, synchedText => synchedText.Time);
            AddInitializer(value => this.text = text, synchedText => synchedText.Text);

            Initialize(parent, sequence);
        }

        private long time;
        private string text;

        #region ISynchronizedText Members

        public long Time
        {
            get { return time; }
        }

        public string Text
        {
            get { return text; }
        }

        #endregion
    }
}
