using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace TestLibrary.Media
{
    public class QuestionFilesCollection:ObservableCollection<QuestionFile>
    {
        /// <summary>
        /// добавление файла
        /// </summary>
        /// <param name="mediaFile"></param>
        public void Add(IMediaFile mediaFile,NewTest baseTest)
        {
            foreach (IMediaFile questionFile in baseTest.Files)
            {
                
            }
           base.Add(QuestionFile.CreateQuestionFile(mediaFile));
        }

        public void Add(string path)
        {

        }
    }
}
