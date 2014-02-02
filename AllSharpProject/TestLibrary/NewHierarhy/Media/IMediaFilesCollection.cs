using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TestLibrary.Media
{
    /// <summary>
    /// коллекция файлов
    /// </summary>
    public class MediaFilesCollection:ObservableCollection<IMediaFile>
    {
        public IMediaFile this[string ID]
        {
            get
            {
                return this.FirstOrDefault(item => item.ID.Equals(ID));
            }
            set
            {
                int n = this.TakeWhile(item => !item.ID.Equals(ID)).Count();
                this[n] = value;
            }
        }

        public void Add(IMediaFile file)
        {
            if (file == null & this.Any(item => item.ID.Equals(file.ID)))
            {
                throw new ArgumentOutOfRangeException("file","такой файл уже есть");
            }

            base.Add(file);
        }

        public void Add(string path,FileType type)
        {
            IMediaFile file = null;
            if (type == FileType.Image)
            {
               file = new ImageFile();
               file.LoadFile(path);
            }
            if (type == FileType.Text)
            {
                file = new TextFile();
                file.LoadFile(path);
            }
            this.Add(file);
        }

        /// <summary>
        /// выбирает все изображения из списка файлов
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ImageFile> GetImageFiles()
        {
            var result = new ObservableCollection<ImageFile>();
            foreach (IMediaFile item in this.Where(item => item.Type.Equals(FileType.Image)))
            {
                result.Add((ImageFile) item);
            }
            return result;
        }

        /// <summary>
        /// выбирает все текстовые файлы из списка файлов
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<TextFile> GetTextFiles()
        {
            var result = new ObservableCollection<TextFile>();
            foreach (IMediaFile item in this.Where(item => item.Type.Equals(FileType.Text)))
            {
                result.Add((TextFile)item);
            }
            return result;
        }
        
    }
}
