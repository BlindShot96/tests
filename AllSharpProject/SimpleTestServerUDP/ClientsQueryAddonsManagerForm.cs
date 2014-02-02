using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleTestServerUDP.Helpers;
using SimpleTestServerUDP.Internet;
using TestLibrary.Helpers;
using System.IO;
using System.Xml.Serialization;

namespace SimpleTestServerUDP
{
    //public partial class ClientsQueryAddonsManagerForm : Form
    //{
    //    /// <summary>
    //    /// основная форма
    //    /// </summary>
    //    StartNewForm Form1;

    //    SimpleTestServerUDP.Helpers.ClientsQueryAddonsManager Manager = new SimpleTestServerUDP.Helpers.ClientsQueryAddonsManager();

    //    public ClientsQueryAddonsManagerForm(StartNewForm form1)
    //    {
    //        InitializeComponent();
    //        this.Form1 = form1;

    //        if (File.Exists("addons.xml") == true)
    //        {
    //            try
    //            {
    //                //this.Manager = this.Open();
    //            }
    //            catch
    //            { 
                  
    //            }
    //        }

    //        StandartCliensQuery q = new StandartCliensQuery();
    //        this.Manager.Addons.Add(q.ID, q);
    //        UpdateAddonsListBox();
            
    //    }

    //    private void listBox1_DoubleClick(object sender, EventArgs e)
    //    {
    //        if (AddonsListBox.SelectedItems != null)
    //        {
    //            var addon = Manager.Addons.ElementAt(AddonsListBox.SelectedIndex).Value;
                
    //            string text = string.Format("Название: {0}"
    //                + Environment.NewLine + "Версия: {1}"
    //                + Environment.NewLine + "Автор: {2}",
    //                addon.Name, addon.Version, addon.Author);
    //            MessageBox.Show(text);
    //        }
    //    }

    //    private void MakeChangedQueryButton_Click(object sender, EventArgs e)
    //    {
    //        if (AddonsListBox.SelectedItems != null)
    //        {
    //            var addon = Manager.Addons.ElementAt(AddonsListBox.SelectedIndex).Value;
    //            MakeQueryEventArgs re = new MakeQueryEventArgs(addon);
    //            Form1.OnMakeQueryEvent(this, re);
    //        }
            
    //    }

    //    private void AddNewButton_Click(object sender, EventArgs e)
    //    {
    //        OpenFileDialog openFileDialog1 = new OpenFileDialog();
    //        openFileDialog1.Filter = "Dll|*.dll";
    //        openFileDialog1.Title = "Select a Addon";

    //        string UnableLoad = "Не удалось загрузить плагины: ";
    //        int count_unable = 0;
    //        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    //        {
    //            foreach (string path in openFileDialog1.FileNames)
    //            {
    //                try
    //                {
    //                    Manager.LoadAddon(path);
    //                }
    //                catch
    //                {
    //                    UnableLoad += Environment.NewLine + path;
    //                    count_unable++;
    //                }
    //            }

    //            if (count_unable != 0)
    //            {
    //                MessageBox.Show(UnableLoad);
    //            }

    //            this.UpdateAddonsListBox();
    //        }
    //    }

    //    private void DeleteButton_Click(object sender, EventArgs e)
    //    {
    //        if (AddonsListBox.SelectedItems.Count > 0)
    //        {
    //            Manager.Addons.Remove(Manager.Addons.ElementAt(AddonsListBox.SelectedIndex).Key);
    //            AddonsListBox.Items.RemoveAt(AddonsListBox.SelectedIndex);
    //            UpdateAddonsListBox();
    //        }
    //    }

    //    private void UpdateAddonsListBox()
    //    {
    //        foreach (var item in Manager.Addons.Values)
    //        {
    //            this.AddonsListBox.Items.Add(item.Name + " " + item.Version);  
    //        }
    //    }

    //    public void Save(SimpleTestServerUDP.Helpers.ClientsQueryAddonsManager Manager)
    //    {
    //        if (File.Exists("addons.xml") == true)
    //        {
    //            try
    //            {
    //                File.Delete("addons.xml");
    //            }
    //            catch
    //            {
    //                int i = 0;
    //            }
    //        }

    //        TextWriter writer = new StreamWriter("addons.xml");
    //        XmlSerializer serializer = new XmlSerializer(typeof(SimpleTestServerUDP.Helpers.ClientsQueryAddonsManager));
    //        serializer.Serialize(writer, Manager);
    //        writer.Close();
    //    }

    //    //public ClientsQueryAddonsManager Open()
    //    //{
    //    //    ClientsQueryAddonsManager res = (ClientsQueryAddonsManager)SaveMaster.Open(typeof(Manager),"addons.xml", SerializationMethod.XML);
    //    //    return res;
    //    //}

    //    private void ClientsQueryAddonsManagerForm_FormClosing(object sender, FormClosingEventArgs e)
    //    {
    //        //this.Save(this.Manager);
    //    }

    //}
}
