using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleTestServerUDP.Internet;
using System.Windows.Forms;

namespace SimpleTestServerUDP.Helpers
{
    public class StandartCliensQuery : IDictionaryQueryAddon<string, Client>
    {
        public string Author
        {
            get { return "Пётр Зайдель"; }
        }

        public string ID
        {
            get { return this.Name + this.Author + this.Version + this.Type.ToString(); }
        }

        public string Name
        {
            get { return "Показать всех клиентов"; }
        }

        public DictionaryAddonType Type
        {
            get { return DictionaryAddonType.ClientsQuesryAddon; }
        }

        public string Version
        {
            get { return "1.0"; }
        }


        public Dictionary<string, Client> MakeQuery(Dictionary<string, Client> original)
        {
            return original;
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

        public void ShowQuery(System.Windows.Forms.ListView ClientsList, Dictionary<string, Client> result)
        {
            ClientsList.Invoke((MethodInvoker)delegate
            {
                ClientsList.Items.Clear();
                foreach (Client c in result.Values)
                {
                    if (c.Status == Internet.Status.AnswersVerified)
                    {
                        ListViewItem item = new ListViewItem(new string[6] { c.Status.ToString(), c.Data.Name, c.Data.LastName, c.Data.Group, c.Data.Result.Percent.ToString(), c.Data.Result.Mark.ToString() });
                        item.Name = c.ID;
                        ClientsList.Items.Add(item);
                    }
                    if (c.Status == Internet.Status.RecieveTest)
                    {
                        ListViewItem item = new ListViewItem(new string[6] { c.Status.ToString(), c.Data.Name, c.Data.LastName, c.Data.Group, " ", " " });
                        item.Name = c.ID;
                        ClientsList.Items.Add(item);
                    }
                }

            });
        }
    }
}
