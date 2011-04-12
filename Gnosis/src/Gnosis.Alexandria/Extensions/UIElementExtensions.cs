using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gnosis.Alexandria.Extensions
{
    public static class UIElementExtensions
    {
        public static bool IsWindowChrome(this UIElement self)
        {
            if (self == null)
                return false;

            var frameworkElement = self as FrameworkElement;
            if (frameworkElement != null && frameworkElement.Name == "Chrome")
                return true;

            return false;
        }
    }
}
