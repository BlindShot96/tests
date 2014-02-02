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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleTestCreator.Controls.QuestionsListBox;
using TestLibrary;

namespace SimpleTestCreator
{
    /// <summary>
    /// Логика взаимодействия для QuestionsListBox.xaml
    /// </summary>
    public partial class QuestionsListBox : UserControl
    {
        private Dictionary<QuestionType, string> QTypeToString = new Dictionary<QuestionType,string>
        { 
           {QuestionType.SingleChoice , "SingleChoice"},
           {QuestionType.MultiChoice , "MultiChoice"},
           {QuestionType.TextQuestion , "TextQuestion"}         
        };

        private ObservableCollection<QuestionListBoxItem> Items = new ObservableCollection<QuestionListBoxItem>();

        private List<QuestionBase> Questions = new List<QuestionBase>();

        public QuestionsListBox(List<QuestionBase> questions)
        {
            InitializeComponent();
            LoadQuestions(questions);
        }

        public void LoadQuestions(List<QuestionBase> questions)
        {
            this.Questions = questions;
            this.Questions.OrderBy(q => q.Number);

            foreach (var item in this.Questions)
            {
                string type = QTypeToString[item.Type];
                QuestionListBoxItem i = new QuestionListBoxItem(item.Data.Text, type);
                this.Items.Add(i);
            }

            this.QList.ItemsSource = this.Items;
           
        }
    }
}
