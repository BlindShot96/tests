using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TestLibrary.Helpers;
using TestLibrary.Media;

namespace TestLibrary
{
     public abstract class NewTestBase : NotificationObject
    {


        private ObservableCollection<QuestionBase> _questions = new ObservableCollection<QuestionBase>();

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

        public void RemoveQuestion(QuestionBase q)
        {
            this._questions.Remove(q);
        }
        public void RemoveQuestion(string id)
        {
            RemoveQuestion(this._questions.First(i => i.ID.Equals(id)));
        }
        public void RemoveQuestion(int num)
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

    }
}
