using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestLibrary
{
    public class RightAnswer
    { 
        /// <summary>
        /// текст ответа клиента
        /// </summary>
        public string Data;

        public static RightAnswer Parse(XElement ans)
        {
            RightAnswer res = new RightAnswer();
            res.Data = ans.Value;
            res.Data = res.Data.Trim(new char[] { 'r', 'n', '/' });
            res.Data = res.Data.Trim();
            return res;
        }

       
    }
}
