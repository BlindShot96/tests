using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestLibrary
{
    public class ClientAnswer
    {
        /// <summary>
        /// текст ответа клиента
        /// </summary>
        public string Data;

        public static ClientAnswer Parse(XElement ans)
        {
            ClientAnswer res = new ClientAnswer();
            res.Data = ans.Value;
            res.Data = res.Data.Trim(new char[]{'r','n','/'});
            res.Data = res.Data.Trim();
            return res;
        }      
    }
}
