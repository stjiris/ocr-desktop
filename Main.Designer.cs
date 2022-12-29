namespace Tesseract_UI_Tools
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.FolderBrowserDialogInput = new System.Windows.Forms.FolderBrowserDialog();
            this.LanguagesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.InputFolderTextBox = new System.Windows.Forms.TextBox();
            this.tesseractUIParametersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.InputFolderButton = new System.Windows.Forms.Button();
            this.OutputFolderButton = new System.Windows.Forms.Button();
            this.OutputFolderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.QualityTrackBar = new System.Windows.Forms.TrackBar();
            this.DpiTrackBar = new System.Windows.Forms.TrackBar();
            this.ScrollTip = new System.Windows.Forms.ToolTip(this.components);
            this.OverwriteBox = new System.Windows.Forms.CheckBox();
            this.ClearBox = new System.Windows.Forms.CheckBox();
            this.StartStopBtn = new System.Windows.Forms.Button();
            this.StatusProgressBar = new System.Windows.Forms.ProgressBar();
            this.label10 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ResetLabel = new System.Windows.Forms.Label();
            this.MinConfBar = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ReportsFolderLabel = new System.Windows.Forms.Label();
            this.StrategyBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.NotifyIconSucess = new System.Windows.Forms.NotifyIcon(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.mailBox = new System.Windows.Forms.TextBox();
            this.emailUIParametersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.ResetLangs = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tesseractUIParametersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DpiTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinConfBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailUIParametersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // LanguagesCheckedListBox
            // 
            this.LanguagesCheckedListBox.ColumnWidth = 220;
            this.LanguagesCheckedListBox.FormattingEnabled = true;
            this.LanguagesCheckedListBox.Location = new System.Drawing.Point(12, 116);
            this.LanguagesCheckedListBox.MultiColumn = true;
            this.LanguagesCheckedListBox.Name = "LanguagesCheckedListBox";
            this.LanguagesCheckedListBox.Size = new System.Drawing.Size(776, 151);
            this.LanguagesCheckedListBox.TabIndex = 5;
            // 
            // InputFolderTextBox
            // 
            this.InputFolderTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tesseractUIParametersBindingSource, "InputFolder", true));
            this.InputFolderTextBox.Location = new System.Drawing.Point(205, 32);
            this.InputFolderTextBox.Name = "InputFolderTextBox";
            this.InputFolderTextBox.ReadOnly = true;
            this.InputFolderTextBox.Size = new System.Drawing.Size(583, 26);
            this.InputFolderTextBox.TabIndex = 2;
            this.InputFolderTextBox.Click += new System.EventHandler(this.InputFolderClick);
            // 
            // tesseractUIParametersBindingSource
            // 
            this.tesseractUIParametersBindingSource.DataSource = typeof(Tesseract_UI_Tools.TesseractUIParameters);
            // 
            // InputFolderButton
            // 
            this.InputFolderButton.Location = new System.Drawing.Point(12, 32);
            this.InputFolderButton.Name = "InputFolderButton";
            this.InputFolderButton.Size = new System.Drawing.Size(187, 26);
            this.InputFolderButton.TabIndex = 1;
            this.InputFolderButton.Text = "Select Input Folder";
            this.InputFolderButton.UseVisualStyleBackColor = true;
            this.InputFolderButton.Click += new System.EventHandler(this.InputFolderClick);
            // 
            // OutputFolderButton
            // 
            this.OutputFolderButton.Location = new System.Drawing.Point(12, 64);
            this.OutputFolderButton.Name = "OutputFolderButton";
            this.OutputFolderButton.Size = new System.Drawing.Size(187, 26);
            this.OutputFolderButton.TabIndex = 3;
            this.OutputFolderButton.Text = "Select Output Folder";
            this.OutputFolderButton.UseVisualStyleBackColor = true;
            this.OutputFolderButton.Click += new System.EventHandler(this.OutputFolderClick);
            // 
            // OutputFolderTextBox
            // 
            this.OutputFolderTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tesseractUIParametersBindingSource, "OutputFolder", true));
            this.OutputFolderTextBox.Location = new System.Drawing.Point(205, 64);
            this.OutputFolderTextBox.Name = "OutputFolderTextBox";
            this.OutputFolderTextBox.ReadOnly = true;
            this.OutputFolderTextBox.Size = new System.Drawing.Size(583, 26);
            this.OutputFolderTextBox.TabIndex = 4;
            this.OutputFolderTextBox.Click += new System.EventHandler(this.OutputFolderClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folders";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(205, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(429, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "(Select the input folder to process files and output folder for the results)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Languages";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(205, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(349, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "(Select one or more languages that your files may contain)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 309);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Output Parameters";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 340);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "DPI:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(358, 343);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(430, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "(Affects the width and height of each page. Minimum: 70 Maximum: 300)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 398);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Quality:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label9.Location = new System.Drawing.Point(356, 401);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(432, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "(Affects the JPG compression of each image. Minimum: 0 Maximum: 100)";
            // 
            // QualityTrackBar
            // 
            this.QualityTrackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "Quality", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.QualityTrackBar.LargeChange = 10;
            this.QualityTrackBar.Location = new System.Drawing.Point(104, 391);
            this.QualityTrackBar.Maximum = 100;
            this.QualityTrackBar.Name = "QualityTrackBar";
            this.QualityTrackBar.Size = new System.Drawing.Size(176, 53);
            this.QualityTrackBar.SmallChange = 5;
            this.QualityTrackBar.TabIndex = 8;
            this.QualityTrackBar.Tag = "QUALITY";
            this.QualityTrackBar.TickFrequency = 5;
            this.QualityTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.QualityTrackBar.Value = 100;
            // 
            // DpiTrackBar
            // 
            this.DpiTrackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "Dpi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DpiTrackBar.LargeChange = 20;
            this.DpiTrackBar.Location = new System.Drawing.Point(104, 332);
            this.DpiTrackBar.Maximum = 300;
            this.DpiTrackBar.Minimum = 70;
            this.DpiTrackBar.Name = "DpiTrackBar";
            this.DpiTrackBar.Size = new System.Drawing.Size(176, 53);
            this.DpiTrackBar.SmallChange = 5;
            this.DpiTrackBar.TabIndex = 7;
            this.DpiTrackBar.Tag = "DPI";
            this.DpiTrackBar.TickFrequency = 5;
            this.DpiTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.DpiTrackBar.Value = 100;
            // 
            // OverwriteBox
            // 
            this.OverwriteBox.AutoSize = true;
            this.OverwriteBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.tesseractUIParametersBindingSource, "Overwrite", true));
            this.OverwriteBox.Location = new System.Drawing.Point(12, 509);
            this.OverwriteBox.Name = "OverwriteBox";
            this.OverwriteBox.Size = new System.Drawing.Size(92, 24);
            this.OverwriteBox.TabIndex = 10;
            this.OverwriteBox.Text = "Overwrite";
            this.OverwriteBox.UseVisualStyleBackColor = true;
            // 
            // ClearBox
            // 
            this.ClearBox.AutoSize = true;
            this.ClearBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.tesseractUIParametersBindingSource, "Clear", true));
            this.ClearBox.Location = new System.Drawing.Point(110, 509);
            this.ClearBox.Name = "ClearBox";
            this.ClearBox.Size = new System.Drawing.Size(170, 24);
            this.ClearBox.TabIndex = 11;
            this.ClearBox.Text = "Clear Temporary Files";
            this.ClearBox.UseVisualStyleBackColor = true;
            // 
            // StartStopBtn
            // 
            this.StartStopBtn.Location = new System.Drawing.Point(698, 506);
            this.StartStopBtn.Name = "StartStopBtn";
            this.StartStopBtn.Size = new System.Drawing.Size(90, 28);
            this.StartStopBtn.TabIndex = 12;
            this.StartStopBtn.Text = "Start";
            this.StartStopBtn.UseVisualStyleBackColor = true;
            this.StartStopBtn.Click += new System.EventHandler(this.StartStopBtn_Click);
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Location = new System.Drawing.Point(12, 584);
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(776, 28);
            this.StatusProgressBar.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 561);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Status:";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StatusLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.StatusLabel.Location = new System.Drawing.Point(63, 562);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(65, 17);
            this.StatusLabel.TabIndex = 32;
            this.StatusLabel.Text = "Initializing";
            // 
            // ResetLabel
            // 
            this.ResetLabel.AutoSize = true;
            this.ResetLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ResetLabel.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ResetLabel.ForeColor = System.Drawing.Color.Red;
            this.ResetLabel.Location = new System.Drawing.Point(695, 10);
            this.ResetLabel.Name = "ResetLabel";
            this.ResetLabel.Size = new System.Drawing.Size(93, 17);
            this.ResetLabel.TabIndex = 12;
            this.ResetLabel.Text = "(reset params)";
            this.ResetLabel.Click += new System.EventHandler(this.ResetLabel_Click);
            // 
            // MinConfBar
            // 
            this.MinConfBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "MinimumConfidence", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MinConfBar.LargeChange = 10;
            this.MinConfBar.Location = new System.Drawing.Point(104, 450);
            this.MinConfBar.Maximum = 100;
            this.MinConfBar.Name = "MinConfBar";
            this.MinConfBar.Size = new System.Drawing.Size(176, 53);
            this.MinConfBar.SmallChange = 5;
            this.MinConfBar.TabIndex = 9;
            this.MinConfBar.Tag = "MIN CONF";
            this.MinConfBar.TickFrequency = 5;
            this.MinConfBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.MinConfBar.Value = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(383, 463);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(405, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "(Minimum confidence allowed for each word when creating the PDF)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(26, 462);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Min Conf:";
            // 
            // ReportsFolderLabel
            // 
            this.ReportsFolderLabel.AutoSize = true;
            this.ReportsFolderLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReportsFolderLabel.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReportsFolderLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ReportsFolderLabel.Location = new System.Drawing.Point(560, 512);
            this.ReportsFolderLabel.Name = "ReportsFolderLabel";
            this.ReportsFolderLabel.Size = new System.Drawing.Size(132, 17);
            this.ReportsFolderLabel.TabIndex = 33;
            this.ReportsFolderLabel.Text = "(open reports folder)";
            this.ReportsFolderLabel.Click += new System.EventHandler(this.ReportsFolderLabel_Click);
            // 
            // StrategyBox
            // 
            this.StrategyBox.FormattingEnabled = true;
            this.StrategyBox.Location = new System.Drawing.Point(179, 273);
            this.StrategyBox.Name = "StrategyBox";
            this.StrategyBox.Size = new System.Drawing.Size(187, 27);
            this.StrategyBox.TabIndex = 6;
            this.StrategyBox.SelectedIndexChanged += new System.EventHandler(this.StrategyBox_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 276);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(161, 20);
            this.label13.TabIndex = 35;
            this.label13.Text = "Preprocessing strategy:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label14.Location = new System.Drawing.Point(548, 277);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(240, 17);
            this.label14.TabIndex = 36;
            this.label14.Text = "(Select preprocessing strategy for OCR)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tesseractUIParametersBindingSource, "Language", true));
            this.label15.Location = new System.Drawing.Point(560, 93);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 20);
            this.label15.TabIndex = 37;
            this.label15.Text = "label15";
            // 
            // NotifyIconSucess
            // 
            this.NotifyIconSucess.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIconSucess.BalloonTipText = "OCR Finished";
            this.NotifyIconSucess.BalloonTipTitle = "OCR Sucess";
            this.NotifyIconSucess.Text = "NotifySucess";
            this.NotifyIconSucess.Visible = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // mailBox
            // 
            this.mailBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.emailUIParametersBindingSource, "EmailTo", true));
            this.mailBox.Location = new System.Drawing.Point(110, 535);
            this.mailBox.Name = "mailBox";
            this.mailBox.Size = new System.Drawing.Size(446, 26);
            this.mailBox.TabIndex = 38;
            // 
            // emailUIParametersBindingSource
            // 
            this.emailUIParametersBindingSource.DataSource = typeof(Tesseract_UI_Tools.EmailUIParameters);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 538);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 20);
            this.label16.TabIndex = 39;
            this.label16.Text = "Notify Email:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label17.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label17.Location = new System.Drawing.Point(562, 541);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(130, 17);
            this.label17.TabIndex = 40;
            this.label17.Text = "(open email settings)";
            this.label17.Click += new System.EventHandler(this.OpenMailSettingsClick);
            // 
            // ResetLangs
            // 
            this.ResetLangs.AutoSize = true;
            this.ResetLangs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ResetLangs.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ResetLangs.ForeColor = System.Drawing.Color.Red;
            this.ResetLangs.Location = new System.Drawing.Point(98, 94);
            this.ResetLangs.Name = "ResetLangs";
            this.ResetLangs.Size = new System.Drawing.Size(64, 17);
            this.ResetLangs.TabIndex = 41;
            this.ResetLangs.Text = "(deselect)";
            this.ResetLangs.Click += new System.EventHandler(this.ResetLangs_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "Dpi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown1.Location = new System.Drawing.Point(286, 338);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(66, 26);
            this.numericUpDown1.TabIndex = 42;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "Quality", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown2.Location = new System.Drawing.Point(286, 396);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(66, 26);
            this.numericUpDown2.TabIndex = 43;
            this.numericUpDown2.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "MinimumConfidence", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown3.Location = new System.Drawing.Point(286, 460);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(66, 26);
            this.numericUpDown3.TabIndex = 44;
            this.numericUpDown3.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.tesseractUIParametersBindingSource, "DebugPDF", true));
            this.checkBox1.Location = new System.Drawing.Point(286, 509);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(133, 24);
            this.checkBox1.TabIndex = 45;
            this.checkBox1.Text = "Confidence PDF";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 624);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.ResetLangs);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.mailBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.StrategyBox);
            this.Controls.Add(this.ReportsFolderLabel);
            this.Controls.Add(this.MinConfBar);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ResetLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.StatusProgressBar);
            this.Controls.Add(this.StartStopBtn);
            this.Controls.Add(this.ClearBox);
            this.Controls.Add(this.OverwriteBox);
            this.Controls.Add(this.QualityTrackBar);
            this.Controls.Add(this.DpiTrackBar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OutputFolderTextBox);
            this.Controls.Add(this.OutputFolderButton);
            this.Controls.Add(this.InputFolderButton);
            this.Controls.Add(this.InputFolderTextBox);
            this.Controls.Add(this.LanguagesCheckedListBox);
            this.Name = "Main";
            this.Text = "Tesseract UI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tesseractUIParametersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DpiTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinConfBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailUIParametersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private FolderBrowserDialog FolderBrowserDialogInput;
        private CheckedListBox LanguagesCheckedListBox;
        private TextBox InputFolderTextBox;
        private Button InputFolderButton;
        private Button OutputFolderButton;
        private TextBox OutputFolderTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TrackBar QualityTrackBar;
        private TrackBar DpiTrackBar;
        private BindingSource tesseractUIParametersBindingSource;
        private ToolTip ScrollTip;
        private CheckBox OverwriteBox;
        private CheckBox ClearBox;
        private Button StartStopBtn;
        private ProgressBar StatusProgressBar;
        private Label label10;
        private Label StatusLabel;
        private Label ResetLabel;
        private TrackBar MinConfBar;
        private Label label11;
        private Label label12;
        private Label ReportsFolderLabel;
        private ComboBox StrategyBox;
        private Label label13;
        private Label label14;
        private Label label15;
        private NotifyIcon NotifyIconSucess;
        private ErrorProvider errorProvider1;
        private Label label17;
        private Label label16;
        private TextBox mailBox;
        private BindingSource emailUIParametersBindingSource;
        private Label ResetLangs;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
        private CheckBox checkBox1;
    }
}