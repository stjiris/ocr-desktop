using System.Configuration;

namespace Tesseract_UI_Tools
{
    public partial class Main : Form
    {
        private TesseractUIParameters TessParams = new TesseractUIParameters();
        private TesseractMainWorker TesseractMainWorkerInstance;
        private EmailUIParameters EmailParams = new EmailUIParameters();

        public Main()
        {
            InitializeComponent();

            TessParams.PropertyChanged += TessParams_PropertyChanged;
            emailUIParametersBindingSource.DataSource = EmailParams;
            tesseractUIParametersBindingSource.DataSource = TessParams;

            TesseractMainWorkerInstance = new TesseractMainWorker(TessParams);
            TesseractMainWorkerInstance.RunWorkerCompleted += TesseractMainWorkerInstance_RunWorkerCompleted;
            TesseractMainWorkerInstance.ProgressChanged += TesseractMainWorkerInstance_ProgressChanged;

            TesseractMainWorkerInstance.Report();

            foreach( string Strat in AOcrStrategy.Strategies())
            {
                StrategyBox.Items.Add(Strat);
                if( TessParams.Strategy == Strat)
                {
                    StrategyBox.SelectedIndex = StrategyBox.Items.Count - 1;
                }
            }

            StartStopBtn.Enabled = TessParams.Validate();
        }

        private void TessParams_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if( e.PropertyName == "Language" )
            {
                string[] SelectedLangs = TessParams.GetLanguage();
                for(int i = 0; i < LanguagesCheckedListBox.Items.Count; i++)
                {
                    if( SelectedLangs.Any( l => TessdataUtil.Code2Lang(l) == LanguagesCheckedListBox.Items[i].ToString() ) ){
                        LanguagesCheckedListBox.SetItemChecked(i, true);   
                    }
                    else
                    {
                        LanguagesCheckedListBox.SetItemChecked(i, false);
                    }

                }
            }
            StartStopBtn.Enabled = TessParams.Validate();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            string[] langs = await TessdataUtil.Setup();
            // some languages first
            var L = new List<string>(new string[] {"eng","spa","fra","ara","chi_sim","rus"}).Concat(langs).ToList();
            L.ForEach( lang => LanguagesCheckedListBox.Items.Add( TessdataUtil.Code2Lang(lang), TessParams.GetLanguage().Any(l => l == lang) ) );
            LanguagesCheckedListBox.ItemCheck += LanguagesCheckedListBox_ItemCheck;
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
            EmailParams.Save();
        }

        private void TrackBar_Scroll_Tooltip(object sender, EventArgs e)
        {
            TrackBar senderObj = (TrackBar)sender;
            ScrollTip.SetToolTip(senderObj, $"{senderObj.Tag} {senderObj.Value}");
        }


        private void LanguagesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // This triggers BEFORE LanguagesCheckedListBox is updated there are workarounds with begin Invoke
            // TessParams also trigger this we need to prevent recursion.
            string LangChanged = TessdataUtil.Lang2Code(LanguagesCheckedListBox.Items[e.Index].ToString());
            IEnumerable<string> CurrLangs = LanguagesCheckedListBox.CheckedItems.OfType<string>().Select(l => TessdataUtil.Lang2Code(l));
            if ( e.NewValue == CheckState.Checked && !TessParams.GetLanguage().Contains(LangChanged) )
            {
                TessParams.SetLanguage(CurrLangs.Append(LangChanged).ToArray());
            }
            else if( e.NewValue == CheckState.Unchecked && TessParams.GetLanguage().Contains(LangChanged) )
            {
                TessParams.SetLanguage(CurrLangs.Where(Lang => Lang != LangChanged).ToArray());
            }
            ToggleForm(true);
        }

        private void StrategyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StrategyBox.SelectedItem == null) return;
            TessParams.Strategy = StrategyBox.SelectedItem.ToString();
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
            StrategyBox.Enabled = Enabled;
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

        private void ReportsFolderLabel_Click(object sender, EventArgs e)
        {
            TesseractMainWorkerInstance.OpenReportsFolder();
        }

        private void SendMail(string sub, string txt)
        {
            EmailUIParameters Params = new EmailUIParameters();
            if (Params.EmailTo == "") return;
            try{
                var client = new System.Net.Mail.SmtpClient(Params.Host, Params.Port);
                client.SendAsync(Params.EmailTo, Params.EmailTo, sub, txt, null);
                client.SendCompleted += delegate { client.Dispose(); };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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

            if( e.Cancelled)
            {
                // User cancelled
            }
            else if( e.Error != null )
            {
                System.Diagnostics.Debug.WriteLine("Error! " + e.Error.Message);
                SendMail("OCR Error!", e.Error.Message);
            }
            else
            {
                SendMail("OCR Success!", $"No errors to report.");
            }
        }

        private void OpenMailSettingsClick(object sender, EventArgs e)
        {
            var form = new MailSettingsForm(EmailParams);
            form.ShowDialog();
        }

        private void ResetLangs_Click(object sender, EventArgs e)
        {
            TessParams.SetLanguage(new string[] { });
        }
    }
}