using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestLibrary
{
    public class ClientQuestion
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID;

        /// <summary>
        /// тип вопроса
        /// </summary>
        public QuestionType Type;

        /// <summary>
        ///  ответы клиента
        /// </summary>
        public List<ClientAnswer> Answers = new List<ClientAnswer>();
    }
}
