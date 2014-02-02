using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TestLibrary
{
    [XmlRoot("Answer")]
    public class MultiChoiceBallAswer : Answer
    {
        public MultiChoiceBallAswer() : base()
        {
        }

        public MultiChoiceBallAswer(MediaData data, int ball):base(data)
        {
            if (ball < 0)
            {
                ball = 0;
            }

            this.ball = ball;
        }
        public MultiChoiceBallAswer(MediaData data) :base(data)
        {
        }

        public MultiChoiceBallAswer(MediaData data, bool istrue) : base(data, istrue)
        {
        }

        /// <summary>
        /// Конвертер AnswerBase в MultiChoiceBallAnswer
        /// </summary>
        /// <param name="ans"></param>
        /// <returns></returns>
        public static MultiChoiceBallAswer FromAswerBase(AnswerBase ans)
        {
            MultiChoiceBallAswer res = new MultiChoiceBallAswer();
            try
            {
                res = (MultiChoiceBallAswer) ans;
            }
            catch (Exception)
            {
                int ball = 0;
                if (ans.IsTrue == true)
                {
                    ball = 1;
                }

                res = new MultiChoiceBallAswer(ans.Data,ball) {ID = ans.ID};
            }

            return res;
        }

        private int ball = 0;
        /// <summary>
        /// балл за ответ
        /// </summary>
        [XmlAttribute("MultiChoiceAnswerBall")]
        public int Ball
        {
            get { return ball; }
            set
            {
                ball = value;
            }
        }

    }
}
