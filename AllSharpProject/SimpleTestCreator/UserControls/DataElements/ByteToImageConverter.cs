using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SimpleTestCreator.ViewModels2;
using TestLibrary;

namespace SimpleTestCreator.UserControls.DataElements
{


    // Custom class implements the IValueConverter interface.
    public class MediaFileToImageItemConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var file = (MediaFile) value;

            ImageItem res;
            try
            {
                BitmapImage imageSource = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(file.Bytes))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    imageSource.BeginInit();
                    imageSource.StreamSource = stream;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();
                }
                res = new ImageItem(file, imageSource,
                    file.FileName + file.FileExtension);
                return res;
            }
            catch
            {
                res = new ImageItem(file, new BitmapImage(new Uri(@"Resources\Images\ImageNotFound.bmp")),
                    "Не удалось загрузить файл");
                return res;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = (ImageItem) value;
            return item.File;
        }
    }

    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var file = (byte[]) value;

            Image res;
            try
            {
                res = Image.FromStream(new MemoryStream(file));
                
                return res;
            }
            catch(Exception ex)
            {
                res = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\Images\ImageNotFound.bmp");
                return res;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageIn = (Image) value;
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

        public static bool IsFileImage(MediaFile file)
        {

            Image res;
            try
            {
                res = Image.FromStream(new MemoryStream(file.Bytes));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsFileImage(byte[] file)
        {

            Image res;
            try
            {
                res = Image.FromStream(new MemoryStream(file));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

  

}
