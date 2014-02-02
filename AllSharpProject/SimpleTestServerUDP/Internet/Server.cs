using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using SimpleTestServerUDP.DataManagers;
using TestLibrary;
using TestLibrary.ClientEdit;
using TestLibrary.Helpers;
using System.Timers;

namespace SimpleTestServerUDP.Internet
{


    public delegate void ClientRecievedEventHandler(Object sender, ClientRecievedEventArgs e);
    public class ClientRecievedEventArgs : EventArgs
    {
        public Client Client { get; private set; }

        public ClientRecievedEventArgs(Client Client)
        {
            this.Client = Client;
        }
    }

    public class Server
    {
        public void DeleteClient(String id)
        {
            this.Manager.Clients.Remove(id);
        }

        /// <summary>
        /// события принятия сообщения
        /// </summary>
        public event ClientRecievedEventHandler ClientRecieved;

        /// <summary>
        ///Работа с TCP
        /// </summary>
        private TcpListener TcpServer;

        /// <summary>
        /// поток для прослушки TCP порта
        /// </summary>
        private Thread _tcpRecieveThread;

        /// <summary>
        /// флаг остановки сервера
        /// </summary>
        private bool _falgStop = false;

        /// <summary>
        /// оригинальный тест
        /// </summary>
        public Test OriginalTest;

      //  private string _test_string; 

        private byte[] SerializedTest;

        private Manager manager = new Manager();
        public Manager Manager
        {
            get { return manager; }
        }


        private void SetTestString(Test t)
        {
            string test = SaveMaster.Serialize(this.OriginalTest, SerializationMethod.XML);
            //test = "*" + Encoding.Default.GetBytes(test).Length.ToString() + "\n" + (string)(new TestLibrary.Helpers.ClientXmlConverter().Convert(test, null, null, null));
            test = (string)(new TestLibrary.Helpers.ClientXmlConverter().Convert(test, null, null, null));

          //  this._test_string = "*" + Encoding.Default.GetBytes(test).Length.ToString()+"\n"+ Encoding.UTF8.GetString(Encoding.Default.GetBytes(test));
            this.SerializedTest = Encoding.UTF8.GetBytes(test);
        }

        public Server(int TCP_port, Test OriginalTest)
        {
            if (OriginalTest == null) { throw new ArgumentNullException(); }

            this.OriginalTest = OriginalTest;

            SetTestString(OriginalTest);

            try
            {
                this.TcpServer = new TcpListener(TCP_port);
            }
            catch
            {
                throw new Exception("Невозможно запустить сервер");
            }
        }

        /// <summary>
        /// запуск прослушивания
        /// </summary>
        public void Start()
        {
            this._falgStop = false;
            this._tcpRecieveThread =  new Thread(AcceptClient);
            this._tcpRecieveThread.Start();
        }

        private void AcceptClient()
        {
            this.TcpServer.Start();
            while (true)
            {
                TcpClient client = this.TcpServer.AcceptTcpClient();
                client.SendBufferSize = this.SerializedTest.Length;
                client.ReceiveBufferSize = this.SerializedTest.Length;

                new Thread(Recieve).Start(client);

                if (this._falgStop == true)
                {
                    break;
                }
            }
        }

        private const int MAX_BUFFER_SIZE = 512;
        private void Recieve(object o)
        {
            TcpClient tcp_client = (TcpClient)o;

            List<byte[]> buf = new List<byte[]>();

            int read = -1;
            int pos = 0;

            byte[] END_DATA = Encoding.UTF8.GetBytes("#@END@#");

            while (true)
            {
                byte[] mas = new byte[MAX_BUFFER_SIZE];
                read = tcp_client.GetStream().Read(mas, 0, mas.Length);

                buf.Add(mas);
                pos += read;

                string str1 = Encoding.UTF8.GetString(mas);
                string str2 = Encoding.UTF8.GetString(END_DATA);
                if (str1.IndexOf(str2, System.StringComparison.Ordinal) > -1 || read == 0)
                {
                    int t = 0;              
                    t++;
                    break;
                }
            }

            if (read == -1)
            {
                tcp_client.Close();
                return;
            }

            byte[] result = new byte[MAX_BUFFER_SIZE*buf.Count];
            int position = 0;
            for (int i = 0; i < buf.Count; i++)
            {
                Array.Copy(buf[i], 0, result, position, MAX_BUFFER_SIZE);
                position += MAX_BUFFER_SIZE;
            }

            

            try
            {
                Client client = new Client(result, ref this.OriginalTest);
                
                MakeClientRequest(client, tcp_client);
            }
            catch
            {
                Exception ex = new Exception("Не правльный запрос");
                Client.SendError(tcp_client, ex);
            }

        }



