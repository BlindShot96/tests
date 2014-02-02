using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SimpleTestCreator.DataAccess;
using SimpleTestCreator.Views;
using TestLibrary;

namespace SimpleTestCreator.ViewModels2
{

    public static class QuestionViewModelFactory
    {
        public static QuestionViewModelBase GetQuestionViewModelBase(QuestionBase q)
        {
            switch (q.Type)
            {
                case QuestionType.SingleChoice:
                {
                    return  new SingleChoiceQuestionViewModel((QSingleChoice)q);
                }
                case QuestionType.MultiChoice:
                {
                    return new MultiChoiceQuestionViewModel((QMultiChoice)q);
                }
                case QuestionType.TextQuestion:
                {
                    return new TextAnswerQuestionViewModel((QTextChoice)q);
                }
                default:
                {
                    return null;
                }
            }
        }
    }

    public static class QuestionControlFactory
    {
        public static UserControl GetQuestionControl(QuestionBase q)
        {
            switch (q.Type)
            {
                case QuestionType.SingleChoice:
                {
                    return new SQView((SingleChoiceQuestionViewModel)QuestionViewModelFactory.GetQuestionViewModelBase(q));
                }
                case QuestionType.MultiChoice:
                {
                    return  new MQView((MultiChoiceQuestionViewModel)QuestionViewModelFactory.GetQuestionViewModelBase(q));
                }
                case QuestionType.TextQuestion:
                {
                    return new TQView((TextAnswerQuestionViewModel)QuestionViewModelFactory.GetQuestionViewModelBase(q));
                }
                default:
                {
                    return null;
                }
            }
        }

        public static UserControl GetQuestionControl(QuestionViewModelBase q)
        {
            if (q == null)
            {
                return null;
            }
            switch (q.QuestionModel.Type)
            {
                case QuestionType.SingleChoice:
                 {
                        return new SQView((SingleChoiceQuestionViewModel)q);
                 }
                case QuestionType.MultiChoice:
                {
                    return new MQView((MultiChoiceQuestionViewModel)q);
                }
                case QuestionType.TextQuestion:
                {
                    return new TQView((TextAnswerQuestionViewModel)q);
                }
                default:
                    {
                        return null;
                    }
            }
        }
    }

    public abstract class QuestionViewModelBase:ViewModelBase
    {

        public virtual string Header
        {
            get
            {
                return "Вопрос: " + this.Name + "   Тип: " + this.Type;
            }
        }

        public virtual string Name {
            get { return QuestionModel.Name; }
            set
            {
                this.QuestionModel.Name = value; OnPropertyChanged("Name");
                OnPropertyChanged("Header");
            }
        }

        public virtual string Type {
            get { return QuestionModel.GetTypeOfQuestion(); }
        }

        private QuestionBase questionModel;
        public virtual QuestionBase QuestionModel
        {
            get { return questionModel; }
            set { this.questionModel = value; OnPropertyChanged("QuestionModel"); }
        }

        public virtual DataElementsViewModel DataElementsViewModel
        {
            get
            {
                return new DataElementsViewModel(this.QuestionModel.Data);
            }
            set
            {
                this.QuestionModel.Data = value.DataModel;
            }
        }

        protected QuestionViewModelBase(QuestionBase questionModel)
        {
            if (questionModel == null) { throw new ArgumentNullException(); }

            this.questionModel = questionModel;
        }

        protected QuestionViewModelBase()
        {}

        public void SaveToTest(DataManager manger)
        {
            if (manger == null) { throw new ArgumentNullException(); }
            manger.AddQuestion(questionModel);
        }

        public override string ToString()
        {
            return QuestionModel.ToString();
        }
    }
}
