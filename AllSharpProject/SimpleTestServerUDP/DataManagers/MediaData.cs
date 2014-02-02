using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleTestServerUDP.DataManagers
{
    public class MediaData
    {
        public string Text = null;

        public List<MediaFile> Files;

        public void LoadFiles(string[] path)
        {
            foreach (string p in path)
            {
                MediaFile f = new MediaFile();
                f.LoadFile(p);
                this.Files.Add(f);
            }
        }

    }

    public class MediaFile
    {
        FileStream fs;
       public byte[] bytes;
       public string FileExtension = null;
       public string FileName = null;
       public string Text = null;

       public void LoadFile(string path)
       {
           
          this.bytes = File.ReadAllBytes(path);
          this.FileExtension = Path.GetExtension(path);
          this.FileName = Path.GetFileNameWithoutExtension(path);
       }
    }
}
