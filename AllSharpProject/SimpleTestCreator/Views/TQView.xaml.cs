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

namespace SimpleTestCreator.Views
{
    /// <summary>
    /// Логика взаимодействия для TQView.xaml
    /// </summary>
    public partial class TQView : UserControl
    {
        private TextAnswerQuestionViewModel viewModel;
        public TextAnswerQuestionViewModel ViewModel
        {
            get
            {
                if (viewModel == null)
                {
                    viewModel = this.DataContext as TextAnswerQuestionViewModel;
                }
                return viewModel;
            }
            set
            {
                viewModel = value;
                this.DataContext = value;
            }
        }

        public TQView()
        {
            InitializeComponent();
        }

        public TQView(TextAnswerQuestionViewModel vm):this()
        {
            this.DataContext = vm;
            //this.MediaDataContentPresenter.Content 
            //    = new DataElementsControl(this.ViewModel.DataElementsViewModel);
        }
        public TQView(TestLibrary.QTextChoice q) : this(new TextAnswerQuestionViewModel(q))
        {
        }


    }
}
