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
        readonly StreamWriter logExceptionsFile = new(Path.Combine(Application.UserAppDataPath, "exceptions.log"), true);

        public QueueForm()
        {
            InitializeComponent();
            emailUIParametersBindingSource.DataSource = EmailParams;
            queueTable.DataSource = queue;

            worker.ProgressChanged += WorkerProgressUpdate;
            worker.RunWorkerCompleted += WorkerComplete;
            worker.RunWorkerCompleted += RunNextItem;

            worker.VisualReport();
            logExceptionsFile.WriteLine($"{DateTime.Now} - New QueueForm");
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
                logExceptionsFile.WriteLine($"{DateTime.Now} - WorkComplete - Error");
                logExceptionsFile.WriteLine(e.Error.ToString());
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
                
                logExceptionsFile.WriteLine($"{DateTime.Now} - SendMail - Info");
                logExceptionsFile.WriteLine($"Host: {client.Host} Port:{client.Port}");
                logExceptionsFile.WriteLine($"From: {mail.From} To:{mail.To}");
                logExceptionsFile.WriteLine($"Subject: {mail.Subject}");
                
                client.SendAsync(mail, null);
            }
            catch (Exception e)
            {
                logExceptionsFile.WriteLine($"{DateTime.Now} - SendMail - Error");
                logExceptionsFile.WriteLine(e);
            }
            try
            {
                GmailProvider.SendMail(Params.EmailTo, sub, htmlBody);
            }
            catch(Exception e)
            {
                logExceptionsFile.WriteLine($"{DateTime.Now} - SendMail - Google Error");
                logExceptionsFile.WriteLine(e);
            }
        }

        private void SendMailCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs args)
        {
            logExceptionsFile.WriteLine($"{DateTime.Now} - SendMailCompleted");
            if (args.Error != null)
            {
                logExceptionsFile.WriteLine(args.Error);
            }
            else if (args.Cancelled)
            {
                logExceptionsFile.WriteLine("Cancelled");
            }
            else
            {
                logExceptionsFile.WriteLine($"No Error");
            }
        }

        private void QueueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            logExceptionsFile.Close();
        }
    }
}
