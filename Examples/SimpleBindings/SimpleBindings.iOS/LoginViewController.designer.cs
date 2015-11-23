// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SimpleBindings.iOS
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton Authenticate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField Password { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField Username { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (Authenticate != null) {
				Authenticate.Dispose ();
				Authenticate = null;
			}
			if (Password != null) {
				Password.Dispose ();
				Password = null;
			}
			if (Username != null) {
				Username.Dispose ();
				Username = null;
			}
		}
	}
}
