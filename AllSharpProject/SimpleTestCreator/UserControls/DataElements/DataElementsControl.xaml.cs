using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;
using SimpleTestCreator.ViewModels2;
using SimpleTestCreator.WindowsV;
using TestLibrary;

namespace SimpleTestCreator.UserControls.DataElements
{


    /// <summary>
	/// Логика взаимодействия для DataElementsControl.xaml
	/// </summary>
	public partial class DataElementsControl : UserControl
    {
        private DataElementsViewModel viewModel;
        public DataElementsViewModel ViewModel
        {
            get
            {
                if (viewModel == null)
                {
                    viewModel = this.DataContext as DataElementsViewModel;
                }
                return viewModel;
            }
            set
            {
                viewModel = value;
                this.DataContext = value;
            }
        }


        public DataElementsControl()
	    {
			this.InitializeComponent();          
	    }

        public DataElementsControl(DataElementsViewModel vm):this()
        {
            this.DataContext = vm;
        }

        public DataElementsControl(MediaData data) : this(new DataElementsViewModel(data))
        {
        }


        private void ImagesListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ImagesListBox.SelectedItem != null)
            {
                MediaFile file = this.ViewModel.DataModel.Files[this.ImagesListBox.SelectedIndex]; 
                ImageFileWindow window = new ImageFileWindow(file);
                window.Show();
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ImageItem item = ((sender as Button).DataContext as ImageItem);
            if (item != null)
            {
                this.ViewModel.SelectedImage = item;

                if (this.ViewModel.RemoveImageCommand.CanExecute(null))
                {
                    this.ViewModel.RemoveImageCommand.Execute(null);
                }
            }

        }
    }
}