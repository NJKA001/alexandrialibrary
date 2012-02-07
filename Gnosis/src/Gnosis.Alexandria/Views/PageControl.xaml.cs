using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PageControl.xaml
    /// </summary>
    public partial class PageControl
        : UserControl, INotifyPropertyChanged
    {
        public PageControl()
        {
            InitializeComponent();
            numberOfPagesTextBlock.DataContext = this;
            pageIndexComboBox.DataContext = this;

            Visibility = System.Windows.Visibility.Collapsed;
        }

        private readonly ObservableCollection<int> pages = new ObservableCollection<int>() { 1 };
        private readonly IList<Action> selectedPageChangedCallbacks = new List<Action>();
        private int numberOfPages = 1;
        private int selectedPage = 1;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NotifySelectedPageChanged()
        {
            foreach (var callback in selectedPageChangedCallbacks)
                callback();
        }

        private void pageIndexComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var x = e.AddedItems.Count;
        }

        private void firstButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lastButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public IEnumerable<int> Pages
        {
            get { return pages; }
        }

        public int NumberOfPages
        {
            get { return numberOfPages; }
            set
            {
                if (numberOfPages != value)
                {
                    numberOfPages = value;
                    NotifyPropertyChanged("NumberOfPages");

                    pages.Clear();

                    if (numberOfPages == 0)
                        pages.Add(1);
                    else
                    {
                        for (var page = 1; page <= numberOfPages; page++)
                            pages.Add(page);
                    }

                    Visibility = numberOfPages > 1 ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        public int SelectedPage
        {
            get { return selectedPage; }
            set
            {
                if (selectedPage != value && pages.Contains(value))
                {
                    selectedPage = value;
                    NotifyPropertyChanged("SelectedPage");
                    NotifySelectedPageChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddSelectedPageChangedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            selectedPageChangedCallbacks.Add(callback);
        }
    }
}
