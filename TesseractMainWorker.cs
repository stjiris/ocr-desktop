using System.ComponentModel;
using OpenCvSharp;
using System.Drawing.Imaging;
using Tesseract_UI_Tools.OcrStrategy;
using System.Threading;

namespace Tesseract_UI_Tools
{
    internal class TesseractMainWorker : BackgroundWorker
    {
        // setup state and directories
        TesseractMainWorkerProgressUserState State = new TesseractMainWorkerProgressUserState("Initialized", 0);
        DirectoryInfo Files = Directory.CreateDirectory(Path.Combine(Application.UserAppDataPath, "Files"));
        DirectoryInfo Reports = Directory.CreateDirectory(Path.Combine(Application.UserAppDataPath, "Reports"));
        TesseractUIParameters Params;
        Progress<float> SubProgress = new Progress<float>();

        /// <summary>
        /// Initialize worker with access to <see cref="TesseractUIParameters"/>
        /// </summary>
        /// <param name="Params"></param>
        public TesseractMainWorker(TesseractUIParameters Params)
        {
            this.Params = Params;

            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
            DoWork += Start;

            SubProgress.ProgressChanged += SubProgress_ProgressChanged;

            // Fix XFont problems? https://stackoverflow.com/a/68041013/2573422
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Open file explorer on <see cref="Reports"/> folder
        /// </summary>
        public void OpenReportsFolder()
        {
            System.Diagnostics.Process.Start("explorer.exe", Reports.FullName);
        }


        /// <summary>
        /// Emit subprogress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubProgress_ProgressChanged(object? sender, float e)
        {
            Report((int)Math.Floor(e * 100));
        }

        private void Start(object? sender, DoWorkEventArgs e)
        {
            AdvancedReportTable report = new AdvancedReportTable(Reports.FullName, Params);
            Report("Reading Input Folder", 0);
            foreach (string CurrentFile in Directory.EnumerateFiles(Params.InputFolder))
            {
                string FileName = Path.GetFileNameWithoutExtension(CurrentFile);
                string OutputFile = Path.Combine(Params.OutputFolder, $"{FileName}.pdf");
                string ReportFile = Path.Combine(Reports.FullName, $"{FileName}.{Uri.EscapeDataString(Params.Language)}.{Params.Strategy}.html");
                try
                {
                    if (CancellationPending) break;
                    ATiffPagesGenerator? Generator = TiffPagesGeneratorProvider.GetTiffPagesGenerator(CurrentFile);
                    if (Generator == null || !Generator.CanRun ) continue;

                    if (File.Exists(OutputFile) && !Params.Overwrite) continue;

                    report.StartFile(FileName);
                    DirectoryInfo Tmp = Files.CreateSubdirectory(FileName);
                    Report($"Spliting TIFFs of {FileName}", 0);
                    string[] Pages = Generator.GenerateTIFFs(Tmp.FullName, Params.Overwrite, SubProgress, this);
                    report.Pages(Pages.Length);
                    if (CancellationPending) return;

                    Report($"Creating JPEGs of {FileName}", 0);
                    string[] Jpegs = Generator.GenerateJPEGs(Pages, Tmp.FullName, Params.Dpi, Params.Quality, Params.Overwrite, SubProgress, this);
                    if (CancellationPending) return;

                    Report($"Creating HOCRs of {FileName}", 0);
                    string[] Tsvs = Generator.GenerateTsvs(Pages, Tmp.FullName, Params.GetLanguage(), Params.Strategy, Params.Overwrite, SubProgress, this);
                    if (CancellationPending) return;

                    Report($"Creating PDF of {FileName}", 0);
                    int words = Generator.GeneratePDF(Jpegs, Tsvs, Pages, OutputFile, Params.MinimumConfidence, Params.DebugPDF, SubProgress, this);
                    if (CancellationPending) return;

                    

                    Report($"Generating Report of {FileName}", 0);
                    float meanConf = Generator.GenerateReport(Tsvs, Pages, ReportFile);

                    report.Stop(words, meanConf);

                    if (Params.Clear && !CancellationPending)
                    {
                        Tmp.Delete(true);
                    }
                }
                catch (Exception ex)
                {
                    report.SetError(ex);
                }
            }
            report.Close();
            Report("", 0);
            e.Result = report.FullPath;
        }



        private void Report(string Text, int Value)
        {
            State.Text = Text;
            State.Value = Value;
            Report();
        }
        private void Report(string Text)
        {
            State.Text = Text;
            Report();
        }
        private void Report(int Value)
        {
            State.Value = Value;
            Report();
        }
        public void Report()
        {
            ReportProgress(0, State);
        }
    }

    public class TesseractMainWorkerProgressUserState
    {
        public string Text { get; internal set; }
        public int Value { get; internal set; }
        public string ReportPath { get; internal set; }

        public TesseractMainWorkerProgressUserState(string text, int value)
        {
            Text = text;
            Value = value;
        }
    }
}
