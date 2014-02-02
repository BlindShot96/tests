using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using SimpleTestCreator.DataAccess;
using SimpleTestCreator.Views;
using TestLibrary;

namespace SimpleTestCreator.ViewModels2
{

    public class MainViewModel : ViewModelBase
    {
        #region DataManager
        private DataManager _manager = new DataManager();
        public DataManager Manager
        {
            get { return _manager; }
        }
        #endregion //DataManger

        #region ShowingQuestion

        private QuestionViewModelBase _showingQuestion;
        public QuestionViewModelBase ShowingQuestion
        {
            get { return _showingQuestion; }
            set
            {
                _showingQuestion = value; this.OnPropertyChanged("ShowingQuestion");
                this.OnUpdateShowingQuestionHandler(this,new EventArgs());
            }
        }

        #endregion//ShowingQuestion

        /// <summary>
        /// Questions List
        /// </summary>
        public ObservableCollection<QuestionBase> Questions
        {
            get
            {
                return _manager.Test.Questions;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this._manager.Test.Questions = value;
                OnPropertyChanged("Questions");
            }
        }

        /// <summary>
        /// List of Question View Models for show in list or in question control 
        /// </summary>
        public ObservableCollection<QuestionViewModelBase> QuestionViewModels
        {
            get 
            {
                var res = new ObservableCollection<QuestionViewModelBase>();
                foreach (QuestionBase question in this.Manager.Test.Questions)
                {
                    QuestionViewModelBase qb = QuestionViewModelFactory.GetQuestionViewModelBase(question);
                    res.Add(qb);  
                }
                return res;
            }
        }

        /// <summary>
        /// Test Setting & Pararmeters & MediaData
        /// </summary>
        public Test TestSettings
        {
            get
            {
                if (_manager.Test != null)
                {
                    return _manager.Test;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this._manager.Test.Data = value.Data;
                this._manager.Test.Settings = value.Settings;
                OnPropertyChanged("TestSettings");
            }
        }

        public MainViewModel()
        {
            //_manager.Load(DataManager._Debug_GetTest());
        }

        public MainViewModel(string testFile)
        {
            _manager.Load(testFile);
        }

        /// <summary>
        /// задать показываемый вопрос
        /// </summary>
        /// <param name="q">вопрос</param>
        public void ShowQuestion(QuestionBase q)
        {
            this.ShowingQuestion = QuestionViewModelFactory.GetQuestionViewModelBase(q);
            this.OnUpdateQuestionsListHandler(this,new EventArgs());
        }

        /// <summary>
        /// задать показываемый вопрос
        /// </summary>
        /// <param name="num">номер вопроса</param>
        public void ShowQuestion(int num)
        {
          ShowQuestion(this._manager.Test[num]);
        }

        /// <summary>
        ///  задать показываемый вопрос
        /// </summary>
        /// <param name="id">ID вопроса</param>
        public void ShowQuestion(string id)
        {
            ShowQuestion(this._manager.Test[id]);
        }

        #region Event_update_questions_view_models

        public event EventHandler OnUpdateQuestionsListHandler = (sender, args) => { };

        #endregion

        #region Event_UpdateShowingQuestion

        public event EventHandler OnUpdateShowingQuestionHandler = (sender, args) => { };

        #endregion


        #region Command_New

        public ICommand NewTestCommand
        {
            get
            {
                return new RelayCommand(NewTestCommand_Executed, NewTestCommand_CanExecute);
            }
        }

        private void NewTestCommand_Executed(object sender)
        {
            MainWindow2 window2 = new MainWindow2();
            window2.Show();
        }

        private bool NewTestCommand_CanExecute(object sender)
        {
            return true;
        }

        #endregion

        #region Command_Open

        public ICommand OpenTestCommand
        {
            get
            {
                return new RelayCommand(OpenCommand_Executed, OpenCommand_CanExecute);
            }
        }

        private void OpenCommand_Executed(object sender)
        {
           OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    Manager.Load(dlg.FileName);
                    this.OnUpdateQuestionsListHandler(this,new EventArgs());
                    this.OnUpdateShowingQuestionHandler(this,new EventArgs());
                    this.OnPropertyChanged("Manager");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка при открытии файла теста");
                }
            }
        }

        private bool OpenCommand_CanExecute(object sender)
        {
            return true;
        }

        #endregion

        #region Command_Save

        public ICommand SaveTestCommand
        {
            get { return new RelayCommand(SaveCommand_Executed,SaveCommand_CanExecute); }
        }

