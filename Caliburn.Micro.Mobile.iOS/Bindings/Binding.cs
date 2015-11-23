using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace Caliburn.Micro.Mobile.iOS.Bindings
{
    public abstract class Binding : IDisposable
    {
        public virtual void Dispose()
        {
            _bindings.ForEach(b => b?.Dispose());
            _bindings.Clear();
        }

        protected IDisposable Bind<TViewModel>(TViewModel viewModel, string property, Action onChanged)
            where TViewModel : INotifyPropertyChanged
        {
            var ret = new ViewModelChange(viewModel, property, onChanged);
            _bindings.Add(ret);
            return ret;
        }

        private readonly List<IDisposable> _bindings = new List<IDisposable>(); 

        private class ViewModelChange : IDisposable
        {
            readonly WeakReference<INotifyPropertyChanged> _viewModel;
            readonly PropertyChangedEventHandler _handler;

            public ViewModelChange(INotifyPropertyChanged viewModel, string property, Action onChanged)
            {
                _viewModel = new WeakReference<INotifyPropertyChanged>(viewModel);
                _handler = (s, e) =>
                {
                    if (e.PropertyName != property)
                        return;

                    onChanged();
                };
                viewModel.PropertyChanged += _handler;
            }

            public void Dispose()
            {
                INotifyPropertyChanged vm;
                if(_viewModel.TryGetTarget(out vm))
                    vm.PropertyChanged -= _handler;
            }
        }
    }

    public abstract class Binding<TViewModel> : Binding
        where TViewModel : INotifyPropertyChanged
    {
        protected Binding(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        protected TViewModel ViewModel { get; set; }
    }
}
