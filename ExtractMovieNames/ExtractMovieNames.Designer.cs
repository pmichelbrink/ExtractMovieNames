namespace ExtractMovieNames
{
    partial class ExtractMovieNames
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractMovieNames));
            this.btnExtract = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtFiles = new System.Windows.Forms.TextBox();
            this.lblFiles = new System.Windows.Forms.Label();
            this.grpExtract = new System.Windows.Forms.GroupBox();
            this.gbPath = new System.Windows.Forms.GroupBox();
            this.btnFolderBrowse = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.rbAllDrives = new System.Windows.Forms.RadioButton();
            this.rbSelectedFolder = new System.Windows.Forms.RadioButton();
            this.grpConsolidate = new System.Windows.Forms.GroupBox();
            this.btnConsolidate = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFindDuplicates = new System.Windows.Forms.Button();
            this.btnDupBrowse = new System.Windows.Forms.Button();
            this.txtDupSourceFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpExtract.SuspendLayout();
            this.gbPath.SuspendLayout();
            this.grpConsolidate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(185, 288);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(75, 23);
            this.btnExtract.TabIndex = 0;
            this.btnExtract.Text = "Extract";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(10, 19);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(244, 154);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.Text = "";
            // 
            // txtFiles
            // 
            this.txtFiles.Location = new System.Drawing.Point(47, 16);
            this.txtFiles.Name = "txtFiles";
            this.txtFiles.Size = new System.Drawing.Size(176, 20);
            this.txtFiles.TabIndex = 2;
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(10, 16);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(31, 13);
            this.lblFiles.TabIndex = 3;
            this.lblFiles.Text = "Files:";
            // 
            // grpExtract
            // 
            this.grpExtract.Controls.Add(this.gbPath);
            this.grpExtract.Controls.Add(this.rtbLog);
            this.grpExtract.Controls.Add(this.btnExtract);
            this.grpExtract.Location = new System.Drawing.Point(8, 0);
            this.grpExtract.Name = "grpExtract";
            this.grpExtract.Size = new System.Drawing.Size(274, 324);
            this.grpExtract.TabIndex = 4;
            this.grpExtract.TabStop = false;
            this.grpExtract.Text = "Extract";
            // 
            // gbPath
            // 
            this.gbPath.Controls.Add(this.btnFolderBrowse);
            this.gbPath.Controls.Add(this.txtPath);
            this.gbPath.Controls.Add(this.rbAllDrives);
            this.gbPath.Controls.Add(this.rbSelectedFolder);
            this.gbPath.Location = new System.Drawing.Point(10, 179);
            this.gbPath.Name = "gbPath";
            this.gbPath.Size = new System.Drawing.Size(244, 103);
            this.gbPath.TabIndex = 4;
            this.gbPath.TabStop = false;
            this.gbPath.Text = "Path";
            // 
            // btnFolderBrowse
            // 
            this.btnFolderBrowse.Location = new System.Drawing.Point(212, 66);
            this.btnFolderBrowse.Name = "btnFolderBrowse";
            this.btnFolderBrowse.Size = new System.Drawing.Size(26, 23);
            this.btnFolderBrowse.TabIndex = 5;
            this.btnFolderBrowse.Text = "...";
            this.btnFolderBrowse.UseVisualStyleBackColor = true;
            this.btnFolderBrowse.Click += new System.EventHandler(this.btnFolderBrowse_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(37, 66);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(169, 20);
            this.txtPath.TabIndex = 4;
            // 
            // rbAllDrives
            // 
            this.rbAllDrives.AutoSize = true;
            this.rbAllDrives.Checked = true;
            this.rbAllDrives.Location = new System.Drawing.Point(20, 19);
            this.rbAllDrives.Name = "rbAllDrives";
            this.rbAllDrives.Size = new System.Drawing.Size(97, 17);
            this.rbAllDrives.TabIndex = 2;
            this.rbAllDrives.TabStop = true;
            this.rbAllDrives.Text = "Scan All Drives";
            this.rbAllDrives.UseVisualStyleBackColor = true;
            // 
            // rbSelectedFolder
            // 
            this.rbSelectedFolder.AutoSize = true;
            this.rbSelectedFolder.Location = new System.Drawing.Point(20, 42);
            this.rbSelectedFolder.Name = "rbSelectedFolder";
            this.rbSelectedFolder.Size = new System.Drawing.Size(102, 17);
            this.rbSelectedFolder.TabIndex = 3;
            this.rbSelectedFolder.Text = "Selected Folder:";
            this.rbSelectedFolder.UseVisualStyleBackColor = true;
            // 
            // grpConsolidate
            // 
            this.grpConsolidate.Controls.Add(this.btnConsolidate);
            this.grpConsolidate.Controls.Add(this.btnBrowse);
            this.grpConsolidate.Controls.Add(this.lblFiles);
            this.grpConsolidate.Controls.Add(this.txtFiles);
            this.grpConsolidate.Location = new System.Drawing.Point(8, 330);
            this.grpConsolidate.Name = "grpConsolidate";
            this.grpConsolidate.Size = new System.Drawing.Size(274, 89);
            this.grpConsolidate.TabIndex = 5;
            this.grpConsolidate.TabStop = false;
            this.grpConsolidate.Text = "Consolidate";
            // 
            // btnConsolidate
            // 
            this.btnConsolidate.Location = new System.Drawing.Point(189, 58);
            this.btnConsolidate.Name = "btnConsolidate";
            this.btnConsolidate.Size = new System.Drawing.Size(75, 23);
            this.btnConsolidate.TabIndex = 5;
            this.btnConsolidate.Text = "Consolidate";
            this.btnConsolidate.UseVisualStyleBackColor = true;
            this.btnConsolidate.Click += new System.EventHandler(this.btnConsolidate_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(229, 14);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFindDuplicates);
            this.groupBox1.Controls.Add(this.btnDupBrowse);
            this.groupBox1.Controls.Add(this.txtDupSourceFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 425);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 87);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Duplicates";
            // 
            // btnFindDuplicates
            // 
            this.btnFindDuplicates.Location = new System.Drawing.Point(172, 58);
            this.btnFindDuplicates.Name = "btnFindDuplicates";
            this.btnFindDuplicates.Size = new System.Drawing.Size(88, 23);
            this.btnFindDuplicates.TabIndex = 3;
            this.btnFindDuplicates.Text = "Find Duplicates";
            this.btnFindDuplicates.UseVisualStyleBackColor = true;
            this.btnFindDuplicates.Click += new System.EventHandler(this.btnFindDuplicates_Click);
            // 
            // btnDupBrowse
            // 
            this.btnDupBrowse.Location = new System.Drawing.Point(225, 14);
            this.btnDupBrowse.Name = "btnDupBrowse";
            this.btnDupBrowse.Size = new System.Drawing.Size(34, 23);
            this.btnDupBrowse.TabIndex = 2;
            this.btnDupBrowse.Text = "...";
            this.btnDupBrowse.UseVisualStyleBackColor = true;
            this.btnDupBrowse.Click += new System.EventHandler(this.btnDupBrowse_Click);
            // 
            // txtDupSourceFile
            // 
            this.txtDupSourceFile.Location = new System.Drawing.Point(78, 17);
            this.txtDupSourceFile.Name = "txtDupSourceFile";
            this.txtDupSourceFile.Size = new System.Drawing.Size(141, 20);
            this.txtDupSourceFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source File:";
            // 
            // ExtractMovieNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(288, 524);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpConsolidate);
            this.Controls.Add(this.grpExtract);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExtractMovieNames";
            this.Text = "Extract Movie Names";
            this.grpExtract.ResumeLayout(false);
            this.gbPath.ResumeLayout(false);
            this.gbPath.PerformLayout();
            this.grpConsolidate.ResumeLayout(false);
            this.grpConsolidate.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox txtFiles;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.GroupBox grpExtract;
        private System.Windows.Forms.GroupBox grpConsolidate;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnConsolidate;
        private System.Windows.Forms.GroupBox gbPath;
        private System.Windows.Forms.Button btnFolderBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.RadioButton rbAllDrives;
        private System.Windows.Forms.RadioButton rbSelectedFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFindDuplicates;
        private System.Windows.Forms.Button btnDupBrowse;
        private System.Windows.Forms.TextBox txtDupSourceFile;
        private System.Windows.Forms.Label label1;
    }
}

