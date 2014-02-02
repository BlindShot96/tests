using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTestServerUDP.DataManagers
{
    class Answer
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        public int ID;

        /// <summary>
        /// является ли верным ответом
        /// </summary>
        public bool IsRight;

        /// <summary>
        /// информация вопроса
        /// </summary>
        MediaData Data;

        /// <summary>
        /// максимальый балл
        /// </summary>
        public int MaxMark;
    }
}
