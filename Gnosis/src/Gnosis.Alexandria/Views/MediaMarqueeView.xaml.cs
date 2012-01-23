using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MediaMarqueeView.xaml
    /// </summary>
    public partial class MediaMarqueeView : UserControl
    {
        public MediaMarqueeView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private IMediaMarqueeRepository repository;
        private IMediaMarqueePage page;

        public void Initialize(ILogger logger, IMediaMarqueeRepository repository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (repository == null)
                throw new ArgumentNullException("repository");

            this.logger = logger;
            this.repository = repository;
        }

        public void LoadPage(MediaCategory category, int pageIndex, int pageSize)
        {
            try
            {
                page = repository.GetMarqueePage(category, pageIndex, pageSize);
                this.mediaMarqueeContainer.ItemsSource = page.Items;
            }
            catch (Exception ex)
            {
                logger.Error("  MediaMarqueeView.LoadPage", ex);
            }
        }
    }
}
