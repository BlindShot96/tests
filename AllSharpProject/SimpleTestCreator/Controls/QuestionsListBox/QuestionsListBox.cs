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
using TestLibrary;

namespace SimpleTestCreator.Controls.QuestionsListBox
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SimpleTestCreator.Controls"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SimpleTestCreator.Controls;assembly=SimpleTestCreator.Controls"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:_QuestionsListBox/>
    ///
    /// </summary>
    public class QuestionsListBox :ListBox
    {
        private Dictionary<QuestionType, string> QTypeToString = new Dictionary<QuestionType, string>
        { 
           {QuestionType.SingleChoice , "SingleChoice"},
           {QuestionType.MultiChoice , "MultiChoice"},
           {QuestionType.TextQuestion , "TextQuestion"}         
        };

        private ObservableCollection<QuestionListBoxItem> Items = new ObservableCollection<QuestionListBoxItem>();

        private List<QuestionBase> Questions = new List<QuestionBase>();

        static QuestionsListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QuestionsListBox), new FrameworkPropertyMetadata(typeof(QuestionsListBox)));
        }

        public QuestionsListBox(List<QuestionBase> questions):base()
        {
            LoadQuestions(questions);
            this.ItemsSource = this.Items;
        }

        public QuestionsListBox()
            : base()
        { this.ItemsSource = this.Items; }

        public void LoadQuestions(List<QuestionBase> questions)
        {
            this.Questions = questions;
            this.Questions.OrderBy(q => q.Number);

            foreach (var item in this.Questions)
            {
                string type = QTypeToString[item.Type];
                QuestionListBoxItem i = new QuestionListBoxItem(item.Data.Text, type);
                this.Items.Add(i);

                this.Items.Add(i);
            }
        }
    }
}
