using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestLibrary
{
    public enum QuestionType
    {
        SingleChoice,
        MultiChoice,
    }

    public class Question
    {
        public static Dictionary<string, QuestionType> QuestionDictionary = new Dictionary<string, QuestionType>
        {
            { "SingleChoice", QuestionType.SingleChoice },
            { "MultiChoice", QuestionType.MultiChoice }
        };

        /// <summary>
        /// тип вопроса
        /// </summary>
        public QuestionType Type;

        /// <summary>
        /// результат по вопросу
        /// ТОЛЬКО ДЛЯ КЛИЕНТА
        /// </summary>
        //public QuestionResult Result;

        /// <summary>
        /// данные вопроса
        /// </summary>
        public MediaData Data;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID;

        /// <summary>
        /// максимальый балл
        /// </summary>
        public int MaxMark =  1;


        /// <summary>
        /// минимальный балл
        /// </summary>
        public int MinMark =  0;

        /// <summary>
        /// список ответов на вопрос
        /// </summary>
        public List<Answer> Answers= new List<Answer>();

        /// <summary>
        /// правильные ответы
        /// </summary>
        public List<RightAnswer> RightAnswers = new List<RightAnswer>();

        /// <summary>
        /// парсинг вопроса
        /// </summary>
        /// <param name="question">элемент XML с вопросом</param>
        /// <exception cref="ArgumentException">неправлиный тип вопроса</exception>
        public virtual void Parse(XElement question)
        {
            this.ID = Int32.Parse(question.Attribute("ID").Value);
            if (this.ID == null) { 
                throw new ArgumentException("Uncorrect xml file"); }

            this.MaxMark = Int32.Parse(question.Attribute("MaxMark").Value);
            if (this.MaxMark == null || this.MaxMark <= 0) { this.MaxMark = 1; }

            this.MinMark = Int32.Parse(question.Attribute("MinMark").Value);
            if (this.MinMark == null || this.MaxMark < 0 || this.MinMark >= this.MaxMark) { this.MinMark = 0; }

            try
            {
                this.Type = Question.GetTypeOfQuestion(question);
            }
            catch
            {
                throw new ArgumentException("Uncorrect xml file");
            }

            XElement Mdata = question.Element("MediaData");
            if (Mdata != null)
            {
                this.Data = new MediaData();
                this.Data.Parse(Mdata);
            }

            switch (this.Type)
            { 
                case QuestionType.SingleChoice:
                    {
                        foreach (XElement answer in question.Element("Answers").Elements())
                        {
                            Answer ans = new Answer();
                            ans.Parse(answer);
                            this.Answers.Add(ans);
                        }
                        break;
                    }
                case QuestionType.MultiChoice:
                    {
                        break;
                    }
            }

            foreach (XElement RigthAns in question.Element("RightAnswers").Elements())
            {
                RightAnswer ans = RightAnswer.Parse(RigthAns);
                this.RightAnswers.Add(ans);
            }
        }

        


        /// <summary>
        /// находит тип вопроса 
        /// </summary>
        /// <param name="question">xml с вопросом</param>
        /// <returns>тип вопроса</returns>
        public static QuestionType GetTypeOfQuestion(XElement  question)
        {
            string type = question.Attribute("Type").Value;
            try
            {
                return QuestionDictionary[type];
            }
            catch
            {
                throw new ArgumentException("Uncorrect xml file");
            }
        }


    }
}
