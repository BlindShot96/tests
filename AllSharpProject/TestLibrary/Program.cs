using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using TestLibrary.ClientEdit;
using TestLibrary.Managers;

namespace TestLibrary
{
    static class Program
    {
        
        static void Main()
        {
            ShowSerDeserTest();
            //ClientData client = ShowClientVerify(ref OriginalTest);
            //ClientsManager manager = new ClientsManager();
            //manager.Clients.Add(client.ID, client);

            //Dictionary<string, ClientData> client100 = manager.GetClientsByQuery(new AllBallsQuery());
            //int one = client100.Count;
            //Dictionary<string, ClientData> clientZero = manager.GetClientsByQuery(new NullBallsQuery());
            //one = clientZero.Count;
        }

        public static Test ShowSerDeserTest()
        {

            //MediaFile file1 = new MediaFile("File 1", @"D:\Jellyfish.jpg");
            //MediaFile file2 = new MediaFile("File 2", @"D:\pinguins.jpg");

            MediaData data1 = new MediaData("Data111");
            MediaData data2 = new MediaData("Data222");

            QSingleChoice q1 = new QSingleChoice();
            q1.Data = data1;
            q1.MinBall = 0;
            q1.MaxBall = 1;
            q1.Number = 1;
            //q1.Type = QuestionType.SingleChoice;
            Answer ans1_1 = new Answer();
            ans1_1.Data = new MediaData("answer1");
            ans1_1.IsTrue = true;

            Answer ans1_2 = new Answer();
            ans1_2.Data = new MediaData("answer2");
            ans1_2.IsTrue = false;
            q1.Answers.Add(ans1_1.ID, ans1_1);
            q1.Answers.Add(ans1_2.ID, ans1_2);


            //QSingleChoice q2 = new QSingleChoice();
            //q2.ID = "fgfrt";
            //q2.Data = data2;
            //q2.MinBall = 0;
            //q2.MaxBall = 1;
            //q2.Number = 1;
            //q2.Type = QuestionType.SingleChoice;
            //Answer ans2_1 = new Answer();
            //ans2_1.Data = new MediaData("2answer1");
            //ans2_1.IsTrue = true;
            //ans2_1.ID = ans2_1.Data.Text + ans2_1.IsTrue.ToString();

            //Answer ans2_2 = new Answer();
            //ans2_2.Data = new MediaData("2answer2");
            //ans2_2.IsTrue = false;
            //ans2_2.ID = ans2_2.Data.Text + ans2_2.IsTrue.ToString();
            //q2.Answers.Add(ans2_1.ID, ans2_1);
            //q2.Answers.Add(ans2_2.ID, ans2_2);

            QMultiChoice q2 = new QMultiChoice();
            {
                q2.MinBall = 0;
                q2.MaxBall = 2;
                q2.Number = 2;
               // q2.Type = QuestionType.MultiChoice;
                q2.VerifyByAllBall = true;
                q2.Data = new MediaData("q2data");

                Answer ans1 = new Answer();
                ans1.Data = new MediaData("answer1");
                ans1.IsTrue = true;


                Answer ans2 = new Answer();
                ans2.Data = new MediaData("answer2");
                ans2.IsTrue = true;


                Answer ans3 = new Answer();
                ans3.Data = new MediaData("answer3");
                ans3.IsTrue = false;
    

                Answer ans4 = new Answer();
                ans4.Data = new MediaData("answer4");
                ans4.IsTrue = false;
             

                q2.Answers.Add(ans1.ID,ans1);
                q2.Answers.Add(ans2.ID,ans2);
                q2.Answers.Add(ans3.ID,ans3); 
                q2.Answers.Add(ans4.ID,ans4);
            }

            Test t = new Test();
            t.MaxMark = 5;
            t.MinMark = 2;
            t.MinuteTime = 10;
            t.Name = "ЭТоТест11";
            t.TeacherName = "teacher1";
            t.Data = new MediaData("testdata1");
            t.Questions.Add(q1.ID, q1);
            t.Questions.Add(q2.ID, q2);

            //string s = TestLibrary.Helpers.SaveMaster.Serialize(t, Helpers.SerializationMethod.XML);
            TestLibrary.Helpers.SaveMaster.Save(t, @"C:\ObjectSer.xml", Helpers.SerializationMethod.XML);

            //Test dt = (Test)TestLibrary.Helpers.SaveMaster.Open(typeof(Test), @"D:\ObjectSer.xml", Helpers.SerializationMethod.XML);
            //Console.WriteLine(s);
            Console.WriteLine("------------------------");
            Console.ReadKey();

            return null;
        }

