using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace TestLibrary
{
    public class ClientAnswerMain
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
        /// название теста
        /// </summary>
        public string Name;

        /// <summary>
        /// список вопросов
        /// </summary>
        public Dictionary<int, ClientQuestion> Questions = new Dictionary<int, ClientQuestion>();

        public static ClientAnswerMain Parse(XElement ans)
        {
            ClientAnswerMain res = new ClientAnswerMain();
            res.Name = ans.Attribute("Name").Value;
            foreach (XElement question in ans.Element("Questions").Elements())
            {
                ClientQuestion q = ClientQuestion.Parse(question);
                res.Questions.Add(q.ID, q);
            }
            return res;
        }

        public static Result Verify(ClientAnswerMain client, Test original)
        {
            int RightQuestins = 0;
            int Wrong =  0;
            foreach (ClientQuestion cq in client.Questions.Values)
            {
                if (cq.Type == QuestionType.SingleChoice)
                { 
                    if(cq.Answers[0].Data == original.Questions[cq.ID].RightAnswers[0].Data)
                    {
                        RightQuestins++;
                    }
                    else
                    {
                      Wrong++;
                    }
                }

                if (cq.Type == QuestionType.MultiChoice)
                { 
                   //------
                }
            }

            Result res = new Result(RightQuestins, Wrong);
            return res;
        }
    }
}
