using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CrawlerLibrary;
using System.Collections.ObjectModel;

namespace CrawlerUI
{
    internal class TreeViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            if (value is CrawlResult)
            {
                CrawlResult crawlResult = (CrawlResult)value;

                TreeViewItem currentTreeViewItem = new TreeViewItem() { Header = crawlResult.Url };
                ToTreeViewItem(crawlResult, currentTreeViewItem);

                return new ObservableCollection<TreeViewItem>() { currentTreeViewItem };
            }
            else
            {
                return new object();
            }            
        }

        private TreeViewItem ToTreeViewItem(CrawlResult crawlResult, TreeViewItem treeViewItem)
        {

            if (crawlResult.Children != null)
            {
                foreach (CrawlResult childNode in crawlResult.Children)
                {
                    TreeViewItem currentTreeViewItem = new TreeViewItem() { Header = childNode.Url };
                    ToTreeViewItem(childNode, currentTreeViewItem);
                    treeViewItem.Items.Add(currentTreeViewItem);
                }
            }
            treeViewItem.Foreground = System.Windows.Media.Brushes.LightGreen;
            return treeViewItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
