namespace Tesseract_UI_Tools
{
    public partial class TesseractForm : Form
    {
        public TesseractUIParameters TessParams {
            get;
            private set;
        }
        /// <summary>
        /// Initialize form
        /// </summary>
        /// Set binding sources for <see cref="TesseractUIParameters"/> with UI and events
        /// Get strategies <see cref="AOcrStrategy"/>
        public TesseractForm(TesseractUIParameters? defaults = null)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            TessParams = defaults != null ? defaults : new TesseractUIParameters();

            TessParams.PropertyChanged += TessParams_PropertyChanged;
            tesseractUIParametersBindingSource.DataSource = TessParams;

            StrategyBox.Items.Add("");
            foreach( string Strat in AOcrStrategy.Strategies())
            {
                StrategyBox.Items.Add(Strat);
                if( TessParams.Strategy == Strat)
                {
                    StrategyBox.SelectedIndex = StrategyBox.Items.Count - 1;
                }
            }

            ConfirmBtn.Enabled = TessParams.Validate();
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
            ConfirmBtn.Enabled = TessParams.Validate();
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

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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
                ConfirmBtn.Enabled = TessParams.Validate();
            }
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