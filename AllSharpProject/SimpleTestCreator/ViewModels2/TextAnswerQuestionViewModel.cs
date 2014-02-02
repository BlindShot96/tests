using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestLibrary;

namespace SimpleTestCreator.ViewModels2
{
    public class TextAnswerQuestionViewModel  : QuestionViewModelBase
    {
        /// <summary>
        /// OLD: ответ в виде строки
        /// </summary>
        public string TextAnswer {
            get
            {
                if (this.QuestionModel.Answers.Count == 0)
                {
                    addNewAnswer();
                }

                try
                {
                    return this.QuestionModel.Answers[0].Data.Text;
                }
                catch (Exception)
                {

                    return "";
                }
            }
            set
            {

                if (this.QuestionModel.Answers.Count == 0)
                {
                    addNewAnswer();
                }

                try
                {
                     this.QuestionModel.Answers[0].Data.Text = value;
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// список ответов на вопрос
        /// </summary>
        public ObservableCollection<AnswerBase> Answers
        {
            get { return this.QuestionModel.Answers; }
            set { this.QuestionModel.Answers = value; }
        }

        /// <summary>
        /// выбранный ответ
        /// </summary>
        public AnswerBase SelectedAnswer { get; set; }

        private void addNewAnswer()
        {
            if (this.QuestionModel.Answers.Count == 0)
            {
                this.QuestionModel.Answers.Add(new Answer(new MediaData("")){ID = new Random().Next().ToString()});
            }
        }

        public string Text {
            get
            {
                return this.QuestionModel.Data.Text;
            }
            set
            {
                this.QuestionModel.Data.Text = value;
            }
        }

        public MediaData Data
        {
            get { return this.QuestionModel.Data; }
            set { this.QuestionModel.Data = value; }
        }

        public TextAnswerQuestionViewModel(QTextChoice questionModel)
            : base(questionModel)
        { }

        #region Command_AddAnswer

        public ICommand AddAnswerCommand
        {
            get { return new RelayCommand(AddAnswer_Executed, AddAnswer_CanExecute); }
        }

        private void AddAnswer_Executed(object sender)
        {
            this.QuestionModel.Answers.Add(new Answer(new MediaData("Ответ")) { ID = new Random().Next().ToString() });
        }

        private bool AddAnswer_CanExecute(object sender)
        {
            return this.QuestionModel != null;
        }

        #endregion

        #region Command_RemoveAnswer

        public ICommand RemoveAnswerCommand
        {
            get { return new RelayCommand(RemoveAnswer_Executed, RemoveAnswer_CanExecute); }
        }

        private void RemoveAnswer_Executed(object param)
        {
            Answer sa = (Answer)this.SelectedAnswer;
            if (param != null && param is Answer)
            {
                sa = (Answer)param;
            }

            if (sa == null)
            {
                MessageBox.Show("Ошибка при выполнении команды");
            }

            this.QuestionModel.Answers.Remove(sa);
        }

        private bool RemoveAnswer_CanExecute(object sender)
        {
            if (sender != null)
            {
                return sender is Answer;
            }
            else
            {
                return SelectedAnswer != null;
            }

        }

        #endregion
       
    }
}
