using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestLibrary.Media
{
    public class TextFile:IMediaFile
    {
        private string text;

        /// <summary>
        /// тип файла
        /// </summary>
        public override FileType Type
        {
            get { return  FileType.Text;}
        }

        /// <summary>
        /// текст
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public override byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(this.Text);
        }

        public override void SetBytes(byte[] data)
        {
            using (StreamReader redader = new StreamReader(new MemoryStream(data)))
            {
                this.Text = redader.ReadToEnd();
            }
        }

        /// <summary>
        /// загрузить файл
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public override void LoadFile(Stream stream, string fileName)
        {
            using (var reader = new StreamReader(stream))
            {
                this.Text = reader.ReadToEnd();
                this.FileName = fileName;
            }
        }
    }
}
