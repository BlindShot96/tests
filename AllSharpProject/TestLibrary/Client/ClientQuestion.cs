using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestLibrary.ClientEdit
{
    /// <summary>
    /// вопрос клиента
    /// </summary>
    [XmlRoot("ClientQuestion")]
    public class ClientQuestion : TestLibrary.Helpers.NotificationObject
    {
        /// <summary>
        /// ID вопроса из теста
        /// </summary>
        [XmlAttribute("ID")]
        public string QuestionID { get; set; }

        /// <summary>
        /// тип вопроса
        /// </summary>
        [XmlAttribute("Type")]
        public QuestionType Type;

        /// <summary>
        /// балл за вопрос
        /// </summary>
        [XmlAttribute("Ball")]
        public int Ball { get;  set; }

        /// <summary>
        /// максимальный балл за вопрос
        /// </summary>
        [XmlAttribute("MaxBall")]
        public int MaxBall { get; set; }

        /// <summary>
        /// минимальный балл за вопрос
        /// </summary>
        [XmlAttribute("MinBall")]
        public int MinBall { get; set; }


        private  ObservableCollection<ClientQuestionAnswer> answers = new ObservableCollection<ClientQuestionAnswer>();

        /// <summary>
        /// ответы на вопрос
        /// </summary>
        [XmlArray("Answers")]
        [XmlArrayItem("Answer")]
        public  ObservableCollection<ClientQuestionAnswer> Answers {
            get { return answers; }
            set { answers = value; RaisePropertyChanged(() => answers); }
        }


    }
}
