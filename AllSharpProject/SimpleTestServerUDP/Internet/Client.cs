using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using SimpleTestServerUDP.DataManagers;
using TestLibrary.ClientEdit;
using TestLibrary.Helpers;
using System.Timers;
using TestLibrary;

namespace SimpleTestServerUDP.Internet
{
    public enum Status
    {
        RecieveTest,
        AnswersVerified,
        Deleted
    }

    public class Client
    {

        public static Encoding UTF8Encoding = Encoding.UTF8;

        /// <summary>
        /// оригинальный тест
        /// </summary>
        public Test OrigialTest;

        /// <summary>
        /// тест - данные клиента
        /// </summary>
        public ClientData Data;

        /// <summary>
        /// состояние клиента
        /// </summary>
        public Status Status;

        /// <summary>
        /// текущий xml файл 
        /// данного клиента
        /// </summary>
        public XDocument CurrentXml;

        /// <summary>
        /// Gets the Buffer.
        /// </summary>
        public byte[] Buffer;


        /// <summary>
        /// Uniq ID of this client тоже самое что и IP
        /// </summary>
        public string ID { get { return this.Data.ID; } }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="Original">оригинальный тест</param>
        /// <param name="client">интернет соединение</param>
        public Client(byte[] bytes,ref Test Original)
        {
            this.Buffer = bytes;
            this.OrigialTest = Original;
            this.Data = new ClientData();

            if (SetIdentificationData(this.Buffer) == false)
            {
                throw new Exception();
            }

            try
            {
                string FileString = UTF8Encoding.GetString(bytes);
                FileString = Regex.Replace(FileString, "\\0", "");
                FileString = Regex.Replace(FileString, "#@END@#", "");
                this.CurrentXml = XDocument.Parse(FileString);
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// получит ID из xml
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private  bool SetIdentificationData(byte[] bytes)
        {
            try
            {
                string fileString = UTF8Encoding.GetString(bytes);
                fileString = Regex.Replace(fileString, "\\0", "");
                fileString = Regex.Replace(fileString, "#@END@#", "");

                XDocument clientXDoc = XDocument.Parse(fileString);

                string name = clientXDoc.Element("Client").Attribute("Name").Value;
                string lastName = clientXDoc.Element("Client").Attribute("LastName").Value;
                string group = clientXDoc.Element("Client").Attribute("Group").Value;

                if (name == null || lastName == null || group == null)
                {
                    return false;
                }

                this.Data.Name = name;
                this.Data.LastName = lastName;
                this.Data.Group = group;

                return true;
            }
            catch
            {
                return false;
            }

        }

        

        /// <summary>
        /// парсит и проверяет ответы лиента
        /// </summary>
        /// <param name="OriginalTest"></param>
        public void Verify(ref Test OriginalTest, byte[] buf)
        {
            try
            {
                string fileString = UTF8Encoding.GetString(buf);
                fileString = Regex.Replace(fileString, "\\0", "");
                fileString = Regex.Replace(fileString, "#@END@#", "");

                XDocument clientXDoc = XDocument.Parse(fileString);
                XElement report_xml = clientXDoc.Element("Client").Element("Report");
                string report_str = report_xml.ToString();

                this.Data.Report = (ClientReport)SaveMaster.DeserializeXML(typeof(ClientReport), report_str);

                DClientReport rep = new DClientReport();
                rep.Questions = this.Data.Report.Questions;

                this.Data.Result = rep.Verify(ref OriginalTest);

                this.Status = Status.AnswersVerified;
            }
            catch
            {
                throw new Exception("не удалось проверить тест");
            }
        }

        #region write

        /// <summary>
        /// отправляет результаты тестирования на указанный адрес
        /// </summary>
        /// <param name="client"></param>
        public static void WriteInfo(ClientResult cl, TcpClient client)
        {
            XDocument doc = new XDocument(
                    new XElement("Result",
                    new XAttribute("Mark", cl.Mark),
                    new XAttribute("Percent", cl.Percent),
                    new XAttribute("Balls", cl.ClientBalls),
                    new XAttribute("AllBalls", cl.ClientBalls)
                    ));
            string str = doc.ToString();
            Client.Write(client,str);
        }

        /// <summary>
        /// метод отправки строки на указанный адрес
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="client"></param>
        public static void Write(TcpClient client, String msg)
        {
            Write(client, UTF8Encoding.GetBytes(msg));
        }

        private static List<byte[]> divide_bytes_mas(byte[] bytes)
        {
            int max = 512;//максимальное кол-во байт для одного пакета

            List<byte[]> list_bytes = new List<byte[]>();
            int k = bytes.Length / max;
            int all_l = 0;
            for (int i = 0; i < k; i++)
            {
                byte[] mas = new byte[max];
                Array.Copy(bytes, i * max, mas, 0, max);
                list_bytes.Add(mas);
                all_l += max;
            }
            if (all_l < bytes.Length)
            {
                byte[] mas = new byte[bytes.Length - all_l];
                Array.Copy(bytes, all_l, mas, 0, bytes.Length - all_l);
                list_bytes.Add(mas);
            }

            list_bytes.Add(UTF8Encoding.GetBytes(Environment.NewLine+"#@END@#"));

            return list_bytes;
        }


        /// <summary>
        /// метод отправки массива байт на указанный адрес
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="client"></param>
        public static void Write(TcpClient client, byte[] bytes)
        {         
            try
            {
                client.SendBufferSize = 512;
                if (client.GetStream() != null)
                {
                    lock (client)
                    {
                        Thread thread = new Thread(() =>
                        {
                          foreach (byte[] mas in divide_bytes_mas(bytes))
                          {
                              client.GetStream().Write(mas, 0, mas.Length);
                          }
                          client.Close();
                           
                        });
                        thread.Start();
                    }                                  
                }

            }
            catch(Exception ex)
            {
               client.Close();
            }
        }

        /// <summary>
        /// Callback for the write opertaion.
        /// </summary>
        /// <param name="result">The async result object</param>
        private static void WriteCallback(IAsyncResult result)
        {
            TcpClient client = (TcpClient) result.AsyncState;
            try
            {             
                client.GetStream().EndWrite(result);
                client.Close();
            }
            catch (Exception ex)
            {
               client.Close(); 
            }
        }

        /// <summary>
        /// отправка сообщения об ошибке
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="client"></param>
        public static void SendError(TcpClient client, Exception ex)
        { 
           ExceptionToXMLBuider exception= new ExceptionToXMLBuider(ex);
           string msg = ExceptionToXMLBuider.Serialize(exception);
           Write(client,msg);
        }

        #endregion


    }





    #region old
    /////<summary>
    /////Начинает прослушивать данного клиента.
    /////Запускать в отдельном потоке
    /////</summary>
    //public void StartReading()
    //{
    //    if (this.NetworkStream != null)
    //    {
    //        ReadingThread = new Thread(new ThreadStart(() =>
    //        {
    //            byte[] buf = new byte[this.client.ReceiveBufferSize];

    //            this.ReadingTimer.Start();
    //            {
    //               int readcount =  this.NetworkStream.Read(buf, 0, buf.Length);
    //               if (readcount == 0)
    //               {
    //                   this.Dispose();
    //                   return;
    //               }
    //            }
    //            this.ReadingTimer.Stop();

    //            this.Buffer = buf;
    //            string FileString = Encoding.Default.GetString(this.Buffer);
    //            FileString = Regex.Replace(FileString, "\\0", "");
    //            try
    //            {
    //                this.CurrentXml = XDocument.Parse(FileString);
    //            }
    //            catch
    //            {
    //                Exception ex = new Exception("Неправильный xml файл");
    //                this.SendError(ex);
    //                this.Dispose();
    //                return;
    //            }



    //        }));
    //        ReadingThread.Start();
    //    }

    //}

    //public void MakeClientRequest(XDocument xml)
    //{
    //    try
    //    {
    //        string Request = xml.Element("Client").Attribute("Request").Value;

    //        switch (Request)
    //        {
    //            case "GETTEST":
    //                {
    //                    break;
    //                }
    //            case "SETRESULT":
    //                {
    //                    break;
    //                }
    //             default:
    //                {
    //                    Exception ex = new Exception("неправильный запрос");
    //                    this.SendError(ex);
    //                    this.Dispose();
    //                    return;
    //                }
    //        }

    //    }
    //    catch
    //    {
    //        Exception ex = new Exception("Неправильный xml файл");
    //        this.SendError(ex);
    //        this.Dispose();
    //    }
    //}
    #endregion
}
