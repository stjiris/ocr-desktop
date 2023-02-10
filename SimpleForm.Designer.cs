namespace Tesseract_UI_Tools
{
    partial class SimpleForm
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
            this.addJobBtn = new System.Windows.Forms.Button();
            this.statusProgressBar = new System.Windows.Forms.ProgressBar();
            this.statusRightLbl = new System.Windows.Forms.Label();
            this.statusLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addJobBtn
            // 
            this.addJobBtn.Location = new System.Drawing.Point(12, 12);
            this.addJobBtn.Name = "addJobBtn";
            this.addJobBtn.Size = new System.Drawing.Size(218, 28);
            this.addJobBtn.TabIndex = 0;
            this.addJobBtn.Text = "Selecionar ficheiro";
            this.addJobBtn.UseVisualStyleBackColor = true;
            this.addJobBtn.Click += new System.EventHandler(this.AddJob);
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Location = new System.Drawing.Point(12, 46);
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(776, 28);
            this.statusProgressBar.TabIndex = 2;
            // 
            // statusRightLbl
            // 
            this.statusRightLbl.AutoSize = true;
            this.statusRightLbl.Location = new System.Drawing.Point(236, 17);
            this.statusRightLbl.Name = "statusRightLbl";
            this.statusRightLbl.Size = new System.Drawing.Size(57, 20);
            this.statusRightLbl.TabIndex = 3;
            this.statusRightLbl.Text = "Estado:";
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.ForeColor = System.Drawing.SystemColors.GrayText;
            this.statusLbl.Location = new System.Drawing.Point(299, 17);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(15, 20);
            this.statusLbl.TabIndex = 6;
            this.statusLbl.Text = "-";
            // 
            // SimpleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 85);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.statusRightLbl);
            this.Controls.Add(this.statusProgressBar);
            this.Controls.Add(this.addJobBtn);
            this.Name = "SimpleForm";
            this.Text = "IRIS OCR Desktop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueueForm_FormClosing);
            this.Load += new System.EventHandler(this.SimpleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button addJobBtn;
        private ProgressBar statusProgressBar;
        private Label statusRightLbl;
        private Label statusLbl;
    }
}