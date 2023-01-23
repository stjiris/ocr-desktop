namespace Tesseract_UI_Tools
{
    partial class MailSettingsForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.emailUIParametersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.GoogleBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.emailUIParametersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.emailUIParametersBindingSource, "EmailFrom", true));
            this.textBox1.Location = new System.Drawing.Point(86, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(395, 26);
            this.textBox1.TabIndex = 0;
            // 
            // emailUIParametersBindingSource
            // 
            this.emailUIParametersBindingSource.DataSource = typeof(Tesseract_UI_Tools.EmailUIParameters);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "From:";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.emailUIParametersBindingSource, "Host", true));
            this.textBox2.Location = new System.Drawing.Point(86, 44);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(253, 26);
            this.textBox2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Host:";
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.emailUIParametersBindingSource, "Port", true));
            this.textBox3.Location = new System.Drawing.Point(363, 44);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(118, 26);
            this.textBox3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = ":";
            // 
            // saveSettingsBtn
            // 
            this.saveSettingsBtn.Location = new System.Drawing.Point(267, 76);
            this.saveSettingsBtn.Name = "saveSettingsBtn";
            this.saveSettingsBtn.Size = new System.Drawing.Size(214, 28);
            this.saveSettingsBtn.TabIndex = 6;
            this.saveSettingsBtn.Text = "Save Settings";
            this.saveSettingsBtn.UseVisualStyleBackColor = true;
            this.saveSettingsBtn.Click += new System.EventHandler(this.SaveSettings);
            // 
            // GoogleBtn
            // 
            this.GoogleBtn.Location = new System.Drawing.Point(267, 110);
            this.GoogleBtn.Name = "GoogleBtn";
            this.GoogleBtn.Size = new System.Drawing.Size(214, 28);
            this.GoogleBtn.TabIndex = 7;
            this.GoogleBtn.Text = "Google";
            this.GoogleBtn.UseVisualStyleBackColor = true;
            this.GoogleBtn.Click += new System.EventHandler(this.GoogleBtn_Click);
            // 
            // MailSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 148);
            this.Controls.Add(this.GoogleBtn);
            this.Controls.Add(this.saveSettingsBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.emailUIParametersBindingSource, "Host", true));
            this.Name = "MailSettingsForm";
            this.Text = "Mail Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MailSettingsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.emailUIParametersBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private BindingSource emailUIParametersBindingSource;
        private Label label2;
        private TextBox textBox3;
        private Label label3;
        private Button saveSettingsBtn;
        private Button GoogleBtn;
    }
}