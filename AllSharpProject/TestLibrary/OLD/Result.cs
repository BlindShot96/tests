using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLibrary
{
    /// <summary>
    /// класс для расчёта оценок
    /// </summary>
    public class Result
    {
        /// <summary>
        /// оценка за тест
        /// </summary>
        public int Mark;

        /// <summary>
        /// процент правильных ответов
        /// </summary>
        public float Percent;

        /// <summary>
        /// кол-во правильных ответов
        /// </summary>
        public int RightAnswers;

        /// <summary>
        /// кол-во неверных ответов
        /// </summary>
        public int WrongAnswers;

        /// <summary>
        /// комментарий к результату
        /// </summary>
        public MediaData Comment;

        public Result(int RightAnswers, int WrongAnswers)
        {
            this.RightAnswers = RightAnswers;
            this.WrongAnswers = WrongAnswers;
            this.Mark = 0;

            this.Percent = (float)((float)(this.RightAnswers)/((float)this.RightAnswers+(float)this.WrongAnswers));

            if (this.Percent >= 0 && this.Percent <= 0.3) { this.Mark = 2; }
            if (this.Percent >= 0.31 && this.Percent <= 0.7) { this.Mark = 3; }
            if (this.Percent >= 0.7 && this.Percent <= 0.9) { this.Mark = 4; }
            if (this.Percent >= 0.91 && this.Percent <= 1) { this.Mark = 5; } 
        }
    }
}
