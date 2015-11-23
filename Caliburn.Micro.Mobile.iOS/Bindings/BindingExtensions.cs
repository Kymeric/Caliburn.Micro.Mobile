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

        public static UITableViewBinding<TViewModel, TItem> Bind<TViewModel, TItem>(this UITableView view,
                                                                                    TViewModel viewModel,
                                                                                    Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty)
            where TViewModel : INotifyPropertyChanged
        {
            return new UITableViewBinding<TViewModel, TItem>(viewModel, view, itemsProperty);
        }

        public static UIButtonBinding<TViewModel> Bind<TViewModel>(this UIButton button, TViewModel viewModel,
                                                                   Expression<Func<TViewModel, Func<Task>>> clickMethod)
            where TViewModel : INotifyPropertyChanged
        {
            return new UIButtonBinding<TViewModel>(viewModel, button, clickMethod);
        }
    }
}