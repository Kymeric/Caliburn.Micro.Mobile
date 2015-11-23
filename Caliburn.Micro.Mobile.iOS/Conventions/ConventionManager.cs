using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Caliburn.Micro.Mobile.Expressions;
using Caliburn.Micro.Mobile.iOS.Bindings;
using UIKit;

namespace Caliburn.Micro.Mobile.iOS.Conventions
{
    public static class ConventionManager
    {
        public static void Apply<TController, TViewModel>(TController controller, TViewModel viewModel)
            where TController : UIResponder
            where TViewModel : INotifyPropertyChanged
        {
            foreach (var match in viewModel.GetType()
                                           .GetProperties()
                                           .Join(controller.GetType().GetProperties(), vmp => vmp.Name, cp => cp.Name,
                                               (vmp, cp) => new {ViewModelProperty = vmp, ControllerProperty = cp}))
            {
                if (match.ControllerProperty.PropertyType == typeof (UITextField) &&
                    match.ViewModelProperty.PropertyType == typeof (string))
                {
                    var uiTextField = (UITextField)match.ControllerProperty.GetValue(controller);
                    uiTextField.Bind(viewModel, 
                        ExpressionFactory.GetProperty<TViewModel, string>(viewModel, match.ControllerProperty));
                }
            }
        }
    }
}
