using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleTestServerUDP.Internet;
using SimpleTestServerUDP.DataManagers;
using TestLibrary;
using System.IO;
using System.Xml.Linq;
using TestLibrary.Helpers;

namespace SimpleTestServerUDP
{

    public partial class StartForm : Form
    {


        /// <summary>
        /// объект UDP сервера
        /// </summary>
        Server server;

        /// <summary>
        /// правилоьные ответы
        /// </summary>
        Test test;

        Dictionary<int, Client> BindingClients = new Dictionary<int, Client>();

        public StartForm()
        {
            InitializeComponent();

        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                try
                {
                    server = new Server(5678, this.test);
                    server.Start();
                    server.ClientRecieved += ClientReadEH;
                    MessageBox.Show("Сервер запущен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно запустить сервер");
                }
            }
            else
            {
                if (MessageBox.Show("Перезапустить сервер?", "Перезапуск сервера", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    try
                    {
                        server.Stop();
                        server = null;
                        server = new Server(5678,this.test);
                        server.Start();
                        server.ClientRecieved += ClientReadEH;
                        MessageBox.Show("Сервер перезапущен");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Невозможно запустить сервер");
                    }
                }
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                if (MessageBox.Show("Остановить сервер?", "Остановка сервера", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    server.Stop();
                    server = null;
                    MessageBox.Show("Сервер остановлен");
                }
                
            }
        }

        private void OpenFileBtn_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML Test|*.xml";
            openFileDialog1.Title = "Select a Test File";


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog1.OpenFile());
                string xml = reader.ReadToEnd();
                Test test = (Test)SaveMaster.DeserializeXML(typeof(Test), xml);
                this.test = test;
            }


        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }

        public void ClientReadEH(object sender, ClientRecievedEventArgs e)
        {
            Server serv = (Server)sender;
            ClientList.Invoke((MethodInvoker)delegate
            {
                int i = 0;
                ClientList.Items.Clear();
                BindingClients.Clear();
                foreach (Client c in serv.Manager.Clients.Values)
                {
                    if (c.Status == Internet.Status.AnswersVerified)
                    {
                        ListViewItem item = new ListViewItem(new string[6] { c.Status.ToString(),c.Data.Group, c.Data.Name, c.Data.LastName, c.Data.Result.Percent.ToString(), c.Data.Result.Mark.ToString() });
                        ClientList.Items.Add(item);
                        BindingClients.Add(i, c);
                    }
                    if (c.Status == Internet.Status.RecieveTest)
                    {
                        ListViewItem item = new ListViewItem(new string[6] { c.Status.ToString(),c.Data.Group, c.Data.Name, c.Data.LastName, " ", " " });
                        ClientList.Items.Add(item);
                        BindingClients.Add(i, c);
                    }
                    i++;
                }

            });
        }

        private void ClientList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ClientList.SelectedItems.Count>0)
            {
                ListViewItem item = ClientList.SelectedItems[0];
                Client client =BindingClients[item.Index];

                if (client.Status == Internet.Status.AnswersVerified)
                {
                    //ClientDetailForm f2 = new ClientDetailForm(client, this.test);
                    //f2.Show();
                }
                else 
                {
                    MessageBox.Show("Ещё не получены ответы клиента", "Невозможно отобразить информацию о клиенте");
                }
            }
    }
    }
}
