using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SimpleTestServerUDP.DataManagers
{
    class Question
    {
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
        public int MaxMark;

        /// <summary>
        /// список ответов на вопрос
        /// </summary>
        public List<Answer> Answers;

        public virtual void Parse(XElement question)
        {
            this.ID = Int32.Parse(question.Attribute("ID").Value);
            this.MaxMark = Int32.Parse(question.Attribute("MaxMark").Value);
        }
    }
}
