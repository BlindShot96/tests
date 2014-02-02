using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TestLibrary.Helpers;
using TestLibrary;

namespace SimpleTestServerUDP.Helpers
{
    public delegate void AddingNewDictionaryItemEventHandler(Object sender, AddingNewDictionaryItemEventArgs e);
    public class AddingNewDictionaryItemEventArgs : EventArgs
    {
        private object value;

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private object key;

        public object Key
        {
            get { return key; }
            set { key = value; }
        }

        public AddingNewDictionaryItemEventArgs() : base() { }
        public AddingNewDictionaryItemEventArgs(object key, object value)
        {
            this.value = value;
            this.key = key;
        }
    }


    /// <summary>
    /// класс, реализующий обект Dictioanary с привязкой данных
    /// </summary>
    /// <typeparam name="TKey">ключ</typeparam>
    /// <typeparam name="TValue">значение</typeparam>
    public class BindingDictioanary<TKey,TValue>:SerializableDictionary<TKey,TValue>
    {

        /// <summary>
        /// добавление нового
        /// </summary>
        private AddingNewDictionaryItemEventHandler onAddingNew;

        /// <summary>
        /// изменение Dictionary
        /// </summary>
        public EventHandler onListChanged;


        // Сводка:
        //     Происходит перед добавлением элемента список.
        public event AddingNewEventHandler AddingNew;


        //
        // Сводка:
        //     Вызывает событие System.ComponentModel.BindingList<T>.AddingNew.
        //
        // Параметры:
        //   e:
        //     An System.ComponentModel.AddingNewEventArgs that contains the event data.
        protected virtual void OnAddNew(AddingNewDictionaryItemEventArgs e)
        {
            if (this.onAddingNew != null)
            {
                this.onAddingNew(this, e);
            }
        }
        //
        // Сводка:
        //     Вызывает событие System.ComponentModel.BindingList<T>.ListChanged.
        //
        // Параметры:
        //   e:
        //     Объект System.ComponentModel.ListChangedEventArgs, содержащий данные, относящиеся
        //     к событию.
        protected virtual void OnListChange(EventArgs e)
        {
            if (this.onListChanged != null)
            {
                this.onListChanged(this, e);
            }
        }

        /// <summary>
        /// добавление новой пары ключ-значение 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            base.Add(key,value);

            AddingNewDictionaryItemEventArgs Re = new AddingNewDictionaryItemEventArgs(key,value);
            this.OnAddNew(Re);

            EventArgs re = new EventArgs();
            this.OnListChange(re);
        }

        /// <summary>
        /// очистить словарь
        /// </summary>
        public void Clear()
        {
            base.Clear();
            EventArgs re = new EventArgs();
            this.OnListChange(re);
        }

        /// <summary>
        /// заменяет элемент с ключом key
        /// на элемент value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Replace(TKey key, TValue value)
        {
            try
            {
                this[key] = value;

                EventArgs re = new EventArgs();
                this.OnListChange( re);
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// удаляет элемент с ключом key
        /// </summary>
        /// <param name="key"></param>
        public bool Remove(TKey key)
        {
            bool res = base.Remove(key);

            if (res == true)
            {
                EventArgs re = new EventArgs();
                this.OnListChange(re);
            }

            return res;
        }

        


    }
}
