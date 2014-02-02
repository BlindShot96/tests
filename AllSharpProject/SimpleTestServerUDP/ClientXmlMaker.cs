using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SimpleTestServerUDP
{
    public class ClientXmlMaker
    {
        public  const  string ClassAttrName = "class";
        public  const string ClassHashMap = "java.util.HashMap";
        public  const string ClassQuestionSingleChoice = "com.example.myapp.testlibrary.Questions.QSingleChoice";
        public  const string ClassQuestionMultiChoice = "com.example.myapp.testlibrary.Questions.QMultiChoice";
        public  const string ClassAnswer = "com.example.myapp.testlibrary.Answer";
        public  const string ClassAttr = "xsi:type"; 

        public  const string IsTrueAttribute = "IsTrue";
        public  const string VerifyByAllAttr = "VerifyByAll";

        /// <summary>
        /// преобразовывает xml, полученную при сериализации на сервере 
        /// в xml, которую понимает клиент (из-за различий в сериализации/десериализации)
        /// </summary>
        /// <param name="original_xml">серверная xml</param>
        /// <returns>xml для клиента</returns>
        public static string GetClientXml(string original_xml)
        {
            XDocument doc = XDocument.Parse(original_xml);
            doc.Element("Test").Element("Questions").Add(new XAttribute(ClassAttrName, ClassHashMap));

            foreach (XElement item in doc.Elements("Questions"))
            {
                XElement question = item.Element("Value").Element("Question");

                if (question.Attribute(ClassAttr).Value == "QSingleChoice")
                {
                    question.Attribute(ClassAttr).Remove();
                    question.Add(new XAttribute(ClassAttrName, ClassQuestionSingleChoice));
                }
                if (question.Attribute(ClassAttr).Value == "QMultiChoice")
                {
                    question.Attribute(ClassAttr).Remove();
                    question.Add(new XAttribute(ClassAttrName, ClassQuestionMultiChoice));
                }


                if (question.Attribute(VerifyByAllAttr) != null)
                {
                    question.Attribute(VerifyByAllAttr).Remove();
                }

                question.Element("Answers").Add(new XAttribute(ClassAttrName, ClassHashMap));

                foreach (XElement val in question.Elements("Answers"))
                {
                    XElement answer = val.Element("Value").Element("Answer");
                    if (answer.Attribute(IsTrueAttribute) != null)
                    {
                        answer.Attribute(IsTrueAttribute).Remove();
                    }
                }
            }

            return doc.ToString();

        }
    }
}