        private void MakeClientRequest(Client client, TcpClient tcp_client)
        {
            try
            {
                string Request = client.CurrentXml.Element("Client").Attribute("Request").Value;

                switch (Request)
                {
                    case "GETTEST":
                        {
                            OnRequestGetTest(client, tcp_client);
                            break;
                        }
                    case "SETRESULT":
                        {
                            OnRequestSetResult(client, tcp_client);
                            break;
                        }
                    default:
                        {
                            Exception ex = new Exception("неправильный запрос");
                            Client.SendError(tcp_client,ex);
                            return;
                        }
                }
            }
            catch
            {
                Exception ex = new Exception("Неправильный xml файл");
                Client.SendError(tcp_client, ex);
            }
        }

        private void OnRequestGetTest(Client client,TcpClient tcp_client)
        {
            try
            {
                client.Data.Name = client.CurrentXml.Element("Client").Attribute("Name").Value;
                client.Data.LastName = client.CurrentXml.Element("Client").Attribute("LastName").Value;
                client.Data.Group = client.CurrentXml.Element("Client").Attribute("Group").Value;
                client.Data.TimeStart = DateTime.Now;
                client.Status = Internet.Status.RecieveTest;
            }
            catch
            {
                Exception ex = new Exception("неправильный xml");
                Client.SendError(tcp_client,ex);
            }

            try
            {
                Client.Write(tcp_client,SerializedTest);
            }
            catch
            {
                Exception ex = new Exception("невозможно передать тест");
                Client.SendError(tcp_client, ex);
            }

            manager.Clients.Add(client.ID, client);
            ClientRecievedEventArgs Re = new ClientRecievedEventArgs(client);
            this.OnClientRecieved(this, Re);
        }

        private void OnRequestSetResult(Client client, TcpClient tcp_client)
        {
            try
            {
                string res = AddClient(client);
                if (Manager.Clients.ContainsKey(client.ID))
                {
                    client = Manager.Clients[client.ID];
                }

                if (res.Equals("") == false)
                {
                    Client.Write(tcp_client,"#"+res);
                }
                else
                {
                    client.Verify(ref this.OriginalTest, client.Buffer);
                  
                    client.Status = Status.AnswersVerified;
                    Client.WriteInfo(client.Data.Result, tcp_client);
                }
            }
            catch (Exception ex)
            {
                Client.SendError(tcp_client, ex);
            }

           // manager.Clients.Add(client.ID, client);

            ClientRecievedEventArgs Re = new ClientRecievedEventArgs(client);
            this.OnClientRecieved(this, Re);
        }

        private string AddClient(Client client)
        {
            if (manager.Clients.ContainsKey(client.ID) == true)
            {
                if (manager.Clients[client.ID].Data.Attemts < this.OriginalTest.Settings.Attemts)
                {
                    manager.Clients[client.ID] = client;
                    manager.Clients[client.ID].Data.Attemts += 1;
                    return "";
                }
                else
                {
                    return "Превышено количество попыток";
                }
            }
            else
            {
                manager.Clients.Add(client.ID,client);
                client.Data.Attemts += 1;
            }
            return "";
        }

        /// <summary>
        /// остановка сервера
        /// </summary>
        public void Stop()
        {
            this._falgStop = true;

            if (this.TcpServer != null)
            {
                TcpServer.Stop();
            }

            if (this._tcpRecieveThread != null)
            {
                this._tcpRecieveThread.Abort();
            }
        }

        /// <summary>
        /// событие изменения списка клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnClientRecieved(object sender, ClientRecievedEventArgs e)
        {
            if (this.ClientRecieved != null)
            {
                this.ClientRecieved(this, e);
            }
        }


        #region old
        ///// <summary>
        ///// метод прослушки
        ///// </summary>
        //private void Recieve()
        //{

        //    while (true)
        //    {
        //      UdpReieveLabel:

        //        IPEndPoint ipendpoint = null;
        //        byte[] message = UdpServer.Receive(ref ipendpoint);

        //        if (message.Length != 0)
        //        {
        //            Client client;
        //            try
        //            {
        //                client = new Client(message);
        //            }
        //            catch { goto UdpReieveLabel; }

        //            if (this.TryGetClient(client.ID) == true)
        //            {
        //                this.clients[client.ID] = client;
        //                this.clients[client.ID].Parse(this.OriginalTest);
        //            }
        //            else
        //            {
        //                client.Parse(this.OriginalTest);
        //                client.Data.Status = Status.AnswersVerified;

