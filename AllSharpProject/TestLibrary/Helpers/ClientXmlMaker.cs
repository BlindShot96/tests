using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace TestLibrary.Helpers
{ 
    /// <summary>
  /// Обеспечивает возможность реализации пользовательской логики в привязке.
  /// </summary>
  public interface IValueConverter
  {
    /// <summary>
    /// Преобразует значение.
    /// </summary>
    /// 
    /// <returns>
    /// Преобразованное значение. Если метод возвращает null, используется действительное значение null.
    /// </returns>
    /// <param name="value">Значение, произведенное исходной привязкой.</param><param name="targetType">Тип свойства цели связывания.</param><param name="parameter">Используемый параметр преобразователя.</param><param name="culture">Язык и региональные параметры, используемые в преобразователе.</param>
    object Convert(object value, Type targetType, object parameter, CultureInfo culture);
    /// <summary>
    /// Преобразует значение.
    /// </summary>
    /// 
    /// <returns>
    /// Преобразованное значение. Если метод возвращает null, используется действительное значение null.
    /// </returns>
    /// <param name="value">Значение, произведенное целью привязки.</param><param name="targetType">Тип, в который выполняется преобразование.</param><param name="parameter">Используемый параметр преобразователя.</param><param name="culture">Язык и региональные параметры, используемые в преобразователе.</param>
    object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
  }

    public class ClientXmlConverter : IValueConverter
    {

        public ClientXmlConverter()
        {
         //  LoadAttributesToAndroid();
            
        }

        private void LoadAttributesToAndroid()
        {
            XDocument doc = XDocument.Load("ClientConverterAndroid.xml");
            SerializableDictionary<KeyValuePair<XName, string>, KeyValuePair<XName, string>> res 
                = new SerializableDictionary<KeyValuePair<XName, string>, KeyValuePair<XName, string>>();

            foreach (XElement el in doc.Element("Root").Element("Attributes").Elements())
            {
                if (el.Name == "Item")
                {
                    var xElement = el.Element("Original");
                    var element = el.Element("New");
                    if (xElement != null && element != null)
                    {
                            KeyValuePair<KeyValuePair<XName, string>, KeyValuePair<XName, string>> pair
                                = new KeyValuePair<KeyValuePair<XName, string>, KeyValuePair<XName, string>>(
                                    new KeyValuePair<XName, string>(XName.Get(xElement.Attribute("Name").Value),
                                        xElement.Attribute("Value").Value),
                                    new KeyValuePair<XName, string>(
                                        XName.Get(element.Attribute("Name").Value),
                                        element.Attribute("Value").Value));

                        res.Add(pair.Key,pair.Value);
                    }
                }
            }

            if (res.Count != 0)
            {
                AttributesToAndroid = res;
            }

        }

        private string OriginalXml = null;
        private string ResultXml;

        private static XNamespace Xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
        private static XNamespace Xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");

        private SerializableDictionary<KeyValuePair<XName, string>, KeyValuePair<XName, string>> AttributesToAndroid =
            new SerializableDictionary<KeyValuePair<XName, string>, KeyValuePair<XName, string>>()
            {
                {
                    new KeyValuePair<XName, string>("{http://www.w3.org/2001/XMLSchema-instance}type", "QMultiChoice"),
                    new KeyValuePair<XName, string>("class", "TestLibrary.Questions.QMultiChoice")
                },      
                {
                    new KeyValuePair<XName, string>("{http://www.w3.org/2001/XMLSchema-instance}type", "QSingleChoice"),
                    new KeyValuePair<XName, string>("class", "TestLibrary.Questions.QSingleChoice")
                },
                 {
                    new KeyValuePair<XName, string>("{http://www.w3.org/2001/XMLSchema-instance}type", "QTextChoice"),
                    new KeyValuePair<XName, string>("class", "TestLibrary.Questions.QTextAnswer")
                },
                 {
                    new KeyValuePair<XName, string>("{http://www.w3.org/2001/XMLSchema-instance}type", "QLikertAnswer"),
                    new KeyValuePair<XName, string>("class", "TestLibrary.Questions.QLikertAnswer")
                },
                {
                    new KeyValuePair<XName, string>("{http://www.w3.org/2001/XMLSchema-instance}type", "Answer"),
                    new KeyValuePair<XName, string>("Null", "Null")
                },
                 {
                    new KeyValuePair<XName, string>("{http://www.w3.org/2001/XMLSchema-instance}type", "MultiChoiceBallAswer"),
                    new KeyValuePair<XName, string>("Null", "Null")
                },
                 {
                    new KeyValuePair<XName, string>("IsTrue", "true"),
                    new KeyValuePair<XName, string>("Null", "Null")
                },
                  {
                    new KeyValuePair<XName, string>("IsTrue", "false"),
                    new KeyValuePair<XName, string>("Null", "Null")
                },
                {
                    new KeyValuePair<XName, string>("VerifyByAll", "true"),
                    new KeyValuePair<XName, string>("Null", "Null")
                },
                {
                    new KeyValuePair<XName, string>("VerifyByAll", "false"),
                    new KeyValuePair<XName, string>("Null", "Null")
                },
                {
                    new KeyValuePair<XName, string>("{http://www.w3.org/1999/xhtml}xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new KeyValuePair<XName, string>("Null", "Null")
                },
                     {
                    new KeyValuePair<XName, string>("{http://www.w3.org/1999/xhtml}xsd", "http://www.w3.org/2001/XMLSchema"),
                    new KeyValuePair<XName, string>("Null", "Null")
                },
                

            };

        private static List<XName> AttributesToDelete = new List<XName>()
        {
            "IsTrue",
            "{http://www.w3.org/1999/xhtml}xsi",
            "{http://www.w3.org/1999/xhtml}xsd",
            "VerifyByAll",
            "MultiChoiceAnswerBall",
            "Attemts",
            "Min5",
            "Min4",
            "Min3"
        };

        private static XAttribute MediaFilesAttr = new XAttribute("class","java.util.ArrayList");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var originalXml = (string) value;
           // originalXml = Regex.Replace(originalXml, "\r\n", "\n");

            XDocument doc;

            try
            {
                doc = XDocument.Parse(originalXml);
                this.OriginalXml = originalXml;
            }
            catch
            {
                return null;
            }

            SetQuestions(GetTestElement(doc));
            SetMediaData(GetTestElement(doc));
            SetAttributes(GetTestElement(doc));


            this.ResultXml = doc.ToString();
            this.ResultXml = Regex.Replace(this.ResultXml, "\r\n", "\n");
            return this.ResultXml;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.ResultXml.Equals(value))
            {
                return this.OriginalXml;
            }
            return null;
        }


        private List<XElement> GetQuestions(XElement test)
        {
            var questionsElement = test.Element("Questions");
            if (questionsElement != null)
            {
                return questionsElement.Elements() as List<XElement>;
            }
            else
            {
                return new List<XElement>();
            }
        }

        private XElement GetTestElement(XDocument doc)
        {
            XElement el = doc.Element("Test");
            el.RemoveAttributes();
            return el;
        }

        private XElement GetTestSettingsElement(XElement testElement)
        {
            return testElement.Element("Settings");
        }

        private List<XElement> GetAnswersElements(XElement question)
        {
            var xElement = question.Element("Answers");
            if (xElement != null)
            {
                return xElement.Elements() as List<XElement>;
            }
            else
            {
                return new List<XElement>();
            }
        }

        private XElement GetMediaData(XElement element)
        {
            return element.Element("MediaData");
        }

        private List<XElement> GetMediaFiles(XElement mediaData)
        {
            var xElement = mediaData.Element("Files");
            if (xElement != null)
            {
                return xElement.Elements() as List<XElement>;
            }
            else
            {
                return new List<XElement>();
            }
        }

        private void SetAttributes(XElement root)
        {
            foreach (XName xName in AttributesToDelete)
            {
                XAttribute attr = root.Attribute(xName);
                if (attr != null)
                {
                    attr.Remove();                  
                }
            }

            foreach (var pair in AttributesToAndroid)
            {
                XName or_name = pair.Key.Key;
                XName new_name = pair.Value.Key;
                string new_value = pair.Key.Value;
                string or_value = "";

                XAttribute attr = root.Attribute(or_name);
                if(attr != null){ or_value = attr.Value;}

                if (attr != null && or_value.Equals(new_value))
                {                  
                    attr.Remove();

                    if (new_name.LocalName.Equals("Null") == false)
                    {
                        root.Add(new XAttribute(pair.Value.Key, pair.Value.Value));
                    }

                    //if (pair.Value.Key.Equals("#Null") != true)
                    //{
                    //    root.Attribute(pair.Key.Key).Remove();
                    //    root.Add(new XAttribute(pair.Value.Key,pair.Value.Value));
                    //}
                    //else
                    //{
                    //      root.Attribute(pair.Key.Key).SetValue(pair.Value.Value);
                    //}
                }
            }

            foreach (var element in root.Elements())
            {
                if (element != null)
                {
                    SetAttributes(element);
                }
            }
        }

        private void SetMediaData(XElement test)
        {
            IEnumerable<XElement> questions =  test.Element("Questions").Elements();
            foreach (XElement question in questions)
            {
                XElement mediaDataEl = GetMediaData(question);
                mediaDataEl.Element("Files").Add(MediaFilesAttr);
            }
        }

        private void SetQuestions(XElement test)
        {
            IEnumerable<XElement> questions = test.Element("Questions").Elements();
            foreach (XElement question in questions)
            {
                if (question.Attribute("{http://www.w3.org/2001/XMLSchema-instance}type").Value.Equals("QTextChoice"))
                {
                    SetTQ(question);
                }
            }
        }

        private void SetTQ(XElement question)
        {
            IEnumerable<XElement> answers = question.Element("Answers").Elements();
            foreach (XElement answer in answers)
            {
                XElement element = answer.Element("MediaData");
                if (element != null) element.Element("Text").Value = " ";
            }
        }



    }
}
