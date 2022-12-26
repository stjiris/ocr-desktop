namespace Tesseract_UI_Tools
{
    public partial class Main : Form
    {
        private TesseractUIParameters TessParams = new TesseractUIParameters();
        private TesseractMainWorker TesseractMainWorkerInstance;
        private EmailUIParameters EmailParams = new EmailUIParameters();

        /// <summary>
        /// Initialize form
        /// </summary>
        /// Set binding sources for <see cref="TesseractUIParameters"/> and <see cref="EmailUIParameters"/> with UI and events
        /// Get strategies <see cref="AOcrStrategy"/>
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

        /// <summary>
        /// Handle property changes of <seealso cref="TesseractUIParameters"/> to reflect them on the UI.
        /// </summary>
        /// Currently <see cref="TesseractUIParameters.Language"/> is represented as a <see cref="ListBox"/> on the UI. This handles that transformation
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Downloads if needed the tessdata folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainForm_Load(object sender, EventArgs e)
        {
            string[] langs = await TessdataUtil.Setup();
            // some languages first
            var L = new List<string>(new string[] {"eng","spa","fra","ara","chi_sim","rus"}).Concat(langs).ToList();
            L.ForEach( lang => LanguagesCheckedListBox.Items.Add( TessdataUtil.Code2Lang(lang), TessParams.GetLanguage().Any(l => l == lang) ) );
            LanguagesCheckedListBox.ItemCheck += LanguagesCheckedListBox_ItemCheck;
        }

        /// <summary>
        /// Select Input folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputFolderClick(object sender, EventArgs e)
        {
            DialogResult result = FolderBrowserDialogInput.ShowDialog();
            if( result == DialogResult.OK)
            {
                TessParams.InputFolder = FolderBrowserDialogInput.SelectedPath;
            }
        }

        /// <summary>
        /// Select Output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputFolderClick(object sender, EventArgs e)
        {
            DialogResult result = FolderBrowserDialogInput.ShowDialog();
            if (result == DialogResult.OK)
            {
                TessParams.OutputFolder = FolderBrowserDialogInput.SelectedPath;
            }
        }

        /// <summary>
        /// Save <see cref="TesseractUIParameters">TessParams</see> and <see cref="EmailUIParameters">EmailParams</see>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            TessParams.Save();
            EmailParams.Save();
        }

        /// <summary>
        /// Display tooltip with units for the trackbars
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBar_Scroll_Tooltip(object sender, EventArgs e)
        {
            TrackBar senderObj = (TrackBar)sender;
            ScrollTip.SetToolTip(senderObj, $"{senderObj.Tag} {senderObj.Value}");
        }

        /// <summary>
        /// Handle UI changes on the ListBox to reflect them on <seealso cref="TesseractUIParameters.Language"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handle UI changes on the SelectBox to reflect them on <seealso cref="TesseractUIParameters"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrategyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StrategyBox.SelectedItem == null) return;
            TessParams.Strategy = StrategyBox.SelectedItem.ToString();
        }

        /// <summary>
        /// Applies <seealso cref="System.Configuration.ApplicationSettingsBase.Reset"/> on <seealso cref="TesseractUIParameters"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetLabel_Click(object sender, EventArgs e)
        {
            TessParams.Reset();
        }

        /// <summary>
        /// Toggles <see cref="TesseractMainWorker"/> to run or stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Enables or disables the whole form.
        /// </summary>
        /// <param name="Enabled"></param>
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

        /// <summary>
        /// Starts a process to one the file explorer on the reports folder location. <see cref="TesseractMainWorker.OpenReportsFolder"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportsFolderLabel_Click(object sender, EventArgs e)
        {
            TesseractMainWorkerInstance.OpenReportsFolder();
        }

        /// <summary>
        /// Sends email using <see cref="EmailUIParameters"/> configuration.
        /// </summary>
        /// <param name="sub">Subject</param>
        /// <param name="txt">Text to Send</param>
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
        /// <summary>
        /// Handle progess changes of <see cref="TesseractMainWorker"/> to reflect them on the UI.
        /// Uses <see cref="TesseractMainWorkerProgressUserState"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TesseractMainWorkerInstance_ProgressChanged(object? sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;

            TesseractMainWorkerProgressUserState State = (TesseractMainWorkerProgressUserState)e.UserState;
            StatusLabel.Text = State.Text;
            StatusProgressBar.Value = State.Value;
        }

        /// <summary>
        /// Handle work completed of <seealso cref="TesseractMainWorker"/>. Tries to send email with result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TesseractMainWorkerInstance_RunWorkerCompleted(object? sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ToggleForm(true);
            StatusProgressBar.Value = 0;
            string report = "";

            if( e.Cancelled)
            {
                report = "User Cancelled";
                
            }
            else if( e.Error != null )
            {
                report = "Error: " + e.Error.Message;
                System.Diagnostics.Debug.WriteLine("Error! " + e.Error.Message);
                SendMail("OCR Error!", e.Error.Message);
                MessageBox.Show(e.ToString(),"Error Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                SendMail("OCR Success!", $"No errors to report.");
                report = "Success";
            }
            StatusLabel.Text = report;

        }

        /// <summary>
        /// Shows new form to change <see cref="EmailUIParameters"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMailSettingsClick(object sender, EventArgs e)
        {
            var form = new MailSettingsForm(EmailParams);
            form.ShowDialog();
        }

        /// <summary>
        /// Resets <see cref="TesseractUIParameters.Language"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetLangs_Click(object sender, EventArgs e)
        {
            TessParams.SetLanguage(new string[] { });
        }
    }
}