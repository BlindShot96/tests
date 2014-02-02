using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SimpleTestCreator.UserControls.DataElements;
using SimpleTestCreator.ViewModels2;
using TestLibrary;

namespace SimpleTestCreator.WindowsV
{
    /// <summary>
    /// Логика взаимодействия для ImageFileWindow.xaml
    /// </summary>
    public partial class ImageFileWindow : Window
    {
        public string Text { get; set; }

        public MediaFile File { get; set; }

        public BitmapImage Image { get; set; }

        public ImageFileWindow(MediaFile imageFile)
        {
            InitializeComponent();
            this.File = imageFile;
            this.Image = ((ImageItem) new MediaFileToImageItemConverter().Convert(imageFile, null, null, null)).Image;
            this.RootBox.Header = "Изображение: " + imageFile.ID;
        //    this.ImageBox.Source = this.Image;
            this.Text = imageFile.Text;
            this.DataContext = this;

        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.File.Text = Text;
            this.Close();

        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
