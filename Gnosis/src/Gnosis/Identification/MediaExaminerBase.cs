using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public abstract class MediaExaminerBase
        : IMediaExaminer
    {
        private readonly IList<IMediaExaminer> children = new List<IMediaExaminer>(); 

        protected abstract bool DoCanExamine(IMediaInfo info);
        protected abstract IMediaInfo DoExamine(IMediaInfo info);

        public bool CanExamine(IMediaInfo info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            return DoCanExamine(info);
        }

        public IMediaInfo Examine(IMediaInfo info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            var result = DoExamine(info);

            foreach (var child in children)
            {
                if (child.CanExamine(result))
                {
                    return child.Examine(result);
                }
            }

            return result;
        }

        public void AddChild(IMediaExaminer child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            children.Add(child);
        }

        public IEnumerable<IMediaExaminer> Children
        {
            get { return children; }
        }
    }
}
