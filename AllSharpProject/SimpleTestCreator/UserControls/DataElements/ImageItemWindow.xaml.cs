using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using SimpleTestCreator.Common;
using TestLibrary;
using Image = System.Drawing.Image;

namespace SimpleTestCreator.UserControls.DataElements
{
    /// <summary>
    /// Логика взаимодействия для ImageItemWindow.xaml
    /// </summary>
    public partial class ImageItemWindow : Window,IDialogWindow
    {
        public bool IsOk
        {
            get;
            private set;
        }

        public MediaFile File { get; set; }
        public MediaFile SavedFile { get; set; }

        public string Text
        {
            get { return File.Text; }
            set { File.Text = value; }
        }

      

        public ImageItemWindow():this(new MediaFile())
        {
        }

        public ImageItemWindow(MediaFile file)
        {
            this.File = file;
            this.SavedFile = file;

            InitializeComponent();
            this.DataContext = this;
        }

        public ImageItemWindow(string path) : this(new MediaFile(path))
        { }

        public void LoadImage(string fileName)
        {
            var buf= new MediaFile(fileName);
            if (ByteToImageConverter.IsFileImage(buf))
            {
                this.File = buf;
            }
        }

        private void OpenImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Images (.jpg)|*.jpg|(.png)|*.png|(.bmp)|*.bmp";
            if (dlg.ShowDialog() == true)
            {
                this.LoadImage(dlg.FileName);
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.IsOk = true;
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.IsOk = false;
            this.DialogResult = false;
            this.File = this.SavedFile;
            this.Close();
        }

       
    }
}
