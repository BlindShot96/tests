using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace SimpleTestServerUDP.DataManagers
{
    class Test
    {

        /// <summary>
        /// поток с файлом теста
        /// </summary>
        public StreamReader Reader { get; private set; }

        /// <summary>
        /// XML документ
        /// </summary>
        public XElement TestDocument { get; private set; }

        /// <summary>
        /// сведения о тесте 
        /// </summary>
        public MediaData Data;

        /// <summary>
        /// название теста
        /// </summary>
        public string Name;

        /// <summary>
        /// имя учителя
        /// </summary>
        public string TeacherName;

        /// <summary>
        /// количество вопросов
        /// </summary>
        public int QuestionsCount;

        /// <summary>
        /// максимальная оценка
        /// </summary>
        public int MaxMark;


        /// <summary>
        /// список вопросов
        /// </summary>
        public List<Question> Questions = new List<Question>();


       public Test(XElement test)
       {


       }

       /// <summary>
       /// читает информацию из загруженнгого файла теста
       /// </summary>
       public void Parse()
       {
           if (TestDocument == null) { throw new ArgumentNullException("Xdocument"); }
           try
           {
               XElement Test = this.TestDocument;

               this.Name = Test.Attribute("Name").Value;
               this.TeacherName = Test.Attribute("TeacherName").Value;
               this.QuestionsCount = Int32.Parse(Test.Attribute("QuestionsCount").Value);
               this.MaxMark = Int32.Parse(Test.Attribute("MaxMark").Value);

               //foreach (XElement Question in Test.Elements())
               //{
               //    Question Qd;
               //    //Qd.ID = Int32.Parse(Question.Attribute("ID").Value);
               //    //Qd.MaxMark = Int32.Parse(Question.Attribute("MaxMark").Value);
               //    //Qd.Type = Question.QuestionDictionary[Question.Attribute("Type").Value];
               //    //Qd.Text = Question.Element("Text").Value;

               //   // Qd.Answers = new List<DataManagers.Answer>();

               //    //foreach (XElement Answer in Question.Elements())
               //    //{
               //    //    if (Answer.Name == "Answer")
               //    //    {
               //    //        Answer ans;
               //    //        ans.ID = Int32.Parse(Answer.Attribute("ID").Value);
               //    //        ans.IsRight = Boolean.Parse(Answer.Attribute("IsTrue").Value);
               //    //       // ans.Text = Answer.Value;

               //    //        Qd.Answers.Add(ans);
               //    //    }
               //    //}

               //    //this.Questions.Add(Qd);
               //}
           }
           catch (Exception ex) 
           {
               throw new Exception("IT ISNT <TEST>");
               return;
           }
           return;
       }


        /// <summary>
        /// возвращает строку xml файла теста
        /// </summary>
        /// <returns></returns>
       public override string ToString()
       {
           return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + this.TestDocument.ToString();
       }

    }
}
