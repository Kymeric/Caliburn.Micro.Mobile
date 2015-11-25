using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Foundation;
using UIKit;

namespace Caliburn.Micro.Mobile.iOS.Bindings
{
    public class UITableViewGroupedBinding<TViewModel, TItem, TKey> : UITableViewBinding<TViewModel, TItem>
        where TViewModel : INotifyPropertyChanged
    {
        public UITableViewGroupedBinding(TViewModel viewModel, UITableView view,
                                         Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty,
                                         Action<TItem, UITableViewCell> cellBinding,
                                         Func<TItem, TKey> getKey,
                                         Func<IGrouping<TKey, TItem>, string> headerTitle = null,
                                         Func<IGrouping<TKey, TItem>, string> footerTitle = null,
                                         IComparer<TKey> keySort = null)
            : base(viewModel)
        {
            view.Source = new BoundGroupedTableSource(viewModel, view, itemsProperty, cellBinding, getKey, headerTitle, footerTitle, keySort);
        }

        private class BoundGroupedTableSource : BoundTableSource
        {
            private readonly Func<TItem, TKey> _getKey;
            private readonly IComparer<TKey> _keySort;
            private readonly Func<IGrouping<TKey, TItem>, string> _headerTitle;
            private readonly Func<IGrouping<TKey, TItem>, string> _footerTitle;

            public BoundGroupedTableSource(TViewModel viewModel, UITableView view,
                                           Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty,
                                           Action<TItem, UITableViewCell> cellBinding,
                                           Func<TItem, TKey> getKey,
                                           Func<IGrouping<TKey, TItem>, string> headerTitle = null,
                                           Func<IGrouping<TKey, TItem>, string> footerTitle = null,
                                           IComparer<TKey> keySort = null)
                : base(viewModel, view, itemsProperty, cellBinding)
            {
                _getKey = getKey;
                _headerTitle = headerTitle;
                _footerTitle = footerTitle;
                _keySort = keySort;
            }

            protected override void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    var sections = Sections;
                    var newSections = sections.Where(s => s.All(i => e.NewItems.Contains(i))).ToList();

                    foreach (var newSection in newSections)
                    {
                        View.InsertSections(NSIndexSet.FromIndex(sections.IndexOf(newSection)), UITableViewRowAnimation.None);
                    }

                    var newRowGroups = e.NewItems.Cast<TItem>().Where(i => !newSections.Any(ns => ns.Contains(i)))
                                   .GroupBy(_getKey).ToList();
                    foreach (var newRowGroup in newRowGroups)
                    {
                        var sectionIndex = Sections.FindIndex(g => object.Equals(g.Key, newRowGroup.Key));
                        var section = sections[sectionIndex];
                        View.InsertRows(newRowGroup.Select(nr => NSIndexPath.FromRowSection(section.ToList().IndexOf(nr), sectionIndex)).ToArray(), UITableViewRowAnimation.None);
                    }
                }
                //TODO: Handle these so we don't have to do a ReloadData?
                //else if (e.Action == NotifyCollectionChangedAction.Remove)
                //{
                //}
                //else if (e.Action == NotifyCollectionChangedAction.Replace)
                //{
                //    View.ReloadRows(Enumerable.Range(e.OldStartingIndex, e.OldItems.Count)
                //        .Select(i => NSIndexPath.FromRowSection(i, 0)).ToArray(),
                //        UITableViewRowAnimation.Automatic);
                //}
                else
                {
                    Debug.WriteLine($"Reloading Table Data for action: {e.Action}");
                    View.ReloadData();
                }
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                var g = Sections.ElementAt((int)section);
                return g.Count();
            }

            protected override TItem GetItem(NSIndexPath indexPath)
            {
                return Sections[indexPath.Section].ElementAt(indexPath.Row);
            }

            public override nint NumberOfSections(UITableView tableView)
            {
                return Sections.Count;
            }

            public override string TitleForHeader(UITableView tableView, nint section)
            {
                return _headerTitle?.Invoke(Sections.ElementAt((int)section));
            }

            public override string TitleForFooter(UITableView tableView, nint section)
            {
                return _footerTitle?.Invoke(Sections.ElementAt((int)section));
            }

            private List<IGrouping<TKey, TItem>> GetSections(IEnumerable<TItem> items)
            {
                var sections = items.GroupBy(_getKey).ToList();
                if (_keySort == null)
                    sections = sections.OrderBy(g => g.Key).ToList();
                else
                    sections.Sort((first, second) => _keySort.Compare(first.Key, second.Key));

                return sections;
            }

            private List<IGrouping<TKey, TItem>> Sections => GetSections(Items);
        }
    }
}