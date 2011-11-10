using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for HoveringCloseIcon.xaml
    /// </summary>
    public partial class HoveringCloseIcon
        : UserControl, INotifyPropertyChanged
    {
        public HoveringCloseIcon()
        {
            InitializeComponent();

            this.closeItemImage.DataContext = this;
            this.closeItemImageOnHover.DataContext = this;
        }

        private bool mouseIsOverClose;
        private bool clickedOnClose;
        private bool isClosed;

        private void PropertyHasChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void closeItemImage_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseIsOverClose = true;
        }

        private void closeItemImageOnHover_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseIsOverClose = false;
            ClickedOnClose = false;
        }

        private void closeItemImageOnHover_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClickedOnClose = true;
        }

        private void closeItemImageOnHover_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ClickedOnClose)
            {
                IsClosed = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty IsClosedProperty =
            DependencyProperty.Register("IsClosed", typeof(bool), typeof(HoveringCloseIcon), new UIPropertyMetadata(false));

        public bool MouseIsOverClose
        {
            get { return mouseIsOverClose; }
            private set
            {
                if (mouseIsOverClose != value)
                {
                    mouseIsOverClose = value;
                    PropertyHasChanged("MouseIsOverClose");
                    PropertyHasChanged("CloseIconVisibility");
                    PropertyHasChanged("CloseIconHoverVisibility");
                }
            }
        }

        public bool ClickedOnClose
        {
            get { return clickedOnClose; }
            private set
            {
                if (clickedOnClose != value)
                {
                    clickedOnClose = value;
                    PropertyHasChanged("ClickedOnClose");
                }
            }
        }

        public bool IsClosed
        {
            get { return (bool)GetValue(IsClosedProperty); }
            set { SetValue(IsClosedProperty, value); }
        }
        public Visibility CloseIconVisibility
        {
            get { return mouseIsOverClose ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility CloseIconHoverVisibility
        {
            get { return mouseIsOverClose ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
