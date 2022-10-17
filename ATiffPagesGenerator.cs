using System.Reflection;
using System.Drawing.Imaging;
using Tess = Tesseract;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Xml;

namespace Tesseract_UI_Tools
{
    public abstract class ATiffPagesGenerator
    {
        public static string[] FORMATS = new string[] { };

        private EncoderParameters QualityEncoderParameters = new EncoderParameters(1);
        protected string FilePath;
        public ATiffPagesGenerator(string FilePath)
        {
            this.FilePath = FilePath;
        }

        public string TiffPage(int I)
        {
            return $"{I}.tiff";
        }

        public string JpegPage(int I, int Dpi, int Quality)
        {
            return $"{I}.{Dpi}.{Quality}.jpeg";
        }
        public string HocrPage(int I, string Languages)
        {
            return $"{I}.{Languages}.hocr";
        }

        public abstract string[] GenerateTIFFs(string FolderPath, bool Overwrite=false, IProgress<float>? Progress=null);
        public string[] GenerateJPEGs(string[] TiffPages, string FolderPath, int Dpi = 100, int Quality = 100, bool Overwrite=false, IProgress<float>? Progress = null)
        {
            QualityEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Quality);
            ImageCodecInfo JpegImageCodecInfo = ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
            
            string[] JpegPages = new string[TiffPages.Length];

            for (int i = 0; i < TiffPages.Length; i++)
            {
                if (Progress != null) Progress.Report((float)i / TiffPages.Length);

                string FullName = Path.Combine(FolderPath, JpegPage(i, Dpi, Quality));
                JpegPages[i] = FullName;
                if (File.Exists(FullName) && !Overwrite) continue;

                using (Bitmap Tiff = (Bitmap)Image.FromStream(File.OpenText(TiffPages[i]).BaseStream))
                {
                    float Scale = Dpi / Tiff.HorizontalResolution;
                    using (Bitmap Resize = new Bitmap(Tiff, new Size((int)(Tiff.Width * Scale), (int)(Tiff.Height * Scale))))
                    {
                        Resize.SetResolution(Dpi, Dpi);
                        Resize.Save(FullName, JpegImageCodecInfo, QualityEncoderParameters);
                    }
                }
            }
            
            return JpegPages;
        }

        public string[] GenerateHocrs(string[] TiffPages, string FolderPath, string[] Languages, bool Overwrite=false, IProgress<float>? Progress = null)
        {
            string[] HocrPages = new string[TiffPages.Length];
            using (Tess.TesseractEngine engine = TessdataUtil.CreateEngine(Languages))
            {
                for( int i = 0; i < TiffPages.Length; i++)
                {
                    if (Progress != null) Progress.Report((float)i / TiffPages.Length);

                    string FullName = Path.Combine(FolderPath, HocrPage(i, TessdataUtil.LanguagesToString(Languages)));
                    HocrPages[i] = FullName;
                    if (File.Exists(FullName) && !Overwrite) continue;

                    using (Tess.Pix PixImage = Tess.Pix.LoadFromFile(TiffPages[i]))
                    using (Tess.Page ProcessedPage = engine.Process(PixImage, pageSegMode: Tess.PageSegMode.AutoOsd))
                    {
                        File.WriteAllText(FullName, ProcessedPage.GetHOCRText(i, true));
                    }
                }
            }
            return HocrPages;
        }

        public void GeneratePDF(string[] Jpegs, string[] Hocrs, string[] OriginalTiffs, string OutputFile, IProgress<float>? Progress)
        {
            PdfDocument doc = new PdfDocument();
            for (int i = 0; i < Jpegs.Length; i++)
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
                PdfUtil.AddTextLayer(g, Hocrs[i], Jpegs[i], OriginalTiffs[i]);
            }
            doc.Save(OutputFile);
        }
    }

    public class TiffPagesGeneratorProvider{
        public static ATiffPagesGenerator? GetTiffPagesGenerator(string FilePath)
        {
            string ext = Path.GetExtension(FilePath).ToLower().Substring(1); // Remove . (dot)
            Type? Generator = Assembly.GetAssembly(typeof(ATiffPagesGenerator)).GetTypes()
                .Where(mType => mType.IsClass && !mType.IsAbstract && mType.IsSubclassOf(typeof(ATiffPagesGenerator)))
                .FirstOrDefault(mType => ((string[])mType.GetField("FORMATS").GetValue(null)).Any(forms => forms.Equals(ext)));
            if (Generator == null)
            {
                return null;
            }

            
            return (ATiffPagesGenerator)Generator.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { FilePath });
        }
    }
}
