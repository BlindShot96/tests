using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestLibrary;

namespace SimpleTestServerUDP.DataManagers
{


    public struct ClientData
    {


        /// <summary>
        /// состояние клиента
        /// </summary>
        public Status Status;

        /// <summary>
        /// текущий xml файл 
        /// данного клиента
        /// </summary>
        public XDocument CurrentXml;

        /// <summary>
        /// имя клиента
        /// </summary>
        public string Name;

        /// <summary>
        /// фамилия клиента
        /// </summary>
        public string LastName;

        /// <summary>
        /// группа клиента
        /// </summary>
        public string Group;

        /// <summary>
        /// Gets the Buffer.
        /// </summary>
        public byte[] Buffer;

        /// <summary>
        /// Result string from client
        /// </summary>
        public string Result;

        /// <summary>
        /// ответ клиента
        /// </summary>
        //public ClientAnswerMain ClientAnswerResult;

        /// <summary>
        /// результат теста
        /// </summary>
        //public Result ClientResult;


    }
}
