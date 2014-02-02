using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.RightsManagement;
using System.Text;
using System.Windows.Media.Imaging;

namespace TestLibrary.Media
{
    public class ImageFile:IMediaFile
    {
        private BitmapImage image;

        public override FileType Type
        {
            get { return FileType.Image; }
        }

        /// <summary>
        /// изображение
        /// </summary>
        public BitmapImage Image
        {
            get { return image; }
            set { image = value; }
        }


        /// <summary>
        /// конвертирует изображение в байты
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static byte[] ConvertToBytes(BitmapImage bitmapImage)
        {
            byte[] data;
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// конвертирует байты в изображение
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static BitmapImage ConvertBytesToImage(byte[] bytes)
        {
            var imageSource = new BitmapImage();
            using (var stream = new MemoryStream(bytes))
            {
                stream.Seek(0, SeekOrigin.Begin);
                imageSource.BeginInit();
                imageSource.StreamSource = stream;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.EndInit();
            }
            return imageSource;
        }

        public override byte[] GetBytes()
        {
            return ConvertToBytes(this.Image);
        }

        public override void SetBytes(byte[] data)
        {
            try
            {
                this.Image = ConvertBytesToImage(data);
            }
            catch (Exception ex)
            {
                this.Image = null;
            }
        }

        /// <summary>
        /// загрузка файла
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public override void LoadFile(System.IO.Stream stream, string fileName)
        {
            using (stream)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();

                this.image = bitmap;
                this.FileName = fileName;
            } 
 
        }
    }
}
