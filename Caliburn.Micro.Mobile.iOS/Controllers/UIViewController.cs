using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Caliburn.Micro.Mobile.iOS.Controllers
{
    public abstract class UIViewController<TViewModel> : UIViewController
    {
        protected UIViewController(IntPtr handle)
            : base(handle)
        {
            ViewModel = (TViewModel)ViewModelLocator.LocateForView(this);
        }

        public TViewModel ViewModel { get; set; }
    }
}
