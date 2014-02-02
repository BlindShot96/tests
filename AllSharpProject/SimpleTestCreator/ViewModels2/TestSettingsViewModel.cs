using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using TestLibrary;

namespace SimpleTestCreator.ViewModels2
{
    public class TestSettingsViewModel : ViewModelBase
    {
        public TestSettings DataSettings { get; set; }
        private Test original_test;

        public int Attemts
        {
            get { return this.DataSettings.Attemts; }
            set { this.DataSettings.Attemts = value; OnPropertyChanged("Attemts"); } 
        }

        public string TestName { 
            get { return this.DataSettings.Name; }
            set { this.DataSettings.Name = value; OnPropertyChanged("TestName"); }
        }

        public string TeacherName
        {
            get { return this.DataSettings.TeacherName; }
            set
            {
                this.DataSettings.TeacherName = value;
                OnPropertyChanged("TeacherName");
            }
        }

        public string Description { get; set; }

        //public int Min5
        //{
        //    get { return DataSettings. }
        //    set { min5 = value; base.OnPropertyChanged("Min5"); }
        //}

        //public int Min4
        //{
        //    get { return min4; }
        //    set { min4 = value; base.OnPropertyChanged("Min4"); }
        //}

        //public int Min3
        //{
        //    get { return min3; }
        //    set { min3 = value; base.OnPropertyChanged("Min3"); }
        //}

        //private int min5 = 85;
        //private int min4 = 70;
        //private int min3 = 50;


        public TestSettingsViewModel(Test test)
        {
            this.DataSettings = (TestSettings)test.Settings.Clone();
            this.original_test = test;
            this.Description = this.original_test.Data.Text;
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    this.original_test.Settings = this.DataSettings;
                    this.original_test.Data.Text = Description;
                });
            }
        }
    }
}
