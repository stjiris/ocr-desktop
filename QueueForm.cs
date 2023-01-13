using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tesseract_UI_Tools
{
    public partial class QueueForm : Form
    {
        readonly TesseractUIParameters LastParameters = new();
        readonly EmailUIParameters EmailParams = new();
        readonly BindingList<QueueItem> queue = new();
        readonly TesseractMainWorker worker = new();
        
        public QueueForm()
        {
            InitializeComponent();
            emailUIParametersBindingSource.DataSource = EmailParams;
            queueTable.DataSource = queue;

            worker.ProgressChanged += WorkerProgressUpdate;
            worker.RunWorkerCompleted += WorkerComplete;
            worker.RunWorkerCompleted += RunNextItem;

            worker.VisualReport();
        }

        private void RunNextItem(object? sender, RunWorkerCompletedEventArgs e)
        {
            TryRunNextItem();
        }
        private void TryRunNextItem()
        {
            var next = queue.FirstOrDefault(o => o.Status == QueueItemStatus.CREATED);
            if( next != null && !worker.IsBusy )
            {
                worker.RunWorkerAsync(next);
            }
        }

        private void AddJob(object sender, EventArgs e)
        {
            var tesseractForm = new TesseractForm(LastParameters);
            DialogResult r = tesseractForm.ShowDialog();
            if( r == DialogResult.OK )
            {
                queue.Add(new QueueItem((TesseractUIParameters)LastParameters.Clone()));
                TryRunNextItem();
            }
        }
        private void OpenMailSettingsClick(object sender, EventArgs e)
        {
            var form = new MailSettingsForm(EmailParams);
            form.ShowDialog();
        }

        private void WorkerProgressUpdate(object? sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;

            TesseractMainWorkerProgressUserState State = (TesseractMainWorkerProgressUserState)e.UserState;
            statusLbl.Text = State.Text;
            statusProgressBar.Value = State.Value;
        }

        private void WorkerComplete(object? sender, RunWorkerCompletedEventArgs e )
        {
            statusProgressBar.Value = 0;
            string report;

            if (e.Cancelled)
            {
                report = "User Cancelled";

            }
            else if (e.Error != null)
            {
                report = "Error: " + e.Error.Message;
                System.Diagnostics.Debug.WriteLine("Error! " + e.Error.Message);
                SendMail("OCR Error", e.Error.Message);
                MessageBox.Show(e.Error.ToString(), "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string? ReportPath = e.Result as string;
                if( ReportPath != null && File.Exists(ReportPath))
                {
                    SendMail("OCR Report", File.ReadAllText(ReportPath));
                }
                else if(ReportPath != null)
                {
                    SendMail("OCR Report", $"Worker finnished with the following string: {ReportPath}");
                }
                report = "Terminated";
            }
            statusLbl.Text = report;
        }

        private void SendMail(string sub, string htmlBody)
        {
            EmailUIParameters Params = EmailParams;
            if (Params.EmailTo == "") return;
            try
            {
                var client = new System.Net.Mail.SmtpClient(Params.Host, Params.Port)
                {
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                };
                client.SendCompleted += SendMailCompleted;
                var mail = new System.Net.Mail.MailMessage(Params.EmailFrom, Params.EmailTo)
                {
                    IsBodyHtml = true,
                    Body = htmlBody,
                    Subject = sub
                };
                
                client.SendAsync(mail, null);
                MessageBox.Show($"Smtp client to {client.Host}:{client.Port}\n" +
                    $"From: {mail.From}\n" +
                    $"To: {mail.To}\n" +
                    $"Subject: {mail.Subject}\n");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void SendMailCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs args)
        {
            if (args.Error != null)
            {
                System.Diagnostics.Debug.WriteLine(args.Error.ToString());
                MessageBox.Show(args.Error.ToString(), "Email Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (args.Cancelled)
            {
                System.Diagnostics.Debug.WriteLine("Cancelled");
                MessageBox.Show("Mail cancelled", "Email Cancelled!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OK");
                MessageBox.Show("Mail sent without an error.", "Email Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
