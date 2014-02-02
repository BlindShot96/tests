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
using System.Windows.Shapes;
using SimpleTestCreator.Controls.QuestionsListBox;
using TestLibrary;
using Microsoft.Win32;

namespace SimpleTestCreator
{
    /// <summary>
    /// Логика взаимодействия для MainTestWindow.xaml
    /// </summary>
    public partial class MainTestWindow : Window
    {

        public MainTestWindow()
        {
            InitializeComponent();
            TestLibrary.Helpers.SaveMaster.Save(DataAccess.DataManager._Debug_GetTest(), @"C:\Users\пётр\Desktop\test-1.xml", TestLibrary.Helpers.SerializationMethod.XML);
            DataContext = new ViewModels.MainWindowViewModel();
        }


    }
}
