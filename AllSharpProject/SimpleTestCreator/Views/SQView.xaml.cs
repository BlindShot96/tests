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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleTestCreator.UserControls.DataElements;
using SimpleTestCreator.ViewModels2;
using TestLibrary;

namespace SimpleTestCreator
{
    /// <summary>
    /// Логика взаимодействия для SQView.xaml
    /// </summary>
    public partial class SQView : UserControl
    {
        private SingleChoiceQuestionViewModel viewModel;
        public SingleChoiceQuestionViewModel ViewModel
        {
            get
            {
                if (viewModel == null)
                {
                    viewModel = this.DataContext as SingleChoiceQuestionViewModel;
                }
                return viewModel;
            }
            set
            {
                viewModel = value;
                this.DataContext = value;
            }
        }

        public SQView()
        {
            InitializeComponent();
        }
        public SQView(SingleChoiceQuestionViewModel vm):this()
        {
            this.DataContext = vm;
           
        }
        public SQView(QSingleChoice q) : this(new SingleChoiceQuestionViewModel(q))
        {
        }


        private void IsTrueAnswerRB_OnClick(object sender, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton) sender;
            foreach (Answer item in AnswersListBox.Items)
            {
                item.IsTrue = false;
            }
            btn.IsChecked = true;
        }

        private void AnswersListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AnswersListBox.SelectedItem != null)
            {
                this.ViewModel.ShowAnswerCommand.Execute((Answer)AnswersListBox.SelectedItem);
            }
        }

    }
}
