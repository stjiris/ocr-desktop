using System.Reflection;
using System.Drawing.Imaging;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Xml;
using OpenCvSharp;
using OpenCvSharp.Text;
using System.ComponentModel;

namespace Tesseract_UI_Tools
{
    public abstract class ATiffPagesGenerator
    {
        public static readonly string[] FORMATS = Array.Empty<string>();
        public static readonly string PDF_TAG = "tesseract-ui-tools-generated";

        private readonly EncoderParameters QualityEncoderParameters = new(1);
        protected string FilePath;
        protected bool _CanRun;
        public bool CanRun { get { return _CanRun; } protected set { _CanRun = value; } }

        protected IProgress<float>? Progress;
        public ATiffPagesGenerator(string FilePath)
        {
            this.FilePath = FilePath;
            CanRun = true;
        }

        public void SetProgress(IProgress<float> o) => Progress = o;

        public static string TiffPage(int I)
        {
            return $"{I}.tiff";
        }

        public static string JpegPage(int I, int Dpi, int Quality)
        {
            return $"{I}.{Dpi}.{Quality}.jpeg";
        }
        public static string TsvPage(int I, string Strat, string Languages)
        {
            return $"{I}.{Strat}.{Uri.EscapeDataString(Languages)}.tsv";
        }

        public abstract string[] GenerateTIFFs(string FolderPath, bool Overwrite=false, BackgroundWorker? worker=null);
        public string[] GenerateJPEGs(string[] TiffPages, string FolderPath, int Dpi = 100, int Quality = 100, bool Overwrite=false, BackgroundWorker? worker = null)
        {
            QualityEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Quality);
            ImageCodecInfo JpegImageCodecInfo = ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
            
            string[] JpegPages = new string[TiffPages.Length];

            for (int i = 0; i < TiffPages.Length && (worker == null || !worker.CancellationPending); i++)
            {
                if (Progress != null) Progress.Report((float)i / TiffPages.Length);

                string FullName = Path.Combine(FolderPath, JpegPage(i, Dpi, Quality));
                JpegPages[i] = FullName;
                if (File.Exists(FullName) && !Overwrite) continue;

                using Bitmap Tiff = (Bitmap)Image.FromStream(File.OpenText(TiffPages[i]).BaseStream);
                float Scale = Dpi / Tiff.HorizontalResolution;
                using Bitmap Resize = new(Tiff, new System.Drawing.Size((int)(Tiff.Width * Scale), (int)(Tiff.Height * Scale)));
                Resize.SetResolution(Dpi, Dpi);
                Resize.Save(FullName, JpegImageCodecInfo, QualityEncoderParameters);
            }
            
            return JpegPages;
        }

        public string[] GenerateTsvs(string[] TiffPages, string FolderPath, string[] Languages, string Strategy, bool Overwrite=false, BackgroundWorker? worker = null)
        {
            string[] TsvsPages = new string[TiffPages.Length];
            using AOcrStrategy? OcrStrategy = AOcrStrategy.GetStrategy(Strategy, Languages);
            if (OcrStrategy == null) throw new ArgumentException("Invalid Strategy");
            for (int i = 0; i < TiffPages.Length && (worker == null || !worker.CancellationPending); i++)
            {
                if (Progress != null) Progress.Report((float)i / TiffPages.Length);

                string FullName = Path.Combine(FolderPath, TsvPage(i, Strategy, TessdataUtil.LanguagesToString(Languages)));
                TsvsPages[i] = FullName;
                if (File.Exists(FullName) && !Overwrite) continue;

                OcrStrategy.GenerateTsv(TiffPages[i], TsvsPages[i]);
            }
            return TsvsPages;
        }

        public void GeneratePDF(string[] Jpegs, string[] Tsvs, string[] OriginalTiffs, string OutputFile, float MinConf = 25, bool DebugPDF = false, BackgroundWorker? worker = null)
        {
            PdfDocument doc = new();
            doc.Info.Creator = PDF_TAG;
            
            doc.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression;
            doc.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic;
            doc.Options.NoCompression = false;
            // Defaults to false in debug build,
            // so we set it to true.
            doc.Options.CompressContentStreams = true;

            for (int i = 0; i < Jpegs.Length && (worker == null || !worker.CancellationPending); i++)
            {
                if (Progress != null) Progress.Report((float)i / Jpegs.Length);
                PdfPage Page = doc.AddPage();
                XGraphics g = XGraphics.FromPdfPage(Page);
                using( XImage Jpeg = XImage.FromFile(Jpegs[i]))
                {
                    Page.Width = Jpeg.PixelWidth;
                    Page.Height = Jpeg.PixelHeight;
                    g.DrawImage(Jpeg, 0, 0, Jpeg.PixelWidth, Jpeg.PixelHeight);
                }
                PdfUtil.AddTextLayer(g, Tsvs[i], Jpegs[i], OriginalTiffs[i], MinConf, DebugPDF);
            }
            if((worker == null || !worker.CancellationPending))
            {
                doc.Save(OutputFile);
            }
        }

        internal (int, int, float, float) GetStatistics(string[] Tsvs, float MinConf = 25, BackgroundWorker? worker = null)
        {
            int wordsThresh = 0;
            int wordsTotal = 0;
            float meanConfThresh = 0;
            float meanConfTotal = 0;
            for (int i = 0; i < Tsvs.Length && (worker == null || !worker.CancellationPending); i++)
            {
                if (Progress != null) Progress.Report((float)i / Tsvs.Length);
                float[] confidencesTotal = OCROutput.Load(Tsvs[i]).Confidences;
                float[] confidencesThresh = confidencesTotal.Where(c => c >= MinConf).ToArray();
                wordsTotal += confidencesTotal.Length;
                wordsThresh += confidencesThresh.Length;
                meanConfTotal += confidencesTotal.Sum();
                meanConfThresh += confidencesThresh.Sum();
            }
            return (wordsThresh, wordsTotal, meanConfThresh / wordsThresh, meanConfTotal / wordsTotal);
        }
    }

    public class TiffPagesGeneratorProvider{
        public static ATiffPagesGenerator? GetTiffPagesGenerator(string FilePath)
        {
            string ext = Path.GetExtension(FilePath).ToLower()[1..]; // Remove . (dot)
            Type? Generator = Assembly.GetAssembly(typeof(ATiffPagesGenerator))?.GetTypes()
                .Where(mType => mType.IsClass && !mType.IsAbstract && mType.IsSubclassOf(typeof(ATiffPagesGenerator)))
                .FirstOrDefault(mType => (mType.GetField("FORMATS")?.GetValue(null) as string[] ?? Array.Empty<string>()).Any(forms => forms.Equals(ext)));
            if (Generator == null)
            {
                return null;
            }

            
            return Generator.GetConstructor(new Type[] { typeof(string) })?.Invoke(new object[] { FilePath }) as ATiffPagesGenerator;
        }
    }
}
