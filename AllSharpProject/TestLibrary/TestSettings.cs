using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestLibrary
{
    [XmlRoot("Settings")]
    public class TestSettings:ICloneable,INotifyPropertyChanged //: TestLibrary.Helpers.NotificationObject
    {

        private string name = "";

        /// <summary>
        /// название теста
        /// </summary>
        [XmlAttribute("Name")]
        public string Name {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        //{
        //    get { return Name; }
        //    set { Name = value; RaisePropertyChanged(() => Name); }
        //}

        private string teacher_name = "";
        /// <summary>
        /// имя учителя
        /// </summary>
        [XmlAttribute("TeacherName")]
        public string TeacherName
        {
            get { return teacher_name; }
            set { teacher_name = value; OnPropertyChanged("TeacherName"); }
        }

        private int max_mark = 1;

        /// <summary>
        /// максимальная оценка
        /// </summary>
        [XmlAttribute("MaxMark")]
        public int MaxMark {
            get { return max_mark; }
            set { max_mark = value; OnPropertyChanged("MaxMark"); }
        }

        //{
        //    get { return MaxMark; }
        //    set { MaxMark = value; RaisePropertyChanged(() => MaxMark); }
        //}

        private int min_mark = 0;
        /// <summary>
        /// минимальная оценка
        /// </summary>
        [XmlAttribute("MinMark")]
        public int MinMark {
            get { return min_mark; }
            set { min_mark = value; OnPropertyChanged("MinMark"); }
        }

        private int attemts = 1;

        //{
        //    get { return MinMark; }
        //    set { MinMark = value; RaisePropertyChanged(() => MinMark); }
        //}
        [XmlAttribute("Attemts")]
        public int Attemts {
            get { return attemts; }
            set { attemts = value; OnPropertyChanged("Attemts"); }
        }

        private int minute_time = 0;
        /// <summary>
        /// время на тест в минутах
        /// </summary>
        [XmlAttribute("MinuteTime")]
        public int MinuteTime {
            get { return minute_time; }
            set { minute_time = value; OnPropertyChanged("MinuteTime"); }
        }

        //{
        //    get { return MinuteTime; }
        //    set { MinuteTime = value; RaisePropertyChanged(() => MinuteTime); }
        //}

        [XmlAttribute("Min5")]
        public int Min5
        {
            get { return min5; }
            set { min5 = value; OnPropertyChanged("Min5"); }
        }

        [XmlAttribute("Min4")]
        public int Min4
        {
            get { return min4; }
            set { min4 = value;  OnPropertyChanged("Min4"); }
        }

        [XmlAttribute("Min3")]
        public int Min3
        {
            get { return min3; }
            set { min3 = value; OnPropertyChanged("Min3"); }
        }

        private int min5 = 85;
        private int min4 = 70;
        private int min3 = 50;

        public object Clone()
        {
            var res = new TestSettings();
            res.Attemts = this.Attemts;
            res.MaxMark = this.MaxMark;
            res.MinMark = this.MinMark;
            res.MinuteTime = this.MinMark;
            res.Name = this.Name;
            res.TeacherName = this.TeacherName;
            res.Min5 = this.Min5;
            res.Min4 = this.Min4;
            res.Min3 = this.Min3;
            
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
