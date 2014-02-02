using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace SimpleTestServerUDP.Internet
{
    [XmlRoot("Exception")]
    public class ExceptionToXMLBuider
    {
        /// <summary>
        /// сериализуемое исключение
        /// </summary>
        public SerializableException Exception;


        public ExceptionToXMLBuider()
        { }

        public ExceptionToXMLBuider(Exception e)
        {
            this.Exception = new SerializableException(e);
        }

        /// <summary>
        /// сериализует исключение в xml - строку
        /// </summary>
        /// <param name="ex">Исключение</param>
        /// <returns></returns>
        public static string Serialize(ExceptionToXMLBuider ex)
        {
            if (ex == null) { throw new ArgumentNullException(); }

            string path = @"exception.xml";
            string res = null;

            TextWriter wr = new StreamWriter(new MemoryStream());

            using (TextWriter writer = new StreamWriter(new MemoryStream()))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ExceptionToXMLBuider));
                serializer.Serialize(writer, ex);
            }

            using (StreamReader reader = new StreamReader(path))
            {
                res = reader.ReadToEnd();
            }
            File.Delete(path);

            return res;
        }

    }

    public class SerializableException
    {
        [XmlIgnore]
        public Exception Exeption;

        /// <summary>
        /// сообщение исключения
        /// </summary>
        public string Message;

        /// <summary>
        /// стек вызовов
        /// </summary>
        public string StackTrace;

        /// <summary>
        /// метод, выбросивший исключение
        /// </summary>
        public string TargetSite;

        /// <summary>
        /// исключение, вызвавшее текущее
        /// </summary>
        [XmlElement(IsNullable = false)]
        public SerializableException InnnerException;

        public SerializableException() { }

        public SerializableException(Exception ex)
        {
            if (ex == null) { throw new ArgumentNullException(); }

            this.Exeption = ex;
            this.Message = ex.Message;
            this.StackTrace = ex.StackTrace;
            
            //this.TargetSite = ex.TargetSite.Name;

            if (ex.InnerException == null) { this.InnnerException = null; return; }
            this.InnnerException = new SerializableException(ex.InnerException);
        }
    }
}
