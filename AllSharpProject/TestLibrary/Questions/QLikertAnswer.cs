using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestLibrary.ClientEdit;

namespace TestLibrary
{
    public class QLikertAnswer : QuestionBase
    {
        public override QuestionType Type
        {
            get
            {
                return QuestionType.LikertQuestion;
            }
        }


        public override int Verify(ClientQuestion clientQuestion)
        {
            int res = this.MinBall;
            try
            {
                if (this.answers.ElementAt(0).Data.Text == clientQuestion.Answers[0].Data.Text)
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
