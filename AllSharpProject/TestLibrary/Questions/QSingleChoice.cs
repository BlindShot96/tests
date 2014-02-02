using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using TestLibrary.ClientEdit;

namespace TestLibrary
{
    public class QSingleChoice:QuestionBase
    {
        [XmlIgnore]
        public override QuestionType Type
        {
            get
            {
                return QuestionType.SingleChoice;
            }
        }

        [XmlAttribute("MaxBall")]
        public override int MaxBall
        {
            get { return base.MaxBall; }
            set { base.MaxBall = value; }
        }

        [XmlArray("Answers"),
        XmlArrayItem("Answer")]
        public new ObservableCollection<AnswerBase> Answers
        {
            get { return answers; }
            set
            {
                this.answers = value;
                RaisePropertyChanged(() => this.Answers);
            }
        }

        [XmlAttribute("class")]
        public  const string android_class = "testlibrary._questions.QSingleChoice";

        public override int Verify(ClientQuestion clientQuestion)
        {
            int res = this.MinBall;
            try
            {
                if (this.GetRightAnswers().ElementAt(0).ID.Equals(clientQuestion.Answers[0].Data.Text))
                {  
                    res = this.MaxBall;
                }
            }
            catch (Exception ex)
            {
                return res;
            }
            return res;
        }

    }
}
