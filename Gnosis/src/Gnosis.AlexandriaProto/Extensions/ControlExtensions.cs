using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace Gnosis.Alexandria.Extensions
{
    public static class ControlExtensions
    {
        public static bool HasMouseOverScrollbar(this Control control)
        {
            var position = Mouse.GetPosition(control);
            HitTestResult result = VisualTreeHelper.HitTest(control, position);
            if (result == null)
                return false;

            DependencyObject hit = result.VisualHit;
            while (hit != null)
            {
                if (hit is ScrollBar)
                    return true;

                // VisualTreeHelper works with objects of type Visual or Visual3D.
                // If the current object is not derived from Visual or Visual3D,
                // then use the LogicalTreeHelper to find the parent element.
                if (hit is Visual || hit is System.Windows.Media.Media3D.Visual3D)
                    hit = VisualTreeHelper.GetParent(hit);
                else
                    hit = LogicalTreeHelper.GetParent(hit);
            }

            return false;
        }
    }
}
