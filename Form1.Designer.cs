namespace ZastitaInformacija_19322
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fileSystemWatcher1 = new FileSystemWatcher();
            checkBox1 = new CheckBox();
            browse_btn = new Button();
            path_txt = new TextBox();
            logTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            filesListBox = new ListBox();
            panel1 = new Panel();
            label3 = new Label();
            panel5 = new Panel();
            sha1_cbox = new CheckBox();
            panel4 = new Panel();
            pcbc_cbox = new CheckBox();
            panel3 = new Panel();
            label4 = new Label();
            rc6_rbtn = new RadioButton();
            playfair_rbtn = new RadioButton();
            panel2 = new Panel();
            label5 = new Label();
            decrypt_rbtn = new RadioButton();
            encrypt_rbtn = new RadioButton();
            start_btn = new Button();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.IncludeSubdirectories = true;
            fileSystemWatcher1.Path = "C:\\Users\\Anastasija\\Desktop\\Target";
            fileSystemWatcher1.SynchronizingObject = this;
            fileSystemWatcher1.Changed += fileSystemWatcher1_Changed;
            fileSystemWatcher1.Created += fileSystemWatcher1_Created;
            fileSystemWatcher1.Deleted += fileSystemWatcher1_Deleted;
            fileSystemWatcher1.Renamed += fileSystemWatcher1_Renamed;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(891, 43);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(155, 24);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "FileSystemWatcher";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // browse_btn
            // 
            browse_btn.Location = new Point(43, 41);
            browse_btn.Name = "browse_btn";
            browse_btn.Size = new Size(119, 29);
            browse_btn.TabIndex = 1;
            browse_btn.Text = "Choose Target";
            browse_btn.UseVisualStyleBackColor = true;
            browse_btn.Click += browse_btn_Click;
            // 
            // path_txt
            // 
            path_txt.Location = new Point(179, 43);
            path_txt.Name = "path_txt";
            path_txt.Size = new Size(696, 27);
            path_txt.TabIndex = 2;
            // 
            // logTextBox
            // 
            logTextBox.Location = new Point(555, 114);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.Size = new Size(491, 342);
            logTextBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(555, 91);
            label1.Name = "label1";
            label1.Size = new Size(43, 20);
            label1.TabIndex = 4;
            label1.Text = "Logs:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 7F);
            label2.Location = new Point(555, 459);
            label2.Name = "label2";
            label2.Size = new Size(236, 15);
            label2.TabIndex = 5;
            label2.Text = "*you can see entire log history in log_file.txt";
            // 
            // filesListBox
            // 
            filesListBox.FormattingEnabled = true;
            filesListBox.Location = new Point(43, 114);
            filesListBox.Name = "filesListBox";
            filesListBox.Size = new Size(491, 344);
            filesListBox.TabIndex = 6;
            filesListBox.SelectedIndexChanged += filesListBox_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(1052, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(210, 415);
            panel1.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 9);
            label3.Name = "label3";
            label3.Size = new Size(61, 20);
            label3.TabIndex = 9;
            label3.Text = "Options";
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(sha1_cbox);
            panel5.Location = new Point(17, 345);
            panel5.Name = "panel5";
            panel5.Size = new Size(175, 49);
            panel5.TabIndex = 2;
            // 
            // sha1_cbox
            // 
            sha1_cbox.AutoSize = true;
            sha1_cbox.Location = new Point(13, 13);
            sha1_cbox.Name = "sha1_cbox";
            sha1_cbox.Size = new Size(68, 24);
            sha1_cbox.TabIndex = 9;
            sha1_cbox.Text = "SHA1";
            sha1_cbox.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(pcbc_cbox);
            panel4.Location = new Point(17, 279);
            panel4.Name = "panel4";
            panel4.Size = new Size(175, 48);
            panel4.TabIndex = 8;
            // 
            // pcbc_cbox
            // 
            pcbc_cbox.AutoSize = true;
            pcbc_cbox.Location = new Point(13, 12);
            pcbc_cbox.Name = "pcbc_cbox";
            pcbc_cbox.Size = new Size(66, 24);
            pcbc_cbox.TabIndex = 8;
            pcbc_cbox.Text = "PCBC";
            pcbc_cbox.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(label4);
            panel3.Controls.Add(rc6_rbtn);
            panel3.Controls.Add(playfair_rbtn);
            panel3.Location = new Point(17, 151);
            panel3.Name = "panel3";
            panel3.Size = new Size(175, 113);
            panel3.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 11);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 4;
            label4.Text = "Algorithm:";
            // 
            // rc6_rbtn
            // 
            rc6_rbtn.AutoSize = true;
            rc6_rbtn.Location = new Point(33, 42);
            rc6_rbtn.Name = "rc6_rbtn";
            rc6_rbtn.Size = new Size(56, 24);
            rc6_rbtn.TabIndex = 3;
            rc6_rbtn.TabStop = true;
            rc6_rbtn.Text = "RC6";
            rc6_rbtn.UseVisualStyleBackColor = true;
            rc6_rbtn.CheckedChanged += rc6_rbtn_CheckedChanged;
            // 
            // playfair_rbtn
            // 
            playfair_rbtn.AutoSize = true;
            playfair_rbtn.Location = new Point(33, 74);
            playfair_rbtn.Name = "playfair_rbtn";
            playfair_rbtn.Size = new Size(79, 24);
            playfair_rbtn.TabIndex = 2;
            playfair_rbtn.TabStop = true;
            playfair_rbtn.Text = "Playfair";
            playfair_rbtn.UseVisualStyleBackColor = true;
            playfair_rbtn.CheckedChanged += playfair_rbtn_CheckedChanged;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label5);
            panel2.Controls.Add(decrypt_rbtn);
            panel2.Controls.Add(encrypt_rbtn);
            panel2.Location = new Point(17, 36);
            panel2.Name = "panel2";
            panel2.Size = new Size(175, 100);
            panel2.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 10);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 8;
            label5.Text = "Operation:";
            // 
            // decrypt_rbtn
            // 
            decrypt_rbtn.AutoSize = true;
            decrypt_rbtn.Location = new Point(33, 67);
            decrypt_rbtn.Name = "decrypt_rbtn";
            decrypt_rbtn.Size = new Size(82, 24);
            decrypt_rbtn.TabIndex = 1;
            decrypt_rbtn.TabStop = true;
            decrypt_rbtn.Text = "Decrypt";
            decrypt_rbtn.UseVisualStyleBackColor = true;
            decrypt_rbtn.CheckedChanged += decrypt_rbtn_CheckedChanged;
            // 
            // encrypt_rbtn
            // 
            encrypt_rbtn.AutoSize = true;
            encrypt_rbtn.Location = new Point(33, 37);
            encrypt_rbtn.Name = "encrypt_rbtn";
            encrypt_rbtn.Size = new Size(79, 24);
            encrypt_rbtn.TabIndex = 0;
            encrypt_rbtn.TabStop = true;
            encrypt_rbtn.Text = "Encrypt";
            encrypt_rbtn.UseVisualStyleBackColor = true;
            encrypt_rbtn.CheckedChanged += encrypt_rbtn_CheckedChanged;
            // 
            // start_btn
            // 
            start_btn.Location = new Point(230, 476);
            start_btn.Name = "start_btn";
            start_btn.Size = new Size(94, 29);
            start_btn.TabIndex = 8;
            start_btn.Text = "button1";
            start_btn.UseVisualStyleBackColor = true;
            start_btn.Click += start_btn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1274, 546);
            Controls.Add(start_btn);
            Controls.Add(panel1);
            Controls.Add(filesListBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(logTextBox);
            Controls.Add(path_txt);
            Controls.Add(browse_btn);
            Controls.Add(checkBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FileSystemWatcher fileSystemWatcher1;
        private CheckBox checkBox1;
        private TextBox path_txt;
        private Button browse_btn;
        private TextBox logTextBox;
        private Label label1;
        private Label label2;
        private ListBox filesListBox;
        private Panel panel1;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Label label3;
        private RadioButton rc6_rbtn;
        private RadioButton playfair_rbtn;
        private RadioButton decrypt_rbtn;
        private RadioButton encrypt_rbtn;
        private CheckBox sha1_cbox;
        private CheckBox pcbc_cbox;
        private Label label4;
        private Label label5;
        private Button start_btn;
    }
}