        //                //client.WriteInfo();

        //                this.clients.Add(client.ID, client);
        //            }

        //            ClientRecievedEventArgs Re = new ClientRecievedEventArgs(client);
        //            this.OnClientsQueryMake(this, Re);

        //            if (this.FalgStop == true)
        //            {
        //                break;
        //            }
        //        }
        //    }


        //}

        //private bool TryGetClient(string ID)
        //{
        //    try
        //    {
        //        Client client = this.clients[ID];
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private void TcpGetTestRecieve()
        //{
        //    byte[] Test = Encoding.Default.GetBytes(this.OriginalTest.TestDocument.ToString());
        //    string s = Encoding.Default.GetString(Test);

        //    this.TcpServer.Start();

        //    while (true)
        //    {
        //        TcpClient client = this.TcpServer.AcceptTcpClient();
        //        client.SendBufferSize = Test.Length;

        //        new Thread(new ThreadStart(() =>
        //        {
        //            byte[] rec = new byte[client.ReceiveBufferSize];
        //            client.GetStream().Read(rec, 0, client.ReceiveBufferSize);

        //            try
        //            {

        //                Client c = new Client(rec);

        //                if (this.TryGetClient(c.ID) == true)
        //                {
        //                    if (this.clients[c.ID].Data.Status == Status.RecieveTest)
        //                    {
        //                        this.clients[c.ID] = c;
        //                    }
        //                }
        //                else
        //                {
        //                    c.Data.Status = Status.RecieveTest;
        //                    this.clients.Add(c.ID, c);
        //                }

        //                string msg = Encoding.Default.GetString(rec);
        //                msg = Regex.Replace(msg, "\\0", "");

        //                XDocument doc = XDocument.Parse(msg);
        //                string Post = doc.Element("Client").Value;

        //                if (Post == "GET_TEST")
        //                {
        //                    Client.Write(Test, client);
        //                }

        //                ClientRecievedEventArgs Re = new ClientRecievedEventArgs(c);
        //                this.OnClientsQueryMake(this, Re);
        //            }
        //            catch { }
        //        }
        //            )).Start();

        //        if (this.FalgStop == true)
        //        {
        //            break;
        //        }
        //    }
        //}

        //private void TcpSetResultRecieve()
        //{
        //    this.TcpServer.Start();

        //    while (true)
        //    {
        //        TcpClient client = this.TcpServer.AcceptTcpClient();

        //        byte[] Buffer = new byte[client.ReceiveBufferSize];

        //        new Thread(new ThreadStart(() =>
        //        {
        //            //принятые байты от клиента
        //            byte[] rec = new byte[client.ReceiveBufferSize];
        //            client.GetStream().Read(rec, 0, client.ReceiveBufferSize);

        //            try
        //            {
        //                //создаём клиента, парсим и проверяем тест
        //                Client c = new Client(rec);
        //                c.Parse(this.OriginalTest);

        //                if (this.TryGetClient(c.ID) == true)
        //                {
        //                    if (this.clients[c.ID].Data.Status == Status.RecieveTest)
        //                    {
        //                        this.clients[c.ID] = c;
        //                    }
        //                }
        //                else
        //                {
        //                    c.Data.Status = Status.RecieveTest;
        //                    this.clients.Add(c.ID, c);
        //                }

        //                string msg = Encoding.Default.GetString(rec);
        //                msg = Regex.Replace(msg, "\\0", "");

        //                XDocument doc = XDocument.Parse(msg);
        //                string Post = doc.Element("Client").Value;

        //                if (Post == "SET_RESULT")
        //                {
        //                    Write(Test, client);
        //                }
        //                ClientRecievedEventArgs Re = new ClientRecievedEventArgs(c);
        //                this.OnClientsQueryMake(this, Re);
        //            }
        //            catch { }
        //        }
        //            )).Start();

        //        if (this.FalgStop == true)
        //        {
        //            break;
        //        }
        //    }
        //}

        //public static void Write(byte[] bytes, TcpClient client)
        //{
        //    NetworkStream networkStream = client.GetStream();
        //    networkStream.BeginWrite(bytes, 0, bytes.Length, WriteCallback, client);
        //}

        ///// <summary>
        ///// Callback for the write opertaion.
        ///// </summary>
        ///// <param name="result">The async result object</param>
        //private static void WriteCallback(IAsyncResult result)
        //{
        //     TcpClient client = result.AsyncState as TcpClient;
        //    NetworkStream networkStream = client.GetStream();
        //    networkStream.EndWrite(result);
        //    client.Close();
        //}
        #endregion
    }
}
