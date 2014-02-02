using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestLibrary.ClientEdit
{
    /// <summary>
    /// все данные клиента
    /// </summary>
    [XmlRoot("Client")]
    public class ClientData  : TestLibrary.Helpers.NotificationObject
    {
        /// <summary>
        /// ид клиента
        /// высчитывается из имени + фамлмия + группа
        /// </summary>
        [XmlIgnore]
        public string ID { get { return Name + LastName + Group; } }

        /// <summary>
        /// имя пользователя
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// фамилия
        /// </summary>
        [XmlAttribute("LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// количество попыток
        /// </summary>
        [XmlIgnore]
        public int Attemts = 0;

        /// <summary>
        /// группа
        /// </summary>
        [XmlAttribute("Group")]
        public string Group { get; set; }

        /// <summary>
        /// время начала прохождения теста
        /// </summary>
        [XmlAttribute("TimeStart")]
        public DateTime TimeStart { get; set; }

        /// <summary>
        /// время окончания прохождения теста
        /// </summary>
        [XmlAttribute("TimeEnd")]
        public DateTime TimeEnd { get; set; }

        protected ClientReport report;

        /// <summary>
        /// ответы клиента на тест
        /// </summary>
        [XmlElement("Report")]
        public ClientReport Report
        {
            get
            {
                if (report == null)
                {
                    return new ClientReport();
                }
                else
                {
                    return report;
                }
            }
            set 
            {
                this.report = value;
                RaisePropertyChanged(() => report);
            }
        }

        protected ClientResult result;

        /// <summary>
        /// результат тестирования
        /// </summary>
        [XmlIgnore]
        public ClientResult Result
        {
            get
            {
                if (result == null)
                {
                    return new ClientResult();
                }
                else
                {
                    return result;
                }
            }
            set { this.result = value; RaisePropertyChanged(() => result); }
        }

        public ClientData() 
        {

        }
        public ClientData(string name, string lastName, string group, ClientReport report)
        { 
          this.Name = name;
          this.LastName = lastName;
          this.Group = group;
          this.Report = report;

        }

    }
}
