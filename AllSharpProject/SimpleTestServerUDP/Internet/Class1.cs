using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TestLibrary;
using TestLibrary.ClientEdit;

namespace SimpleTestServerUDP.Internet
{

        /// <summary>
        /// ответ клиента
        /// </summary>
        [XmlRoot("Report")]
        public class DClientReport : TestLibrary.Helpers.NotificationObject
        {

            private ObservableCollection<ClientQuestion> questions = new ObservableCollection<ClientQuestion>();

            /// <summary>
            /// вопросы от клиента
            /// </summary>
            [XmlArray("Questions"),
            XmlArrayItem("Question")]
            public ObservableCollection<ClientQuestion> Questions
            {
                get
                {
                    return this.questions;
                }
                set
                {
                    this.questions = value;
                }
            }

            /// <summary>
            /// проверка ответов пользователя
            /// </summary>
            /// <param name="OriginalTest">оригинальный тест</param>
            /// <returns></returns>
            public ClientResult Verify(ref Test OriginalTest)
            {
                ClientResult res = new ClientResult();

                int ClientBalls = 0;


                foreach (ClientQuestion cq in this.Questions)
                {
                    try
                    {
                        QuestionBase qb = FindOriginalQuestion(cq, ref OriginalTest);
                        cq.Ball = qb.Verify(cq);
                        cq.MaxBall = qb.MaxBall;
                        cq.MinBall = qb.MinBall;
                        ClientBalls += cq.Ball;
                    }
                    catch
                    {
                        throw new ArgumentException();
                    }
                }

                res = new ClientResult(OriginalTest.MaxQuestionsBall, ClientBalls, OriginalTest.Settings);
                return res;
            }

            

            public QuestionBase FindOriginalQuestion(ClientQuestion q, ref Test originalTest)
            {
                try
                {
                    return originalTest[q.QuestionID];
                }
                catch
                {
                    throw new ArgumentException();
                }
            }
        }
}
