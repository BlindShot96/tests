using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TestLibrary
{
    public class Answer : AnswerBase
    {
        static Random rnd = new Random();
        public Answer()
        {
            //this.id = rnd.Next(0, 100000).ToString();
            //this.Data = new MediaData();
        }

        public Answer(MediaData Data, bool IsTrue):this()
        {
            if (Data == null)
            {
               Data = new MediaData();
            }
            this.Data = Data;

            this.IsTrue = IsTrue;
        }

        public Answer(MediaData Data) :this()
        {
            if (Data == null)
            {
                Data = new MediaData();
            }
            this.Data = Data;
        }
    }
}
