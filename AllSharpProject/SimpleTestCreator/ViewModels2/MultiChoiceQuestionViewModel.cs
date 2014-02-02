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
    public enum QuestionMode
    {
        ByBalls,
        ByNumber
    }

    public class MultiChoiceQuestionViewModel: QuestionViewModelBase
    {
        public MultiChoiceBallAswer SelectedAnswer { get; set; }

        public ObservableCollection<MultiChoiceBallAswer> Answers
        {
            get { return ((QMultiChoice)this.QuestionModel).Answers; }
            set { ((QMultiChoice)this.QuestionModel).Answers = value; }
        }
       

        public string Text
        {
            get
            {
                return this.QuestionModel.Data.Text;
            }
            set
            {
                this.QuestionModel.Data.Text = value;
            }
        }

        private QuestionMode _questionMode = QuestionMode.ByNumber;
        /// <summary>
        /// способ проверки вопроса
        /// </summary>
        public QuestionMode QuestionMode
        {
            get
            {
                return _questionMode;
            }
            set
            {
                switch (value)
                {
                    case QuestionMode.ByBalls:
                    {
                        ((QMultiChoice) this.QuestionModel).VerifyByBalls = true;
                        break;
                    }
                    case QuestionMode.ByNumber:
                    {
                        ((QMultiChoice) this.QuestionModel).VerifyByBalls = false;
                        break;
                    }
                }
                _questionMode = value;
            }
        }

      
        public MediaData Data
        {
            get { return this.QuestionModel.Data; }
            set { this.QuestionModel.Data = value; }
        }

        public MultiChoiceQuestionViewModel(QMultiChoice questionModel)
            : base(questionModel)
        { }

        #region Command_ShowAnswer

        public ICommand ShowAnswerCommand
        {
            get { return new RelayCommand(ShowAnswer_Executed, ShowAnswer_CanExecute); }
        }

        private void ShowAnswer_Executed(object param)
        {
            MultiChoiceBallAswer sa = this.SelectedAnswer;
            if (param != null && param is MultiChoiceBallAswer)
            {
                sa = (MultiChoiceBallAswer)param;
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
                return sender is MultiChoiceBallAswer;
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
            ((QMultiChoice)this.QuestionModel).Answers.Add(new MultiChoiceBallAswer(new MediaData("Ответ")){ID = new Random().Next().ToString()});
        }

        private bool AddAnswer_CanExecute(object sender)
        {
            return this.QuestionModel !=null;
        }

        #endregion

        #region Command_RemoveAnswer

        public ICommand RemoveAnswerCommand
        {
            get { return new RelayCommand(RemoveAnswer_Executed, RemoveAnswer_CanExecute); }
        }

        private void RemoveAnswer_Executed(object param)
        {
            MultiChoiceBallAswer sa = this.SelectedAnswer;
            if (param != null && param is MultiChoiceBallAswer)
            {
                sa = (MultiChoiceBallAswer)param;
            }

            if (sa == null)
            {
                MessageBox.Show("Ошибка при выполнении команды");
            }

            ((QMultiChoice)this.QuestionModel).Answers.Remove(sa);
            this.OnPropertyChanged("Answers");
        }

        private bool RemoveAnswer_CanExecute(object sender)
        {
            if (sender != null)
            {
                return sender is MultiChoiceBallAswer;
            }
            else
            {
                return SelectedAnswer != null;
            }

        }

        #endregion

        //public ICommand UpMultiChoiceBallAnswerBallCommand
        //{
        //    get { return new RelayCommand(
        //        o =>
        //        {
        //            ((MultiChoiceBallAswer) this.SelectedAnswer).Ball++;
        //        },
        //        o => this.SelectedAnswer!=null && this.QuestionMode == QuestionMode.ByBalls);}
        //}
        //public ICommand DownMultiChoiceBallAnswerBallCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(
        //            o =>
        //            {
        //                if (((MultiChoiceBallAswer) this.SelectedAnswer).Ball > 0)
        //                {
        //                    ((MultiChoiceBallAswer) this.SelectedAnswer).Ball--;
        //                }
        //            },
        //            o => this.SelectedAnswer != null && this.QuestionMode == QuestionMode.ByBalls);
        //    }
        //}

    }
}
