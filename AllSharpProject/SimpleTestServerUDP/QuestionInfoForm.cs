using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleTestServerUDP.Internet;
using TestLibrary;
using TestLibrary.ClientEdit;

namespace SimpleTestServerUDP
{
    public partial class QuestionInfoForm : Form
    {
        private QuestionBase Question;
        private Client client;

        public QuestionInfoForm(Client client, QuestionBase question)
        {
            InitializeComponent();
            this.Question = question;
            this.client = client;
            FillQuestionBox();
            FillClientAnswersBox();
            this.Text = " Вопрос " + question.Name +"  Ученика: " + client.Data.Name + " " + this.client.Data.LastName + " " + this.client.Data.Group ;        
        }

        private void FillQuestionBox()
        {
            //string Qtext = "Текст вопроса: " + this.Question.Data.Text
            //    + Environment.NewLine + "Тип вопроса: " + this.Question.Type.ToString();

            //string QQues = "Варианты ответа:";
            //foreach (Answer ans in this.Question.Answers)
            //{
            //    QQues += Environment.NewLine + string.Format("Текст: {0}", ans.Data.Text);
            //}

            //string QRQues = "Правильные ответы: ";
            //foreach (RightAnswer ans in this.Question.RightAnswers)
            //{
            //    QRQues += Environment.NewLine + " Ответ: " + ans.Data;
            //}

            //this.QuestionBox.Text = Qtext + Environment.NewLine + QQues + Environment.NewLine + QRQues;
            this.QuestionBox.Text = QuestionToTextParser.GetText(this.Question);
        }

        private void FillClientAnswersBox()
        {
            string res = "";
            ClientQuestion clientQ = null;
            foreach (var clientQuestion in client.Data.Report.Questions)
            {
                if (clientQuestion.QuestionID.Equals(Question.ID))
                {
                    clientQ = clientQuestion;
                }
            }

            if (clientQ != null)
            {
                res = string.Format("Балл: {0} " + Environment.NewLine, clientQ.Ball);
                int n = 1;
                foreach (var clientQuestionAnswer in clientQ.Answers)
                {
                    string ans = "";
                    if (Question.Type == QuestionType.MultiChoice)
                    {
                        foreach (AnswerBase answer in ((QMultiChoice)Question).Answers)
                        {
                            if (answer.ID.Equals(clientQuestionAnswer.Data.Text))
                            {
                                ans = answer.Data.Text;
                            }
                        }
                    }
                    foreach (AnswerBase answer in Question.Answers)
                    {
                        if (answer.ID.Equals(clientQuestionAnswer.Data.Text))
                        {
                            ans = answer.Data.Text;
                        }
                    }
                    if (ans.Equals(""))
                    {
                        ans = clientQuestionAnswer.Data.Text;
                    }

                    res += string.Format("{0}) {1}", n, ans) + Environment.NewLine;
                    n++;
                }
                this.AnswerBox.Text = res;
            }
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class QuestionToTextParser
    {
        public static string GetText(QuestionBase question)
        {
            if (question.Type == QuestionType.SingleChoice)
            {
                return getSQText((QSingleChoice) question);
            }
            if (question.Type == QuestionType.MultiChoice)
            {
                return getMQText((QMultiChoice)question);
            }
            if (question.Type == QuestionType.TextQuestion)
            {
                return getTQText((QTextChoice)question);
            }
            return "";
        }

        private static string getMQText(QMultiChoice question)
        {
            string q_main = string.Format(
                    "Название вопроса: {0}" + Environment.NewLine +
                    "Тип: {1}" + Environment.NewLine +
                    "Максимальный балл: {2}" + Environment.NewLine +
                    "Текст вопроса: {3}" + Environment.NewLine,
                    question.Name,
                    "Множественный выбор",
                    question.MaxBall,
                    question.Data.Text
                 );
            string q_answers = "";
            #region get_answers
            if (question.VerifyByBalls == true)
            {
                int n = 1;
                foreach (MultiChoiceBallAswer ans in question.Answers)
                {
                    string answer;
                    answer = string.Format("{0}) Балл[{1}] {2}", n, ans.Ball, ans.Data.Text);
                    n++;
                    q_answers += answer + Environment.NewLine;
                }
            }
            else
            {
                int n = 1;
                foreach (Answer ans in question.Answers)
                {
                    string answer;
                    if (ans.IsTrue == true)
                    {
                        answer = string.Format("{0}) [Правильный] {1}", n, ans.Data.Text);
                    }
                    else
                    {
                        answer = string.Format("{0}) {1}", n, ans.Data.Text);  
                    }
                    q_answers += answer + Environment.NewLine;
                    n++;
                }
            }

            #endregion

            q_main += Environment.NewLine + q_answers;
            return q_main;
        }
        private static string getSQText(QSingleChoice question)
        {
            string q_main = string.Format(
                   "Название вопроса: {0}" + Environment.NewLine +
                   "Тип: {1}" + Environment.NewLine +
                   "Максимальный балл: {2}" + Environment.NewLine +
                   "Текст вопроса: {3}" + Environment.NewLine,
                   question.Name,
                   "Одиночный  выбор",
                   question.MaxBall,
                   question.Data.Text
                );
            string q_answers = "";
            #region get_answers
                int n = 1;
                foreach (Answer ans in question.Answers)
                {
                    string answer;
                    if (ans.IsTrue == true)
                    {
                        answer = string.Format("{0}) [Правильный] {1}", n, ans.Data.Text);
                    }
                    else
                    {
                        answer = string.Format("{0}) {1}", n, ans.Data.Text);
                    }
                    q_answers += answer + Environment.NewLine;
                    n++;
                }

            #endregion

            q_main += Environment.NewLine + q_answers;
            return q_main;
        }
        private static string getTQText(QTextChoice question)
        {
            string q_main = string.Format(
                  "Название вопроса: {0}" + Environment.NewLine +
                  "Тип: {1}" + Environment.NewLine +
                  "Максимальный балл: {2}" + Environment.NewLine +
                  "Текст вопроса: {3}" + Environment.NewLine,
                  question.Name,
                  "Одиночный  выбор",
                  question.MaxBall,
                  question.Data.Text
               );
            string q_answers = "";
            #region get_answers
            int n = 1;
            foreach (Answer ans in question.Answers)
            {
                string answer;
                answer = string.Format("{0}) [Правильный] {1}", n, ans.Data.Text);

                q_answers += answer + Environment.NewLine;
                n++;
            }

            #endregion

            q_main += Environment.NewLine + q_answers;
            return q_main;
        }
    }

}
