using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Validation
{
    public class PathValidation
    {
        public PathValidation(bool isValid, bool exists)
            : this(isValid, exists, null)
        {
        }

        public PathValidation(bool isValid, bool exists, Exception error)
        {
            this.isValid = isValid;
            this.exists = exists;
            this.error = error;
        }

        private bool isValid;
        private bool exists;
        private Exception error;

        public bool IsValid
        {
            get { return isValid; }
        }

        public bool Exists
        {
            get { return exists; }
        }

        public Exception Error
        {
            get { return error; }
        }
    }
}
