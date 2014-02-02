using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleTestCreator.Views
{
    /// <summary>
    /// Логика взаимодействия для ChooseDialog.xaml
    /// </summary>
    public partial class ChooseDialog : Window
    {
        private List<object> _items = new List<object>();

        public ChooseDialog(List<object> items)
        {
            InitializeComponent();
            if (items != null)
            {
                this.Items = items;
            }
        }
        public ChooseDialog() : this(new List<object>())
        {
        }

        public List<object> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public object SelectedItem
        {
            get; set;
        }

        public ICommand OkCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    this.DialogResult = true;
                    this.Close();
                }, o =>
                {
                   return this.ItemsListBox.SelectedItems.Count != 0;
                });
        }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    this.DialogResult = false;
                    this.Close();  
                }, o => { return true; });
            }
        }
    }
}
