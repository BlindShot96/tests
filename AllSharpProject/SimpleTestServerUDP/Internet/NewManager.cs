using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestLibrary.Helpers;

namespace SimpleTestServerUDP.Internet
{
    //class NewManager
    //{
    //     /// <summary>
    //    /// клиенты
    //    /// </summary>
    //    public ObservableCollection<Client> Clients { get; private set; }

    //    /// <summary>
    //    /// получить клиентов по запросу
    //    /// </summary>
    //    /// <param name="query">запрос</param>
    //    /// <returns>словарь клиентов</returns>
    //    public ObservableCollection<Client> GetClientsByQuery(IDictionaryQueryAddon<string, Client> query)
    //    {
    //        try
    //        {
    //           // var result = query.MakeQuery(this.Clients);
    //            //return result;
    //            return null;
    //        }
    //        catch
    //        {
    //            throw new Exception();
    //        }
    //    }

    //    /// <summary>
    //    /// добавить клиента
    //    /// </summary>
    //    /// <param name="key">ид</param>
    //    /// <param name="client">клиент</param>
    //    /// <param name="exist">делегат - что делать если уже существует клиент с таким же ид</param>
    //    /// <param name="notexist">делегат - что делать если не существует клиента с таким же ид</param>
    //    public void Add(Client value, Action<ObservableCollection<Client>, Client> exist, 
    //        Action<ObservableCollection<Client>, Client> notexist)
    //    {
    //        if (this.Clients.Contains(value,new KeyEqualityComparer<Client>(t=> t.ID)) == true)
    //        {
    //            exist(this.Clients, value);
    //        }
    //        else
    //        {
    //            notexist(this.Clients, value);
    //        }
    //    }

    //    public void Add(Client value, bool canOverride)
    //    {
    //        this.Add(value, (clients, client) =>
    //        {
    //            if (canOverride)
    //            {
    //                this.Clients[this.Clients.IndexOf(
    //                    this.Clients.First(t => t.ID == value.ID))] = value;
    //            }
    //        }, (clients, client) => this.Clients.Add(value));
    //    }

    //    public NewManager()
    //    {
    //        this.Clients = new ObservableCollection<Client>();
    //    }
    //}
}
