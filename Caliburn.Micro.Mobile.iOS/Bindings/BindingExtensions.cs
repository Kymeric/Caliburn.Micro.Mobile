using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace Caliburn.Micro.Mobile.iOS.Bindings
{
    public static class BindingExtensions
    {
        public static UITextFieldBinding<TViewModel> Bind<TViewModel>(this UITextField field, TViewModel viewModel,
                                                          Expression<Func<TViewModel, string>> textProperty)
            where TViewModel : INotifyPropertyChanged
        {
            return new UITextFieldBinding<TViewModel>(viewModel, field, textProperty);
        }

        public static UIButtonBinding<TViewModel> Bind<TViewModel>(this UIButton button, TViewModel viewModel,
                                                                   Expression<Func<TViewModel, Func<Task>>> clickMethod)
            where TViewModel : INotifyPropertyChanged
        {
            return new UIButtonBinding<TViewModel>(viewModel, button, clickMethod);
        }

        public static UITableViewBinding<TViewModel, TItem> Bind<TViewModel, TItem>(this UITableView view,
                                                                                    TViewModel viewModel,
                                                                                    Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty,
                                                                                    Action<TItem, UITableViewCell> cellBinding)
            where TViewModel : INotifyPropertyChanged
        {
            return new UITableViewBinding<TViewModel, TItem>(viewModel, view, itemsProperty, cellBinding);
        }

        public static UITableViewGroupedBinding<TViewModel, TItem, TKey> Bind<TViewModel, TItem, TKey>(this UITableView view,
                                                                                    TViewModel viewModel,
                                                                                    Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty,
                                                                                    Action<TItem, UITableViewCell> cellBinding,
                                                                                    Func<TItem, TKey> getKey,
                                                                                    Func<IGrouping<TKey, TItem>, string> headerTitle = null,
                                                                                    Func<IGrouping<TKey, TItem>, string> footerTitle = null,
                                                                                    IComparer<TKey> keySort = null)
            where TViewModel : INotifyPropertyChanged
        {
            return new UITableViewGroupedBinding<TViewModel, TItem, TKey>(viewModel, view, itemsProperty, cellBinding, getKey, headerTitle, footerTitle, keySort);
        }
    }
}