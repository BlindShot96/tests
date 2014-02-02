using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using SimpleTestCreator.ViewModels2;
using TestLibrary;

namespace SimpleTestCreator
{
   
    /// <summary>
    /// Логика взаимодействия для MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {

        private MainViewModel _viewModel;
        public MainViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public MainWindow2()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            this.DataContext = ViewModel;

            _viewModel.OnUpdateQuestionsListHandler += (sender, args) =>
            {
                this.QuestionListView.ItemsSource = _viewModel.QuestionViewModels;
                if (sender is int)
                {
                    this.QuestionListView.SelectedIndex = (int)sender;
                }
            };

            _viewModel.OnUpdateShowingQuestionHandler += (sender, args) =>
            {
                this.QuestionContentControl.Content =
                    QuestionControlFactory.GetQuestionControl(this.ViewModel.ShowingQuestion);
            };

        }


        private void AddQuestionMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            if (menu == null)
            {
                return;
            }

            QuestionType type = QuestionType.Null;;
            if (menu.Uid.Equals("SQ"))
            {
              type = QuestionType.SingleChoice;  
            }
            if (menu.Uid.Equals("MQ"))
            {
                type = QuestionType.MultiChoice;
            }
            if (menu.Uid.Equals("TQ"))
            {
                type = QuestionType.TextQuestion;
            }

            if (type != QuestionType.Null && this.ViewModel.AddQuestion2Command.CanExecute(type)==true)
            {
               this.ViewModel.AddQuestion2Command.Execute(type);
                this.QuestionListView.SelectedIndex =
                    ((ObservableCollection<QuestionViewModelBase>) this.QuestionListView.ItemsSource).Count-1;
            }
        }
    }
}
