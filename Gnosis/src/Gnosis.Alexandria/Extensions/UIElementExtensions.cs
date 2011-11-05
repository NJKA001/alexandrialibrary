using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Gnosis.Alexandria.Extensions
{
    public static class UIElementExtensions
    {
        #region FindContainingItem

        public static T FindContainingItem<T>(this UIElement element)
            where T : UIElement
        {
            if (element != null)
            {
                var item = element as T;
                if (item != null)
                    return item;

                var parent = VisualTreeHelper.GetParent(element) as UIElement;
                if (parent != null)
                    return FindContainingItem<T>(parent);
            }

            return null;
        }

        #endregion

        #region FindContainingListViewItem

        public static ListViewItem FindContainingListViewItem(this UIElement element)
        {
            if (element != null)
            {
                var item = element as ListViewItem;
                if (item != null)
                    return item;

                var parent = VisualTreeHelper.GetParent(element) as UIElement;
                if (parent != null)
                    return FindContainingListViewItem(parent);
            }

            return null;
        }

        #endregion

        #region FindContainingTreeViewItem

        /// <summary>
        /// Go up the visual tree to find the TreeViewItem that contains the given UI element
        /// </summary>
        /// <param name="element">The UI element</param>
        /// <returns>The TreeViewItem that contains the given element, or null if the given element is not a child of a TreeViewItem</returns>
        public static TreeViewItem FindContainingTreeViewItem(this UIElement element)
        {
            if (element != null)
            {
                var item = element as TreeViewItem;
                if (item != null)
                    return item;

                var popup = element as Popup;
                if (popup != null && popup.PlacementTarget != null)
                    return FindContainingTreeViewItem(popup.PlacementTarget);

                var parent = VisualTreeHelper.GetParent(element) as UIElement;
                if (parent != null)
                    return FindContainingTreeViewItem(parent);
            }

            return null;
        }

        #endregion

        #region IsWindowChrome

        public static bool IsWindowChrome(this UIElement self)
        {
            if (self == null)
                return false;

            var frameworkElement = self as FrameworkElement;
            if (frameworkElement != null && frameworkElement.Name == "Chrome")
                return true;

            return false;
        }

        #endregion
    }
}
