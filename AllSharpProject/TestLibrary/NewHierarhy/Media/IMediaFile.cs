using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TestLibrary
{
    /// <summary>
    /// типы файлов
    /// </summary>
    public enum FileType
    {
        Image,
        Html,
        Text
    }

    public abstract class IMediaFile
    {
        /// <summary>
        /// имя файла с расширением
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// тип файла
        /// </summary>
        public abstract FileType Type
        {
            get;
        }

        /// <summary>
        /// уникальный идентификатор файла
        /// </summary>
        public string ID {
            get { return this.FileName; }
        }

        protected byte[] bytes;

        /// <summary>
        /// байты файла
        /// </summary>
        public byte[] Bytes {
            get
            {
                bytes = GetBytes();
                return bytes;
            }
            set
            {
                SetBytes(value);
                bytes = value;
            }
        }

        /// <summary>
        /// получить байты из файла
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetBytes();

        /// <summary>
        /// загрузить байты в файл
        /// </summary>
        /// <param name="data"></param>
        public abstract void SetBytes(byte[] data);

        /// <summary>
        /// загрузка файла из потока
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public abstract void LoadFile(Stream stream, string fileName);

        /// <summary>
        /// загрузка файла
        /// </summary>
        /// <param name="path"></param>
        public virtual void LoadFile(string path)
        {
            try
            {
                using (Stream stream = File.OpenRead(path))
                {
                    this.LoadFile(stream, Path.GetFileName(path));
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("неправильный аргумент с путём к файлу",ex);
            }
        }

    }


}
