using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using TestLibrary.Helpers;
using System.Collections.ObjectModel;

namespace TestLibrary
{
    /// <summary>
    /// базовый класс для всех видов тестов
    /// </summary>
    [XmlInclude(typeof(Test))]
    public abstract class TestBase : NotificationObject,IBase
    {
        #region public Fields

        protected MediaData data = new MediaData();

        /// <summary>
        /// сведения о тесте 
        /// </summary>
        [XmlElement("MediaData")]
        public MediaData Data
        {
            get
            {
                return data;
            }
            set { this.data = value; RaisePropertyChanged(() => this.data); }
        }

        private TestSettings _settings = new TestSettings();

        [XmlElement("Settings")]
        public TestSettings Settings
        {
            get 
            {
                return _settings; 
            }
            set 
            {
                this._settings = value;
                RaisePropertyChanged(() => this._settings);
            }
        }

        [XmlIgnore]
        public int MaxQuestionsBall 
        {    get 
             {
               int res = 0;
               foreach (QuestionBase q in this._questions)
               {
                 res += q.MaxBall;
               }
               return res;
             } 
        }

        protected ObservableCollection<QuestionBase> _questions = new ObservableCollection<QuestionBase>();


        /// <summary>
        /// список вопросов
        /// </summary>
        [XmlArray("Questions"),
        XmlArrayItem("Question")]
        public ObservableCollection<QuestionBase> Questions
        {
            get { if (_questions == null) { return new ObservableCollection<QuestionBase>(); } return _questions; }
            set
            {
                if (_questions != value)
                {
                    _questions = value;
                    RaisePropertyChanged(() => _questions);
                }
            }
        }

        public QuestionBase this[string id]
        {
            get
            {
                return this._questions.First(i => i.ID.Equals(id));
            }
            set 
            {
                this._questions[this._questions.IndexOf(this._questions.First(k => k.ID.Equals(id)))] = value;
            }
        }
        public QuestionBase this[int num]
        {
            get { return this._questions[num]; }
            set
            {
                this._questions[num] = value;
            }
        }

        public void RemoveFile(QuestionBase q)
        {
            this._questions.Remove(q);
        }
        public void RemoveFile(string id)
        {
            RemoveFile(this._questions.First(i => i.ID.Equals(id)));
        }
        public void RemoveFile(int num)
        {
            this._questions.RemoveAt(num);
        }

        public void AddQuestion(QuestionBase q, bool EnableReplace)
        {
            if (this._questions.Contains(q, new KeyEqualityComparer<QuestionBase>(k => k.ID)) == true)
            {
                if (EnableReplace == true)
                {
                    this._questions[this._questions.IndexOf(this._questions.First(k => k.ID.Equals(q.ID)))] = q;
                }
                else { throw new ArgumentOutOfRangeException("такой вопрос уже есть"); }
            }
            else
            {
                this._questions.Add(q);
            }
        }
        public void AddQuestion(QuestionBase q)
        {
            AddQuestion(q, false);
        }

        #endregion
    }
}
