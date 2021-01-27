using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pe.Installer
{
    public partial class InstallerForm: Form
    {
        public InstallerForm()
        {
            InitializeComponent();
            Font = SystemFonts.MessageBoxFont;
            LoggerFactory = new InternalLoggerFactory(this.listLog);
            Logger = LoggerFactory.CreateLogger(GetType());

            Logger.LogInfo("START");
        }


        #region property

        ILoggerFactory LoggerFactory { get; }
        ILogger Logger { get; }

        bool Installed { get; set; }

        Task CurrentTask { get; set; }
        CancellationTokenSource CancellationTokenSource { get; set; }

        string ExecutePath { get; set; }

        #endregion

        #region function

        #endregion

        private void InstallerForm_Load(object sender, EventArgs e)
        {
            // ディレクトリ設定
            this.inputDirectoryPath.Text = Constants.InstallDirectoryPath;

            // プラットフォーム設定
            if(Environment.Is64BitOperatingSystem) {
                this.listPlatform.Items.Add(new PlatformListItem(Properties.Resources.String_Platform_64bit, "x64"));
            }
            this.listPlatform.Items.Add(new PlatformListItem(Properties.Resources.String_Platform_32bit, "x86"));
            this.listPlatform.SelectedIndex = 0;

            // リソース増えるとしんどいので手で頑張る
            Text = Properties.Resources.String_Caption;
            this.labelDirectoryPath.Text = Properties.Resources.String_Label_DirectoryPath_A;
            this.labelPlatform.Text = Properties.Resources.String_Label_Platform_A;
            this.labelTotalProgress.Text = Properties.Resources.String_Label_TotalProgress_A;
            this.labelCurrentProgress.Text = Properties.Resources.String_Label_CurrentProgress_A;
            this.commandDirectoryPath.Text = Properties.Resources.String_SelectDirectoryPath_A;
            this.commandExecute.Text = Properties.Resources.String_Execute_Install_A;
            this.commandClose.Text = Properties.Resources.String_Close_A;

        }

        private void commandDirectoryPath_Click(object sender, EventArgs e)
        {
            using(var dialog = new FolderBrowserDialog() {
                SelectedPath = this.inputDirectoryPath.Text,
                ShowNewFolderButton = true,
            }) {
                if(dialog.ShowDialog() == DialogResult.OK) {
                    this.inputDirectoryPath.Text = dialog.SelectedPath;
                }
            }

        }

        private void commandClose_Click(object sender, EventArgs e)
        {
            if(CancellationTokenSource != null) {
                this.commandClose.Enabled = false;
                CancellationTokenSource.Cancel();
            } else {
                Close();
            }
        }

        private async void commandExecute_Click(object sender, EventArgs e)
        {
            if(Installed) {
                var appPath = Path.Combine(this.commandClose.Text, "Pe.exe");
#if DEBUG
                var user = Path.Combine(Constants.ApplicationDataDirectoryPath, "user");
                var machine = Path.Combine(Constants.ApplicationDataDirectoryPath, "machine");
                var temp = Path.Combine(Constants.ApplicationDataDirectoryPath, "temp");
                Process.Start(appPath, $" --user-dir=\"{user}\" --machine-dir=\"{machine}\" --temp-dir=\"{temp}\"");
#else
                Process.Start(appPath);
#endif
                Close();
            } else {
                Logger.LogInfo("INSTALL");

                this.commandExecute.Enabled = false;
                this.commandDirectoryPath.Enabled = false;
                this.inputDirectoryPath.ReadOnly = true;
                this.listPlatform.Enabled = false;

                this.commandClose.Text = Properties.Resources.String_Cancel_A;
                CancellationTokenSource = new CancellationTokenSource();

                var progress = new {
                    Total = new ProgressLogger(this.progressTotal),
                    Current = new ProgressLogger(this.progressCurrent),
                };

                progress.Total.Reset(100 / 4);

                try {
                    var selectedItem = (PlatformListItem)this.listPlatform.SelectedItem;

                    var downloader = new Downloader(progress.Current, LoggerFactory);

                    var updateItemData = await downloader.GetUpdateItemDataAsync(Constants.UpdateFileUri, selectedItem.Value, CancellationTokenSource.Token);
                    progress.Total.Stepup();

                    var stream = await downloader.GetArchiveAsync(updateItemData.ArchiveUri, CancellationTokenSource.Token);
                    progress.Total.Stepup();

                    var hash = new Checker(progress.Current, LoggerFactory);
                    var isChecked = await hash.CheckAsync(stream, updateItemData.ArchiveSize, updateItemData.ArchiveHashKind, updateItemData.ArchiveHashValue);
                    if(!isChecked) {
                        Logger.LogWarning("check error");
                        return;
                    }
                    progress.Total.Stepup();

                    var extractor = new Extractor(progress.Current, LoggerFactory);
                    await extractor.ExtractAsync(stream, new DirectoryInfo(this.inputDirectoryPath.Text), updateItemData.ArchiveKind);
                    progress.Total.Stepup();

                    Installed = true;
                    this.commandExecute.Text = Properties.Resources.String_Execute_Start_A;

                } catch(OperationCanceledException) {
                    Logger.LogInfo("cancel");
                } catch(Exception ex) {
                    Logger.LogError(ex.ToString());
                } finally {
                    this.commandExecute.Enabled = true;
                    this.commandDirectoryPath.Enabled = !Installed;
                    this.inputDirectoryPath.ReadOnly = Installed;
                    this.listPlatform.Enabled = !Installed;

                    this.commandClose.Enabled = true;

                    this.commandClose.Text = Properties.Resources.String_Close_A;

                    //progress.Total.Reset(0); // 全体進捗は一応残しておく
                    progress.Current.Reset(0);

                    var cancel = CancellationTokenSource;
                    CancellationTokenSource = null;
                    try {
                        cancel.Dispose();
                    } catch { }
                }
            }
        }

    }
}
