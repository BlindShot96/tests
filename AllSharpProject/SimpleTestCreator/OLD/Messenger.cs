using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace SimpleTestCreator.Common
{

    public class Messenger
    {                     
        public event Action<QuestionBase> RemoveQuestionEvent = delegate { };
        public event Action AddNewQuestionEvent = delegate { };
        public event Action<QuestionBase> ShowQuestionEvent = delegate { };
        public event Action<QuestionBase> QuestionChangedEvent = delegate { };
        public event Action SaveTestEvent = delegate { };
        public event Action SaveTestAsEvent = delegate { };
        

        public void AddNewQuestion()
        {
            AddNewQuestionEvent();
        }

        public void RemoveQuestion(int num)
        {
            try
            {
                QuestionBase q = Manager.GetQuestion(num);
                RemoveQuestion(q);
            }
            catch(Exception e)
            {
                throw new Exception("", e);
            }
        }

        public void RemoveQuestion(string Id)
        {
            try
            {
                QuestionBase q = Manager.GetQuestion(Id);
                RemoveQuestion(q);
            }
            catch (Exception e)
            {
                throw new Exception("", e);
            }
        }

        public void RemoveQuestion(QuestionBase q)
        {
            RemoveQuestionEvent(q);
        }

        public void ShowQuestion(int num)
        {
            try
            {
                QuestionBase q = Manager.GetQuestion(num);
                ShowQuestion(q);
            }
            catch (Exception e)
            {
                throw new Exception("", e);
            }
        }

        public void ShowQuestion(string id)
        {
            try
            {
                QuestionBase q = Manager.GetQuestion(id);
                ShowQuestion(q);
            }
            catch (Exception e)
            {
                throw new Exception("", e);
            }
        }

        public void ShowQuestion(QuestionBase q)
        {
            try
            {
                ShowQuestionEvent(q);
            }
            catch (Exception e)
            {
                throw new Exception("", e);
            }
        }

        public void SaveTest()
        {
            Manager.SaveTest();
            SaveTestEvent();
        }

        public void SaveTestAs()
        {
            Manager.SaveTestAs();
            SaveTestAsEvent();
        }

    }
}
