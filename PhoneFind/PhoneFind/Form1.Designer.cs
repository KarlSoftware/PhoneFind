namespace PhoneFind
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ofdPhones = new System.Windows.Forms.OpenFileDialog();
            this.grpbxPreparations = new System.Windows.Forms.GroupBox();
            this.checkbxAutomaticCheck = new System.Windows.Forms.CheckBox();
            this.grpbxSearchType = new System.Windows.Forms.GroupBox();
            this.radioNameAndAddress = new System.Windows.Forms.RadioButton();
            this.radioNumber = new System.Windows.Forms.RadioButton();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpbxMessages = new System.Windows.Forms.GroupBox();
            this.listbxMessages = new System.Windows.Forms.ListBox();
            this.grpbxNumbers = new System.Windows.Forms.GroupBox();
            this.listbxNumbers = new System.Windows.Forms.ListBox();
            this.listviewMultipleResults = new System.Windows.Forms.ListView();
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupboxResults = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.grpbxResultsContainer = new System.Windows.Forms.GroupBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpbxPreparations.SuspendLayout();
            this.grpbxSearchType.SuspendLayout();
            this.grpbxMessages.SuspendLayout();
            this.grpbxNumbers.SuspendLayout();
            this.groupboxResults.SuspendLayout();
            this.grpbxResultsContainer.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdPhones
            // 
            this.ofdPhones.Title = "Open File";
            // 
            // grpbxPreparations
            // 
            this.grpbxPreparations.BackColor = System.Drawing.Color.Transparent;
            this.grpbxPreparations.Controls.Add(this.checkbxAutomaticCheck);
            this.grpbxPreparations.Controls.Add(this.grpbxSearchType);
            this.grpbxPreparations.Controls.Add(this.btnRun);
            this.grpbxPreparations.Controls.Add(this.btnLoadFile);
            this.grpbxPreparations.Controls.Add(this.txtFile);
            this.grpbxPreparations.Controls.Add(this.label3);
            this.grpbxPreparations.Location = new System.Drawing.Point(29, 39);
            this.grpbxPreparations.Name = "grpbxPreparations";
            this.grpbxPreparations.Size = new System.Drawing.Size(234, 243);
            this.grpbxPreparations.TabIndex = 2;
            this.grpbxPreparations.TabStop = false;
            this.grpbxPreparations.Text = "Settings";
            // 
            // checkbxAutomaticCheck
            // 
            this.checkbxAutomaticCheck.AutoSize = true;
            this.checkbxAutomaticCheck.Location = new System.Drawing.Point(27, 157);
            this.checkbxAutomaticCheck.Name = "checkbxAutomaticCheck";
            this.checkbxAutomaticCheck.Size = new System.Drawing.Size(107, 17);
            this.checkbxAutomaticCheck.TabIndex = 7;
            this.checkbxAutomaticCheck.Text = "Automatic Check";
            this.toolTip.SetToolTip(this.checkbxAutomaticCheck, "If selected the records will be automatically filled. In case of multiple results" +
        " for one search, empty fields will be replaced.");
            this.checkbxAutomaticCheck.UseVisualStyleBackColor = true;
            // 
            // grpbxSearchType
            // 
            this.grpbxSearchType.Controls.Add(this.radioNameAndAddress);
            this.grpbxSearchType.Controls.Add(this.radioNumber);
            this.grpbxSearchType.Location = new System.Drawing.Point(18, 69);
            this.grpbxSearchType.Name = "grpbxSearchType";
            this.grpbxSearchType.Size = new System.Drawing.Size(200, 73);
            this.grpbxSearchType.TabIndex = 6;
            this.grpbxSearchType.TabStop = false;
            this.grpbxSearchType.Text = "Search Type";
            // 
            // radioNameAndAddress
            // 
            this.radioNameAndAddress.AutoSize = true;
            this.radioNameAndAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioNameAndAddress.Location = new System.Drawing.Point(9, 43);
            this.radioNameAndAddress.Name = "radioNameAndAddress";
            this.radioNameAndAddress.Size = new System.Drawing.Size(115, 17);
            this.radioNameAndAddress.TabIndex = 1;
            this.radioNameAndAddress.TabStop = true;
            this.radioNameAndAddress.Text = "Name and Address";
            this.radioNameAndAddress.UseVisualStyleBackColor = true;
            // 
            // radioNumber
            // 
            this.radioNumber.AutoSize = true;
            this.radioNumber.Location = new System.Drawing.Point(9, 20);
            this.radioNumber.Name = "radioNumber";
            this.radioNumber.Size = new System.Drawing.Size(118, 17);
            this.radioNumber.TabIndex = 0;
            this.radioNumber.TabStop = true;
            this.radioNumber.Text = "Single Search Term";
            this.radioNumber.UseVisualStyleBackColor = true;
            this.radioNumber.CheckedChanged += new System.EventHandler(this.radioNumber_CheckedChanged);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRun.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.btnRun.Location = new System.Drawing.Point(18, 190);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(200, 37);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Font = new System.Drawing.Font("Verdana", 6.75F);
            this.btnLoadFile.Location = new System.Drawing.Point(166, 36);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(52, 22);
            this.btnLoadFile.TabIndex = 4;
            this.btnLoadFile.Text = "Browse";
            this.btnLoadFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.Color.Azure;
            this.txtFile.ForeColor = System.Drawing.Color.DimGray;
            this.txtFile.Location = new System.Drawing.Point(18, 37);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(142, 20);
            this.txtFile.TabIndex = 3;
            this.txtFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFile_KeyPress);
            this.txtFile.MouseHover += new System.EventHandler(this.txtFile_MouseHover);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Load file:";
            // 
            // grpbxMessages
            // 
            this.grpbxMessages.BackColor = System.Drawing.Color.Transparent;
            this.grpbxMessages.Controls.Add(this.listbxMessages);
            this.grpbxMessages.Location = new System.Drawing.Point(29, 288);
            this.grpbxMessages.Name = "grpbxMessages";
            this.grpbxMessages.Size = new System.Drawing.Size(234, 277);
            this.grpbxMessages.TabIndex = 3;
            this.grpbxMessages.TabStop = false;
            this.grpbxMessages.Text = "Messages";
            // 
            // listbxMessages
            // 
            this.listbxMessages.BackColor = System.Drawing.SystemColors.Control;
            this.listbxMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbxMessages.Font = new System.Drawing.Font("Verdana", 6.75F);
            this.listbxMessages.FormattingEnabled = true;
            this.listbxMessages.ItemHeight = 12;
            this.listbxMessages.Location = new System.Drawing.Point(6, 19);
            this.listbxMessages.Name = "listbxMessages";
            this.listbxMessages.Size = new System.Drawing.Size(219, 252);
            this.listbxMessages.TabIndex = 0;
            // 
            // grpbxNumbers
            // 
            this.grpbxNumbers.BackColor = System.Drawing.Color.Transparent;
            this.grpbxNumbers.Controls.Add(this.listbxNumbers);
            this.grpbxNumbers.Location = new System.Drawing.Point(276, 39);
            this.grpbxNumbers.Name = "grpbxNumbers";
            this.grpbxNumbers.Size = new System.Drawing.Size(290, 243);
            this.grpbxNumbers.TabIndex = 4;
            this.grpbxNumbers.TabStop = false;
            this.grpbxNumbers.Text = "Numbers";
            // 
            // listbxNumbers
            // 
            this.listbxNumbers.BackColor = System.Drawing.SystemColors.Control;
            this.listbxNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbxNumbers.Enabled = false;
            this.listbxNumbers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listbxNumbers.FormattingEnabled = true;
            this.listbxNumbers.Location = new System.Drawing.Point(12, 19);
            this.listbxNumbers.Name = "listbxNumbers";
            this.listbxNumbers.Size = new System.Drawing.Size(261, 208);
            this.listbxNumbers.TabIndex = 1;
            this.listbxNumbers.MouseHover += new System.EventHandler(this.listbxNumbers_MouseHover);
            // 
            // listviewMultipleResults
            // 
            this.listviewMultipleResults.BackColor = System.Drawing.SystemColors.Control;
            this.listviewMultipleResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listviewMultipleResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnType});
            this.listviewMultipleResults.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.listviewMultipleResults.FullRowSelect = true;
            this.listviewMultipleResults.GridLines = true;
            this.listviewMultipleResults.Location = new System.Drawing.Point(6, 19);
            this.listviewMultipleResults.Name = "listviewMultipleResults";
            this.listviewMultipleResults.Size = new System.Drawing.Size(418, 192);
            this.listviewMultipleResults.TabIndex = 5;
            this.listviewMultipleResults.UseCompatibleStateImageBehavior = false;
            this.listviewMultipleResults.View = System.Windows.Forms.View.List;
            // 
            // columnType
            // 
            this.columnType.Text = "";
            this.columnType.Width = 0;
            // 
            // groupboxResults
            // 
            this.groupboxResults.BackColor = System.Drawing.Color.Transparent;
            this.groupboxResults.Controls.Add(this.btnClear);
            this.groupboxResults.Controls.Add(this.btnSelect);
            this.groupboxResults.Controls.Add(this.grpbxResultsContainer);
            this.groupboxResults.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupboxResults.Location = new System.Drawing.Point(276, 288);
            this.groupboxResults.Name = "groupboxResults";
            this.groupboxResults.Size = new System.Drawing.Size(442, 277);
            this.groupboxResults.TabIndex = 6;
            this.groupboxResults.TabStop = false;
            this.groupboxResults.Text = "Results";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Tomato;
            this.btnClear.Location = new System.Drawing.Point(359, 242);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(76, 29);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.Orange;
            this.btnSelect.Location = new System.Drawing.Point(6, 242);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 29);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // grpbxResultsContainer
            // 
            this.grpbxResultsContainer.Controls.Add(this.listviewMultipleResults);
            this.grpbxResultsContainer.Location = new System.Drawing.Point(6, 19);
            this.grpbxResultsContainer.Name = "grpbxResultsContainer";
            this.grpbxResultsContainer.Size = new System.Drawing.Size(430, 217);
            this.grpbxResultsContainer.TabIndex = 9;
            this.grpbxResultsContainer.TabStop = false;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.toolTip.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.toolTip.InitialDelay = 0;
            this.toolTip.OwnerDraw = true;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipTitle = "Selected Item";
            this.toolTip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTip_Draw);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "people.png");
            this.imgList.Images.SetKeyName(1, "industry.png");
            // 
            // progressbar
            // 
            this.progressbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.progressbar.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.progressbar.Location = new System.Drawing.Point(29, 579);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(689, 23);
            this.progressbar.TabIndex = 8;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(730, 24);
            this.menu.TabIndex = 10;
            this.menu.Text = "Menu";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem2,
            this.helpToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.toolStripMenuItem1.Text = "Menu";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(109, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(580, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Click on my face for help!";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(580, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 112);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(730, 620);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressbar);
            this.Controls.Add(this.groupboxResults);
            this.Controls.Add(this.grpbxNumbers);
            this.Controls.Add(this.grpbxMessages);
            this.Controls.Add(this.grpbxPreparations);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Action Rex";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpbxPreparations.ResumeLayout(false);
            this.grpbxPreparations.PerformLayout();
            this.grpbxSearchType.ResumeLayout(false);
            this.grpbxSearchType.PerformLayout();
            this.grpbxMessages.ResumeLayout(false);
            this.grpbxNumbers.ResumeLayout(false);
            this.groupboxResults.ResumeLayout(false);
            this.grpbxResultsContainer.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdPhones;
        private System.Windows.Forms.GroupBox grpbxPreparations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.GroupBox grpbxMessages;
        private System.Windows.Forms.ListBox listbxMessages;
        private System.Windows.Forms.GroupBox grpbxNumbers;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ListView listviewMultipleResults;
        private System.Windows.Forms.GroupBox groupboxResults;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.GroupBox grpbxSearchType;
        private System.Windows.Forms.RadioButton radioNameAndAddress;
        private System.Windows.Forms.RadioButton radioNumber;
        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox grpbxResultsContainer;
        private System.Windows.Forms.ListBox listbxNumbers;
        private System.Windows.Forms.CheckBox checkbxAutomaticCheck;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}

