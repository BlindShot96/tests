using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Input;

using TestLibrary;

namespace SimpleTestCreator.ViewModels2
{
    public class SingleChoiceQuestionViewModel : QuestionViewModelBase
    {
        public Answer SelectedAnswer { get; set; }

        public ObservableCollection<AnswerBase> Answers
        {
            get { return this.QuestionModel.Answers; }
            set { this.QuestionModel.Answers = value; }
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

        public SingleChoiceQuestionViewModel(QSingleChoice questionModel)
            : base(questionModel)
        { }

        public void SetAnswerTrue(Answer ans)
        {
            if (this.QuestionModel.Answers.Contains(ans))
            {
                throw new ArgumentOutOfRangeException();
            }
    
            foreach (Answer item in this.QuestionModel.Answers)
            {
                item.IsTrue = false;
            }
            ans.IsTrue = true;
        }

        #region Command_ShowAnswer

        public ICommand ShowAnswerCommand
        {
            get { return new RelayCommand(ShowAnswer_Executed, ShowAnswer_CanExecute); }
        }

        private void ShowAnswer_Executed(object param)
        {
            Answer sa = this.SelectedAnswer;
            if (param != null && param is Answer)
            {
                sa = (Answer)param;
            }

            if (sa == null)
            {
                MessageBox.Show("Ошибка при выполнении команды");
            }

            var f2 = new SingleChoiceAnswerWindow(sa);
            f2.ShowDialog();
        }

        private bool ShowAnswer_CanExecute(object sender)
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

        #region Command_AddAnswer

        public ICommand AddAnswerCommand
        {
            get { return new RelayCommand(AddAnswer_Executed, AddAnswer_CanExecute); }
        }

        private void AddAnswer_Executed(object sender)
        {
            //SingleChoiceAnswerWindow f2 = new SingleChoiceAnswerWindow();
            //f2.ShowDialog();

            //if (f2.IsOk == true)
            //{
            //    if (f2.IsNew == true)
            //    {
            //        this.QuestionModel.Answers.Add(f2.AnswerModel);
            //    }
            //}

            this.QuestionModel.Answers.Add(new Answer(new MediaData("Ответ")){ID = new Random().Next().ToString()});
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
            Answer sa = this.SelectedAnswer;
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
