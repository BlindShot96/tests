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
    public partial class ClientDetailForm : Form
    {
        Client client;
        Test OriginalTest;

        List<QuestionBase> BindingOriginalQuestions = new List<QuestionBase>();
        List<ClientQuestion> BindingClientQuestions = new List<ClientQuestion>();

        public ClientDetailForm(Client client, Test OriginalTest)
        {
            if (client == null) { throw new ArgumentNullException(); }
            if (OriginalTest == null) { throw new ArgumentNullException(); }
            InitializeComponent();
            this.client = client;
            this.OriginalTest = OriginalTest;
            this.Text = client.Data.Name + " " + this.client.Data.LastName + " " + this.client.Data.Group;
            FillInfoBox();
            FillQuestionsBox();
        }

        private void FillInfoBox()
        {
            string res = string.Format(
                   "Имя: {0}" + Environment.NewLine +
                   "Фамилия: {1}" + Environment.NewLine +
                   "Группа: {2}" + Environment.NewLine +
                   "Балл: {3}" + Environment.NewLine +
                   "Процент правильности: {4}" + Environment.NewLine +
                   "Оценка: {5}" + Environment.NewLine,
                   this.client.Data.Name,
                   this.client.Data.LastName,
                   this.client.Data.Group,
                   this.client.Data.Result.ClientBalls,
                   this.client.Data.Result.Percent,
                   this.client.Data.Result.Mark
                );

            this.InfoBox.Clear();
            this.InfoBox.Text = res;
        }

        private void FillQuestionsBox()
        {
            foreach (ClientQuestion clientQuestion in this.client.Data.Report.Questions)
            {
                QuestionBase q = client.Data.Report.FindOriginalQuestion(clientQuestion,ref OriginalTest);
                QuestionsBox.Items.Add(q.Name);
                this.BindingOriginalQuestions.Add(q);
                this.BindingClientQuestions.Add(clientQuestion);
            }
            //foreach (QuestionBase q in OriginalTest.Questions)
            //{
            //    QuestionsBox.Items.Add(q.Name);
            //    this.BindingOriginalQuestions.Add(q);
            //    ClientQuestion cq = this.client.Data.Report.Questions[q.Number];
            //    this.BindingClientQuestions.Add(cq);
            //}
        }

        private void QuestionsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClientQuestion cq = this.BindingClientQuestions[this.QuestionsBox.SelectedIndex];
            QuestionBase q = this.BindingOriginalQuestions[this.QuestionsBox.SelectedIndex];
            QuestionInfoForm f2 = new QuestionInfoForm(client, q);
            f2.Show();
        }
    }
}
