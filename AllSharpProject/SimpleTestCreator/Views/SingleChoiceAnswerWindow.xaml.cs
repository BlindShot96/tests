using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestLibrary;

namespace SimpleTestCreator
{
	/// <summary>
	/// Логика взаимодействия для SingleChoiceAnswerWindow.xaml
	/// </summary>
	public partial class SingleChoiceAnswerWindow : Window
	{
	    public bool IsOk { get; private set; }
	    public bool IsNew { get; private set; }

	    public Answer AnswerModel { get; set; }
	    private Answer AnswerSaved { get; set; }

	    public SingleChoiceAnswerWindow(Answer ans)
		{
			this.InitializeComponent();			
			// Вставьте ниже код, необходимый для создания объекта.
	        this.AnswerModel = ans;
	        this.AnswerSaved = ans;

	        this.DataContext = this.AnswerModel;
		}

	    public SingleChoiceAnswerWindow() : this(new Answer(new MediaData("Ответ")))
	    {
	        this.IsNew = true;
	    }


	    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
	    {
            this.DialogResult = true;
	        this.IsOk = true;
	        this.Close();
	    }

	    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
	    {
	        this.AnswerModel = this.AnswerSaved;
	        this.DialogResult = false;
            this.IsOk = false;
            this.Close();
	    }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.AnswerModel = this.AnswerSaved;
            this.DialogResult = false;
            this.IsOk = false;
        }

        
	}
}