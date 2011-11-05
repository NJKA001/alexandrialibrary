using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Gnosis.Alexandria.Extensions
{
    public static class TreeViewExtensions
    {
        public static void SetSelectedItem(this TreeView control, object item)
        {
            DependencyObject dObject = control
                .ItemContainerGenerator
                .ContainerFromItem(item);

            //uncomment the following line if UI updates are unnecessary
            //((TreeViewItem)dObject).IsSelected = true;                

            MethodInfo selectMethod =
                typeof(TreeViewItem).GetMethod("Select",
                BindingFlags.NonPublic | BindingFlags.Instance);

            selectMethod.Invoke(dObject, new object[] { true });
        }
    }
}
