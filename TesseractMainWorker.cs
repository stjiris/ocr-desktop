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
        readonly TesseractMainWorkerProgressUserState State = new("Ready", 0);
        readonly DirectoryInfo Files = Directory.CreateDirectory(Path.Combine(Application.UserAppDataPath, "Files"));
        readonly DirectoryInfo Reports = Directory.CreateDirectory(Path.Combine(Application.UserAppDataPath, "Reports"));
        readonly public Progress<float> SubProgress = new();

        public TesseractMainWorker()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
            SubProgress.ProgressChanged += SubProgress_ProgressChanged;

            DoWork += Start;

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
            VisualReport((int)Math.Floor(e * 100));
        }

        private void Start(object? sender, DoWorkEventArgs e)
        {
            QueueItem? item = e.Argument as QueueItem;
            if ( item == null) throw new Exception("Missing Queue Item");

            TesseractUIParameters Params = item.TesseractParams;
            item.Update(QueueItemStatus.RUNNING);

            AdvancedReportTable report = new(Reports.FullName, Params);
            VisualReport("Reading Input Folder", 0);
            foreach (string CurrentFile in Directory.EnumerateFiles(Params.InputFolder))
            {
                string FileName = Path.GetFileNameWithoutExtension(CurrentFile);
                string OutputFile = Path.Combine(Params.OutputFolder, $"{FileName}.pdf");
                string ReportFile = Path.Combine(Reports.FullName, $"{FileName}.{Uri.EscapeDataString(Params.Language)}.{Params.Strategy}.pdf");
                try
                {
                    if (CancellationPending) break;
                    ATiffPagesGenerator? Generator = TiffPagesGeneratorProvider.GetTiffPagesGenerator(CurrentFile);
                    if (Generator == null || !Generator.CanRun ) continue;

                    if (File.Exists(OutputFile) && !Params.Overwrite) continue;

                    report.StartFile(FileName);
                    Generator.SetProgress(SubProgress);
                    Generator.SetWorker(this);

                    DirectoryInfo Tmp = Files.CreateSubdirectory(FileName);
                    VisualReport($"Spliting TIFFs of {FileName}", 0);
                    string[] Pages = Generator.GenerateTIFFs(Tmp.FullName, Params.Overwrite);
                    report.Pages(Pages.Length);
                    if (CancellationPending) return;

                    VisualReport($"Creating JPEGs of {FileName}", 0);
                    string[] Jpegs = Generator.GenerateJPEGs(Pages, Tmp.FullName, Params.Dpi, Params.Quality, Params.Overwrite);
                    if (CancellationPending) return;

                    VisualReport($"Creating HOCRs of {FileName}", 0);
                    string[] Tsvs = Generator.GenerateTsvs(Pages, Tmp.FullName, Params.GetLanguage(), Params.Strategy, Params.Overwrite);
                    if (CancellationPending) return;

                    VisualReport($"Creating PDF of {FileName}", 0);
                    Generator.GeneratePDF(Jpegs, Tsvs, Pages, OutputFile, Params.MinimumConfidence, false);
                    if (CancellationPending) return;

                    VisualReport($"Generating Report of {FileName}", 0);
                    Generator.GeneratePDF(Jpegs, Tsvs, Pages, ReportFile, 0, true);


                    (int, int, float, float) stats = Generator.GetStatistics(Tsvs, Params.MinimumConfidence);
                    report.Stop(stats.Item1, stats.Item2, stats.Item3, stats.Item4);
            
                    if (Params.Clear && !CancellationPending)
                    {
                        Tmp.Delete(true);
                    }
                }
                catch (Exception ex)
                {
                    report.SetError(ex.InnerException ?? ex);
                }
            }
            report.Close();
            VisualReport("", 0);
            e.Result = report.FullPath;
            item.Update(QueueItemStatus.FINISHED);
        }


        private void VisualReport(string Text, int Value)
        {
            State.Text = Text;
            State.Value = Value;
            VisualReport();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051: Remove unused private member", Justification = "Might be needed..?")]
        private void VisualReport(string Text)
        {
            State.Text = Text;
            VisualReport();
        }
        private void VisualReport(int Value)
        {
            State.Value = Value;
            VisualReport();
        }
        public void VisualReport()
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
            ReportPath = "";
        }
    }
}
