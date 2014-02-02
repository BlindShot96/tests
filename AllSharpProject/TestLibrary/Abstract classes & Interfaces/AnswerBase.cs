using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestLibrary
{
    /// <summary>
    /// абстрактный класс - ответ
    /// </summary>
    [XmlInclude(typeof(Answer))]
    [XmlInclude(typeof(MultiChoiceBallAswer))]
    [XmlRoot("Answer")]
    public abstract class AnswerBase :TestLibrary.Helpers.NotificationObject, IBase
    {
        private int RndId = GetRandom();
        private static int GetRandom()
        {
            Random rnd = new Random();
            return rnd.Next(Int32.MinValue,Int32.MaxValue);
        }

        #region public Fields

        protected string id;

        /// <summary>
        /// идентификатор
        /// </summary>
        [XmlAttribute("ID")]
        public virtual string ID { 
            get { return id; }
            set { id = value; }
        }

        protected MediaData data;

        /// <summary>
        /// информация ответа
        /// </summary>
        [XmlElement("MediaData")]
        public virtual MediaData Data
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
                RaisePropertyChanged(() => this.data);
            }
        }

        protected bool isTrue;

        /// <summary>
        /// правильность ответа
        /// </summary>
        [XmlAttribute("IsTrue")]
        public virtual bool IsTrue { get { return isTrue; } set { this.isTrue = value; RaisePropertyChanged(() => this.IsTrue); } }

        #endregion
    }
}