        public static ClientData ShowClientVerify(ref Test origialTest)
        {
            ClientQuestion q1 = new ClientQuestion();
            {
                q1.QuestionID = "first";
                //q1.Type = QuestionType.SingleChoice;
                q1.Ball = 0;
                ClientQuestionAnswer ans = new ClientQuestionAnswer();
                ans.Data = new MediaData("answer1");
                q1.Answers.Add(ans);
            }

            ClientQuestion q2 = new ClientQuestion();
            {
                q2.QuestionID = "second";
               // q2.Type = QuestionType.SingleChoice;
                q2.Ball = 0;
                ClientQuestionAnswer ans1 = new ClientQuestionAnswer();
                {
                    ans1.Data = new MediaData("answer2");
                }

                ClientQuestionAnswer ans2 = new ClientQuestionAnswer();
                {
                    ans2.Data = new MediaData("answer4");
                }
                q2.Answers.Add(ans1);
                q2.Answers.Add(ans2);
            }

            ClientReport report = new ClientReport();
            report.Questions.Add(q1.QuestionID,q1);
            report.Questions.Add(q2.QuestionID, q2);

            ClientData client = new ClientData();
            {
                client.Name = "Andrew";
                client.Group = "10.3";
                client.LastName = "Gorbatov";
                //client.TimeStart = DateTime.Now;
                //client.TimeEnd = DateTime.Now;
                client.Report = report;
                //client.Result = client.Report.Verify(ref origialTest);
            }


            string s = TestLibrary.Helpers.SaveMaster.Serialize(client, Helpers.SerializationMethod.XML);
            //TestLibrary.Helpers.SaveMaster.Save(client, @"D:\client.xml", Helpers.SerializationMethod.XML);
            Console.Write(s);
            Console.ReadKey();
            return client;

        }
    }

    class AllBallsQuery : IDictionaryQueryAddon<string, ClientData>
    {


        public string ID
        {
            get { return "balls100"; }
        }

        public string Name
        {
            get { return "100balls"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public string Author
        {
            get { return "PeterZ"; }
        }

        public DictionaryAddonType Type
        {
            get { return DictionaryAddonType.ClientsQuesryAddon; }
        }

        public Dictionary<string, ClientData> MakeQuery(Dictionary<string, ClientData> original)
        {
            Dictionary<string, ClientData> result = original.Where(p => p.Value.Result.Percent == 100).ToDictionary(p => p.Key, p => p.Value);
            return result;
        }


        public void ShowQuery(System.Windows.Forms.ListView ClientsList)
        {
            throw new NotImplementedException();
        }


        public void ShowQuery(System.Windows.Forms.ListView ClientsList, Dictionary<string, ClientData> result)
        {
            throw new NotImplementedException();
        }
    }

    class NullBallsQuery : IDictionaryQueryAddon<string, ClientData>
    {


        public string ID
        {
            get { return "ballsZero"; }
        }

        public string Name
        {
            get { return "Zeroballs"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public string Author
        {
            get { return "PeterZ"; }
        }

        public DictionaryAddonType Type
        {
            get { return DictionaryAddonType.ClientsQuesryAddon; }
        }

        public Dictionary<string, ClientData> MakeQuery(Dictionary<string, ClientData> original)
        {
            Dictionary<string, ClientData> result = original.Where(p => p.Value.Result.Percent == 0).ToDictionary(p => p.Key, p => p.Value);
            return result;
        }


        public void ShowQuery(System.Windows.Forms.ListView ClientsList)
        {
            throw new NotImplementedException();
        }


        public void ShowQuery(System.Windows.Forms.ListView ClientsList, Dictionary<string, ClientData> result)
        {
            throw new NotImplementedException();
        }
    }

}

