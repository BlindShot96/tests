using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using TestLibrary.ClientEdit;

namespace TestLibrary
{
    public class QTextChoice : QuestionBase
    {
        [XmlIgnore]
        public override QuestionType Type
        {
            get
            {
                return QuestionType.TextQuestion;
            }
        }

        [XmlAttribute("MaxBall")]
        public override int MaxBall {
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

        public override int Verify(ClientQuestion clientQuestion)
        {
            return VerifyList(clientQuestion);
            //int res = this.MinBall;
            //string s = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(clientQuestion.Answers[0].Data.Text));
            //string ss = s;
            //try
            //{
            //    if (this.answers.ElementAt(0).Data.Text.Equals(clientQuestion.Answers[0].Data.Text))
            //    {
            //        res = this.MaxBall;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return res;
            //}
            //return res;
        }

        private int VerifyList(ClientQuestion clientQuestion)
        {
            int res = this.MinBall;
            string ans_client = prepareAnswer(clientQuestion.Answers[0].Data.Text);
            try
            {
                foreach (AnswerBase answer in Answers)
                {
                    string ans_test = prepareAnswer(answer.Data.Text);
                    if (ans_test.Equals(ans_client) == true)
                    {
                        res = this.MaxBall;
                        break;
                    }
                }                
            }
            catch (Exception ex)
            {
                return res;
            }
            return res;
        }

        private string prepareAnswer(string ans)
        {
            string res = ans;
            res = Regex.Replace(res, " ", ""); ;
            res = res.ToLower();
            res = Regex.Replace(res, Environment.NewLine, "");
            res = Regex.Replace(res, "/n", "");

            return res;
        }

        private string DeleteSpace(string ans)
        {
            string res = "";
            for (int i = 0; i < ans.Length; i++)
            {
                if (ans.ToCharArray()[i].Equals(" ") == false)
                {
                    res += ans.ToCharArray()[i];
                }
            }
            return res;
        }
    }
}
