using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SimpleTestCreator.ViewModels2;
using TestLibrary;

namespace SimpleTestCreator.Views
{
    /// <summary>
    /// Логика взаимодействия для TestSettingsWindow.xaml
    /// </summary>
    public partial class TestSettingsWindow : Window//, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        //public Test Test
        //{ get; set; }

        //public string TestName { get; set; }
        //public string TeacherName { get; set; }
        //public string Description { get; set; }

        //public int Min5
        //{
        //    get { return min5; }
        //    set { min5 = value; PropertyChanged(this, new PropertyChangedEventArgs("Min5")); }
        //}

        //public int Min4
        //{
        //    get { return min4; }
        //    set { min4 = value; PropertyChanged(this, new PropertyChangedEventArgs("Min4")); }
        //}

        //public int Min3
        //{
        //    get { return min3; }
        //    set { min3 = value; PropertyChanged(this, new PropertyChangedEventArgs("Min3")); }
        //}

        //private int min5 = 85;
        //private int min4 = 70;
        //private int min3 = 50;


        //public TestSettingsWindow(Test test)
        //{
        //    this.Test = test;

        //    this.TestName = (string) this.Test.Settings.Name.Clone();
        //    this.TeacherName = (string) this.Test.Settings.TeacherName.Clone();
        //    this.Description = (string) this.Test.Data.Text;

        //    //[TEST]
        //    this.Min5 = 85;
        //    this.Min4 = 70;
        //    this.Min3 = 50;
        //    //[TEST]

        //    InitializeComponent();
        //    this.DataContext = this;
        //}

        public TestSettingsViewModel ViewModel { get; set; }

        public TestSettingsWindow(TestSettingsViewModel vm)
        {
            ViewModel = vm;
            this.InitializeComponent();
            this.DataContext = vm;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.SaveCommand.Execute(null);

            this.Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


    }
}
