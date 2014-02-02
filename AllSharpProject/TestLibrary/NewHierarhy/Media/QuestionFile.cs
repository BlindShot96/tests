using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLibrary.Media
{
    public class QuestionFile
    {
        /// <summary>
        /// находит файл с нужным ID
        /// </summary>
        /// <param name="test">Объект теста</param>
        /// <returns></returns>
        public IMediaFile GetMediaFile(Test test)
        {
            return null;
        }

        /// <summary>
        /// ID файла из списка в объхекте теста
        /// </summary>
        public string FileID
        {
            get; 
            set;
        }

        /// <summary>
        /// описание файла
        /// </summary>
        public string Description
        { get; set; }

        /// <summary>
        /// имя файла
        /// </summary>
        public string Name
        { get; set; }

        public static QuestionFile CreateQuestionFile(IMediaFile mediaFile)
        {
            if (mediaFile == null)
            {
                throw new ArgumentNullException("mediaFile","параметр равен null");
            }
            if (mediaFile.ID == null)
            {
                throw  new ArgumentOutOfRangeException("mediaFile.ID","ID IMediaFile = null");
            }

            return new QuestionFile() {FileID = mediaFile.ID};
        }
    }
}
