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
            Items = new BindableCollection<string>(Enumerable.Range(0, 10).Select(i => $"Item{i}"));
        }

        public BindableCollection<string> Items
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

        private BindableCollection<string> _items;

        public string SelectedItem
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
        private string _selectedItem;

    }
}
