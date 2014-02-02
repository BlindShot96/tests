using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestLibrary.ClientEdit;
using TestLibrary.Helpers;
using TestLibrary;
using SimpleTestServerUDP.Helpers;

namespace SimpleTestServerUDP.Internet
{
    /// <summary>
    /// что нужно делать если клиент с таким ид уже существует
    /// </summary>
    /// <param name="Clients">словарь клиентов</param>
    /// <param name="client">клиент для добавления</param>
    public delegate void ClientAddExist(Dictionary<string, Client> Clients, Client client);

    /// <summary>
    /// что нужно делать если клиент с таким ид НЕ существует
    /// </summary>
    /// <param name="Clients">словарь клиентов</param>
    /// <param name="client">клиент для добавления</param
    public delegate void ClientAddNotExist(Dictionary<string, Client> Clients, Client client);

    public class Manager
    {
        /// <summary>
        /// клиенты
        /// </summary>
        public BindingDictioanary<string, Client> Clients { get; private set; }

        ///// <summary>
        ///// получить клиентов по запросу
        ///// </summary>
        ///// <param name="query">запрос</param>
        ///// <returns>словарь клиентов</returns>
        //public Dictionary<string, Client> GetClientsByQuery(IDictionaryQueryAddon<string, Client> query)
        //{
        //    try
        //    {
        //        var result = query.MakeQuery(this.Clients);
        //        return result;
        //    }
        //    catch
        //    {
        //        throw new Exception();
        //    }
        //}

        /// <summary>
        /// добавить клиента
        /// </summary>
        /// <param name="key">ид</param>
        /// <param name="client">клиент</param>
        /// <param name="exist">делегат - что делать если уже существует клиент с таким же ид</param>
        /// <param name="notexist">делегат - что делать если не существует клиента с таким же ид</param>
        public void Add(string key, Client value, Action<Dictionary<string, Client>, Client> exist, Action<Dictionary<string, Client>, Client> notexist)
        {
            if (this.Clients.ContainsKey(key) == true)
            {
                exist(this.Clients, value);
            }
            else
            {
                notexist(this.Clients, value);
            }
        }

        public Manager()
        {
            this.Clients = new BindingDictioanary<string, Client>();
        }

    }
}
