using System.Configuration;

namespace Tesseract_UI_Tools
{
    public partial class Main : Form
    {
        private TesseractUIParameters TessParams = new TesseractUIParameters();
        private TesseractMainWorker TesseractMainWorkerInstance;

        public Main()
        {
            InitializeComponent();
            TessParams.PropertyChanged += TessParams_PropertyChanged;
            tesseractUIParametersBindingSource.DataSource = TessParams;
            StartStopBtn.Enabled = TessParams.Validate();

            TesseractMainWorkerInstance = new TesseractMainWorker(TessParams);
            TesseractMainWorkerInstance.RunWorkerCompleted += TesseractMainWorkerInstance_RunWorkerCompleted;
            TesseractMainWorkerInstance.ProgressChanged += TesseractMainWorkerInstance_ProgressChanged;

            TesseractMainWorkerInstance.Report();
        }

        private void TessParams_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if( e.PropertyName == "Language" )
            {
                string[] SelectedLangs = TessParams.GetLanguage();
                for(int i = 0; i < LanguagesCheckedListBox.Items.Count; i++)
                {
                    if( SelectedLangs.Any( l => l == LanguagesCheckedListBox.Items[i].ToString() ) ){
                        LanguagesCheckedListBox.SetItemChecked(i, true);   
                    }
                    else
                    {
                        LanguagesCheckedListBox.SetItemChecked(i, false);
                    }

                }
            }
            if( TessParams.Validate() || TesseractMainWorkerInstance != null)
            {
                StartStopBtn.Enabled = true;
            }
            else
            {
                StartStopBtn.Enabled = false;
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            string[] langs = await TessdataUtil.Setup();
            langs.ToList().ForEach( lang => LanguagesCheckedListBox.Items.Add( lang, TessParams.GetLanguage().Any(l => l == lang) ) );
        }

        private void InputFolderClick(object sender, EventArgs e)
        {
            DialogResult result = FolderBrowserDialogInput.ShowDialog();
            if( result == DialogResult.OK)
            {
                TessParams.InputFolder = FolderBrowserDialogInput.SelectedPath;
            }
        }

        private void OutputFolderClick(object sender, EventArgs e)
        {
            DialogResult result = FolderBrowserDialogInput.ShowDialog();
            if (result == DialogResult.OK)
            {
                TessParams.OutputFolder = FolderBrowserDialogInput.SelectedPath;
            }
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            TessParams.Save();
        }

        private void TrackBar_Scroll_Tooltip(object sender, EventArgs e)
        {
            TrackBar senderObj = (TrackBar)sender;
            ScrollTip.SetToolTip(senderObj, $"{senderObj.Tag} {senderObj.Value}");
        }

        private void LanguagesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TessParams.SetLanguage(LanguagesCheckedListBox.CheckedItems.OfType<string>().ToArray());
        }

        private void ResetLabel_Click(object sender, EventArgs e)
        {
            TessParams.Reset();
        }

        private void StartStopBtn_Click(object sender, EventArgs e)
        {
            if (TesseractMainWorkerInstance.CancellationPending) return;
            if( TesseractMainWorkerInstance.IsBusy)
            {
                DialogResult Result = MessageBox.Show("Are you sure you want to Stop the process ?\nNote: Tesseract might take some time to exit.", "Stopping Tesseract", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if( Result == DialogResult.OK)
                {
                    TesseractMainWorkerInstance.CancelAsync();
                    StartStopBtn.Enabled = false;
                }
            }
            else
            {
                TesseractMainWorkerInstance.RunWorkerAsync();
                ToggleForm(false);
            }
        }

        private void ToggleForm(bool Enabled)
        {
            ResetLabel.Enabled = Enabled;
            InputFolderButton.Enabled = Enabled;
            InputFolderTextBox.Enabled = Enabled;
            OutputFolderButton.Enabled = Enabled;
            OutputFolderTextBox.Enabled = Enabled;
            LanguagesCheckedListBox.Enabled = Enabled;
            DpiTrackBar.Enabled = Enabled;
            QualityTrackBar.Enabled = Enabled;
            MinConfBar.Enabled = Enabled;
            OverwriteBox.Enabled = Enabled;
            ClearBox.Enabled = Enabled;
            if( Enabled)
            {
                StartStopBtn.Text = "Start";
                StartStopBtn.Enabled = TessParams.Validate();
            }
            else
            {
                StartStopBtn.Text = "Stop";
                StartStopBtn.Enabled = true;
            }
        }

        /* WORKER EVENTS */
        private void TesseractMainWorkerInstance_ProgressChanged(object? sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;

            TesseractMainWorkerProgressUserState State = (TesseractMainWorkerProgressUserState)e.UserState;
            StatusLabel.Text = State.Text;
            StatusProgressBar.Value = State.Value;
        }
        private void TesseractMainWorkerInstance_RunWorkerCompleted(object? sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ToggleForm(true);
        }
    }
}