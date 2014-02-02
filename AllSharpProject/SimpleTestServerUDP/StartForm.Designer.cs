namespace SimpleTestServerUDP
{
    partial class StartForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenFileBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Group = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PercentMark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientList = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.OpenFileBtn);
            this.groupBox1.Controls.Add(this.StartBtn);
            this.groupBox1.Controls.Add(this.StopBtn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(771, 249);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление сервером";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.Location = new System.Drawing.Point(37, 155);
            this.OpenFileBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(164, 64);
            this.OpenFileBtn.TabIndex = 7;
            this.OpenFileBtn.Text = "Выбрать файл теста";
            this.OpenFileBtn.UseVisualStyleBackColor = true;
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartBtn.Location = new System.Drawing.Point(37, 39);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(304, 76);
            this.StartBtn.TabIndex = 5;
            this.StartBtn.Text = "Запустить";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StopBtn.Location = new System.Drawing.Point(436, 39);
            this.StopBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(304, 76);
            this.StopBtn.TabIndex = 6;
            this.StopBtn.Text = "Остановить";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Status
            // 
            this.Status.DisplayIndex = 4;
            this.Status.Text = "Статус";
            this.Status.Width = 72;
            // 
            // Group
            // 
            this.Group.DisplayIndex = 5;
            this.Group.Text = "Группа";
            // 
            // p_Name
            // 
            this.p_Name.DisplayIndex = 0;
            this.p_Name.Text = "Имя";
            this.p_Name.Width = 136;
            // 
            // LastName
            // 
            this.LastName.DisplayIndex = 1;
            this.LastName.Text = "Фамилия";
            this.LastName.Width = 155;
            // 
            // PercentMark
            // 
            this.PercentMark.DisplayIndex = 2;
            this.PercentMark.Text = "Процент";
            this.PercentMark.Width = 84;
            // 
            // Mark
            // 
            this.Mark.DisplayIndex = 3;
            this.Mark.Text = "Оценка";
            this.Mark.Width = 94;
            // 
            // ClientsList
            // 
            this.ClientList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Status,
            this.Group,
            this.p_Name,
            this.LastName,
            this.PercentMark,
            this.Mark});
            this.ClientList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ClientList.FullRowSelect = true;
            this.ClientList.GridLines = true;
            this.ClientList.Location = new System.Drawing.Point(0, 268);
            this.ClientList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClientList.Name = "ClientList";
            this.ClientList.Size = new System.Drawing.Size(771, 482);
            this.ClientList.TabIndex = 3;
            this.ClientList.UseCompatibleStateImageBehavior = false;
            this.ClientList.View = System.Windows.Forms.View.Details;
            this.ClientList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClientList_MouseDoubleClick);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 750);
            this.Controls.Add(this.ClientList);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "StartForm";
            this.Text = "Серверный модуль";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartForm_FormClosing);
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenFileBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader Group;
        private System.Windows.Forms.ColumnHeader p_Name;
        private System.Windows.Forms.ColumnHeader LastName;
        private System.Windows.Forms.ColumnHeader PercentMark;
        private System.Windows.Forms.ColumnHeader Mark;
        private System.Windows.Forms.ListView ClientList;
    }
}

