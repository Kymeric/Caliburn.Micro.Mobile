using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Threading;
using Caliburn.Micro.Mobile.iOS.Bindings;
using Caliburn.Micro.Mobile.iOS.Controllers;
using SimpleBindings.ViewModels;
using UIKit;

namespace SimpleBindings.iOS
{
	partial class LoginViewController : UIViewController<LoginViewModel>
    {
		public LoginViewController (IntPtr handle)
            : base (handle)
		{
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();
	        Username.Bind(ViewModel, vm => vm.Username);
	        Password.Bind(ViewModel, vm => vm.Password);
	        Authenticate.Bind(ViewModel, vm => vm.AuthenticateAsync);
	    }

	    private Timer _timer;
    }
}
