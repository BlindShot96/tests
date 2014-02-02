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
using TestLibrary;

namespace SimpleTestCreator
{
    /// <summary>
    /// Логика взаимодействия для QuestionTypeChooseDialog.xaml
    /// </summary>
    public partial class QuestionTypeChooseDialog : Window
    {
        public List<QuestionType> Types;
        public QuestionType ChoosenType { get; private set; }

        private List<string> _stringTypes
        {
            get
            {
                List<string> res = new List<string>();
                if (Types != null)
                {
                    foreach (QuestionType type in Types)
                    {
                        string str = "";
                        switch (type)
                        {
                                case  QuestionType.SingleChoice:
                            {
                                str = "Одиночный выбор";
                                break;
                            }

                                case QuestionType.MultiChoice:
                            {
                                str = "Множественный выбор";
                                break;
                            }
                                case QuestionType.TextQuestion:
                            {
                                str = "Ответ в виде строки";
                                break;
                            }
                        }

                        res.Add(str);
                    }
                }
                return res;
            }
        }

        public QuestionTypeChooseDialog(List<QuestionType> types)
        {
            InitializeComponent();
            this.Types = types;
            this.TypesListBox.ItemsSource = this._stringTypes;
        }

        private void OkBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.TypesListBox.SelectedItems.Count != 0)
            {
                this.ChoosenType = this.Types[TypesListBox.SelectedIndex];
                this.DialogResult = true;
                this.Close();
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void TypesListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.TypesListBox.SelectedItem != null)
            {
                this.ChoosenType = this.Types[TypesListBox.SelectedIndex];
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
