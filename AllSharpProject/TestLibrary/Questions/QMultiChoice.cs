using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TestLibrary.ClientEdit;
using System.Xml.Serialization;

namespace TestLibrary
{
    public class QMultiChoice:QuestionBase
    {
        [XmlIgnore]
        public override QuestionType Type
        {
            get
            {
                return QuestionType.MultiChoice;
            }
        }

        [XmlAttribute("MaxBall")]
        public override int MaxBall {
            get
            {
                if (this.VerifyByBalls == true)
                {
                    return Answers.Sum(answer => answer.Ball);
                }
                else
                {
                    return this.maxBall;
                }
            }
            set
            {
                this.maxBall = value;
            }
        }

        private new ObservableCollection<MultiChoiceBallAswer> answers = new ObservableCollection<MultiChoiceBallAswer>();

         [XmlArray("Answers"),
        XmlArrayItem("Answer")]
        public new ObservableCollection<MultiChoiceBallAswer> Answers
        {
            get { return answers; }
            set
            {
                this.answers = value;
                RaisePropertyChanged(() => this.Answers);
            }
        }

        public new ObservableCollection<MultiChoiceBallAswer> GetRightAnswers()
        {
            var rightAnswers = new ObservableCollection<MultiChoiceBallAswer>();
            foreach (MultiChoiceBallAswer ans in this.Answers)
            {
                if (ans.IsTrue == true)
                {
                    rightAnswers.Add(ans);
                }
            }

            return rightAnswers;
        }


        [XmlAttribute("VerifyByAll")]
        public bool VerifyByBalls = false;

        private  int VerifyOLD(ClientQuestion clientQuestion)
        {
            int res = this.MinBall;
            if (clientQuestion.Answers.Count != this.GetRightAnswers().Count)
            { res = this.MinBall; return res; }

            int allball = this.GetRightAnswers().Count;
            int ball = 0;

            for (int i = 0; i < clientQuestion.Answers.Count; i++)
            {
                if (FindAnswerFromRightAnswers(clientQuestion.Answers[i].Data.Text) == true)
                {
                    ball++;
                }
                else
                {
                  //  ball--;
                }
            }

            float procent = ((float)ball / (float)this.GetRightAnswers().Count) * 100;
            if (this.VerifyByBalls == true)
            {
                if (procent <= 30) 
                {
                    res = this.MinBall;
                }
                if (procent >= 80) 
                { 
                    res = this.MaxBall; 
                }
                if (procent > 30 & procent < 80) 
                { 
                    res = (this.MaxBall - this.MinBall) / 2; 
                }
            }
            else
            {              
                if (procent <= 50) 
                { 
                    res = this.MinBall; 
                }
                if (procent > 50) 
                { 
                    res = this.MaxBall; 
                }
            }

            return res;
        }

        public override int Verify(ClientQuestion clientQuestion)
        {
            if (VerifyByBalls == true)
            {
                return VerifyBalls(clientQuestion);
            }
            else
            {

                return VerifyCountAll(clientQuestion);
            }

        }

       private int VerifyCountRight(ClientQuestion clientQuestion)
        {
            int resultBall = this.MinBall;
            int rightCount = 0;
            int originalRightAnswersCount = this.GetRightAnswers().Count();


            foreach (ClientQuestionAnswer clientQuestionAnswer in clientQuestion.Answers)
            {
                if (IsAnswerRight(clientQuestionAnswer) == true)
                {
                    rightCount++;
                }

            }

           if (originalRightAnswersCount == 0)
           {
               return 0;
           }

           resultBall = (int) Math.Round((double) ((rightCount/originalRightAnswersCount)*this.MaxBall));
           return resultBall;

        }

       private int VerifyCountAll(ClientQuestion clientQuestion)
        {
            int resultBall = this.MinBall;
            int rightCount = 0;
            int originalRightAnswersCount = this.GetRightAnswers().Count();
            int wrongCount = 0;

            foreach (ClientQuestionAnswer clientQuestionAnswer in clientQuestion.Answers)
            {
                if (IsAnswerRight(clientQuestionAnswer) == true)
                {
                    rightCount++;
                }
                else
                {
                    wrongCount++;
                }
            }

            if (originalRightAnswersCount == 0)
            {
                return 0;
            }

            if (originalRightAnswersCount == rightCount && wrongCount == 0)
            {
                resultBall =  this.MaxBall;
            }
            else
            {
                resultBall = this.MinBall;
            }
            return resultBall;

        }

        private int VerifyBalls(ClientQuestion clientQuestion)
        {
            int resultBall = clientQuestion.Answers.Where(clientQuestionAnswer => FindOriginalAnswer(clientQuestionAnswer) != null).
                Sum(clientQuestionAnswer => ( FindOriginalAnswer(clientQuestionAnswer)).Ball);

            if (resultBall < 0)
            {
                resultBall = 0;
            }

            return resultBall;

        }

        /// <summary>
        /// проверка ответа клиента
        /// </summary>
        /// <param name="clientQuestionAnswer"></param>
        /// <returns></returns>
        private bool IsAnswerRight(ClientQuestionAnswer clientQuestionAnswer)
        {
            return this.GetRightAnswers().Any(a => clientQuestionAnswer.Data.Text == a.ID);
        }

        private MultiChoiceBallAswer FindOriginalAnswer(ClientQuestionAnswer answer)
        {
            return Answers.FirstOrDefault(answerBase => answerBase.ID == answer.Data.Text);
        }

        /// <summary>
        /// есть ли в оригинальном вопросе c правильными ответами с данным ID
        /// </summary>
        /// <param name="data">ID</param>
        /// <returns>есть - true, нет - false</returns>
        private bool FindAnswerFromRightAnswers(string data)
        {
            return this.GetRightAnswers().Any(a => data == a.ID);
        }
    }
}
