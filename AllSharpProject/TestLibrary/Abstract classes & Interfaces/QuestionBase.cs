using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using TestLibrary.ClientEdit;


namespace TestLibrary
{

    public enum QuestionType
    {

        [StringValue("#Null")]
        Null,

        [StringValue("SingleChoice")]
        SingleChoice,

        [StringValue("MultiChoice")]
        MultiChoice,

        [StringValue("TextQuestion")]
        TextQuestion ,

        [StringValue("LikertQuestion")]
        LikertQuestion
    }

    /// <summary>
    /// абстрактный класс - вопросы
    /// </summary>
    [XmlInclude(typeof(QSingleChoice))]
    [XmlInclude(typeof(QMultiChoice))]
    [XmlInclude(typeof(QTextChoice))]
    [XmlInclude(typeof(QLikertAnswer))]
    [XmlRoot("Question")]
    public abstract class QuestionBase:TestLibrary.Helpers.NotificationObject, IBase
    {
        private int RndId = GetRandom();
        private static int GetRandom()
        {
            Random rnd = new Random();
            return rnd.Next(Int32.MinValue, Int32.MaxValue);
        }

        #region public Fields

        private string _name;

        /// <summary>
        /// Название вопроса
        /// </summary>
        [XmlAttribute("Name")]
        public virtual string Name { get { return _name; } set { _name = value; RaisePropertyChanged(() => _name); } }

        ///// <summary>
        ///// тема вопроса
        ///// </summary>
        //[XmlAttribute("Theme")]
        //public string Theme { get; set; }


        private string id;

        /// <summary>
        /// уникальный ид вопроса
        /// </summary>
        [XmlAttribute("ID")]
        public virtual string ID
        {
            get
            {
                return this.Number.ToString();
            }
            set
            {
               // this.id = value;
            }
        }



        /// <summary>
        /// тип вопроса
        /// </summary>
        [XmlAttribute("Type")]
        public virtual QuestionType Type
        {
            get
            {
                return QuestionType.Null;
            }
        }


        protected MediaData data;

        /// <summary>
        /// данные вопроса
        /// </summary>
        [XmlElement("MediaData")]
        public virtual MediaData Data
        {
            get
            {
                if (data == null)
                {
                    this.data = new MediaData();
                }
                return this.data;
            }
            set { this.data = value; RaisePropertyChanged(() => this.Data); }
        }

        protected int maxBall=1;

        /// <summary>
        /// макс балл за вопрос
        /// </summary>
       // [XmlAttribute("MaxBall")]
        [XmlIgnore]
        public virtual int MaxBall
        {
            get
            {
                if (maxBall == 0)
                {
                    maxBall = 1;
                }
                return maxBall;
            }
            set
            {
                if (value > 0 || value > minBall) 
                { 
                    maxBall = value;
                    RaisePropertyChanged(() => maxBall);
                }
                else {  }
            }
        }

        private int minBall;

        /// <summary>
        /// мин балл за вопрос
        /// </summary>
        [XmlAttribute("MinBall")]
        public virtual int MinBall
        {
            get
            {
                if (minBall < 0) { return 0; } return minBall;
            }
            set
            {
                if (value >= 0 || value <= maxBall)
                {
                    maxBall = value;
                    RaisePropertyChanged(() => maxBall);
                }
                else {  }
            }
        }

        /// <summary>
        /// номер показа
        /// </summary>
        [XmlAttribute("Number")]
        public int Number { get; set; }


        protected ObservableCollection<AnswerBase> answers = new ObservableCollection<AnswerBase>();

        /// <summary>
        /// словарь ответов
        /// </summary>
        //  [XmlArray("Answers"),
        //XmlArrayItem("Answer")]
        [XmlIgnore]
        public virtual ObservableCollection<AnswerBase> Answers
        {
            get { return answers; }
            set
            {
                this.answers = value;
                RaisePropertyChanged(() => this.Answers);
            }
        }

        #endregion

        #region public Methods


        /// <summary>
        /// получит все правильные ответы на вопрос
        /// </summary>
        /// <returns></returns>
        public virtual ObservableCollection<AnswerBase> GetRightAnswers()
        {
            ObservableCollection<AnswerBase> rightAnswers = new ObservableCollection<AnswerBase>();
            foreach (AnswerBase ans in this.answers)
            {
                if (ans.IsTrue == true)
                {
                    rightAnswers.Add(ans);
                }
            }

            return rightAnswers;
        }
       
        /// <summary>
        /// проверка ответа пользователя
        /// </summary>
        /// <returns>балл за вопрос</returns>
        public abstract int Verify(ClientQuestion clientQuestion);

        /// <summary>
        /// возвращает тип вопроса в виде строки
        /// </summary>
        /// <returns></returns>
        public string GetTypeOfQuestion()
        {
            try
            {
                return StringEnum.GetStringValue(this.Type);
            }
            catch
            {
                return null;
            }
        }

        public override string ToString()
        {
            return this.Number +") " + this.Name + "  " + this.GetTypeOfQuestion();
        }

        #endregion

    }
}
