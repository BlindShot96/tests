using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using TestLibrary.Helpers;
using TestLibrary.ClientEdit;
using SimpleTestServerUDP.Internet;
using SimpleTestServerUDP.Helpers;
using TestLibrary;
using System.IO;

namespace SimpleTestServerUDP
{

    public partial class StartNewForm : Form
    {

        private bool ISByClientsQuery = true; 

        /// <summary>
        /// событие - показать клиентов по запросу
        /// </summary>
        public event MakeQueryEventHandler MakeQueryEvent;

        /// <summary>
        /// интернет сервер
        /// </summary>
        public Server server { get; private set; }

        private int Port = 5678;

        /// <summary>
        /// тест
        /// </summary>
        public Test OriginalTest { get; set; }

        private IDictionaryQueryAddon<string, Client> ThatQuery = null;

        public StartNewForm(Test originalTest)
        {
            if (originalTest == null) { throw new ArgumentNullException(); }

            InitializeComponent();
            this.OriginalTest = originalTest;
          //  this.MakeQueryEvent += this.OnMakeQueryEvent;


        }
        public StartNewForm()
        {
            InitializeComponent();
           // this.MakeQueryEvent += this.OnMakeQueryEvent;
            String host = System.Net.Dns.GetHostName();
            System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[0];
            IPLabel.Text = "Мой IP: " + GetPublicIP();

            string s = "";
            foreach (IPAddress ipAddress in System.Net.Dns.GetHostByName(host).AddressList)
            {
                s += ip.ToString() + Environment.NewLine;
            }

           // MessageBox.Show(s);

        }

