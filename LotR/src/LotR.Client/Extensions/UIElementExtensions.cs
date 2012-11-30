using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LotR.Client.Extensions
{
    public static class UIElementExtensions
    {
        public static T FindContainerItem<T>(this UIElement element)
            where T : UIElement
        {
            if (element == null)
                return null;

            var item = element as T;
            if (item != null)
                return item;

            var parent = VisualTreeHelper.GetParent(element) as UIElement;
            if (parent == null)
                return null;

            return parent.FindContainerItem<T>();
        }

        private static ListBoxItem FindContainingListBoxItem(this UIElement element)
        {
            if (element != null)
            {
                var item = element as ListBoxItem;
                if (item != null)
                    return item;

                var parent = VisualTreeHelper.GetParent(element) as UIElement;
                if (parent != null)
                    return parent.FindContainingListBoxItem();
            }

            return null;
        }

        private static ListViewItem FindContainingListViewItem(this UIElement element)
        {
            if (element == null)
                return null;
            
            var item = element as ListViewItem;
            if (item != null)
                return item;

            var parent = VisualTreeHelper.GetParent(element) as UIElement;
            if (parent == null)
                return null;

            return parent.FindContainingListViewItem();
        }
    }
}
