using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp6.Helper
{
    public static class VisualHelper
    {
        public static T TryFindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parent = null;

            if (child is FrameworkElement frameworkElement)
            {
                parent = frameworkElement.Parent;
            }

            //we've reached the end of the tree
            if (parent == null) return null;

            if (parent is T)
            {
                return parent as T;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parent);
            }
        }
    }
}
