namespace SimpleTestServerUDP
{
    partial class QuestionInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.QuestionBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AnswerBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // QuestionBox
            // 
            this.QuestionBox.Location = new System.Drawing.Point(12, 42);
            this.QuestionBox.Name = "QuestionBox";
            this.QuestionBox.ReadOnly = true;
            this.QuestionBox.Size = new System.Drawing.Size(857, 312);
            this.QuestionBox.TabIndex = 0;
            this.QuestionBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Вопрос:";
            // 
            // AnswerBox
            // 
            this.AnswerBox.Location = new System.Drawing.Point(12, 388);
            this.AnswerBox.Name = "AnswerBox";
            this.AnswerBox.ReadOnly = true;
            this.AnswerBox.Size = new System.Drawing.Size(857, 133);
            this.AnswerBox.TabIndex = 2;
            this.AnswerBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ответ клиента:";
            // 
            // QuestionInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 533);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AnswerBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.QuestionBox);
            this.Name = "QuestionInfoForm";
            this.Text = "QuestionInfoForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox QuestionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox AnswerBox;
        private System.Windows.Forms.Label label2;
    }
}