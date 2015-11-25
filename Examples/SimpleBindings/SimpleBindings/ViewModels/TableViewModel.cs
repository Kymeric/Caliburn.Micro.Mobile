using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SimpleBindings.ViewModels
{
    public class TableViewModel : Screen
    {
        public TableViewModel()
        {
            Items = new BindableCollection<DateTime>(Enumerable.Range(0, 10).Select(i => DateTime.Today.AddSeconds(i)));
        }

        public BindableCollection<DateTime> Items
        {
            get { return _items; }
            set
            {
                if (value == _items)
                    return;
                _items = value;
                NotifyOfPropertyChange();
            }
        }

        private BindableCollection<DateTime> _items;

        public DateTime SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value == _selectedItem)
                    return;
                _selectedItem = value;
                NotifyOfPropertyChange();
            }
        }
        private DateTime _selectedItem;

    }
}
