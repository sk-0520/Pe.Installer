namespace Pe.Installer
{
    partial class InstallerForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerForm));
            this.gridMain = new System.Windows.Forms.TableLayoutPanel();
            this.labelDirectoryPath = new System.Windows.Forms.Label();
            this.labelPlatform = new System.Windows.Forms.Label();
            this.commandDirectoryPath = new System.Windows.Forms.Button();
            this.listPlatform = new System.Windows.Forms.ComboBox();
            this.labelTotalProgress = new System.Windows.Forms.Label();
            this.labelCurrentProgress = new System.Windows.Forms.Label();
            this.progressTotal = new System.Windows.Forms.ProgressBar();
            this.listLog = new System.Windows.Forms.ListBox();
            this.progressCurrent = new System.Windows.Forms.ProgressBar();
            this.commandExecute = new System.Windows.Forms.Button();
            this.commandClose = new System.Windows.Forms.Button();
            this.inputDirectoryPath = new System.Windows.Forms.TextBox();
            this.linkProject = new System.Windows.Forms.LinkLabel();
            this.inputUpdateUri = new System.Windows.Forms.TextBox();
            this.labelUpdateUri = new System.Windows.Forms.Label();
            this.gridMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridMain
            // 
            this.gridMain.ColumnCount = 3;
            this.gridMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.gridMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gridMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.gridMain.Controls.Add(this.labelDirectoryPath, 0, 1);
            this.gridMain.Controls.Add(this.labelPlatform, 0, 2);
            this.gridMain.Controls.Add(this.commandDirectoryPath, 2, 1);
            this.gridMain.Controls.Add(this.listPlatform, 1, 2);
            this.gridMain.Controls.Add(this.labelTotalProgress, 0, 3);
            this.gridMain.Controls.Add(this.labelCurrentProgress, 0, 4);
            this.gridMain.Controls.Add(this.progressTotal, 1, 3);
            this.gridMain.Controls.Add(this.listLog, 1, 5);
            this.gridMain.Controls.Add(this.progressCurrent, 1, 4);
            this.gridMain.Controls.Add(this.commandExecute, 1, 6);
            this.gridMain.Controls.Add(this.commandClose, 2, 6);
            this.gridMain.Controls.Add(this.inputDirectoryPath, 1, 1);
            this.gridMain.Controls.Add(this.linkProject, 0, 6);
            this.gridMain.Controls.Add(this.inputUpdateUri, 1, 0);
            this.gridMain.Controls.Add(this.labelUpdateUri, 0, 0);
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.Location = new System.Drawing.Point(6, 6);
            this.gridMain.Name = "gridMain";
            this.gridMain.RowCount = 7;
            this.gridMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.gridMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.gridMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.gridMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.gridMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.gridMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gridMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.gridMain.Size = new System.Drawing.Size(452, 449);
            this.gridMain.TabIndex = 0;
            // 
            // labelDirectoryPath
            // 
            this.labelDirectoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDirectoryPath.AutoSize = true;
            this.labelDirectoryPath.Location = new System.Drawing.Point(3, 25);
            this.labelDirectoryPath.Name = "labelDirectoryPath";
            this.labelDirectoryPath.Size = new System.Drawing.Size(85, 34);
            this.labelDirectoryPath.TabIndex = 0;
            this.labelDirectoryPath.Text = "*ディレクトリ*";
            this.labelDirectoryPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPlatform
            // 
            this.labelPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlatform.AutoSize = true;
            this.labelPlatform.Location = new System.Drawing.Point(3, 59);
            this.labelPlatform.Name = "labelPlatform";
            this.labelPlatform.Size = new System.Drawing.Size(85, 26);
            this.labelPlatform.TabIndex = 1;
            this.labelPlatform.Text = "*プラットフォーム*";
            this.labelPlatform.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // commandDirectoryPath
            // 
            this.commandDirectoryPath.AutoSize = true;
            this.commandDirectoryPath.Location = new System.Drawing.Point(368, 28);
            this.commandDirectoryPath.Name = "commandDirectoryPath";
            this.commandDirectoryPath.Size = new System.Drawing.Size(75, 28);
            this.commandDirectoryPath.TabIndex = 3;
            this.commandDirectoryPath.Text = "選択...";
            this.commandDirectoryPath.UseVisualStyleBackColor = true;
            this.commandDirectoryPath.Click += new System.EventHandler(this.commandDirectoryPath_Click);
            // 
            // listPlatform
            // 
            this.listPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.listPlatform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listPlatform.FormattingEnabled = true;
            this.listPlatform.Location = new System.Drawing.Point(94, 62);
            this.listPlatform.Name = "listPlatform";
            this.listPlatform.Size = new System.Drawing.Size(268, 20);
            this.listPlatform.TabIndex = 4;
            // 
            // labelTotalProgress
            // 
            this.labelTotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalProgress.AutoSize = true;
            this.labelTotalProgress.Location = new System.Drawing.Point(3, 85);
            this.labelTotalProgress.Name = "labelTotalProgress";
            this.labelTotalProgress.Size = new System.Drawing.Size(85, 29);
            this.labelTotalProgress.TabIndex = 5;
            this.labelTotalProgress.Text = "*全体進捗*";
            this.labelTotalProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCurrentProgress
            // 
            this.labelCurrentProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentProgress.AutoSize = true;
            this.labelCurrentProgress.Location = new System.Drawing.Point(3, 114);
            this.labelCurrentProgress.Name = "labelCurrentProgress";
            this.labelCurrentProgress.Size = new System.Drawing.Size(85, 29);
            this.labelCurrentProgress.TabIndex = 7;
            this.labelCurrentProgress.Text = "*現在進捗*";
            this.labelCurrentProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressTotal
            // 
            this.progressTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressTotal.Location = new System.Drawing.Point(94, 88);
            this.progressTotal.Name = "progressTotal";
            this.progressTotal.Size = new System.Drawing.Size(268, 23);
            this.progressTotal.TabIndex = 6;
            // 
            // listLog
            // 
            this.listLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLog.FormattingEnabled = true;
            this.listLog.HorizontalScrollbar = true;
            this.listLog.ItemHeight = 12;
            this.listLog.Location = new System.Drawing.Point(94, 146);
            this.listLog.Name = "listLog";
            this.listLog.ScrollAlwaysVisible = true;
            this.listLog.Size = new System.Drawing.Size(268, 253);
            this.listLog.TabIndex = 8;
            // 
            // progressCurrent
            // 
            this.progressCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressCurrent.Location = new System.Drawing.Point(94, 117);
            this.progressCurrent.Name = "progressCurrent";
            this.progressCurrent.Size = new System.Drawing.Size(268, 23);
            this.progressCurrent.TabIndex = 6;
            // 
            // commandExecute
            // 
            this.commandExecute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandExecute.Location = new System.Drawing.Point(94, 405);
            this.commandExecute.Name = "commandExecute";
            this.commandExecute.Size = new System.Drawing.Size(268, 41);
            this.commandExecute.TabIndex = 10;
            this.commandExecute.Text = "*インストール/実行*";
            this.commandExecute.UseVisualStyleBackColor = true;
            this.commandExecute.Click += new System.EventHandler(this.commandExecute_Click);
            // 
            // commandClose
            // 
            this.commandClose.AutoSize = true;
            this.commandClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandClose.Location = new System.Drawing.Point(368, 405);
            this.commandClose.Name = "commandClose";
            this.commandClose.Size = new System.Drawing.Size(81, 41);
            this.commandClose.TabIndex = 11;
            this.commandClose.Text = "*閉じる*";
            this.commandClose.UseVisualStyleBackColor = true;
            this.commandClose.Click += new System.EventHandler(this.commandClose_Click);
            // 
            // inputDirectoryPath
            // 
            this.inputDirectoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.inputDirectoryPath.Location = new System.Drawing.Point(94, 32);
            this.inputDirectoryPath.Name = "inputDirectoryPath";
            this.inputDirectoryPath.Size = new System.Drawing.Size(268, 19);
            this.inputDirectoryPath.TabIndex = 2;
            // 
            // linkProject
            // 
            this.linkProject.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkProject.AutoSize = true;
            this.linkProject.Location = new System.Drawing.Point(11, 419);
            this.linkProject.Name = "linkProject";
            this.linkProject.Size = new System.Drawing.Size(68, 12);
            this.linkProject.TabIndex = 12;
            this.linkProject.TabStop = true;
            this.linkProject.Text = "*プロジェクト*";
            this.linkProject.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkProject_LinkClicked);
            // 
            // inputTargetUri
            // 
            this.inputUpdateUri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.inputUpdateUri.Location = new System.Drawing.Point(94, 3);
            this.inputUpdateUri.Name = "inputTargetUri";
            this.inputUpdateUri.Size = new System.Drawing.Size(268, 19);
            this.inputUpdateUri.TabIndex = 12;
            // 
            // labelTargetUri
            // 
            this.labelUpdateUri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUpdateUri.AutoSize = true;
            this.labelUpdateUri.Location = new System.Drawing.Point(3, 0);
            this.labelUpdateUri.Name = "labelTargetUri";
            this.labelUpdateUri.Size = new System.Drawing.Size(85, 25);
            this.labelUpdateUri.TabIndex = 0;
            this.labelUpdateUri.Text = "*URI*";
            this.labelUpdateUri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(464, 461);
            this.Controls.Add(this.gridMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(318, 314);
            this.Name = "InstallerForm";
            this.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "*Pe Installer*";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InstallerForm_FormClosing);
            this.Load += new System.EventHandler(this.InstallerForm_Load);
            this.gridMain.ResumeLayout(false);
            this.gridMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel gridMain;
        private System.Windows.Forms.Label labelDirectoryPath;
        private System.Windows.Forms.Label labelPlatform;
        private System.Windows.Forms.TextBox inputDirectoryPath;
        private System.Windows.Forms.Button commandDirectoryPath;
        private System.Windows.Forms.ComboBox listPlatform;
        private System.Windows.Forms.Label labelTotalProgress;
        private System.Windows.Forms.Label labelCurrentProgress;
        private System.Windows.Forms.ProgressBar progressTotal;
        private System.Windows.Forms.ListBox listLog;
        private System.Windows.Forms.ProgressBar progressCurrent;
        private System.Windows.Forms.Button commandExecute;
        private System.Windows.Forms.Button commandClose;
        private System.Windows.Forms.LinkLabel linkProject;
        private System.Windows.Forms.TextBox inputUpdateUri;
        private System.Windows.Forms.Label labelUpdateUri;
    }
}

