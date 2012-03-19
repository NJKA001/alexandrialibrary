using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class MarqueePage
        : IMarqueePage
    {
        public MarqueePage(IEnumerable<IMarquee> items, int numberOfPages, int pageIndex, int pageSize)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (numberOfPages < 1)
                throw new ArgumentException("numberOfPages cannot be less than 1");
            if (pageIndex < 0)
                throw new ArgumentException("pageIndex cannot be less than 0");
            if (pageSize < 1)
                throw new ArgumentException("pageSize cannot be less than 1");

            this.items = items;
            this.numberOfPages = numberOfPages;
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
        }

        private readonly IEnumerable<IMarquee> items;
        private readonly int numberOfPages;
        private readonly int pageIndex;
        private readonly int pageSize;

        public IEnumerable<IMarquee> Items
        {
            get { return items; }
        }

        public int NumberOfPages
        {
            get { return numberOfPages; }
        }

        public int PageIndex
        {
            get { return pageIndex; }
        }

        public int PageSize
        {
            get { return pageSize; }
        }
    }
}
