namespace Tesseract_UI_Tools
{
    partial class QueueForm
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
            this.components = new System.ComponentModel.Container();
            this.addJobBtn = new System.Windows.Forms.Button();
            this.statusProgressBar = new System.Windows.Forms.ProgressBar();
            this.statusRightLbl = new System.Windows.Forms.Label();
            this.queueLbl = new System.Windows.Forms.Label();
            this.queueTable = new System.Windows.Forms.DataGridView();
            this.statusLbl = new System.Windows.Forms.Label();
            this.mailSettingsBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.emailUIParametersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.emailRightLbl = new System.Windows.Forms.Label();
            this.buttonFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.queueTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailUIParametersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // addJobBtn
            // 
            this.addJobBtn.Location = new System.Drawing.Point(12, 341);
            this.addJobBtn.Name = "addJobBtn";
            this.addJobBtn.Size = new System.Drawing.Size(218, 28);
            this.addJobBtn.TabIndex = 0;
            this.addJobBtn.Text = "Add Job";
            this.addJobBtn.UseVisualStyleBackColor = true;
            this.addJobBtn.Click += new System.EventHandler(this.AddJob);
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Location = new System.Drawing.Point(12, 409);
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(776, 28);
            this.statusProgressBar.TabIndex = 2;
            // 
            // statusRightLbl
            // 
            this.statusRightLbl.AutoSize = true;
            this.statusRightLbl.Location = new System.Drawing.Point(12, 380);
            this.statusRightLbl.Name = "statusRightLbl";
            this.statusRightLbl.Size = new System.Drawing.Size(52, 20);
            this.statusRightLbl.TabIndex = 3;
            this.statusRightLbl.Text = "Status:";
            // 
            // queueLbl
            // 
            this.queueLbl.AutoSize = true;
            this.queueLbl.Location = new System.Drawing.Point(12, 9);
            this.queueLbl.Name = "queueLbl";
            this.queueLbl.Size = new System.Drawing.Size(55, 20);
            this.queueLbl.TabIndex = 4;
            this.queueLbl.Text = "Queue:";
            // 
            // queueTable
            // 
            this.queueTable.AllowUserToAddRows = false;
            this.queueTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.queueTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.queueTable.Location = new System.Drawing.Point(12, 32);
            this.queueTable.MultiSelect = false;
            this.queueTable.Name = "queueTable";
            this.queueTable.RowHeadersWidth = 49;
            this.queueTable.RowTemplate.Height = 28;
            this.queueTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.queueTable.ShowEditingIcon = false;
            this.queueTable.Size = new System.Drawing.Size(776, 303);
            this.queueTable.TabIndex = 5;
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.ForeColor = System.Drawing.SystemColors.GrayText;
            this.statusLbl.Location = new System.Drawing.Point(70, 380);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(15, 20);
            this.statusLbl.TabIndex = 6;
            this.statusLbl.Text = "-";
            // 
            // mailSettingsBtn
            // 
            this.mailSettingsBtn.Location = new System.Drawing.Point(650, 341);
            this.mailSettingsBtn.Name = "mailSettingsBtn";
            this.mailSettingsBtn.Size = new System.Drawing.Size(138, 28);
            this.mailSettingsBtn.TabIndex = 7;
            this.mailSettingsBtn.Text = "Mail Settings";
            this.mailSettingsBtn.UseVisualStyleBackColor = true;
            this.mailSettingsBtn.Click += new System.EventHandler(this.OpenMailSettingsClick);
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.emailUIParametersBindingSource, "EmailTo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(348, 343);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 26);
            this.textBox1.TabIndex = 8;
            // 
            // emailUIParametersBindingSource
            // 
            this.emailUIParametersBindingSource.DataSource = typeof(Tesseract_UI_Tools.EmailUIParameters);
            // 
            // emailRightLbl
            // 
            this.emailRightLbl.AutoSize = true;
            this.emailRightLbl.Location = new System.Drawing.Point(236, 346);
            this.emailRightLbl.Name = "emailRightLbl";
            this.emailRightLbl.Size = new System.Drawing.Size(106, 20);
            this.emailRightLbl.TabIndex = 9;
            this.emailRightLbl.Text = "Email Address:";
            // 
            // buttonFolder
            // 
            this.buttonFolder.Location = new System.Drawing.Point(650, 375);
            this.buttonFolder.Name = "buttonFolder";
            this.buttonFolder.Size = new System.Drawing.Size(138, 28);
            this.buttonFolder.TabIndex = 10;
            this.buttonFolder.Text = "Reports Folder";
            this.buttonFolder.UseVisualStyleBackColor = true;
            this.buttonFolder.Click += new System.EventHandler(this.buttonFolder_Click);
            // 
            // QueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 441);
            this.Controls.Add(this.buttonFolder);
            this.Controls.Add(this.emailRightLbl);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.mailSettingsBtn);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.queueTable);
            this.Controls.Add(this.queueLbl);
            this.Controls.Add(this.statusRightLbl);
            this.Controls.Add(this.statusProgressBar);
            this.Controls.Add(this.addJobBtn);
            this.Name = "QueueForm";
            this.Text = "Tesseract UI Tools - Queue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueueForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.queueTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailUIParametersBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button addJobBtn;
        private ProgressBar statusProgressBar;
        private Label statusRightLbl;
        private Label queueLbl;
        private DataGridView queueTable;
        private Label statusLbl;
        private Button mailSettingsBtn;
        private TextBox textBox1;
        private Label emailRightLbl;
        private BindingSource emailUIParametersBindingSource;
        private Button buttonFolder;
    }
}