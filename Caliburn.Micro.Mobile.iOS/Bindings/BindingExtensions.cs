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
        /// <summary>
        /// Binds a<seealso cref="UITextField"/> to a ViewModel
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel Type</typeparam>
        /// <param name="field"></param>
        /// <param name="viewModel"></param>
        /// <param name="textProperty">The ViewModel property to bind to the Text property of the field</param>
        /// <returns></returns>
        public static UITextFieldBinding<TViewModel> Bind<TViewModel>(this UITextField field, TViewModel viewModel,
                                                          Expression<Func<TViewModel, string>> textProperty)
            where TViewModel : INotifyPropertyChanged
        {
            return new UITextFieldBinding<TViewModel>(viewModel, field, textProperty);
        }

        /// <summary>
        /// Binds a <seealso cref="UIButton"/> to a ViewModel
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel Type</typeparam>
        /// <param name="button"></param>
        /// <param name="viewModel"></param>
        /// <param name="clickMethod">The method to be called when the button is clicked</param>
        /// <returns></returns>
        public static UIButtonBinding<TViewModel> Bind<TViewModel>(this UIButton button, TViewModel viewModel,
                                                                   Expression<Func<TViewModel, Func<Task>>> clickMethod)
            where TViewModel : INotifyPropertyChanged
        {
            return new UIButtonBinding<TViewModel>(viewModel, button, clickMethod);
        }

        /// <summary>
        /// Binds a <seealso cref="UITableView"/> to a ViewModel
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel Type</typeparam>
        /// <typeparam name="TItem">Item Type</typeparam>
        /// <param name="view">The View</param>
        /// <param name="viewModel">The ViewModel</param>
        /// <param name="itemsProperty">The Items Property</param>
        /// <param name="cellBinding">The logic for binding a <seealso cref="TItem"/> to a <seealso cref="UITableViewCell"/></param>
        /// <returns></returns>
        public static UITableViewBinding<TViewModel, TItem> Bind<TViewModel, TItem>(this UITableView view,
                                                                                    TViewModel viewModel,
                                                                                    Expression<Func<TViewModel, BindableCollection<TItem>>> itemsProperty,
                                                                                    Action<TItem, UITableViewCell> cellBinding)
            where TViewModel : INotifyPropertyChanged
        {
            return new UITableViewBinding<TViewModel, TItem>(viewModel, view, itemsProperty, cellBinding);
        }

        /// <summary>
        /// Binds a <seealso cref="UITableView"/> to a ViewModel with item grouping/sections
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel Type</typeparam>
        /// <typeparam name="TItem">Item Type</typeparam>
        /// <typeparam name="TKey">Key Type</typeparam>
        /// <param name="view">The View</param>
        /// <param name="viewModel">The ViewModel</param>
        /// <param name="itemsProperty">The Items Property</param>
        /// <param name="cellBinding">The logic for binding a <seealso cref="TItem"/> to a <seealso cref="UITableViewCell"/></param>
        /// <param name="getKey">Determines how to group/section the items</param>
        /// <param name="headerTitle">How to build/format the section header for each group/section</param>
        /// <param name="footerTitle">How to build/format the section fooder for each group/section</param>
        /// <param name="keySort">Custom sorting for groups/sections</param>
        /// <returns></returns>
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