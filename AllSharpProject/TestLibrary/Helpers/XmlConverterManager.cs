using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestLibrary.Helpers
{
    /// <summary>
    /// просое добавление нового вопроса
    /// в классе вопроса конвертиование
    /// </summary>
    public class XmlConverterManager
    {
        private static  Dictionary<XName,Action<XElement>> questionTypesToActions = new Dictionary<XName, Action<XElement>>();

        private static  Dictionary<XName,Action<XElement>> eLementNanesToActions = new Dictionary<XName, Action<XElement>>(); 

        public static void  InsertQuestionTypeAction(QuestionBase q)
        {
        }

        public static void InsertXmlElementAction(XName name, Action<XElement> element)
        {
        }
    }
}
