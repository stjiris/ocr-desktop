using System.ComponentModel;
using OpenCvSharp;
using System.Drawing.Imaging;

namespace Tesseract_UI_Tools
{
    internal class TesseractMainWorker : BackgroundWorker
    {
        TesseractMainWorkerProgressUserState State = new TesseractMainWorkerProgressUserState("Initialized", 0);
        DirectoryInfo Files = Directory.CreateDirectory(Path.Combine(Application.UserAppDataPath, "Files"));
        TesseractUIParameters Params;
        Progress<float> SubProgress = new Progress<float>();

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

        private void SubProgress_ProgressChanged(object? sender, float e)
        {
            Report((int)Math.Floor(e * 100));
        }

        private void Start(object? sender, DoWorkEventArgs e)
        {
            Report("Reading Input Folder", 0);
            foreach (string CurrentFile in Directory.EnumerateFiles(Params.InputFolder))
            {
                ATiffPagesGenerator? Generator = TiffPagesGeneratorProvider.GetTiffPagesGenerator(CurrentFile);
                if (Generator == null) continue;

                string FileName = Path.GetFileNameWithoutExtension(CurrentFile);
                string OutputFile = Path.Combine(Params.OutputFolder, $"{FileName}.dpi-{Params.Dpi}.qual-{Params.Quality}.{Params.Language}.pdf");
                if (File.Exists(OutputFile) && !Params.Overwrite) continue;
                
                DirectoryInfo Tmp = Files.CreateSubdirectory(FileName);
                Report($"Spliting TIFFs of {FileName}", 0);
                string[] Pages = Generator.GenerateTIFFs(Tmp.FullName, Params.Overwrite, SubProgress);

                Report($"Creating JPEGs of {FileName}", 0);
                string[] Jpegs = Generator.GenerateJPEGs(Pages, Tmp.FullName, Params.Dpi, Params.Quality, Params.Overwrite, SubProgress);

                Report($"Creating HOCRs of {FileName}", 0);
                string[] Hocrs = Generator.GenerateHocrs(Pages, Tmp.FullName, Params.GetLanguage(), Params.Overwrite, SubProgress);

                Report($"Creating PDF of {FileName}", 0);
                Generator.GeneratePDF(Jpegs, Hocrs, Pages, OutputFile, SubProgress);
            }
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

        public TesseractMainWorkerProgressUserState(string text, int value)
        {
            Text = text;
            Value = value;
        }
    }
}