        public string GetPublicIP()
        {
            var ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            if (
                ipAddress != null)
                return ipAddress.ToString();
            return "null";
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                try
                {
                    server = new Server(Port, OriginalTest);
                    server.Start();
                    server.ClientRecieved += this.OnClientAdd;
                    UpdateStatusStrip("Сервер успешно запущен" + "   " + "Тест: " + this.OriginalTest.Settings.Name);
                    StatusStrip.BackColor = Color.LightGreen;
                }
                catch (Exception ex)
                {
                    UpdateStatusStrip("Невозможно запустить сервер");
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
                        server = new Server(5678, this.OriginalTest);
                        server.Start();
                        server.ClientRecieved += this.OnClientAdd;
                        UpdateStatusStrip("Сервер успешно запущен" + "   " + "Тест: " + this.OriginalTest.Settings.Name);
                        StatusStrip.BackColor = Color.LightGreen;
                    }
                    catch (Exception ex)
                    {
                        UpdateStatusStrip("Невозможно запустить сервер");
                    }
                }
            }
            UpdateListView();
            ResizeColumns();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                if (MessageBox.Show("Остановить сервер?", "Остановка сервера", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    server.Stop();
                    server = null;
                    UpdateStatusStrip("Сервер остановлен");
                    UpdateListView();
                    ResizeColumns();
                    StatusStrip.BackColor = SystemColors.Control;

                }

            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "XML Test|*.xml";
                openFileDialog1.Title = "Select a Test File";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamReader reader = new StreamReader(openFileDialog1.OpenFile());
                    string xml = reader.ReadToEnd();
                    Test test = (Test) SaveMaster.DeserializeXML(typeof (Test), xml);
                    this.OriginalTest = test;
                    UpdateStatusStrip("Тест загружен : " + this.OriginalTest.Settings.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (server != null && server.Manager.Clients.Count != 0)
            {
                try
                {
                    ExcelHelper excel = new ExcelHelper();
                    excel.MakeDocument(new List<Client>(server.Manager.Clients.Values.ToArray()), OriginalTest);
                    SaveFileDialog dlg = new SaveFileDialog();
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        excel.Save(dlg.FileName);
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось экспортровать результаты тестирования");
                }
            }
        }


        private void ClientsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.ClientsList.SelectedItems.Count > 0)
            {
                ListViewItem item = this.ClientsList.SelectedItems[0];
                string id = item.Name;
                Client client = null;

                foreach (Client c in this.server.Manager.Clients.Values)
                {
                    if (c.ID == id)
                    {
                        client = c;
                        break;
                    }
                }

                if (client == null)
                {
                    MessageBox.Show("Не получилось найти клиента","Ошибка");
                    return;
                }

                if (client.Status == Internet.Status.AnswersVerified)
                {
                    ClientDetailForm f2 = new ClientDetailForm(client, this.OriginalTest);
                    f2.Show();
                }
                else
                {
                    MessageBox.Show("Ещё не получены ответы клиента", "Невозможно отобразить информацию о клиенте");
                }

            }
        }

        public void OnMakeQueryEvent(object sender, MakeQueryEventArgs e)
        {
            if (this.server == null) { UpdateStatusStrip("невозможно выполнить запрос. Сервер не запущен!"); return; }
            if (this.server.Manager.Clients.Count == 0) { UpdateStatusStrip("невозможно выполнить запрос. Не подключилось ни одного клиента!"); return; }
            if (e.Query != null & this.server != null)
            {
                this.ThatQuery = e.Query;
                this.ThatQuery.ShowQuery(this.ClientsList, this.ThatQuery.MakeQuery(this.server.Manager.Clients));
            }
        }

        /// <summary>
        /// событие - показать клиентов по запросу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnClientsQueryMake(object sender, MakeQueryEventArgs e)
        {
            if (this.MakeQueryEvent != null)
            {
                this.MakeQueryEvent(this, e);
            }
        }

        public virtual void OnClientAdd(object sender, ClientRecievedEventArgs e)
        {
            if (e.Client != null)
            {
                //if (this.ThatQuery != null)
                //{
                //    this.ThatQuery.ShowQuery(this.ClientsList, this.ThatQuery.MakeQuery(this.server.Manager.Clients));
                //}
                //else
                //{
                //    StandartCliensQuery q = new StandartCliensQuery();
                //    q.ShowQuery(this.ClientsList, q.MakeQuery(this.server.Manager.Clients));
                //}

                this.ClientsList.Invoke((MethodInvoker)delegate
                {
                    UpdateListView();
                });
               // UpdateListView();
               // UpdateStatusStrip("Клиент добавлен: " + e.Client.Data.Name + " " + e.Client.Data.LastName + " " + e.Client.Data.Group);
            }
        }

        public void UpdateStatusStrip(string status)
        {
            this.State.Text =  status;
        }

        private void поКлиентамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ISByClientsQuery = true;

            UpdateListView();
        }

        private void ResizeColumns()
        {
            int widgth = (int)Math.Round((double)this.ClientsList.Width / (double)ClientsList.Columns.Count);

            foreach (ColumnHeader col in ClientsList.Columns)
            {
                int ww = 0;
                ww = col.Text.Length*20;
                if (widgth > ww)
                {
                    col.Width = widgth;
                }
                else
                {
                    col.Width = ww;
                }
                
            }
        }

        private void поВопросамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ISByClientsQuery = false;

            UpdateListView();
        }

        private void StartNewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.server != null)
            {
                this.server.Stop();
                server = null;
            }
        }

        private void StartNewForm_Resize(object sender, EventArgs e)
        {
            ResizeColumns();
        }

        private void ClientsList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.ClientsList.FocusedItem.Bounds.Contains(e.Location) == true)
            {
                ListViewItem item = this.ClientsList.SelectedItems[0];
                string id = item.Name;
                Client client = null;

                foreach (Client c in this.server.Manager.Clients.Values)
                {
                    if (c.ID == id)
                    {
                        client = c;
                        break;
                    }
                }

                if (client == null)
                {
                    MessageBox.Show("Не получилось найти клиента", "Ошибка");
                    return;
                }

                contextMenuStrip1.Items[0].Click += (o, args) =>
                {
                    if (client == null)
                    {
                        MessageBox.Show("Не получилось найти клиента", "Ошибка");
                        return;
                    }

                    if (client.Status == Internet.Status.AnswersVerified)
                    {
                        ClientDetailForm f2 = new ClientDetailForm(client, this.OriginalTest);
                        f2.Show();
                    }
                    else
                    {
                        MessageBox.Show("Ещё не получены ответы клиента", "Невозможно отобразить информацию о клиенте");
                    }
                };
                contextMenuStrip1.Items[1].Click += (o, args) =>
                {
                    if (this.server != null && this.server.Manager != null)
                    {
                        this.server.DeleteClient(id);
                        ResizeColumns();
                        UpdateListView();
                    }
                };
                contextMenuStrip1.Show(Cursor.Position);
            }
        }


        private string PrepareClientStatus(Status status)
        {
            if (status == Status.RecieveTest)
            {
                return "Прохождение теста";
            }
            else
            {
                return "Ответы проверены";
            }
        }

        private void UpdateListView()
        {
            if (ISByClientsQuery == true)
            {
               UpdateByClients();
            }
            else
            {
               UpdateByQuestions();
            }
        }

        private void UpdateByClients()
        {
            ClientsList.Items.Clear();
            ClientsList.Columns.Clear();
            //заполнение столбцов
            ClientsList.Columns.Add("Статус");
            ClientsList.Columns.Add("Имя");
            ClientsList.Columns.Add("Фамилия");
            ClientsList.Columns.Add("Группа");
            ClientsList.Columns.Add("Оценка");
            ClientsList.Columns.Add("Процент");

            ResizeColumns();

            if (server == null)
            {
                return;
            }

            foreach (Client c in this.server.Manager.Clients.Values)
            {
                if (c.Status == Internet.Status.AnswersVerified)
                {
                    ListViewItem item = new ListViewItem(new string[6] { PrepareClientStatus(c.Status), c.Data.Name, c.Data.LastName, c.Data.Group, c.Data.Result.Mark.ToString(), c.Data.Result.Percent.ToString() });
                    item.Name = c.ID;
                    //item.BackColor = Color.LightGreen;
                    ClientsList.Items.Add(item);
                }
                if (c.Status == Internet.Status.RecieveTest)
                {
                    ListViewItem item = new ListViewItem(new string[6] { PrepareClientStatus(c.Status), c.Data.Name, c.Data.LastName, c.Data.Group, "Не проверено", "Не проверено" });
                    item.Name = c.ID;
                    // item.BackColor = Color.IndianRed;
                    ClientsList.Items.Add(item);
                }
            }
        }

        private void UpdateByQuestions()
        {
            ClientsList.Items.Clear();
            ClientsList.Columns.Clear();
            //заполнение столбцов
            ClientsList.Columns.Add("Статус");
            ClientsList.Columns.Add("Имя");
            ClientsList.Columns.Add("Фамилия");
            ClientsList.Columns.Add("Группа");
            ClientsList.Columns.Add("Оценка");
            ClientsList.Columns.Add("Процент");



            if (this.OriginalTest == null)
            {
                ResizeColumns();
                return;
            }

            foreach (var question in this.OriginalTest.Questions)
            {
                ClientsList.Columns.Add(question.Name);
            }

            ResizeColumns();
            //--------------------

            if (server == null)
            {
                return;
            }

            foreach (Client c in this.server.Manager.Clients.Values)
            {
                if (c.Status == Internet.Status.AnswersVerified)
                {
                    List<string> items = new List<string>(new string[6] { PrepareClientStatus(c.Status), c.Data.Name, c.Data.LastName, c.Data.Group, c.Data.Result.Mark.ToString(), c.Data.Result.Percent.ToString() });
                    foreach (var cq in c.Data.Report.Questions)
                    {
                        items.Add(cq.Ball + " / " + cq.MaxBall);
                    }
                    ListViewItem item = new ListViewItem(items.ToArray());
                    item.Name = c.ID;
                    //item.BackColor = Color.LightGreen;
                    ClientsList.Items.Add(item);
                }
                if (c.Status == Internet.Status.RecieveTest)
                {
                    ListViewItem item = new ListViewItem(new string[6] { PrepareClientStatus(c.Status), c.Data.Name, c.Data.LastName, c.Data.Group, "Не проверено", "Не проверено" });
                    item.Name = c.ID;
                    // item.BackColor = Color.IndianRed;
                    ClientsList.Items.Add(item);
                }
            }
        }

    }

    public delegate void MakeQueryEventHandler(object sender, MakeQueryEventArgs e);
    public class MakeQueryEventArgs : EventArgs
    {
        public IDictionaryQueryAddon<string, Client> Query { get; set; }

        public MakeQueryEventArgs() : base() { }
        public MakeQueryEventArgs(IDictionaryQueryAddon<string, Client> Query)
            : base()
        {
            this.Query = Query;
        }
    }
}
