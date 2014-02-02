using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SimpleTestCreator.Common;
using SimpleTestCreator.UserControls.DataElements;
using TestLibrary;

namespace SimpleTestCreator.ViewModels2
{
    public class ImageItem
    {
        public BitmapImage Image { get; set; }
        public string Text { get; set; }
        public MediaFile File { get; set; }

        public ImageItem(MediaFile file, BitmapImage img, string text)
        {
            this.Image = img;
            this.Text = text;
            this.File = file;
        }

        public ImageItem()
        {
        }
    }

    public class DataElementsViewModel:ViewModelBase
    {
        public MediaData DataModel { get; set; }

        public ObservableCollection<ImageItem> ImagesCollection
        {
            get
            {
                var res = new ObservableCollection<ImageItem>();
                foreach (MediaFile mediaFile in this.DataModel.GetImagesFiles())
                {
                    res.Add((ImageItem)new MediaFileToImageItemConverter().Convert(mediaFile,null,null,null));
                }
                return res;
            }
            set
            {
                foreach (MediaFile file in DataModel.Files)
                {
                    if (DataModel.IsFileByExtensionPattern(MediaData.ImagesTypes, file) == true)
                    {
                        this.DataModel.RemoveFile(file);
                    }
                }

                foreach (ImageItem image in value)
                {
                    MediaFile file = image.File;
                    if (DataModel.IsFileByExtensionPattern(MediaData.ImagesTypes, file) == true)
                    {
                        this.DataModel.AddFile(file, false);
                    }
                }
                OnPropertyChanged("ImagesCollection");
                OnUpdateImagesListHandler(this,new EventArgs());
            }
        }

        public ImageItem SelectedImage { get; set; }

        //public string LongText
        //{
        //    get
        //    {
        //        if (DataModel == null)
        //        {
        //            return null;
        //        }
        //        if (!DataModel.GetTextFiles().Any())
        //        {
        //            DataModel.AddFile(new MediaFile("Text") { FileExtension = ".txt", FileName = "LONGTEXT" }, true);
        //        }
        //        return DataModel.GetTextFiles().ElementAt(0).Text;
        //    }
        //    set
        //    {
        //        DataModel.GetTextFiles().ElementAt(0).Text = value;
        //        OnPropertyChanged("LongText");
        //    }
        //}

        public DataElementsViewModel():this(new MediaData())
        {}

        public DataElementsViewModel(MediaData data)
        {
            this.DataModel = data;
           // SetLongTextFile();

            this.DataModel.Files.CollectionChanged += (sender, args) => 
                OnUpdateImagesListHandler(this,new EventArgs());
        }

        //private void SetLongTextFile()
        //{
        //    bool isTextFile = false;
        //    foreach (MediaFile file in DataModel.Files)
        //    {
        //        if (file.FileExtension == ".txt")
        //        {
        //            isTextFile = true;
        //        }  
        //    }

        //    if (isTextFile == false)
        //    {
        //        DataModel.AddFile(new MediaFile()
        //        {
        //            FileExtension = ".text",
        //            FileName = "LongText",
        //            Text = ""
        //        }, true);
        //    }

        //}

        #region Event_update_questions_view_models

        public event EventHandler OnUpdateImagesListHandler = (sender, args) => { };

        #endregion

        #region Command_AddImage

        public ICommand AddImageCommand
        {
            get
            {
                return new RelayCommand(AddImageCommand_Executed, AddImageCommand_CanExecute);
            }
        }

        private void AddImageCommand_Executed(object sender)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Images (.jpg)|*.jpg|(.png)|*.png|(.bmp)|*.bmp";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    DataModel.AddFile(new MediaFile("",new FileStream(dlg.FileName,FileMode.Open)), true);
                    OnPropertyChanged("ImagesCollection");
                    OnUpdateImagesListHandler(this, new EventArgs());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось открыть изображение" + ex.ToString());
                }
            }
        }

        private bool AddImageCommand_CanExecute(object sender)
        {
            return DataModel != null;
        }

        #endregion

        #region Command_RemoveImage

        public ICommand RemoveImageCommand
        {
            get
            {
                return new RelayCommand(RemoveImageCommand_Executed, RemoveImageCommand_CanExecute);
            }
        }

        private void RemoveImageCommand_Executed(object param)
        {
            DataModel.RemoveFile(SelectedImage.File.ID);
            OnPropertyChanged("ImagesCollection");
            OnUpdateImagesListHandler(this,new EventArgs());
        }

        private bool RemoveImageCommand_CanExecute(object param)
        {
            if (param is MediaData && param != null)
            {
                this.SelectedImage.File = (MediaFile)param;
            }

            return DataModel.GetImagesFiles().Any() && SelectedImage != null;
        }

        #endregion


    }
}
