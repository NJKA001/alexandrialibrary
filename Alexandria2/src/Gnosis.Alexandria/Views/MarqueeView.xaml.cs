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
using System.Windows.Threading;

using Gnosis.Alexandria.Controllers;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MediaMarqueeView.xaml
    /// </summary>
    public partial class MarqueeView : UserControl
    {
        public MarqueeView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private IMarqueeController controller;


        private void SelectedPageChanged()
        {
            controller.PageIndex = pageControl.SelectedPage - 1;
            RefreshItems();
        }

        private void filterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Action action = () =>
                {
                    controller.UpdateFilter(filterTextBox.Text);
                    marqueeContainer.ItemsSource = controller.GetItems();
                };
                
                Dispatcher.Invoke(action, DispatcherPriority.DataBind);
            }
            catch (Exception ex)
            {
                logger.Error("  MarqueeView.filterTextBox_KeyUp", ex);
            }
        }

        public void Initialize(ILogger logger, IMarqueeController controller)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (controller == null)
                throw new ArgumentNullException("controller");

            this.logger = logger;
            this.controller = controller;
            
            try
            {
                this.pageControl.AddSelectedPageChangedCallback(() => SelectedPageChanged());
            }
            catch (Exception ex)
            {
                logger.Error("  MarqueeView.Initialize", ex);
            }
        }

        public void RefreshItems()
        {
            try
            {
                Action action = () =>
                {
                    controller.RefreshItems();
                    marqueeContainer.ItemsSource = controller.GetItems();
                    pageControl.NumberOfPages = controller.NumberOfPages;
                };

                Dispatcher.Invoke(action, DispatcherPriority.DataBind);
            }
            catch (Exception ex)
            {
                logger.Error("  MarqueeView.RefreshItems", ex);
            }
        }
    }
}
