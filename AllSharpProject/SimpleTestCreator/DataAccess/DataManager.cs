using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using Microsoft.Win32;
using SimpleTestCreator.Views;
using TestLibrary;
using  System.Collections.ObjectModel;
using TestLibrary.Helpers;

namespace SimpleTestCreator.DataAccess
{
    public class TestChangedEventArgs : EventArgs
    {
        public Test test { get; private set; }
        public TestChangedEventArgs(Test test) : base()
        {
            this.test = test;
        }
    }


    public class DataManager : Helpers.NotificationObject
    {
        private Test test;

        private string currentFile = null;

        public Test Test
        {
            get 
            {
                if (test == null)
                { return new Test(); }
                return test; 
            }
        }

        public bool CanSave
        {
            get
            {
                if (test != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public event EventHandler<TestChangedEventArgs> OnTestChange = delegate { };

        public void AddQuestion(QuestionBase q)
        {
            if (this.test.Questions.Contains(q) == true)
            { return; }

            this.test.Questions.Add(q);
            this.test.Questions.OrderBy(t => t.Number);
            this.OnTestChange(this,new TestChangedEventArgs(this.test));
            RaisePropertyChanged(() => this.Test);
        }
        public void ChangeQuestion(QuestionBase _base, QuestionBase _new)
        {
            this.test.Questions[this.test.Questions.IndexOf(_base)] = _new;
            this.OnTestChange(this, new TestChangedEventArgs(this.test));
            RaisePropertyChanged(() => this.Test);
        }
        public void ChangeQuestion(QuestionBase q)
        {
            ChangeQuestion(this.test.Questions.First(i => i.ID == q.ID), q);
        }

        public void RemoveQuestion(QuestionBase q)
        {
            RemoveQuestion(q.ID);
        }
        public void RemoveQuestion(string id)
        {
            this.test.Questions.Remove(this.test[id]);
            this.test.Questions.OrderBy(t => t.Number);

            for (int i = 0; i < this.test.Questions.Count; i++)
            {
                this.test.Questions[i].Number = i;
            }

            this.OnTestChange(this, new TestChangedEventArgs(this.test));
            RaisePropertyChanged(() => this.Test);
        }

        public void RemoveQuestion(int num)
        {
            RemoveQuestion(this.test.Questions.First(q => q.Number == num).ID);
        }

        public void ReplaceQuestionNumber(int last_num, int new_num)
        {
            if (!(last_num >= 0 && new_num >=0 && new_num!=last_num && last_num<test.Questions.Count && new_num<test.Questions.Count))
            {
                return;
            }

            QuestionBase buf;
            buf = this.test.Questions[new_num];
            this.test.Questions[new_num] = this.test.Questions[last_num];
            this.test.Questions[last_num] = buf;           

            for (int i = 0; i < test.Questions.Count; i++)
            {
                test.Questions[i].Number = i;
            }
            RaisePropertyChanged(() => this.Test);

        }

        public void ChangeTestSettings(TestSettings settings)
        {
            this.test.Settings = settings;
            this.OnTestChange(this, new TestChangedEventArgs(this.test));
            RaisePropertyChanged(() => this.Test);
        }

        public DataManager(string testFile)
        {           
            Load(testFile);
        }

        public DataManager()
        {
            this.test = new Test();
        }

        public void Load(string testFile)
        {
            try
            {
                test = (Test)TestLibrary.Helpers.SaveMaster.Open(typeof(Test), testFile, TestLibrary.Helpers.SerializationMethod.XML);
                this.currentFile = testFile;
                this.OnTestChange(this, new TestChangedEventArgs(this.test));
                RaisePropertyChanged(() => this.Test);
            }
            catch(Exception ex)
            {
                throw new ArgumentOutOfRangeException("testFile");
            }
        }

        public void Load(Test test)
        {
            this.test = test;
            this.OnTestChange(this, new TestChangedEventArgs(this.test));
            RaisePropertyChanged(() => this.Test);
        }

        public void Save()
        {
            if (currentFile == null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "test files (*.xml)|*.xml|All files (*.*)|*.*";
                dlg.FilterIndex = 2;
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog() == true)
                {
                    this.currentFile = dlg.FileName;
                }
            }

            TestLibrary.Helpers.SaveMaster.Save(this.test, currentFile, TestLibrary.Helpers.SerializationMethod.XML);
        }

        public void Save(string filename)
        {
           TestLibrary.Helpers.SaveMaster.Save(this.test,filename,SerializationMethod.XML);           
        }

        public void SaveForAndroid(string filename)
        {
            string test_pc = TestLibrary.Helpers.SaveMaster.Serialize(this.Test, SerializationMethod.XML);
            TestLibrary.Helpers.ClientXmlConverter converter = new ClientXmlConverter();
            string test_android = (string)converter.Convert(test_pc, null, null, null);

            FileStream f = File.Create(filename);
            byte[] bytes = Encoding.UTF8.GetBytes(test_android);
            f.Write(bytes, 0, bytes.Length);
            f.Close();
        }



        #region Debug
        public static ObservableCollection<QuestionBase> _Debug_GetTestQuestions()
        {
            ObservableCollection<QuestionBase> res;
            res = new ObservableCollection<QuestionBase>();

            {
                QSingleChoice q = new QSingleChoice()
                {
                    Name = "A1",
                    Number = 1,
                    Data = new MediaData("Lol1")
                    {
                       Files = new ObservableCollection<MediaFile>()
                       {
                           new MediaFile("TEXT1")
                       }
                    }
                };

                Answer ans = new Answer(new MediaData("Answer1"), true);
                q.Answers.Add(ans);

                Answer ans2 = new Answer(new MediaData("Answer2"), false);
                q.Answers.Add(ans2);

                res.Add(q);
            }


            {
                QSingleChoice q2 = new QSingleChoice()
                {
                    Name = "A2",
                    Number = 2,
                    Data = new MediaData("Lol2")
                    {
                        Files = new ObservableCollection<MediaFile>()
                       {
                           new MediaFile("TEXT2")
                       }
                    }
                };

                var ans = new Answer(new MediaData("Answer1"), false);
                q2.Answers.Add(ans);

                var ans2 = new Answer(new MediaData("Answer2"), true);
                q2.Answers.Add(ans2);

                res.Add(q2);
            }

            {
                QMultiChoice q3 = new QMultiChoice()
                {
                    Name = "A3",
                    Number = 3,
                    Data = new MediaData("Lol3")
                };


                var ans = new MultiChoiceBallAswer(new MediaData("Answer1"), true);
                q3.Answers.Add(ans);

                var ans2 = new MultiChoiceBallAswer(new MediaData("Answer2"), true);
                q3.Answers.Add(ans2);

                res.Add(q3);
            }

            return res;
        }
        public static Test _Debug_GetTest()
        {
            Test t = new Test();
            t.Questions = _Debug_GetTestQuestions();
            t.Settings = new TestSettings();
            t.Settings.Name = "test1";

            return t;

        }
        #endregion


    }
}
