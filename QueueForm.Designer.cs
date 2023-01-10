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
            this.addJobBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.statusProgressBar = new System.Windows.Forms.ProgressBar();
            this.statusLbl = new System.Windows.Forms.Label();
            this.queueLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addJobBtn
            // 
            this.addJobBtn.Location = new System.Drawing.Point(698, 12);
            this.addJobBtn.Name = "addJobBtn";
            this.addJobBtn.Size = new System.Drawing.Size(90, 28);
            this.addJobBtn.TabIndex = 0;
            this.addJobBtn.Text = "Add Job";
            this.addJobBtn.UseVisualStyleBackColor = true;
            this.addJobBtn.Click += new System.EventHandler(this.addJobBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(12, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(347, 346);
            this.listBox1.TabIndex = 1;
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Location = new System.Drawing.Point(12, 404);
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(776, 28);
            this.statusProgressBar.TabIndex = 2;
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(12, 381);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(52, 20);
            this.statusLbl.TabIndex = 3;
            this.statusLbl.Text = "Status:";
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
            // QueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 444);
            this.Controls.Add(this.queueLbl);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.statusProgressBar);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.addJobBtn);
            this.Name = "QueueForm";
            this.Text = "Tesseract UI Tools - Queue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button addJobBtn;
        private ListBox listBox1;
        private ProgressBar statusProgressBar;
        private Label statusLbl;
        private Label queueLbl;
    }
}