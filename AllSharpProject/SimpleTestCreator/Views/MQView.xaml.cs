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
using SimpleTestCreator.ViewModels2;
using TestLibrary;

namespace SimpleTestCreator
{
    /// <summary>
    /// Логика взаимодействия для MQView.xaml
    /// </summary>
    public partial class MQView : UserControl
    {
        private MultiChoiceQuestionViewModel viewModel;
        public MultiChoiceQuestionViewModel ViewModel
        {
            get
            {
                if (viewModel == null)
                {
                    viewModel = this.DataContext as MultiChoiceQuestionViewModel;
                }
                return viewModel;
            }
            set
            {
                viewModel = value;
                this.DataContext = value;
            }
        }

        public MQView()
        {
            InitializeComponent();
        }

        public MQView(MultiChoiceQuestionViewModel vm) : this()
        {
            this.DataContext = vm;
        }

        public MQView(QMultiChoice q) : this(new MultiChoiceQuestionViewModel(q))
        {
        }

        private void AnswersListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AnswersListBox.SelectedItem != null)
            {
                this.ViewModel.ShowAnswerCommand.Execute((Answer)AnswersListBox.SelectedItem);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel != null)
            {
                if (ModeComboBox.SelectedIndex == 0)
                {
                    this.ViewModel.QuestionMode = QuestionMode.ByNumber;
                }
                if (ModeComboBox.SelectedIndex == 1)
                {
                    this.ViewModel.QuestionMode = QuestionMode.ByBalls;
                    this.AnswersListBox.ItemContainerStyle = (Style)(App.Current.Resources["MultiChoiceBallAnswersBoxStyle"]);

                }
            }
        }


        private void UpAnswerBallButton_OnClick(object sender, RoutedEventArgs e)
        {
              
        }

        private void DownAnswerBallButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
