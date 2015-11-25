using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Caliburn.Micro.Mobile.Expressions;
using Foundation;
using UIKit;

namespace Caliburn.Micro.Mobile.iOS.Bindings
{
    public class UITableViewBinding<TViewModel, TItem> : Binding<TViewModel>
        where TViewModel : INotifyPropertyChanged
    {
        public UITableViewBinding(TViewModel viewModel, UITableView view,
                                  Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty,
                                  Action<TItem, UITableViewCell> cellBinding)
            : base(viewModel)
        {
            view.Source = new BoundTableSource(viewModel, view, itemsProperty, cellBinding);
        }

        protected UITableViewBinding(TViewModel viewModel)
            : base(viewModel)
        {

        }

        protected class BoundTableSource : UITableViewSource
        {
            protected readonly UITableView View;
            protected readonly BindableCollection<TItem> Items;
            private readonly Action<TItem> _onSelection;
            private readonly Action<TItem, UITableViewCell> _cellBinding;

            public BoundTableSource(TViewModel viewModel, UITableView view,
                                    Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty,
                                    Action<TItem, UITableViewCell> cellBinding)
            {
                View = view;
                Items = itemsProperty.Compile()(viewModel);
                _cellBinding = cellBinding;

                var selectedProperty =
                    viewModel.GetType()
                             .GetProperty($"Selected{TextHelpers.Singularize(itemsProperty.GetMemberInfo().Name)}");
                if (selectedProperty != null)
                {
                    var selectedSetter = ExpressionFactory.SetProperty<TViewModel, TItem>(viewModel, selectedProperty);
                    if (selectedSetter != null)
                    {
                        _onSelection = selectedSetter.Compile();
                    }
                }
                Items.CollectionChanged += OnCollectionChanged;
            }

            protected virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    View.InsertRows(Enumerable.Range(e.NewStartingIndex, e.NewItems.Count)
                        .Select(i => NSIndexPath.FromRowSection(i, 0)).ToArray(),
                        UITableViewRowAnimation.None);
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    View.DeleteRows(Enumerable.Range(e.OldStartingIndex, e.OldItems.Count)
                        .Select(i => NSIndexPath.FromRowSection(i, 0)).ToArray(),
                        UITableViewRowAnimation.None);
                }
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    View.ReloadRows(Enumerable.Range(e.OldStartingIndex, e.OldItems.Count)
                        .Select(i => NSIndexPath.FromRowSection(i, 0)).ToArray(),
                        UITableViewRowAnimation.None);
                }
                else
                {
                    Debug.WriteLine($"Reloading Table Data for action: {e.Action}");
                    View.ReloadData();
                }
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = View.DequeueReusableCell(CellIdentifier);
                var item = GetItem(indexPath);
                if (cell == null)
                    cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
                BindItemCell(item, cell);
                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return Items.Count;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                if (_onSelection != null)
                {
                    var item = GetItem(indexPath);
                    _onSelection(item);
                }
            }

            protected virtual TItem GetItem(NSIndexPath indexPath)
            {
                return Items[indexPath.Row];
            }

            protected virtual void BindItemCell(TItem item, UITableViewCell cell)
            {
                _cellBinding(item, cell);
            }
        }
        protected static string CellIdentifier => typeof(TItem).Name + "Cell";

    }
}
