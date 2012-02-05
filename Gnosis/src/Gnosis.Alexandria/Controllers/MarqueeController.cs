using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public class MarqueeController
        : IMarqueeController
    {
        public MarqueeController(ILogger logger, IMarqueeRepository repository, MetadataCategory category)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (repository == null)
                throw new ArgumentNullException("repository");

            this.logger = logger;
            this.repository = repository;
            this.category = category;
        }

        private readonly ILogger logger;
        private readonly IMarqueeRepository repository;
        private readonly MetadataCategory category;
        private readonly ObservableCollection<IMarquee> items = new ObservableCollection<IMarquee>();

        private string filter;
        private int numberOfPages = 1;
        private int pageIndex = 0;
        private int pageSize = 100000;

        public int NumberOfPages
        {
            get { return numberOfPages; }
            set
            {
                numberOfPages = value;
                RefreshItems();
            }
        }

        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                RefreshItems();
            }
        }

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                RefreshItems();
            }
        }

        public IEnumerable<IMarquee> GetItems()
        {
            return string.IsNullOrEmpty(filter) ?
                items
                : items.Where(x => x != null && x.Name != null && x.Name.ToLower().Contains(filter.ToLower()));
        }

        public void RefreshItems()
        {
            try
            {
                var page = repository.GetMarqueePage(category, pageIndex, pageSize);
                numberOfPages = page.NumberOfPages;
                items.Clear();

                foreach (var item in page.Items)
                    items.Add(item);
            }
            catch (Exception ex)
            {
                logger.Error("  MaqueeController.RefreshItems", ex);
            }
        }

        public void UpdateFilter(string filter)
        {
            this.filter = filter;
        }
    }
}
