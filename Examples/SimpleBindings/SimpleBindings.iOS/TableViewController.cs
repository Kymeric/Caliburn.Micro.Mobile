using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Threading;
using Caliburn.Micro;
using Caliburn.Micro.Mobile.iOS.Bindings;
using Caliburn.Micro.Mobile.iOS.Controllers;
using SimpleBindings.ViewModels;
using UIKit;

namespace SimpleBindings.iOS
{
    partial class TableViewController : UITableViewController<TableViewModel>
    {
        public TableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Bind as simple list
            //TableView.Bind(ViewModel, vm => vm.Items, (item, cell) => cell.TextLabel.Text = item.ToString());
            //Bind as grouped list
            TableView.Bind(ViewModel, vm => vm.Items, (item, cell) => cell.TextLabel.Text = item.ToString(), i => i.Second % 10, g => g.Key.ToString());

            _timer = new Timer(AddNewItem, null, 3000, 1000);
        }

        private void AddNewItem(object state)
        {
            ViewModel.Items.Add(DateTime.Now);
            while (ViewModel.Items.Count > 5)
            {
                ViewModel.Items.RemoveAt(0);
            }
        }

        private Timer _timer;
    }
}
