using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public class Step
        : IStep
    {
        public Step(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            this.text = text;
        }

        public Step(string text, string itemName, object itemValue)
            : this(text)
        {
            AddItem(itemName, itemValue);
        }

        private readonly string text;
        private readonly IDictionary<string, object> items = new Dictionary<string, object>();

        public string Text
        {
            get { return text; }
        }

        public IEnumerable<KeyValuePair<string, object>> Items
        {
            get { return items; }
        }

        public void AddItem(string name, object value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            items.Add(name, value);
        }
    }
}