        private void SaveCommand_Executed(object sender)
        {
            try
            {
                _manager.Save();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString(),"Ошибка при сохранении теста");
            }
        }

        private bool SaveCommand_CanExecute(object sender)
        {
            return _manager.CanSave;
        }

        #endregion

        #region Command_SaveAs

        public ICommand SaveAsTestCommand
        {
            get { return new RelayCommand(SaveAsCommand_Executed,SaveAsCommand_CanExecute); }
        }

        private void SaveAsCommand_Executed(object sender)
        {
            SaveFileDialog dlg = new SaveFileDialog(); 
            dlg.Filter = "test files (*.xml)|*.xml|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    _manager.Save(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка при сохранении файла теста");
                }
            }
        }

        private bool SaveAsCommand_CanExecute(object sender)
        {
            return _manager.CanSave;
        }

        #endregion

        #region Command_AddQuestion

        public ICommand AddQuestionCommand
        {
            get { return new RelayCommand(AddQuestion_Executed,AddQuestion_CanExecute); }
        }

        public ICommand AddQuestion2Command
        {
            get
            {
                return new RelayCommand(o =>
                {
                    if (o is QuestionType)
                    {
                        var ChoosenType = (QuestionType) o;

                        QuestionBase question = null;
                        if (ChoosenType == QuestionType.SingleChoice)
                        {
                            question = new QSingleChoice();
                        }
                        if (ChoosenType == QuestionType.MultiChoice)
                        {
                            question = new QMultiChoice();
                        }

                        if (ChoosenType == QuestionType.TextQuestion)
                        {
                            question = new QTextChoice();
                        }

                        if (question != null)
                        {
                            question.Name = "Question " + (this.Questions.Count + 1);
                            question.Number = this.Questions.Count;

                            this._manager.AddQuestion(question);
                            OnUpdateQuestionsListHandler(this, new EventArgs());
                            ShowQuestion(question);
                        }
                    }
                },AddQuestion_CanExecute);
            }
        }

        private void AddQuestion_Executed(object sender)
        {
            QuestionTypeChooseDialog dlg = new QuestionTypeChooseDialog(new List<QuestionType>(){QuestionType.SingleChoice,QuestionType.MultiChoice,QuestionType.TextQuestion});
            if (dlg.ShowDialog() == true)
            {
                QuestionBase question = null;
                if (dlg.ChoosenType == QuestionType.SingleChoice)
                {
                    question = new QSingleChoice();
                }
                if (dlg.ChoosenType == QuestionType.MultiChoice)
                {
                    question = new QMultiChoice();
                }

                if (dlg.ChoosenType == QuestionType.TextQuestion)
                {
                    question = new QTextChoice();
                }

                if (question != null)
                {
                    question.Name = "Question " + (this.Questions.Count+1);
                    question.Number = this.Questions.Count;

                    this._manager.AddQuestion(question);
                    OnUpdateQuestionsListHandler(this,new EventArgs());
                    ShowQuestion(question);
                }
            }
        }

        private bool AddQuestion_CanExecute(object sender)
        {
            if (_manager.Test != null &&  _manager.Test.Questions != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Command_RemoveQuestion

        public ICommand RemoveQuestionCommand
        {
            get { return new RelayCommand(RemoveQuestion_Executed, RemoveQuestion_CanExecute); }
        }

        private void RemoveQuestion_Executed(object sender)
        {
          this._manager.RemoveQuestion(this.ShowingQuestion.QuestionModel);
          OnUpdateQuestionsListHandler(this, new EventArgs());
        }

        private bool RemoveQuestion_CanExecute(object sender)
        {
            return ShowingQuestion != null;
        }
        #endregion

        public ICommand OpenTestSettingsCommand
        {
            get
            {
                return  new RelayCommand(
                    o =>
                    {
                        var window = new TestSettingsWindow(new TestSettingsViewModel(this.Manager.Test));
                        window.ShowDialog();
                    },
                    o =>
                    {
                        return this.Manager != null && this.Manager.Test != null;
                    });
            }
        }

        public ICommand UpSelectedQuestionCommand
        {
            get
            {
                return new RelayCommand(
                    o =>
                    {
                        Manager.ReplaceQuestionNumber(this.ShowingQuestion.QuestionModel.Number,
                            this.ShowingQuestion.QuestionModel.Number - 1);
                        OnUpdateQuestionsListHandler(this.ShowingQuestion.QuestionModel.Number, new EventArgs());
                    },
                    o => this.ShowingQuestion != null && this.ShowingQuestion.QuestionModel.Number > 0);
            }
        }

        public ICommand DownSelectedQuestionCommand
        {
            get
            {
                return new RelayCommand(
                    o =>
                    {
                        this.Manager.ReplaceQuestionNumber(this.ShowingQuestion.QuestionModel.Number,
                            this.ShowingQuestion.QuestionModel.Number + 1);
                        OnUpdateQuestionsListHandler(this.ShowingQuestion.QuestionModel.Number , new EventArgs());
                    },
                    o => this.ShowingQuestion != null && this.ShowingQuestion.QuestionModel.Number < this.Questions.Count-1);
            }
        }

        public ICommand SaveAsAndroidCommand
        {
            get
            {
                return new RelayCommand(
                    o =>
                    {
                        SaveFileDialog dlg = new SaveFileDialog();
                        dlg.Filter = "test files (*.xml)|*.xml|All files (*.*)|*.*";
                        dlg.FilterIndex = 2;
                        dlg.RestoreDirectory = true;
                        if (dlg.ShowDialog() == true)
                        {
                            try
                            {
                                _manager.SaveForAndroid(dlg.FileName);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Ошибка при сохранении файла теста");
                            }
                        }
                    },
                    o =>
                    {
                        return _manager.CanSave;
                    });
            }
        }

    }
}
