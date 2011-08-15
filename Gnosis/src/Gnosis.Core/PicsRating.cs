using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class PicsRating
        : IPicsRating
    {
        public PicsRating(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = value;
        }

        private readonly string value;

        #region IPicsRating Members

        public string Value
        {
            get { return value; }
        }

        #endregion

        public override string ToString()
        {
            return value;
        }
    }
}
