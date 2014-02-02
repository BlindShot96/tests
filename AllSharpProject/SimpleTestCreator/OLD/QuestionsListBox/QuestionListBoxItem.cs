using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TestLibrary;

namespace SimpleTestCreator.Controls.QuestionsListBox
{
    public class QuestionListBoxItem: INotifyPropertyChanged
        {
            private Dictionary<string,string> TypeToImage = new Dictionary<string,string>
            {
               {"SingleChoice",System.AppDomain.CurrentDomain.BaseDirectory+@"QuestionTypeImages\SingleChoice.bmp"},
               {"MultiChoice",System.AppDomain.CurrentDomain.BaseDirectory+@"QuestionTypeImages\MultiChoice.bmp"},
               {"TextQuestion",System.AppDomain.CurrentDomain.BaseDirectory+@"QuestionTypeImages\TextAnswer.bmp"},
            };
            
            private string name;
            private string type;
            private BitmapImage typeImage;

            public string QuestionID;

            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }

            public string Type
            {
                get { return type; }
                set
                {
                    type = value; OnPropertyChanged("Type");
                    try
                    {
                       this.typeImage = GetTypeImage(TypeToImage[type]);
                    }
                    catch
                    {
                        this.typeImage = GetTypeImage(TypeToImage["SingleChoice"]);
                    }
                }
            }

            public BitmapImage TypeImage { get { return typeImage; } private set { this.typeImage = value; } }

            public event PropertyChangedEventHandler PropertyChanged;

            public QuestionListBoxItem(string name, string type, string id)
            {
                this.name = name;
                this.Type = type;
                this.QuestionID = id;
            }

            public QuestionListBoxItem(QuestionBase q)
            {
                this.name = q.Name;
                this.Type = q.GetTypeOfQuestion();
                this.QuestionID = q.ID;
            }

            private static BitmapImage GetTypeImage(string path)
            {
                try
                {
                    BitmapImage res = new BitmapImage(new Uri(path));
                    return res;
                }
                catch
                {
                    return null;
                }
            }

            protected void OnPropertyChanged(string info)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(info));
                }
            }

            public override string ToString()
            {
                return Name.ToString();
            }


        }
}
