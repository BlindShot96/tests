using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestLibrary.ClientEdit
{
    /// <summary>
    /// ответ клиента на вопрос
    /// </summary>
    [XmlRoot("Answer")]
    public class ClientQuestionAnswer : TestLibrary.Helpers.NotificationObject
    {
        protected MediaData data;

        /// <summary>
        /// ответ на вопрос
        /// </summary>
        [XmlElement("MediaData")]
        public MediaData Data
        {
            get
            {
                if (data == null)
                {
                    return new MediaData();
                }
                else
                {
                    return data;
                }
            }
            set
            {
                this.data = value;
                RaisePropertyChanged(() => this.Data);
            }
        }
    }
}
