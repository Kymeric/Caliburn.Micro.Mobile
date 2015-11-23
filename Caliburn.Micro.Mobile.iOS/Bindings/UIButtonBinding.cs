using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace Caliburn.Micro.Mobile.iOS.Bindings
{
    public class UIButtonBinding<TViewModel> : Binding<TViewModel>
        where TViewModel : INotifyPropertyChanged
    {
        public UIButtonBinding(TViewModel viewModel, UIButton button, Expression<Func<TViewModel, Func<Task>>> clickMethod)
            : base(viewModel)
        {
            EventHandler handler = (sender, args) =>
            {
                clickMethod.Compile()(viewModel)();
            };
            button.TouchUpInside += handler;
        }
    }
}
