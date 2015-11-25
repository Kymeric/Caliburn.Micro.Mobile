using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Caliburn.Micro.Mobile.Expressions;
using UIKit;

namespace Caliburn.Micro.Mobile.iOS.Bindings
{
    public class UITextFieldBinding<TViewModel> : Binding<TViewModel>
        where TViewModel : INotifyPropertyChanged
    {
        private readonly UITextField _field;

        public UITextFieldBinding(TViewModel viewModel, UITextField field, Expression<Func<TViewModel, string>> textProperty)
            : base(viewModel)
        {
            _field = field;
            var propName = textProperty.GetMemberInfo().Name;

            //VM -> Field
            var textGetter = textProperty.Compile();
            _field.Text = textGetter(ViewModel);
            Bind(ViewModel, propName, () => _field.Text = textGetter(ViewModel));

            //Field -> VM
            var textSetterExpression = ExpressionFactory.SetProperty(viewModel, textProperty);
            if (textSetterExpression != null)
            {
                var textSetter = textSetterExpression.Compile();
                _field.EditingChanged += (sender, args) =>
                {
                    textSetter(_field.Text);
                };
            }
        }
    }
}