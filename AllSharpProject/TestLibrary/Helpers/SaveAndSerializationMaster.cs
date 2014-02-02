using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace TestLibrary.Helpers
{
    /// <summary>
    /// статический класс, предоставляющий статические методы для сохранения, загрузки, сериализации, десериализации
    /// </summary>
    public static class SaveMaster
    {
        /// <summary>
        /// временная директория для сохранения
        /// </summary>
        public static string TEMPORARY_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory + @"\Temp";

        /// <summary>
        /// сериализация
        /// </summary>
        /// <param name="o">объект для сериализации</param>
        /// <param name="method">способ сериализации</param>
        /// <returns>строку с серализованным объектом</returns>
        /// <exception cref="IOException"/>
        /// <exception cref="SerializationException"/>
        public static string Serialize(Object o, SerializationMethod method)
        {
            if (method == SerializationMethod.XML)
            {
                string res = null;
                using (TextWriter writer = new StreamWriter("SerializeFile.xml",false,Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(o.GetType());
                    serializer.Serialize(writer, o);
                }

                using (StreamReader reader = new StreamReader("SerializeFile.xml",Encoding.UTF8))
                {
                    res = reader.ReadToEnd();
                }

                File.Delete("SerializeFile.xml");

                return res;
            }
            if (method == SerializationMethod.Binary)
            {

            }
            return null;
        }

        /// <summary>
        /// десериализация
        /// </summary>
        /// <param name="stream">поток с данными для сериализации</param>
        /// <param name="method">способ сериализации</param>
        /// <returns>объект теста</returns>
        /// <exception cref="IOException"/>
        /// <exception cref="SerializationException"/>
        public static object Deserialize(Type type, Stream stream, SerializationMethod method)
        {
            if (method == SerializationMethod.XML)
            {
                //try
                //{ 
                //    XmlSerializer serializer = new XmlSerializer(type);
                ////-------
                //    byte[] bytes = new byte[stream.Length];
                //    stream.Read(bytes, 0, bytes.Length);
                //    string s = Encoding.Default.GetString(bytes);
                ////---------

                //    XmlReader r = XmlTextReader.Create(stream);
                    
                //    Object res = serializer.Deserialize(
                //    //return res;
                //}
                //catch
                //{
                //    throw new ArgumentException();
                //}
            }
            if (method == SerializationMethod.Binary)
            {

            }
            return null;
        }

        /// <summary>
        /// десериализация xml
        /// </summary>
        /// <param name="type">тип</param>
        /// <param name="xml">строка с xml</param>
        /// <returns></returns>
        public static object DeserializeXML(Type type, string xml)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            using (TextReader reader = new StringReader(xml))
            {
                object result = serializer.Deserialize(reader);
                return result;
            }
        }
        /// <summary>
        /// сериализоать объект и соъранить в указанной директории
        /// </summary>
        /// <param name="o">объект</param>
        /// <param name="path">путь</param>
        /// <param name="method">способ сериализации</param>
        /// <exception cref="IOException"/>
        /// <exception cref="SerializationException"/>
        public static void Save(Object o, string path, SerializationMethod method)
        {
            if (method == SerializationMethod.XML)
            {
               FileStream writer = File.Create(path);
               XmlSerializer serializer = new XmlSerializer(o.GetType());
               serializer.Serialize(writer, o);
                writer.Close();
            }
            if (method == SerializationMethod.Binary)
            {

            }
        }

        /// <summary>
        /// сохранить файл в указанной директории
        /// </summary>
        /// <param name="s">поток</param>
        /// <param name="path">путь</param>
        /// <exception cref="IOException"/>
        public static void Save(Stream s, string path)
        {
            try
            {
                FileStream fs = File.Create(path);
                byte[] bytes = new byte[s.Length];
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
            catch
            {
                throw new IOException();
            }
        }
        
        /// <summary>
        /// сохранить байты в указанном пути 
        /// </summary>
        /// <param name="bytes">байты</param>
        /// <param name="path">путь</param>
        /// <exception cref="IOException"/>
        public static void Save(byte[] bytes, string path)
        {
            try
            {
                FileStream fs = File.Create(path);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
            catch
            {
                throw new IOException();
            }
        }


        /// <summary>
        /// открытие сериализованного объекта из файла
        /// </summary>
        /// <param name="type">тип результата</param>
        /// <param name="path">путь</param>
        /// <param name="method">способ десериализации</param>
        /// <returns>десериализовнный объект</returns>
        /// <exception cref="SerializationException"/>
        /// <exception cref="IOException"/>
        public static object Open(Type type,string path, SerializationMethod method)
        {
            try
            {
                if (method == SerializationMethod.XML)
                {
                    FileStream fs = File.OpenRead(path);
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    string xml = Encoding.UTF8.GetString(bytes);

                    return DeserializeXML(type, xml);
                }
                if (method == SerializationMethod.Binary)
                {
                    return null;
                }
            }
            catch (SerializationException ex)
            {
                throw new SerializationException("", ex); ;
            }
            catch (Exception ex)
            {
                throw new IOException("", ex);
            }
            return null;
        }
    }

   

    public enum SerializationMethod
    {
        XML,
        Binary
    }

}
