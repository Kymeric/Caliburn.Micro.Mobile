using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
                                  BindingDirection direction = BindingDirection.Both)
            : base(viewModel)
        {
            view.Source = new BoundTableSource(viewModel, view, itemsProperty);
        }

        private class BoundTableSource : UITableViewSource
        {
            private readonly TViewModel _viewModel;
            private readonly UITableView _view;
            private readonly BindableCollection<TItem> _items;
            private readonly Action<TItem> _onSelection;

            public BoundTableSource(TViewModel viewModel, UITableView view,
                                    Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty)
            {
                _viewModel = viewModel;
                _view = view;
                _items = itemsProperty.Compile()(viewModel);
                var selectedProperty = viewModel.GetType().GetProperty($"Selected{TextHelpers.Singularize(itemsProperty.GetMemberInfo().Name)}");
                if (selectedProperty != null)
                {
                    var selectedSetter = ExpressionFactory.SetProperty<TViewModel, TItem>(viewModel, selectedProperty);
                    if (selectedSetter != null)
                    {
                        _onSelection = selectedSetter.Compile();
                    }
                }
                _items.CollectionChanged += OnCollectionChanged;
            }

            private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                //TODO: Do this better
                _view.ReloadData();
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = _view.DequeueReusableCell(CellIdentifier);
                var item = _items[indexPath.Row];
                if (cell == null)
                    cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
                cell.TextLabel.Text = item.ToString();
                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return _items.Count;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                _onSelection?.Invoke(_items[indexPath.Row]);
            }
        }

        private static string CellIdentifier => typeof (TItem).Name + "Cell";
    }
}
