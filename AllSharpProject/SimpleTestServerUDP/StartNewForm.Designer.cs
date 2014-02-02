namespace SimpleTestServerUDP
{
    partial class StartNewForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader ClientName;
            System.Windows.Forms.ColumnHeader LastName;
            System.Windows.Forms.ColumnHeader Group;
            System.Windows.Forms.ColumnHeader Mark;
            System.Windows.Forms.ColumnHeader Procent;
            System.Windows.Forms.ColumnHeader St;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartNewForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.управлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запустьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.остановитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.State = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.OpenButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.StartButton = new System.Windows.Forms.ToolStripButton();
            this.StopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExportButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.поКлиентамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поВопросамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IPLabel = new System.Windows.Forms.ToolStripLabel();
            this.ClientsList = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            LastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Group = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Mark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Procent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            St = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClientName
            // 
            ClientName.Text = "Имя";
            ClientName.Width = 156;
            // 
            // LastName
            // 
            LastName.Text = "Фамилия";
            LastName.Width = 173;
            // 
            // Group
            // 
            Group.Text = "Группа";
            Group.Width = 121;
            // 
            // Mark
            // 
            Mark.Text = "Оценка";
            Mark.Width = 114;
            // 
            // Procent
            // 
            Procent.Text = "Процент";
            Procent.Width = 121;
            // 
            // St
            // 
            St.Text = "Сосотояние";
            St.Width = 118;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.управлениеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1024, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.saveToolStripMenuItem.Text = "Сохранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.openToolStripMenuItem.Text = "Открыть";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.exportToolStripMenuItem.Text = "Экспорт";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // управлениеToolStripMenuItem
            // 
            this.управлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.запустьToolStripMenuItem,
            this.остановитьToolStripMenuItem});
            this.управлениеToolStripMenuItem.Name = "управлениеToolStripMenuItem";
            this.управлениеToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.управлениеToolStripMenuItem.Text = "Управление";
            // 
            // запустьToolStripMenuItem
            // 
            this.запустьToolStripMenuItem.Name = "запустьToolStripMenuItem";
            this.запустьToolStripMenuItem.Size = new System.Drawing.Size(159, 24);
            this.запустьToolStripMenuItem.Text = "Запусть";
            this.запустьToolStripMenuItem.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // остановитьToolStripMenuItem
            // 
            this.остановитьToolStripMenuItem.Name = "остановитьToolStripMenuItem";
            this.остановитьToolStripMenuItem.Size = new System.Drawing.Size(159, 24);
            this.остановитьToolStripMenuItem.Text = "Остановить";
            this.остановитьToolStripMenuItem.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.State});
            this.StatusStrip.Location = new System.Drawing.Point(0, 586);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.StatusStrip.Size = new System.Drawing.Size(1024, 22);
            this.StatusStrip.TabIndex = 1;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // State
            // 
            this.State.Name = "State";
            this.State.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton,
            this.toolStripSeparator2,
            this.StartButton,
            this.StopButton,
            this.toolStripSeparator1,
            this.ExportButton,
            this.toolStripDropDownButton1,
            this.IPLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1024, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // OpenButton
            // 
            this.OpenButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenButton.Image")));
            this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(85, 24);
            this.OpenButton.Text = "открыть";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // StartButton
            // 
            this.StartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartButton.Image = global::SimpleTestServerUDP.Properties.Resources.media_playback_start;
            this.StartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(23, 24);
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopButton.Image = global::SimpleTestServerUDP.Properties.Resources.media_playback_stop;
            this.StopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(23, 24);
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // ExportButton
            // 
            this.ExportButton.Image = ((System.Drawing.Image)(resources.GetObject("ExportButton.Image")));
            this.ExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(133, 24);
            this.ExportButton.Text = "экспорт в Excel";
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поКлиентамToolStripMenuItem,
            this.поВопросамToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(64, 24);
            this.toolStripDropDownButton1.Text = "Вид";
            // 
            // поКлиентамToolStripMenuItem
            // 
            this.поКлиентамToolStripMenuItem.Name = "поКлиентамToolStripMenuItem";
            this.поКлиентамToolStripMenuItem.Size = new System.Drawing.Size(172, 24);
            this.поКлиентамToolStripMenuItem.Text = "По клиентам";
            this.поКлиентамToolStripMenuItem.Click += new System.EventHandler(this.поКлиентамToolStripMenuItem_Click);
            // 
            // поВопросамToolStripMenuItem
            // 
            this.поВопросамToolStripMenuItem.Name = "поВопросамToolStripMenuItem";
            this.поВопросамToolStripMenuItem.Size = new System.Drawing.Size(172, 24);
            this.поВопросамToolStripMenuItem.Text = "По вопросам";
            this.поВопросамToolStripMenuItem.Click += new System.EventHandler(this.поВопросамToolStripMenuItem_Click);
            // 
            // IPLabel
            // 
            this.IPLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(111, 24);
            this.IPLabel.Text = "toolStripLabel1";
            // 
            // ClientsList
            // 
            this.ClientsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            St,
            ClientName,
            LastName,
            Group,
            Procent,
            Mark});
            this.ClientsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientsList.FullRowSelect = true;
            this.ClientsList.GridLines = true;
            this.ClientsList.Location = new System.Drawing.Point(0, 55);
            this.ClientsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClientsList.Name = "ClientsList";
            this.ClientsList.Size = new System.Drawing.Size(1024, 531);
            this.ClientsList.TabIndex = 3;
            this.ClientsList.UseCompatibleStateImageBehavior = false;
            this.ClientsList.View = System.Windows.Forms.View.Details;
            this.ClientsList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClientsList_MouseClick);
            this.ClientsList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClientsList_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 74);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.открытьToolStripMenuItem.Text = "Открыть";
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // StartNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 608);
            this.Controls.Add(this.ClientsList);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "StartNewForm";
            this.Text = "Сервер";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartNewForm_FormClosed);
            this.Resize += new System.EventHandler(this.StartNewForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton StartButton;
        private System.Windows.Forms.ToolStripButton StopButton;
        private System.Windows.Forms.ToolStripButton OpenButton;
        private System.Windows.Forms.ToolStripButton ExportButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem управлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem запустьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem остановитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel State;
        private System.Windows.Forms.ListView ClientsList;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem поКлиентамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поВопросамToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel IPLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
    }
}