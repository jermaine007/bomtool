using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace NooneUI.Pdf
{
    public static class UIElementExtensions
    {
        /// <summary>
        /// Find specified type child controls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IEnumerable<T> Find<T>(this DependencyObject element) where T : DependencyObject
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                if (child is T item)
                {
                    yield return item;
                }
                foreach (var o in child.Find<T>())
                {
                    yield return o;
                }
            }
        }
    }
}
