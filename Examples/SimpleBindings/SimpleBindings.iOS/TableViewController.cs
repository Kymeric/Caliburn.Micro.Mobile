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
	partial class TableViewController : UITableViewController<TableViewModel>
	{
		public TableViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.Bind(ViewModel, vm => vm.Items);
            _timer = new Timer(AddNewItem, null, 3000, 1000);
        }

	    private void AddNewItem(object state)
	    {
	        while (ViewModel.Items.Count > 5)
	        {
	            ViewModel.Items.RemoveAt(0);
	        }

	        ViewModel.Items.Add(DateTime.Now.ToString());
	    }

	    private Timer _timer;
    }
}
