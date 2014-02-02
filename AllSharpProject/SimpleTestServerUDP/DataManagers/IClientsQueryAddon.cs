using System.Collections.Generic;
using System.Windows.Forms;

namespace SimpleTestServerUDP
{
    /// <summary>
    /// интервйес для плагина запроса к словарю пользователей
    /// </summary>
    public interface IDictionaryQueryAddon<Tkey,Tvalue>
    {
        /// <summary>
        /// уникальный ид плагина
        /// ид = Name + Version + Author
        /// </summary>
        string ID { get;}

        /// <summary>
        /// название плагина
        /// </summary>
        string Name { get;}

        /// <summary>
        /// версия палгина
        /// </summary>
        string Version { get;}

        /// <summary>
        /// автор плагина
        /// </summary>
        string Author { get;}

        /// <summary>
        /// тип плагина
        /// </summary>
        DictionaryAddonType Type { get;}

        /// <summary>
        /// метод выполняющий запрос
        /// </summary>
        /// <param name="original">оригинальный словарь клиентов</param>
        /// <returns>словарь по запросу</returns>
        Dictionary<Tkey, Tvalue> MakeQuery(Dictionary<Tkey, Tvalue> original);

        /// <summary>
        /// отображение результата ыв таблице
        /// </summary>
        /// <param name="ClientsList">пльзовательский элемент управления</param>
        void ShowQuery(ListView ClientsList, Dictionary<Tkey, Tvalue> result);
    }

    public enum DictionaryAddonType
    { 
      ClientsQuesryAddon
    }
}
