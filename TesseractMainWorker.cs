using System.ComponentModel;
using OpenCvSharp;
using System.Drawing.Imaging;
using IRIS_OCR_Desktop.OcrStrategy;
using System.Threading;

namespace IRIS_OCR_Desktop
{
    internal class TesseractMainWorker : BackgroundWorker
    {
        // setup state and directories
        readonly TesseractMainWorkerProgressUserState State = new("Pronto", 0);
        readonly DirectoryInfo Files = Directory.CreateDirectory(Path.Combine(Application.UserAppDataPath, "Files"));
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
            TesseractUIParameters? Params = e.Argument as TesseractUIParameters;
            if (Params == null) throw new Exception("Parametros em falta.");
            if (!Params.Validate()) throw new Exception("Parametros inválidos.");

            VisualReport("A verificar ficheiro.", 0);
            
            string FileName = Path.GetFileNameWithoutExtension(Params.InputFile);
            string OutputFilePDF = Path.Combine(Files.FullName, $"{FileName}.pdf");
            string OutputFileTXT = Path.Combine(Files.FullName, $"{FileName}.txt");
            

            ATiffPagesGenerator? Generator = TiffPagesGeneratorProvider.GetTiffPagesGenerator(Params.InputFile);
            if (Generator == null || !Generator.CanRun)
            {
                throw new Exception("Não é possível correr este ficheiro.");
            };
            try
            {
                Generator.SetProgress(SubProgress);
                Generator.SetWorker(this);

                DirectoryInfo Tmp = Files.CreateSubdirectory(FileName);
                VisualReport($"A separar páginas", 0);
                string[] Pages = Generator.GenerateTIFFs(Tmp.FullName, Params.Overwrite);

                VisualReport($"A criar camada visual", 0);
                string[] Jpegs = Generator.GenerateJPEGs(Pages, Tmp.FullName, Params.Dpi, Params.Quality, Params.Overwrite);
                if (CancellationPending) return;

                VisualReport($"A criar camada de texto", 0);
                string[] Tsvs = Generator.GenerateTsvs(Pages, Tmp.FullName, Params.GetLanguage(), Params.Strategy, Params.Overwrite);
                if (CancellationPending) return;

                VisualReport($"A criar PDF", 0);
                Generator.GeneratePDF(Jpegs, Tsvs, Pages, OutputFilePDF, Params.MinimumConfidence, false);
                Generator.GenerateTXT(Tsvs, OutputFileTXT, Params.MinimumConfidence);
                if (CancellationPending) return;

                if (Params.Clear && !CancellationPending)
                {
                    Tmp.Delete(true);
                }
            }
            catch (Exception ex)
            {
                VisualReport($"Erro: {ex}", 0);
                throw;
            }
            VisualReport("Terminado", 0);
            e.Result = new string[] { OutputFilePDF, OutputFileTXT };
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
