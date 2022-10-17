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
            ((System.ComponentModel.ISupportInitialize)(this.tesseractUIParametersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DpiTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinConfBar)).BeginInit();
            this.SuspendLayout();
            // 
            // LanguagesCheckedListBox
            // 
            this.LanguagesCheckedListBox.ColumnWidth = 150;
            this.LanguagesCheckedListBox.FormattingEnabled = true;
            this.LanguagesCheckedListBox.Location = new System.Drawing.Point(12, 116);
            this.LanguagesCheckedListBox.MultiColumn = true;
            this.LanguagesCheckedListBox.Name = "LanguagesCheckedListBox";
            this.LanguagesCheckedListBox.Size = new System.Drawing.Size(776, 151);
            this.LanguagesCheckedListBox.TabIndex = 4;
            this.LanguagesCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.LanguagesCheckedListBox_SelectedIndexChanged);
            // 
            // InputFolderTextBox
            // 
            this.InputFolderTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tesseractUIParametersBindingSource, "InputFolder", true));
            this.InputFolderTextBox.Location = new System.Drawing.Point(205, 32);
            this.InputFolderTextBox.Name = "InputFolderTextBox";
            this.InputFolderTextBox.ReadOnly = true;
            this.InputFolderTextBox.Size = new System.Drawing.Size(583, 26);
            this.InputFolderTextBox.TabIndex = 5;
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
            this.InputFolderButton.TabIndex = 6;
            this.InputFolderButton.Text = "Select Input Folder";
            this.InputFolderButton.UseVisualStyleBackColor = true;
            this.InputFolderButton.Click += new System.EventHandler(this.InputFolderClick);
            // 
            // OutputFolderButton
            // 
            this.OutputFolderButton.Location = new System.Drawing.Point(12, 64);
            this.OutputFolderButton.Name = "OutputFolderButton";
            this.OutputFolderButton.Size = new System.Drawing.Size(187, 26);
            this.OutputFolderButton.TabIndex = 7;
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
            this.OutputFolderTextBox.TabIndex = 8;
            this.OutputFolderTextBox.Click += new System.EventHandler(this.OutputFolderClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 9;
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
            this.label2.TabIndex = 10;
            this.label2.Text = "(Select the input folder to process files and output folder for the results)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 11;
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
            this.label4.TabIndex = 12;
            this.label4.Text = "(Select one or more languages that your files may contain)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Output Parameters";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "DPI:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(358, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(430, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "(Affects the width and height of each page. Minimum: 70 Maximum: 300)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 352);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Quality:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label9.Location = new System.Drawing.Point(349, 353);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(439, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "(Affects the JPEG compression of each image. Minimum: 0 Maximum: 100)";
            // 
            // QualityTrackBar
            // 
            this.QualityTrackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "Quality", true));
            this.QualityTrackBar.LargeChange = 10;
            this.QualityTrackBar.Location = new System.Drawing.Point(104, 352);
            this.QualityTrackBar.Maximum = 100;
            this.QualityTrackBar.Name = "QualityTrackBar";
            this.QualityTrackBar.Size = new System.Drawing.Size(239, 53);
            this.QualityTrackBar.SmallChange = 5;
            this.QualityTrackBar.TabIndex = 20;
            this.QualityTrackBar.Tag = "QUALITY";
            this.QualityTrackBar.TickFrequency = 5;
            this.QualityTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.QualityTrackBar.Value = 100;
            this.QualityTrackBar.Scroll += new System.EventHandler(this.TrackBar_Scroll_Tooltip);
            this.QualityTrackBar.MouseHover += new System.EventHandler(this.TrackBar_Scroll_Tooltip);
            // 
            // DpiTrackBar
            // 
            this.DpiTrackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "Dpi", true));
            this.DpiTrackBar.LargeChange = 20;
            this.DpiTrackBar.Location = new System.Drawing.Point(104, 293);
            this.DpiTrackBar.Maximum = 300;
            this.DpiTrackBar.Minimum = 70;
            this.DpiTrackBar.Name = "DpiTrackBar";
            this.DpiTrackBar.Size = new System.Drawing.Size(239, 53);
            this.DpiTrackBar.SmallChange = 5;
            this.DpiTrackBar.TabIndex = 21;
            this.DpiTrackBar.Tag = "DPI";
            this.DpiTrackBar.TickFrequency = 5;
            this.DpiTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.DpiTrackBar.Value = 100;
            this.DpiTrackBar.Scroll += new System.EventHandler(this.TrackBar_Scroll_Tooltip);
            this.DpiTrackBar.MouseHover += new System.EventHandler(this.TrackBar_Scroll_Tooltip);
            // 
            // OverwriteBox
            // 
            this.OverwriteBox.AutoSize = true;
            this.OverwriteBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.tesseractUIParametersBindingSource, "Overwrite", true));
            this.OverwriteBox.Location = new System.Drawing.Point(12, 459);
            this.OverwriteBox.Name = "OverwriteBox";
            this.OverwriteBox.Size = new System.Drawing.Size(92, 24);
            this.OverwriteBox.TabIndex = 26;
            this.OverwriteBox.Text = "Overwrite";
            this.OverwriteBox.UseVisualStyleBackColor = true;
            // 
            // ClearBox
            // 
            this.ClearBox.AutoSize = true;
            this.ClearBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.tesseractUIParametersBindingSource, "Clear", true));
            this.ClearBox.Location = new System.Drawing.Point(110, 459);
            this.ClearBox.Name = "ClearBox";
            this.ClearBox.Size = new System.Drawing.Size(170, 24);
            this.ClearBox.TabIndex = 27;
            this.ClearBox.Text = "Clear Temporary Files";
            this.ClearBox.UseVisualStyleBackColor = true;
            // 
            // StartStopBtn
            // 
            this.StartStopBtn.Location = new System.Drawing.Point(698, 459);
            this.StartStopBtn.Name = "StartStopBtn";
            this.StartStopBtn.Size = new System.Drawing.Size(90, 28);
            this.StartStopBtn.TabIndex = 29;
            this.StartStopBtn.Text = "Start";
            this.StartStopBtn.UseVisualStyleBackColor = true;
            this.StartStopBtn.Click += new System.EventHandler(this.StartStopBtn_Click);
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Location = new System.Drawing.Point(12, 509);
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(776, 28);
            this.StatusProgressBar.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 486);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 20);
            this.label10.TabIndex = 31;
            this.label10.Text = "Status:";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StatusLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.StatusLabel.Location = new System.Drawing.Point(70, 489);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            this.StatusLabel.TabIndex = 32;
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
            this.ResetLabel.TabIndex = 33;
            this.ResetLabel.Text = "(reset params)";
            this.ResetLabel.Click += new System.EventHandler(this.ResetLabel_Click);
            // 
            // MinConfBar
            // 
            this.MinConfBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tesseractUIParametersBindingSource, "MinimumConfidence", true));
            this.MinConfBar.LargeChange = 10;
            this.MinConfBar.Location = new System.Drawing.Point(104, 411);
            this.MinConfBar.Maximum = 100;
            this.MinConfBar.Name = "MinConfBar";
            this.MinConfBar.Size = new System.Drawing.Size(239, 53);
            this.MinConfBar.SmallChange = 5;
            this.MinConfBar.TabIndex = 36;
            this.MinConfBar.Tag = "MIN CONF";
            this.MinConfBar.TickFrequency = 5;
            this.MinConfBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.MinConfBar.Value = 25;
            this.MinConfBar.Scroll += new System.EventHandler(this.TrackBar_Scroll_Tooltip);
            this.MinConfBar.MouseHover += new System.EventHandler(this.TrackBar_Scroll_Tooltip);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(383, 414);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(405, 17);
            this.label11.TabIndex = 35;
            this.label11.Text = "(Minimum confidence allowed for each word when creating the PDF)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(26, 411);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 20);
            this.label12.TabIndex = 34;
            this.label12.Text = "Min Conf:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 549);
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
    }
}