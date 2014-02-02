using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLibrary
{
    class TestResult
    {
        /// <summary>
        /// оценка за тест
        /// </summary>
        public int Mark;

        /// <summary>
        /// данные комментария
        /// </summary>
        public MediaData Comment
        {
            get
            {
                if (Comment == null)
                {
                    return new MediaData(MediaData.Null);
                }
                else
                {
                    return Comment;
                }
            }
            set { this.Comment = value; }
        }
    }
}
