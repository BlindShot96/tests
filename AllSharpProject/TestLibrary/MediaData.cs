using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using TestLibrary.Helpers;

namespace TestLibrary
{
    /// <summary>
    /// структура для хранения данных 
    /// например о вопросе, тесте, ответе
    /// </summary>
    [XmlRoot("MediaData")]
    public class MediaData
    {
        public static List<string> ImagesTypes = new List<string>()
        {
            ".jpg",
            ".JPG",
            ".png",
            ".PNG",
            ".bmp",
            ".BMP"
        };

        public static List<string> TextTypes = new List<string>()
        {
            ".txt",
            ".text"
        };
  
        public const string Null = "#Null";

        /// <summary>
        /// текст
        /// </summary>
        [XmlElement("Text", IsNullable = false)]
        public string Text { get; set; }

   

        /// <summary>
        /// список файлов
        /// </summary>
        [XmlArray("Files"),
        XmlArrayItem("File")]
        public ObservableCollection<MediaFile> Files = new ObservableCollection<MediaFile>();

        /// <summary>
        /// создать MaediaData
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="files">файлы</param>
        /// <param name="enableReplace">можно ли заменить, если уже есть такой файл</param>
        /// <returns></returns>
        public MediaData(string text, IEnumerable<MediaFile> files, bool enableReplace)
        {
            this.Text = text;
            foreach (MediaFile file in files)
            {
                AddFile(file,enableReplace);
            }
        }

        public void AddFile(MediaFile file, bool enableReplace)
        {
            if (this.Files.Contains(file, new KeyEqualityComparer<MediaFile>(k => k.ID)) == true)
            {
                if (enableReplace == true)
                {
                    this.Files[this.Files.IndexOf(this.Files.First(k =>k.ID.Equals(file.ID)))] = file;
                }
            }
            else
            {
                this.Files.Add(file);
            }
        }

        public void RemoveFile(MediaFile file)
        {
            this.Files.Remove(file);
        }
        public void RemoveFile(string id)
        {
            RemoveFile(this.Files.First(i => i.ID.Equals(id)));
        }
        public void RemoveFile(int num)
        {
            this.Files.RemoveAt(num);
        }

        public MediaFile this[string id]
        {
            get
            {
                return this.Files.First(i => i.ID.Equals(id));
            }
            set 
            {
                this.Files[this.Files.IndexOf(this.Files.First(k => k.ID.Equals(id)))] = value;
            }
        }
        public MediaFile this[int num]
        {
            get { return this.Files[num]; }
            set 
            {
                this.Files[num] = value;
            }
        }

        public MediaData(string text)
        {
            this.Text = text;
        }

        public MediaData() { }

        /// <summary>
        /// возвращает список изображений
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MediaFile> GetImagesFiles()
        {
            return GetFilesByPattern(MediaData.ImagesTypes);
        }

        /// <summary>
        /// возвращает список тектсовых файлов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MediaFile> GetTextFiles()
        {
            return GetFilesByPattern(MediaData.TextTypes);
        }

        /// <summary>
        /// возвращет список файлов с одним из указанных разрешений
        /// </summary>
        /// <param name="extensionsList">список разрешений</param>
        /// <returns></returns>
        public IEnumerable<MediaFile> GetFilesByPattern(List<string> extensionsList)
        {
            var res = new List<MediaFile>();

            foreach (MediaFile file in Files)
            {
                if (IsFileByExtensionPattern(extensionsList, file))
                {
                    res.Add(file);
                }
            }

            return res;
        }

        public bool IsFileByExtensionPattern(List<string> extensionsList,MediaFile file)
        {
            bool res = false;

            foreach (string type in extensionsList)
            {
                if (file.FileExtension == type)
                {
                     res = true;
                    break;
                }
            }
            return res;
        }

    }


    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------

    /// <summary>
    /// структура для хранения файла
    /// </summary>
    [XmlRoot("MediaFile",IsNullable = false)]
    public class MediaFile
    {

