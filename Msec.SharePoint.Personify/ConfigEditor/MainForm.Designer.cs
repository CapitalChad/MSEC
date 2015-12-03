namespace Msec.Personify.ConfigEditor {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.Label _siteFilePathLabel;
			System.Windows.Forms.Label _centralAdminFilePathLabel;
			System.Windows.Forms.Label _webServicesRootFilePathLabel;
			System.Windows.Forms.Label _outputLabel;
			this._mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this._fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._mainStatusStrip = new System.Windows.Forms.StatusStrip();
			this._versionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this._siteFilePathTextBox = new System.Windows.Forms.TextBox();
			this._siteFilePathButton = new System.Windows.Forms.Button();
			this._centralAdminFilePathButton = new System.Windows.Forms.Button();
			this._centralAdminFilePathTextBox = new System.Windows.Forms.TextBox();
			this._webServicesRootFilePathButton = new System.Windows.Forms.Button();
			this._webServicesRootFilePathTextBox = new System.Windows.Forms.TextBox();
			this._updateButton = new System.Windows.Forms.Button();
			this._outputTextBox = new System.Windows.Forms.TextBox();
			this._siteOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._centralAdminOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._webServicesRootOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._mainBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			_siteFilePathLabel = new System.Windows.Forms.Label();
			_centralAdminFilePathLabel = new System.Windows.Forms.Label();
			_webServicesRootFilePathLabel = new System.Windows.Forms.Label();
			_outputLabel = new System.Windows.Forms.Label();
			this._mainToolStripContainer.BottomToolStripPanel.SuspendLayout();
			this._mainToolStripContainer.ContentPanel.SuspendLayout();
			this._mainToolStripContainer.TopToolStripPanel.SuspendLayout();
			this._mainToolStripContainer.SuspendLayout();
			this._mainMenuStrip.SuspendLayout();
			this._mainStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// _mainToolStripContainer
			// 
			// 
			// _mainToolStripContainer.BottomToolStripPanel
			// 
			this._mainToolStripContainer.BottomToolStripPanel.Controls.Add(this._mainStatusStrip);
			// 
			// _mainToolStripContainer.ContentPanel
			// 
			this._mainToolStripContainer.ContentPanel.Controls.Add(this._outputTextBox);
			this._mainToolStripContainer.ContentPanel.Controls.Add(_outputLabel);
			this._mainToolStripContainer.ContentPanel.Controls.Add(this._updateButton);
			this._mainToolStripContainer.ContentPanel.Controls.Add(this._webServicesRootFilePathButton);
			this._mainToolStripContainer.ContentPanel.Controls.Add(_webServicesRootFilePathLabel);
			this._mainToolStripContainer.ContentPanel.Controls.Add(this._webServicesRootFilePathTextBox);
			this._mainToolStripContainer.ContentPanel.Controls.Add(this._centralAdminFilePathButton);
			this._mainToolStripContainer.ContentPanel.Controls.Add(_centralAdminFilePathLabel);
			this._mainToolStripContainer.ContentPanel.Controls.Add(this._centralAdminFilePathTextBox);
			this._mainToolStripContainer.ContentPanel.Controls.Add(this._siteFilePathButton);
			this._mainToolStripContainer.ContentPanel.Controls.Add(_siteFilePathLabel);
			this._mainToolStripContainer.ContentPanel.Controls.Add(this._siteFilePathTextBox);
			this._mainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(916, 376);
			this._mainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this._mainToolStripContainer.Location = new System.Drawing.Point(0, 0);
			this._mainToolStripContainer.Name = "_mainToolStripContainer";
			this._mainToolStripContainer.Size = new System.Drawing.Size(916, 422);
			this._mainToolStripContainer.TabIndex = 0;
			this._mainToolStripContainer.Text = "toolStripContainer1";
			// 
			// _mainToolStripContainer.TopToolStripPanel
			// 
			this._mainToolStripContainer.TopToolStripPanel.Controls.Add(this._mainMenuStrip);
			// 
			// _mainMenuStrip
			// 
			this._mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenuItem});
			this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this._mainMenuStrip.Name = "_mainMenuStrip";
			this._mainMenuStrip.Size = new System.Drawing.Size(916, 24);
			this._mainMenuStrip.TabIndex = 0;
			this._mainMenuStrip.Text = "menuStrip1";
			// 
			// _fileMenuItem
			// 
			this._fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileExitMenuItem});
			this._fileMenuItem.Name = "_fileMenuItem";
			this._fileMenuItem.Size = new System.Drawing.Size(35, 20);
			this._fileMenuItem.Text = "&File";
			// 
			// _fileExitMenuItem
			// 
			this._fileExitMenuItem.Name = "_fileExitMenuItem";
			this._fileExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this._fileExitMenuItem.Size = new System.Drawing.Size(152, 22);
			this._fileExitMenuItem.Text = "E&xit";
			this._fileExitMenuItem.Click += new System.EventHandler(this._fileExitMenuItem_Click);
			// 
			// _mainStatusStrip
			// 
			this._mainStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
			this._mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._versionStatusLabel});
			this._mainStatusStrip.Location = new System.Drawing.Point(0, 0);
			this._mainStatusStrip.Name = "_mainStatusStrip";
			this._mainStatusStrip.Size = new System.Drawing.Size(916, 22);
			this._mainStatusStrip.TabIndex = 0;
			// 
			// _versionStatusLabel
			// 
			this._versionStatusLabel.Name = "_versionStatusLabel";
			this._versionStatusLabel.Size = new System.Drawing.Size(901, 17);
			this._versionStatusLabel.Spring = true;
			this._versionStatusLabel.Tag = "";
			this._versionStatusLabel.Text = "1.0.0";
			// 
			// _siteFilePathTextBox
			// 
			this._siteFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._siteFilePathTextBox.Location = new System.Drawing.Point(189, 12);
			this._siteFilePathTextBox.Name = "_siteFilePathTextBox";
			this._siteFilePathTextBox.Size = new System.Drawing.Size(682, 20);
			this._siteFilePathTextBox.TabIndex = 1;
			this._siteFilePathTextBox.Text = "C:\\inetpub\\wwwroot\\wss\\VirtualDirectories";
			this._siteFilePathTextBox.TextChanged += new System.EventHandler(this._siteFilePathTextBox_TextChanged);
			// 
			// _siteFilePathLabel
			// 
			_siteFilePathLabel.AutoSize = true;
			_siteFilePathLabel.Location = new System.Drawing.Point(12, 15);
			_siteFilePathLabel.Name = "_siteFilePathLabel";
			_siteFilePathLabel.Size = new System.Drawing.Size(96, 13);
			_siteFilePathLabel.TabIndex = 0;
			_siteFilePathLabel.Text = "Site web.config file";
			// 
			// _siteFilePathButton
			// 
			this._siteFilePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._siteFilePathButton.Location = new System.Drawing.Point(877, 10);
			this._siteFilePathButton.Name = "_siteFilePathButton";
			this._siteFilePathButton.Size = new System.Drawing.Size(27, 23);
			this._siteFilePathButton.TabIndex = 2;
			this._siteFilePathButton.Text = "...";
			this._siteFilePathButton.UseVisualStyleBackColor = true;
			this._siteFilePathButton.Click += new System.EventHandler(this._siteFilePathButton_Click);
			// 
			// _centralAdminFilePathButton
			// 
			this._centralAdminFilePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._centralAdminFilePathButton.Location = new System.Drawing.Point(877, 36);
			this._centralAdminFilePathButton.Name = "_centralAdminFilePathButton";
			this._centralAdminFilePathButton.Size = new System.Drawing.Size(27, 23);
			this._centralAdminFilePathButton.TabIndex = 5;
			this._centralAdminFilePathButton.Text = "...";
			this._centralAdminFilePathButton.UseVisualStyleBackColor = true;
			this._centralAdminFilePathButton.Click += new System.EventHandler(this._centralAdminFilePathButton_Click);
			// 
			// _centralAdminFilePathLabel
			// 
			_centralAdminFilePathLabel.AutoSize = true;
			_centralAdminFilePathLabel.Location = new System.Drawing.Point(12, 41);
			_centralAdminFilePathLabel.Name = "_centralAdminFilePathLabel";
			_centralAdminFilePathLabel.Size = new System.Drawing.Size(143, 13);
			_centralAdminFilePathLabel.TabIndex = 3;
			_centralAdminFilePathLabel.Text = "Central Admin web.config file";
			// 
			// _centralAdminFilePathTextBox
			// 
			this._centralAdminFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._centralAdminFilePathTextBox.Location = new System.Drawing.Point(189, 38);
			this._centralAdminFilePathTextBox.Name = "_centralAdminFilePathTextBox";
			this._centralAdminFilePathTextBox.Size = new System.Drawing.Size(682, 20);
			this._centralAdminFilePathTextBox.TabIndex = 4;
			this._centralAdminFilePathTextBox.Text = "C:\\inetpub\\wwwroot\\wss\\VirtualDirectories";
			this._centralAdminFilePathTextBox.TextChanged += new System.EventHandler(this._centralAdminFilePathTextBox_TextChanged);
			// 
			// _webServicesRootFilePathButton
			// 
			this._webServicesRootFilePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._webServicesRootFilePathButton.Location = new System.Drawing.Point(877, 62);
			this._webServicesRootFilePathButton.Name = "_webServicesRootFilePathButton";
			this._webServicesRootFilePathButton.Size = new System.Drawing.Size(27, 23);
			this._webServicesRootFilePathButton.TabIndex = 8;
			this._webServicesRootFilePathButton.Text = "...";
			this._webServicesRootFilePathButton.UseVisualStyleBackColor = true;
			this._webServicesRootFilePathButton.Click += new System.EventHandler(this._webServicesRootFilePathButton_Click);
			// 
			// _webServicesRootFilePathLabel
			// 
			_webServicesRootFilePathLabel.AutoSize = true;
			_webServicesRootFilePathLabel.Location = new System.Drawing.Point(12, 67);
			_webServicesRootFilePathLabel.Name = "_webServicesRootFilePathLabel";
			_webServicesRootFilePathLabel.Size = new System.Drawing.Size(171, 13);
			_webServicesRootFilePathLabel.TabIndex = 6;
			_webServicesRootFilePathLabel.Text = "Web Services Root web.config file";
			// 
			// _webServicesRootFilePathTextBox
			// 
			this._webServicesRootFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._webServicesRootFilePathTextBox.Location = new System.Drawing.Point(189, 64);
			this._webServicesRootFilePathTextBox.Name = "_webServicesRootFilePathTextBox";
			this._webServicesRootFilePathTextBox.Size = new System.Drawing.Size(682, 20);
			this._webServicesRootFilePathTextBox.TabIndex = 7;
			this._webServicesRootFilePathTextBox.Text = "C:\\Program Files\\Common Files\\Microsoft Shared\\Web Server Extensions\\14\\WebServic" +
    "es\\Root\\web.config";
			this._webServicesRootFilePathTextBox.TextChanged += new System.EventHandler(this._webServicesRootFilePathTextBox_TextChanged);
			// 
			// _updateButton
			// 
			this._updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._updateButton.Enabled = false;
			this._updateButton.Location = new System.Drawing.Point(829, 91);
			this._updateButton.Name = "_updateButton";
			this._updateButton.Size = new System.Drawing.Size(75, 23);
			this._updateButton.TabIndex = 9;
			this._updateButton.Text = "Update";
			this._updateButton.UseVisualStyleBackColor = true;
			this._updateButton.Click += new System.EventHandler(this._updateButton_Click);
			// 
			// _outputLabel
			// 
			_outputLabel.AutoSize = true;
			_outputLabel.Location = new System.Drawing.Point(15, 106);
			_outputLabel.Name = "_outputLabel";
			_outputLabel.Size = new System.Drawing.Size(39, 13);
			_outputLabel.TabIndex = 10;
			_outputLabel.Text = "Output";
			// 
			// _outputTextBox
			// 
			this._outputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._outputTextBox.Location = new System.Drawing.Point(12, 122);
			this._outputTextBox.Multiline = true;
			this._outputTextBox.Name = "_outputTextBox";
			this._outputTextBox.ReadOnly = true;
			this._outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._outputTextBox.Size = new System.Drawing.Size(892, 242);
			this._outputTextBox.TabIndex = 11;
			// 
			// _siteOpenFileDialog
			// 
			this._siteOpenFileDialog.FileName = "web.config";
			this._siteOpenFileDialog.Filter = "Web configuration file|web.config";
			this._siteOpenFileDialog.InitialDirectory = "C:\\inetpub\\wwwroot\\wss\\VirtualDirectories";
			this._siteOpenFileDialog.Title = "Browse for web.config file";
			// 
			// _centralAdminOpenFileDialog
			// 
			this._centralAdminOpenFileDialog.FileName = "web.config";
			this._centralAdminOpenFileDialog.Filter = "Web configuration file|web.config";
			this._centralAdminOpenFileDialog.InitialDirectory = "C:\\inetpub\\wwwroot\\wss\\VirtualDirectories";
			this._centralAdminOpenFileDialog.Title = "Browse for web.config file";
			// 
			// _webServicesRootOpenFileDialog
			// 
			this._webServicesRootOpenFileDialog.FileName = "web.config";
			this._webServicesRootOpenFileDialog.Filter = "Web configuration file|web.config";
			this._webServicesRootOpenFileDialog.InitialDirectory = "C:\\Program Files\\Common Files\\Microsoft Shared\\Web Server Extensions\\14\\WebServic" +
    "es\\Root";
			this._webServicesRootOpenFileDialog.Title = "Browse for web.config file";
			// 
			// _mainBackgroundWorker
			// 
			this._mainBackgroundWorker.WorkerReportsProgress = true;
			this._mainBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this._mainBackgroundWorker_DoWork);
			this._mainBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this._mainBackgroundWorker_ProgressChanged);
			this._mainBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._mainBackgroundWorker_RunWorkerCompleted);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(916, 422);
			this.Controls.Add(this._mainToolStripContainer);
			this.MainMenuStrip = this._mainMenuStrip;
			this.Name = "MainForm";
			this.Text = "SharePoint/Personify Config Modifier";
			this._mainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this._mainToolStripContainer.BottomToolStripPanel.PerformLayout();
			this._mainToolStripContainer.ContentPanel.ResumeLayout(false);
			this._mainToolStripContainer.ContentPanel.PerformLayout();
			this._mainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this._mainToolStripContainer.TopToolStripPanel.PerformLayout();
			this._mainToolStripContainer.ResumeLayout(false);
			this._mainToolStripContainer.PerformLayout();
			this._mainMenuStrip.ResumeLayout(false);
			this._mainMenuStrip.PerformLayout();
			this._mainStatusStrip.ResumeLayout(false);
			this._mainStatusStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer _mainToolStripContainer;
		private System.Windows.Forms.StatusStrip _mainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel _versionStatusLabel;
		private System.Windows.Forms.MenuStrip _mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem _fileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _fileExitMenuItem;
		private System.Windows.Forms.Button _webServicesRootFilePathButton;
		private System.Windows.Forms.TextBox _webServicesRootFilePathTextBox;
		private System.Windows.Forms.Button _centralAdminFilePathButton;
		private System.Windows.Forms.TextBox _centralAdminFilePathTextBox;
		private System.Windows.Forms.Button _siteFilePathButton;
		private System.Windows.Forms.TextBox _siteFilePathTextBox;
		private System.Windows.Forms.Button _updateButton;
		private System.Windows.Forms.TextBox _outputTextBox;
		private System.Windows.Forms.OpenFileDialog _siteOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog _centralAdminOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog _webServicesRootOpenFileDialog;
		private System.ComponentModel.BackgroundWorker _mainBackgroundWorker;
	}
}

