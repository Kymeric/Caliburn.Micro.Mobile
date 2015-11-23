using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Caliburn.Micro.Mobile.iOS.Controllers
{
    public abstract class UITableViewController<TViewModel> : UITableViewController
    {
        protected UITableViewController(IntPtr handle)
            : base(handle)
        {
            ViewModel = (TViewModel) ViewModelLocator.LocateForView(this);
        }

        public TViewModel ViewModel { get; set; }
    }
}