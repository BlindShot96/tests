using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TestLibrary.Media;

namespace TestLibrary
{
    public sealed class NewTest:TestBase
    {

        private MediaFilesCollection files = new MediaFilesCollection();

        /// <summary>
        /// файлы теста
        /// </summary>
        [XmlArray("Files"),
        XmlArrayItem("File")]
        public MediaFilesCollection Files
        {
            get { return files; }
            set { files = value; }
        }

        /// <summary>
        /// описание теста
        /// </summary>
        [XmlElement("Description")]
        public string Description
        {
            get; 
            set;
        }

        /// <summary>
        /// настройки теста
        /// </summary>
        private TestSettings _settings = new TestSettings();

        /// <summary>
        /// настройки теста
        /// </summary>
        [XmlElement("Settings")]
        public TestSettings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                this._settings = value;
                RaisePropertyChanged(() => this._settings);
            }
        }

        /// <summary>
        /// максимальный балл за тест
        /// </summary>
        [XmlIgnore]
        public int MaxQuestionsBall
        {
            get
            {
                return this._questions.Sum(q => q.MaxBall);
            }
        }
    }
}