        /// <summary>
        /// уникальный идентификатор
        /// </summary>
        [XmlAttribute("ID")]
        public string ID { 
            get { return this.FileName + this.FileExtension; }
            set { }
        }

        /// <summary>
        /// байты файла
        /// </summary>
        [XmlElement("Bytes", DataType = "base64Binary", IsNullable = false)]
        public byte[] Bytes
        { get; set; }

        /// <summary>
        /// расширение файла с точкой
        /// </summary>
        /// <example>.jpg</example>
        [XmlAttribute("FileExtension")]
        public string FileExtension
        { get; set; }

        /// <summary>
        /// имя файла
        /// </summary>
        [XmlAttribute("FileName")]
        public string FileName
        { get; set; }

        /// <summary>
        /// описание файла
        /// </summary>
        [XmlElement("Text", IsNullable = false)]
        public string Text
        { get; set; }

        [XmlIgnore]
        public string FullPath
        {
            get;
            set;
        }

       public MediaFile() { }
       public MediaFile(string text, FileStream file)
       {
           this.Text = text;
           this.LoadFile(file);
           //try
           //{
           //    this.Text = text;
           //    this.FileName = Path.GetFileNameWithoutExtension(file.Name);
           //    this.FileExtension = Path.GetExtension(file.Name);
           //    this.Bytes = new byte[file.Length];
           //}
           //catch
           //{
           //    throw new ArgumentException();
           //}

           //try
           //{
           //    file.Read(this.Bytes, 0, this.Bytes.Length);
           //}
           //catch
           //{
           //    throw new ArgumentException();
           //}
       }
       public MediaFile(Uri path)
       {
           this.LoadFile(path.AbsolutePath);
           //FileStream file;
           //try
           //{
           //    file = File.OpenRead(path);
           //}
           //catch
           //{
           //    throw new ArgumentOutOfRangeException();
           //}

           //try
           //{
           //    this.Text = null;
           //    this.FileName = Path.GetFileNameWithoutExtension(file.Name);
           //    this.FileExtension = Path.GetExtension(file.Name);
           //    this.Bytes = new byte[file.Length];
           //}
           //catch
           //{
           //    throw new ArithmeticException();
           //}

           //try
           //{
           //    file.Read(this.Bytes, 0, this.Bytes.Length);
           //}
           //catch
           //{
           //    throw new IOException();
           //}

       }
       public MediaFile(string text, string path)
       {
           this.Text = text;
           this.LoadFile(path);
           //FileStream file;
           //try
           //{
           //    file = File.OpenRead(path);
           //}
           //catch
           //{
           //   throw new ArgumentOutOfRangeException();
           //}

           //try
           //{
           //    this.Text = text;
           //    this.FileName = Path.GetFileNameWithoutExtension(file.Name);
           //    this.FileExtension = Path.GetExtension(file.Name);
           //    this.Bytes = new byte[file.Length];
           //}
           //catch
           //{
           //    throw new ArithmeticException();
           //}

           //try
           //{
           //    file.Read(this.Bytes, 0, this.Bytes.Length);
           //}
           //catch
           //{
           //    throw new IOException();
           //}
       }

       public MediaFile(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="path">путь</param>
        public void LoadFile(string path)
        {
            FileStream file;
            try
            {
                file = File.OpenRead(path);
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }

            this.LoadFile(file);
        }

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="file">поток с файлом</param>
        public void LoadFile(FileStream file)
        {
            try
            {
                this.FullPath = file.Name;
                this.FileName = Path.GetFileNameWithoutExtension(file.Name);
                this.FileExtension = Path.GetExtension(file.Name);
                this.Bytes = new byte[file.Length];
            }
            catch
            {
                throw new ArgumentException();
            }

            try
            {
                file.Read(this.Bytes, 0, this.Bytes.Length);
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
