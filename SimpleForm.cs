using IRIS_OCR_Desktop;
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
    public partial class SimpleForm : Form
    {
        readonly TesseractUIParameters LastParameters = new();
        readonly TesseractMainWorker worker = new();
        readonly StreamWriter logExceptionsFile = new(Path.Combine(Application.UserAppDataPath, "exceptions.log"), true);

        public SimpleForm()
        {
            InitializeComponent();

            LastParameters.Quality = 100;
            LastParameters.Overwrite = true;
            LastParameters.Language = "por";
            LastParameters.Dpi = 150;
            LastParameters.Clear = true;
            LastParameters.MinimumConfidence = 0;
            LastParameters.Strategy = "Fast";

            worker.ProgressChanged += WorkerProgressUpdate;
            worker.RunWorkerCompleted += WorkerComplete;

            worker.VisualReport();
            logExceptionsFile.WriteLine($"{DateTime.Now} - New QueueForm");
        }

        private void AddJob(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                MessageBox.Show("Ficheiro em execução.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            OpenFileDialog ofd = new();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.tiff;*.tif;*.pdf";
            DialogResult r = ofd.ShowDialog();
            if (r == DialogResult.OK)
            {
                TesseractUIParameters next = (TesseractUIParameters)LastParameters.Clone();
                next.InputFile = ofd.FileName;
                worker.RunWorkerAsync(next);
            }
        }

        private void WorkerProgressUpdate(object? sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;

            TesseractMainWorkerProgressUserState State = (TesseractMainWorkerProgressUserState)e.UserState;
            statusLbl.Text = State.Text;
            statusProgressBar.Value = State.Value;
        }

        private void WorkerComplete(object? sender, RunWorkerCompletedEventArgs e)
        {
            statusProgressBar.Value = 0;
            string report = "";

            if (e.Cancelled)
            {
                report = "Cancelado";
            }
            else if (e.Error != null)
            {
                report = "Erro: " + e.Error.Message;
                System.Diagnostics.Debug.WriteLine("Error! " + e.Error.Message);
                logExceptionsFile.WriteLine($"{DateTime.Now} - WorkComplete - Error");
                logExceptionsFile.WriteLine(e.Error.ToString());
            }
            else if (e.Result != null)
            {
                report = "Terminado";
                string[] FilesToSave = e.Result as string[] ?? Array.Empty<string>();
                SaveFileDialog sfd = new();
                foreach (string file in FilesToSave)
                {
                    sfd.FileName = Path.GetFileName(file);
                    sfd.Filter = $"Ficheiro|*{Path.GetExtension(file)}";
                    sfd.Title = "Salvar Ficheiro";
                    DialogResult dr = sfd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        File.Move(file, sfd.FileName, true);
                    }
                }
            }
            statusLbl.Text = report;
        }

        private void QueueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            logExceptionsFile.Close();
        }

        private async void SimpleForm_Load(object sender, EventArgs e)
        {
            await TessdataUtil.Setup();
        }
    }
}
